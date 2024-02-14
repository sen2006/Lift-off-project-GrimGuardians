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
    public static ControlerMode controlerMode = ControlerMode.mouse;

    Cursor cursor;

    SerialPort port;
    float yaw;
    float pitch;
    float roll;

    float cursorX;
    float cursorY;

    public ControlerHandler()
    {
        try
        {
            if (controlerMode == ControlerMode.mouse)
            {
                Console.WriteLine("controlerMode = mouse");
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
        }
    }

    public float getYaw() { return yaw; }
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

                yaw = float.Parse(values[0]);
                pitch = float.Parse(values[1]);
                roll = float.Parse(values[2]);

                Console.WriteLine("Set Yaw:" + yaw);
                Console.WriteLine("Set Pitch:" + pitch);
                Console.WriteLine("Set Roll:" + roll);

            }
            else { Console.WriteLine("cant read data from port"); }
        } else
        {
            cursorX = Input.mouseX; 
            cursorY = Input.mouseY;
        }

        if (cursor==null) {
            cursor = new Cursor();
            this.game.AddChild(cursor);
        } else cursor.SetXY(cursorX, cursorY);
    }

}
