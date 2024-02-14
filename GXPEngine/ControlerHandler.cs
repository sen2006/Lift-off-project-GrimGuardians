using System;
using System.Drawing;
using System.IO.Ports;
using System.Runtime.ConstrainedExecution;
public class ControlerHandler
{
    SerialPort port;
    float yaw;
    float pitch;
    float roll;

    public ControlerHandler(bool controlerActive)
    {
        if (!controlerActive) { return; }
        port = new SerialPort();
        port.PortName = "COM3";
        port.BaudRate = 57600;
        port.RtsEnable = true;
        port.DtrEnable = true;
        port.Open();
    }

    public float getYaw() { return yaw; }
    public float getPitch() { return pitch; }
    public float getRoll() { return roll; }

    public void Update()
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
    }

}
