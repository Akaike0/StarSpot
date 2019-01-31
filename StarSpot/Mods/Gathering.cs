using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace StarSpot
{
    class Gathering
    {
        // Stats
        public static bool player_buffing = false;
        public static bool player_healing = false;
        private bool player_healing_enabled = false;
        public static bool player_attacking = false;
        public static bool player_looting = false;
        public static bool player_walking = true;
        public static bool player_ressurection = false;
        public static bool player_autologin = false;

        // Options
        public bool looting_option = true;

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

        // Looting class
        Looting lootingC = new Looting();

        // Autoselling timer & bool
        public static bool player_autoselling = false;
        DispatcherTimer autoselling_timer = new DispatcherTimer();

        // Bot using class
        PlayerBots player_bots = new PlayerBots();

        // Casting movement
        CastingMovement castingmovement = new CastingMovement();

        // Fight movement
        FightMovement fightmovement = new FightMovement();

        // Find target
        FindTarget findtarget = new FindTarget();

        // Tabbing
        Tabbing tabbing = new Tabbing();

        // RN
        RandomNR randomNr = new RandomNR();

        // Main class
        public Gathering()
        {
            // Unstucking system timer
            unstucking_system.Tick += new EventHandler(unstucking_system_Tick);
            // Unstuck attack
            unstucking_system_attack.Tick += new EventHandler(unstucking_system_attack_Tick);
            // Autoselling timr
            autoselling_timer.Tick += new EventHandler(autoselling_timer_Tick);
            // Unstucking prewarm
            unstucking_system_prewarm.Tick += new EventHandler(unstucking_system_prewarm_Tick);
        }

        // Start
        public void start()
        {
            // Set options
            if (looting_option != Properties.Settings.Default.looting_option) // Looting
            {
                looting_option = Properties.Settings.Default.looting_option;
            }

            // States switching
            states_switching();

            // Set Timer
            set_timer();

            // Check if character is dead
            resurrection_check();
        }

        // Buffing and healing
        public void buffing()
        {
            // Stop walking
            if (stats.player_ctm_push() != 4294967295)
            {
                stats.player_ctm_push(4294967295);
            }

            // Wait
            System.Threading.Thread.Sleep(500 + randomNr.create(100, 350));

            // Log
            MainWindow.log_text = "Checking Key Using";

            // Use keys
            keyusingsystem.skill_activation();

            // Enable healing
            player_healing = true;

            // Set everything else off
            player_looting = false;
            player_attacking = false;
            player_healing_enabled = false;
            player_walking = false;
            player_buffing = false;
        }

        public void healing()
        {
            if ((stats.player_health_inp() < Convert.ToUInt32(Properties.Settings.Default.healing_under_np) | (stats.player_shield_inp() < Convert.ToUInt32(Properties.Settings.Default.healing_under_np) && !Properties.Settings.Default.ignore_shield_healing)) && Properties.Settings.Default.healing_under_np != 0 && !player_healing_enabled)
            {
                // Log
                MainWindow.log_text = "Healing activated";

                // Enable healing
                player_healing_enabled = true;
            }
            else if ((stats.player_health_inp() > Convert.ToUInt32(Properties.Settings.Default.healing_under_np) && (stats.player_shield_inp() > Convert.ToUInt32(Properties.Settings.Default.healing_under_np) | Properties.Settings.Default.ignore_shield_healing)) && Properties.Settings.Default.healing_under_np != 0 && !player_healing_enabled)
            {
                if (Properties.Settings.Default.move_to_closest_spot && (CTM_System.waypoint_count != 0 | !player_autoselling) && !player_ressurection)
                {
                    // Log
                    MainWindow.log_text = "Checked health.";

                    // Find closes wp
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
                else
                {
                    // Log
                    MainWindow.log_text = "Checked health.";

                    // Enable walking
                    player_walking = true;

                    //  Set everything else off
                    player_attacking = false;
                    player_buffing = false;
                    player_healing = false;
                    player_looting = false;
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
                    if (Properties.Settings.Default.move_to_closest_spot && (CTM_System.waypoint_count != 0 | !player_autoselling) && !player_ressurection)
                    {
                        // Log
                        MainWindow.log_text = "Checked health.";

                        // Find closes wp
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
                    else
                    {
                        // Enable walking
                        player_walking = true;

                        // disable healing
                        player_healing_enabled = false;

                        //  Set everything else off
                        player_attacking = false;
                        player_buffing = false;
                        player_healing = false;
                        player_looting = false;
                    }
                }

                if (stats.player_aggro() == 1)
                {
                    // Log
                    MainWindow.log_text = "Bot got aggro!";

                    // Enable walking
                    player_attacking = true;

                    // disable healing
                    player_healing_enabled = false;

                    //  Set everything else off
                    player_walking = false;
                    player_buffing = false;
                    player_healing = false;
                    player_looting = false;
                }
            }
            if (stats.player_aggro() == 1 && Properties.Settings.Default.healing_under_np == 0)
            {
                // Log
                MainWindow.log_text = "Bot got aggro!";

                // Enable walking
                player_attacking = true;

                // disable healing
                player_healing_enabled = false;

                //  Set everything else off
                player_walking = false;
                player_buffing = false;
                player_healing = false;
                player_looting = false;
            }
            else if (stats.player_aggro() == 0 && Properties.Settings.Default.healing_under_np == 0)
            {
                if (Properties.Settings.Default.move_to_closest_spot && (CTM_System.waypoint_count != 0 | !player_autoselling) && !player_ressurection)
                {
                    // Log
                    MainWindow.log_text = "Healing deactivated.";

                    // Find closes wp
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
                else
                {
                    // Log
                    MainWindow.log_text = "Healing deactivated.";

                    // Enable walking
                    player_walking = true;

                    // disable healing
                    player_healing_enabled = false;

                    //  Set everything else off
                    player_attacking = false;
                    player_buffing = false;
                    player_healing = false;
                    player_looting = false;
                }
            }
        }

        public void attacking()
        {
            if (stats.player_targetid() != 0)
            {
                // Push CTM to target
                if (!unstucking_now)
                {
                    // Reduce the distance to the next position for ctm
                    stats.player_ctm_distance(Properties.Settings.Default.attack_range - randomNr.create(0.0, 0.3));

                    // Set the ctm to the target's position
                    stats.player_ctm_x(TargetInfo.target_position_x);
                    stats.player_ctm_y(TargetInfo.target_position_y);
                    stats.player_ctm_z(TargetInfo.target_position_z);

                    // Interact/Push
                    if (stats.player_ctm_push() != 2)
                    {
                        stats.player_ctm_push(2);
                    }
                }

                // Attack the target
                if (stats.player_aggro() == 0 && !Properties.Settings.Default.gathering_attack_target)
                {
                    stats.player_ctm_interact(1);
                }

                // Check if player got aggro
                if(stats.player_aggro() == 1)
                {
                    // Leave the target 
                    if(TargetInfo.target_tid != stats.player_id() && TargetInfo.target_id != 0)
                    {
                        stats.player_targetid(0);
                        System.Threading.Thread.Sleep(1000);
                    }
                }

                // Unstucking prewarm
                if (stats.player_ctm_push() == 2)
                {
                    // Prewarm for attack unstucking
                    unstucking_system_attack.Start();

                    // Prewarm for normal unstucking
                    unstucking_system_prewarm.Start();
                }
            }

            // Move if mob is casting
            castingmovement.move();

            // Activate skills
            keyusingsystem.skill_activation(); // Key using
            skillsystem.skill_activation(); // Skill system 

            if (TargetInfo.target_distance_toplayer < Properties.Settings.Default.attack_range && TargetInfo.target_id != 0 && Properties.Settings.Default.fight_movement_enabled)
            {
                if (!Blacklist.blacklist_npc_list.Contains(TargetInfo.target_name) | TargetInfo.target_aggro != 0)
                {
                    // Fight movement
                    fightmovement.move();
                }
            }
        }

        public void looting()
        {
            ctm_system.waypoints_reset(); // Reset Waypoint

            // Enable buffing
            player_buffing = true;
        }

        public void walking()
        {
            if (stats.player_health() != 0) // Make sure this works only if the player isn't dead
            {
                if (!player_ressurection && !player_autoselling) // if not in death walking mode
                {
                    // Walking
                    if ((TargetInfo.target_tid == 0 && stats.player_aggro() == 0) | (Blacklist.blacklisted && stats.player_aggro() == 0) | stats.player_targetid() == stats.player_id())
                    {
                        ctm_system.walking(); // Call the walking function

                        if (!unstucking_now)
                        {
                            // Tab
                            findtarget.next_target();
                        }
                    }

                    // Reset for skills counter
                    KeyusingSystem.skills_counter = 1;
                    SkillsSystem.skills_counter = 1;
                }
                else if (player_ressurection) // Death spots
                {
                    // Walking
                    if (Properties.Settings.Default.enable_defending_after_death | Properties.Settings.Default.enable_tabbing_after_death)
                    {
                        if ((TargetInfo.target_tid == 0 && stats.player_aggro() == 0) | (Blacklist.blacklisted && stats.player_aggro() == 0))
                        {
                            ctm_system.walking_dead(); // Call the walking function

                            if (!unstucking_now)
                            {
                                if (Properties.Settings.Default.enable_tabbing_after_death)
                                {
                                    // Tab
                                    findtarget.next_target();
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
                else if (player_autoselling && !player_ressurection) // Enable autoselling
                {
                    if ((TargetInfo.target_tid == 0 && stats.player_aggro() == 0) | (Blacklist.blacklisted && stats.player_aggro() == 0)) // Only if player has no aggro
                    {
                        autoselling();
                    }

                }
            }
        }

        //public void autoselling()
        //{
        //    if (CTM_System.waypoint_count != 0)
        //    {
        //        // Walk until the first spot
        //        if ((TargetInfo.target_tid == 0 && stats.player_aggro() == 0) | (Blacklist.blacklisted && stats.player_aggro() == 0))
        //        {
        //            ctm_system.walking(); // Call the walking function

        //            if (!unstucking_now && Properties.Settings.Default.selling_spots_attack_mobs)
        //            {
        //                // Tab
        //                tabbing.search();
        //            }
        //        }

        //        // Reset for skills counter
        //        KeyusingSystem.skills_counter = 1;
        //        SkillsSystem.skills_counter = 1;
        //    }
        //    else if (CTM_System.waypoint_count == 0) // Make sure that the character is near the first normal spot
        //    {
        //        ctm_system.walking_selling();
        //    }

        //    if (CTM_System.selling_returned) // If the bot is back to the normal spots, disable autoselling
        //    {
        //        player_autoselling = false; // Disable autoselling 

        //        try
        //        {
        //            CTM_System.selling_returned = false; // Reset autoselling
        //            CTM_System.waypoint_selling_count = 0;
        //            CTM_System.ctm_selling_points = CTM_System.waypoints_selling_list[0];
        //        }
        //        catch { }
        //    }
        //}

        public void autoselling()
        {
            if (CTM_System.waypoint_count != 0)
            {
                // Walk until the first spot
                if ((TargetInfo.target_tid == 0 && stats.player_aggro() == 0) | (Blacklist.blacklisted && stats.player_aggro() == 0) | Properties.Settings.Default.enable_autoselling_ignore_mobs)
                {
                    ctm_system.walking(); // Call the walking function

                    if (!unstucking_now && Properties.Settings.Default.selling_spots_attack_mobs)
                    {
                        // Tab
                        tabbing.search();
                    }
                }

                // Reset for skills counter
                KeyusingSystem.skills_counter = 1;
                SkillsSystem.skills_counter = 1;
            }
            else if (CTM_System.waypoint_count == 0) // Make sure that the character is near the first normal spot
            {
                ctm_system.walking_selling();
            }

            if (CTM_System.selling_returned) // If the bot is back to the normal spots, disable autoselling
            {
                player_autoselling = false; // Disable autoselling 

                try
                {
                    CTM_System.selling_returned = false; // Reset autoselling
                    CTM_System.waypoint_selling_count = 0;
                    CTM_System.ctm_selling_points = CTM_System.waypoints_selling_list[0];
                }
                catch { }
            }
        }

        // Check if player is dead
        public void resurrection_check()
        {
            if (stats.player_health() == 0)
            {
                // Exit target
                stats.player_targetid(0);

                // Enable healing
                player_healing = true;

                //  Set everything else off
                player_attacking = false;
                player_buffing = false;
                player_looting = false;
                player_autoselling = false;
                player_walking = false;

                // Let the player walk in death walking mode
                player_ressurection = true;

                for (int i = 0; i < 3; i++) // Click G for resurrection
                {
                    Keysimulation.SimulateKeys.G();

                    if (stats.player_health() != 0)
                    {
                        break;
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

        public void states_switching()
        {
            // Stats requirements
            if (!player_attacking && (!player_ressurection | (player_ressurection && (Properties.Settings.Default.enable_tabbing_after_death | Properties.Settings.Default.enable_defending_after_death))) && (!player_autoselling | (player_autoselling && !Properties.Settings.Default.enable_autoselling_ignore_mobs))) // Attacking
            {
                if (TargetInfo.target_id != 0 | stats.player_aggro() != 0)
                {
                    if ((!Blacklist.blacklisted && (TargetInfo.target_target_tid == 0 | Properties.Settings.Default.attack_mobs_from_other_players)) | TargetInfo.target_tid == stats.player_id() | stats.player_aggro() != 0)
                    {
                        if (TargetInfo.target_name != stats.player_name())
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
            }
            else if (stats.player_aggro() == 0 && player_attacking == true) // Looting
            {
                if (stats.player_targetid() == 0 && TargetInfo.target_id == 0)
                {
                    // Log
                    MainWindow.log_text = "Target is dead or lost. " + "\n" + "Looting if enabled.";

                    player_looting = true; // Turn on looting

                    // Set every other phase off
                    player_buffing = false;
                    player_attacking = false;
                    player_walking = false;
                    player_healing = false;
                    player_healing_enabled = false;
                }
            }
            else if ((player_walking && !player_healing) | (unstucking_now && !player_healing && stats.player_aggro() == 0) | stats.player_targetid() == stats.player_id())
            {
                // Reset Counter
                SkillsSystem.skills_counter = 1;

                //  Set everything else off
                player_attacking = false;
                player_buffing = false;
                player_healing = false;
                player_healing_enabled = false;
                player_looting = false;

                // Enable walking
                player_walking = true;
            }

            // Stats switching
            if (player_buffing)
            {
                buffing();
            }

            if (player_healing)
            {
                if (stats.player_health() != 0) // Use only if player isn't dead
                {
                    healing();
                }
            }

            if (player_attacking)
            {
                attacking();
            }

            if (player_looting)
            {
                looting();
            }

            // Looting class for better looting
            lootingC.loot();

            if (player_walking | (Blacklist.blacklisted && stats.player_aggro() == 0 | ((!Properties.Settings.Default.enable_defending_after_death && !Properties.Settings.Default.enable_tabbing_after_death) && player_ressurection) | (player_autoselling && Properties.Settings.Default.enable_autoselling_ignore_mobs)))
            {
                if (!player_healing)
                {
                    // Run
                    walking();
                }

                // Stop attack
                stop_unstucking_timer();
            }

            // Autologin check
            if (player_autologin)
            {
                autologin_check();
            }

            // Leave target system
            if (TargetInfo.target_id != 0)
            {
                if (TargetInfo.target_health == 0 | (stats.player_targetclass() == 2 && !Properties.Settings.Default.aim_friendly_target))
                {
                    stats.player_targetid(0);
                }

                if (TargetInfo.target_tid != stats.player_id() && stats.player_aggro() == 0 && TargetInfo.target_target_typ == 20 && !Properties.Settings.Default.attack_mobs_from_other_players)
                {
                    stats.player_targetid(0);

                    unstucking_now = true;
                    unstucking_system.Start();
                }
            }
        }

        // Stop timer
        public void stop_unstucking_timer()
        {
            // Reset the timer time
            if (unstucking_system_prewarm.Interval.Seconds != Properties.Settings.Default.unstucking_attack_time)
            {
                unstucking_system_prewarm.Interval = new TimeSpan(0, 0, Properties.Settings.Default.unstucking_attack_time);
            }

            if (unstucking_system_attack.Interval.Seconds != 18)
            {
                unstucking_system_attack.Interval = new TimeSpan(0, 0, 18);
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

            if (unstucking_system_attack.Interval.TotalSeconds != 18)
            {
                unstucking_system_attack.Interval = new TimeSpan(0, 0, 18);
            }

            if (unstucking_system_prewarm.Interval.TotalSeconds != Properties.Settings.Default.unstucking_attack_time)
            {
                unstucking_system_prewarm.Interval = new TimeSpan(0, 0, Properties.Settings.Default.unstucking_attack_time);
            }

            if (unstucking_now == false)
            {
                unstucking_system.Stop(); // Disbale timer
            }

            if (Properties.Settings.Default.autoselling_enabled)
            {
                if (autoselling_timer.Interval.TotalMinutes != Properties.Settings.Default.autoselling_timer) // Set the time
                {
                    autoselling_timer.Interval = new TimeSpan(0, Properties.Settings.Default.autoselling_timer, 0);
                }

                // Start the timer
                autoselling_timer.Start();
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
            if (stats.player_aggro() == 0 && stats.player_iscasting() == 0)
            {
                // Stop walking
                stats.player_ctm_push(4294967295);

                stats.player_targetid(0);

                // Enable unstucking system
                unstucking_now = true;
                unstucking_system.Start(); ;

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
            if (TargetInfo.target_id != 0 && TargetInfo.target_health_inp == 100)
            {
                // Log
                MainWindow.log_text = "Try to unstuck!";

                // Stop walking
                stats.player_ctm_push(4294967295);

                // Delete the target
                stats.player_targetid(0);

                // Enable unstucking system
                unstucking_now = true;
                unstucking_system.Start();

                // Stop the unstucking activator
                unstucking_system_attack.Stop(); // Stop itself
            }
            else
            {
                unstucking_system_attack.Stop(); // Stop itself
            }
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

        // Autoselling timer
        private void autoselling_timer_Tick(object sender, EventArgs e)
        {
            if (!player_autoselling)
            {
                player_autoselling = true; // Enable autoselling bool

                // Add log
                MainWindow.log_text = "Autoselling activated!";
            }
        }
    }
}
