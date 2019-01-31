using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace StarSpot
{
    class AutoLogin
    {
        // Stats class
        Stats stats = new Stats();

        // Main
        public AutoLogin()
        {
            // Timer
            relogin_timer.Interval = new TimeSpan(0, 1, 0);
            relogin_timer.Tick += new EventHandler(relogin_timer_Tick);
            player_detection_logout_timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            player_detection_logout_timer.Tick += new EventHandler(player_detection_logout_timer_Tick);
            player_detection_logout_timer.Start();
            relogin_after_timer.Interval = new TimeSpan(0, 0, 8);
            relogin_after_timer.Tick += new EventHandler(relogin_after_timer_Tick);
        }

        // Autologin
        public static void login()
        {
            // Wait
            System.Threading.Thread.Sleep(8000);

            // Enter the password again
            Keysimulation.SimulateKeys.text(Properties.Settings.Default.autologin_password);

            // Click enter
            Keysimulation.SimulateKeys.Enter();

            // Wait
            System.Threading.Thread.Sleep(2000);

            // Click enter again
            Keysimulation.SimulateKeys.Enter();

            // Wait
            System.Threading.Thread.Sleep(8000);

            // Enable autologin function in the mods
            if (Properties.Settings.Default.mods == "Grinding")
            {
                Grinding.player_autologin = true;
            }

            if (Properties.Settings.Default.mods == "Gathering")
            {
                Gathering.player_autologin = true;
            }

            if (Properties.Settings.Default.mods == "PVP")
            {
                PVP.player_autologin = true;
            }
        }

        // Logout and  login
        public static bool enable_relogin = false;
        public static bool loggedout = false;
        public bool relogin = false;
        public DispatcherTimer relogin_timer = new DispatcherTimer();
        public DispatcherTimer relogin_after_timer = new DispatcherTimer();
        public DispatcherTimer player_detection_logout_timer = new DispatcherTimer();

        // Key Using class
        KeyusingSystem keyusing = new KeyusingSystem();

        public void player_detection_logout_timer_Tick(object sender, EventArgs e)
        {
            if(MainWindow.bot_running && Properties.Settings.Default.player_detection_logout_for != 0 && enable_relogin)
            {
                // Stop running
                try
                {
                    if(stats.player_ctm_push() != 4294967295)
                    {
                        stats.player_ctm_push(4294967295);
                    }
                }
                catch { }

                // Relog
                relog();
            }
        }

        public void relog()
        {
            if(!loggedout)
            {
                // Disable logout
                loggedout = true;

                // Key Using
                if(Properties.Settings.Default.logout_keyusing)
                {
                    keyusing.key_using();
                }

                // Wait
                System.Threading.Thread.Sleep(500);

                // Click enter
                Keysimulation.SimulateKeys.Enter();

                // Wait
                System.Threading.Thread.Sleep(500);

                // Enter the password again
                Keysimulation.SimulateKeys.text("/camp");

                // Click enter
                Keysimulation.SimulateKeys.Enter();

                // Check timer settings
                if(relogin_timer.Interval.TotalMinutes != Properties.Settings.Default.player_detection_logout_for)
                {
                    relogin_timer.Interval = new TimeSpan(0, Properties.Settings.Default.player_detection_logout_for, 0);
                }

                // Start timer
                relogin_timer.Start();
            }
            else
            {
                // Login
                if (relogin)
                {
                    // Click enter
                    Keysimulation.SimulateKeys.MouseClickLogin();

                    // Wait
                    System.Threading.Thread.Sleep(8000);

                    // Wait
                    System.Threading.Thread.Sleep(6000);

                    System.Threading.Thread.Sleep(3000);

                    // Stop the timer
                    relogin_timer.Stop();

                    relogin_after_timer.Start();

                    // Disable relogin
                    relogin = false;
                }
            }
        }

        public void relogin_after_timer_Tick(object sender, EventArgs e)
        {
            // Disable player detection
            PlayerDetection.detected = false;
            PlayerDetection.actors_id = 0;

            // Enable logout
            loggedout = false;
            enable_relogin = false;

            relogin_after_timer.Stop();
        }

        public void relogin_timer_Tick(object sender, EventArgs e)
        {
            // Allow relogin
            relogin = true;
        }
    }
}
