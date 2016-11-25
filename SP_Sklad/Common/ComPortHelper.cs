using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;

namespace SP_Sklad.Common
{
   public class ComPortHelper
    {
        private SerialPort _serialPort;
        private String received { get; set; }
        private String test { get; set; }
        public decimal weight { get; set; }

        public ComPortHelper()
        {
            _serialPort = new SerialPort();
            _serialPort.DataReceived+= new SerialDataReceivedEventHandler(DataReceivedHandler);

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
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (received != null && received.IndexOf('<') != -1 && received.IndexOf('>') != -1)
            {
                var val = Convert.ToDecimal(Regex.Replace(received, "[^0-9 ]", ""));
                weight = (val / 100);
            }
        }

        public void Close()
        {
            weight = 0;
            if (_serialPort.IsOpen)
            {

                _serialPort.DiscardOutBuffer();
                _serialPort.DiscardInBuffer();
                _serialPort.Close();
            }
        }

        public void Open()
        {
            var list = SerialPort.GetPortNames();
            if (!list.Any())
            {
                return;
            }
          
            _serialPort.Open();
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            received += _serialPort.ReadExisting();
            if (received != null && received.Length >= 10 && received.IndexOf('<') != -1 && received.IndexOf('>') != -1)
            {
                var rez = Regex.Replace(received, "[^0-9 ]", "");
                decimal display;
                if (decimal.TryParse(rez, out display))
                {
                    weight = (display / 100);
                }
                else weight = 0;

                received = "";
            }

         }

    }
}
