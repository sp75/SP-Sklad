using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_Sklad.Common
{
   public class ComPortHelper
    {
        private bool _continue;
        private SerialPort _serialPort;

        public ComPortHelper()
        {
            //   string name;
            //      string message;
            //      StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;
            //  Thread readThread = new Thread(Read);

            // Create a new SerialPort object with default settings.
            _serialPort = new SerialPort();

            // Allow the user to set the appropriate properties.
            _serialPort.PortName = "COM1";
            _serialPort.BaudRate = 4800;
            _serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), "None", true); ;
            _serialPort.DataBits = 8;
            _serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "One", true);
            _serialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), "None", true);

            // Set the read/write timeouts
            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;

           
            //      _continue = true;
            //     readThread.Start();

            /*        Console.Write("Name: ");
                    name = Console.ReadLine();

                    Console.WriteLine("Type QUIT to exit");

                    while (_continue)
                    {
                        message = Console.ReadLine();

                        if (stringComparer.Equals("quit", message))
                        {
                            _continue = false;
                        }
                        else
                        {
                            _serialPort.WriteLine(
                                String.Format("<{0}>: {1}", name, message));
                        }
                    }*/

            //    readThread.Join();
            //      _serialPort.Close();
        }

        public void Close()
        {
            _serialPort.Close();
        }

        public void Open()
        {
            var list = SerialPort.GetPortNames();
            if (!list.Any())
            {
                return;
            }

            _serialPort.Open();
            _serialPort.DiscardInBuffer();
            _serialPort.DiscardOutBuffer();
        }

        public String ReadData()
        {
            if (!_serialPort.IsOpen && _serialPort.BytesToRead >= 16)
            {
                return "";
            }

            return _serialPort.ReadExisting();
        }

        public void WriteText()
        {
            _serialPort.WriteLine(String.Format("<{0}>: {1}", "Test", "145.56"));
        }
    }
}
