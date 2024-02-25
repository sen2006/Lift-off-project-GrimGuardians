using GXPEngine;
using System;
using System.IO.Ports;
using System.Runtime.ExceptionServices;

public class ControllerHandler : GameObject
{
    public enum ControllerMode
    {
        mouse = 0,
        controller = 1
    }

    // controler mode:
    public static ControllerMode controllerMode = ControllerMode.controller;

    float LRAxisMin;
    float LRAxisMax;

    float UDAxisMin;
    float UDAxisMax;

    bool calibrated = false;
    int calibrationStep = 0;
    String calibrationText = "";
    EasyDraw calibrationUI;

    public Cursor cursor;

    SerialPort port;
    float yaw;
    float pitch;
    float roll;

    float yawover = 0;
    float yawCorrection = 3600;
    float totalYaw;

    float cursorX;
    float cursorY;

    bool trigger = false;
    bool firstBarrel = false;
    bool secondBarrel = false;
    bool reload = false;
    bool switchAmmo = false;
    bool throwGrenade = false;

    bool isWasTrigger=false;
    bool wasGrenadeThrown = false;

    public ControllerHandler()
    {
        try
        {
            if (controllerMode == ControllerMode.mouse)
            {
                Console.WriteLine("controlerMode = mouse");
                calibrated = true;
                return;
            }
            // establish connection with the controller on COM3 port 57000

            // TODO check all COM's for the controller instead of onty COM3
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
            // if no controller is found or if conection fails, go into mouse mode
            Console.WriteLine("could not find controler");
            Console.WriteLine("switching controlerMode to mouse");
            controllerMode = ControllerMode.mouse;
            calibrated = true;
        }
    }

    /// <summary>
    /// DEPRICATED
    /// </summary>
    public float getYaw() { return totalYaw; }
    /// <summary>
    /// DEPRICATED
    /// </summary>
    public float getPitch() { return pitch; }
    /// <summary>
    /// DEPRICATED
    /// </summary>
    public float getRoll() { return roll; }

    public bool isCalibrated() { return calibrated; }

    void Update()
    {
        if (controllerMode == ControllerMode.controller)
        {
            string line = port.ReadLine();
            if (line != "")
            {
                // read data from controller
                String[] values = line.Split(',');

                // check if the yaw was in the low or high areas
                bool highYaw = yaw > 300;
                bool lowYaw = yaw < 60;

                // read data from the list
                yaw = float.Parse(values[0]);
                pitch = float.Parse(values[1]);
                roll = float.Parse(values[2]);
                trigger = float.Parse(values[3]) > 0;
                firstBarrel = float.Parse(values[4]) > 0;
                secondBarrel = float.Parse(values[5]) > 0;
                reload = float.Parse(values[6]) > 0;
                switchAmmo = float.Parse(values[7]) > 0;
                throwGrenade = float.Parse(values[7]) > 0; //float.Parse(values[7]) > 0;
                // TODO add check if barrel is closed on 8       float.Parse(values[8]) > 0;

                // if yaw went over the 360 or below 0 make it keep going
                if (highYaw && yaw < 60) { yawover += 360; }
                if (lowYaw && yaw > 300) { yawover -= 360; }

                // yaw correction 
                // TODO make this more accurate
                yawCorrection -= Time.deltaTime / 3740f;

                // add all yaws together
                totalYaw = yaw + yawover + yawCorrection;

                // DEBUG TEXT
                //Console.WriteLine("Read from port: " + line);
                //Console.WriteLine("Set Yaw:" + yaw);
                //Console.WriteLine("totalYaw is:" + totalYaw);
                //Console.WriteLine("Set Pitch:" + pitch);
                //Console.WriteLine("Set Roll:" + roll);
                //Console.WriteLine("Set trigger:" + trigger);

                if (!calibrated)
                {
                    calibrate();
                }
                else
                {
                    // set the cursor to the correct position acording to the calibration
                    cursorX = game.width - (totalYaw - LRAxisMax) / (LRAxisMin - LRAxisMax) * game.width;
                    cursorY = game.height - (pitch - UDAxisMax) / (UDAxisMin - UDAxisMax) * game.height;
                } 
            }
            else { Console.WriteLine("ERROR cant read data from port"); } // error message
        } else
        {
            // set the cursor to the mouse
            cursorX = Input.mouseX; 
            cursorY = Input.mouseY;
            trigger = Input.GetMouseButtonDown(0);
            firstBarrel = Input.GetKeyUp(Key.R);
            secondBarrel = Input.GetKeyUp(Key.R);
            switchAmmo = Input.GetKeyUp(Key.X);
            throwGrenade = Input.GetKeyUp(Key.G);
            

            // TODO add the other controlls to the keyboard
            
        }

        // DEBUG TEXT
        //Console.WriteLine("cursorX:" + cursorX);
        //Console.WriteLine("cursorY:" + cursorY);

        if (calibrated)
        {
            if (cursor == null)
            {
                // add the cursor to the engine
                cursor = new Cursor();
                this.game.AddChild(cursor);
            }
            cursor.SetXY(lerp(cursor.x, cursorX, .2f), lerp(cursor.y, cursorY, .2f));
            if (trigger)
            {
                if (!isWasTrigger)
                {
                    // fire a shell when trigger is pressed (runs only once)
                    cursor.fire();
                    isWasTrigger = true;
                }
            }
            else isWasTrigger = false;

            if(firstBarrel)
            {
                cursor.ReloadOne();
            }

            if(secondBarrel)
            {
                cursor.ReloadTwo();
            }

            if(switchAmmo)
            {
                cursor.AmmoSwitch();
            }

            if (throwGrenade)
            {
                if (!wasGrenadeThrown)
                {
                    cursor.ThrowGrenade();
                    wasGrenadeThrown = true;
                }
                else
                {
                    wasGrenadeThrown = false;
                }
            }
        }
    }

    /// <summary>
    /// Calibrates the cursor to the movement of the game controler.
    /// </summary>
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
                calibrationUI=null;
                return;
        }
        calibrationUI.Clear(0);
        calibrationUI.TextAlign(CenterMode.Center, CenterMode.Center);
        calibrationUI.Text(calibrationText, calibrationUI.width/2, calibrationUI.height/2);
    }

    float lerp(float a, float b, float f)
    {
        return a + f * (b - a);
    }
}
