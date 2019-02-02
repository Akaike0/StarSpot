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
using System.Diagnostics;
using System.Net;

namespace StarSpot
{
    public partial class EnterKey
    {
        public EnterKey()
        {
            InitializeComponent();

            // Load the settings key
            key_lbl.Text = Properties.Settings.Default.serial_key;
        }

        private void validate_btn_Click(object sender, RoutedEventArgs e)
        {
            if(key_lbl.Text != "") // Save the key into the settings if it's not null
            {
                // Send the informations to the php script
                string query = new WebClient().DownloadString("" + key_lbl.Text + "&machine=" + Login.machine_id);

                // Save the key into the settings
                Properties.Settings.Default.serial_key = key_lbl.Text;
                Properties.Settings.Default.Save();

                // Info
                MessageBox.Show("Please restart StarSpot to validate the key.", "Error", MessageBoxButton.OK, MessageBoxImage.Information);

                // Close the app
                Process.GetCurrentProcess().Kill();
            }
            else
            {
                // Error
                MessageBox.Show("Please enter a key.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void demo_btn_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.serial_key = "Demo"; // Enter demo as key

            Properties.Settings.Default.Save();

            // Error
            MessageBox.Show("Please restart StarSpot to activate the demo.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

            // Close the app
            Process.GetCurrentProcess().Kill();
        }
    }
}
