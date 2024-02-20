using SP_Sklad.Common;
using SP_Sklad.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP_Sklad.ViewsForm
{
    public partial class frmTestComPort : DevExpress.XtraEditors.XtraForm
    {
        private SerialPort _serialPort;
        private String received { get; set; }

        public UserSettingsRepository user_settings { get; set; }
        System.IO.Stream log_stream = new System.IO.MemoryStream();

        public frmTestComPort(string port_name, int baud_rate)
        {
            InitializeComponent();

            
            _serialPort = new SerialPort();
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
   
            // Allow the user to set the appropriate properties.
            _serialPort.PortName = port_name;// Settings.Default.com_port_name;// "COM1";
            _serialPort.BaudRate = baud_rate;// Convert.ToInt32(Settings.Default.com_port_speed); // 4800;


            _serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), "None", true); ;
            _serialPort.DataBits = 8;
            _serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "One", true);
            _serialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), "None", true);

            // Set the read/write timeouts
            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            string read_existing = _serialPort.ReadExisting();

            received += read_existing;
        }

        private void frmTestComPort_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();

            if (_serialPort.IsOpen)
            {

                _serialPort.DiscardOutBuffer();
                _serialPort.DiscardInBuffer();
                _serialPort.Close();
            }
        }

        private void frmTestComPort_Shown(object sender, EventArgs e)
        {
            var list = SerialPort.GetPortNames();
            if (!list.Any())
            {
                MessageBox.Show("Не знайдено жодного СОМ порта!");
                return;
            }

            try
            {
                _serialPort.Open();

                if (_serialPort.IsOpen)
                {

                    _serialPort.DiscardOutBuffer();
                    _serialPort.DiscardInBuffer();
                }
            }
            catch
            {

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ComPortText.Text = received;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fs = File.Create(saveFileDialog1.FileName))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(received);
                    fs.Write(info, 0, info.Length);
                }
            }
        }
    }
}
