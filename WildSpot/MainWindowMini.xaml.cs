using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
    /// Interaction logic for MainWindowMini.xaml
    /// </summary>
    public partial class MainWindowMini
    {
        // Timer
        DispatcherTimer UI_timer = new DispatcherTimer();

        // RN
        RandomNR randomnr = new RandomNR();
        bool title_changed = false;

        // Stats
        Stats stats = new Stats();

        public MainWindowMini()
        {
            InitializeComponent();

            UI_timer.Tick += new EventHandler(UI_timer_Tick);
            UI_timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            UI_timer.Start();

            if (MainWindow.bot_running)
            {
                this.start_btn.Content = "Stop (F4)";
            }
        }

        private void start_btn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.str_mini_btn = true;

            if (start_btn.Content.ToString() == "Stop (F4)")
            {
                this.start_btn.Content = "Start (F4)";
            }
            else
            {
                this.start_btn.Content = "Stop (F4)";
            }
        }

        // UI Timer
        private void UI_timer_Tick(object sender, EventArgs e)
        {
            #region Shortcuts

            // Start bot
            if (Keyboard.IsKeyDown(Key.End) && !Properties.Settings.Default.disable_start_stop_shortcut)
            {
                MainWindow.str_mini_btn = true;
            }

            #endregion Shortcuts

            #region StartBtn

            if (!MainWindow.bot_running)
            {
                this.start_btn.Content = "Start (F4)";
            }
            else
            {
                this.start_btn.Content = "Stop (F4)";
            }

            #endregion

            #region Character basics

            // Update the UI Health & Shield Power
            try
            {
                character_health_pgb.Value = stats.player_health_inp();
            }
            catch { character_health_pgb.Value = 0; }

            try
            {
                character_shield_pgb.Value = stats.player_shield_inp();
            }
            catch { character_shield_pgb.Value = 0; }

            // Character name
            if (!title_changed && stats.player_name() != "")
            {
                this.Title = randomnr.ST(randomnr.create(2, 3)) + " " + stats.player_name();
                title_changed = true;
            }
            #endregion
        }

        // Close this app
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            // Close the app
            Process.GetCurrentProcess().Kill();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Set mini. true
            MainWindow.minimized = false;

            // Destroy this!
            MainWindow.mini.Visibility = Visibility.Collapsed;
        }

    }
}
