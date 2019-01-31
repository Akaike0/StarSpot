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

namespace StarSpot
{
    public partial class OptionsWindow
    {
        // Blacklist NPC list
        private List<string> blacklist_npc_list = new List<string>();

        // Visibility bool
        public static bool options_window_visible = false;
        
        public OptionsWindow()
        {
            InitializeComponent();

            // Blacklist array creation
            blacklist_npc_list = Properties.Settings.Default.blacklist_npcs.Split(',').ToList<string>();

            for (int i = 0; i < 20; i++)
            {
                if(blacklist_npc_list[i] != "0")
                {
                    blacklist_lbx.Items.Add(blacklist_npc_list[i]);
                }
            }

            // Add key using keys
            add_key_using();

            // Load configurations
            load_configs();
        }

        // Load configs
        public void load_configs()
        {
            // Add mods
            mods_cbx.Items.Add("Grinding");
            mods_cbx.Items.Add("Gathering");
            mods_cbx.Items.Add("Combat");
            mods_cbx.Items.Add("PVP");

            // Add Actionset
            ActionSet_CBX.Items.Add("Set 1");
            ActionSet_CBX.Items.Add("Set 2");
            ActionSet_CBX.SelectedItem = Properties.Settings.Default.action_set_selection;

            // SKills
            // 1 / R
            skills_1_tgw.IsChecked = Properties.Settings.Default.skill_1_enabled;

            // 2 /
            skills_2_tgw.IsChecked = Properties.Settings.Default.skill_2_enabled;

            // 3 /
            skills_3_tgw.IsChecked = Properties.Settings.Default.skill_3_enabled;

            // 4 /
            skills_4_tgw.IsChecked = Properties.Settings.Default.skill_4_enabled;

            // 5 /
            skills_5_tgw.IsChecked = Properties.Settings.Default.skill_5_enabled;

            //6 /
            skills_6_tgw.IsChecked = Properties.Settings.Default.skill_6_enabled;

            // 7 /
            skills_7_tgw.IsChecked = Properties.Settings.Default.skill_7_enabled;

            // 8 /
            skills_8_tgw.IsChecked = Properties.Settings.Default.skill_8_enabled;

            // 8 /
            skills_9_tgw.IsChecked = Properties.Settings.Default.skill_9_enabled;

            // Looting
            enable_looting_cbx.IsChecked = Properties.Settings.Default.looting_option;

            // Random jumping
            enable_random_jumping_cbx.IsChecked = Properties.Settings.Default.random_jumping;
            random_jumping_timer_tbx.Text = Properties.Settings.Default.random_jumping_timer.ToString();

            // Key using
            key_using_1_cbx.Text = Properties.Settings.Default.key_using_1_key;
            key_using_1_cooldown.Text = Properties.Settings.Default.key_using_1_cooldown.ToString();
            key_using_1_ct_tbx.Text = Properties.Settings.Default.key_using_1_ct.ToString();

            key_using_2_cbx.Text = Properties.Settings.Default.key_using_2_key;
            key_using_2_cooldown.Text = Properties.Settings.Default.key_using_2_cooldown.ToString();
            key_using_2_ct_tbx.Text = Properties.Settings.Default.key_using_2_ct.ToString();

            key_using_3_cbx.Text = Properties.Settings.Default.key_using_3_key;
            key_using_3_cooldown.Text = Properties.Settings.Default.key_using_3_cooldown.ToString();
            key_using_3_ct_tbx.Text = Properties.Settings.Default.key_using_3_ct.ToString();

            key_using_4_cbx.Text = Properties.Settings.Default.key_using_4_key;
            key_using_4_cooldown.Text = Properties.Settings.Default.key_using_4_cooldown.ToString();
            key_using_4_ct_tbx.Text = Properties.Settings.Default.key_using_4_ct.ToString();

            key_using_5_cbx.Text = Properties.Settings.Default.key_using_5_key;
            key_using_5_cooldown.Text = Properties.Settings.Default.key_using_5_cooldown.ToString();
            key_using_5_ct_tbx.Text = Properties.Settings.Default.key_using_5_ct.ToString();

            key_using_6_cbx.Text = Properties.Settings.Default.key_using_6_key;
            key_using_6_cooldown.Text = Properties.Settings.Default.key_using_6_cooldown.ToString();
            key_using_6_ct_tbx.Text = Properties.Settings.Default.key_using_6_ct.ToString();

            key_using_7_cbx.Text = Properties.Settings.Default.key_using_7_key;
            key_using_7_cooldown.Text = Properties.Settings.Default.key_using_7_cooldown.ToString();
            key_using_7_ct_tbx.Text = Properties.Settings.Default.key_using_7_ct.ToString();

            key_using_8_cbx.Text = Properties.Settings.Default.key_using_8_key;
            key_using_8_cooldown.Text = Properties.Settings.Default.key_using_8_cooldown.ToString();
            key_using_8_ct_tbx.Text = Properties.Settings.Default.key_using_8_ct.ToString();

            key_using_9_cbx.Text = Properties.Settings.Default.key_using_9_key;
            key_using_9_cooldown.Text = Properties.Settings.Default.key_using_9_cooldown.ToString();
            key_using_9_ct_tbx.Text = Properties.Settings.Default.key_using_9_ct.ToString();

            key_using_10_cbx.Text = Properties.Settings.Default.key_using_10_key;
            key_using_10_cooldown.Text = Properties.Settings.Default.key_using_10_cooldown.ToString();
            key_using_10_ct_tbx.Text = Properties.Settings.Default.key_using_10_ct.ToString();
        
            // Load death spots options
            enable_defending_after_death.IsChecked = Properties.Settings.Default.enable_defending_after_death;
            enable_tabbing_after_death.IsChecked = Properties.Settings.Default.enable_tabbing_after_death;

            // Stop after config
            enable_stop_after_cbx.IsChecked = Properties.Settings.Default.enable_stop_after;
            stop_after_min_tbx.Text = Properties.Settings.Default.stop_at_time.ToString();
            enable_stop_after_exit_wildstar.IsChecked = Properties.Settings.Default.enable_stop_after_exit_wildstar;

            // Start after config
            enable_start_after_cbx.IsChecked = Properties.Settings.Default.enable_start_after;
            start_after_tbx.Text = Properties.Settings.Default.start_after_min.ToString();

            // Level range
            hunting_level_range_tbx.Text = Convert.ToString(Properties.Settings.Default.level_range);

            // Player detection
            player_detection_stop_tbx.Text = Properties.Settings.Default.player_detection_min.ToString();
            player_detection_radius_tbx.Text = Properties.Settings.Default.player_detection_radius.ToString();
            player_detection_logout_for_tbx.Text = Properties.Settings.Default.player_detection_logout_for.ToString();
            enable_keyusing_logout.IsChecked = Properties.Settings.Default.logout_keyusing;
            enable_player_detection_alert.IsChecked = Properties.Settings.Default.player_detection_alert;
            player_detection_alert_duration.Text = Properties.Settings.Default.player_detection_alert_duration.ToString();

            // Attack range
            attack_range_tbx.Text = Properties.Settings.Default.attack_range.ToString();

            // Ignore Players
            enable_ignore_players.IsChecked = Properties.Settings.Default.enable_ignore_players;

            // Unstucking
            attack_unstucking_duration_tbx.Text = Properties.Settings.Default.unstucking_attack_duration.ToString();
            attack_unstucking_tbx.Text = Properties.Settings.Default.unstucking_attack_time.ToString();
            run_unstucking_time_tbx.Text = Properties.Settings.Default.unstucking_run_time.ToString();
            // Exit wildstar after x attempts
            exit_wildstar_unstucking_tbx.Text = Properties.Settings.Default.exit_wildstar_after_x_attempts.ToString();

            // Autoselling
            auto_selling_duration_tbx.Text = Properties.Settings.Default.autoselling_timer.ToString();
            enable_autoselling_cbx.IsChecked = Properties.Settings.Default.autoselling_enabled;
            enable_autoselling_attack_mobs.IsChecked = Properties.Settings.Default.selling_spots_attack_mobs;
            enable_autoselling_ignore_mobs.IsChecked = Properties.Settings.Default.enable_autoselling_ignore_mobs;
            enable_go_to_vendor_after_death.IsChecked = Properties.Settings.Default.got_to_vendor_after_death;

            // Healing
            healing_under_tbx.Text = Properties.Settings.Default.healing_under_np.ToString();
            healing_to_100_usekeys_cbx.IsChecked = Properties.Settings.Default.heal_until_100_key_using_enabled;
            healing_until_tbx.Text = Properties.Settings.Default.heal_until_percent.ToString();
            ignore_shield_cbx.IsChecked = Properties.Settings.Default.ignore_shield_healing;

            // Use mount
            enable_autoselling_use_mount.IsChecked = Properties.Settings.Default.use_grinding_mount;
            enable_autoselling_use_mount_selling_death_cbx.IsChecked = Properties.Settings.Default.use_mount_death_selling;

            // Load the mod
            mods_cbx.Text = Properties.Settings.Default.mods;

            // Aim friendly target
            enable_aim_friendly_target.IsChecked = Properties.Settings.Default.aim_friendly_target;

            // Out of range (search range)
            //leave_target_if_out_of_range_cbx.IsChecked = Properties.Settings.Default.leave_if_out_of_range;

            // Walk in circle
            enable_walking_in_circle.IsChecked = Properties.Settings.Default.walk_in_circle;

            // Search range
            search_range_tbx.Text = Properties.Settings.Default.search_range.ToString();

            // Whitelist
            use_whitelist_cbx.IsChecked = Properties.Settings.Default.whitelist_enabled;

            // Gathering
            gathering_attack_nodes_cbx.IsChecked = Properties.Settings.Default.gathering_attack_target;

            // Fight movement
            fight_movement_cbx.IsChecked = Properties.Settings.Default.fight_movement_enabled;

            // Attack mobs from other players
            enable_attack_player_mobs.IsChecked = Properties.Settings.Default.attack_mobs_from_other_players;

            // Search closest spot after a fight
            enable_move_to_closest_spot.IsChecked = Properties.Settings.Default.move_to_closest_spot;

            // Login pw
            autologin_password.Text = Properties.Settings.Default.autologin_password;

            // Hacks
            enable_maximum_zoom_cbx.IsChecked = Properties.Settings.Default.enable_maximum_zoom;
            enable_no_gpu_rendering_cbx.IsChecked = Properties.Settings.Default.disable_gpu_rendering;
            enable_reduce_fps_cbx.IsChecked = Properties.Settings.Default.reduce_fps_hack;

            // Shortcuts
            disable_start_stop_shortcut_cbx.IsChecked = Properties.Settings.Default.disable_start_stop_shortcut;
        }

        // Add keys
        public void add_key_using()
        { 
            // Add letters
            key_using_1_cbx.Items.Add("");
            key_using_1_cbx.Items.Add("Y");
            key_using_1_cbx.Items.Add("G");
            key_using_1_cbx.Items.Add("X");
            key_using_1_cbx.Items.Add("C");
            key_using_1_cbx.Items.Add("V");
            key_using_1_cbx.Items.Add("B");
            key_using_1_cbx.Items.Add("N");
            key_using_1_cbx.Items.Add("M");
            key_using_1_cbx.Items.Add(",");
            key_using_1_cbx.Items.Add(".");
            key_using_1_cbx.Items.Add("-");

            key_using_2_cbx.Items.Add("");
            key_using_2_cbx.Items.Add("Y");
            key_using_2_cbx.Items.Add("G");
            key_using_2_cbx.Items.Add("X");
            key_using_2_cbx.Items.Add("C");
            key_using_2_cbx.Items.Add("V");
            key_using_2_cbx.Items.Add("B");
            key_using_2_cbx.Items.Add("N");
            key_using_2_cbx.Items.Add("M");
            key_using_2_cbx.Items.Add(",");
            key_using_2_cbx.Items.Add(".");
            key_using_2_cbx.Items.Add("-");

            key_using_3_cbx.Items.Add("");
            key_using_3_cbx.Items.Add("Y");
            key_using_3_cbx.Items.Add("G");
            key_using_3_cbx.Items.Add("X");
            key_using_3_cbx.Items.Add("C");
            key_using_3_cbx.Items.Add("V");
            key_using_3_cbx.Items.Add("B");
            key_using_3_cbx.Items.Add("N");
            key_using_3_cbx.Items.Add("M");
            key_using_3_cbx.Items.Add(",");
            key_using_3_cbx.Items.Add(".");
            key_using_3_cbx.Items.Add("-");

            key_using_4_cbx.Items.Add("");
            key_using_4_cbx.Items.Add("Y");
            key_using_4_cbx.Items.Add("G");
            key_using_4_cbx.Items.Add("X");
            key_using_4_cbx.Items.Add("C");
            key_using_4_cbx.Items.Add("V");
            key_using_4_cbx.Items.Add("B");
            key_using_4_cbx.Items.Add("N");
            key_using_4_cbx.Items.Add("M");
            key_using_4_cbx.Items.Add(",");
            key_using_4_cbx.Items.Add(".");
            key_using_4_cbx.Items.Add("-");

            key_using_5_cbx.Items.Add("");
            key_using_5_cbx.Items.Add("Y");
            key_using_5_cbx.Items.Add("G");
            key_using_5_cbx.Items.Add("X");
            key_using_5_cbx.Items.Add("C");
            key_using_5_cbx.Items.Add("V");
            key_using_5_cbx.Items.Add("B");
            key_using_5_cbx.Items.Add("N");
            key_using_5_cbx.Items.Add("M");
            key_using_5_cbx.Items.Add(",");
            key_using_5_cbx.Items.Add(".");
            key_using_5_cbx.Items.Add("-");

            key_using_6_cbx.Items.Add("");
            key_using_6_cbx.Items.Add("Y");
            key_using_6_cbx.Items.Add("G");
            key_using_6_cbx.Items.Add("X");
            key_using_6_cbx.Items.Add("C");
            key_using_6_cbx.Items.Add("V");
            key_using_6_cbx.Items.Add("B");
            key_using_6_cbx.Items.Add("N");
            key_using_6_cbx.Items.Add("M");
            key_using_6_cbx.Items.Add(",");
            key_using_6_cbx.Items.Add(".");
            key_using_6_cbx.Items.Add("-");

            key_using_7_cbx.Items.Add("");
            key_using_7_cbx.Items.Add("Y");
            key_using_7_cbx.Items.Add("G");
            key_using_7_cbx.Items.Add("X");
            key_using_7_cbx.Items.Add("C");
            key_using_7_cbx.Items.Add("V");
            key_using_7_cbx.Items.Add("B");
            key_using_7_cbx.Items.Add("N");
            key_using_7_cbx.Items.Add("M");
            key_using_7_cbx.Items.Add(",");
            key_using_7_cbx.Items.Add(".");
            key_using_7_cbx.Items.Add("-");

            key_using_8_cbx.Items.Add("");
            key_using_8_cbx.Items.Add("Y");
            key_using_8_cbx.Items.Add("G");
            key_using_8_cbx.Items.Add("X");
            key_using_8_cbx.Items.Add("C");
            key_using_8_cbx.Items.Add("V");
            key_using_8_cbx.Items.Add("B");
            key_using_8_cbx.Items.Add("N");
            key_using_8_cbx.Items.Add("M");
            key_using_8_cbx.Items.Add(",");
            key_using_8_cbx.Items.Add(".");
            key_using_8_cbx.Items.Add("-");

            key_using_9_cbx.Items.Add("");
            key_using_9_cbx.Items.Add("Y");
            key_using_9_cbx.Items.Add("G");
            key_using_9_cbx.Items.Add("X");
            key_using_9_cbx.Items.Add("C");
            key_using_9_cbx.Items.Add("V");
            key_using_9_cbx.Items.Add("B");
            key_using_9_cbx.Items.Add("N");
            key_using_9_cbx.Items.Add("M");
            key_using_9_cbx.Items.Add(",");
            key_using_9_cbx.Items.Add(".");
            key_using_9_cbx.Items.Add("-");

            key_using_10_cbx.Items.Add("");
            key_using_10_cbx.Items.Add("Y");
            key_using_10_cbx.Items.Add("G");
            key_using_10_cbx.Items.Add("X");
            key_using_10_cbx.Items.Add("C");
            key_using_10_cbx.Items.Add("V");
            key_using_10_cbx.Items.Add("B");
            key_using_10_cbx.Items.Add("N");
            key_using_10_cbx.Items.Add("M");
            key_using_10_cbx.Items.Add(",");
            key_using_10_cbx.Items.Add(".");
            key_using_10_cbx.Items.Add("-");

            // Add nummbers
            for (int i = 1; i < 10; i++)
            {
                key_using_1_cbx.Items.Add(i.ToString());
                key_using_2_cbx.Items.Add(i.ToString());
                key_using_3_cbx.Items.Add(i.ToString());
                key_using_4_cbx.Items.Add(i.ToString());
                key_using_5_cbx.Items.Add(i.ToString());
                key_using_6_cbx.Items.Add(i.ToString());
                key_using_7_cbx.Items.Add(i.ToString());
                key_using_8_cbx.Items.Add(i.ToString());
                key_using_9_cbx.Items.Add(i.ToString());
                key_using_10_cbx.Items.Add(i.ToString());
            }
        }

        // Save
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Check errors
            check_enteries();

            try
            {
                // Save the changes
                save_configs();
            }
            catch { 
                // Error
                System.Windows.MessageBox.Show("An entry is wrong, please check your entries.", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            // Disable visibility for opening
            options_window_visible = false;
        }

        // Check enteries
        private void check_enteries()
        {
            // Key using
            if(key_using_1_cooldown.Text == "")
            {
                key_using_1_cooldown.Text = "0";
            }
            if (key_using_2_cooldown.Text == "")
            {
                key_using_2_cooldown.Text = "0";
            }
            if (key_using_3_cooldown.Text == "")
            {
                key_using_3_cooldown.Text = "0";
            }
            if (key_using_4_cooldown.Text == "")
            {
                key_using_4_cooldown.Text = "0";
            }
            if (key_using_5_cooldown.Text == "")
            {
                key_using_5_cooldown.Text = "0";
            }
            if (key_using_6_cooldown.Text == "")
            {
                key_using_6_cooldown.Text = "0";
            }
            if (key_using_7_cooldown.Text == "")
            {
                key_using_7_cooldown.Text = "0";
            }
            if (key_using_8_cooldown.Text == "")
            {
                key_using_8_cooldown.Text = "0";
            }
            if (key_using_9_cooldown.Text == "")
            {
                key_using_9_cooldown.Text = "0";
            }
            if (key_using_10_cooldown.Text == "")
            {
                key_using_10_cooldown.Text = "0";
            }
            if(key_using_1_ct_tbx.Text == "") // Cast Time
            {
                key_using_1_ct_tbx.Text = "0";
            }
            if (key_using_2_cooldown.Text == "")
            {
                key_using_2_cooldown.Text = "0";
            }
            if (key_using_3_cooldown.Text == "")
            {
                key_using_3_cooldown.Text = "0";
            }
            if (key_using_4_cooldown.Text == "")
            {
                key_using_4_cooldown.Text = "0";
            }
            if (key_using_5_cooldown.Text == "")
            {
                key_using_5_cooldown.Text = "0";
            }
            if (key_using_6_cooldown.Text == "")
            {
                key_using_6_cooldown.Text = "0";
            }
            if (key_using_7_cooldown.Text == "")
            {
                key_using_7_cooldown.Text = "0";
            }
            if (key_using_8_cooldown.Text == "")
            {
                key_using_8_cooldown.Text = "0";
            }
            if (key_using_9_cooldown.Text == "")
            {
                key_using_9_cooldown.Text = "0";
            }
            if (key_using_10_cooldown.Text == "")
            {
                key_using_10_cooldown.Text = "0";
            }

            // Other stuff
            if(random_jumping_timer_tbx.Text == "")
            {
                random_jumping_timer_tbx.Text = "0";
            }

            if(stop_after_min_tbx.Text == "")
            {
                stop_after_min_tbx.Text = "0";
            }

            if(hunting_level_range_tbx.Text == "")
            {
                hunting_level_range_tbx.Text = "0";
            }

            if(start_after_tbx.Text == "")
            {
                start_after_tbx.Text = "0";
            }

            if(attack_range_tbx.Text == "")
            {
                attack_range_tbx.Text = "0";
            }

            if(search_range_tbx.Text == "")
            {
                search_range_tbx.Text = "35";
            }

            if(attack_unstucking_tbx.Text == "")
            {
                attack_unstucking_tbx.Text = "8";
            }

            if(attack_unstucking_duration_tbx.Text == "")
            {
                attack_unstucking_duration_tbx.Text = "1";
            }

            if(run_unstucking_time_tbx.Text == "")
            {
                run_unstucking_time_tbx.Text = "10";
            }

            if(auto_selling_duration_tbx.Text == "")
            {
                auto_selling_duration_tbx.Text = "0";
            }

            if(healing_under_tbx.Text == "")
            {
                healing_under_tbx.Text = "0";
            }

            if(healing_until_tbx.Text == "")
            {
                healing_until_tbx.Text = "0";
            }

            if(player_detection_radius_tbx.Text == "")
            {
                player_detection_radius_tbx.Text = "0";
            }

            if (player_detection_stop_tbx.Text == "")
            {
                player_detection_stop_tbx.Text = "0";
            }

            if(player_detection_logout_for_tbx.Text == "")
            {
                player_detection_logout_for_tbx.Text = "0";
            }

            if(player_detection_alert_duration.Text == "")
            {
                player_detection_alert_duration.Text = "0";
            }
        }

        // Save properties
        private void save_configs()
        {
            // Add Actionset
            Properties.Settings.Default.action_set_selection = ActionSet_CBX.Text;

            // Skills
            // 1 /
            Properties.Settings.Default.skill_1_enabled = skills_1_tgw.IsChecked;
            // 2 /
            Properties.Settings.Default.skill_2_enabled = skills_2_tgw.IsChecked;
            // 3 /
            Properties.Settings.Default.skill_3_enabled = skills_3_tgw.IsChecked;
            // 4 /
            Properties.Settings.Default.skill_4_enabled = skills_4_tgw.IsChecked;
            // 5 /
            Properties.Settings.Default.skill_5_enabled = skills_5_tgw.IsChecked;
            // 6 /
            Properties.Settings.Default.skill_6_enabled = skills_6_tgw.IsChecked;
            // 7 /
            Properties.Settings.Default.skill_7_enabled = skills_7_tgw.IsChecked;
            // 8 /
            Properties.Settings.Default.skill_8_enabled = skills_8_tgw.IsChecked;
            // 9 /
            Properties.Settings.Default.skill_9_enabled = skills_9_tgw.IsChecked;

            // Looting
            Properties.Settings.Default.looting_option = enable_looting_cbx.IsChecked.Value;

            // Random jumping
            Properties.Settings.Default.random_jumping = enable_random_jumping_cbx.IsChecked.Value;
            Properties.Settings.Default.random_jumping_timer = Convert.ToInt32(random_jumping_timer_tbx.Text);

            // Key Using
            Properties.Settings.Default.key_using_1_key = key_using_1_cbx.Text;
            Properties.Settings.Default.key_using_1_cooldown = Convert.ToInt32(key_using_1_cooldown.Text);
            Properties.Settings.Default.key_using_1_ct = Convert.ToInt32(key_using_1_ct_tbx.Text);

            Properties.Settings.Default.key_using_2_key = key_using_2_cbx.Text;
            Properties.Settings.Default.key_using_2_cooldown = Convert.ToInt32(key_using_2_cooldown.Text);
            Properties.Settings.Default.key_using_2_ct = Convert.ToInt32(key_using_2_ct_tbx.Text);

            Properties.Settings.Default.key_using_3_key = key_using_3_cbx.Text;
            Properties.Settings.Default.key_using_3_cooldown = Convert.ToInt32(key_using_3_cooldown.Text);
            Properties.Settings.Default.key_using_3_ct = Convert.ToInt32(key_using_3_ct_tbx.Text);

            Properties.Settings.Default.key_using_4_key = key_using_4_cbx.Text;
            Properties.Settings.Default.key_using_4_cooldown = Convert.ToInt32(key_using_4_cooldown.Text);
            Properties.Settings.Default.key_using_4_ct = Convert.ToInt32(key_using_4_ct_tbx.Text);

            Properties.Settings.Default.key_using_5_key = key_using_5_cbx.Text;
            Properties.Settings.Default.key_using_5_cooldown = Convert.ToInt32(key_using_5_cooldown.Text);
            Properties.Settings.Default.key_using_5_ct = Convert.ToInt32(key_using_5_ct_tbx.Text);

            Properties.Settings.Default.key_using_6_key = key_using_6_cbx.Text;
            Properties.Settings.Default.key_using_6_cooldown = Convert.ToInt32(key_using_6_cooldown.Text);
            Properties.Settings.Default.key_using_6_ct = Convert.ToInt32(key_using_6_ct_tbx.Text);

            Properties.Settings.Default.key_using_7_key = key_using_7_cbx.Text;
            Properties.Settings.Default.key_using_7_cooldown = Convert.ToInt32(key_using_7_cooldown.Text);
            Properties.Settings.Default.key_using_7_ct = Convert.ToInt32(key_using_7_ct_tbx.Text);

            Properties.Settings.Default.key_using_8_key = key_using_8_cbx.Text;
            Properties.Settings.Default.key_using_8_cooldown = Convert.ToInt32(key_using_8_cooldown.Text);
            Properties.Settings.Default.key_using_8_ct = Convert.ToInt32(key_using_8_ct_tbx.Text);

            Properties.Settings.Default.key_using_9_key = key_using_9_cbx.Text;
            Properties.Settings.Default.key_using_9_cooldown = Convert.ToInt32(key_using_9_cooldown.Text);
            Properties.Settings.Default.key_using_9_ct = Convert.ToInt32(key_using_9_ct_tbx.Text);

            Properties.Settings.Default.key_using_10_key = key_using_10_cbx.Text;
            Properties.Settings.Default.key_using_10_cooldown = Convert.ToInt32(key_using_10_cooldown.Text);
            Properties.Settings.Default.key_using_10_ct = Convert.ToInt32(key_using_10_ct_tbx.Text);

            // Death spots options
            Properties.Settings.Default.enable_defending_after_death = enable_defending_after_death.IsChecked.Value;
            Properties.Settings.Default.enable_tabbing_after_death = enable_tabbing_after_death.IsChecked.Value;

            // Stop after config
            Properties.Settings.Default.enable_stop_after = enable_stop_after_cbx.IsChecked.Value;
            Properties.Settings.Default.stop_at_time = stop_after_min_tbx.Text;
            Properties.Settings.Default.enable_stop_after_exit_wildstar = enable_stop_after_exit_wildstar.IsChecked.Value;

            // Start after config
            Properties.Settings.Default.enable_start_after = enable_start_after_cbx.IsChecked.Value;
            Properties.Settings.Default.start_after_min = Convert.ToInt32(start_after_tbx.Text);

            // Level range
            if (hunting_level_range_tbx.Text != " " && hunting_level_range_tbx.Text != "")
            {
                Properties.Settings.Default.level_range = Convert.ToInt32(hunting_level_range_tbx.Text);
            }

            // Player detection
            Properties.Settings.Default.player_detection_min = Convert.ToInt32(player_detection_stop_tbx.Text);
            Properties.Settings.Default.player_detection_radius = Convert.ToInt32(player_detection_radius_tbx.Text);
            Properties.Settings.Default.player_detection_logout_for = Convert.ToInt32(player_detection_logout_for_tbx.Text);
            Properties.Settings.Default.logout_keyusing = enable_keyusing_logout.IsChecked.Value;
            Properties.Settings.Default.player_detection_alert = enable_player_detection_alert.IsChecked.Value;
            Properties.Settings.Default.player_detection_alert_duration = Convert.ToInt32(player_detection_alert_duration.Text);

            // Blacklist 
            Properties.Settings.Default.blacklist_npcs = ConvertStringArrayToStringJoin(blacklist_npc_list.ToArray());

            // Bot using
            //Properties.Settings.Default.bot_1_key = bot_1_key_cbx.Text;
            //Properties.Settings.Default.bot_1_use = bot_1_use.IsChecked;
            //Properties.Settings.Default.bot_2_key = bot_2_key_cbx.Text;
            //Properties.Settings.Default.bot_2_use = bot_2_use.IsChecked;

            // Attack range
            Properties.Settings.Default.attack_range = Convert.ToUInt32(attack_range_tbx.Text);

            // Ignore Players
            Properties.Settings.Default.enable_ignore_players = enable_ignore_players.IsChecked.Value;

            // Unstucking
            if(Convert.ToInt32(run_unstucking_time_tbx.Text) <= 4)
            {
                Properties.Settings.Default.unstucking_run_time = 5;
            } else { Properties.Settings.Default.unstucking_run_time = Convert.ToInt32(run_unstucking_time_tbx.Text); }
            Properties.Settings.Default.unstucking_attack_time = Convert.ToInt32(attack_unstucking_tbx.Text);
            Properties.Settings.Default.unstucking_attack_duration = Convert.ToInt32(attack_unstucking_duration_tbx.Text);
            // Exit wildstar after x attempts
            Properties.Settings.Default.exit_wildstar_after_x_attempts = Convert.ToInt32(exit_wildstar_unstucking_tbx.Text);

            // Autoselling
            Properties.Settings.Default.autoselling_timer = Convert.ToInt32(auto_selling_duration_tbx.Text);
            Properties.Settings.Default.autoselling_enabled = enable_autoselling_cbx.IsChecked.Value;
            Properties.Settings.Default.selling_spots_attack_mobs = enable_autoselling_attack_mobs.IsChecked.Value;
            Properties.Settings.Default.enable_autoselling_ignore_mobs = enable_autoselling_ignore_mobs.IsChecked.Value;
            Properties.Settings.Default.got_to_vendor_after_death = enable_go_to_vendor_after_death.IsChecked.Value;

            // Healing
            Properties.Settings.Default.healing_under_np = Convert.ToInt32(healing_under_tbx.Text);
            Properties.Settings.Default.heal_until_100_key_using_enabled = healing_to_100_usekeys_cbx.IsChecked.Value;
            Properties.Settings.Default.heal_until_percent = Convert.ToInt32(healing_until_tbx.Text);
            Properties.Settings.Default.ignore_shield_healing = ignore_shield_cbx.IsChecked.Value;

            // Use mount
            Properties.Settings.Default.use_grinding_mount = enable_autoselling_use_mount.IsChecked.Value;
            Properties.Settings.Default.use_mount_death_selling = enable_autoselling_use_mount_selling_death_cbx.IsChecked.Value;

            // Mods
            Properties.Settings.Default.mods = mods_cbx.Text;

            // Search range
            Properties.Settings.Default.search_range = Convert.ToInt32(search_range_tbx.Text);

            // Aim friendly target
            Properties.Settings.Default.aim_friendly_target = enable_aim_friendly_target.IsChecked.Value;

            // Out of range (search range)
            //Properties.Settings.Default.leave_if_out_of_range = leave_target_if_out_of_range_cbx.IsChecked.Value;

            // Walk in circle
            Properties.Settings.Default.walk_in_circle = enable_walking_in_circle.IsChecked.Value;

            // Whitelist
            Properties.Settings.Default.whitelist_enabled = use_whitelist_cbx.IsChecked.Value;

            // Gathering
            Properties.Settings.Default.gathering_attack_target = gathering_attack_nodes_cbx.IsChecked.Value;

            // Fight movement
            Properties.Settings.Default.fight_movement_enabled = fight_movement_cbx.IsChecked.Value;

            // Attack mobs from other players
            Properties.Settings.Default.attack_mobs_from_other_players = enable_attack_player_mobs.IsChecked.Value;

            // Search for closest spot
            Properties.Settings.Default.move_to_closest_spot = enable_move_to_closest_spot.IsChecked.Value;

            // Login pw
            Properties.Settings.Default.autologin_password = autologin_password.Text;

            // Hacks
            Properties.Settings.Default.disable_gpu_rendering = enable_no_gpu_rendering_cbx.IsChecked.Value;
            Properties.Settings.Default.enable_maximum_zoom = enable_maximum_zoom_cbx.IsChecked.Value;
            Properties.Settings.Default.reduce_fps_hack = enable_reduce_fps_cbx.IsChecked.Value;

            // Shortcuts
            Properties.Settings.Default.disable_start_stop_shortcut = disable_start_stop_shortcut_cbx.IsChecked.Value;

            // Save it
            Properties.Settings.Default.Save();
        }

        // Array function
        static string ConvertStringArrayToStringJoin(string[] array)
        {
            //
            // Use string Join to concatenate the string elements.
            //
            string result = string.Join(",", array);
            return result;
        }

        // Skill More Buttons
        #region Skills and Keys
        private void skill_0_more_Click(object sender, RoutedEventArgs e)
        {
            // Skill More Window
            SkillsMoreWindow skillmorewindow = new SkillsMoreWindow();

            // Select the skill in the skillmorewindow
            SkillsMoreWindow.skill_selected = -1;

            // Open the Window
            try
            {
                if (SkillsMoreWindow.showed == false)
                {
                    skillmorewindow.Show(); // Show window
                    SkillsMoreWindow.showed = true; // Window is opened
                }

            }
            catch { }
        }
        private void skill_1_more_Click(object sender, RoutedEventArgs e)
        {
            // Skill More Window
            SkillsMoreWindow skillmorewindow = new SkillsMoreWindow();

            // Select the skill in the skillmorewindow
            SkillsMoreWindow.skill_selected = 0;

            // Open the Window
            try
            {
                if (SkillsMoreWindow.showed == false)
                {
                    skillmorewindow.Show(); // Show window
                    SkillsMoreWindow.showed = true; // Window is opened
                }
                   
            }
            catch { }
        }
        private void skill_2_more_Click(object sender, RoutedEventArgs e)
        {
            // Skill More Window
            SkillsMoreWindow skillmorewindow = new SkillsMoreWindow();

            // Select the skill in the skillmorewindow
            SkillsMoreWindow.skill_selected = 1;

            // Open the Window
            try
            {
                if (SkillsMoreWindow.showed == false)
                {
                    skillmorewindow.Show(); // Show window
                    SkillsMoreWindow.showed = true; // Window is opened
                }

            }
            catch { }
        }
        private void skill_3_more_Click(object sender, RoutedEventArgs e)
        {
            // Skill More Window
            SkillsMoreWindow skillmorewindow = new SkillsMoreWindow();

            // Select the skill in the skillmorewindow
            SkillsMoreWindow.skill_selected = 2;

            // Open the Window
            try
            {
                if (SkillsMoreWindow.showed == false)
                {
                    skillmorewindow.Show(); // Show window
                    SkillsMoreWindow.showed = true; // Window is opened
                }

            }
            catch { }
        }
        private void skill_4_more_Click(object sender, RoutedEventArgs e)
        {
            // Skill More Window
            SkillsMoreWindow skillmorewindow = new SkillsMoreWindow();

            // Select the skill in the skillmorewindow
            SkillsMoreWindow.skill_selected = 3;

            // Open the Window
            try
            {
                if (SkillsMoreWindow.showed == false)
                {
                    skillmorewindow.Show(); // Show window
                    SkillsMoreWindow.showed = true; // Window is opened
                }

            }
            catch { }
        }
        private void skill_5_more_Click(object sender, RoutedEventArgs e)
        {
            // Skill More Window
            SkillsMoreWindow skillmorewindow = new SkillsMoreWindow();

            // Select the skill in the skillmorewindow
            SkillsMoreWindow.skill_selected = 4;

            // Open the Window
            try
            {
                if (SkillsMoreWindow.showed == false)
                {
                    skillmorewindow.Show(); // Show window
                    SkillsMoreWindow.showed = true; // Window is opened
                }

            }
            catch { }
        }
        private void skill_6_more_Click(object sender, RoutedEventArgs e)
        {
            // Skill More Window
            SkillsMoreWindow skillmorewindow = new SkillsMoreWindow();

            // Select the skill in the skillmorewindow
            SkillsMoreWindow.skill_selected = 5;

            // Open the Window
            try
            {
                if (SkillsMoreWindow.showed == false)
                {
                    skillmorewindow.Show(); // Show window
                    SkillsMoreWindow.showed = true; // Window is opened
                }

            }
            catch { }
        }
        private void skill_7_more_Click(object sender, RoutedEventArgs e)
        {
            // Skill More Window
            SkillsMoreWindow skillmorewindow = new SkillsMoreWindow();

            // Select the skill in the skillmorewindow
            SkillsMoreWindow.skill_selected = 6;

            // Open the Window
            try
            {
                if (SkillsMoreWindow.showed == false)
                {
                    skillmorewindow.Show(); // Show window
                    SkillsMoreWindow.showed = true; // Window is opened
                }

            }
            catch { }
        }
        private void skill_8_more_Click(object sender, RoutedEventArgs e)
        {
            // Skill More Window
            SkillsMoreWindow skillmorewindow = new SkillsMoreWindow();

            // Select the skill in the skillmorewindow
            SkillsMoreWindow.skill_selected = 7;

            // Open the Window
            try
            {
                if (SkillsMoreWindow.showed == false)
                {
                    skillmorewindow.Show(); // Show window
                    SkillsMoreWindow.showed = true; // Window is opened
                }

            }
            catch { }
        }

        // Key Using More Buttons
        private void key_using_1_more_Click(object sender, RoutedEventArgs e)
        {
            // Skill More Window
            SkillsMoreWindow skillmorewindow = new SkillsMoreWindow();

            // Select the skill in the skillmorewindow
            SkillsMoreWindow.skill_selected = 8;

            // Open the Window
            try
            {
                if (SkillsMoreWindow.showed == false)
                {
                    skillmorewindow.Show(); // Show window
                    SkillsMoreWindow.showed = true; // Window is opened
                }

            }
            catch { }
        }
        private void key_using_2_more_Click(object sender, RoutedEventArgs e)
        {
            // Skill More Window
            SkillsMoreWindow skillmorewindow = new SkillsMoreWindow();

            // Select the skill in the skillmorewindow
            SkillsMoreWindow.skill_selected = 9;

            // Open the Window
            try
            {
                if (SkillsMoreWindow.showed == false)
                {
                    skillmorewindow.Show(); // Show window
                    SkillsMoreWindow.showed = true; // Window is opened
                }

            }
            catch { }
        }
        private void key_using_3_more_Click(object sender, RoutedEventArgs e)
        {
            // Skill More Window
            SkillsMoreWindow skillmorewindow = new SkillsMoreWindow();

            // Select the skill in the skillmorewindow
            SkillsMoreWindow.skill_selected = 10;

            // Open the Window
            try
            {
                if (SkillsMoreWindow.showed == false)
                {
                    skillmorewindow.Show(); // Show window
                    SkillsMoreWindow.showed = true; // Window is opened
                }

            }
            catch { }
        }
        private void key_using_4_more_Click(object sender, RoutedEventArgs e)
        {
            // Skill More Window
            SkillsMoreWindow skillmorewindow = new SkillsMoreWindow();

            // Select the skill in the skillmorewindow
            SkillsMoreWindow.skill_selected = 11;

            // Open the Window
            try
            {
                if (SkillsMoreWindow.showed == false)
                {
                    skillmorewindow.Show(); // Show window
                    SkillsMoreWindow.showed = true; // Window is opened
                }

            }
            catch { }
        }
        private void key_using_5_more_Click(object sender, RoutedEventArgs e)
        {
            // Skill More Window
            SkillsMoreWindow skillmorewindow = new SkillsMoreWindow();

            // Select the skill in the skillmorewindow
            SkillsMoreWindow.skill_selected = 12;

            // Open the Window
            try
            {
                if (SkillsMoreWindow.showed == false)
                {
                    skillmorewindow.Show(); // Show window
                    SkillsMoreWindow.showed = true; // Window is opened
                }

            }
            catch { }
        }
        private void key_using_6_more_Click(object sender, RoutedEventArgs e)
        {
            // Skill More Window
            SkillsMoreWindow skillmorewindow = new SkillsMoreWindow();

            // Select the skill in the skillmorewindow
            SkillsMoreWindow.skill_selected = 13;

            // Open the Window
            try
            {
                if (SkillsMoreWindow.showed == false)
                {
                    skillmorewindow.Show(); // Show window
                    SkillsMoreWindow.showed = true; // Window is opened
                }

            }
            catch { }
        }
        private void key_using_7_more_Click(object sender, RoutedEventArgs e)
        {
            // Skill More Window
            SkillsMoreWindow skillmorewindow = new SkillsMoreWindow();

            // Select the skill in the skillmorewindow
            SkillsMoreWindow.skill_selected = 14;

            // Open the Window
            try
            {
                if (SkillsMoreWindow.showed == false)
                {
                    skillmorewindow.Show(); // Show window
                    SkillsMoreWindow.showed = true; // Window is opened
                }

            }
            catch { }
        }
        private void key_using_8_more_Click(object sender, RoutedEventArgs e)
        {
            // Skill More Window
            SkillsMoreWindow skillmorewindow = new SkillsMoreWindow();

            // Select the skill in the skillmorewindow
            SkillsMoreWindow.skill_selected = 15;

            // Open the Window
            try
            {
                if (SkillsMoreWindow.showed == false)
                {
                    skillmorewindow.Show(); // Show window
                    SkillsMoreWindow.showed = true; // Window is opened
                }

            }
            catch { }
        }
        private void key_using_9_more_Click(object sender, RoutedEventArgs e)
        {
            // Skill More Window
            SkillsMoreWindow skillmorewindow = new SkillsMoreWindow();

            // Select the skill in the skillmorewindow
            SkillsMoreWindow.skill_selected = 16;

            // Open the Window
            try
            {
                if (SkillsMoreWindow.showed == false)
                {
                    skillmorewindow.Show(); // Show window
                    SkillsMoreWindow.showed = true; // Window is opened
                }

            }
            catch { }
        }
        private void key_using_10_more_Click(object sender, RoutedEventArgs e)
        {
            // Skill More Window
            SkillsMoreWindow skillmorewindow = new SkillsMoreWindow();

            // Select the skill in the skillmorewindow
            SkillsMoreWindow.skill_selected = 17;

            // Open the Window
            try
            {
                if (SkillsMoreWindow.showed == false)
                {
                    skillmorewindow.Show(); // Show window
                    SkillsMoreWindow.showed = true; // Window is opened
                }

            }
            catch { }
        }
        #endregion Skills and Keys

        // Blacklist add button
        private void add_npc_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TargetInfo.target_id != 0) // Check if player has a target
                {
                    if (!blacklist_npc_list.Contains(TargetInfo.target_name)) // Check if blacklist has already such a name
                    {
                        try
                        {
                            // Add the npc to the lbx
                            blacklist_lbx.Items.Add(TargetInfo.target_name); // Add to the blacklist

                            // Delete it from the array
                            blacklist_npc_list[blacklist_lbx.Items.Count] = TargetInfo.target_name; // Delete the old space from the array
                        }
                        catch { }
                    }
                }
            }
            catch { }
        }


        private void add_npc_by_name_btn_Click(object sender, RoutedEventArgs e)
        {
            if(blacklist_npc_tbx.Text != "")
            {
                if (!blacklist_npc_list.Contains(blacklist_npc_tbx.Text)) // Check if blacklist has already such a name
                {
                    try
                    {
                        // Add the npc to the lbx
                        blacklist_lbx.Items.Add(blacklist_npc_tbx.Text); // Add to the blacklist

                        // Delete it from the array
                        blacklist_npc_list[blacklist_lbx.Items.Count] = blacklist_npc_tbx.Text; // Delete the old space from the array
                    }
                    catch { }
                }
            }
            else
            {
                MessageBox.Show("The text box is empty! Please enter a name.", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void delete_npc_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Delete it from the array
                if (blacklist_npc_list.Contains(blacklist_lbx.SelectedItem.ToString()))
                {
                    int index = blacklist_npc_list.IndexOf(blacklist_lbx.SelectedItem.ToString());
                    blacklist_npc_list[index] = "0";
                }

                // Delete the selected item
                blacklist_lbx.Items.Remove(blacklist_lbx.SelectedItem);
            }
            catch { }
        }

        // PVP Btn
        private void pvp_accept_btn_Click(object sender, RoutedEventArgs e)
        {
            // Open the pvp accept tool window
            if (!PVPToolWindow.bwindow_visibility)
            {
                PVPToolWindow pvp_tools_window = new PVPToolWindow();
                pvp_tools_window.Show();
            }
        }

        private void btn_login_click(object sender, RoutedEventArgs e)
        {
            // Open the pvp accept tool window
            if (!LoginClick.bwindow_visibility)
            {
                LoginClick login_click_window = new LoginClick();
                login_click_window.Show();
            }
        }
    }
}
