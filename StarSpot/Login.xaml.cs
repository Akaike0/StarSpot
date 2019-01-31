using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SKGL;
using System.Diagnostics;
using System.Windows.Threading;
using System.Net;
using System.Runtime.InteropServices;

namespace StarSpot
{
    public partial class Login
    {
        // SKGL Class
        SKGL.Generate CreateID = new SKGL.Generate();

        // Private machine id class
        public static Int32 machine_id;

        // Create main window
        MainWindow mainwindow = new MainWindow();

        // Premium timer
        DispatcherTimer timer = new DispatcherTimer();

        // Update class
        Update update = new Update();

        // Premium
        public static string premium;

        // ID 
        public static int ID = 0;

        public Login()
        {
            InitializeComponent();

            // Check update
            update.check_update();

            // Add the machine id
            machine_id_lbl.Content = Convert.ToString(CreateID.MachineCode);
            machine_id = CreateID.MachineCode;

            licence_check();

            // Set the timer
            timer.Interval = new TimeSpan(0, 20, 0);
            timer.Tick += new EventHandler(timer_Tick);

            licence_check();
        }

        // Licence check
        private void licence_check()
        {
            try
            {
                // Send the informations to the php script
                string query = new WebClient().DownloadString("" + Properties.Settings.Default.serial_key + "&machine=" + CreateID.MachineCode.ToString());

                switch (query.ToLower())
                {
                    case "valid":
                        {
                            premium_lbl.Content = "Valid";
                            premium_lbl.Foreground = new SolidColorBrush(Colors.Yellow);
                            break;
                        }
                    case "invalid":
                        {
                            premium_lbl.Content = "Invalid";
                            premium = "Invalid";
                            ID = 1;
                            break;
                        }
                }
            }
            catch
            {
                // Show the valid label
                premium_lbl.Content = "Invalid - Connection Error";
            }
        }

        // Timer
        private void timer_Tick(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        // Buttons
        private void login_btn_Click(object sender, RoutedEventArgs e) // Login btn
        {
            if (Properties.Settings.Default.serial_key == "") // Check if a serial key is avialable
            {
                // Create a enter key window
                EnterKey enterkey = new EnterKey();

                enterkey.Show(); // Show the enter key window
            }
            else
            {
                this.Visibility = Visibility.Collapsed; // Disappear login window ...

                if (premium_lbl.Content.ToString() == "Invalid")
                {
                    // Shwo info
                    MessageBox.Show("You are using the demo version which works 20 minutes.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    premium = "Invalid";

                    // Start the timer
                    timer.Start();
                }

                mainwindow.Show(); // Show the main window
            }
        }

        private void key_btn_Click(object sender, RoutedEventArgs e) // Key btn to change the key
        {
            // Create a enter key window
            EnterKey enterkey = new EnterKey();

            enterkey.Show(); // Show the enter key window
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Close the app
            Process.GetCurrentProcess().Kill();
        }
    }
}
