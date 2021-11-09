using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using ScalesCOMServerLib;
using SP_Sklad.Properties;
using SP_Sklad.SkladData;

namespace SP_Sklad.Common
{
    public class ComPortHelper
    {
        private SerialPort _serialPort;
        private String received { get; set; }
        public decimal weight { get; set; }
        public WeighingScales weighing_scales { get; set; }
        private ScalesServerBTA _bta { get; set; }

        public ComPortHelper()
            : this(1)
        {
        }

        public ComPortHelper(int weigher_index)
        {
            _serialPort = new SerialPort();
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            // Allow the user to set the appropriate properties.

            if (weigher_index == 1)
            {
                _serialPort.PortName = Settings.Default.com_port_name;
                _serialPort.BaudRate = Convert.ToInt32(Settings.Default.com_port_speed);
                weighing_scales = new BaseEntities().WeighingScales.FirstOrDefault(f => f.Id == Settings.Default.weighing_scales);
            }
            else if (weigher_index == 2)
            {
                _serialPort.PortName = Settings.Default.com_port_name_2;
                _serialPort.BaudRate = Convert.ToInt32(Settings.Default.com_port_speed_2);
                weighing_scales = new BaseEntities().WeighingScales.FirstOrDefault(f => f.Id == Settings.Default.weighing_scales_2);
            }


            _serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), "None", true); ;
            _serialPort.DataBits = 8;
            _serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "One", true);
            _serialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), "None", true);

            // Set the read/write timeouts
            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;

            if (weighing_scales.ExternalLibrary == "ScalesCOMServer.dll")
            {
                _bta = new ScalesServerBTA();
            }
        }

        public void Dispose()
        {
            _serialPort.Dispose();
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

            if (weighing_scales.ExternalLibrary == "ScalesCOMServer.dll")
            {
                _bta.CloseComPort(out int close_a);
            }
        }

        public void Open()
        {
            var list = SerialPort.GetPortNames();
            if (!list.Any())
            {
                MessageBox.Show("Не знайдено жодного СОМ порта!");
                return;
            }

            if (weighing_scales.ExternalLibrary == "ScalesCOMServer.dll")
            {
                _bta.OpenComPort(_serialPort.PortName, out int a);
                _bta.GetInfo(out int w, out int p, out int c, out int err);
                weight = Convert.ToDecimal(w) / Convert.ToDecimal(weighing_scales.AccuracyScales);
            }
            else
            {
                _serialPort.Open();
            }
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            string read_existing = _serialPort.ReadExisting();

            received += read_existing;

       //     weight = GetWeight(received);

            /*   try
                 {
                     File.AppendAllText(Path.Combine(Application.StartupPath, "com_port.log"), read_existing);
                     read_existing = "";
                 }
                 catch
                 {
                     ;
                 }*/

            if (received != null && received.Length >= 10 && received.IndexOf('<') != -1 && received.IndexOf('>') != -1)
            {
                var rez = Regex.Replace(received, "[^0-9 ]", "");

                if (decimal.TryParse(rez, out decimal display))
                {
                    weight = (display / 100);
                }
                else weight = 0;

                received = "";

                return;
            }

            if (received != null)
            {
                //   String ss = "R01W  12.56 R01W";

                var R01W = new Regex("R01W").Matches(received);
                if (R01W.Count >= 2)
                {
                    string str_weight = received.Substring(R01W[0].Index + R01W[0].Length, R01W[1].Index - R01W[0].Index - R01W[1].Length).Trim();

                    decimal display;
                    if (decimal.TryParse(str_weight, NumberStyles.Number | NumberStyles.AllowCurrencySymbol, CultureInfo.CreateSpecificCulture("en-GB"), out display))
                    {
                        weight = display;
                    }
                    else weight = 0;

                    received = "";

                    return;
                }
            }

            if (received != null)
            {
                //   String ss = "=00535.0(kg)";

                int amount = new Regex("=").Matches(received).Count;
                int amount2 = new Regex("(kg)").Matches(received).Count;
                if (amount >= 1 && amount2 >= 1)
                {
                    var sp = received.Split(new[] { "=", "(kg)" }, StringSplitOptions.RemoveEmptyEntries);
                    if (sp.Any())
                    {
                        var number = sp[0].Trim().Replace(',', '.');
                        if (decimal.TryParse(number, NumberStyles.AllowDecimalPoint, CultureInfo.CreateSpecificCulture("en-US"), out decimal display))
                        {
                            weight = display;
                        }
                        else
                        {
                            weight = 0;
                        }
                        received = "";

                        return;
                    }
                }
            }

        }

        private decimal GetWeight( string received_text)
        {
            if (string.IsNullOrEmpty(received_text))
            {
                return 0;
            }

            int amount = new Regex(weighing_scales.Prefix).Matches(received_text).Count;
            int amount2 = new Regex(weighing_scales.Suffix).Matches(received_text).Count;
            if (amount >= 1 && amount2 >= 1)
            {
                received = "";

                var sp = received_text.Split(new[] { weighing_scales.Prefix, weighing_scales.Suffix }, StringSplitOptions.RemoveEmptyEntries);
                if (sp.Any())
                {
                    var number = Regex.Replace(sp[0].Trim().Replace(',', '.'), @"[^\d\.,]", "");
                    if (decimal.TryParse(number, NumberStyles.AllowDecimalPoint, CultureInfo.CreateSpecificCulture("en-US"), out decimal display))
                    {
                        return display / Convert.ToDecimal( weighing_scales.AccuracyScales);
                    }
                }
            }

            return 0;
        }

    }
}
