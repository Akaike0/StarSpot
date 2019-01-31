using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Reflection;
using System.ComponentModel;
using System.Windows.Resources;
using System.Windows.Threading;
using System.Collections;
using System.Threading;

namespace StarSpot
{
    public partial class MainWindow
    {
        // Get Process
        public static Process getCurrentProcess = Process.GetCurrentProcess();
        public static Process[] Wildstar = Process.GetProcessesByName("WildStar64");
        public static Process[] Wildstar_x86 = Process.GetProcessesByName("WildStar32");
        public static IntPtr hwnd;
        private ProcessReader processreader = new ProcessReader();
        public static uint BaseModuleAddress;
        private int[] GameID = new int[10];

        private string Characternames;
        private string[] PlayerNames = new string[10];
        private IntPtr[] ProcessHandle = new IntPtr[10];

        // Is bot running bool
        public static bool bot_running = false;

        // Character Stats
        private Stats stats = new Stats(); // Stats Class
        DispatcherTimer character_stats = new DispatcherTimer();    // Character Stats Timer
        DispatcherTimer mods_timer = new DispatcherTimer();

        // Mods Thread
        private BackgroundWorker mods_bgw = new BackgroundWorker();

        // Grinding class
        Grinding grinding = new Grinding();
        // PVP class
        PVP pvp = new PVP();
        // Gathering class
        Gathering gathering = new Gathering();
        // Combat class
        Combat combat = new Combat();
        // Blacklist check
        Blacklist blacklist = new Blacklist();
        // Actors class
        TargetInfo targetinfo = new TargetInfo();
        // Find closes waypoint
        FindWaypoint findwaypoint = new FindWaypoint();
        // Random jumping class
        RandomJumpingSystem jumping_system = new RandomJumpingSystem();
        // Player detection class
        PlayerDetection player_detection = new PlayerDetection();
        // Hacks class
        Hacks hacks = new Hacks();
        // Use mount class
        UseMount usemount = new UseMount();
        // RN
        RandomNR randomnr = new RandomNR();
        bool title_changed = false;

        // Stop & Start Options Timer
        DispatcherTimer stop_after = new DispatcherTimer();
        private bool stop_after_stopped = false;
        DispatcherTimer start_after = new DispatcherTimer();
        private bool start_after_active = false;

        // Premium timer
        DispatcherTimer premium_timer = new DispatcherTimer();

        // PD timer
        DispatcherTimer pd_timer = new DispatcherTimer();
        DispatcherTimer id_check_timer = new DispatcherTimer();

        // Time
        DateTime datetime = new DateTime(); 

        // Log text string
        public static string log_text;
        public static string log_text_last_added;

        // Selected client string
        public static string client_selected;

        // Minimized?
        public static bool minimized = false;
        public static bool str_mini_btn = false;

        public MainWindow()
        {
            InitializeComponent();

            // Start character stats
            //character_stats.Start();

            // Add client versions
            client_version_tbx.Items.Add("x64");
            //client_version_tbx.Items.Add("x86");

            // Timer setup
            timer_set();

            // UI
            // Enable read only for log tbx
            log_tbx.IsReadOnly = true;

            // Load spots from settings
            SpotsWindow.load_spots_settings();
        }

        // Client selection
        private void client_version_tbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Set the version
            client_selected = client_version_tbx.SelectedItem.ToString();

            // Add character names
            add_character_names();

            // Hide this cbx
            client_version_tbx.Visibility = Visibility.Collapsed;
        }
        private void add_character_names()
        {
            // Shows Processes with Characternames
            if (client_version_tbx.SelectedItem.ToString() == "x64")
            {
                for (int i = 0; i < Wildstar.Length; i++)
                {

                    // Open the process
                    ProcessReader.OpenProcxss(Wildstar[i].Id);
                    ProcessReader.base_adress = Wildstar[i].MainModule.BaseAddress.ToInt64();

                    try
                    {
                        Characternames = stats.player_name();

                        if (Characternames.Length > 0)
                        {
                            PlayerNames[i] = Characternames;
                            this.ProcessHandle[i] = Wildstar[i].MainWindowHandle;
                            GameID[i] = Wildstar[i].Id;
                        }
                    }
                    catch { }
                }

                for (int j = 0; j < this.ProcessHandle.Length; j++)
                {
                    if (this.PlayerNames[j] != null)
                    {
                        charactername_tbx.Items.Add(this.PlayerNames[j]);
                    }
                }
            }
            else if (client_version_tbx.SelectedItem.ToString() == "x86")
            {
                for (int i = 0; i < Wildstar_x86.Length; i++)
                {

                    // Open the process
                    ProcessReader.OpenProcxss(Wildstar_x86[i].Id);
                    ProcessReader.base_adress_x86 = Wildstar_x86[i].MainModule.BaseAddress.ToInt32();

                    try
                    {
                        Characternames = stats.player_name();

                        if ((Characternames.Length < 20) && (Characternames.Length > 0))
                        {
                            PlayerNames[i] = Characternames;
                            this.ProcessHandle[i] = Wildstar_x86[i].MainWindowHandle;
                            GameID[i] = Wildstar_x86[i].Id;
                        }
                    }
                    catch { }
                }

                for (int j = 0; j < this.ProcessHandle.Length; j++)
                {
                    if (this.PlayerNames[j] != null)
                    {
                        charactername_tbx.Items.Add(this.PlayerNames[j]);
                    }
                }
            }

            // # Shows Processes with Characternames
        }

        // Test everything with it!! it's a magic string!!!1
        public static string test_string;

        // Timer setup
        private void timer_set()
        {
            // Character Stats
            character_stats.Tick += new EventHandler(character_stats_Tick); // Timer Tick
            character_stats.Interval = new TimeSpan(0, 0, 0, 0, 100);

            // Stop Options Timer
            stop_after.Tick += new EventHandler(stop_after_Tick);
            stop_after.Interval = new TimeSpan(0, 0, 0, 0, 100);
            start_after.Tick += new EventHandler(start_after_Tick);
            start_after.Interval = new TimeSpan(0, 5, 0);

            // Mods timer
            mods_timer.Tick += new EventHandler(mods_timer_Tick);
            mods_timer.Interval = new TimeSpan(0, 0, 0, 0, 100);

            // Mods Backgroundworker
            mods_bgw.DoWork += new DoWorkEventHandler(mods_bgw_DoWork);
            mods_timer.Start();

            // PD timer
            pd_timer.Interval = new TimeSpan(0, 6 + randomnr.create(2, 10), 0);
            pd_timer.Tick += new EventHandler(pd_timer_Tick);
            pd_timer.Start();

            // ID check timer
            id_check_timer.Interval = new TimeSpan(0, 5 + randomnr.create(1, 12), 0);
            id_check_timer.Tick += new EventHandler(id_check_timer_Tick);
            id_check_timer.Start();

            // Premium timer
            if (Login.premium == "Invalid" | Login.ID == 1)
            {
                premium_timer.Interval = new TimeSpan(0, 21, 0);
                premium_timer.Tick += new EventHandler(premium_timer_Tick);
                premium_timer.Start();
            }
        }

        // Character Stats 
        private void character_stats_Tick(object sender, EventArgs e)
        {
            // Find target
            targetinfo.find_target();

            // Find players - check player detection
            player_detection.check();

            // Hacks
            hacks.start();

            // Use mount check
            usemount.start();

            // Test
            try
            {
                //test_string = stats.actionbar_slot_1_cooldown().ToString();
            }
            catch { }
            //try
            //{
            //    if (test_string != "")
            //    {
            //        test_lbl.Content = test_string;
            //    }
            //}
            //catch { }

            // Update UI
            update_ui();

            #region Options update
            if (!bot_running) // Stop the timer
            {
                if (Properties.Settings.Default.enable_start_after)
                    start_after.Stop();
                start_after_active = false;

                if (Properties.Settings.Default.enable_stop_after)
                    stop_after.Stop();
            }

            // Stop Options Timer
            //if (Properties.Settings.Default.stop_after_min > 0)
            //{
            //    if (stop_after.Interval.TotalMinutes != Properties.Settings.Default.stop_after_min)
            //    {
            //        stop_after.Interval = new TimeSpan(0, Properties.Settings.Default.stop_after_min, 0);
            //    }
            //}

            if (Properties.Settings.Default.start_after_min > 0)
            {
                if (start_after.Interval.TotalMinutes != Properties.Settings.Default.start_after_min)
                {
                    start_after.Interval = new TimeSpan(0, Properties.Settings.Default.start_after_min, 0);
                }
            }
            #endregion
        }
        private void charactername_tbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (charactername_tbx.SelectedIndex != -1)
            {
                if (client_version_tbx.SelectedItem.ToString() == "x64")
                {
                    ProcessReader.OpenProcxss(Wildstar[charactername_tbx.SelectedIndex].Id);

                    ProcessReader.base_adress = Wildstar[charactername_tbx.SelectedIndex].MainModule.BaseAddress.ToInt64();

                    hwnd = Wildstar[charactername_tbx.SelectedIndex].MainWindowHandle;

                    // Give the exitwildstar class the process nr
                    ExitWildstar.process_nr = charactername_tbx.SelectedIndex;

                    // Keysimulation hwnd
                    Keysimulation.hwnd = hwnd;
                }
                else if (client_version_tbx.SelectedItem.ToString() == "x86")
                {
                    ProcessReader.OpenProcxss(Wildstar_x86[charactername_tbx.SelectedIndex].Id);

                    ProcessReader.base_adress_x86 = Wildstar_x86[charactername_tbx.SelectedIndex].MainModule.BaseAddress.ToInt32();

                    hwnd = Wildstar_x86[charactername_tbx.SelectedIndex].MainWindowHandle;
                }

                // Disable CBX
                charactername_tbx.IsEnabled = false;

                // Start Character Timer
                character_stats.Start();
            }
        }

        // Mods runner
        private void mods_timer_Tick(object sender, EventArgs e)
        {
            // Start the Mods Runner
            if (!mods_bgw.IsBusy)
            {
                mods_bgw.RunWorkerAsync();
            }
        }
        private void mods_bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            if (stats.player_name() != "")
            {
                if (bot_running && Properties.Settings.Default.mods == "Grinding")
                {
                    if (start_after_active == false)
                    {
                        if (Properties.Settings.Default.player_detection_radius == 0) // Check if player detection is enabled
                        {
                            grinding.start(); // Grinding
                        }
                        else
                        {
                            // Enable player detection
                            if ((!PlayerDetection.detected && !AutoLogin.enable_relogin) | stats.player_aggro() == 1) // Check if players detected
                            {
                                grinding.start(); // Grinding
                            }
                        }
                    }

                    if (Properties.Settings.Default.random_jumping == true && !AutoLogin.enable_relogin)
                    {
                        jumping_system.random_jump();
                    }
                }
                else if (bot_running && Properties.Settings.Default.mods == "Gathering")
                {
                    if (start_after_active == false)
                    {
                        if (Properties.Settings.Default.player_detection_radius == 0) // Check if player detection is enabled
                        {
                            gathering.start();
                        }
                        else
                        {
                            // Enable player detection
                            if ((!PlayerDetection.detected && !AutoLogin.enable_relogin) | stats.player_aggro() == 1) // Check if players detected
                            {
                                gathering.start();
                            }
                        }
                    }

                    if (Properties.Settings.Default.random_jumping == true && !AutoLogin.enable_relogin)
                    {
                        jumping_system.random_jump();
                    }
                }
                else if (bot_running && Properties.Settings.Default.mods == "PVP")
                {
                    if (start_after_active == false)
                    {
                        pvp.start(); // Grinding
                    }

                    if (Properties.Settings.Default.random_jumping == true)
                    {
                        if (stats.pvp_match_status() == 2 | stats.pvp_match_status() == 4)
                        {
                            jumping_system.random_jump();
                        }
                    }
                }
                else if (bot_running == true && Properties.Settings.Default.mods == "Combat")
                {
                    if (Properties.Settings.Default.aim_friendly_target | stats.player_targetclass() != 2)
                    {
                        combat.start();
                    }
                }
            }
            else
            {
                if (Properties.Settings.Default.autologin_password != "" && Properties.Settings.Default.mods != "Combat" && bot_running)
                {
                    AutoLogin.login();
                }
            }
        }

        // Stop Options
        private void stop_after_Tick(object sender, EventArgs e)
        {
            // Get current time
            datetime = DateTime.Now;

            if (Properties.Settings.Default.stop_at_time.ToString() == datetime.ToShortTimeString()) // Check if it's time
            {
                stop_after_stopped = true;
            }

            if(stop_after_stopped && stats.player_aggro() == 0)
            {
                if (!log_tbx.Text.Contains("The bot stopped (Stop after function)."))
                {
                    log_text = "The bot stopped (Stop after function) after " + Properties.Settings.Default.stop_at_time + " min.";
                }

                if(Properties.Settings.Default.enable_stop_after_exit_wildstar)
                {
                    MainWindow.Wildstar[ExitWildstar.process_nr].Kill();
                }

                bot_running = false; // Set the run bool to false
                start_btn.Content = "Start (END)"; // Reset the start button text
                stop_after_stopped = false; // Reset stop after function
            }
        }
        private void start_after_Tick(object sender, EventArgs e)
        {
            // Deactivate the bool to run the bot
            start_after_active = false;
        }

        // UI
        private void update_ui()
        {
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
            if (!title_changed)
            {
                this.Title = randomnr.ST(randomnr.create(2, 12)) + " " + stats.player_name();
                title_changed = true;
            }
            #endregion

            #region Log

            if (log_text != "" && log_text != null)
            {
                // Add new log
                if (log_tbx.Text.Length == 0)
                {
                    log_tbx.Text += DateTime.Now.ToString("h:mm:ss tt") + " " + log_text;
                }
                else
                {

                    if (log_text_last_added != log_text)
                    {
                        log_tbx.Text += "\n" + DateTime.Now.ToString("h:mm:ss tt") + " " + log_text;
                    }
                }

                if (log_tbx.LineCount >= 20) // Delete old lines
                {
                    while (log_tbx.LineCount > 10)
                    {
                        log_tbx.Text = log_tbx.Text.Remove(0, log_tbx.GetLineLength(0));
                    }
                }

                // Add the last added text
                log_text_last_added = log_text;

                // Reset the log text
                log_text = "";
            }

            if (bot_running == true) // Disable autoscrolling if bot is off
            {
                // Enable autoscroll
                log_tbx.CaretIndex = log_tbx.Text.Length;
                var rect = log_tbx.GetRectFromCharacterIndex(log_tbx.CaretIndex);
                log_tbx.ScrollToHorizontalOffset(rect.Right);
                log_tbx.ScrollToEnd();
            }

            #endregion

            #region Shortcuts

            // Start bot
            if(Keyboard.IsKeyDown(Key.End) && !Properties.Settings.Default.disable_start_stop_shortcut)
            {
                start_btn_data();
            }

            #endregion Shortcuts

            #region Minimized
            if(minimized)
            {
                if (this.Visibility != Visibility.Collapsed)
                {
                    this.Visibility = Visibility.Collapsed;
                }

                // Start btn
                if(str_mini_btn)
                {
                    // Click once
                    str_mini_btn = false;
                    start_btn_data();
                }
            }
            else
            { 
                if (this.Visibility != Visibility.Visible) { this.Visibility = Visibility.Visible; } 
            }
            #endregion
        }

        // Premium timer
        private void premium_timer_Tick(object sender, EventArgs e)
        {
            // Close the app
            Process.GetCurrentProcess().Kill();
        }

        // PD timer
        private void pd_timer_Tick(object sender, EventArgs e)
        {
            try
            {
                Protection.dProtect();

                id_check_timer.Interval = new TimeSpan(0, 5 + randomnr.create(1, 12), 0);
            }
            catch { }
        }
        // ID check timer
        private void id_check_timer_Tick(object sender, EventArgs e)
        {
            try
            {
                Protection.pProtect();
                Protection.idProtect();

                id_check_timer.Interval = new TimeSpan(0, 5 + randomnr.create(1, 12), 0);
            }
            catch { }
        }
        // Buttons
        private void start_btn_Click(object sender, RoutedEventArgs e)
        {
            start_btn_data();
        }
        private void start_btn_data()
        {
            if (Properties.Settings.Default.mods == "Grinding" | Properties.Settings.Default.mods == "PVP" | Properties.Settings.Default.mods == "Gathering")
            {
                if (CTM_System.waypoints_list.Count != 0 && start_btn.Content.ToString() == "Start (END)")
                {
                    if (Properties.Settings.Default.autoselling_enabled && CTM_System.waypoints_selling_list.Count != 0 && Properties.Settings.Default.mods != "PVP")
                    {
                        bot_running = true;

                        // Set this CTM
                        if ((!Grinding.player_autoselling && !Grinding.player_ressurection && Properties.Settings.Default.mods == "Grinding") | (!Gathering.player_autoselling && !Gathering.player_ressurection && Properties.Settings.Default.mods == "Gathering"))
                        {
                            // Reset spots
                            reset_spots();

                            // Find closes waypoint
                            findwaypoint.closes_waypoint();

                            // Set this waypoint to the current list
                            CTM_System.ctm_points = CTM_System.waypoints_list[findwaypoint.best_waypoint_index];
                            CTM_System.waypoint_count = findwaypoint.best_waypoint_index;

                            stats.player_ctm_x(CTM_System.ctm_points.X);
                            stats.player_ctm_y(CTM_System.ctm_points.Y);
                        }

                        // Enable start after & stop after
                        if (Properties.Settings.Default.enable_start_after)
                        {
                            // Add a log text
                            log_text = "The bot will start in" + " " + Properties.Settings.Default.start_after_min.ToString() + " min.";

                            // Start the timer
                            start_after.Start(); start_after_active = true;
                        }

                        if (Properties.Settings.Default.enable_stop_after)
                        {
                            // Add a log text
                            log_text = "The bot will stop at" + " " + Properties.Settings.Default.stop_at_time;

                            stop_after.Start();
                        }

                        // Set button to stop
                        start_btn.Content = "Stop (END)";
                    }
                    else if (Properties.Settings.Default.autoselling_enabled && CTM_System.waypoints_selling_list.Count == 0 && Properties.Settings.Default.mods != "PVP")
                    {
                        // Error
                        MessageBox.Show("Create more selling spots or disable auto selling!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else if (!Properties.Settings.Default.autoselling_enabled | Properties.Settings.Default.mods == "PVP")
                    {
                        bot_running = true;

                        // Set this CTM
                        if ((!Grinding.player_ressurection && Properties.Settings.Default.mods == "Grinding") | (!Gathering.player_ressurection && Properties.Settings.Default.mods == "Gathering") | (!PVP.player_ressurection && Properties.Settings.Default.mods == "PVP"))
                        {
                            // Reset spots
                            reset_spots();

                            // Find closes waypoint
                            findwaypoint.closes_waypoint();

                            if (findwaypoint.waypoint_found)
                            {
                                // Set this waypoint to the current list
                                CTM_System.ctm_points = CTM_System.waypoints_list[findwaypoint.best_waypoint_index];
                                CTM_System.waypoint_count = findwaypoint.best_waypoint_index;

                                stats.player_ctm_x(CTM_System.ctm_points.X);
                                stats.player_ctm_y(CTM_System.ctm_points.Y);
                            }
                        }

                        // Enable start after & stop after
                        if (Properties.Settings.Default.enable_start_after)
                        {
                            // Add a log text
                            log_text = "The bot will start in" + " " + Properties.Settings.Default.start_after_min.ToString() + " min.";

                            // Start the timer
                            start_after.Start(); start_after_active = true;
                        }

                        if (Properties.Settings.Default.enable_stop_after)
                        {
                            // Add a log text
                            log_text = "The bot will stop at" + " " + Properties.Settings.Default.stop_at_time;

                            stop_after.Start();
                        }

                        // Set button to stop
                        start_btn.Content = "Stop (END)";
                    }
                }
                else if (CTM_System.waypoints_list.Count == 0)
                {
                    MessageBox.Show("Create more spots!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (start_btn.Content.ToString() != "Start")
                {
                    bot_running = false; // Stop bot

                    // Set button to start
                    start_btn.Content = "Start (END)";
                }
            }
            else if (Properties.Settings.Default.mods == "Combat")
            {
                if ((start_btn.Content.ToString() == "Start (END)"))
                {
                    bot_running = true;

                    // Reset spots
                    reset_spots();

                    // Enable start after & stop after
                    if (Properties.Settings.Default.enable_start_after)
                    {
                        // Add a log text
                        log_text = "The bot will start in" + " " + Properties.Settings.Default.start_after_min.ToString() + " min.";

                        // Start the timer
                        start_after.Start(); start_after_active = true;
                    }

                    if (Properties.Settings.Default.enable_stop_after)
                    {
                        // Add a log text
                        log_text = "The bot will stop at" + " " + Properties.Settings.Default.stop_at_time;

                        stop_after.Start();
                    }

                    // Set button to stop
                    start_btn.Content = "Stop (END)";
                }
                else if (start_btn.Content.ToString() != "Start (END)")
                {
                    bot_running = false; // Stop bot

                    // Stop some functions
                    stop_after.Stop();
                    start_after.Stop();

                    // Set button to start
                    start_btn.Content = "Start (END)";
                }
            }
        }
        private void spots_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!SpotsWindow.spots_window_visible)
            {
                // Create a spots window
                SpotsWindow spots_window = new SpotsWindow();

                // Open it and make it visible
                SpotsWindow.spots_window_visible = true;
                spots_window.Show();
            }
        }
        private void Options_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!OptionsWindow.options_window_visible)
            {
                // Create a new option class
                OptionsWindow window = new OptionsWindow();

                // Enable visibility
                OptionsWindow.options_window_visible = true;

                // Open it!
                window.Show();
            }
        }

        // Reset spots function
        public void reset_spots()
        {
            // Reset normal spots
            try
            {
                if (Properties.Settings.Default.mods != "PVP")
                {
                    CTM_System.ctm_points = CTM_System.waypoints_list[CTM_System.waypoint_count];
                }
                else
                {
                    CTM_System.ctm_points = CTM_System.waypoints_list[0];
                }
            }
            catch { }

            // Reset death spots
            try
            {
                if (Properties.Settings.Default.mods != "PVP")
                {
                    CTM_System.ctm_death_points = CTM_System.waypoints_death_list[CTM_System.waypoint_death_count];
                }
                else
                {
                    CTM_System.ctm_death_points = CTM_System.waypoints_death_list[0];
                }
            }
            catch { }

            // Reset selling spots
            try
            {
                CTM_System.ctm_selling_points = CTM_System.waypoints_selling_list[CTM_System.waypoint_selling_count];
            }
            catch { }
        }

        // Create a new radar window
        Radar radar = new Radar();
        private void radar_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!radar.Visible)
            {
                try
                {
                    radar.Show();
                }
                catch { }
            }
        }

        // Close this app
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            // Close the app
            Process.GetCurrentProcess().Kill();
        }

        public static MainWindowMini mini = new MainWindowMini();

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (charactername_tbx.SelectedIndex != -1)
            {
                // Create new mini window
                mini.Show();
                mini.Visibility = Visibility.Visible;

                // Set mini. true
                minimized = true;
            }
        }
    }
}
