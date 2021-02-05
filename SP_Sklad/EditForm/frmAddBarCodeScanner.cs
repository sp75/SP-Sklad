using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SP_Sklad.SkladData;
using RawInput_dll;
using System.Globalization;
using SP_Sklad.Properties;

namespace SP_Sklad.ViewsForm
{
    public partial class frmAddBarCodeScanner : DevExpress.XtraEditors.XtraForm
    {
        private readonly RawInput _rawinput;
        public string DeviceName { get; set; }
        public frmAddBarCodeScanner()
        {
            InitializeComponent();

            _rawinput = new RawInput(Handle, true);
            _rawinput.KeyPressed += OnKeyPressed;
        }

        private void OnKeyPressed(object sender, RawInputEventArg e)
        {
            lbHandle.Focus();

            if (e.KeyPressEvent.VKey == 13 && e.KeyPressEvent.Message == Win32.WM_KEYUP)
            {
                lbHandle.Text = e.KeyPressEvent.DeviceHandle.ToString();
                lbType.Text = e.KeyPressEvent.DeviceType;
                lbName.Text = e.KeyPressEvent.DeviceName;
                lbDescription.Text = e.KeyPressEvent.Name;
                lbSource.Text = e.KeyPressEvent.Source;

                Settings.Default.barcode_scanner_name = e.KeyPressEvent.DeviceName;
                DeviceName = e.KeyPressEvent.DeviceName;
            }
        }

        private void frmSetDiscountCard_Shown(object sender, EventArgs e)
        {
          
        }


        private void frmAddBarCodeScanner_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_rawinput != null) _rawinput.KeyPressed -= OnKeyPressed;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Settings.Default.barcode_scanner_name = DeviceName;
            Settings.Default.Save();
        }
    }
}