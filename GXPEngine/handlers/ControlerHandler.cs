using GXPEngine;
using GXPEngine.Core;
using System;
using System.Drawing;
using System.IO.Ports;
using System.Runtime.ConstrainedExecution;
public class ControlerHandler : GameObject
{
    public enum ControlerMode
    {
        mouse = 0,
        cotroler = 1
    }

    // controler mode:
    public static ControlerMode controlerMode = ControlerMode.cotroler;

    float LRAxisMin;
    float LRAxisMax;

    float UDAxisMin;
    float UDAxisMax;

    bool calibrated = false;
    int calibrationStep = 0;
    String calibrationText = "";
    EasyDraw calibrationUI;

    Cursor cursor;

    SerialPort port;
    float yaw;
    float pitch;
    float roll;

    int yawover = 3600;
    float totalYaw;

    float cursorX;
    float cursorY;

    bool trigger = false;
    bool isWasTrigger=false;

    public ControlerHandler()
    {
        try
        {
            if (controlerMode == ControlerMode.mouse)
            {
                Console.WriteLine("controlerMode = mouse");
                calibrated = true;
                return;
            }
            Console.WriteLine("controlerMode = controler");
            port = new SerialPort();
            port.PortName = "COM3";
            port.BaudRate = 57600;
            port.RtsEnable = true;
            port.DtrEnable = true;
            port.Open();
        }
        catch
        {
            Console.WriteLine("could not find controler");
            Console.WriteLine("switching controlerMode to mouse");
            controlerMode = ControlerMode.mouse;
            calibrated = true;
        }
    }

    public float getYaw() { return totalYaw; }
    public float getPitch() { return pitch; }
    public float getRoll() { return roll; }

    public void Update()
    {
        if (controlerMode == ControlerMode.cotroler)
        {
            string line = port.ReadLine();
            if (line != "")
            {
                Console.WriteLine("Read from port: " + line);
                String[] values = line.Split(',');

                bool highYaw = yaw > 300;
                bool lowYaw = yaw < 60;

                yaw = float.Parse(values[0]);
                pitch = float.Parse(values[1]);
                roll = float.Parse(values[2]);

                if (highYaw && yaw < 60) { yawover += 360; }
                if (lowYaw && yaw > 300) { yawover -= 360; }
                totalYaw = yaw + yawover;
                //while (totalYaw<0) { yawover += 360; totalYaw = yaw + yawover; ; }

                

                Console.WriteLine("Set Yaw:" + yaw);
                Console.WriteLine("totalYaw is:" + totalYaw);
                Console.WriteLine("Set Pitch:" + pitch);
                Console.WriteLine("Set Roll:" + roll);

                trigger = float.Parse(values[3]) > 0;

                Console.WriteLine("Set trigger:" + trigger);

                if (!calibrated)
                {
                    calibrate();
                }
                else
                {
                    cursorX = game.width - (totalYaw - LRAxisMax) / (LRAxisMin - LRAxisMax) * game.width;
                    cursorY = game.height - (pitch - UDAxisMax) / (UDAxisMin - UDAxisMax) * game.height;
                } 
            }
            else { Console.WriteLine("cant read data from port"); }
        } else
        {
            cursorX = Input.mouseX; 
            cursorY = Input.mouseY;
            trigger = Input.GetMouseButtonDown(0);
            
        }

        //Console.WriteLine("cursorX:" + cursorX);
        //Console.WriteLine("cursorY:" + cursorY);

        if (calibrated)
        {
            if (cursor == null)
            {
                cursor = new Cursor();
                this.game.AddChild(cursor);
            }
            cursor.SetXY(cursorX, cursorY);
            if (trigger)
            {
                if (!isWasTrigger)
                {
                    cursor.fire();
                    isWasTrigger = true;
                }
            }
            else isWasTrigger = false;
        }
    }

    void calibrate() 
    {   
        MyGame game = MyGame.GetGame();
        if (calibrationUI == null)
        {
            calibrationUI = new EasyDraw(game.width, game.height);
            game.AddChild(calibrationUI);
        }
        bool triggered = false;
        if (trigger)
        {
            if (!isWasTrigger)
            {
                triggered = true;
                isWasTrigger = true;
            }
        }
        else isWasTrigger = false;



        switch (calibrationStep)
        {
            case 0: 
                calibrationText = "please point at the top left of the screen and press trigger";
                if (triggered) { LRAxisMin = totalYaw; UDAxisMin = pitch; calibrationStep++; }
                break;
            case 1:
                calibrationText = "please point at the bottom right of the screen and press trigger";
                if (triggered) { LRAxisMax = totalYaw; UDAxisMax = pitch; calibrationStep++; }
                break;
            case 2:
                calibrationText = "calibration done";
                calibrated = true;
                calibrationUI.Destroy();
                break;
        }
        calibrationUI.Clear(0);
        calibrationUI.Text(calibrationText);
    }
}
