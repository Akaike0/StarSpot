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
using System.Windows.Threading;

namespace StarSpot
{
    public partial class LoginClick
    {
        // Timer
        public static DispatcherTimer updater = new DispatcherTimer();
        String[] position;

        public LoginClick()
        {
            InitializeComponent();

            // Change the visibility bool
            bwindow_visibility = true;

            position = Properties.Settings.Default.loginclick_x_y.Split(',');

            // Set the old position
            lbl_position_x.Content = "Position X:" + " " + position[0];
            lbl_position_y.Content = "Position Y:" + " " + position[1];

            // Enable the timer
            updater.Interval = new TimeSpan(0, 0, 0, 0, 100);
            updater.Tick += new EventHandler(updater_Tick);
            updater.Start();
        }

        // Windows stats
        public static bool bwindow_visibility = false;
        Stats stats = new Stats();

        // Update timer
        private void updater_Tick(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.F5) && bwindow_visibility)
            {
                // Save the position
                position[0] = stats.cursor_login_x().ToString();
                position[1] = stats.cursor_login_y().ToString();

                // Set the label's content
                lbl_position_x.Content = "Position X:" + " " + position[0];
                lbl_position_y.Content = "Position Y:" + " " + position[1];

                // Save settings
                Properties.Settings.Default.loginclick_x_y = string.Join(",", position);

                Properties.Settings.Default.Save();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Save settings
            Properties.Settings.Default.loginclick_x_y = string.Join(",", position);

            Properties.Settings.Default.Save();

            // Change the visibility state
            bwindow_visibility = false;
        }

    }
}
