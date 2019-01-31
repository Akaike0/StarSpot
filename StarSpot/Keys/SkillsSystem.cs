using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;

namespace StarSpot
{
    class SkillsSystem
    {
        // Stats class
        Stats stats = new Stats();

        // Backgroundworker
        private BackgroundWorker mods_bgw = new BackgroundWorker();

        // Skills conditions array
        public string[] skill_r_array;
        public string[] skill_1_array;
        public string[] skill_2_array;
        public string[] skill_3_array;
        public string[] skill_4_array;
        public string[] skill_5_array;
        public string[] skill_6_array;
        public string[] skill_7_array;
        public string[] skill_8_array;

        // Skills Counter
        public static int skills_counter = 1;

        // Entity list
        ActorsList elist = new ActorsList();

        // Random NR
        RandomNR randomnr = new RandomNR();

        // Last selected target id
        private UInt64 last_target_id = 0;

        public double Distance2D(float x1, float y1, float x2, float y2)
        {
            double result = 0;

            double part1 = System.Math.Pow((x2 - x1), 2);

            double part2 = System.Math.Pow((y2 - y1), 2);

            double underRadical = part1 + part2;

            result = System.Math.Sqrt(underRadical);

            return result;
        } // Distance calculator

        public SkillsSystem()
        {
            // Load arrays
            skill_r_array = Properties.Settings.Default.skill_1_conditions_array.Split(',');
            skill_1_array = Properties.Settings.Default.skill_2_conditions_array.Split(',');
            skill_2_array = Properties.Settings.Default.skill_3_conditions_array.Split(',');
            skill_3_array = Properties.Settings.Default.skill_4_conditions_array.Split(',');
            skill_4_array = Properties.Settings.Default.skill_5_conditions_array.Split(',');
            skill_5_array = Properties.Settings.Default.skill_6_conditions_array.Split(',');
            skill_6_array = Properties.Settings.Default.skill_7_conditions_array.Split(',');
            skill_7_array = Properties.Settings.Default.skill_8_conditions_array.Split(',');
            skill_8_array = Properties.Settings.Default.skill_9_conditions_array.Split(',');

            // Set backgroundworker
            mods_bgw.DoWork += new DoWorkEventHandler(mods_bgw_DoWork);
        }

        // Skill aktivation
        public void skill_activation()
        {
            // Activate the bgw
            if (!mods_bgw.IsBusy)
            {
                mods_bgw.RunWorkerAsync();
            }

            // Load arrays
            skill_r_array = Properties.Settings.Default.skill_1_conditions_array.Split(',');
            skill_1_array = Properties.Settings.Default.skill_2_conditions_array.Split(',');
            skill_2_array = Properties.Settings.Default.skill_3_conditions_array.Split(',');
            skill_3_array = Properties.Settings.Default.skill_4_conditions_array.Split(',');
            skill_4_array = Properties.Settings.Default.skill_5_conditions_array.Split(',');
            skill_5_array = Properties.Settings.Default.skill_6_conditions_array.Split(',');
            skill_6_array = Properties.Settings.Default.skill_7_conditions_array.Split(',');
            skill_7_array = Properties.Settings.Default.skill_8_conditions_array.Split(',');
            skill_8_array = Properties.Settings.Default.skill_9_conditions_array.Split(',');
        }

        public void mods_bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            // Set the last target
            if (last_target_id == 0 && stats.player_targetid() != 0)
            {
                last_target_id = stats.player_targetid();
            }

            // Check if the character has a new target
            if(last_target_id != 0 && last_target_id != stats.player_targetid() && stats.player_targetid() != 0)
            {
                // Reset this function
                last_target_id = 0;

                // Reset skills
                skills_counter = 1;
            }

            // Execute skills
            if (((stats.player_targetid() == 0 | (TargetInfo.target_id != 0 && TargetInfo.target_tid == 0)) && stats.player_aggro() == 1) && Properties.Settings.Default.mods != "Combat") // If aggro but no target
            {
                if (stats.bots_amount() == 0)
                {
                    // Select nearest target
                    //Keysimulation.SimulateKeys.Tab();
                    try
                    {
                        elist.update(); // Update the list

                        foreach (Actors entity in elist)
                        {
                            if (Properties.Settings.Default.mods != "PVP")
                            {
                                // Distance to the npc
                                double distance = Distance2D(stats.player_position_x(), stats.player_position_y(), entity.position_x, entity.position_y);

                                if (distance <= Properties.Settings.Default.search_range + 40 && entity.target_target_id == stats.player_id() && entity.health != 0 && entity.aggro == 1)
                                {
                                    stats.player_targetHandle(128);
                                    stats.player_targetid((uint)entity.id);
                                }
                            }
                            else
                            {
                                // Tab
                                Keysimulation.SimulateKeys.Tab();
                            }
                        }
                    }
                    catch { }
                }
                else
                {
                    try
                    {
                        elist.update(); // Update the list

                        foreach (Actors entity in elist)
                        {
                            if (entity.unitowner == stats.player_id() && entity.target_target_id != 0) // Find a bot and go to the bot
                            {
                                // Distance to the bot
                                double distance = Distance2D(stats.player_position_x(), stats.player_position_y(), entity.position_x, entity.position_y);

                                //// Set the entity position into ctm
                                //stats.player_ctm_x(entity.position_x);
                                //stats.player_ctm_y(entity.position_y);

                                //stats.player_ctm_push(0); // Go!

                                
                                if (distance <= Properties.Settings.Default.search_range + 40 && stats.player_targetid() != entity.target_target_id && entity.health != 0)
                                {
                                    // Select nearest target
                                    stats.player_targetHandle(128);
                                    stats.player_targetid((uint)entity.target_target_id);
                                }
                            }
                        }
                    }
                    catch { }
                }
            }

            if (stats.player_targetid() != 0 && ((Properties.Settings.Default.mods != "Gathering" && TargetInfo.target_distance_toplayer <= Properties.Settings.Default.attack_range + 1.5f) | (Properties.Settings.Default.mods == "Gathering" && TargetInfo.target_distance_toplayer <= Properties.Settings.Default.attack_range + 1.5f && (stats.player_aggro() != 0 | Properties.Settings.Default.gathering_attack_target))) && TargetInfo.target_health != 0 && (Properties.Settings.Default.aim_friendly_target | stats.player_targetclass() != 2 | Properties.Settings.Default.mods == "Gathering")) // Dieeeee!
            {
                // Skills

                #region skill r
                if (Properties.Settings.Default.skill_1_enabled && skills_counter == 1 && TargetInfo.target_health != 0)
                {
                    // Reset the conditions stats
                    stats_bool = true;

                    if (
                            stats.actionbar_slot_r_cooldown() == 0
                        )
                    {
                        // Check conditions
                        conditions("1");

                        if (stats_bool)
                        {
                            // Log text
                            MainWindow.log_text = "Used Skill 'R'";

                            // Key pressing
                            Keysimulation.SimulateKeys.R();

                            // Cast time
                            System.Threading.Thread.Sleep(500);

                            skills_counter = 2;
                        }
                        else
                        {
                            skills_counter = 2;
                        }
                    }
                    else
                    {
                        skills_counter = 2;
                    }
                }
                else if (!Properties.Settings.Default.skill_1_enabled && skills_counter == 1)
                {
                    skills_counter = 2;
                }

                #endregion skill r

                #region skill 1
                if (Properties.Settings.Default.skill_2_enabled && skills_counter == 2 && TargetInfo.target_health != 0)
                {
                    // Reset the conditions stats
                    stats_bool = true;

                    if (
                            stats.actionbar_slot_1_cooldown() == 0
                        )
                    {
                        // Check conditions
                        conditions("2");

                        if (stats_bool)
                        {
                            // Log text
                            MainWindow.log_text = "Used Skill '1' ID: " + stats.actionbar_slots_id(1);

                            if (Properties.Settings.Default.skill_2_double_click == false)
                            {
                                if (Properties.Settings.Default.skill_1_repeat_cdt == 0)
                                {
                                    if (Properties.Settings.Default.skill_1_interrupt_enabled)
                                    {
                                        if (TargetInfo.target_iscasting != 0)
                                        {
                                            // Key pressing
                                            Keysimulation.SimulateKeys.KeySwitch(0);

                                            // Cast time
                                            System.Threading.Thread.Sleep(skills_ct_DB(stats.actionbar_slots_id(1)));

                                            System.Threading.Thread.Sleep(101);
                                        }

                                        // Go to the next skill
                                        skills_counter = 3;
                                    }
                                    else
                                    {
                                        // Key pressing
                                        Keysimulation.SimulateKeys.KeySwitch(0);

                                        // Cast time
                                        System.Threading.Thread.Sleep(skills_ct_DB(stats.actionbar_slots_id(1)));

                                        System.Threading.Thread.Sleep(101);

                                        // Go to the next skill
                                        skills_counter = 3;
                                    }
                                }
                                else
                                {
                                    for (int a = 0; a != Convert.ToInt32(Properties.Settings.Default.skill_1_repeat_cdt) + 2; a++)
                                    {
                                        // Key pressing
                                        Keysimulation.SimulateKeys.KeySwitch(0);

                                        // Cast time
                                        System.Threading.Thread.Sleep(skills_ct_DB(stats.actionbar_slots_id(1)));

                                        if ((a == (Convert.ToInt32(Properties.Settings.Default.skill_1_repeat_cdt) + 2) - 1) | TargetInfo.target_health == 0)
                                        {
                                            // Go to the next skill
                                            skills_counter = 3;

                                            break;
                                        }

                                        if (TargetInfo.target_id == 0 | TargetInfo.target_health == 0)
                                        {
                                            // Go to the next skill
                                            skills_counter = 3;

                                            break;
                                        }
                                    }
                                }
                            }

                            if (Properties.Settings.Default.skill_2_double_click == true)
                            {
                                // Key pressing
                                Keysimulation.SimulateKeys.KeySwitch(0);

                                // Cast time
                                System.Threading.Thread.Sleep(skills_ct_DB(stats.actionbar_slots_id(1)));

                                // Key pressing
                                Keysimulation.SimulateKeys.KeySwitch(0);
                                System.Threading.Thread.Sleep(500);

                                skills_counter = 3;
                            }
                        }
                        else
                        {
                            skills_counter = 3;
                        }
                    }
                    else
                    {
                        skills_counter = 3;
                    }
                }
                else if (!Properties.Settings.Default.skill_2_enabled && skills_counter == 2)
                {
                    skills_counter = 3;
                }
                #endregion skill 1

                #region skill 2
                if (Properties.Settings.Default.skill_3_enabled && skills_counter == 3 && TargetInfo.target_health != 0)
                {
                    // Reset the conditions stats
                    stats_bool = true;

                    if (
                            stats.actionbar_slot_2_cooldown() == 0
                        )
                    {
                        // Check conditions
                        conditions("3");

                        if (stats_bool)
                        {
                            // Log text
                            MainWindow.log_text = "Used Skill '2' ID: " + stats.actionbar_slots_id(2);

                            if (Properties.Settings.Default.skill_3_double_click == false)
                            {
                                if (Properties.Settings.Default.skill_2_repeat_cdt == 0)
                                {
                                    if (Properties.Settings.Default.skill_2_interrupt_enabled)
                                    {
                                        if (TargetInfo.target_iscasting != 0)
                                        {
                                            // Key pressing
                                            Keysimulation.SimulateKeys.KeySwitch(1);

                                            // Cast time
                                            System.Threading.Thread.Sleep(skills_ct_DB(stats.actionbar_slots_id(2)));

                                            System.Threading.Thread.Sleep(101);
                                        }

                                        // Go to the next skill
                                        skills_counter = 4;
                                    }
                                    else
                                    {
                                        // Key pressing
                                        Keysimulation.SimulateKeys.KeySwitch(1);

                                        // Cast time
                                        System.Threading.Thread.Sleep(skills_ct_DB(stats.actionbar_slots_id(2)));

                                        System.Threading.Thread.Sleep(101);

                                        // Go to the next skill
                                        skills_counter = 4;
                                    }
                                }
                                else
                                {
                                    for (int a = 0; a != Convert.ToInt32(Properties.Settings.Default.skill_2_repeat_cdt) + 2; a++)
                                    {
                                        // Key pressing
                                        Keysimulation.SimulateKeys.KeySwitch(1);

                                        // Cast time
                                        System.Threading.Thread.Sleep(skills_ct_DB(stats.actionbar_slots_id(2)));

                                        if ((a == (Convert.ToInt32(Properties.Settings.Default.skill_2_repeat_cdt) + 2) - 1) | TargetInfo.target_health == 0)
                                        {
                                            // Go to the next skill
                                            skills_counter = 4;

                                            break;
                                        }

                                        if (TargetInfo.target_id == 0 | TargetInfo.target_health == 0)
                                        {
                                            // Go to the next skill
                                            skills_counter = 4;

                                            break;
                                        }
                                    }
                                }
                            }

                            if (Properties.Settings.Default.skill_3_double_click == true)
                            {
                                // Key pressing
                                Keysimulation.SimulateKeys.KeySwitch(1);

                                // Cast time
                                System.Threading.Thread.Sleep(skills_ct_DB(stats.actionbar_slots_id(2)));

                                // Key pressing
                                Keysimulation.SimulateKeys.KeySwitch(1);
                                System.Threading.Thread.Sleep(500);

                                skills_counter = 4;
                            }
                        }
                        else
                        {
                            skills_counter = 4;
                        }
                    }
                    else
                    {
                        skills_counter = 4;
                    }
                }
                else if (!Properties.Settings.Default.skill_3_enabled && skills_counter == 3)
                {
                    skills_counter = 4;
                }
                #endregion skill 2

                #region skill 3
                if (Properties.Settings.Default.skill_4_enabled && skills_counter == 4 && TargetInfo.target_health != 0)
                {
                    // Reset the conditions stats
                    stats_bool = true;

                    if (
                            stats.actionbar_slot_3_cooldown() == 0
                        )
                    {
                        // Check conditions
                        conditions("4");

                        if (stats_bool)
                        {
                            // Log text
                            MainWindow.log_text = "Used Skill '3' ID: " + stats.actionbar_slots_id(3);

                            if (Properties.Settings.Default.skill_4_double_click == false)
                            {
                                if (Properties.Settings.Default.skill_3_repeat_cdt == 0)
                                {
                                    if (Properties.Settings.Default.skill_3_interrupt_enabled)
                                    {
                                        if (TargetInfo.target_iscasting != 0)
                                        {
                                            // Key pressing
                                            Keysimulation.SimulateKeys.KeySwitch(2);

                                            // Cast time
                                            System.Threading.Thread.Sleep(skills_ct_DB(stats.actionbar_slots_id(3)));

                                            System.Threading.Thread.Sleep(101);
                                        }

                                        // Go to the next skill
                                        skills_counter = 5;
                                    }
                                    else
                                    {
                                        // Key pressing
                                        Keysimulation.SimulateKeys.KeySwitch(2);

                                        // Cast time
                                        System.Threading.Thread.Sleep(skills_ct_DB(stats.actionbar_slots_id(3)));

                                        System.Threading.Thread.Sleep(101);

                                        // Go to the next skill
                                        skills_counter = 5;
                                    }
                                }
                                else
                                {
                                    for (int a = 0; a != Convert.ToInt32(Properties.Settings.Default.skill_3_repeat_cdt) + 2; a++)
                                    {
                                        // Key pressing
                                        Keysimulation.SimulateKeys.KeySwitch(2);

                                        // Cast time
                                        System.Threading.Thread.Sleep(skills_ct_DB(stats.actionbar_slots_id(3)));

                                        if ((a == (Convert.ToInt32(Properties.Settings.Default.skill_3_repeat_cdt) + 2) - 1) | TargetInfo.target_health == 0)
                                        {
                                            // Go to the next skill
                                            skills_counter = 5;

                                            break;
                                        }

                                        if (TargetInfo.target_id == 0 | TargetInfo.target_health == 0)
                                        {
                                            // Go to the next skill
                                            skills_counter = 5;

                                            break;
                                        }
                                    }
                                }
                            }

                            if (Properties.Settings.Default.skill_4_double_click == true)
                            {
                                // Key pressing
                                Keysimulation.SimulateKeys.KeySwitch(2);

                                // Cast time
                                System.Threading.Thread.Sleep(skills_ct_DB(stats.actionbar_slots_id(3)));

                                // Key pressing
                                Keysimulation.SimulateKeys.KeySwitch(2);
                                System.Threading.Thread.Sleep(500);

                                skills_counter = 5;
                            }
                        }
                        else
                        {
                            skills_counter = 5;
                        }
                    }
                    else
                    {
                        skills_counter = 5;
                    }
                }
                else if (!Properties.Settings.Default.skill_4_enabled && skills_counter == 4)
                {
                    skills_counter = 5;
                }
                #endregion skill 3

                #region skill 4
                if (Properties.Settings.Default.skill_5_enabled && skills_counter == 5 && TargetInfo.target_health != 0)
                {
                    // Reset the conditions stats
                    stats_bool = true;

                    if (
                            stats.actionbar_slot_4_cooldown() == 0
                        )
                    {
                        // Check conditions
                        conditions("5");

                        if (stats_bool)
                        {
                            // Log text
                            MainWindow.log_text = "Used Skill '4' ID: " + stats.actionbar_slots_id(4);

                            if (Properties.Settings.Default.skill_5_double_click == false)
                            {
                                if (Properties.Settings.Default.skill_4_repeat_cdt == 0)
                                {
                                    if (Properties.Settings.Default.skill_4_interrupt_enabled)
                                    {
                                        if (TargetInfo.target_iscasting != 0)
                                        {
                                            // Key pressing
                                            Keysimulation.SimulateKeys.KeySwitch(3);

                                            // Cast time
                                            System.Threading.Thread.Sleep(skills_ct_DB(stats.actionbar_slots_id(4)));

                                            System.Threading.Thread.Sleep(101);
                                        }

                                        // Go to the next skill
                                        skills_counter = 6;
                                    }
                                    else
                                    {
                                        // Key pressing
                                        Keysimulation.SimulateKeys.KeySwitch(3);

                                        // Cast time
                                        System.Threading.Thread.Sleep(skills_ct_DB(stats.actionbar_slots_id(4)));

                                        System.Threading.Thread.Sleep(101);

                                        // Go to the next skill
                                        skills_counter = 6;
                                    }
                                }
                                else
                                {
                                    for (int a = 0; a != Convert.ToInt32(Properties.Settings.Default.skill_4_repeat_cdt) + 2; a++)
                                    {
                                        // Key pressing
                                        Keysimulation.SimulateKeys.KeySwitch(3);

                                        // Cast time
                                        System.Threading.Thread.Sleep(skills_ct_DB(stats.actionbar_slots_id(4)));

                                        if ((a == (Convert.ToInt32(Properties.Settings.Default.skill_4_repeat_cdt) + 2) - 1) | TargetInfo.target_health == 0)
                                        {
                                            // Go to the next skill
                                            skills_counter = 6;

                                            break;
                                        }

                                        if (TargetInfo.target_id == 0 | TargetInfo.target_health == 0)
                                        {
                                            // Go to the next skill
                                            skills_counter = 6;

                                            break;
                                        }
                                    }
                                }
                            }

                            if (Properties.Settings.Default.skill_5_double_click == true)
                            {
                                // Key pressing
                                Keysimulation.SimulateKeys.KeySwitch(3);

                                // Cast time
                                System.Threading.Thread.Sleep(skills_ct_DB(stats.actionbar_slots_id(4)));

                                // Key pressing
                                Keysimulation.SimulateKeys.KeySwitch(3);

                                System.Threading.Thread.Sleep(500);

                                skills_counter = 6;
                            }
                        }
                        else
                        {
                            skills_counter = 6;
                        }
                    }
                    else
                    {
                        skills_counter = 6;
                    }
                }
                else if (!Properties.Settings.Default.skill_5_enabled && skills_counter == 5)
                {
                    skills_counter = 6;
                }
                #endregion skill 4

                #region skill 5
                if (Properties.Settings.Default.skill_6_enabled && skills_counter == 6 && TargetInfo.target_health != 0)
                {
                    // Reset the conditions stats
                    stats_bool = true;

                    if (
                            stats.actionbar_slot_5_cooldown() == 0
                        )
                    {
                        // Check conditions
                        conditions("6");

                        if (stats_bool)
                        {
                            // Log text
                            MainWindow.log_text = "Used Skill '5' ID: " + stats.actionbar_slots_id(5);

                            if (Properties.Settings.Default.skill_6_double_click == false)
                            {
                                if (Properties.Settings.Default.skill_5_repeat_cdt == 0)
                                {
                                    if (Properties.Settings.Default.skill_5_interrupt_enabled)
                                    {
                                        if (TargetInfo.target_iscasting != 0)
                                        {
                                            // Key pressing
                                            Keysimulation.SimulateKeys.KeySwitch(4);

                                            // Cast time
                                            System.Threading.Thread.Sleep(skills_ct_DB(stats.actionbar_slots_id(5)));

                                            System.Threading.Thread.Sleep(101);
                                        }

                                        // Go to the next skill
                                        skills_counter = 7;
                                    }
                                    else
                                    {
                                        // Key pressing
                                        Keysimulation.SimulateKeys.KeySwitch(4);

                                        // Cast time
                                        System.Threading.Thread.Sleep(skills_ct_DB(stats.actionbar_slots_id(5)));

                                        System.Threading.Thread.Sleep(101);

                                        // Go to the next skill
                                        skills_counter = 7;
                                    }
                                }
                                else
                                {
                                    for (int a = 0; a != Convert.ToInt32(Properties.Settings.Default.skill_5_repeat_cdt) + 2; a++)
                                    {
                                        // Key pressing
                                        Keysimulation.SimulateKeys.KeySwitch(4);

                                        // Cast time
                                        System.Threading.Thread.Sleep(skills_ct_DB(stats.actionbar_slots_id(5)));

                                        if ((a == (Convert.ToInt32(Properties.Settings.Default.skill_5_repeat_cdt) + 2) - 1) | TargetInfo.target_health == 0)
                                        {
                                            // Go to the next skill
                                            skills_counter = 7;

                                            break;
                                        }

                                        if (TargetInfo.target_id == 0 | TargetInfo.target_health == 0)
                                        {
                                            // Go to the next skill
                                            skills_counter = 7;

                                            break;
                                        }
                                    }
                                }
                            }

                            if (Properties.Settings.Default.skill_6_double_click == true)
                            {
                                // Key pressing
                                Keysimulation.SimulateKeys.KeySwitch(4);

                                // Cast time
                                System.Threading.Thread.Sleep(skills_ct_DB(stats.actionbar_slots_id(5)));

                                // Key pressing
                                Keysimulation.SimulateKeys.KeySwitch(4);

                                System.Threading.Thread.Sleep(500);

                                skills_counter = 7;
                            }
                        }
                        else
                        {
                            skills_counter = 7;
                        }
                    }
                    else
                    {
                        skills_counter = 7;
                    }
                }
                else if (!Properties.Settings.Default.skill_6_enabled && skills_counter == 6)
                {
                    skills_counter = 7;
                }
                #endregion skill 5

                #region skill 6
                if (Properties.Settings.Default.skill_7_enabled && skills_counter == 7 && TargetInfo.target_health != 0)
                {
                    // Reset the conditions stats
                    stats_bool = true;

                    if (
                           stats.actionbar_slot_6_cooldown() == 0
                       )
                    {
                        // Check conditions
                        conditions("7");

                        if (stats_bool)
                        {
                            // Log text
                            MainWindow.log_text = "Used Skill '6' ID: " + stats.actionbar_slots_id(6);

                            if (Properties.Settings.Default.skill_7_double_click == false)
                            {
                                if (Properties.Settings.Default.skill_6_repeat_cdt == 0)
                                {
                                    if (Properties.Settings.Default.skill_6_interrupt_enabled)
                                    {
                                        if (TargetInfo.target_iscasting != 0)
                                        {
                                            // Key pressing
                                            Keysimulation.SimulateKeys.KeySwitch(5);

                                            // Cast time
                                            System.Threading.Thread.Sleep(skills_ct_DB(stats.actionbar_slots_id(6)));

                                            System.Threading.Thread.Sleep(101);
                                        }

                                        // Go to the next skill
                                        skills_counter = 8;
                                    }
                                    else
                                    {
                                        // Key pressing
                                        Keysimulation.SimulateKeys.KeySwitch(5);

                                        // Cast time
                                        System.Threading.Thread.Sleep(skills_ct_DB(stats.actionbar_slots_id(6)));

                                        System.Threading.Thread.Sleep(101);

                                        // Go to the next skill
                                        skills_counter = 8;
                                    }
                                }
                                else
                                {
                                    for (int a = 0; a != Convert.ToInt32(Properties.Settings.Default.skill_6_repeat_cdt) + 2; a++)
                                    {
                                        // Key pressing
                                        Keysimulation.SimulateKeys.KeySwitch(5);

                                        // Cast time
                                        System.Threading.Thread.Sleep(skills_ct_DB(stats.actionbar_slots_id(6)));

                                        if ((a == (Convert.ToInt32(Properties.Settings.Default.skill_6_repeat_cdt) + 2) - 1) | TargetInfo.target_health == 0)
                                        {
                                            // Go to the next skill
                                            skills_counter = 8;

                                            break;
                                        }

                                        if (TargetInfo.target_id == 0 | TargetInfo.target_health == 0)
                                        {
                                            // Go to the next skill
                                            skills_counter = 8;

                                            break;
                                        }
                                    }
                                }
                            }

                            if (Properties.Settings.Default.skill_7_double_click == true)
                            {
                                // Key pressing
                                Keysimulation.SimulateKeys.KeySwitch(5);

                                // Cast time
                                System.Threading.Thread.Sleep(skills_ct_DB(stats.actionbar_slots_id(6)));

                                // Key pressing
                                Keysimulation.SimulateKeys.KeySwitch(5);
                                System.Threading.Thread.Sleep(500);

                                // Go to the next skill
                                skills_counter = 8;
                            }
                        }
                        else
                        {
                            // Go to the next skill
                            skills_counter = 8;
                        }
                    }
                    else
                    {
                        // Go to the next skill
                        skills_counter = 8;
                    }
                }
                else if (!Properties.Settings.Default.skill_7_enabled && skills_counter == 7)
                {
                    // Go to the next skill
                    skills_counter = 8;
                }
                #endregion skill 6

                #region skill 7
                if (Properties.Settings.Default.skill_8_enabled && skills_counter == 8 && TargetInfo.target_health != 0)
                {
                    // Reset the conditions stats
                    stats_bool = true;

                    if (
                            stats.actionbar_slot_7_cooldown() == 0
                        )
                    {
                        // Check conditions
                        conditions("8");

                        if (stats_bool)
                        {
                            // Log text
                            MainWindow.log_text = "Used Skill '7' ID: " + stats.actionbar_slots_id(7);

                            if (Properties.Settings.Default.skill_8_double_click == false)
                            {
                                if (Properties.Settings.Default.skill_7_repeat_cdt == 0)
                                {
                                    if (Properties.Settings.Default.skill_7_interrupt_enabled)
                                    {
                                        if (TargetInfo.target_iscasting != 0)
                                        {
                                            // Key pressing
                                            Keysimulation.SimulateKeys.KeySwitch(6);

                                            // Cast time
                                            System.Threading.Thread.Sleep(skills_ct_DB(stats.actionbar_slots_id(7)));

                                            System.Threading.Thread.Sleep(101);
                                        }

                                        // Go to the next skill
                                        skills_counter = 9;
                                    }
                                    else
                                    {
                                        // Key pressing
                                        Keysimulation.SimulateKeys.KeySwitch(6);

                                        // Cast time
                                        System.Threading.Thread.Sleep(skills_ct_DB(stats.actionbar_slots_id(7)));

                                        System.Threading.Thread.Sleep(101);

                                        // Go to the next skill
                                        skills_counter = 9;
                                    }
                                }
                                else
                                {
                                    for (int a = 0; a != Convert.ToInt32(Properties.Settings.Default.skill_7_repeat_cdt) + 2; a++)
                                    {
                                        // Key pressing
                                        Keysimulation.SimulateKeys.KeySwitch(6);

                                        // Cast time
                                        System.Threading.Thread.Sleep(skills_ct_DB(stats.actionbar_slots_id(7)));

                                        if ((a == (Convert.ToInt32(Properties.Settings.Default.skill_7_repeat_cdt) + 2) - 1) | TargetInfo.target_health == 0)
                                        {
                                            skills_counter = 9;

                                            break;
                                        }

                                        if (TargetInfo.target_id == 0 | TargetInfo.target_health == 0)
                                        {
                                            // Go to the next skill
                                            skills_counter = 9;

                                            break;
                                        }
                                    }
                                }
                            }

                            if (Properties.Settings.Default.skill_8_double_click == true)
                            {
                                // Key pressing
                                Keysimulation.SimulateKeys.KeySwitch(6);

                                // Cast time
                                System.Threading.Thread.Sleep(skills_ct_DB(stats.actionbar_slots_id(7)));

                                // Key pressing
                                Keysimulation.SimulateKeys.KeySwitch(6);
                                System.Threading.Thread.Sleep(500);

                                // Go to the next skill
                                skills_counter = 9;
                            }
                        }
                        else
                        {
                            // Go to the next skill
                            skills_counter = 9;
                        }
                    }
                    else
                    {
                        // Go to the next skill
                        skills_counter = 9;
                    }
                }
                else if (!Properties.Settings.Default.skill_8_enabled && skills_counter == 8)
                {
                    skills_counter = 9;
                }
                #endregion skill 7

                #region skill 8
                if (Properties.Settings.Default.skill_9_enabled && skills_counter == 9 && TargetInfo.target_health != 0)
                {
                    // Reset the conditions stats
                    stats_bool = true;

                    if (
                            stats.actionbar_slot_8_cooldown() == 0
                        )
                    {
                        // Check conditions
                        conditions("9");

                        if (stats_bool)
                        {
                            // Log text
                            MainWindow.log_text = "Used Skill '8' ID: " + stats.actionbar_slots_id(8);

                            if (Properties.Settings.Default.skill_9_double_click == false)
                            {
                                if (Properties.Settings.Default.skill_8_repeat_cdt == 0)
                                {
                                    if (Properties.Settings.Default.skill_8_interrupt_enabled)
                                    {
                                        if (TargetInfo.target_iscasting != 0)
                                        {
                                            // Key pressing
                                            Keysimulation.SimulateKeys.KeySwitch(7);

                                            // Cast time
                                            System.Threading.Thread.Sleep(Properties.Settings.Default.skill_9_charge + 200);

                                            System.Threading.Thread.Sleep(101);

                                            if (stats.player_classes() == 1)
                                            {
                                                System.Threading.Thread.Sleep(600);
                                            }
                                        }

                                        // Go to the next skill
                                        skills_counter = 10;
                                    }
                                    else
                                    {
                                        // Key pressing
                                        Keysimulation.SimulateKeys.KeySwitch(7);

                                        // Cast time
                                        System.Threading.Thread.Sleep(Properties.Settings.Default.skill_9_charge + 200);

                                        System.Threading.Thread.Sleep(101);

                                        if (stats.player_classes() == 1)
                                        {
                                            System.Threading.Thread.Sleep(600);
                                        }

                                        // Go to the next skill
                                        skills_counter = 10;
                                    }
                                }
                                else
                                {
                                    for (int a = 0; a != Convert.ToInt32(Properties.Settings.Default.skill_8_repeat_cdt) + 2; a++)
                                    {
                                        // Key pressing
                                        Keysimulation.SimulateKeys.KeySwitch(7);

                                        // Cast time
                                        System.Threading.Thread.Sleep(Properties.Settings.Default.skill_9_charge + 200);

                                        if ((a == (Convert.ToInt32(Properties.Settings.Default.skill_8_repeat_cdt) + 2) - 1) | TargetInfo.target_health == 0)
                                        {
                                            skills_counter = 10;

                                            if (stats.player_classes() == 1)
                                            {
                                                System.Threading.Thread.Sleep(600);
                                            }

                                            break;
                                        }

                                        if (TargetInfo.target_id == 0 | TargetInfo.target_health == 0)
                                        {
                                            // Go to the next skill
                                            skills_counter = 10;

                                            break;
                                        }
                                    }
                                }
                            }

                            if (Properties.Settings.Default.skill_9_double_click == true)
                            {
                                // Key pressing
                                Keysimulation.SimulateKeys.KeySwitch(7);

                                // Cast time
                                System.Threading.Thread.Sleep(Properties.Settings.Default.skill_9_charge + 200);

                                // Key pressing
                                Keysimulation.SimulateKeys.KeySwitch(7);
                                Keysimulation.SimulateKeys.KeySwitch(7);

                                System.Threading.Thread.Sleep(500);

                                // Go to the next skill
                                skills_counter = 10;
                            }
                        }
                        else
                        {
                            // Go to the next skill
                            skills_counter = 10;
                        }
                    }
                    else
                    {
                        // Go to the next skill
                        skills_counter = 10;
                    }
                }
                else if (!Properties.Settings.Default.skill_9_enabled && skills_counter == 9)
                {
                    // Go to the next skill
                    skills_counter = 10;
                }
                #endregion skill 8

                if (skills_counter > 9)
                {
                    skills_counter = 1;
                }
            }

            System.Threading.Thread.Sleep(120);
        }

        // Conditions check
        public void conditions(string c)
        {
            switch (c)
            {
                case "1":
                    skill_r(skill_r_array);
                    break;
                case "2":
                    skill_1(skill_1_array);
                    break;
                case "3":
                    skill_2(skill_2_array);
                    break;
                case "4":
                    skill_3(skill_3_array);
                    break;
                case "5":
                    skill_4(skill_4_array);
                    break;
                case "6":
                    skill_5(skill_5_array);
                    break;
                case "7":
                    skill_6(skill_6_array);
                    break;
                case "8":
                    skill_7(skill_7_array);
                    break;
                case "9":
                    skill_8(skill_8_array);
                    break;
            }
        }

        // Stats bool
        private bool stats_bool;

        // Conditions
        public void skill_r(string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                switch (array[i])
                {
                    case "1":
                        if (stats.player_health_inp() > Convert.ToUInt64(Properties.Settings.Default.skill_r_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "2":
                        if (stats.player_health_inp() < Convert.ToUInt64(Properties.Settings.Default.skill_r_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "3":
                        if (stats.player_shield_inp() > Convert.ToUInt64(Properties.Settings.Default.skill_r_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "4":
                        if (stats.player_shield_inp() < Convert.ToUInt64(Properties.Settings.Default.skill_r_shield_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "5":
                        if (TargetInfo.target_health_inp > Convert.ToUInt64(Properties.Settings.Default.skill_r_target_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "6":
                        if (TargetInfo.target_health_inp < Convert.ToUInt64(Properties.Settings.Default.skill_r_target_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "7":
                        if (TargetInfo.target_shield_inp > Convert.ToUInt64(Properties.Settings.Default.skill_r_target_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "8":
                        if (TargetInfo.target_shield_inp < Convert.ToUInt64(Properties.Settings.Default.skill_r_target_shield_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "9":
                        if (stats.player_classes() == 1 | stats.player_classes() == 2 | stats.player_classes() == 3 | stats.player_classes() == 4)
                        {
                            if (stats.player_otherpowers() > Convert.ToUInt64(Properties.Settings.Default.skill_r_power))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 5)
                        {
                            if (stats.player_suitpower() > Convert.ToUInt64(Properties.Settings.Default.skill_r_power))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 6)
                        {
                            if (stats.player_spellpower() > Convert.ToUInt64(Properties.Settings.Default.skill_r_power))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        break;
                    case "10":
                        if (stats.player_classes() == 1 | stats.player_classes() == 2 | stats.player_classes() == 3 | stats.player_classes() == 4)
                        {
                            if (stats.player_otherpowers() < Convert.ToUInt64(Properties.Settings.Default.skill_r_power_under))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 5)
                        {
                            if (stats.player_suitpower() < Convert.ToUInt64(Properties.Settings.Default.skill_r_power_under))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 6)
                        {
                            if (stats.player_spellpower() < Convert.ToUInt64(Properties.Settings.Default.skill_r_power_under))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        break;
                    case "0":
                        break;
                }
            }
        }
        public void skill_1(string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                switch (array[i])
                {
                    case "1":
                        if (stats.player_health_inp() > Convert.ToUInt64(Properties.Settings.Default.skill_1_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "2":
                        if (stats.player_health_inp() < Convert.ToUInt64(Properties.Settings.Default.skill_1_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "3":
                        if (stats.player_shield_inp() > Convert.ToUInt64(Properties.Settings.Default.skill_1_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "4":
                        if (stats.player_shield_inp() < Convert.ToUInt64(Properties.Settings.Default.skill_1_shield_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "5":
                        if (TargetInfo.target_health_inp > Convert.ToUInt64(Properties.Settings.Default.skill_1_target_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "6":
                        if (TargetInfo.target_health_inp < Convert.ToUInt64(Properties.Settings.Default.skill_1_target_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "7":
                        if (TargetInfo.target_shield_inp > Convert.ToUInt64(Properties.Settings.Default.skill_1_target_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "8":
                        if (TargetInfo.target_shield_inp < Convert.ToUInt64(Properties.Settings.Default.skill_1_target_shield_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "9":
                        if (stats.player_classes() == 1 | stats.player_classes() == 2 | stats.player_classes() == 3 | stats.player_classes() == 4)
                        {
                            if ((stats.player_otherpowers() > Convert.ToUInt64(Properties.Settings.Default.skill_1_power)) | (stats.player_otherpowers() == 1 && stats.player_classes() == 1))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 5)
                        {
                            if (stats.player_suitpower() > Convert.ToUInt64(Properties.Settings.Default.skill_1_power))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 7)
                        {
                            if (stats.player_spellpower() > Convert.ToUInt64(Properties.Settings.Default.skill_1_power))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        break;
                    case "10":
                        if (stats.player_classes() == 1 | stats.player_classes() == 2 | stats.player_classes() == 3 | stats.player_classes() == 4)
                        {
                            if (stats.player_otherpowers() < Convert.ToUInt64(Properties.Settings.Default.skill_1_power_under))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 5)
                        {
                            if (stats.player_suitpower() < Convert.ToUInt64(Properties.Settings.Default.skill_1_power_under))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 6)
                        {
                            if (stats.player_spellpower() < Convert.ToUInt64(Properties.Settings.Default.skill_1_power_under))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        break;
                    case "0":
                        break;
                }
            }
        }
        public void skill_2(string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                switch (array[i])
                {
                    case "0":
                        break;
                    case "1":
                        if (stats.player_health_inp() > Convert.ToUInt64(Properties.Settings.Default.skill_2_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "2":
                        if (stats.player_health_inp() < Convert.ToUInt64(Properties.Settings.Default.skill_2_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "3":
                        if (stats.player_shield_inp() > Convert.ToUInt64(Properties.Settings.Default.skill_2_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "4":
                        if (stats.player_shield_inp() < Convert.ToUInt64(Properties.Settings.Default.skill_2_shield_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "5":
                        if (TargetInfo.target_health_inp > Convert.ToUInt64(Properties.Settings.Default.skill_2_target_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "6":
                        if (TargetInfo.target_health_inp < Convert.ToUInt64(Properties.Settings.Default.skill_2_target_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "7":
                        if (TargetInfo.target_shield_inp > Convert.ToUInt64(Properties.Settings.Default.skill_2_target_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "8":
                        if (TargetInfo.target_shield_inp < Convert.ToUInt64(Properties.Settings.Default.skill_2_target_shield_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "9":
                        if (stats.player_classes() == 1 | stats.player_classes() == 2 | stats.player_classes() == 3 | stats.player_classes() == 4)
                        {
                            if ((stats.player_otherpowers() > Convert.ToUInt64(Properties.Settings.Default.skill_2_power)) | (stats.player_otherpowers() == 1 && stats.player_classes() == 1))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 5)
                        {
                            if (stats.player_suitpower() > Convert.ToUInt64(Properties.Settings.Default.skill_2_power))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 6)
                        {
                            if (stats.player_spellpower() > Convert.ToUInt64(Properties.Settings.Default.skill_2_power))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        break;
                    case "10":
                        if (stats.player_classes() == 1 | stats.player_classes() == 2 | stats.player_classes() == 3 | stats.player_classes() == 4)
                        {
                            if (stats.player_otherpowers() < Convert.ToUInt64(Properties.Settings.Default.skill_2_power_under))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 5)
                        {
                            if (stats.player_suitpower() < Convert.ToUInt64(Properties.Settings.Default.skill_2_power_under))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 6)
                        {
                            if (stats.player_spellpower() < Convert.ToUInt64(Properties.Settings.Default.skill_2_power_under))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        break;
                }
            }
        }
        public void skill_3(string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                switch (array[i])
                {
                    case "0":
                        break;
                    case "1":
                        if (stats.player_health_inp() > Convert.ToUInt64(Properties.Settings.Default.skill_3_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "2":
                        if (stats.player_health_inp() < Convert.ToUInt64(Properties.Settings.Default.skill_3_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "3":
                        if (stats.player_shield_inp() > Convert.ToUInt64(Properties.Settings.Default.skill_3_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "4":
                        if (stats.player_shield_inp() < Convert.ToUInt64(Properties.Settings.Default.skill_3_shield_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "5":
                        if (TargetInfo.target_health_inp > Convert.ToUInt64(Properties.Settings.Default.skill_3_target_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "6":
                        if (TargetInfo.target_health_inp < Convert.ToUInt64(Properties.Settings.Default.skill_3_target_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "7":
                        if (TargetInfo.target_shield_inp > Convert.ToUInt64(Properties.Settings.Default.skill_3_target_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "8":
                        if (TargetInfo.target_shield_inp < Convert.ToUInt64(Properties.Settings.Default.skill_3_target_shield_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "9":
                        if (stats.player_classes() == 1 | stats.player_classes() == 2 | stats.player_classes() == 3 | stats.player_classes() == 4)
                        {
                            if ((stats.player_otherpowers() > Convert.ToUInt64(Properties.Settings.Default.skill_3_power)) | (stats.player_otherpowers() == 1 && stats.player_classes() == 1))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 5)
                        {
                            if (stats.player_suitpower() > Convert.ToUInt64(Properties.Settings.Default.skill_3_power))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 6)
                        {
                            if (stats.player_spellpower() > Convert.ToUInt64(Properties.Settings.Default.skill_3_power))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        break;
                    case "10":
                        if (stats.player_classes() == 1 | stats.player_classes() == 2 | stats.player_classes() == 3 | stats.player_classes() == 4)
                        {
                            if (stats.player_otherpowers() < Convert.ToUInt64(Properties.Settings.Default.skill_3_power_under))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 5)
                        {
                            if (stats.player_suitpower() < Convert.ToUInt64(Properties.Settings.Default.skill_3_power_under))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 6)
                        {
                            if (stats.player_spellpower() < Convert.ToUInt64(Properties.Settings.Default.skill_3_power_under))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        break;
                }
            }
        }
        public void skill_4(string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                switch (array[i])
                {
                    case "0":
                        break;
                    case "1":
                        if (stats.player_health_inp() > Convert.ToUInt64(Properties.Settings.Default.skill_4_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "2":
                        if (stats.player_health_inp() < Convert.ToUInt64(Properties.Settings.Default.skill_4_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "3":
                        if (stats.player_shield_inp() > Convert.ToUInt64(Properties.Settings.Default.skill_4_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "4":
                        if (stats.player_shield_inp() < Convert.ToUInt64(Properties.Settings.Default.skill_4_shield_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "5":
                        if (TargetInfo.target_health_inp > Convert.ToUInt64(Properties.Settings.Default.skill_4_target_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "6":
                        if (TargetInfo.target_health_inp < Convert.ToUInt64(Properties.Settings.Default.skill_4_target_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "7":
                        if (TargetInfo.target_shield_inp > Convert.ToUInt64(Properties.Settings.Default.skill_4_target_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "8":
                        if (TargetInfo.target_shield_inp < Convert.ToUInt64(Properties.Settings.Default.skill_4_target_shield_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "9":
                        if (stats.player_classes() == 1 | stats.player_classes() == 2 | stats.player_classes() == 3 | stats.player_classes() == 4)
                        {
                            if ((stats.player_otherpowers() > Convert.ToUInt64(Properties.Settings.Default.skill_4_power)) | (stats.player_otherpowers() == 1 && stats.player_classes() == 1))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 5)
                        {
                            if (stats.player_suitpower() > Convert.ToUInt64(Properties.Settings.Default.skill_4_power))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 6)
                        {
                            if (stats.player_spellpower() > Convert.ToUInt64(Properties.Settings.Default.skill_4_power))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        break;
                    case "10":
                        if (stats.player_classes() == 1 | stats.player_classes() == 2 | stats.player_classes() == 3 | stats.player_classes() == 4)
                        {
                            if (stats.player_otherpowers() < Convert.ToUInt64(Properties.Settings.Default.skill_4_power_under))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 5)
                        {
                            if (stats.player_suitpower() < Convert.ToUInt64(Properties.Settings.Default.skill_4_power_under))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 6)
                        {
                            if (stats.player_spellpower() < Convert.ToUInt64(Properties.Settings.Default.skill_4_power_under))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        break;
                }
            }
        }
        public void skill_5(string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                switch (array[i])
                {
                    case "0":
                        break;
                    case "1":
                        if (stats.player_health_inp() > Convert.ToUInt64(Properties.Settings.Default.skill_5_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "2":
                        if (stats.player_health_inp() < Convert.ToUInt64(Properties.Settings.Default.skill_5_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "3":
                        if (stats.player_shield_inp() > Convert.ToUInt64(Properties.Settings.Default.skill_5_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "4":
                        if (stats.player_shield_inp() < Convert.ToUInt64(Properties.Settings.Default.skill_5_shield_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "5":
                        if (TargetInfo.target_health_inp > Convert.ToUInt64(Properties.Settings.Default.skill_5_target_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "6":
                        if (TargetInfo.target_health_inp < Convert.ToUInt64(Properties.Settings.Default.skill_5_target_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "7":
                        if (TargetInfo.target_shield_inp > Convert.ToUInt64(Properties.Settings.Default.skill_5_target_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "8":
                        if (TargetInfo.target_shield_inp < Convert.ToUInt64(Properties.Settings.Default.skill_5_target_shield_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "9":
                        if (stats.player_classes() == 1 | stats.player_classes() == 2 | stats.player_classes() == 3 | stats.player_classes() == 4)
                        {
                            if ((stats.player_otherpowers() > Convert.ToUInt64(Properties.Settings.Default.skill_5_power)) | (stats.player_otherpowers() == 1 && stats.player_classes() == 1))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 5)
                        {
                            if (stats.player_suitpower() > Convert.ToUInt64(Properties.Settings.Default.skill_5_power))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 6)
                        {
                            if (stats.player_spellpower() > Convert.ToUInt64(Properties.Settings.Default.skill_5_power))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        break;
                    case "10":
                        if (stats.player_classes() == 1 | stats.player_classes() == 2 | stats.player_classes() == 3 | stats.player_classes() == 4)
                        {
                            if (stats.player_otherpowers() < Convert.ToUInt64(Properties.Settings.Default.skill_5_power_under))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 5)
                        {
                            if (stats.player_suitpower() < Convert.ToUInt64(Properties.Settings.Default.skill_5_power_under))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 6)
                        {
                            if (stats.player_spellpower() < Convert.ToUInt64(Properties.Settings.Default.skill_5_power_under))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        break;
                }
            }
        }
        public void skill_6(string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                switch (array[i])
                {
                    case "0":
                        break;
                    case "1":
                        if (stats.player_health_inp() > Convert.ToUInt64(Properties.Settings.Default.skill_6_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "2":
                        if (stats.player_health_inp() < Convert.ToUInt64(Properties.Settings.Default.skill_6_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "3":
                        if (stats.player_shield_inp() > Convert.ToUInt64(Properties.Settings.Default.skill_6_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "4":
                        if (stats.player_shield_inp() < Convert.ToUInt64(Properties.Settings.Default.skill_6_shield_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "5":
                        if (TargetInfo.target_health_inp > Convert.ToUInt64(Properties.Settings.Default.skill_6_target_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "6":
                        if (TargetInfo.target_health_inp < Convert.ToUInt64(Properties.Settings.Default.skill_6_target_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "7":
                        if (TargetInfo.target_shield_inp > Convert.ToUInt64(Properties.Settings.Default.skill_6_target_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "8":
                        if (TargetInfo.target_shield_inp < Convert.ToUInt64(Properties.Settings.Default.skill_6_target_shield_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "9":
                        if (stats.player_classes() == 1 | stats.player_classes() == 2 | stats.player_classes() == 3 | stats.player_classes() == 4)
                        {
                            if ((stats.player_otherpowers() > Convert.ToUInt64(Properties.Settings.Default.skill_6_power)) | (stats.player_otherpowers() == 1 && stats.player_classes() == 1))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 5)
                        {
                            if (stats.player_suitpower() > Convert.ToUInt64(Properties.Settings.Default.skill_6_power))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 6)
                        {
                            if (stats.player_spellpower() > Convert.ToUInt64(Properties.Settings.Default.skill_6_power))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        break;
                    case "10":
                        if (stats.player_classes() == 1 | stats.player_classes() == 2 | stats.player_classes() == 3 | stats.player_classes() == 4)
                        {
                            if (stats.player_otherpowers() < Convert.ToUInt64(Properties.Settings.Default.skill_6_power_under))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 5)
                        {
                            if (stats.player_suitpower() < Convert.ToUInt64(Properties.Settings.Default.skill_6_power_under))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 6)
                        {
                            if (stats.player_spellpower() < Convert.ToUInt64(Properties.Settings.Default.skill_6_power_under))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        break;
                }
            }
        }
        public void skill_7(string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                switch (array[i])
                {
                    case "0":
                        break;
                    case "1":
                        if (stats.player_health_inp() > Convert.ToUInt64(Properties.Settings.Default.skill_7_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "2":
                        if (stats.player_health_inp() < Convert.ToUInt64(Properties.Settings.Default.skill_7_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "3":
                        if (stats.player_shield_inp() > Convert.ToUInt64(Properties.Settings.Default.skill_7_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "4":
                        if (stats.player_shield_inp() < Convert.ToUInt64(Properties.Settings.Default.skill_7_shield_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "5":
                        if (TargetInfo.target_health_inp > Convert.ToUInt64(Properties.Settings.Default.skill_7_target_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "6":
                        if (TargetInfo.target_health_inp < Convert.ToUInt64(Properties.Settings.Default.skill_7_target_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "7":
                        if (TargetInfo.target_shield_inp > Convert.ToUInt64(Properties.Settings.Default.skill_7_target_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "8":
                        if (TargetInfo.target_shield_inp < Convert.ToUInt64(Properties.Settings.Default.skill_7_target_shield_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "9":
                        if (stats.player_classes() == 1 | stats.player_classes() == 2 | stats.player_classes() == 3 | stats.player_classes() == 4)
                        {
                            if ((stats.player_otherpowers() > Convert.ToUInt64(Properties.Settings.Default.skill_7_power)) | (stats.player_otherpowers() == 1 && stats.player_classes() == 1))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 5)
                        {
                            if (stats.player_suitpower() > Convert.ToUInt64(Properties.Settings.Default.skill_7_power))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 6)
                        {
                            if (stats.player_spellpower() > Convert.ToUInt64(Properties.Settings.Default.skill_7_power))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        break;
                    case "10":
                        if (stats.player_classes() == 1 | stats.player_classes() == 2 | stats.player_classes() == 3 | stats.player_classes() == 4)
                        {
                            if (stats.player_otherpowers() < Convert.ToUInt64(Properties.Settings.Default.skill_7_power_under))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 5)
                        {
                            if (stats.player_suitpower() < Convert.ToUInt64(Properties.Settings.Default.skill_7_power_under))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 6)
                        {
                            if (stats.player_spellpower() < Convert.ToUInt64(Properties.Settings.Default.skill_7_power_under))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        break;
                }
            }
        }
        public void skill_8(string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                switch (array[i])
                {
                    case "0":
                        break;
                    case "1":
                        if (stats.player_health_inp() > Convert.ToUInt64(Properties.Settings.Default.skill_8_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "2":
                        if (stats.player_health_inp() < Convert.ToUInt64(Properties.Settings.Default.skill_8_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "3":
                        if (stats.player_shield_inp() > Convert.ToUInt64(Properties.Settings.Default.skill_8_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "4":
                        if (stats.player_shield_inp() < Convert.ToUInt64(Properties.Settings.Default.skill_8_shield_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "5":
                        if (TargetInfo.target_health_inp > Convert.ToUInt64(Properties.Settings.Default.skill_8_target_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "6":
                        if (TargetInfo.target_health_inp < Convert.ToUInt64(Properties.Settings.Default.skill_8_target_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "7":
                        if (TargetInfo.target_shield_inp > Convert.ToUInt64(Properties.Settings.Default.skill_8_target_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "8":
                        if (TargetInfo.target_shield_inp < Convert.ToUInt64(Properties.Settings.Default.skill_8_target_shield_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "9":
                        if (stats.player_classes() == 1 | stats.player_classes() == 2 | stats.player_classes() == 3 | stats.player_classes() == 4)
                        {
                            if ((stats.player_otherpowers() > Convert.ToUInt64(Properties.Settings.Default.skill_8_power)) | (stats.player_otherpowers() == 1 && stats.player_classes() == 1))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 5)
                        {
                            if (stats.player_suitpower() > Convert.ToUInt64(Properties.Settings.Default.skill_8_power))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 6)
                        {
                            if (stats.player_spellpower() > Convert.ToUInt64(Properties.Settings.Default.skill_8_power))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        break;
                    case "10":
                        if (stats.player_classes() == 1 | stats.player_classes() == 2 | stats.player_classes() == 3 | stats.player_classes() == 4)
                        {
                            if (stats.player_otherpowers() < Convert.ToUInt64(Properties.Settings.Default.skill_8_power_under))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 5)
                        {
                            if (stats.player_suitpower() < Convert.ToUInt64(Properties.Settings.Default.skill_8_power_under))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        else if (stats.player_classes() == 6)
                        {
                            if (stats.player_spellpower() < Convert.ToUInt64(Properties.Settings.Default.skill_8_power_under))
                            {
                                //stats_bool = true;
                            }
                            else
                            {
                                stats_bool = false;
                            }
                        }
                        break;
                }
            }
        }

        // Skill DB
        private int skills_ct_DB(UInt64 skill_id)
        {
            int cast_time = 0;

            switch (skill_id)
            {

                default:
                    return 646;

                // Warrior
                #region Assault
                case 18309:
                    cast_time = randomnr.create(1,3) +  randomnr.create(1,3) + 648;
                    return cast_time;

                case 37968:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 19778:
                    cast_time = randomnr.create(1,3) +  3000;
                    return cast_time;

                case 30896:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 37482:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 21806:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 18580:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                #endregion Assault
                #region Support
                case 39339:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 35468:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 23169:
                    cast_time = randomnr.create(1,3) +  3000;
                    return cast_time;

                case 18360:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 18572:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 23328:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 31155:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 18578:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 38445:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 37245:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                #endregion Support
                #region Utility
                case 38017:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 18569:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 18547:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 18363:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 18585:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 35146:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 35501:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 35526:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 32720:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                #endregion Utility
                #region Path
                case 46781:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 46794:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 46773:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                #endregion Path

                // Esper
                #region Assault
                case 19102:
                    cast_time = randomnr.create(1,3) +  1250;
                    return cast_time;

                case 19019:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 28756:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 19163:
                    cast_time = randomnr.create(1,3) +  3000;
                    return cast_time;

                case 19024:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 21613:
                    cast_time = randomnr.create(1,3) +  750;
                    return cast_time;

                case 21779:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 27734:
                    cast_time = randomnr.create(1,3) +  2646;
                    return cast_time;

                case 19139:
                    cast_time = randomnr.create(1,3) +  1646;
                    return cast_time;

                case 19136:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                #endregion Assault
                #region Support
                case 19030:
                    cast_time = randomnr.create(1,3) +  1646;
                    return cast_time;

                case 19031:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 19218:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 19033:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 19207:
                    cast_time = randomnr.create(1,3) +  2646;
                    return cast_time;

                case 19341:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 19362:
                    cast_time = randomnr.create(1,3) +  3000;
                    return cast_time;

                case 22282:
                    cast_time = randomnr.create(1,3) +  1250;
                    return cast_time;

                case 19379:
                    cast_time = randomnr.create(1,3) +  2000;
                    return cast_time;

                case 19258:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                #endregion Support
                #region Utility
                case 19022:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 21743:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 19025:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 19190:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 19271:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 19028:
                    cast_time = randomnr.create(1,3) +  6460;
                    return cast_time;

                case 19273:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 19355:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 19029:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 21812:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                #endregion Utility
                #region Path
                case 46819:
                    cast_time = randomnr.create(1,3) +  2000;
                    return cast_time;

                case 46803:
                    cast_time = randomnr.create(1,3) +  20000;
                    return cast_time;

                case 38229:
                    cast_time = randomnr.create(1,3) +  3000;
                    return cast_time;

                #endregion Path

                // Spellsinger
                #region Assault
                case 27638:
                    cast_time = randomnr.create(1,3) +  1000;
                    return cast_time;

                case 20684:
                    cast_time = randomnr.create(1,3) +  2400;
                    return cast_time;

                case 20734:
                    cast_time = randomnr.create(1,3) +  2646;
                    return cast_time;

                case 27774:
                    cast_time = randomnr.create(1,3) +  1300;
                    return cast_time;

                case 21056:
                    cast_time = randomnr.create(1,3) +  1000;
                    return cast_time;

                case 30666:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 23274:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 21650:
                    cast_time = randomnr.create(1,3) +  2000;
                    return cast_time;

                case 27736:
                    cast_time = randomnr.create(1,3) +  1800;
                    return cast_time;

                #endregion Assault
                #region Support
                case 23012:
                    cast_time = randomnr.create(1,3) +  2000;
                    return cast_time;

                case 23441:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 23479:
                    cast_time = randomnr.create(1,3) +  2600;
                    return cast_time;

                case 21490:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 23468:
                    cast_time = randomnr.create(1,3) +  1000;
                    return cast_time;

                case 23418:
                    cast_time = randomnr.create(1,3) +  1600;
                    return cast_time;

                case 23463:
                    cast_time = randomnr.create(1,3) +  1300;
                    return cast_time;

                case 23481:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 23959:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 27504:
                    cast_time = randomnr.create(1,3) +  1800;
                    return cast_time;

                #endregion Support
                #region Utility
                case 20325:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 20323:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 37920:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 30160:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 23255:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 23664:
                    cast_time = randomnr.create(1,3) +  2000;
                    return cast_time;

                case 34372:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 16454:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 20901:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 23414:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                #endregion Utility
                #region Path
                case 18975:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 47782:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 38054:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 47811:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                #endregion Path

                // Stalker
                #region Assault
                case 23148:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 23161:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 23183:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 23221:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 23205:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 32336:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 23268:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 23337:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 23984:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                #endregion Assault
                #region Support
                case 23523:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 23646:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 23608:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 23694:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 23955:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 31937:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 23699:
                    cast_time = randomnr.create(1,3) +  2646;
                    return cast_time;

                case 23892:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 31976:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 23908:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                #endregion Support
                #region Utility
                case 23173:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 23587:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 23704:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 26865:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 23236:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 23281:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 23829:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 32921:
                    cast_time = randomnr.create(1,3) +  3000;
                    return cast_time;

                case 23705:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 23896:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                #endregion Utility
                #region Path
                // Same as warrior
                #endregion Path

                // Engineer
                #region Assault
                case 26468:
                    cast_time = randomnr.create(1,3) +  1250;
                    return cast_time;

                case 25473:
                    cast_time = randomnr.create(1,3) +  3000;
                    return cast_time;

                case 25673:
                    cast_time = randomnr.create(1,3) +  400;
                    return cast_time;

                case 27002:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 20396:
                    cast_time = randomnr.create(1,3) +  1750;
                    return cast_time;

                case 25538:
                    cast_time = randomnr.create(1,3) +  1900;
                    return cast_time;

                case 25739:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 20639:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 22652:
                    cast_time = randomnr.create(1,3) +  3000;
                    return cast_time;

                case 20763:
                    cast_time = randomnr.create(1,3) +  350;
                    return cast_time;

                #endregion Assault
                #region Support
                case 27082:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 25472:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 25623:
                    cast_time = randomnr.create(1,3) +  2646;
                    return cast_time;

                case 25680:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 26059:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 25626:
                    cast_time = randomnr.create(1,3) +  1250;
                    return cast_time;

                case 26010:
                    cast_time = randomnr.create(1,3) +  2000;
                    return cast_time;

                case 26775:
                    cast_time = randomnr.create(1,3) +  1250;
                    return cast_time;

                case 20628:
                    cast_time = randomnr.create(1,3) +  3000;
                    return cast_time;

                case 34347:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                #endregion Support
                #region Utility
                case 25635:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 20492:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 26762:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 27021:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 26991:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 26998:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 25951:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 25819:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 28614:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 34176:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                #endregion Utility
                #region Path
                // Same as Warrior
                #endregion Path

                // Medic
                #region Assault
                case 47778:
                    cast_time = randomnr.create(1,3) +  1250;
                    return cast_time;

                case 16322:
                    cast_time = randomnr.create(1,3) +  1250;
                    return cast_time;

                case 47675:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 26038:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 25666:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 26061:
                    cast_time = randomnr.create(1,3) +  1646;
                    return cast_time;

                case 47793:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 27080:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 25692:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 25735:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                #endregion Assault
                #region Support
                case 47784:
                    cast_time = randomnr.create(1,3) +  1250;
                    return cast_time;

                case 25579:
                    cast_time = randomnr.create(1,3) +  1250;
                    return cast_time;

                case 47807:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 25530:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 16336:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 26120:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 24867:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 25566:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 16757:
                    cast_time = randomnr.create(1,3) +  1646;
                    return cast_time;

                case 26433:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                #endregion Support
                #region Utility
                case 26543:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 25747:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 22861:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 25617:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 25613:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 26786:
                    cast_time = randomnr.create(1,3) +  2000;
                    return cast_time;

                case 25729:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 26529:
                    cast_time = randomnr.create(1,3) +  1250;
                    return cast_time;

                case 25799:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                case 25595:
                    cast_time = randomnr.create(1,3) +  250;
                    return cast_time;

                #endregion Utility
                #region Path
                case 37303:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 46795:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                case 38113:
                    cast_time = randomnr.create(4,13) +  646;
                    return cast_time;

                #endregion Path
            }
        }
    }
}
