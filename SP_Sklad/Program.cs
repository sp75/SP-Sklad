using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Data.Filtering;
using DevExpress.Skins;
using DevExpress.UserSkins;
using SP_Sklad.SkladData;

namespace SP_Sklad
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Create a new object, representing the German culture. 
        //    CultureInfo culture = CultureInfo.CreateSpecificCulture("uk-UA");

            // The following line provides localization for the application's user interface. 
         //   Thread.CurrentThread.CurrentUICulture = culture;

            // The following line provides localization for data formats. 
        //    Thread.CurrentThread.CurrentCulture = culture;

            // Set this culture as the default culture for all threads in this application. 
            // Note: The following properties are supported in the .NET Framework 4.5+
       //     CultureInfo.DefaultThreadCurrentCulture = culture;
        //    CultureInfo.DefaultThreadCurrentUICulture = culture;

            // Add the event handler for handling UI thread exceptions to the event.
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

            // Set the unhandled exception mode to force all Windows Forms errors to go through
            // our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Add the event handler for handling non-UI thread exceptions to the event. 
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            Application.Run(new frmLogin());

            // Stop the application and all the threads in suspended state.
            Environment.Exit(-1);
        }

        // All exceptions thrown by the main thread are handled over this method
        // Handle the UI exceptions by showing a dialog box, and asking the user whether
        // or not they wish to abort execution.
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {

            DialogResult result = DialogResult.Cancel;
            try
            {
                result = ShowThreadExceptionDialog("Windows Forms Error", e.Exception);
            }
            catch
            {
                try
                {
                    MessageBox.Show("Fatal Windows Forms Error",
                        "Fatal Windows Forms Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }

            // Exits the program when the user clicks Abort.
            if (result == DialogResult.Abort)
                Application.Exit();

        }

        // All exceptions thrown by additional threads are handled in this method
        // Handle the UI exceptions by showing a dialog box, and asking the user whether
        // or not they wish to abort execution.
        // NOTE: This exception cannot be kept from terminating the application - it can only 
        // log the event, and inform the user about it. 
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                string errorMsg = "An application error occurred. Please contact the adminstrator " +
                    "with the following information:\n\n";

                // Since we can't prevent the app from terminating, log this to the event log.
                if (!EventLog.SourceExists("ThreadException"))
                {
                    EventLog.CreateEventSource("ThreadException", "Application");
                }

                // Create an EventLog instance and assign its source.
                EventLog myLog = new EventLog();
                myLog.Source = "ThreadException";
                myLog.WriteEntry(errorMsg + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace);
            }
            catch (Exception exc)
            {
                try
                {
                    MessageBox.Show("Fatal Non-UI Error. Could not write the error to the event log. Reason: "
                        + exc.Message, "Fatal Non-UI Error",
                         MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }
        }

        // Creates the error message and displays it.
        private static DialogResult ShowThreadExceptionDialog(string title, Exception e)
        {
            string errorMsg = "Сталася помилка в програмі. Будь ласка, зверніться до адміністратора з наступною інформацією:\n\n";

            errorMsg = errorMsg + e.LastMessage();

            using (MemoryStream ms = new MemoryStream())
            {
                Rectangle bounds = Screen.GetBounds(Point.Empty);
                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                    }

                    bitmap.Save(ms, ImageFormat.Jpeg);
                }

                using (var db = new BaseEntities())
                {
                    var dd = db.ErrorLog.Add(new ErrorLog
                    {
                        Message = e.Message,
                        OnDate = DateTime.Now,
                        StackTrace = e.ToMessageAndCompleteStacktrace(),
                        Title = title,
                        UserId = DBHelper.CurrentUser.UserId,
                        ScreenShot = ms.ToArray(),
                        Ver = $"v.{ new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime }"
                    });

                    db.SaveChanges();
                }
            }



            return MessageBox.Show(errorMsg, title, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
        }
    }

    public static class ExceptionExtensions
    {
        public static string ToMessageAndCompleteStacktrace(this Exception exception)
        {
            Exception e = exception;
            StringBuilder s = new StringBuilder();
            while (e != null)
            {
                s.AppendLine("Exception type: " + e.GetType().FullName);
                s.AppendLine("Message       : " + e.Message);
                s.AppendLine("Stacktrace:");
                s.AppendLine(e.StackTrace);
                s.AppendLine();
                e = e.InnerException;
            }
            return s.ToString();
        }

        public static string LastMessage(this Exception exception)
        {
            Exception e = exception;
            String s = "";
            while (e != null)
            {
                s = "Exception type: " + e.GetType().FullName + Environment.NewLine;
                s += "Message       : " + e.Message + Environment.NewLine;
                s += "Stacktrace:" + Environment.NewLine;
                s += e.StackTrace + Environment.NewLine;

                e = e.InnerException;
            }

            return s;
        }
    }
}
