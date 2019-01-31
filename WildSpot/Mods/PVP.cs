using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace StarSpot
{
    class PVP
    {
        // Stats
        public static bool player_buffing = false;
        public static bool player_healing = false;
        private bool player_healing_enabled = false;
        public static bool player_attacking = false;
        public static bool player_looting = false;
        public static bool player_walking = true;
        public static bool player_walking_togate = true;
        public static bool player_ressurection = false;
        public static bool player_autologin = false;

        // CTM class
        CTM_System ctm_system = new CTM_System();
        // Find the closes waypoint
        FindWaypoint find_waypoint = new FindWaypoint();

        // Stats class
        Stats stats = new Stats();

        // TargetInfos class
        TargetInfo TargetInfo = new TargetInfo();

        // Blacklist class
        Blacklist blacklist = new Blacklist();

        // Unstucking System
        DispatcherTimer unstucking_system = new DispatcherTimer();
        public bool unstucking_now = false;

        // Unstucking prewarm system
        DispatcherTimer unstucking_system_prewarm = new DispatcherTimer();

        // Unstuck long attack
        DispatcherTimer unstucking_system_attack = new DispatcherTimer();

        // Skills class
        SkillsSystem skillsystem = new SkillsSystem();

        // Key using class
        KeyusingSystem keyusingsystem = new KeyusingSystem();

        // Autoselling timer & bool
        public static bool player_autoselling = false;
        DispatcherTimer autoselling_timer = new DispatcherTimer();

        // Bot using class
        PlayerBots player_bots = new PlayerBots();

        // Casting movement
        CastingMovement castingmovement = new CastingMovement();
        // Fight movement
        FightMovement fightmovement = new FightMovement();

        // Entity class
        ActorsList elist = new ActorsList();

        // RandomNR
        RandomNR randomnr = new RandomNR();

        // Players found list
        public bool players_searching_done = false;
        public static int players_found = 0;
        public List<UInt64> players_found_ids = new List<UInt64>();
        public bool wait_before_start = true;

        // Tabbing
        Tabbing tabbing = new Tabbing();

        // Temp health
        //private UInt64 health_temp = 0;

        // Main class
        public PVP()
        {
            // Unstucking system timer
            unstucking_system.Tick += new EventHandler(unstucking_system_Tick);
            // Unstuck attack
            unstucking_system_attack.Tick += new EventHandler(unstucking_system_attack_Tick);
            // Unstucking prewarm
            unstucking_system_prewarm.Tick += new EventHandler(unstucking_system_prewarm_Tick);
        }

        // Start
        public void start()
        {
            // States switching
            states_switching();

            // Set Timer
            set_timer();

            // Check if character is dead
            resurrection_check();
        }

        // States
        private void buffing()
        {
            // Stop walking
            if (stats.player_ctm_push() != 4294967295)
            {
                stats.player_ctm_push(4294967295);
            }

            // Wait
            System.Threading.Thread.Sleep(500 + randomnr.create(100, 350));

            if (!unstucking_now)
            {
                // Use keys
                keyusingsystem.skill_activation();
            }

            // Enable healing
            player_healing = true;

            //  Set everything else off
            player_attacking = false;
            player_looting = false;
            player_walking = false;
            player_buffing = false;
        }

        private void healing()
        {
            if ((stats.player_health_inp() < Convert.ToUInt32(Properties.Settings.Default.healing_under_np) | (stats.player_shield_inp() < Convert.ToUInt32(Properties.Settings.Default.healing_under_np) && !Properties.Settings.Default.ignore_shield_healing)) && Properties.Settings.Default.healing_under_np != 0 && !player_healing_enabled)
            {
                // Enable healing
                player_healing_enabled = true;
            }
            else if ((stats.player_health_inp() > Convert.ToUInt32(Properties.Settings.Default.healing_under_np) && (stats.player_shield_inp() > Convert.ToUInt32(Properties.Settings.Default.healing_under_np) | Properties.Settings.Default.ignore_shield_healing)) && Properties.Settings.Default.healing_under_np != 0 && !player_healing_enabled)
            {
                // Find closes wp
                find_waypoint.closes_waypoint();

                if (find_waypoint.waypoint_found)
                {
                    // Set this waypoint to the current list
                    CTM_System.ctm_points = CTM_System.waypoints_list[find_waypoint.best_waypoint_index];
                    CTM_System.waypoint_count = find_waypoint.best_waypoint_index;

                    // Set this CTM
                    stats.player_ctm_x(CTM_System.ctm_points.X);
                    stats.player_ctm_y(CTM_System.ctm_points.Y);

                    // Enable walking
                    player_walking = true;

                    // disable healing
                    player_healing_enabled = false;

                    //  Set everything else off
                    player_attacking = false;
                    player_buffing = false;
                    player_healing = false;
                    player_looting = false;

                    // Reset waypoint found
                    find_waypoint.waypoint_found = false;
                }
            }

            if (player_healing_enabled)
            {
                if (stats.player_health_inp() < Convert.ToUInt32(Properties.Settings.Default.heal_until_percent - 1) | (stats.player_shield_inp() < Convert.ToUInt32(Properties.Settings.Default.heal_until_percent - 1) && !Properties.Settings.Default.ignore_shield_healing))
                {
                    // Stop moving
                    stats.player_ctm_push(4294967295);

                    if (Properties.Settings.Default.heal_until_100_key_using_enabled)
                    {
                        // Use keys
                        keyusingsystem.skill_activation();
                    }
                }

                if (stats.player_health_inp() > Convert.ToUInt32(Properties.Settings.Default.heal_until_percent - 5) && (stats.player_shield_inp() > Convert.ToUInt32(Properties.Settings.Default.heal_until_percent - 5) | Properties.Settings.Default.ignore_shield_healing))
                {
                    // Find closes wp
                    find_waypoint.closes_waypoint();

                    if (find_waypoint.waypoint_found)
                    {
                        // Set this waypoint to the current list
                        CTM_System.ctm_points = CTM_System.waypoints_list[find_waypoint.best_waypoint_index];
                        CTM_System.waypoint_count = find_waypoint.best_waypoint_index;

                        // Set this CTM
                        stats.player_ctm_x(CTM_System.ctm_points.X);
                        stats.player_ctm_y(CTM_System.ctm_points.Y);

                        // Enable walking
                        player_walking = true;

                        // disable healing
                        player_healing_enabled = false;

                        //  Set everything else off
                        player_attacking = false;
                        player_buffing = false;
                        player_healing = false;
                        player_looting = false;

                        // Reset waypoint found
                        find_waypoint.waypoint_found = false;
                    }
                }

                if (stats.player_aggro() == 1 && stats.player_targetid() != 0 && !Blacklist.blacklisted)
                {
                    // Reset everything
                    find_waypoint.best_waypoint_index = 0;
                    find_waypoint.dist = double.PositiveInfinity;
                    find_waypoint.temp_dist = 0;

                    // Enable walking
                    player_attacking = true;

                    // disable healing
                    player_healing_enabled = false;

                    //  Set everything else off
                    player_walking = false;
                    player_buffing = false;
                    player_healing = false;
                    player_looting = false;

                    // Reset waypoint found
                    find_waypoint.waypoint_found = false;
                }
            }
            else if (Properties.Settings.Default.healing_under_np == 0)
            {
                // Find closes wp
                find_waypoint.closes_waypoint();


                // Set this waypoint to the current list
                CTM_System.ctm_points = CTM_System.waypoints_list[find_waypoint.best_waypoint_index];
                CTM_System.waypoint_count = find_waypoint.best_waypoint_index;

                // Set this CTM
                stats.player_ctm_x(CTM_System.ctm_points.X);
                stats.player_ctm_y(CTM_System.ctm_points.Y);

                // Enable walking
                player_walking = true;

                // disable healing
                player_healing_enabled = false;

                //  Set everything else off
                player_attacking = false;
                player_buffing = false;
                player_healing = false;
                player_looting = false;

                // Reset waypoint found
                find_waypoint.waypoint_found = false;
            }
        }

        private void attacking()
        {
            if (stats.player_targetid() != 0)
            {
                // Reduce the distance to the next position for ctm
                stats.player_ctm_distance(Properties.Settings.Default.attack_range - randomnr.create(0.0, 0.3));

                // Unstucking prewarm
                if (stats.player_ctm_push() == 2)
                {
                    // Prewarm for attack unstucking
                    unstucking_system_attack.Start();
                }
            }

            if (stats.player_targetid() != 0)
            {
                // Prewarm for normal unstucking
                unstucking_system_prewarm.Start(); // Start unstucking prewarm
            }

            if (TargetInfo.target_distance_toplayer > Properties.Settings.Default.attack_range && TargetInfo.target_id != 0) // Do this only if the target is out of range
            {
                // Set the ctm to the target's position
                stats.player_ctm_x(TargetInfo.target_position_x);
                stats.player_ctm_y(TargetInfo.target_position_y);

                // Interact/Push
                if (stats.player_ctm_push() != 2 && stats.player_targetid() != 0)
                {
                    stats.player_ctm_push(2);
                }
            }
            else if (TargetInfo.target_distance_toplayer < Properties.Settings.Default.attack_range | TargetInfo.target_id == 0)
            {
                stats.player_ctm_push(4294967295);
            }

            // Activate skills
            skillsystem.skill_activation(); // Skill system 
            keyusingsystem.skill_activation(); // Key using

            // Move if mob is casting
            //castingmovement.move();

            if (TargetInfo.target_distance_toplayer < Properties.Settings.Default.attack_range && TargetInfo.target_id != 0 && Properties.Settings.Default.fight_movement_enabled)
            {
                // Fight movement
                fightmovement.move();
            }
        }

        private void looting()
        {
            // Enable buffing
            player_buffing = true;
        }

        // Bool match started
        bool find_wp = false;

        private void walking()
        {
            // Walk
            if ((stats.pvp_match_status() == 1 | stats.pvp_match_status() == 2) && stats.pvp_match_ingame() == 1) // Check the distance
            {
                if (find_wp)
                {
                    // Find closest WP
                    find_waypoint.closes_waypoint();

                    // Set the new spot and change the status
                    if (find_waypoint.waypoint_found)
                    {
                        // Set this waypoint to the current list
                        CTM_System.ctm_points = CTM_System.waypoints_list[find_waypoint.best_waypoint_index];
                        CTM_System.waypoint_count = find_waypoint.best_waypoint_index;

                        // Set this CTM
                        stats.player_ctm_x(CTM_System.ctm_points.X);
                        stats.player_ctm_y(CTM_System.ctm_points.Y);

                        // Turn off this function
                        find_wp = false;

                        // Reset
                        find_waypoint.waypoint_found = false;
                    }
                }

                if (stats.pvp_match_status() == 1 && !find_wp) // Wait ...
                {
                    if (player_walking_togate)
                    {

                            // Set this waypoint to the current list
                            CTM_System.ctm_points = CTM_System.waypoints_list[find_waypoint.best_waypoint_index];
                            CTM_System.waypoint_count = find_waypoint.best_waypoint_index;

                            // Set this CTM
                            stats.player_ctm_x(CTM_System.ctm_points.X);
                            stats.player_ctm_y(CTM_System.ctm_points.Y);

                            ctm_system.walking(); // Call the walking function
                            System.Threading.Thread.Sleep(1200 + randomnr.create(0, 186 + randomnr.create(4, 52)));
                            player_walking_togate = false; // Turn off
                    }

                    if (!player_walking_togate)
                    {
                        // Stop players movement fix
                        stats.player_ctm_push(4294967295);
                    }
                }

                if (stats.pvp_match_status() == 2 && !find_wp) // PVP matchstarted
                {
                    if (stats.player_health() != 0) // Make sure this works only if the player isn't dead
                    {
                        // Brauch kein closes wp, muss noch einstellen, dass er einfach zu den normalen wechselst, falls zu weit entfernt
                        if (!player_ressurection) // if not in death walking mode
                        {
                            ctm_system.walking(); // Call the walking function

                            if (!unstucking_now)
                            {
                                // Tabbing
                                tabbing.search();
                            }

                            // Reset for skills counter
                            KeyusingSystem.skills_counter = 1;
                            SkillsSystem.skills_counter = 1;
                        }
                        else if (player_ressurection) // Death spots
                        {
                            // Walking
                            if (Properties.Settings.Default.enable_defending_after_death == true | Properties.Settings.Default.enable_tabbing_after_death == true)
                            {
                                if (TargetInfo.target_tid == 0 && stats.player_aggro() == 0)
                                {

                                    ctm_system.walking_dead(); // Call the walking function

                                    if (unstucking_now == false)
                                    {
                                        if (Properties.Settings.Default.enable_tabbing_after_death == true)
                                        {
                                            // Tabbing
                                            tabbing.search();
                                        }
                                    }
                                }
                            }
                            else // Just walk ...
                            {
                                ctm_system.walking_dead(); // Call the walking function
                            }

                            // Auto reset for skills counter
                            KeyusingSystem.skills_counter = 1;
                            SkillsSystem.skills_counter = 1;
                        }
                    }
                }
            } // END Distance check
            else if (stats.pvp_match_ingame() == 0)
            {
                // Stop moving
                stats.player_ctm_push(4294967295);

                // Reset ctm
                CTM_System.ctm_points = CTM_System.waypoints_list[0];
                CTM_System.waypoint_count = 0;
                try
                {
                    CTM_System.ctm_death_points = CTM_System.waypoints_death_list[0];
                }
                catch { }
                CTM_System.waypoint_death_count = 0;

                // Do nothing ... chill and enable cooldown for pvp

                // Turn on
                player_walking_togate = true;
                find_wp = true;

                // Click accept
                if (stats.pvp_match_ready() != 0)
                {
                    PVPAccept.accept();
                }
            }
        }

        // Check if player is dead
        public void resurrection_check()
        {
            if (stats.player_health() == 0)
            {
                // Exit target
                stats.player_targetid(0);

                // Enable walking
                player_walking = true;

                //  Set everything else off
                player_attacking = false;
                player_buffing = false;
                player_looting = false;
                player_autoselling = false;

                // Let the player walk in death walking mode
                player_ressurection = true;

                System.Threading.Thread.Sleep(7000); // Wait until clicking G

                for (int i = 0; i < 5; i++) // Click G for resurrection
                {
                    Keysimulation.SimulateKeys.G();

                    if (i > 4 && stats.player_health() != 0)
                    {
                        break;
                    }
                    else if (i > 4 && stats.player_health() == 0)
                    {
                        i = 0;
                    }
                }

                try
                {
                    // Reset the death spots
                    CTM_System.waypoint_death_count = 0; // Reset the counter
                    ctm_system.walking_dead_loop = false; // Set the loop to false

                    // Reset list choice
                    CTM_System.walking_second_death_list = true;
                    CTM_System.walking_first_death_list = true;

                    // Reset the normal waypoints to ZERO
                    CTM_System.waypoint_count = 0;
                    CTM_System.ctm_points = CTM_System.waypoints_list[0];
                }
                catch { MainWindow.log_text = "No death spots available. Stop bot."; MainWindow.bot_running = false; }
            }
        }

        // Autologin check
        public void autologin_check()
        {
            // Auto login
            if (player_autologin)
            {
                //  Set everything else off
                player_attacking = false;
                player_buffing = false;
                player_healing = false;
                player_healing_enabled = false;
                player_looting = false;
                player_walking = false;

                // Search nearest wp
                if (!player_ressurection)
                {
                    // Find closes wp
                    find_waypoint.closes_waypoint();

                    if (find_waypoint.waypoint_found)
                    {
                        // Set this waypoint to the current list
                        CTM_System.ctm_points = CTM_System.waypoints_list[find_waypoint.best_waypoint_index];
                        CTM_System.waypoint_count = find_waypoint.best_waypoint_index;

                        // Set this CTM
                        stats.player_ctm_x(CTM_System.ctm_points.X);
                        stats.player_ctm_y(CTM_System.ctm_points.Y);

                        // Enable walking
                        player_walking = true;

                        // disable healing
                        player_healing_enabled = false;

                        //  Set everything else off
                        player_attacking = false;
                        player_buffing = false;
                        player_healing = false;
                        player_looting = false;

                        // Reset waypoint found
                        find_waypoint.waypoint_found = false;
                    }
                }

                // Disable autologin
                player_autologin = false;

                // Enable walking
                player_walking = true;
            }
        }

        // Switch everythiiiing
        public void states_switching()
        {
            // Stats requirements
            if (TargetInfo.target_owner == 0 && !player_attacking && !Blacklist.blacklisted) // Attacking
            {
                if (TargetInfo.target_id != 0)
                {
                    if ((TargetInfo.target_tid == 0 | TargetInfo.target_tid != stats.player_id() | TargetInfo.target_tid == stats.player_id()) && !Blacklist.blacklisted)
                    {
                        if (TargetInfo.target_id != 0)
                        {
                            // Log
                            MainWindow.log_text = "Attacking: " + TargetInfo.target_id.ToString() + " " + TargetInfo.target_name + ".";
                        }

                        // Stop walking
                        if (!player_attacking)
                        {
                            stats.player_ctm_push(4294967295);
                        }

                        // Enable attacking
                        player_attacking = true;

                        // Set every other phase off
                        player_buffing = false;
                        player_looting = false;
                        player_walking = false;
                        player_healing = false;
                        player_healing_enabled = false;

                        // Disable ctm unstucking system
                        CTM_System.unstucking_system_Timer.Stop();
                        CTM_System.unstucking_active = false;
                    }
                }
            }
            else if (player_attacking) // Looting
            {
                if (stats.player_targetid() == 0 | TargetInfo.target_health == 0)
                {
                    // Log
                    MainWindow.log_text = "Target is dead or lost. " + "\n" + "Looting if enabled.";

                    if (TargetInfo.target_health == 0) // If the target is dead, clear the player stats
                    {
                        stats.player_targetid(0);
                    }

                    player_looting = true; // Turn on looting

                    // Set every other phase off
                    player_buffing = false;
                    player_attacking = false;
                    player_walking = false;
                    player_healing = false;
                    player_healing_enabled = false;
                }
            }
            else if (player_buffing && !player_attacking) // Buffing
            {
                // Set everything else off
                player_looting = false;
                player_attacking = false;
                player_walking = false;
                player_healing = false;
                player_healing_enabled = false;
            }
            else if ((player_walking && !player_healing) | (unstucking_now && !player_healing && !player_attacking) | Blacklist.blacklisted)
            {
                // Reset Counter
                SkillsSystem.skills_counter = 1;

                // Enable walking
                player_walking = true;

                //  Set everything else off
                player_attacking = false;
                player_buffing = false;
                player_looting = false;
                player_healing = false;
                player_healing_enabled = false;
            }

            // Stats switching
            if (player_buffing)
            {
                buffing();
            }

            if (player_healing)
            {
                healing();
            }

            if (player_attacking)
            {
                attacking();
            }

            if (player_looting)
            {
                looting();
            }

            if (player_walking | Blacklist.blacklisted)
            {
                // Run
                walking();

                // Stop attack
                stop_unstucking_timer();
            }

            // Autologin check
            if (player_autologin)
            {
                autologin_check();
            }

            // Death target fix
            if (TargetInfo.target_id != 0 && (TargetInfo.target_health == 0 | (stats.player_targetclass() == 2 && !Properties.Settings.Default.aim_friendly_target)))
            {
                stats.player_targetid(0);
            }

            // Check window stats
            window_stats_check();

            // Leave target system
            if (TargetInfo.target_id != 0)
            {
                if (TargetInfo.target_health == 0 | TargetInfo.target_owner != 0)
                {
                    stats.player_targetid(0);
                }
            }
        }

        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMAXIMIZED = 3;
        private const int SW_RESTORE = 9;

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        // Check if window is minimized
        public void window_stats_check()
        {
            // Restore the window if it is minimized
            ShowWindow(MainWindow.hwnd, SW_RESTORE);
        }

        // Distance calculation
        public double Distance2D(float x1, float y1, float x2, float y2)
        {
            double result = 0;

            double part1 = System.Math.Pow((x2 - x1), 2);

            double part2 = System.Math.Pow((y2 - y1), 2);

            double underRadical = part1 + part2;

            result = System.Math.Sqrt(underRadical);

            return result;
        }
        public static double Distance3D(float x1, float y1, float z1, float x2, float y2, float z2)
        {
            //     __________________________________
            //d = √ (x2-x1)^2 + (y2-y1)^2 + (z2-z1)^2
            //

            // Our end result
            double result = 0;
            // Take x2-x1, then square it
            double part1 = System.Math.Pow((x2 - x1), 2);
            // Take y2-y1, then sqaure it
            double part2 = System.Math.Pow((y2 - y1), 2);
            // Take z2-z1, then square it
            double part3 = System.Math.Pow((z2 - z1), 2);
            // Add both of the parts together
            double underRadical = part1 + part2 + part3;
            // Get the square root of the parts
            result = System.Math.Sqrt(underRadical);
            // Return our result
            return result;
        }

        #region Unstucking
        // Stop timer
        public void stop_unstucking_timer()
        {
            // Reset the timer time
            if (unstucking_system_prewarm.Interval.Seconds != Properties.Settings.Default.unstucking_attack_time)
            {
                unstucking_system_prewarm.Interval = new TimeSpan(0, 0, Properties.Settings.Default.unstucking_attack_time);
            }

            if (unstucking_system_attack.Interval.Seconds != 8)
            {
                unstucking_system_attack.Interval = new TimeSpan(0, 0, 8);
            }

            // Stop the timer
            unstucking_system_prewarm.Stop();
            unstucking_system_attack.Stop();
        }

        // Set timer
        public void set_timer()
        {
            if (unstucking_system.Interval.TotalSeconds != Properties.Settings.Default.unstucking_attack_duration)
            {
                unstucking_system.Interval = new TimeSpan(0, 0, Properties.Settings.Default.unstucking_attack_duration);
            }


            if (unstucking_system_attack.Interval.TotalSeconds != 8)
            {
                unstucking_system_attack.Interval = new TimeSpan(0, 0, 8);
            }

            if (unstucking_system_prewarm.Interval.TotalSeconds != Properties.Settings.Default.unstucking_attack_time)
            {
                unstucking_system_prewarm.Interval = new TimeSpan(0, 0, Properties.Settings.Default.unstucking_attack_time);
            }

            if (!unstucking_now)
            {
                unstucking_system.Stop(); // Disbale timer
            }
        }

        // Unstucking System
        private void unstucking_system_Tick(object sender, EventArgs e)
        {
            unstucking_now = false; // Disable unstucking
        }

        // Unstucking prewarm system
        private void unstucking_system_prewarm_Tick(object sender, EventArgs e)
        {
            if (stats.player_targetid() != 0 && stats.player_aggro() == 0)
            {
                // Stop walking
                stats.player_ctm_push(4294967295);

                stats.player_targetid(0);

                // Enable unstucking system
                unstucking_now = true;
                unstucking_system.Start();

                unstucking_system_prewarm.Stop(); // Stop itself
            }
            else
            {
                unstucking_system_prewarm.Stop(); // Stop itself
            }
        }

        // Unstuck attack if attacking takes too long
        private void unstucking_system_attack_Tick(object sender, EventArgs e)
        {
            //if (TargetInfo.health_inp > health_temp)
            //{
            //    // Stop walking
            //    stats.player_ctm_push(4294967295);

            //    // Enable unstucking system
            //    unstucking_now = true;
            //    unstucking_system.Start();

            //    // Delete the target
            //    stats.player_targetid(0);

            //    // Stop the unstucking activator
            //    unstucking_system_attack.Stop(); // Stop itself
            //}
            //else
            //{
            //    unstucking_system_attack.Stop(); // Stop itself
            //}
        }
        #endregion Unstucking
    }
}
