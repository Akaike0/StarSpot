using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace StarSpot
{
    class PlayerDetection
    {
        // Foreground function
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr hWnd);

        // Entity list
        ActorsList elist = new ActorsList();

        // Stats class
        Stats stats = new Stats();

        // Player detection bool
        public static UInt64 actors_id = 0; 
        public static bool detected = false;

        // Detection timer
        DispatcherTimer detection_timer = new DispatcherTimer();
        // Refresh timer
        DispatcherTimer refresh_timer = new DispatcherTimer();

        // Alert timer
        DispatcherTimer detection_alert = new DispatcherTimer();

        // Fix
        private string detected_name = "";
        DispatcherTimer false_detection_timer = new DispatcherTimer();

        // Random
        RandomNR randomnr = new RandomNR();

        // Player distance
        private double distance_to_player = 0;

        public PlayerDetection()
        {
            // Load timer
            detection_timer.Tick += new EventHandler(detection_timer_Tick);

            // Refresh timer
            refresh_timer.Tick += new EventHandler(refresh_timer_Tick);
            refresh_timer.Interval = new TimeSpan(0, 0, 65 + randomnr.create(3, 6));

            // Alert duration
            detection_alert.Tick += new EventHandler(detection_alert_Tick);
            detection_alert.Interval = new TimeSpan(0, 0, 1);

            // False detection fix
            false_detection_timer.Tick += new EventHandler(false_detection_timer_Tick);
            false_detection_timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            false_detection_timer.Start();
        }

        public void check()
        {
            // Load config
            load_config();

            if (Properties.Settings.Default.player_detection_radius != 0 && Properties.Settings.Default.mods != "PVP") // Make sure it's not the pvp mode
            {
                try
                {
                    // Update the entity list
                    elist.update();

                    foreach (Actors entity in elist)
                    {
                        // Calculate the distance
                        if (entity.typ == 20 && entity.id != stats.player_id() && actors_id == 0 && entity.name != stats.player_name() && !AutoLogin.enable_relogin)
                        {
                            distance_to_player = Distance2D(stats.player_position_x(), stats.player_position_y(), entity.position_x, entity.position_y);
                        }

                        if (entity.typ == 20 && distance_to_player <= Properties.Settings.Default.player_detection_radius && actors_id == 0 && entity.name != stats.player_name() && !AutoLogin.enable_relogin)
                        {
                            //MainWindow.test_string = actors_id.ToString() + " " + distance_to_player.ToString() + " " + detected.ToString();

                            if (distance_to_player <= Properties.Settings.Default.player_detection_radius && entity.id != stats.player_id() && entity.name != stats.player_name())
                            {

                                    // Add a log
                                    MainWindow.log_text = "Player detected! Name: " + entity.name + " Level: " + entity.level.ToString() + ".";

                                    if (entity.name != stats.player_name())
                                    {
                                        detected_name = entity.name;
                                    }

                                    // Start the timer
                                    detection_timer.Start();

                                    // Set the index
                                    actors_id = entity.id;
                            }
                        }

                        if (entity.id == actors_id && actors_id != 0)
                        {
                            //MainWindow.test_string = actors_id.ToString() + " " + distance_to_player.ToString() + " " + detected.ToString();

                            // Calculate the distance
                            distance_to_player = Distance2D(stats.player_position_x(), stats.player_position_y(), entity.position_x, entity.position_y);

                            // Disable detection
                            if (distance_to_player > Properties.Settings.Default.player_detection_radius)
                            {
                                // Add a log
                                MainWindow.log_text = "Player out of range! Name: " + entity.name + " Level: " + entity.level.ToString() + ".";

                                // Stop the timer
                                detection_timer.Stop();

                                // Disable detected
                                detected = false;

                                // Set the id to null
                                actors_id = 0;
                            }
                        }
                    }
                }
                catch { }
            }
        }

        public double Distance2D(float x1, float y1, float x2, float y2)
        {
            double result = 0;

            double part1 = System.Math.Pow((x2 - x1), 2);

            double part2 = System.Math.Pow((y2 - y1), 2);

            double underRadical = part1 + part2;

            result = System.Math.Sqrt(underRadical);

            return result;
        } // Distance calculator

        public void load_config() // Loads configurations for timer etc
        {
            // Timer timespan
            if (Properties.Settings.Default.player_detection_min == 0)
            {
                if (detection_timer.Interval.TotalSeconds != 1)
                {
                    detection_timer.Interval = new TimeSpan(0, 0, 1);
                }
            }
            else
            {
                if (detection_timer.Interval.TotalSeconds != Properties.Settings.Default.player_detection_min)
                {
                    detection_timer.Interval = new TimeSpan(0, 0, Properties.Settings.Default.player_detection_min);
                }
            }
        }

        // Ini autologin
        AutoLogin autologin = new AutoLogin();

        // Timer
        public void detection_timer_Tick(object sender, EventArgs e)
        {
            if (actors_id != 0 && actors_id != stats.player_id())
            {
                // Enable detected
                detected = true;

                // Enable logout
                if(Properties.Settings.Default.player_detection_logout_for != 0)
                {
                    AutoLogin.enable_relogin = true;
                }

                // Start the refresh
                refresh_timer.Start();

                // Enable alert if enabled
                if (Properties.Settings.Default.player_detection_alert)
                {
                    // Start the alert
                    detection_alert.Start();

                    // Set current duration time
                    try
                    {
                        if (detection_alert.Interval.TotalSeconds != Properties.Settings.Default.player_detection_alert_duration && Properties.Settings.Default.player_detection_alert_duration != 0)
                        {
                            detection_alert.Interval = new TimeSpan(0, 0, Properties.Settings.Default.player_detection_alert_duration);
                        }
                    }
                    catch { }
                }
            }
        }
        public void refresh_timer_Tick(object sender, EventArgs e)
        {
            if (!AutoLogin.enable_relogin)
            {
                // Refresh
                if (detected && actors_id != 0 && MainWindow.bot_running)
                {
                    // Disable detected
                    detected = false;

                    // Set the id to null
                    actors_id = 0;

                    // Stop the timer
                    refresh_timer.Stop();
                }
                else
                {
                    // Stop the timer
                    refresh_timer.Stop();
                }
            }
        }

        // False detection timer
        public void false_detection_timer_Tick(object sender, EventArgs e)
        {
            if(detected_name == stats.player_name() && detected_name != "")
            {
                // Stop the timer
                detection_timer.Stop();

                // Disable detected
                detected = false;

                // Set the id to null
                actors_id = 0;

                // Set detected name to null
                detected_name = "";

                AutoLogin.enable_relogin = false;
            }
        }

        // Alert duration timer
        private void detection_alert_Tick(object sender, EventArgs e)
        {
            if(detected)
            {
                // Set window to foreground
                SetForegroundWindow(MainWindow.hwnd);

                // Play sound
                SystemSounds.Beep.Play();
            }
        }
    }
}
