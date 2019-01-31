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
    /// <summary>
    /// Interaktionslogik für PVPToolWindow.xaml
    /// </summary>
    public partial class PVPToolWindow
    {
        // Timer
        public static DispatcherTimer updater = new DispatcherTimer();

        public PVPToolWindow()
        {
            InitializeComponent();

            // Change the visibility bool
            bwindow_visibility = true;

            // Set the old position
            lbl_position_x.Content = "Position X:" + " " + Properties.Settings.Default.pvp_cursor_x_position.ToString();
            lbl_position_y.Content = "Position Y:" + " " + Properties.Settings.Default.pvp_cursor_y_position.ToString();

            // Enable the timer
            updater.Interval = new TimeSpan(0, 0, 0, 0, 100);
            updater.Tick += new EventHandler(updater_Tick);
            updater.Start();
        }

        // Positions
        public static UInt64 Iposition_x;
        public static UInt64 Iposition_y;
        
        // Stats class
        Stats stats = new Stats();

        // Windows stats
        public static bool bwindow_visibility = false;

        // Update timer
        private void updater_Tick(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.F5) && bwindow_visibility)
            {
                // Save the position
                Iposition_x = stats.cursor_login_x();
                Iposition_y = stats.cursor_login_y();

                // Set the label's content
                lbl_position_x.Content = "Position X:" + " " + Iposition_x;
                lbl_position_y.Content = "Position Y:" + " " + Iposition_y;

                // Save settings
                Properties.Settings.Default.pvp_cursor_y_position = Convert.ToInt32(Iposition_y);
                Properties.Settings.Default.pvp_cursor_x_position = Convert.ToInt32(Iposition_x);

                Properties.Settings.Default.Save();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Save settings
            Properties.Settings.Default.pvp_cursor_y_position = Convert.ToInt32(Iposition_y);
            Properties.Settings.Default.pvp_cursor_x_position = Convert.ToInt32(Iposition_x);

            Properties.Settings.Default.Save();

            // Change the visibility state
            bwindow_visibility = false;
        }
    }
}
