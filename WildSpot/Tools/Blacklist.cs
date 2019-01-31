using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace StarSpot
{
    class Blacklist
    {
        // Blacklist NPC list
        public static string[] blacklist_npc_list = new string[40];

        // Timer for loading configs
        DispatcherTimer timer = new DispatcherTimer();

        // Classes
        Stats stats = new Stats();

        // Blacklist bool
        public static bool blacklisted = false;

        public Blacklist()
        {
            // Timer settings
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            if (stats.player_targetid() != 0 && TargetInfo.target_id != 0 && Properties.Settings.Default.mods != "Combat" && MainWindow.bot_running)
            {
                if (!Properties.Settings.Default.whitelist_enabled && Properties.Settings.Default.mods != "Gathering")
                {
                    if (Properties.Settings.Default.mods != "PVP")
                    {
                        if (stats.player_aggro() == 0)
                        {
                            if ((blacklist_npc_list.Contains(TargetInfo.target_name) && blacklist_npc_list.Count() != 0) | TargetInfo.target_level <= Convert.ToUInt64(Properties.Settings.Default.level_range) | TargetInfo.target_distance > Properties.Settings.Default.search_range + 10 | (TargetInfo.target_typ == 20 && TargetInfo.target_tid == stats.player_id()))
                            {
                                blacklisted = true;

                                stats.player_targetid(0); // Remove the target
                            }
                            else
                            {
                                blacklisted = false;
                            }
                        }

                        if (stats.player_aggro() != 0 && (Properties.Settings.Default.mods == "Grinding" | Properties.Settings.Default.mods == "Gathering") && TargetInfo.target_aggro != 0)
                        {
                            blacklisted = false;
                        }

                        // Player ignoring
                        if(TargetInfo.target_typ == 20 && Properties.Settings.Default.enable_ignore_players)
                        {
                            blacklisted = true;

                            stats.player_targetid(0); // Remove the target
                        }

                        // Check distance to leave a target
                        if (TargetInfo.target_distance > Properties.Settings.Default.search_range)
                        {
                            if (Properties.Settings.Default.mods == "Grinding" && !Grinding.player_ressurection)
                            {
                                blacklisted = true;

                                // Leave the target
                                stats.player_targetid(0);
                            }
                        }
                    }

                    // Leave target if it's blacklisted and has no aggro
                    if (blacklist_npc_list.Contains(TargetInfo.target_name) && TargetInfo.target_aggro == 0)
                    {
                        // Leave the target
                        stats.player_targetid(0);

                        // Enable blacklist
                        blacklisted = true;
                    }
                }
                else // Whitelist
                {
                    if (stats.player_aggro() == 0)
                    {
                        if (!blacklist_npc_list.Contains(TargetInfo.target_name) | TargetInfo.target_level <= Convert.ToUInt64(Properties.Settings.Default.level_range) | TargetInfo.target_distance > Properties.Settings.Default.search_range + 10)
                        {
                            blacklisted = true;

                            // Leave the target
                            stats.player_targetid(0);
                        }
                        else
                        {
                            blacklisted = false;
                        }
                    }

                    // Player ignoring
                    if (TargetInfo.target_typ == 20 && Properties.Settings.Default.enable_ignore_players)
                    {
                        blacklisted = true;

                        stats.player_targetid(0); // Remove the target
                    }

                    // Check distance to leave a target
                    if (TargetInfo.target_distance > Properties.Settings.Default.search_range && Properties.Settings.Default.mods != "PVP")
                    {
                        if (stats.player_aggro() == 0)
                        {
                            blacklisted = true;

                            // Leave the target
                            stats.player_targetid(0);
                        }
                    }

                    // Leave target if it's blacklisted and has no aggro
                    if (!blacklist_npc_list.Contains(TargetInfo.target_name) && TargetInfo.target_aggro == 0)
                    {
                        // Leave the target
                        stats.player_targetid(0);

                        // Enable blacklist
                        blacklisted = true;
                    }
                }

                // PVP only
                if (Properties.Settings.Default.mods == "PVP")
                {
                    if (TargetInfo.target_distance_toplayer > Properties.Settings.Default.search_range + 5)
                    {
                        // Enable blacklist
                        blacklisted = true;

                        // Leave the target
                        stats.player_targetid(0);
                    }
                    else
                    {
                        blacklisted = false;
                    }
                }
            }

            if (TargetInfo.target_id == 0 | stats.player_targetid() == 0) // Disable blacklist if no target
            {
                //blacklisted = false;
            }

            // Load the npcs
            try
            {
                blacklist_npc_list = Properties.Settings.Default.blacklist_npcs.Split(',');
            }
            catch { }
        }
    }
}
