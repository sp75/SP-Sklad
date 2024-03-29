using SP.Base.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        private SerialPort _serialPort;
        private String received { get; set; }
        private String test { get; set; }
        public decimal weight { get; set; }

        public Form1()
        {
            InitializeComponent();
            weight = 0;

            _serialPort = new SerialPort();
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            // Allow the user to set the appropriate properties.
            _serialPort.PortName = "COM1";
            _serialPort.BaudRate = 9600; // 4800;
            _serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), "None", true); ;
            _serialPort.DataBits = 8;
            _serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "One", true);
            _serialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), "None", true);

            // Set the read/write timeouts
            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;
            _serialPort.Encoding = new ASCIIEncoding();       
        }

        public void ClosePort()
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
                MessageBox.Show("Не знайдено жодного СОМ порта!");
                return;
            }

    //        _serialPort.Open();
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            received += _serialPort.ReadExisting();
          

    //        listBox1.Items.Add(_serialPort.ReadByte().ToString("X2"));
      //      textBox1.Text = received;

           if (received != null)
            {
                //   String ss = "R01W  12.56 R01W";
                int amount = new Regex("R01W").Matches(received).Count;
                if (amount >= 2)
                {
                    var sp = received.Split(new[] { "R01W" }, StringSplitOptions.RemoveEmptyEntries);
                    if (sp.Count() >= 1)
                    {
                        decimal display;
                        if (decimal.TryParse(sp[0].Trim(), System.Globalization.NumberStyles.Number | System.Globalization.NumberStyles.AllowCurrencySymbol, CultureInfo.CreateSpecificCulture("en-GB"), out display))
                        {
                            weight = display;
                        }
                        else weight = 0;

                    }
                }
            }

           textBox1.Text = Convert.ToString( weight);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Open();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("sdsdsdsd");

            using (var db = new SPBaseModel("SPBaseModel"))
            {
                db.RemoteCustomerOrders.Add(new RemoteCustomerOrders
                {
                    Amount = 1,
                    CreatedAt = DateTime.Now,
                    MatId = 1,
                    PosId = 1,
                    WbillId =1,
                    CustomerId = Guid.NewGuid()
                });

                db.SaveChanges();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClosePort();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            received = ".56R01W  12.56 R01W 12";
            var R01W = new Regex("R01W").Matches(received);
          //  int amount = new Regex("R01W").Matches(received).Count;
            if (R01W.Count >= 2)
            {
                string str_weight = received.Substring(R01W[0].Index + R01W[0].Length, R01W[1].Index - R01W[0].Index - R01W[1].Length).Trim();

            //    var sp = received.Split(new[] { "R01W" }, StringSplitOptions.RemoveEmptyEntries);
            //    if (sp.Count() >= 1)
          //      {
                    decimal display;
                    if (decimal.TryParse(str_weight, System.Globalization.NumberStyles.Number | System.Globalization.NumberStyles.AllowCurrencySymbol, CultureInfo.CreateSpecificCulture("en-GB"), out display))
                    {
                        weight = display;
                    }
                    else weight = 0;

                   
                    received = "";

                    return;
             //   }
            }
        }
    }
}
