using System;
using System.IO.Ports;
using System.Runtime.ConstrainedExecution;
public class ControlerHandler
{
    float yaw;
    float pitch;
    float roll;

    public ControlerHandler(bool controlerActive)
    {
        if (!controlerActive) { return; }
        SerialPort port = new SerialPort();
        port.PortName = "COM4";
        port.BaudRate = 57600;
        port.RtsEnable = true;
        port.DtrEnable = true;
        port.Open();
        while (true)
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
            } else { Console.WriteLine("cant read data from port"); }
        }
    }

    public float getYaw() {  return yaw; }
    public float getPitch() { return pitch; }
    public float getRoll() { return roll; }

}
/*
 * 
*/
