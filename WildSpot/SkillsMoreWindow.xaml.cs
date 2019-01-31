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
    public partial class SkillsMoreWindow
    {
        // Options visibility timer
        DispatcherTimer visibility_timer = new DispatcherTimer();

        // Stats class
        Stats stats = new Stats();

        // Arrays for ... skills
        public string[] skill_1_conditions_array;
        public string[] skill_2_conditions_array;
        public string[] skill_3_conditions_array;
        public string[] skill_4_conditions_array;
        public string[] skill_5_conditions_array;
        public string[] skill_6_conditions_array;
        public string[] skill_7_conditions_array;
        public string[] skill_8_conditions_array;
        public string[] skill_9_conditions_array;

        public string[] key_1_conditions_array;
        public string[] key_2_conditions_array;
        public string[] key_3_conditions_array;
        public string[] key_4_conditions_array;
        public string[] key_5_conditions_array;
        public string[] key_6_conditions_array;
        public string[] key_7_conditions_array;
        public string[] key_8_conditions_array;
        public string[] key_9_conditions_array;
        public string[] key_10_conditions_array;

        // Selected Skill
        public static int skill_selected = 0;
        
        // Window showed bool
        public static bool showed = false;

        private bool change_skill_settings = false;

        public SkillsMoreWindow()
        {
            InitializeComponent();

            // Set the timer
            visibility_timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            visibility_timer.Tick += new EventHandler(visibility_timer_Tick);
            visibility_timer.Start();

            // Skills convert string to array
            skill_1_conditions_array = Properties.Settings.Default.skill_1_conditions_array.Split(',');
            skill_2_conditions_array = Properties.Settings.Default.skill_2_conditions_array.Split(',');
            skill_3_conditions_array = Properties.Settings.Default.skill_3_conditions_array.Split(',');
            skill_4_conditions_array = Properties.Settings.Default.skill_4_conditions_array.Split(',');
            skill_5_conditions_array = Properties.Settings.Default.skill_5_conditions_array.Split(',');
            skill_6_conditions_array = Properties.Settings.Default.skill_6_conditions_array.Split(',');
            skill_7_conditions_array = Properties.Settings.Default.skill_7_conditions_array.Split(',');
            skill_8_conditions_array = Properties.Settings.Default.skill_8_conditions_array.Split(',');
            skill_9_conditions_array = Properties.Settings.Default.skill_9_conditions_array.Split(',');

            key_1_conditions_array = Properties.Settings.Default.key_using_1_conditions_array.Split(',');
            key_2_conditions_array = Properties.Settings.Default.key_using_2_conditions_array.Split(',');
            key_3_conditions_array = Properties.Settings.Default.key_using_3_conditions_array.Split(',');
            key_4_conditions_array = Properties.Settings.Default.key_using_4_conditions_array.Split(',');
            key_5_conditions_array = Properties.Settings.Default.key_using_5_conditions_array.Split(',');
            key_6_conditions_array = Properties.Settings.Default.key_using_6_conditions_array.Split(',');
            key_7_conditions_array = Properties.Settings.Default.key_using_7_conditions_array.Split(',');
            key_8_conditions_array = Properties.Settings.Default.key_using_8_conditions_array.Split(',');
            key_9_conditions_array = Properties.Settings.Default.key_using_8_conditions_array.Split(',');
            key_10_conditions_array = Properties.Settings.Default.key_using_10_conditions_array.Split(',');
        }

        public void load_configs()
        {
            // Set special power lbl
            if(stats.player_classes() == 1)
            {
                special_power_lbl.Content = "Kinetic over:";
                special_power_under_lbl.Content = "Kinetic under:";
            }
            else if (stats.player_classes() == 2)
            {
                special_power_lbl.Content = "Volatility over:";
                special_power_under_lbl.Content = "Volatility under:";
            }
            else if (stats.player_classes() == 3)
            {
                special_power_lbl.Content = "Psi over:";
                special_power_under_lbl.Content = "Psi under:";
            }
            else if (stats.player_classes() == 4)
            {
                special_power_lbl.Content = "Actuators over:";
                special_power_under_lbl.Content = "Actuators under:";
            }
            else if (stats.player_classes() == 5)
            {
                special_power_lbl.Content = "Suit Power over:";
                special_power_under_lbl.Content = "Suit Power under:";
            }
            else if (stats.player_classes() == 7)
            {
                special_power_lbl.Content = "Spellpower over:";
                special_power_under_lbl.Content = "Spellpower under:";
            }

            // Conditions
            if (skill_selected == -1)
            {
                // Over conditions
                health_over_tbx.Text = Properties.Settings.Default.skill_r_health_over_cdt.ToString();
                shield_over_tbx.Text = Properties.Settings.Default.skill_r_shield_over_cdt.ToString();
                target_health_over_tbx.Text = Properties.Settings.Default.skill_r_target_health_over_cdt.ToString();
                target_shield_over_tbx.Text = Properties.Settings.Default.skill_r_target_shield_over_cdt.ToString();

                // Under conditions
                health_under_tbx.Text = Properties.Settings.Default.skill_r_health_cdt.ToString();
                shield_under_tbx.Text = Properties.Settings.Default.skill_r_shield_cdt.ToString();
                target_health_under_tbx.Text = Properties.Settings.Default.skill_r_target_health_cdt.ToString();
                target_shield_under_tbx.Text = Properties.Settings.Default.skill_r_target_shield_cdt.ToString();

                // Power
                special_power_tbx.Text = Properties.Settings.Default.skill_r_power;
                special_power_under_tbx.Text = Properties.Settings.Default.skill_r_power_under;

                // Charging
                skill_charge.IsChecked = Properties.Settings.Default.skill_2_double_click;
            }
            if (skill_selected == 0)
            {
                // Over conditions
                health_over_tbx.Text = Properties.Settings.Default.skill_1_health_over_cdt.ToString();
                shield_over_tbx.Text = Properties.Settings.Default.skill_1_shield_over_cdt.ToString();
                target_health_over_tbx.Text = Properties.Settings.Default.skill_1_target_health_over_cdt.ToString();
                target_shield_over_tbx.Text = Properties.Settings.Default.skill_1_target_shield_over_cdt.ToString();

                // Under conditions
                health_under_tbx.Text = Properties.Settings.Default.skill_1_health_cdt.ToString();
                shield_under_tbx.Text = Properties.Settings.Default.skill_1_shield_cdt.ToString();
                target_health_under_tbx.Text = Properties.Settings.Default.skill_1_target_health_cdt.ToString();
                target_shield_under_tbx.Text = Properties.Settings.Default.skill_1_target_shield_cdt.ToString();

                // Charging
                skill_charge.IsChecked = Properties.Settings.Default.skill_2_double_click;

                // Power
                special_power_tbx.Text = Properties.Settings.Default.skill_1_power;
                special_power_under_tbx.Text = Properties.Settings.Default.skill_1_power_under;

                // Repeat
                repeat_skill_tbx.Text = Properties.Settings.Default.skill_1_repeat_cdt.ToString();

                // Interrupt
                skill_interrupt_cbx.IsChecked = Properties.Settings.Default.skill_1_interrupt_enabled;
            }
            else if (skill_selected == 1)
            {
                // Over conditions
                health_over_tbx.Text = Properties.Settings.Default.skill_2_health_over_cdt.ToString();
                shield_over_tbx.Text = Properties.Settings.Default.skill_2_shield_over_cdt.ToString();
                target_health_over_tbx.Text = Properties.Settings.Default.skill_2_target_health_over_cdt.ToString();
                target_shield_over_tbx.Text = Properties.Settings.Default.skill_2_target_shield_over_cdt.ToString();

                // Under conditions
                health_under_tbx.Text = Properties.Settings.Default.skill_2_health_cdt.ToString();
                shield_under_tbx.Text = Properties.Settings.Default.skill_2_shield_cdt.ToString();
                target_health_under_tbx.Text = Properties.Settings.Default.skill_2_target_health_cdt.ToString();
                target_shield_under_tbx.Text = Properties.Settings.Default.skill_2_target_shield_cdt.ToString();

                // Power
                special_power_tbx.Text = Properties.Settings.Default.skill_2_power;
                special_power_under_tbx.Text = Properties.Settings.Default.skill_2_power_under;

                // Charging
                skill_charge.IsChecked = Properties.Settings.Default.skill_3_double_click;

                // Repeat
                repeat_skill_tbx.Text = Properties.Settings.Default.skill_2_repeat_cdt.ToString();

                // Interrupt
                skill_interrupt_cbx.IsChecked = Properties.Settings.Default.skill_2_interrupt_enabled;
            }
            else if (skill_selected == 2)
            {
                // Over conditions
                health_over_tbx.Text = Properties.Settings.Default.skill_3_health_over_cdt.ToString();
                shield_over_tbx.Text = Properties.Settings.Default.skill_3_shield_over_cdt.ToString();
                target_health_over_tbx.Text = Properties.Settings.Default.skill_3_target_health_over_cdt.ToString();
                target_shield_over_tbx.Text = Properties.Settings.Default.skill_3_target_shield_over_cdt.ToString();

                // Under conditions
                health_under_tbx.Text = Properties.Settings.Default.skill_3_health_cdt.ToString();
                shield_under_tbx.Text = Properties.Settings.Default.skill_3_shield_cdt.ToString();
                target_health_under_tbx.Text = Properties.Settings.Default.skill_3_target_health_cdt.ToString();
                target_shield_under_tbx.Text = Properties.Settings.Default.skill_3_target_shield_cdt.ToString();

                // Power
                special_power_tbx.Text = Properties.Settings.Default.skill_3_power;
                special_power_under_tbx.Text = Properties.Settings.Default.skill_3_power_under;

                // Charging
                skill_charge.IsChecked = Properties.Settings.Default.skill_4_double_click;

                // Repeat
                repeat_skill_tbx.Text = Properties.Settings.Default.skill_3_repeat_cdt.ToString();

                // Interrupt
                skill_interrupt_cbx.IsChecked = Properties.Settings.Default.skill_3_interrupt_enabled;
            }
            else if (skill_selected == 3)
            {
                // Over conditions
                health_over_tbx.Text = Properties.Settings.Default.skill_4_health_over_cdt.ToString();
                shield_over_tbx.Text = Properties.Settings.Default.skill_4_shield_over_cdt.ToString();
                target_health_over_tbx.Text = Properties.Settings.Default.skill_4_target_health_over_cdt.ToString();
                target_shield_over_tbx.Text = Properties.Settings.Default.skill_4_target_shield_over_cdt.ToString();

                // Under conditions
                health_under_tbx.Text = Properties.Settings.Default.skill_4_health_cdt.ToString();
                shield_under_tbx.Text = Properties.Settings.Default.skill_4_shield_cdt.ToString();
                target_health_under_tbx.Text = Properties.Settings.Default.skill_4_target_health_cdt.ToString();
                target_shield_under_tbx.Text = Properties.Settings.Default.skill_4_target_shield_cdt.ToString();

                // Power
                special_power_tbx.Text = Properties.Settings.Default.skill_4_power;
                special_power_under_tbx.Text = Properties.Settings.Default.skill_4_power_under;

                // Charging
                skill_charge.IsChecked = Properties.Settings.Default.skill_5_double_click;

                // Repeat
                repeat_skill_tbx.Text = Properties.Settings.Default.skill_4_repeat_cdt.ToString();

                // Interrupt
                skill_interrupt_cbx.IsChecked = Properties.Settings.Default.skill_4_interrupt_enabled;
            }
            else if (skill_selected == 4)
            {
                // Over conditions
                health_over_tbx.Text = Properties.Settings.Default.skill_5_health_over_cdt.ToString();
                shield_over_tbx.Text = Properties.Settings.Default.skill_5_shield_over_cdt.ToString();
                target_health_over_tbx.Text = Properties.Settings.Default.skill_5_target_health_over_cdt.ToString();
                target_shield_over_tbx.Text = Properties.Settings.Default.skill_5_target_shield_over_cdt.ToString();

                // Under conditions
                health_under_tbx.Text = Properties.Settings.Default.skill_5_health_cdt.ToString();
                shield_under_tbx.Text = Properties.Settings.Default.skill_5_shield_cdt.ToString();
                target_health_under_tbx.Text = Properties.Settings.Default.skill_5_target_health_cdt.ToString();
                target_shield_under_tbx.Text = Properties.Settings.Default.skill_5_target_shield_cdt.ToString();

                // Power
                special_power_tbx.Text = Properties.Settings.Default.skill_5_power;
                special_power_under_tbx.Text = Properties.Settings.Default.skill_5_power_under;

                // Charging
                skill_charge.IsChecked = Properties.Settings.Default.skill_6_double_click;

                // Repeat
                repeat_skill_tbx.Text = Properties.Settings.Default.skill_5_repeat_cdt.ToString();

                // Interrupt
                skill_interrupt_cbx.IsChecked = Properties.Settings.Default.skill_5_interrupt_enabled;
            }
            else if (skill_selected == 5)
            {
                // Over conditions
                health_over_tbx.Text = Properties.Settings.Default.skill_6_health_over_cdt.ToString();
                shield_over_tbx.Text = Properties.Settings.Default.skill_6_shield_over_cdt.ToString();
                target_health_over_tbx.Text = Properties.Settings.Default.skill_6_target_health_over_cdt.ToString();
                target_shield_over_tbx.Text = Properties.Settings.Default.skill_6_target_shield_over_cdt.ToString();

                // Under conditions
                health_under_tbx.Text = Properties.Settings.Default.skill_6_health_cdt.ToString();
                shield_under_tbx.Text = Properties.Settings.Default.skill_6_shield_cdt.ToString();
                target_health_under_tbx.Text = Properties.Settings.Default.skill_6_target_health_cdt.ToString();
                target_shield_under_tbx.Text = Properties.Settings.Default.skill_6_target_shield_cdt.ToString();

                // Power
                special_power_tbx.Text = Properties.Settings.Default.skill_6_power;
                special_power_under_tbx.Text = Properties.Settings.Default.skill_6_power_under;

                // Charging
                skill_charge.IsChecked = Properties.Settings.Default.skill_7_double_click;

                // Repeat
                repeat_skill_tbx.Text = Properties.Settings.Default.skill_6_repeat_cdt.ToString();

                // Interrupt
                skill_interrupt_cbx.IsChecked = Properties.Settings.Default.skill_6_interrupt_enabled;
            }
            else if (skill_selected == 6)
            {
                // Over conditions
                health_over_tbx.Text = Properties.Settings.Default.skill_7_health_over_cdt.ToString();
                shield_over_tbx.Text = Properties.Settings.Default.skill_7_shield_over_cdt.ToString();
                target_health_over_tbx.Text = Properties.Settings.Default.skill_7_target_health_over_cdt.ToString();
                target_shield_over_tbx.Text = Properties.Settings.Default.skill_7_target_shield_over_cdt.ToString();

                // Under conditions
                health_under_tbx.Text = Properties.Settings.Default.skill_7_health_cdt.ToString();
                shield_under_tbx.Text = Properties.Settings.Default.skill_7_shield_cdt.ToString();
                target_health_under_tbx.Text = Properties.Settings.Default.skill_7_target_health_cdt.ToString();
                target_shield_under_tbx.Text = Properties.Settings.Default.skill_7_target_shield_cdt.ToString();

                // Power
                special_power_tbx.Text = Properties.Settings.Default.skill_7_power;
                special_power_under_tbx.Text = Properties.Settings.Default.skill_7_power_under;

                // Charging
                skill_charge.IsChecked = Properties.Settings.Default.skill_8_double_click;

                // Repeat
                repeat_skill_tbx.Text = Properties.Settings.Default.skill_7_repeat_cdt.ToString();

                // Interrupt
                skill_interrupt_cbx.IsChecked = Properties.Settings.Default.skill_7_interrupt_enabled;
            }
            else if (skill_selected == 7)
            {
                // Over conditions
                health_over_tbx.Text = Properties.Settings.Default.skill_8_health_over_cdt.ToString();
                shield_over_tbx.Text = Properties.Settings.Default.skill_8_shield_over_cdt.ToString();
                target_health_over_tbx.Text = Properties.Settings.Default.skill_8_target_health_over_cdt.ToString();
                target_shield_over_tbx.Text = Properties.Settings.Default.skill_8_target_shield_over_cdt.ToString();

                // Under conditions
                health_under_tbx.Text = Properties.Settings.Default.skill_8_health_cdt.ToString();
                shield_under_tbx.Text = Properties.Settings.Default.skill_8_shield_cdt.ToString();
                target_health_under_tbx.Text = Properties.Settings.Default.skill_8_target_health_cdt.ToString();
                target_shield_under_tbx.Text = Properties.Settings.Default.skill_8_target_shield_cdt.ToString();

                // Power
                special_power_tbx.Text = Properties.Settings.Default.skill_8_power;
                special_power_under_tbx.Text = Properties.Settings.Default.skill_8_power_under;

                // Charging
                skill_charge.IsChecked = Properties.Settings.Default.skill_9_double_click;

                // Repeat
                repeat_skill_tbx.Text = Properties.Settings.Default.skill_8_repeat_cdt.ToString();

                // Interrupt
                skill_interrupt_cbx.IsChecked = Properties.Settings.Default.skill_8_interrupt_enabled;
            }
            else if (skill_selected == 8) // Below only key using skills
            {
                // Over conditions
                health_over_tbx.Text = Properties.Settings.Default.key_using_1_health_over_cdt.ToString();
                shield_over_tbx.Text = Properties.Settings.Default.key_using_1_shield_over_cdt.ToString();
                target_health_over_tbx.Text = Properties.Settings.Default.key_using_1_target_health_over_cdt.ToString();
                target_shield_over_tbx.Text = Properties.Settings.Default.key_using_1_target_shield_over_cdt.ToString();

                // Under conditions
                health_under_tbx.Text = Properties.Settings.Default.key_using_1_health_cdt.ToString();
                shield_under_tbx.Text = Properties.Settings.Default.key_using_1_shield_cdt.ToString();
                target_health_under_tbx.Text = Properties.Settings.Default.key_using_1_target_health_cdt.ToString();
                target_shield_under_tbx.Text = Properties.Settings.Default.key_using_1_target_shield_cdt.ToString();

                // Power
                special_power_tbx.Text = Properties.Settings.Default.key_using_1_power;
                special_power_under_tbx.Text = Properties.Settings.Default.key_using_1_power_under;

                use_infight_cbx.IsChecked = Properties.Settings.Default.key_using_1_infight;
            }
            else if (skill_selected == 9)
            {
                // Over conditions
                health_over_tbx.Text = Properties.Settings.Default.key_using_2_health_over_cdt.ToString();
                shield_over_tbx.Text = Properties.Settings.Default.key_using_2_shield_over_cdt.ToString();
                target_health_over_tbx.Text = Properties.Settings.Default.key_using_2_target_health_over_cdt.ToString();
                target_shield_over_tbx.Text = Properties.Settings.Default.key_using_2_target_shield_over_cdt.ToString();

                // Under conditions
                health_under_tbx.Text = Properties.Settings.Default.key_using_2_health_cdt.ToString();
                shield_under_tbx.Text = Properties.Settings.Default.key_using_2_shield_cdt.ToString();
                target_health_under_tbx.Text = Properties.Settings.Default.key_using_2_target_health_cdt.ToString();
                target_shield_under_tbx.Text = Properties.Settings.Default.key_using_2_target_shield_cdt.ToString();

                // Power
                special_power_tbx.Text = Properties.Settings.Default.key_using_2_power;
                special_power_under_tbx.Text = Properties.Settings.Default.key_using_2_power_under;

                use_infight_cbx.IsChecked = Properties.Settings.Default.key_using_2_infight;
            }
            else if (skill_selected == 10)
            {
                // Over conditions
                health_over_tbx.Text = Properties.Settings.Default.key_using_3_health_over_cdt.ToString();
                shield_over_tbx.Text = Properties.Settings.Default.key_using_3_shield_over_cdt.ToString();
                target_health_over_tbx.Text = Properties.Settings.Default.key_using_3_target_health_over_cdt.ToString();
                target_shield_over_tbx.Text = Properties.Settings.Default.key_using_3_target_shield_over_cdt.ToString();

                // Under conditions
                health_under_tbx.Text = Properties.Settings.Default.key_using_3_health_cdt.ToString();
                shield_under_tbx.Text = Properties.Settings.Default.key_using_3_shield_cdt.ToString();
                target_health_under_tbx.Text = Properties.Settings.Default.key_using_3_target_health_cdt.ToString();
                target_shield_under_tbx.Text = Properties.Settings.Default.key_using_3_target_shield_cdt.ToString();

                // Power
                special_power_tbx.Text = Properties.Settings.Default.key_using_3_power;
                special_power_under_tbx.Text = Properties.Settings.Default.key_using_3_power_under;

                use_infight_cbx.IsChecked = Properties.Settings.Default.key_using_3_infight;
            }
            else if (skill_selected == 11)
            {
                // Over conditions
                health_over_tbx.Text = Properties.Settings.Default.key_using_4_health_over_cdt.ToString();
                shield_over_tbx.Text = Properties.Settings.Default.key_using_4_shield_over_cdt.ToString();
                target_health_over_tbx.Text = Properties.Settings.Default.key_using_4_target_health_over_cdt.ToString();
                target_shield_over_tbx.Text = Properties.Settings.Default.key_using_4_target_shield_over_cdt.ToString();

                // Under conditions
                health_under_tbx.Text = Properties.Settings.Default.key_using_4_health_cdt.ToString();
                shield_under_tbx.Text = Properties.Settings.Default.key_using_4_shield_cdt.ToString();
                target_health_under_tbx.Text = Properties.Settings.Default.key_using_4_target_health_cdt.ToString();
                target_shield_under_tbx.Text = Properties.Settings.Default.key_using_4_target_shield_cdt.ToString();

                // Power
                special_power_tbx.Text = Properties.Settings.Default.key_using_4_power;
                special_power_under_tbx.Text = Properties.Settings.Default.key_using_4_power_under;

                use_infight_cbx.IsChecked = Properties.Settings.Default.key_using_4_infight;
            }
            else if (skill_selected == 12)
            {
                // Over conditions
                health_over_tbx.Text = Properties.Settings.Default.key_using_5_health_over_cdt.ToString();
                shield_over_tbx.Text = Properties.Settings.Default.key_using_5_shield_over_cdt.ToString();
                target_health_over_tbx.Text = Properties.Settings.Default.key_using_5_target_health_over_cdt.ToString();
                target_shield_over_tbx.Text = Properties.Settings.Default.key_using_5_target_shield_over_cdt.ToString();

                // Under conditions
                health_under_tbx.Text = Properties.Settings.Default.key_using_5_health_cdt.ToString();
                shield_under_tbx.Text = Properties.Settings.Default.key_using_5_shield_cdt.ToString();
                target_health_under_tbx.Text = Properties.Settings.Default.key_using_5_target_health_cdt.ToString();
                target_shield_under_tbx.Text = Properties.Settings.Default.key_using_5_target_shield_cdt.ToString();

                // Power
                special_power_tbx.Text = Properties.Settings.Default.key_using_5_power;
                special_power_under_tbx.Text = Properties.Settings.Default.key_using_5_power_under;

                use_infight_cbx.IsChecked = Properties.Settings.Default.key_using_5_infight;
            }
            else if (skill_selected == 13)
            {
                // Over conditions
                health_over_tbx.Text = Properties.Settings.Default.key_using_6_health_over_cdt.ToString();
                shield_over_tbx.Text = Properties.Settings.Default.key_using_6_shield_over_cdt.ToString();
                target_health_over_tbx.Text = Properties.Settings.Default.key_using_6_target_health_over_cdt.ToString();
                target_shield_over_tbx.Text = Properties.Settings.Default.key_using_6_target_shield_over_cdt.ToString();

                // Under conditions
                health_under_tbx.Text = Properties.Settings.Default.key_using_6_health_cdt.ToString();
                shield_under_tbx.Text = Properties.Settings.Default.key_using_6_shield_cdt.ToString();
                target_health_under_tbx.Text = Properties.Settings.Default.key_using_6_target_health_cdt.ToString();
                target_shield_under_tbx.Text = Properties.Settings.Default.key_using_6_target_shield_cdt.ToString();

                // Power
                special_power_tbx.Text = Properties.Settings.Default.key_using_6_power;
                special_power_under_tbx.Text = Properties.Settings.Default.key_using_6_power_under;

                use_infight_cbx.IsChecked = Properties.Settings.Default.key_using_6_infight;
            }
            else if (skill_selected == 14)
            {
                // Over conditions
                health_over_tbx.Text = Properties.Settings.Default.key_using_7_health_over_cdt.ToString();
                shield_over_tbx.Text = Properties.Settings.Default.key_using_7_shield_over_cdt.ToString();
                target_health_over_tbx.Text = Properties.Settings.Default.key_using_7_target_health_over_cdt.ToString();
                target_shield_over_tbx.Text = Properties.Settings.Default.key_using_7_target_shield_over_cdt.ToString();

                // Under conditions
                health_under_tbx.Text = Properties.Settings.Default.key_using_7_health_cdt.ToString();
                shield_under_tbx.Text = Properties.Settings.Default.key_using_7_shield_cdt.ToString();
                target_health_under_tbx.Text = Properties.Settings.Default.key_using_7_target_health_cdt.ToString();
                target_shield_under_tbx.Text = Properties.Settings.Default.key_using_7_target_shield_cdt.ToString();

                // Power
                special_power_tbx.Text = Properties.Settings.Default.key_using_7_power;
                special_power_under_tbx.Text = Properties.Settings.Default.key_using_7_power_under;

                use_infight_cbx.IsChecked = Properties.Settings.Default.key_using_7_infight;
            }
            else if (skill_selected == 15)
            {
                // Over conditions
                health_over_tbx.Text = Properties.Settings.Default.key_using_8_health_over_cdt.ToString();
                shield_over_tbx.Text = Properties.Settings.Default.key_using_8_shield_over_cdt.ToString();
                target_health_over_tbx.Text = Properties.Settings.Default.key_using_8_target_health_over_cdt.ToString();
                target_shield_over_tbx.Text = Properties.Settings.Default.key_using_8_target_shield_over_cdt.ToString();

                // Under conditions
                health_under_tbx.Text = Properties.Settings.Default.key_using_8_health_cdt.ToString();
                shield_under_tbx.Text = Properties.Settings.Default.key_using_8_shield_cdt.ToString();
                target_health_under_tbx.Text = Properties.Settings.Default.key_using_8_target_health_cdt.ToString();
                target_shield_under_tbx.Text = Properties.Settings.Default.key_using_8_target_shield_cdt.ToString();

                // Power
                special_power_tbx.Text = Properties.Settings.Default.key_using_8_power;
                special_power_under_tbx.Text = Properties.Settings.Default.key_using_8_power_under;

                use_infight_cbx.IsChecked = Properties.Settings.Default.key_using_8_infight;
            }
            else if (skill_selected == 16)
            {
                // Over conditions
                health_over_tbx.Text = Properties.Settings.Default.key_using_9_health_over_cdt.ToString();
                shield_over_tbx.Text = Properties.Settings.Default.key_using_9_shield_over_cdt.ToString();
                target_health_over_tbx.Text = Properties.Settings.Default.key_using_9_target_health_over_cdt.ToString();
                target_shield_over_tbx.Text = Properties.Settings.Default.key_using_9_target_shield_over_cdt.ToString();

                // Under conditions
                health_under_tbx.Text = Properties.Settings.Default.key_using_9_health_cdt.ToString();
                shield_under_tbx.Text = Properties.Settings.Default.key_using_9_shield_cdt.ToString();
                target_health_under_tbx.Text = Properties.Settings.Default.key_using_9_target_health_cdt.ToString();
                target_shield_under_tbx.Text = Properties.Settings.Default.key_using_9_target_shield_cdt.ToString();

                // Power
                special_power_tbx.Text = Properties.Settings.Default.key_using_9_power;
                special_power_under_tbx.Text = Properties.Settings.Default.key_using_9_power_under;

                use_infight_cbx.IsChecked = Properties.Settings.Default.key_using_9_infight;
            }
            else if (skill_selected == 17)
            {
                // Over conditions
                health_over_tbx.Text = Properties.Settings.Default.key_using_10_health_over_cdt.ToString();
                shield_over_tbx.Text = Properties.Settings.Default.key_using_10_shield_over_cdt.ToString();
                target_health_over_tbx.Text = Properties.Settings.Default.key_using_10_target_health_over_cdt.ToString();
                target_shield_over_tbx.Text = Properties.Settings.Default.key_using_10_target_shield_over_cdt.ToString();

                // Under conditions
                health_under_tbx.Text = Properties.Settings.Default.key_using_10_health_cdt.ToString();
                shield_under_tbx.Text = Properties.Settings.Default.key_using_10_shield_cdt.ToString();
                target_health_under_tbx.Text = Properties.Settings.Default.key_using_10_target_health_cdt.ToString();
                target_shield_under_tbx.Text = Properties.Settings.Default.key_using_10_target_shield_cdt.ToString();

                // Power
                special_power_tbx.Text = Properties.Settings.Default.key_using_10_power;
                special_power_under_tbx.Text = Properties.Settings.Default.key_using_10_power_under;

                use_infight_cbx.IsChecked = Properties.Settings.Default.key_using_10_infight;
            }
        }

        // Visibility timer for options
        private void visibility_timer_Tick(object sender, EventArgs e)
        {
            while (!change_skill_settings)
            {
                load_configs();

                change_skill_settings = true;
            }

            // Visibility
            if (skill_selected < 8)
            {
                if (skill_selected != -1)
                {
                    skill_charge.Visibility = Visibility.Visible;
                    skill_interrupt_cbx.Visibility = Visibility.Visible;
                    repeat_skill_tbx.IsEnabled = true;
                }
                else
                {
                    skill_charge.Visibility = Visibility.Collapsed;
                    skill_interrupt_cbx.Visibility = Visibility.Collapsed;
                    repeat_skill_tbx.IsEnabled = false;
                }

                use_infight_cbx.Visibility = Visibility.Collapsed;
            }
            else
            {
                repeat_skill_tbx.IsEnabled = false;
                skill_charge.Visibility = Visibility.Collapsed;
                skill_interrupt_cbx.Visibility = Visibility.Collapsed;
                use_infight_cbx.Visibility = Visibility.Visible;
            }
        }

        // Function for convert an array into a string
        static string ConvertStringArrayToStringJoin(string[] array)
        {
            //
            // Use string Join to concatenate the string elements.
            //
            string result = string.Join(",", array);
            return result;
        }

        // Check enteries
        private void check_enteries()
        {
            if(health_over_tbx.Text == "")
            {
                health_over_tbx.Text = "0";
            }

            if(health_under_tbx.Text == "")
            {
                health_under_tbx.Text = "0";
            }

            if(shield_over_tbx.Text == "")
            {
                shield_over_tbx.Text = "0";
            }

            if(shield_under_tbx.Text == "")
            {
                shield_under_tbx.Text = "0";
            }

            if(target_health_over_tbx.Text == "")
            {
                target_health_over_tbx.Text = "0";
            }

            if(target_health_under_tbx.Text == "")
            {
                target_health_under_tbx.Text = "0";
            }

            if(target_shield_over_tbx.Text == "")
            {
                target_shield_over_tbx.Text = "0";
            }

            if(target_shield_under_tbx.Text == "")
            {
                target_shield_under_tbx.Text = "0";
            }

            if(special_power_tbx.Text == "")
            {
                special_power_tbx.Text = "0";
            }

            if (special_power_under_tbx.Text == "")
            {
                special_power_under_tbx.Text = "0";
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Check errors
            check_enteries();

            // Save Settings
            #region skills
            // Conditions
            if (skill_selected == -1)
            {
                // Over conditions
                Properties.Settings.Default.skill_r_health_over_cdt = Convert.ToInt32(health_over_tbx.Text);
                Properties.Settings.Default.skill_r_shield_over_cdt = Convert.ToInt32(shield_over_tbx.Text);
                Properties.Settings.Default.skill_r_target_health_over_cdt = Convert.ToInt32(target_health_over_tbx.Text);
                Properties.Settings.Default.skill_r_target_shield_over_cdt = Convert.ToInt32(target_shield_over_tbx.Text);

                // Under conditions
                Properties.Settings.Default.skill_r_health_cdt = Convert.ToInt32(health_under_tbx.Text);
                Properties.Settings.Default.skill_r_shield_cdt = Convert.ToInt32(shield_under_tbx.Text);
                Properties.Settings.Default.skill_r_target_health_cdt = Convert.ToInt32(target_health_under_tbx.Text);
                Properties.Settings.Default.skill_r_target_shield_cdt = Convert.ToInt32(target_shield_under_tbx.Text);

                // Conditions arraay
                if(health_over_tbx.Text != "0")
                {
                    for(int i = 0; i < skill_1_conditions_array.Length; i++)
                    {
                        if (skill_1_conditions_array[i] == "0" && !skill_1_conditions_array.Contains("1"))
                        {
                            skill_1_conditions_array[i] = "1";
                        }
                    }
                }
                 else
                {
                    if (skill_1_conditions_array.Contains("1"))
                    {
                        for (int i = 0; i < skill_1_conditions_array.Length; i++)
                        {
                            if (skill_1_conditions_array[i] == "1")
                            {
                                skill_1_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_1_conditions_array.Length; i++)
                    {
                        if (skill_1_conditions_array[i] == "0" && !skill_1_conditions_array.Contains("2"))
                        {
                            skill_1_conditions_array[i] = "2";
                        }
                    }
                }
                 else
                {
                    if (skill_1_conditions_array.Contains("2"))
                    {
                        for (int i = 0; i < skill_1_conditions_array.Length; i++)
                        {
                            if (skill_1_conditions_array[i] == "2")
                            {
                                skill_1_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_1_conditions_array.Length; i++)
                    {
                        if (skill_1_conditions_array[i] == "0" && !skill_1_conditions_array.Contains("3"))
                        {
                            skill_1_conditions_array[i] = "3";
                        }
                    }
                }
                 else
                {
                    if (skill_1_conditions_array.Contains("3"))
                    {
                        for (int i = 0; i < skill_1_conditions_array.Length; i++)
                        {
                            if (skill_1_conditions_array[i] == "3")
                            {
                                skill_1_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_1_conditions_array.Length; i++)
                    {
                        if (skill_1_conditions_array[i] == "0" && !skill_1_conditions_array.Contains("4"))
                        {
                            skill_1_conditions_array[i] = "4";
                        }
                    }
                }
                 else
                {
                    if (skill_1_conditions_array.Contains("4"))
                    {
                        for (int i = 0; i < skill_1_conditions_array.Length; i++)
                        {
                            if (skill_1_conditions_array[i] == "4")
                            {
                                skill_1_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_1_conditions_array.Length; i++)
                    {
                        if (skill_1_conditions_array[i] == "0" && !skill_1_conditions_array.Contains("5"))
                        {
                            skill_1_conditions_array[i] = "5";
                        }
                    }
                }
                 else
                {
                    if (skill_1_conditions_array.Contains("5"))
                    {
                        for (int i = 0; i < skill_1_conditions_array.Length; i++)
                        {
                            if (skill_1_conditions_array[i] == "5")
                            {
                                skill_1_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_1_conditions_array.Length; i++)
                    {
                        if (skill_1_conditions_array[i] == "0" && !skill_1_conditions_array.Contains("6"))
                        {
                            skill_1_conditions_array[i] = "6";
                        }
                    }
                }
                 else
                {
                    if (skill_1_conditions_array.Contains("6"))
                    {
                        for (int i = 0; i < skill_1_conditions_array.Length; i++)
                        {
                            if (skill_1_conditions_array[i] == "6")
                            {
                                skill_1_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_1_conditions_array.Length; i++)
                    {
                        if (skill_1_conditions_array[i] == "0" && !skill_1_conditions_array.Contains("7"))
                        {
                            skill_1_conditions_array[i] = "7";
                        }
                    }
                }
                 else
                {
                    if (skill_1_conditions_array.Contains("7"))
                    {
                        for (int i = 0; i < skill_1_conditions_array.Length; i++)
                        {
                            if (skill_1_conditions_array[i] == "7")
                            {
                                skill_1_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_1_conditions_array.Length; i++)
                    {
                        if (skill_1_conditions_array[i] == "0" && !skill_1_conditions_array.Contains("8"))
                        {
                            skill_1_conditions_array[i] = "8";
                        }
                    }
                }
                else
                {
                    if (skill_1_conditions_array.Contains("8"))
                    {
                        for (int i = 0; i < skill_1_conditions_array.Length; i++)
                        {
                            if (skill_1_conditions_array[i] == "8")
                            {
                                skill_1_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_1_conditions_array.Length; i++)
                    {
                        if (skill_1_conditions_array[i] == "0" && !skill_1_conditions_array.Contains("9"))
                        {
                            skill_1_conditions_array[i] = "9";
                        }
                    }
                }
                else
                {
                    if (skill_1_conditions_array.Contains("9"))
                    {
                        for (int i = 0; i < skill_1_conditions_array.Length; i++)
                        {
                            if (skill_1_conditions_array[i] == "9")
                            {
                                skill_1_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_1_conditions_array.Length; i++)
                    {
                        if (skill_1_conditions_array[i] == "0" && !skill_1_conditions_array.Contains("10"))
                        {
                            skill_1_conditions_array[i] = "10";
                        }
                    }
                }
                else
                {
                    if (skill_1_conditions_array.Contains("10"))
                    {
                        for (int i = 0; i < skill_1_conditions_array.Length; i++)
                        {
                            if (skill_1_conditions_array[i] == "10")
                            {
                                skill_1_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                Properties.Settings.Default.skill_r_power = special_power_tbx.Text;
                Properties.Settings.Default.skill_r_power_under = special_power_under_tbx.Text;

                Properties.Settings.Default.skill_1_conditions_array = ConvertStringArrayToStringJoin(skill_1_conditions_array);
            }
            else if (skill_selected == 0)
            {
               // Over conditions
               Properties.Settings.Default.skill_1_health_over_cdt = Convert.ToInt32(health_over_tbx.Text);
               Properties.Settings.Default.skill_1_shield_over_cdt = Convert.ToInt32(shield_over_tbx.Text);
               Properties.Settings.Default.skill_1_target_health_over_cdt = Convert.ToInt32(target_health_over_tbx.Text);
               Properties.Settings.Default.skill_1_target_shield_over_cdt = Convert.ToInt32(target_shield_over_tbx.Text);

               // Under conditions
               Properties.Settings.Default.skill_1_health_cdt = Convert.ToInt32(health_under_tbx.Text);
               Properties.Settings.Default.skill_1_shield_cdt = Convert.ToInt32(shield_under_tbx.Text);
               Properties.Settings.Default.skill_1_target_health_cdt = Convert.ToInt32(target_health_under_tbx.Text);
               Properties.Settings.Default.skill_1_target_shield_cdt = Convert.ToInt32(target_shield_under_tbx.Text);

               // Charging
               Properties.Settings.Default.skill_2_double_click = skill_charge.IsChecked.Value;

               // Repeat
               Properties.Settings.Default.skill_1_repeat_cdt = Convert.ToInt32(repeat_skill_tbx.Text);

               // Interrupt
               Properties.Settings.Default.skill_1_interrupt_enabled = skill_interrupt_cbx.IsChecked.Value;

               if (health_over_tbx.Text != "0")
               {
                   for (int i = 0; i < skill_2_conditions_array.Length; i++)
                   {
                       if (skill_2_conditions_array[i] == "0" && !skill_2_conditions_array.Contains("1"))
                       {
                           skill_2_conditions_array[i] = "1";
                       }
                   }
               }
                else
                {
                    if (skill_2_conditions_array.Contains("1"))
                    {
                        for (int i = 0; i < skill_2_conditions_array.Length; i++)
                        {
                            if (skill_2_conditions_array[i] == "1")
                            {
                                skill_2_conditions_array[i] = "0";
                            }
                        }
                    }
                }

               if (health_under_tbx.Text != "0")
               {
                   for (int i = 0; i < skill_2_conditions_array.Length; i++)
                   {
                       if (skill_2_conditions_array[i] == "0" && !skill_2_conditions_array.Contains("2"))
                       {
                           skill_2_conditions_array[i] = "2";
                       }
                   }
               }
                else
                {
                    if (skill_2_conditions_array.Contains("2"))
                    {
                        for (int i = 0; i < skill_2_conditions_array.Length; i++)
                        {
                            if (skill_2_conditions_array[i] == "2")
                            {
                                skill_2_conditions_array[i] = "0";
                            }
                        }
                    }
                }

               if (shield_over_tbx.Text != "0")
               {
                   for (int i = 0; i < skill_2_conditions_array.Length; i++)
                   {
                       if (skill_2_conditions_array[i] == "0" && !skill_2_conditions_array.Contains("3"))
                       {
                           skill_2_conditions_array[i] = "3";
                       }
                   }
               }
                else
                {
                    if (skill_2_conditions_array.Contains("3"))
                    {
                        for (int i = 0; i < skill_2_conditions_array.Length; i++)
                        {
                            if (skill_2_conditions_array[i] == "3")
                            {
                                skill_2_conditions_array[i] = "0";
                            }
                        }
                    }
                }

               if (shield_under_tbx.Text != "0")
               {
                   for (int i = 0; i < skill_2_conditions_array.Length; i++)
                   {
                       if (skill_2_conditions_array[i] == "0" && !skill_2_conditions_array.Contains("4"))
                       {
                           skill_2_conditions_array[i] = "4";
                       }
                   }
               }
                else
                {
                    if (skill_2_conditions_array.Contains("4"))
                    {
                        for (int i = 0; i < skill_2_conditions_array.Length; i++)
                        {
                            if (skill_2_conditions_array[i] == "4")
                            {
                                skill_2_conditions_array[i] = "0";
                            }
                        }
                    }
                }

               if (target_health_over_tbx.Text != "0")
               {
                   for (int i = 0; i < skill_2_conditions_array.Length; i++)
                   {
                       if (skill_2_conditions_array[i] == "0" && !skill_2_conditions_array.Contains("5"))
                       {
                           skill_2_conditions_array[i] = "5";
                       }
                   }
               }
                else
                {
                    if (skill_2_conditions_array.Contains("5"))
                    {
                        for (int i = 0; i < skill_2_conditions_array.Length; i++)
                        {
                            if (skill_2_conditions_array[i] == "5")
                            {
                                skill_2_conditions_array[i] = "0";
                            }
                        }
                    }
                }

               if (target_health_under_tbx.Text != "0")
               {
                   for (int i = 0; i < skill_2_conditions_array.Length; i++)
                   {
                       if (skill_2_conditions_array[i] == "0" && !skill_2_conditions_array.Contains("6"))
                       {
                           skill_2_conditions_array[i] = "6";
                       }
                   }
               }
                else
                {
                    if (skill_2_conditions_array.Contains("6"))
                    {
                        for (int i = 0; i < skill_2_conditions_array.Length; i++)
                        {
                            if (skill_2_conditions_array[i] == "6")
                            {
                                skill_2_conditions_array[i] = "0";
                            }
                        }
                    }
                }

               if (target_shield_over_tbx.Text != "0")
               {
                   for (int i = 0; i < skill_2_conditions_array.Length; i++)
                   {
                       if (skill_2_conditions_array[i] == "0" && !skill_2_conditions_array.Contains("7"))
                       {
                           skill_2_conditions_array[i] = "7";
                       }
                   }
               }
                else
                {
                    if (skill_2_conditions_array.Contains("7"))
                    {
                        for (int i = 0; i < skill_2_conditions_array.Length; i++)
                        {
                            if (skill_2_conditions_array[i] == "7")
                            {
                                skill_2_conditions_array[i] = "0";
                            }
                        }
                    }
                }

               if (target_shield_under_tbx.Text != "0")
               {
                   for (int i = 0; i < skill_2_conditions_array.Length; i++)
                   {
                       if (skill_2_conditions_array[i] == "0" && !skill_2_conditions_array.Contains("8"))
                       {
                           skill_2_conditions_array[i] = "8";
                       }
                   }
               }
                else
                {
                    if (skill_2_conditions_array.Contains("8"))
                    {
                        for (int i = 0; i < skill_2_conditions_array.Length; i++)
                        {
                            if (skill_2_conditions_array[i] == "8")
                            {
                                skill_2_conditions_array[i] = "0";
                            }
                        }
                    }
                }

               if (special_power_tbx.Text != "0")
               {
                   for (int i = 0; i < skill_2_conditions_array.Length; i++)
                   {
                       if (skill_2_conditions_array[i] == "0" && !skill_2_conditions_array.Contains("9"))
                       {
                           skill_2_conditions_array[i] = "9";
                       }
                   }
               }
               else
               {
                   if (skill_2_conditions_array.Contains("9"))
                   {
                       for (int i = 0; i < skill_2_conditions_array.Length; i++)
                       {
                           if (skill_2_conditions_array[i] == "9")
                           {
                               skill_2_conditions_array[i] = "0";
                           }
                       }
                   }
               }

               if (special_power_under_tbx.Text != "0")
               {
                   for (int i = 0; i < skill_2_conditions_array.Length; i++)
                   {
                       if (skill_2_conditions_array[i] == "0" && !skill_2_conditions_array.Contains("10"))
                       {
                           skill_2_conditions_array[i] = "10";
                       }
                   }
               }
               else
               {
                   if (skill_2_conditions_array.Contains("10"))
                   {
                       for (int i = 0; i < skill_2_conditions_array.Length; i++)
                       {
                           if (skill_2_conditions_array[i] == "10")
                           {
                               skill_2_conditions_array[i] = "0";
                           }
                       }
                   }
               }

               Properties.Settings.Default.skill_1_power = special_power_tbx.Text;
               Properties.Settings.Default.skill_1_power_under = special_power_under_tbx.Text;

               Properties.Settings.Default.skill_2_conditions_array = ConvertStringArrayToStringJoin(skill_2_conditions_array);
            }
            else if (skill_selected == 1)
            {
                // Over conditions
                Properties.Settings.Default.skill_2_health_over_cdt = Convert.ToInt32(health_over_tbx.Text);
                Properties.Settings.Default.skill_2_shield_over_cdt = Convert.ToInt32(shield_over_tbx.Text);
                Properties.Settings.Default.skill_2_target_health_over_cdt = Convert.ToInt32(target_health_over_tbx.Text);
                Properties.Settings.Default.skill_2_target_shield_over_cdt = Convert.ToInt32(target_shield_over_tbx.Text);

                // Under conditions
                Properties.Settings.Default.skill_2_health_cdt = Convert.ToInt32(health_under_tbx.Text);
                Properties.Settings.Default.skill_2_shield_cdt = Convert.ToInt32(shield_under_tbx.Text);
                Properties.Settings.Default.skill_2_target_health_cdt = Convert.ToInt32(target_health_under_tbx.Text);
                Properties.Settings.Default.skill_2_target_shield_cdt = Convert.ToInt32(target_shield_under_tbx.Text);

                // Charging
                Properties.Settings.Default.skill_3_double_click = skill_charge.IsChecked.Value;

                // Repeat
                Properties.Settings.Default.skill_2_repeat_cdt = Convert.ToInt32(repeat_skill_tbx.Text);

                // Interrupt
                Properties.Settings.Default.skill_2_interrupt_enabled = skill_interrupt_cbx.IsChecked.Value;

                if (health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_3_conditions_array.Length; i++)
                    {
                        if (skill_3_conditions_array[i] == "0" && !skill_3_conditions_array.Contains("1"))
                        {
                            skill_3_conditions_array[i] = "1";
                        }
                    }
                }
                else
                {
                    if (skill_3_conditions_array.Contains("1"))
                    {
                        for (int i = 0; i < skill_3_conditions_array.Length; i++)
                        {
                            if (skill_3_conditions_array[i] == "1")
                            {
                                skill_3_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_3_conditions_array.Length; i++)
                    {
                        if (skill_3_conditions_array[i] == "0" && !skill_3_conditions_array.Contains("2"))
                        {
                            skill_3_conditions_array[i] = "2";
                        }
                    }
                }
                else
                {
                    if (skill_3_conditions_array.Contains("2"))
                    {
                        for (int i = 0; i < skill_3_conditions_array.Length; i++)
                        {
                            if (skill_3_conditions_array[i] == "2")
                            {
                                skill_3_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_3_conditions_array.Length; i++)
                    {
                        if (skill_3_conditions_array[i] == "0" && !skill_3_conditions_array.Contains("3"))
                        {
                            skill_3_conditions_array[i] = "3";
                        }
                    }
                }
                else
                {
                    if (skill_3_conditions_array.Contains("3"))
                    {
                        for (int i = 0; i < skill_3_conditions_array.Length; i++)
                        {
                            if (skill_3_conditions_array[i] == "3")
                            {
                                skill_3_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_3_conditions_array.Length; i++)
                    {
                        if (skill_3_conditions_array[i] == "0" && !skill_3_conditions_array.Contains("4"))
                        {
                            skill_3_conditions_array[i] = "4";
                        }
                    }
                }
                else
                {
                    if (skill_3_conditions_array.Contains("4"))
                    {
                        for (int i = 0; i < skill_3_conditions_array.Length; i++)
                        {
                            if (skill_3_conditions_array[i] == "4")
                            {
                                skill_3_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_3_conditions_array.Length; i++)
                    {
                        if (skill_3_conditions_array[i] == "0" && !skill_3_conditions_array.Contains("5"))
                        {
                            skill_3_conditions_array[i] = "5";
                        }
                    }
                }
                else
                {
                    if (skill_3_conditions_array.Contains("5"))
                    {
                        for (int i = 0; i < skill_3_conditions_array.Length; i++)
                        {
                            if (skill_3_conditions_array[i] == "5")
                            {
                                skill_3_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_3_conditions_array.Length; i++)
                    {
                        if (skill_3_conditions_array[i] == "0" && !skill_3_conditions_array.Contains("6"))
                        {
                            skill_3_conditions_array[i] = "6";
                        }
                    }
                }
                else
                {
                    if (skill_3_conditions_array.Contains("6"))
                    {
                        for (int i = 0; i < skill_3_conditions_array.Length; i++)
                        {
                            if (skill_3_conditions_array[i] == "6")
                            {
                                skill_3_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_3_conditions_array.Length; i++)
                    {
                        if (skill_3_conditions_array[i] == "0" && !skill_3_conditions_array.Contains("7"))
                        {
                            skill_3_conditions_array[i] = "7";
                        }
                    }
                }
                else
                {
                    if (skill_3_conditions_array.Contains("7"))
                    {
                        for (int i = 0; i < skill_3_conditions_array.Length; i++)
                        {
                            if (skill_3_conditions_array[i] == "7")
                            {
                                skill_3_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_3_conditions_array.Length; i++)
                    {
                        if (skill_3_conditions_array[i] == "0" && !skill_3_conditions_array.Contains("8"))
                        {
                            skill_3_conditions_array[i] = "8";
                        }
                    }
                }
                else
                {
                    if (skill_3_conditions_array.Contains("8"))
                    {
                        for (int i = 0; i < skill_3_conditions_array.Length; i++)
                        {
                            if (skill_3_conditions_array[i] == "8")
                            {
                                skill_3_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_3_conditions_array.Length; i++)
                    {
                        if (skill_3_conditions_array[i] == "0" && !skill_3_conditions_array.Contains("9"))
                        {
                            skill_3_conditions_array[i] = "9";
                        }
                    }
                }
                else
                {
                    if (skill_3_conditions_array.Contains("9"))
                    {
                        for (int i = 0; i < skill_3_conditions_array.Length; i++)
                        {
                            if (skill_3_conditions_array[i] == "9")
                            {
                                skill_3_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_3_conditions_array.Length; i++)
                    {
                        if (skill_3_conditions_array[i] == "0" && !skill_3_conditions_array.Contains("10"))
                        {
                            skill_3_conditions_array[i] = "10";
                        }
                    }
                }
                else
                {
                    if (skill_3_conditions_array.Contains("10"))
                    {
                        for (int i = 0; i < skill_3_conditions_array.Length; i++)
                        {
                            if (skill_3_conditions_array[i] == "10")
                            {
                                skill_3_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                Properties.Settings.Default.skill_2_power = special_power_tbx.Text;
                Properties.Settings.Default.skill_2_power_under = special_power_under_tbx.Text;

                Properties.Settings.Default.skill_3_conditions_array = ConvertStringArrayToStringJoin(skill_3_conditions_array);
            }
            else if (skill_selected == 2)
            {
                // Over conditions
                Properties.Settings.Default.skill_3_health_over_cdt = Convert.ToInt32(health_over_tbx.Text);
                Properties.Settings.Default.skill_3_shield_over_cdt = Convert.ToInt32(shield_over_tbx.Text);
                Properties.Settings.Default.skill_3_target_health_over_cdt = Convert.ToInt32(target_health_over_tbx.Text);
                Properties.Settings.Default.skill_3_target_shield_over_cdt = Convert.ToInt32(target_shield_over_tbx.Text);

                // Under conditions
                Properties.Settings.Default.skill_3_health_cdt = Convert.ToInt32(health_under_tbx.Text);
                Properties.Settings.Default.skill_3_shield_cdt = Convert.ToInt32(shield_under_tbx.Text);
                Properties.Settings.Default.skill_3_target_health_cdt = Convert.ToInt32(target_health_under_tbx.Text);
                Properties.Settings.Default.skill_3_target_shield_cdt = Convert.ToInt32(target_shield_under_tbx.Text);

                // Charging
                Properties.Settings.Default.skill_4_double_click = skill_charge.IsChecked.Value;

                // Repeat
                Properties.Settings.Default.skill_3_repeat_cdt = Convert.ToInt32(repeat_skill_tbx.Text);

                // Interrupt
                Properties.Settings.Default.skill_3_interrupt_enabled = skill_interrupt_cbx.IsChecked.Value;

                if (health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_4_conditions_array.Length; i++)
                    {
                        if (skill_4_conditions_array[i] == "0" && !skill_4_conditions_array.Contains("1"))
                        {
                            skill_4_conditions_array[i] = "1";
                        }
                    }
                }
                else
                {
                    if (skill_4_conditions_array.Contains("1"))
                    {
                        for (int i = 0; i < skill_4_conditions_array.Length; i++)
                        {
                            if (skill_4_conditions_array[i] == "1")
                            {
                                skill_4_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_4_conditions_array.Length; i++)
                    {
                        if (skill_4_conditions_array[i] == "0" && !skill_4_conditions_array.Contains("2"))
                        {
                            skill_4_conditions_array[i] = "2";
                        }
                    }
                }
                else
                {
                    if (skill_4_conditions_array.Contains("2"))
                    {
                        for (int i = 0; i < skill_4_conditions_array.Length; i++)
                        {
                            if (skill_4_conditions_array[i] == "2")
                            {
                                skill_4_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_4_conditions_array.Length; i++)
                    {
                        if (skill_4_conditions_array[i] == "0" && !skill_4_conditions_array.Contains("3"))
                        {
                            skill_4_conditions_array[i] = "3";
                        }
                    }
                }
                else
                {
                    if (skill_4_conditions_array.Contains("3"))
                    {
                        for (int i = 0; i < skill_4_conditions_array.Length; i++)
                        {
                            if (skill_4_conditions_array[i] == "3")
                            {
                                skill_4_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_4_conditions_array.Length; i++)
                    {
                        if (skill_4_conditions_array[i] == "0" && !skill_4_conditions_array.Contains("4"))
                        {
                            skill_4_conditions_array[i] = "4";
                        }
                    }
                }
                else
                {
                    if (skill_4_conditions_array.Contains("4"))
                    {
                        for (int i = 0; i < skill_4_conditions_array.Length; i++)
                        {
                            if (skill_4_conditions_array[i] == "4")
                            {
                                skill_4_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_4_conditions_array.Length; i++)
                    {
                        if (skill_4_conditions_array[i] == "0" && !skill_4_conditions_array.Contains("5"))
                        {
                            skill_4_conditions_array[i] = "5";
                        }
                    }
                }
                else
                {
                    if (skill_4_conditions_array.Contains("5"))
                    {
                        for (int i = 0; i < skill_4_conditions_array.Length; i++)
                        {
                            if (skill_4_conditions_array[i] == "5")
                            {
                                skill_4_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_4_conditions_array.Length; i++)
                    {
                        if (skill_4_conditions_array[i] == "0" && !skill_4_conditions_array.Contains("6"))
                        {
                            skill_4_conditions_array[i] = "6";
                        }
                    }
                }
                else
                {
                    if (skill_4_conditions_array.Contains("6"))
                    {
                        for (int i = 0; i < skill_4_conditions_array.Length; i++)
                        {
                            if (skill_4_conditions_array[i] == "6")
                            {
                                skill_4_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_4_conditions_array.Length; i++)
                    {
                        if (skill_4_conditions_array[i] == "0" && !skill_4_conditions_array.Contains("7"))
                        {
                            skill_4_conditions_array[i] = "7";
                        }
                    }
                }
                else
                {
                    if (skill_4_conditions_array.Contains("7"))
                    {
                        for (int i = 0; i < skill_4_conditions_array.Length; i++)
                        {
                            if (skill_4_conditions_array[i] == "7")
                            {
                                skill_4_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_4_conditions_array.Length; i++)
                    {
                        if (skill_4_conditions_array[i] == "0" && !skill_4_conditions_array.Contains("8"))
                        {
                            skill_4_conditions_array[i] = "8";
                        }
                    }
                }
                else
                {
                    if (skill_4_conditions_array.Contains("8"))
                    {
                        for (int i = 0; i < skill_4_conditions_array.Length; i++)
                        {
                            if (skill_4_conditions_array[i] == "8")
                            {
                                skill_4_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_4_conditions_array.Length; i++)
                    {
                        if (skill_4_conditions_array[i] == "0" && !skill_4_conditions_array.Contains("9"))
                        {
                            skill_4_conditions_array[i] = "9";
                        }
                    }
                }
                else
                {
                    if (skill_4_conditions_array.Contains("9"))
                    {
                        for (int i = 0; i < skill_4_conditions_array.Length; i++)
                        {
                            if (skill_4_conditions_array[i] == "9")
                            {
                                skill_4_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_4_conditions_array.Length; i++)
                    {
                        if (skill_4_conditions_array[i] == "0" && !skill_4_conditions_array.Contains("10"))
                        {
                            skill_4_conditions_array[i] = "10";
                        }
                    }
                }
                else
                {
                    if (skill_4_conditions_array.Contains("10"))
                    {
                        for (int i = 0; i < skill_4_conditions_array.Length; i++)
                        {
                            if (skill_4_conditions_array[i] == "10")
                            {
                                skill_4_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                Properties.Settings.Default.skill_3_power = special_power_tbx.Text;
                Properties.Settings.Default.skill_3_power_under = special_power_under_tbx.Text;

                Properties.Settings.Default.skill_4_conditions_array = ConvertStringArrayToStringJoin(skill_4_conditions_array);
            }
            else if (skill_selected == 3)
            {
                // Over conditions
                Properties.Settings.Default.skill_4_health_over_cdt = Convert.ToInt32(health_over_tbx.Text);
                Properties.Settings.Default.skill_4_shield_over_cdt = Convert.ToInt32(shield_over_tbx.Text);
                Properties.Settings.Default.skill_4_target_health_over_cdt = Convert.ToInt32(target_health_over_tbx.Text);
                Properties.Settings.Default.skill_4_target_shield_over_cdt = Convert.ToInt32(target_shield_over_tbx.Text);

                // Under conditions
                Properties.Settings.Default.skill_4_health_cdt = Convert.ToInt32(health_under_tbx.Text);
                Properties.Settings.Default.skill_4_shield_cdt = Convert.ToInt32(shield_under_tbx.Text);
                Properties.Settings.Default.skill_4_target_health_cdt = Convert.ToInt32(target_health_under_tbx.Text);
                Properties.Settings.Default.skill_4_target_shield_cdt = Convert.ToInt32(target_shield_under_tbx.Text);

                // Charging
                Properties.Settings.Default.skill_5_double_click = skill_charge.IsChecked.Value;

                // Repeat
                Properties.Settings.Default.skill_4_repeat_cdt = Convert.ToInt32(repeat_skill_tbx.Text);

                // Interrupt
                Properties.Settings.Default.skill_4_interrupt_enabled = skill_interrupt_cbx.IsChecked.Value;

                if (health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_5_conditions_array.Length; i++)
                    {
                        if (skill_5_conditions_array[i] == "0" && !skill_5_conditions_array.Contains("1"))
                        {
                            skill_5_conditions_array[i] = "1";
                        }
                    }
                }
                else
                {
                    if (skill_5_conditions_array.Contains("1"))
                    {
                        for (int i = 0; i < skill_5_conditions_array.Length; i++)
                        {
                            if (skill_5_conditions_array[i] == "1")
                            {
                                skill_5_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_5_conditions_array.Length; i++)
                    {
                        if (skill_5_conditions_array[i] == "0" && !skill_5_conditions_array.Contains("2"))
                        {
                            skill_5_conditions_array[i] = "2";
                        }
                    }
                }
                else
                {
                    if (skill_5_conditions_array.Contains("2"))
                    {
                        for (int i = 0; i < skill_5_conditions_array.Length; i++)
                        {
                            if (skill_5_conditions_array[i] == "2")
                            {
                                skill_5_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_5_conditions_array.Length; i++)
                    {
                        if (skill_5_conditions_array[i] == "0" && !skill_5_conditions_array.Contains("3"))
                        {
                            skill_5_conditions_array[i] = "3";
                        }
                    }
                }
                else
                {
                    if (skill_5_conditions_array.Contains("3"))
                    {
                        for (int i = 0; i < skill_5_conditions_array.Length; i++)
                        {
                            if (skill_5_conditions_array[i] == "3")
                            {
                                skill_5_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_5_conditions_array.Length; i++)
                    {
                        if (skill_5_conditions_array[i] == "0" && !skill_5_conditions_array.Contains("4"))
                        {
                            skill_5_conditions_array[i] = "4";
                        }
                    }
                }
                else
                {
                    if (skill_5_conditions_array.Contains("4"))
                    {
                        for (int i = 0; i < skill_5_conditions_array.Length; i++)
                        {
                            if (skill_5_conditions_array[i] == "4")
                            {
                                skill_5_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_5_conditions_array.Length; i++)
                    {
                        if (skill_5_conditions_array[i] == "0" && !skill_5_conditions_array.Contains("5"))
                        {
                            skill_5_conditions_array[i] = "5";
                        }
                    }
                }
                else
                {
                    if (skill_5_conditions_array.Contains("5"))
                    {
                        for (int i = 0; i < skill_5_conditions_array.Length; i++)
                        {
                            if (skill_5_conditions_array[i] == "5")
                            {
                                skill_5_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_5_conditions_array.Length; i++)
                    {
                        if (skill_5_conditions_array[i] == "0" && !skill_5_conditions_array.Contains("6"))
                        {
                            skill_5_conditions_array[i] = "6";
                        }
                    }
                }
                else
                {
                    if (skill_5_conditions_array.Contains("6"))
                    {
                        for (int i = 0; i < skill_5_conditions_array.Length; i++)
                        {
                            if (skill_5_conditions_array[i] == "6")
                            {
                                skill_5_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_5_conditions_array.Length; i++)
                    {
                        if (skill_5_conditions_array[i] == "0" && !skill_5_conditions_array.Contains("7"))
                        {
                            skill_5_conditions_array[i] = "7";
                        }
                    }
                }
                else
                {
                    if (skill_5_conditions_array.Contains("7"))
                    {
                        for (int i = 0; i < skill_5_conditions_array.Length; i++)
                        {
                            if (skill_5_conditions_array[i] == "7")
                            {
                                skill_5_conditions_array[i] = "7";
                            }
                        }
                    }
                }

                if (target_shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_5_conditions_array.Length; i++)
                    {
                        if (skill_5_conditions_array[i] == "0" && !skill_5_conditions_array.Contains("8"))
                        {
                            skill_5_conditions_array[i] = "8";
                        }
                    }
                }
                else
                {
                    if (skill_5_conditions_array.Contains("8"))
                    {
                        for (int i = 0; i < skill_5_conditions_array.Length; i++)
                        {
                            if (skill_5_conditions_array[i] == "8")
                            {
                                skill_5_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_5_conditions_array.Length; i++)
                    {
                        if (skill_5_conditions_array[i] == "0" && !skill_5_conditions_array.Contains("9"))
                        {
                            skill_5_conditions_array[i] = "9";
                        }
                    }
                }
                else
                {
                    if (skill_5_conditions_array.Contains("9"))
                    {
                        for (int i = 0; i < skill_5_conditions_array.Length; i++)
                        {
                            if (skill_5_conditions_array[i] == "9")
                            {
                                skill_5_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_5_conditions_array.Length; i++)
                    {
                        if (skill_5_conditions_array[i] == "0" && !skill_5_conditions_array.Contains("10"))
                        {
                            skill_5_conditions_array[i] = "10";
                        }
                    }
                }
                else
                {
                    if (skill_5_conditions_array.Contains("10"))
                    {
                        for (int i = 0; i < skill_5_conditions_array.Length; i++)
                        {
                            if (skill_5_conditions_array[i] == "10")
                            {
                                skill_5_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                Properties.Settings.Default.skill_4_power = special_power_tbx.Text;
                Properties.Settings.Default.skill_4_power_under = special_power_under_tbx.Text;

                Properties.Settings.Default.skill_5_conditions_array = ConvertStringArrayToStringJoin(skill_5_conditions_array);
            }
            else if (skill_selected == 4)
            {
                // Over conditions
                Properties.Settings.Default.skill_5_health_over_cdt = Convert.ToInt32(health_over_tbx.Text);
                Properties.Settings.Default.skill_5_shield_over_cdt = Convert.ToInt32(shield_over_tbx.Text);
                Properties.Settings.Default.skill_5_target_health_over_cdt = Convert.ToInt32(target_health_over_tbx.Text);
                Properties.Settings.Default.skill_5_target_shield_over_cdt = Convert.ToInt32(target_shield_over_tbx.Text);

                // Under conditions
                Properties.Settings.Default.skill_5_health_cdt = Convert.ToInt32(health_under_tbx.Text);
                Properties.Settings.Default.skill_5_shield_cdt = Convert.ToInt32(shield_under_tbx.Text);
                Properties.Settings.Default.skill_5_target_health_cdt = Convert.ToInt32(target_health_under_tbx.Text);
                Properties.Settings.Default.skill_5_target_shield_cdt = Convert.ToInt32(target_shield_under_tbx.Text);
                
                // Charging
                Properties.Settings.Default.skill_6_double_click = skill_charge.IsChecked.Value;

                // Repeat
                Properties.Settings.Default.skill_5_repeat_cdt = Convert.ToInt32(repeat_skill_tbx.Text);

                // Interrupt
                Properties.Settings.Default.skill_5_interrupt_enabled = skill_interrupt_cbx.IsChecked.Value;

                if (health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_6_conditions_array.Length; i++)
                    {
                        if (skill_6_conditions_array[i] == "0" && !skill_6_conditions_array.Contains("1"))
                        {
                            skill_6_conditions_array[i] = "1";
                        }
                    }
                }
                else
                {
                    if (skill_6_conditions_array.Contains("1"))
                    {
                        for (int i = 0; i < skill_6_conditions_array.Length; i++)
                        {
                            if (skill_6_conditions_array[i] == "1")
                            {
                                skill_6_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_6_conditions_array.Length; i++)
                    {
                        if (skill_6_conditions_array[i] == "0" && !skill_6_conditions_array.Contains("2"))
                        {
                            skill_6_conditions_array[i] = "2";
                        }
                    }
                }
                else
                {
                    if (skill_6_conditions_array.Contains("2"))
                    {
                        for (int i = 0; i < skill_6_conditions_array.Length; i++)
                        {
                            if (skill_6_conditions_array[i] == "2")
                            {
                                skill_6_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_6_conditions_array.Length; i++)
                    {
                        if (skill_6_conditions_array[i] == "0" && !skill_6_conditions_array.Contains("3"))
                        {
                            skill_6_conditions_array[i] = "3";
                        }
                    }
                }
                else
                {
                    if (skill_6_conditions_array.Contains("3"))
                    {
                        for (int i = 0; i < skill_6_conditions_array.Length; i++)
                        {
                            if (skill_6_conditions_array[i] == "3")
                            {
                                skill_6_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_6_conditions_array.Length; i++)
                    {
                        if (skill_6_conditions_array[i] == "0" && !skill_6_conditions_array.Contains("4"))
                        {
                            skill_6_conditions_array[i] = "4";
                        }
                    }
                }
                else
                {
                    if (skill_6_conditions_array.Contains("4"))
                    {
                        for (int i = 0; i < skill_6_conditions_array.Length; i++)
                        {
                            if (skill_6_conditions_array[i] == "4")
                            {
                                skill_6_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_6_conditions_array.Length; i++)
                    {
                        if (skill_6_conditions_array[i] == "0" && !skill_6_conditions_array.Contains("5"))
                        {
                            skill_6_conditions_array[i] = "5";
                        }
                    }
                }
                else
                {
                    if (skill_6_conditions_array.Contains("5"))
                    {
                        for (int i = 0; i < skill_6_conditions_array.Length; i++)
                        {
                            if (skill_6_conditions_array[i] == "5")
                            {
                                skill_6_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_6_conditions_array.Length; i++)
                    {
                        if (skill_6_conditions_array[i] == "0" && !skill_6_conditions_array.Contains("6"))
                        {
                            skill_6_conditions_array[i] = "6";
                        }
                    }
                }
                else
                {
                    if (skill_6_conditions_array.Contains("6"))
                    {
                        for (int i = 0; i < skill_6_conditions_array.Length; i++)
                        {
                            if (skill_6_conditions_array[i] == "6")
                            {
                                skill_6_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_6_conditions_array.Length; i++)
                    {
                        if (skill_6_conditions_array[i] == "0" && !skill_6_conditions_array.Contains("7"))
                        {
                            skill_6_conditions_array[i] = "7";
                        }
                    }
                }
                else
                {
                    if (skill_6_conditions_array.Contains("7"))
                    {
                        for (int i = 0; i < skill_6_conditions_array.Length; i++)
                        {
                            if (skill_6_conditions_array[i] == "7")
                            {
                                skill_6_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_6_conditions_array.Length; i++)
                    {
                        if (skill_6_conditions_array[i] == "0" && !skill_6_conditions_array.Contains("8"))
                        {
                            skill_6_conditions_array[i] = "8";
                        }
                    }
                }
                else
                {
                    if (skill_6_conditions_array.Contains("8"))
                    {
                        for (int i = 0; i < skill_6_conditions_array.Length; i++)
                        {
                            if (skill_6_conditions_array[i] == "8")
                            {
                                skill_6_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_6_conditions_array.Length; i++)
                    {
                        if (skill_6_conditions_array[i] == "0" && !skill_6_conditions_array.Contains("9"))
                        {
                            skill_6_conditions_array[i] = "9";
                        }
                    }
                }
                else
                {
                    if (skill_6_conditions_array.Contains("9"))
                    {
                        for (int i = 0; i < skill_6_conditions_array.Length; i++)
                        {
                            if (skill_6_conditions_array[i] == "9")
                            {
                                skill_6_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_6_conditions_array.Length; i++)
                    {
                        if (skill_6_conditions_array[i] == "0" && !skill_6_conditions_array.Contains("10"))
                        {
                            skill_6_conditions_array[i] = "10";
                        }
                    }
                }
                else
                {
                    if (skill_6_conditions_array.Contains("10"))
                    {
                        for (int i = 0; i < skill_6_conditions_array.Length; i++)
                        {
                            if (skill_6_conditions_array[i] == "10")
                            {
                                skill_6_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                Properties.Settings.Default.skill_5_power = special_power_tbx.Text;
                Properties.Settings.Default.skill_5_power_under = special_power_under_tbx.Text;

                Properties.Settings.Default.skill_6_conditions_array = ConvertStringArrayToStringJoin(skill_6_conditions_array);
            }
            else if (skill_selected == 5)
            {
                // Over conditions
                Properties.Settings.Default.skill_6_health_over_cdt = Convert.ToInt32(health_over_tbx.Text);
                Properties.Settings.Default.skill_6_shield_over_cdt = Convert.ToInt32(shield_over_tbx.Text);
                Properties.Settings.Default.skill_6_target_health_over_cdt = Convert.ToInt32(target_health_over_tbx.Text);
                Properties.Settings.Default.skill_6_target_shield_over_cdt = Convert.ToInt32(target_shield_over_tbx.Text);

                // Under conditions
                Properties.Settings.Default.skill_6_health_cdt = Convert.ToInt32(health_under_tbx.Text);
                Properties.Settings.Default.skill_6_shield_cdt = Convert.ToInt32(shield_under_tbx.Text);
                Properties.Settings.Default.skill_6_target_health_cdt = Convert.ToInt32(target_health_under_tbx.Text);
                Properties.Settings.Default.skill_6_target_shield_cdt = Convert.ToInt32(target_shield_under_tbx.Text);
                
                // Charging
                Properties.Settings.Default.skill_7_double_click = skill_charge.IsChecked.Value;

                // Repeat
                Properties.Settings.Default.skill_6_repeat_cdt = Convert.ToInt32(repeat_skill_tbx.Text);

                // Interrupt
                Properties.Settings.Default.skill_6_interrupt_enabled = skill_interrupt_cbx.IsChecked.Value;

                if (health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_7_conditions_array.Length; i++)
                    {
                        if (skill_7_conditions_array[i] == "0" && !skill_7_conditions_array.Contains("1"))
                        {
                            skill_7_conditions_array[i] = "1";
                        }
                    }
                }
                else
                {
                    if (skill_7_conditions_array.Contains("1"))
                    {
                        for (int i = 0; i < skill_7_conditions_array.Length; i++)
                        {
                            if (skill_7_conditions_array[i] == "1")
                            {
                                skill_7_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_7_conditions_array.Length; i++)
                    {
                        if (skill_7_conditions_array[i] == "0" && !skill_7_conditions_array.Contains("2"))
                        {
                            skill_7_conditions_array[i] = "2";
                        }
                    }
                }
                else
                {
                    if (skill_7_conditions_array.Contains("2"))
                    {
                        for (int i = 0; i < skill_7_conditions_array.Length; i++)
                        {
                            if (skill_7_conditions_array[i] == "2")
                            {
                                skill_7_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_7_conditions_array.Length; i++)
                    {
                        if (skill_7_conditions_array[i] == "0" && !skill_7_conditions_array.Contains("3"))
                        {
                            skill_7_conditions_array[i] = "3";
                        }
                    }
                }
                else
                {
                    if (skill_7_conditions_array.Contains("3"))
                    {
                        for (int i = 0; i < skill_7_conditions_array.Length; i++)
                        {
                            if (skill_7_conditions_array[i] == "3")
                            {
                                skill_7_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_7_conditions_array.Length; i++)
                    {
                        if (skill_7_conditions_array[i] == "0" && !skill_7_conditions_array.Contains("4"))
                        {
                            skill_7_conditions_array[i] = "4";
                        }
                    }
                }
                else
                {
                    if (skill_7_conditions_array.Contains("4"))
                    {
                        for (int i = 0; i < skill_7_conditions_array.Length; i++)
                        {
                            if (skill_7_conditions_array[i] == "4")
                            {
                                skill_7_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_7_conditions_array.Length; i++)
                    {
                        if (skill_7_conditions_array[i] == "0" && !skill_7_conditions_array.Contains("5"))
                        {
                            skill_7_conditions_array[i] = "5";
                        }
                    }
                }
                else
                {
                    if (skill_7_conditions_array.Contains("5"))
                    {
                        for (int i = 0; i < skill_7_conditions_array.Length; i++)
                        {
                            if (skill_7_conditions_array[i] == "5")
                            {
                                skill_7_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_7_conditions_array.Length; i++)
                    {
                        if (skill_7_conditions_array[i] == "0" && !skill_7_conditions_array.Contains("6"))
                        {
                            skill_7_conditions_array[i] = "6";
                        }
                    }
                }
                else
                {
                    if (skill_7_conditions_array.Contains("6"))
                    {
                        for (int i = 0; i < skill_7_conditions_array.Length; i++)
                        {
                            if (skill_7_conditions_array[i] == "6")
                            {
                                skill_7_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_7_conditions_array.Length; i++)
                    {
                        if (skill_7_conditions_array[i] == "0" && !skill_7_conditions_array.Contains("7"))
                        {
                            skill_7_conditions_array[i] = "7";
                        }
                    }
                }
                else
                {
                    if (skill_7_conditions_array.Contains("7"))
                    {
                        for (int i = 0; i < skill_7_conditions_array.Length; i++)
                        {
                            if (skill_7_conditions_array[i] == "7")
                            {
                                skill_7_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_7_conditions_array.Length; i++)
                    {
                        if (skill_7_conditions_array[i] == "0" && !skill_7_conditions_array.Contains("8"))
                        {
                            skill_7_conditions_array[i] = "8";
                        }
                    }
                }
                else
                {
                    if (skill_7_conditions_array.Contains("8"))
                    {
                        for (int i = 0; i < skill_7_conditions_array.Length; i++)
                        {
                            if (skill_7_conditions_array[i] == "8")
                            {
                                skill_7_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_7_conditions_array.Length; i++)
                    {
                        if (skill_7_conditions_array[i] == "0" && !skill_7_conditions_array.Contains("9"))
                        {
                            skill_7_conditions_array[i] = "9";
                        }
                    }
                }
                else
                {
                    if (skill_7_conditions_array.Contains("9"))
                    {
                        for (int i = 0; i < skill_7_conditions_array.Length; i++)
                        {
                            if (skill_7_conditions_array[i] == "9")
                            {
                                skill_7_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_7_conditions_array.Length; i++)
                    {
                        if (skill_7_conditions_array[i] == "0" && !skill_7_conditions_array.Contains("10"))
                        {
                            skill_7_conditions_array[i] = "10";
                        }
                    }
                }
                else
                {
                    if (skill_7_conditions_array.Contains("10"))
                    {
                        for (int i = 0; i < skill_7_conditions_array.Length; i++)
                        {
                            if (skill_7_conditions_array[i] == "10")
                            {
                                skill_7_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                Properties.Settings.Default.skill_6_power = special_power_tbx.Text;
                Properties.Settings.Default.skill_6_power_under = special_power_under_tbx.Text;

                Properties.Settings.Default.skill_7_conditions_array = ConvertStringArrayToStringJoin(skill_7_conditions_array);
            }
            else if (skill_selected == 6)
            {
                // Over conditions
                Properties.Settings.Default.skill_7_health_over_cdt = Convert.ToInt32(health_over_tbx.Text);
                Properties.Settings.Default.skill_7_shield_over_cdt = Convert.ToInt32(shield_over_tbx.Text);
                Properties.Settings.Default.skill_7_target_health_over_cdt = Convert.ToInt32(target_health_over_tbx.Text);
                Properties.Settings.Default.skill_7_target_shield_over_cdt = Convert.ToInt32(target_shield_over_tbx.Text);

                // Under conditions
                Properties.Settings.Default.skill_7_health_cdt = Convert.ToInt32(health_under_tbx.Text);
                Properties.Settings.Default.skill_7_shield_cdt = Convert.ToInt32(shield_under_tbx.Text);
                Properties.Settings.Default.skill_7_target_health_cdt = Convert.ToInt32(target_health_under_tbx.Text);
                Properties.Settings.Default.skill_7_target_shield_cdt = Convert.ToInt32(target_shield_under_tbx.Text);

                // Charging
                Properties.Settings.Default.skill_8_double_click = skill_charge.IsChecked.Value;

                // Repeat
                Properties.Settings.Default.skill_7_repeat_cdt = Convert.ToInt32(repeat_skill_tbx.Text);

                // Interrupt
                Properties.Settings.Default.skill_7_interrupt_enabled = skill_interrupt_cbx.IsChecked.Value;

                if (health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_8_conditions_array.Length; i++)
                    {
                        if (skill_8_conditions_array[i] == "0" && !skill_8_conditions_array.Contains("1"))
                        {
                            skill_8_conditions_array[i] = "1";
                        }
                    }
                }
                else
                {
                    if (skill_8_conditions_array.Contains("1"))
                    {
                        for (int i = 0; i < skill_8_conditions_array.Length; i++)
                        {
                            if (skill_8_conditions_array[i] == "1")
                            {
                                skill_8_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_8_conditions_array.Length; i++)
                    {
                        if (skill_8_conditions_array[i] == "0" && !skill_8_conditions_array.Contains("2"))
                        {
                            skill_8_conditions_array[i] = "2";
                        }
                    }
                }
                else
                {
                    if (skill_8_conditions_array.Contains("2"))
                    {
                        for (int i = 0; i < skill_8_conditions_array.Length; i++)
                        {
                            if (skill_8_conditions_array[i] == "2")
                            {
                                skill_8_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_8_conditions_array.Length; i++)
                    {
                        if (skill_8_conditions_array[i] == "0" && !skill_8_conditions_array.Contains("3"))
                        {
                            skill_8_conditions_array[i] = "3";
                        }
                    }
                }
                else
                {
                    if (skill_8_conditions_array.Contains("3"))
                    {
                        for (int i = 0; i < skill_8_conditions_array.Length; i++)
                        {
                            if (skill_8_conditions_array[i] == "3")
                            {
                                skill_8_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_8_conditions_array.Length; i++)
                    {
                        if (skill_8_conditions_array[i] == "0" && !skill_8_conditions_array.Contains("4"))
                        {
                            skill_8_conditions_array[i] = "4";
                        }
                    }
                }
                else
                {
                    if (skill_8_conditions_array.Contains("4"))
                    {
                        for (int i = 0; i < skill_8_conditions_array.Length; i++)
                        {
                            if (skill_8_conditions_array[i] == "4")
                            {
                                skill_8_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_8_conditions_array.Length; i++)
                    {
                        if (skill_8_conditions_array[i] == "0" && !skill_8_conditions_array.Contains("5"))
                        {
                            skill_8_conditions_array[i] = "5";
                        }
                    }
                }
                else
                {
                    if (skill_8_conditions_array.Contains("5"))
                    {
                        for (int i = 0; i < skill_8_conditions_array.Length; i++)
                        {
                            if (skill_8_conditions_array[i] == "5")
                            {
                                skill_8_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_8_conditions_array.Length; i++)
                    {
                        if (skill_8_conditions_array[i] == "0" && !skill_8_conditions_array.Contains("6"))
                        {
                            skill_8_conditions_array[i] = "6";
                        }
                    }
                }
                else
                {
                    if (skill_8_conditions_array.Contains("6"))
                    {
                        for (int i = 0; i < skill_8_conditions_array.Length; i++)
                        {
                            if (skill_8_conditions_array[i] == "6")
                            {
                                skill_8_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_8_conditions_array.Length; i++)
                    {
                        if (skill_8_conditions_array[i] == "0" && !skill_8_conditions_array.Contains("7"))
                        {
                            skill_8_conditions_array[i] = "7";
                        }
                    }
                }
                else
                {
                    if (skill_8_conditions_array.Contains("7"))
                    {
                        for (int i = 0; i < skill_8_conditions_array.Length; i++)
                        {
                            if (skill_8_conditions_array[i] == "7")
                            {
                                skill_8_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_8_conditions_array.Length; i++)
                    {
                        if (skill_8_conditions_array[i] == "0" && !skill_8_conditions_array.Contains("8"))
                        {
                            skill_8_conditions_array[i] = "8";
                        }
                    }
                }
                else
                {
                    if (skill_8_conditions_array.Contains("8"))
                    {
                        for (int i = 0; i < skill_8_conditions_array.Length; i++)
                        {
                            if (skill_8_conditions_array[i] == "8")
                            {
                                skill_8_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_8_conditions_array.Length; i++)
                    {
                        if (skill_8_conditions_array[i] == "0" && !skill_8_conditions_array.Contains("9"))
                        {
                            skill_8_conditions_array[i] = "9";
                        }
                    }
                }
                else
                {
                    if (skill_8_conditions_array.Contains("9"))
                    {
                        for (int i = 0; i < skill_8_conditions_array.Length; i++)
                        {
                            if (skill_8_conditions_array[i] == "9")
                            {
                                skill_8_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_8_conditions_array.Length; i++)
                    {
                        if (skill_8_conditions_array[i] == "0" && !skill_8_conditions_array.Contains("10"))
                        {
                            skill_8_conditions_array[i] = "10";
                        }
                    }
                }
                else
                {
                    if (skill_8_conditions_array.Contains("10"))
                    {
                        for (int i = 0; i < skill_8_conditions_array.Length; i++)
                        {
                            if (skill_8_conditions_array[i] == "10")
                            {
                                skill_8_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                Properties.Settings.Default.skill_7_power = special_power_tbx.Text;
                Properties.Settings.Default.skill_7_power_under = special_power_under_tbx.Text;

                Properties.Settings.Default.skill_8_conditions_array = ConvertStringArrayToStringJoin(skill_8_conditions_array);
            }
            else if (skill_selected == 7)
            {
                // Over conditions
                Properties.Settings.Default.skill_8_health_over_cdt = Convert.ToInt32(health_over_tbx.Text);
                Properties.Settings.Default.skill_8_shield_over_cdt = Convert.ToInt32(shield_over_tbx.Text);
                Properties.Settings.Default.skill_8_target_health_over_cdt = Convert.ToInt32(target_health_over_tbx.Text);
                Properties.Settings.Default.skill_8_target_shield_over_cdt = Convert.ToInt32(target_shield_over_tbx.Text);

                // Under conditions
                Properties.Settings.Default.skill_8_health_cdt = Convert.ToInt32(health_under_tbx.Text);
                Properties.Settings.Default.skill_8_shield_cdt = Convert.ToInt32(shield_under_tbx.Text);
                Properties.Settings.Default.skill_8_target_health_cdt = Convert.ToInt32(target_health_under_tbx.Text);
                Properties.Settings.Default.skill_8_target_shield_cdt = Convert.ToInt32(target_shield_under_tbx.Text);

                // Charging
                Properties.Settings.Default.skill_9_double_click = skill_charge.IsChecked.Value;

                // Repeat
                Properties.Settings.Default.skill_8_repeat_cdt = Convert.ToInt32(repeat_skill_tbx.Text);

                // Interrupt
                Properties.Settings.Default.skill_8_interrupt_enabled = skill_interrupt_cbx.IsChecked.Value;

                if (health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_9_conditions_array.Length; i++)
                    {
                        if (skill_9_conditions_array[i] == "0" && !skill_9_conditions_array.Contains("1"))
                        {
                            skill_9_conditions_array[i] = "1";
                        }
                    }
                }
                else
                {
                    if (skill_9_conditions_array.Contains("1"))
                    {
                        for (int i = 0; i < skill_9_conditions_array.Length; i++)
                        {
                            if (skill_9_conditions_array[i] == "1")
                            {
                                skill_9_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_9_conditions_array.Length; i++)
                    {
                        if (skill_9_conditions_array[i] == "0" && !skill_9_conditions_array.Contains("2"))
                        {
                            skill_9_conditions_array[i] = "2";
                        }
                    }
                }
                else
                {
                    if (skill_9_conditions_array.Contains("2"))
                    {
                        for (int i = 0; i < skill_9_conditions_array.Length; i++)
                        {
                            if (skill_9_conditions_array[i] == "2")
                            {
                                skill_9_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_9_conditions_array.Length; i++)
                    {
                        if (skill_9_conditions_array[i] == "0" && !skill_9_conditions_array.Contains("3"))
                        {
                            skill_9_conditions_array[i] = "3";
                        }
                    }
                }
                else
                {
                    if (skill_9_conditions_array.Contains("3"))
                    {
                        for (int i = 0; i < skill_9_conditions_array.Length; i++)
                        {
                            if (skill_9_conditions_array[i] == "3")
                            {
                                skill_9_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_9_conditions_array.Length; i++)
                    {
                        if (skill_9_conditions_array[i] == "0" && !skill_9_conditions_array.Contains("4"))
                        {
                            skill_9_conditions_array[i] = "4";
                        }
                    }
                }
                else
                {
                    if (skill_9_conditions_array.Contains("4"))
                    {
                        for (int i = 0; i < skill_9_conditions_array.Length; i++)
                        {
                            if (skill_9_conditions_array[i] == "4")
                            {
                                skill_9_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_9_conditions_array.Length; i++)
                    {
                        if (skill_9_conditions_array[i] == "0" && !skill_9_conditions_array.Contains("5"))
                        {
                            skill_9_conditions_array[i] = "5";
                        }
                    }
                }
                else
                {
                    if (skill_9_conditions_array.Contains("5"))
                    {
                        for (int i = 0; i < skill_9_conditions_array.Length; i++)
                        {
                            if (skill_9_conditions_array[i] == "5")
                            {
                                skill_9_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_9_conditions_array.Length; i++)
                    {
                        if (skill_9_conditions_array[i] == "0" && !skill_9_conditions_array.Contains("6"))
                        {
                            skill_9_conditions_array[i] = "6";
                        }
                    }
                }
                else
                {
                    if (skill_9_conditions_array.Contains("6"))
                    {
                        for (int i = 0; i < skill_9_conditions_array.Length; i++)
                        {
                            if (skill_9_conditions_array[i] == "6")
                            {
                                skill_9_conditions_array[i] = "0";
                            }
                        }
                    }
                }


                if (target_shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_9_conditions_array.Length; i++)
                    {
                        if (skill_9_conditions_array[i] == "0" && !skill_9_conditions_array.Contains("7"))
                        {
                            skill_9_conditions_array[i] = "7";
                        }
                    }
                }
                else
                {
                    if (skill_9_conditions_array.Contains("7"))
                    {
                        for (int i = 0; i < skill_9_conditions_array.Length; i++)
                        {
                            if (skill_9_conditions_array[i] == "7")
                            {
                                skill_9_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_9_conditions_array.Length; i++)
                    {
                        if (skill_9_conditions_array[i] == "0" && !skill_9_conditions_array.Contains("8"))
                        {
                            skill_9_conditions_array[i] = "8";
                        }
                    }
                }
                else
                {
                    if(skill_9_conditions_array.Contains("8"))
                    {
                        for(int i = 0; i < skill_9_conditions_array.Length; i++)
                        {
                            if(skill_9_conditions_array[i] == "8")
                            {
                                skill_9_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_9_conditions_array.Length; i++)
                    {
                        if (skill_9_conditions_array[i] == "0" && !skill_9_conditions_array.Contains("9"))
                        {
                            skill_9_conditions_array[i] = "9";
                        }
                    }
                }
                else
                {
                    if (skill_9_conditions_array.Contains("9"))
                    {
                        for (int i = 0; i < skill_9_conditions_array.Length; i++)
                        {
                            if (skill_9_conditions_array[i] == "9")
                            {
                                skill_9_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_under_tbx.Text != "0")
                {
                    for (int i = 0; i < skill_9_conditions_array.Length; i++)
                    {
                        if (skill_9_conditions_array[i] == "0" && !skill_9_conditions_array.Contains("10"))
                        {
                            skill_9_conditions_array[i] = "0";
                        }
                    }
                }
                else
                {
                    if (skill_9_conditions_array.Contains("10"))
                    {
                        for (int i = 0; i < skill_9_conditions_array.Length; i++)
                        {
                            if (skill_9_conditions_array[i] == "10")
                            {
                                skill_9_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                Properties.Settings.Default.skill_8_power = special_power_tbx.Text;
                Properties.Settings.Default.skill_8_power_under = special_power_under_tbx.Text;

                Properties.Settings.Default.skill_9_conditions_array = ConvertStringArrayToStringJoin(skill_9_conditions_array);
            #endregion skills
            } 
            else if (skill_selected == 8) // Below only key using skills
            {
                // Over conditions
                Properties.Settings.Default.key_using_1_health_over_cdt = Convert.ToInt32(health_over_tbx.Text);
                Properties.Settings.Default.key_using_1_shield_over_cdt = Convert.ToInt32(shield_over_tbx.Text);
                Properties.Settings.Default.key_using_1_target_health_over_cdt = Convert.ToInt32(target_health_over_tbx.Text);
                Properties.Settings.Default.key_using_1_target_shield_over_cdt = Convert.ToInt32(target_shield_over_tbx.Text);

                // Under conditions
                Properties.Settings.Default.key_using_1_health_cdt = Convert.ToInt32(health_under_tbx.Text);
                Properties.Settings.Default.key_using_1_shield_cdt = Convert.ToInt32(shield_under_tbx.Text);
                Properties.Settings.Default.key_using_1_target_health_cdt = Convert.ToInt32(target_health_under_tbx.Text);
                Properties.Settings.Default.key_using_1_target_shield_cdt = Convert.ToInt32(target_shield_under_tbx.Text);

                Properties.Settings.Default.key_using_1_infight = use_infight_cbx.IsChecked.Value;

                // Conditions arraay
                if (health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_1_conditions_array.Length; i++)
                    {
                        if (key_1_conditions_array[i] == "0" && !key_1_conditions_array.Contains("1"))
                        {
                            key_1_conditions_array[i] = "1";
                        }
                    }
                }
                else
                {
                    if (key_1_conditions_array.Contains("1"))
                    {
                        for (int i = 0; i < key_1_conditions_array.Length; i++)
                        {
                            if (key_1_conditions_array[i] == "1")
                            {
                                key_1_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_1_conditions_array.Length; i++)
                    {
                        if (key_1_conditions_array[i] == "0" && !key_1_conditions_array.Contains("2"))
                        {
                            key_1_conditions_array[i] = "2";
                        }
                    }
                }
                else
                {
                    if (key_1_conditions_array.Contains("2"))
                    {
                        for (int i = 0; i < key_1_conditions_array.Length; i++)
                        {
                            if (key_1_conditions_array[i] == "2")
                            {
                                key_1_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_1_conditions_array.Length; i++)
                    {
                        if (key_1_conditions_array[i] == "0" && !key_1_conditions_array.Contains("3"))
                        {
                            key_1_conditions_array[i] = "3";
                        }
                    }
                }
                else
                {
                    if (key_1_conditions_array.Contains("3"))
                    {
                        for (int i = 0; i < key_1_conditions_array.Length; i++)
                        {
                            if (key_1_conditions_array[i] == "3")
                            {
                                key_1_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_1_conditions_array.Length; i++)
                    {
                        if (key_1_conditions_array[i] == "0" && !key_1_conditions_array.Contains("4"))
                        {
                            key_1_conditions_array[i] = "4";
                        }
                    }
                }
                else
                {
                    if (key_1_conditions_array.Contains("4"))
                    {
                        for (int i = 0; i < key_1_conditions_array.Length; i++)
                        {
                            if (key_1_conditions_array[i] == "4")
                            {
                                key_1_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_1_conditions_array.Length; i++)
                    {
                        if (key_1_conditions_array[i] == "0" && !key_1_conditions_array.Contains("5"))
                        {
                            key_1_conditions_array[i] = "5";
                        }
                    }
                }
                else
                {
                    if (key_1_conditions_array.Contains("5"))
                    {
                        for (int i = 0; i < key_1_conditions_array.Length; i++)
                        {
                            if (key_1_conditions_array[i] == "5")
                            {
                                key_1_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_1_conditions_array.Length; i++)
                    {
                        if (key_1_conditions_array[i] == "0" && !key_1_conditions_array.Contains("6"))
                        {
                            key_1_conditions_array[i] = "6";
                        }
                    }
                }
                else
                {
                    if (key_1_conditions_array.Contains("6"))
                    {
                        for (int i = 0; i < key_1_conditions_array.Length; i++)
                        {
                            if (key_1_conditions_array[i] == "6")
                            {
                                key_1_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_1_conditions_array.Length; i++)
                    {
                        if (key_1_conditions_array[i] == "0" && !key_1_conditions_array.Contains("7"))
                        {
                            key_1_conditions_array[i] = "7";
                        }
                    }
                }
                else
                {
                    if (key_1_conditions_array.Contains("7"))
                    {
                        for (int i = 0; i < key_1_conditions_array.Length; i++)
                        {
                            if (key_1_conditions_array[i] == "7")
                            {
                                key_1_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_1_conditions_array.Length; i++)
                    {
                        if (key_1_conditions_array[i] == "0" && !key_1_conditions_array.Contains("8"))
                        {
                            key_1_conditions_array[i] = "8";
                        }
                    }
                }
                else
                {
                    if (key_1_conditions_array.Contains("8"))
                    {
                        for (int i = 0; i < key_1_conditions_array.Length; i++)
                        {
                            if (key_1_conditions_array[i] == "8")
                            {
                                key_1_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_tbx.Text != "0")
                {
                    for (int i = 0; i < key_1_conditions_array.Length; i++)
                    {
                        if (key_1_conditions_array[i] == "0" && !key_1_conditions_array.Contains("9"))
                        {
                            key_1_conditions_array[i] = "9";
                        }
                    }
                }
                else
                {
                    if (key_1_conditions_array.Contains("9"))
                    {
                        for (int i = 0; i < key_1_conditions_array.Length; i++)
                        {
                            if (key_1_conditions_array[i] == "9")
                            {
                                key_1_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_1_conditions_array.Length; i++)
                    {
                        if (key_1_conditions_array[i] == "0" && !key_1_conditions_array.Contains("10"))
                        {
                            key_1_conditions_array[i] = "10";
                        }
                    }
                }
                else
                {
                    if (key_1_conditions_array.Contains("10"))
                    {
                        for (int i = 0; i < key_1_conditions_array.Length; i++)
                        {
                            if (key_1_conditions_array[i] == "10")
                            {
                                key_1_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                Properties.Settings.Default.key_using_1_power = special_power_tbx.Text;
                Properties.Settings.Default.key_using_1_power_under = special_power_under_tbx.Text;

                Properties.Settings.Default.key_using_1_conditions_array = ConvertStringArrayToStringJoin(key_1_conditions_array);
            }
            else if (skill_selected == 9)
            {
                // Over conditions
                Properties.Settings.Default.key_using_2_health_over_cdt = Convert.ToInt32(health_over_tbx.Text);
                Properties.Settings.Default.key_using_2_shield_over_cdt = Convert.ToInt32(shield_over_tbx.Text);
                Properties.Settings.Default.key_using_2_target_health_over_cdt = Convert.ToInt32(target_health_over_tbx.Text);
                Properties.Settings.Default.key_using_2_target_shield_over_cdt = Convert.ToInt32(target_shield_over_tbx.Text);

                // Under conditions
                Properties.Settings.Default.key_using_2_health_cdt = Convert.ToInt32(health_under_tbx.Text);
                Properties.Settings.Default.key_using_2_shield_cdt = Convert.ToInt32(shield_under_tbx.Text);
                Properties.Settings.Default.key_using_2_target_health_cdt = Convert.ToInt32(target_health_under_tbx.Text);
                Properties.Settings.Default.key_using_2_target_shield_cdt = Convert.ToInt32(target_shield_under_tbx.Text);

                Properties.Settings.Default.key_using_2_infight = use_infight_cbx.IsChecked.Value;

                // Conditions arraay
                if (health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_2_conditions_array.Length; i++)
                    {
                        if (key_2_conditions_array[i] == "0" && !key_2_conditions_array.Contains("1"))
                        {
                            key_2_conditions_array[i] = "1";
                        }
                    }
                }
                else
                {
                    if (key_2_conditions_array.Contains("1"))
                    {
                        for (int i = 0; i < key_2_conditions_array.Length; i++)
                        {
                            if (key_2_conditions_array[i] == "1")
                            {
                                key_2_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_2_conditions_array.Length; i++)
                    {
                        if (key_2_conditions_array[i] == "0" && !key_2_conditions_array.Contains("2"))
                        {
                            key_2_conditions_array[i] = "2";
                        }
                    }
                }
                else
                {
                    if (key_2_conditions_array.Contains("2"))
                    {
                        for (int i = 0; i < key_2_conditions_array.Length; i++)
                        {
                            if (key_2_conditions_array[i] == "2")
                            {
                                key_2_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_2_conditions_array.Length; i++)
                    {
                        if (key_2_conditions_array[i] == "0" && !key_2_conditions_array.Contains("3"))
                        {
                            key_2_conditions_array[i] = "3";
                        }
                    }
                }
                else
                {
                    if (key_2_conditions_array.Contains("3"))
                    {
                        for (int i = 0; i < key_2_conditions_array.Length; i++)
                        {
                            if (key_2_conditions_array[i] == "3")
                            {
                                key_2_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_2_conditions_array.Length; i++)
                    {
                        if (key_2_conditions_array[i] == "0" && !key_2_conditions_array.Contains("4"))
                        {
                            key_2_conditions_array[i] = "4";
                        }
                    }
                }
                else
                {
                    if (key_2_conditions_array.Contains("4"))
                    {
                        for (int i = 0; i < key_2_conditions_array.Length; i++)
                        {
                            if (key_2_conditions_array[i] == "4")
                            {
                                key_2_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_2_conditions_array.Length; i++)
                    {
                        if (key_2_conditions_array[i] == "0" && !key_2_conditions_array.Contains("5"))
                        {
                            key_2_conditions_array[i] = "5";
                        }
                    }
                }
                else
                {
                    if (key_2_conditions_array.Contains("5"))
                    {
                        for (int i = 0; i < key_2_conditions_array.Length; i++)
                        {
                            if (key_2_conditions_array[i] == "5")
                            {
                                key_2_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_2_conditions_array.Length; i++)
                    {
                        if (key_2_conditions_array[i] == "0" && !key_2_conditions_array.Contains("6"))
                        {
                            key_2_conditions_array[i] = "6";
                        }
                    }
                }
                else
                {
                    if (key_2_conditions_array.Contains("6"))
                    {
                        for (int i = 0; i < key_2_conditions_array.Length; i++)
                        {
                            if (key_2_conditions_array[i] == "6")
                            {
                                key_2_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_2_conditions_array.Length; i++)
                    {
                        if (key_2_conditions_array[i] == "0" && !key_2_conditions_array.Contains("7"))
                        {
                            key_2_conditions_array[i] = "7";
                        }
                    }
                }
                else
                {
                    if (key_2_conditions_array.Contains("7"))
                    {
                        for (int i = 0; i < key_2_conditions_array.Length; i++)
                        {
                            if (key_2_conditions_array[i] == "7")
                            {
                                key_2_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_2_conditions_array.Length; i++)
                    {
                        if (key_2_conditions_array[i] == "0" && !key_2_conditions_array.Contains("8"))
                        {
                            key_2_conditions_array[i] = "8";
                        }
                    }
                }
                else
                {
                    if (key_2_conditions_array.Contains("8"))
                    {
                        for (int i = 0; i < key_2_conditions_array.Length; i++)
                        {
                            if (key_2_conditions_array[i] == "8")
                            {
                                key_2_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_tbx.Text != "0")
                {
                    for (int i = 0; i < key_2_conditions_array.Length; i++)
                    {
                        if (key_2_conditions_array[i] == "0" && !key_2_conditions_array.Contains("9"))
                        {
                            key_2_conditions_array[i] = "9";
                        }
                    }
                }
                else
                {
                    if (key_2_conditions_array.Contains("9"))
                    {
                        for (int i = 0; i < key_2_conditions_array.Length; i++)
                        {
                            if (key_2_conditions_array[i] == "9")
                            {
                                key_2_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_2_conditions_array.Length; i++)
                    {
                        if (key_2_conditions_array[i] == "0" && !key_2_conditions_array.Contains("10"))
                        {
                            key_2_conditions_array[i] = "10";
                        }
                    }
                }
                else
                {
                    if (key_2_conditions_array.Contains("10"))
                    {
                        for (int i = 0; i < key_2_conditions_array.Length; i++)
                        {
                            if (key_2_conditions_array[i] == "10")
                            {
                                key_2_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                Properties.Settings.Default.key_using_2_power = special_power_tbx.Text;
                Properties.Settings.Default.key_using_2_power_under = special_power_under_tbx.Text;

                Properties.Settings.Default.key_using_2_conditions_array = ConvertStringArrayToStringJoin(key_2_conditions_array);
            }
            else if (skill_selected == 10)
            {
                // Over conditions
                Properties.Settings.Default.key_using_3_health_over_cdt = Convert.ToInt32(health_over_tbx.Text);
                Properties.Settings.Default.key_using_3_shield_over_cdt = Convert.ToInt32(shield_over_tbx.Text);
                Properties.Settings.Default.key_using_3_target_health_over_cdt = Convert.ToInt32(target_health_over_tbx.Text);
                Properties.Settings.Default.key_using_3_target_shield_over_cdt = Convert.ToInt32(target_shield_over_tbx.Text);

                // Under conditions
                Properties.Settings.Default.key_using_3_health_cdt = Convert.ToInt32(health_under_tbx.Text);
                Properties.Settings.Default.key_using_3_shield_cdt = Convert.ToInt32(shield_under_tbx.Text);
                Properties.Settings.Default.key_using_3_target_health_cdt = Convert.ToInt32(target_health_under_tbx.Text);
                Properties.Settings.Default.key_using_3_target_shield_cdt = Convert.ToInt32(target_shield_under_tbx.Text);

                Properties.Settings.Default.key_using_3_infight = use_infight_cbx.IsChecked.Value;

                // Conditions arraay
                if (health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_3_conditions_array.Length; i++)
                    {
                        if (key_3_conditions_array[i] == "0" && !key_3_conditions_array.Contains("1"))
                        {
                            key_3_conditions_array[i] = "1";
                        }
                    }
                }
                else
                {
                    if (key_3_conditions_array.Contains("1"))
                    {
                        for (int i = 0; i < key_3_conditions_array.Length; i++)
                        {
                            if (key_3_conditions_array[i] == "1")
                            {
                                key_3_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_3_conditions_array.Length; i++)
                    {
                        if (key_3_conditions_array[i] == "0" && !key_3_conditions_array.Contains("2"))
                        {
                            key_3_conditions_array[i] = "2";
                        }
                    }
                }
                else
                {
                    if (key_3_conditions_array.Contains("2"))
                    {
                        for (int i = 0; i < key_3_conditions_array.Length; i++)
                        {
                            if (key_3_conditions_array[i] == "2")
                            {
                                key_3_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_3_conditions_array.Length; i++)
                    {
                        if (key_3_conditions_array[i] == "0" && !key_3_conditions_array.Contains("3"))
                        {
                            key_3_conditions_array[i] = "3";
                        }
                    }
                }
                else
                {
                    if (key_3_conditions_array.Contains("3"))
                    {
                        for (int i = 0; i < key_3_conditions_array.Length; i++)
                        {
                            if (key_3_conditions_array[i] == "3")
                            {
                                key_3_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_3_conditions_array.Length; i++)
                    {
                        if (key_3_conditions_array[i] == "0" && !key_3_conditions_array.Contains("4"))
                        {
                            key_3_conditions_array[i] = "4";
                        }
                    }
                }
                else
                {
                    if (key_3_conditions_array.Contains("4"))
                    {
                        for (int i = 0; i < key_3_conditions_array.Length; i++)
                        {
                            if (key_3_conditions_array[i] == "4")
                            {
                                key_3_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_3_conditions_array.Length; i++)
                    {
                        if (key_3_conditions_array[i] == "0" && !key_3_conditions_array.Contains("5"))
                        {
                            key_3_conditions_array[i] = "5";
                        }
                    }
                }
                else
                {
                    if (key_3_conditions_array.Contains("5"))
                    {
                        for (int i = 0; i < key_3_conditions_array.Length; i++)
                        {
                            if (key_3_conditions_array[i] == "5")
                            {
                                key_3_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_3_conditions_array.Length; i++)
                    {
                        if (key_3_conditions_array[i] == "0" && !key_3_conditions_array.Contains("6"))
                        {
                            key_3_conditions_array[i] = "6";
                        }
                    }
                }
                else
                {
                    if (key_3_conditions_array.Contains("6"))
                    {
                        for (int i = 0; i < key_3_conditions_array.Length; i++)
                        {
                            if (key_3_conditions_array[i] == "6")
                            {
                                key_3_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_3_conditions_array.Length; i++)
                    {
                        if (key_3_conditions_array[i] == "0" && !key_3_conditions_array.Contains("7"))
                        {
                            key_3_conditions_array[i] = "7";
                        }
                    }
                }
                else
                {
                    if (key_3_conditions_array.Contains("7"))
                    {
                        for (int i = 0; i < key_3_conditions_array.Length; i++)
                        {
                            if (key_3_conditions_array[i] == "7")
                            {
                                key_3_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_3_conditions_array.Length; i++)
                    {
                        if (key_3_conditions_array[i] == "0" && !key_3_conditions_array.Contains("8"))
                        {
                            key_3_conditions_array[i] = "8";
                        }
                    }
                }
                else
                {
                    if (key_3_conditions_array.Contains("8"))
                    {
                        for (int i = 0; i < key_3_conditions_array.Length; i++)
                        {
                            if (key_3_conditions_array[i] == "8")
                            {
                                key_3_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_tbx.Text != "0")
                {
                    for (int i = 0; i < key_3_conditions_array.Length; i++)
                    {
                        if (key_3_conditions_array[i] == "0" && !key_3_conditions_array.Contains("9"))
                        {
                            key_3_conditions_array[i] = "9";
                        }
                    }
                }
                else
                {
                    if (key_3_conditions_array.Contains("9"))
                    {
                        for (int i = 0; i < key_3_conditions_array.Length; i++)
                        {
                            if (key_3_conditions_array[i] == "9")
                            {
                                key_3_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_3_conditions_array.Length; i++)
                    {
                        if (key_3_conditions_array[i] == "0" && !key_3_conditions_array.Contains("10"))
                        {
                            key_3_conditions_array[i] = "10";
                        }
                    }
                }
                else
                {
                    if (key_3_conditions_array.Contains("10"))
                    {
                        for (int i = 0; i < key_3_conditions_array.Length; i++)
                        {
                            if (key_3_conditions_array[i] == "10")
                            {
                                key_3_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                Properties.Settings.Default.key_using_3_power = special_power_tbx.Text;
                Properties.Settings.Default.key_using_3_power_under = special_power_under_tbx.Text;

                Properties.Settings.Default.key_using_3_conditions_array = ConvertStringArrayToStringJoin(key_3_conditions_array);
            }
            else if (skill_selected == 11)
            {
                // Over conditions
                Properties.Settings.Default.key_using_4_health_over_cdt = Convert.ToInt32(health_over_tbx.Text);
                Properties.Settings.Default.key_using_4_shield_over_cdt = Convert.ToInt32(shield_over_tbx.Text);
                Properties.Settings.Default.key_using_4_target_health_over_cdt = Convert.ToInt32(target_health_over_tbx.Text);
                Properties.Settings.Default.key_using_4_target_shield_over_cdt = Convert.ToInt32(target_shield_over_tbx.Text);

                // Under conditions
                Properties.Settings.Default.key_using_4_health_cdt = Convert.ToInt32(health_under_tbx.Text);
                Properties.Settings.Default.key_using_4_shield_cdt = Convert.ToInt32(shield_under_tbx.Text);
                Properties.Settings.Default.key_using_4_target_health_cdt = Convert.ToInt32(target_health_under_tbx.Text);
                Properties.Settings.Default.key_using_4_target_shield_cdt = Convert.ToInt32(target_shield_under_tbx.Text);

                Properties.Settings.Default.key_using_4_infight = use_infight_cbx.IsChecked.Value;

                // Conditions arraay
                if (health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_4_conditions_array.Length; i++)
                    {
                        if (key_4_conditions_array[i] == "0" && !key_4_conditions_array.Contains("1"))
                        {
                            key_4_conditions_array[i] = "1";
                        }
                    }
                }
                else
                {
                    if (key_4_conditions_array.Contains("1"))
                    {
                        for (int i = 0; i < key_4_conditions_array.Length; i++)
                        {
                            if (key_4_conditions_array[i] == "1")
                            {
                                key_4_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_4_conditions_array.Length; i++)
                    {
                        if (key_4_conditions_array[i] == "0" && !key_4_conditions_array.Contains("2"))
                        {
                            key_4_conditions_array[i] = "2";
                        }
                    }
                }
                else
                {
                    if (key_4_conditions_array.Contains("2"))
                    {
                        for (int i = 0; i < key_4_conditions_array.Length; i++)
                        {
                            if (key_4_conditions_array[i] == "2")
                            {
                                key_4_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_4_conditions_array.Length; i++)
                    {
                        if (key_4_conditions_array[i] == "0" && !key_4_conditions_array.Contains("3"))
                        {
                            key_4_conditions_array[i] = "3";
                        }
                    }
                }
                else
                {
                    if (key_4_conditions_array.Contains("3"))
                    {
                        for (int i = 0; i < key_4_conditions_array.Length; i++)
                        {
                            if (key_4_conditions_array[i] == "3")
                            {
                                key_4_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_4_conditions_array.Length; i++)
                    {
                        if (key_4_conditions_array[i] == "0" && !key_4_conditions_array.Contains("4"))
                        {
                            key_4_conditions_array[i] = "4";
                        }
                    }
                }
                else
                {
                    if (key_4_conditions_array.Contains("4"))
                    {
                        for (int i = 0; i < key_4_conditions_array.Length; i++)
                        {
                            if (key_4_conditions_array[i] == "4")
                            {
                                key_4_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_4_conditions_array.Length; i++)
                    {
                        if (key_4_conditions_array[i] == "0" && !key_4_conditions_array.Contains("5"))
                        {
                            key_4_conditions_array[i] = "5";
                        }
                    }
                }
                else
                {
                    if (key_4_conditions_array.Contains("5"))
                    {
                        for (int i = 0; i < key_4_conditions_array.Length; i++)
                        {
                            if (key_4_conditions_array[i] == "5")
                            {
                                key_4_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_4_conditions_array.Length; i++)
                    {
                        if (key_4_conditions_array[i] == "0" && !key_4_conditions_array.Contains("6"))
                        {
                            key_4_conditions_array[i] = "6";
                        }
                    }
                }
                else
                {
                    if (key_4_conditions_array.Contains("6"))
                    {
                        for (int i = 0; i < key_4_conditions_array.Length; i++)
                        {
                            if (key_4_conditions_array[i] == "6")
                            {
                                key_4_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_4_conditions_array.Length; i++)
                    {
                        if (key_4_conditions_array[i] == "0" && !key_4_conditions_array.Contains("7"))
                        {
                            key_4_conditions_array[i] = "7";
                        }
                    }
                }
                else
                {
                    if (key_4_conditions_array.Contains("7"))
                    {
                        for (int i = 0; i < key_4_conditions_array.Length; i++)
                        {
                            if (key_4_conditions_array[i] == "7")
                            {
                                key_4_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_4_conditions_array.Length; i++)
                    {
                        if (key_4_conditions_array[i] == "0" && !key_4_conditions_array.Contains("8"))
                        {
                            key_4_conditions_array[i] = "8";
                        }
                    }
                }
                else
                {
                    if (key_4_conditions_array.Contains("8"))
                    {
                        for (int i = 0; i < key_4_conditions_array.Length; i++)
                        {
                            if (key_4_conditions_array[i] == "8")
                            {
                                key_4_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_tbx.Text != "0")
                {
                    for (int i = 0; i < key_4_conditions_array.Length; i++)
                    {
                        if (key_4_conditions_array[i] == "0" && !key_4_conditions_array.Contains("9"))
                        {
                            key_4_conditions_array[i] = "9";
                        }
                    }
                }
                else
                {
                    if (key_4_conditions_array.Contains("9"))
                    {
                        for (int i = 0; i < key_4_conditions_array.Length; i++)
                        {
                            if (key_4_conditions_array[i] == "9")
                            {
                                key_4_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_4_conditions_array.Length; i++)
                    {
                        if (key_4_conditions_array[i] == "0" && !key_4_conditions_array.Contains("10"))
                        {
                            key_4_conditions_array[i] = "10";
                        }
                    }
                }
                else
                {
                    if (key_4_conditions_array.Contains("10"))
                    {
                        for (int i = 0; i < key_4_conditions_array.Length; i++)
                        {
                            if (key_4_conditions_array[i] == "10")
                            {
                                key_4_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                Properties.Settings.Default.key_using_4_power = special_power_tbx.Text;
                Properties.Settings.Default.key_using_4_power_under = special_power_under_tbx.Text;

                Properties.Settings.Default.key_using_4_conditions_array = ConvertStringArrayToStringJoin(key_4_conditions_array);
            }
            else if (skill_selected == 12) 
            {
                // Over conditions
                Properties.Settings.Default.key_using_5_health_over_cdt = Convert.ToInt32(health_over_tbx.Text);
                Properties.Settings.Default.key_using_5_shield_over_cdt = Convert.ToInt32(shield_over_tbx.Text);
                Properties.Settings.Default.key_using_5_target_health_over_cdt = Convert.ToInt32(target_health_over_tbx.Text);
                Properties.Settings.Default.key_using_5_target_shield_over_cdt = Convert.ToInt32(target_shield_over_tbx.Text);

                // Under conditions
                Properties.Settings.Default.key_using_5_health_cdt = Convert.ToInt32(health_under_tbx.Text);
                Properties.Settings.Default.key_using_5_shield_cdt = Convert.ToInt32(shield_under_tbx.Text);
                Properties.Settings.Default.key_using_5_target_health_cdt = Convert.ToInt32(target_health_under_tbx.Text);
                Properties.Settings.Default.key_using_5_target_shield_cdt = Convert.ToInt32(target_shield_under_tbx.Text);

                Properties.Settings.Default.key_using_5_infight = use_infight_cbx.IsChecked.Value;

                // Conditions arraay
                if (health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_5_conditions_array.Length; i++)
                    {
                        if (key_5_conditions_array[i] == "0" && !key_5_conditions_array.Contains("1"))
                        {
                            key_5_conditions_array[i] = "1";
                        }
                    }
                }
                else
                {
                    if (key_5_conditions_array.Contains("1"))
                    {
                        for (int i = 0; i < key_5_conditions_array.Length; i++)
                        {
                            if (key_5_conditions_array[i] == "1")
                            {
                                key_5_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_5_conditions_array.Length; i++)
                    {
                        if (key_5_conditions_array[i] == "0" && !key_5_conditions_array.Contains("2"))
                        {
                            key_5_conditions_array[i] = "2";
                        }
                    }
                }
                else
                {
                    if (key_5_conditions_array.Contains("2"))
                    {
                        for (int i = 0; i < key_5_conditions_array.Length; i++)
                        {
                            if (key_5_conditions_array[i] == "2")
                            {
                                key_5_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_5_conditions_array.Length; i++)
                    {
                        if (key_5_conditions_array[i] == "0" && !key_5_conditions_array.Contains("3"))
                        {
                            key_5_conditions_array[i] = "3";
                        }
                    }
                }
                else
                {
                    if (key_5_conditions_array.Contains("3"))
                    {
                        for (int i = 0; i < key_5_conditions_array.Length; i++)
                        {
                            if (key_5_conditions_array[i] == "3")
                            {
                                key_5_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_5_conditions_array.Length; i++)
                    {
                        if (key_5_conditions_array[i] == "0" && !key_5_conditions_array.Contains("4"))
                        {
                            key_5_conditions_array[i] = "4";
                        }
                    }
                }
                else
                {
                    if (key_5_conditions_array.Contains("4"))
                    {
                        for (int i = 0; i < key_5_conditions_array.Length; i++)
                        {
                            if (key_5_conditions_array[i] == "4")
                            {
                                key_5_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_5_conditions_array.Length; i++)
                    {
                        if (key_5_conditions_array[i] == "0" && !key_5_conditions_array.Contains("5"))
                        {
                            key_5_conditions_array[i] = "5";
                        }
                    }
                }
                else
                {
                    if (key_5_conditions_array.Contains("5"))
                    {
                        for (int i = 0; i < key_5_conditions_array.Length; i++)
                        {
                            if (key_5_conditions_array[i] == "5")
                            {
                                key_5_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_5_conditions_array.Length; i++)
                    {
                        if (key_5_conditions_array[i] == "0" && !key_5_conditions_array.Contains("6"))
                        {
                            key_5_conditions_array[i] = "6";
                        }
                    }
                }
                else
                {
                    if (key_5_conditions_array.Contains("6"))
                    {
                        for (int i = 0; i < key_5_conditions_array.Length; i++)
                        {
                            if (key_5_conditions_array[i] == "6")
                            {
                                key_5_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_5_conditions_array.Length; i++)
                    {
                        if (key_5_conditions_array[i] == "0" && !key_5_conditions_array.Contains("7"))
                        {
                            key_5_conditions_array[i] = "7";
                        }
                    }
                }
                else
                {
                    if (key_5_conditions_array.Contains("7"))
                    {
                        for (int i = 0; i < key_5_conditions_array.Length; i++)
                        {
                            if (key_5_conditions_array[i] == "7")
                            {
                                key_5_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_5_conditions_array.Length; i++)
                    {
                        if (key_5_conditions_array[i] == "0" && !key_5_conditions_array.Contains("8"))
                        {
                            key_5_conditions_array[i] = "8";
                        }
                    }
                }
                else
                {
                    if (key_5_conditions_array.Contains("8"))
                    {
                        for (int i = 0; i < key_5_conditions_array.Length; i++)
                        {
                            if (key_5_conditions_array[i] == "8")
                            {
                                key_5_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_tbx.Text != "0")
                {
                    for (int i = 0; i < key_5_conditions_array.Length; i++)
                    {
                        if (key_5_conditions_array[i] == "0" && !key_5_conditions_array.Contains("9"))
                        {
                            key_5_conditions_array[i] = "9";
                        }
                    }
                }
                else
                {
                    if (key_5_conditions_array.Contains("9"))
                    {
                        for (int i = 0; i < key_5_conditions_array.Length; i++)
                        {
                            if (key_5_conditions_array[i] == "9")
                            {
                                key_5_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_5_conditions_array.Length; i++)
                    {
                        if (key_5_conditions_array[i] == "0" && !key_5_conditions_array.Contains("10"))
                        {
                            key_5_conditions_array[i] = "10";
                        }
                    }
                }
                else
                {
                    if (key_5_conditions_array.Contains("10"))
                    {
                        for (int i = 0; i < key_5_conditions_array.Length; i++)
                        {
                            if (key_5_conditions_array[i] == "10")
                            {
                                key_5_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                Properties.Settings.Default.key_using_5_power = special_power_tbx.Text;
                Properties.Settings.Default.key_using_5_power_under = special_power_under_tbx.Text;

                Properties.Settings.Default.key_using_5_conditions_array = ConvertStringArrayToStringJoin(key_5_conditions_array);
            }
            else if (skill_selected == 13)
            {
                // Over conditions
                Properties.Settings.Default.key_using_6_health_over_cdt = Convert.ToInt32(health_over_tbx.Text);
                Properties.Settings.Default.key_using_6_shield_over_cdt = Convert.ToInt32(shield_over_tbx.Text);
                Properties.Settings.Default.key_using_6_target_health_over_cdt = Convert.ToInt32(target_health_over_tbx.Text);
                Properties.Settings.Default.key_using_6_target_shield_over_cdt = Convert.ToInt32(target_shield_over_tbx.Text);

                // Under conditions
                Properties.Settings.Default.key_using_6_health_cdt = Convert.ToInt32(health_under_tbx.Text);
                Properties.Settings.Default.key_using_6_shield_cdt = Convert.ToInt32(shield_under_tbx.Text);
                Properties.Settings.Default.key_using_6_target_health_cdt = Convert.ToInt32(target_health_under_tbx.Text);
                Properties.Settings.Default.key_using_6_target_shield_cdt = Convert.ToInt32(target_shield_under_tbx.Text);

                Properties.Settings.Default.key_using_6_infight = use_infight_cbx.IsChecked.Value;

                // Conditions arraay
                if (health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_6_conditions_array.Length; i++)
                    {
                        if (key_6_conditions_array[i] == "0" && !key_6_conditions_array.Contains("1"))
                        {
                            key_6_conditions_array[i] = "1";
                        }
                    }
                }
                else
                {
                    if (key_6_conditions_array.Contains("1"))
                    {
                        for (int i = 0; i < key_6_conditions_array.Length; i++)
                        {
                            if (key_6_conditions_array[i] == "1")
                            {
                                key_6_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_6_conditions_array.Length; i++)
                    {
                        if (key_6_conditions_array[i] == "0" && !key_6_conditions_array.Contains("2"))
                        {
                            key_6_conditions_array[i] = "2";
                        }
                    }
                }
                else
                {
                    if (key_6_conditions_array.Contains("2"))
                    {
                        for (int i = 0; i < key_6_conditions_array.Length; i++)
                        {
                            if (key_6_conditions_array[i] == "2")
                            {
                                key_6_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_6_conditions_array.Length; i++)
                    {
                        if (key_6_conditions_array[i] == "0" && !key_6_conditions_array.Contains("3"))
                        {
                            key_6_conditions_array[i] = "3";
                        }
                    }
                }
                else
                {
                    if (key_6_conditions_array.Contains("3"))
                    {
                        for (int i = 0; i < key_6_conditions_array.Length; i++)
                        {
                            if (key_6_conditions_array[i] == "3")
                            {
                                key_6_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_6_conditions_array.Length; i++)
                    {
                        if (key_6_conditions_array[i] == "0" && !key_6_conditions_array.Contains("4"))
                        {
                            key_6_conditions_array[i] = "4";
                        }
                    }
                }
                else
                {
                    if (key_6_conditions_array.Contains("4"))
                    {
                        for (int i = 0; i < key_6_conditions_array.Length; i++)
                        {
                            if (key_6_conditions_array[i] == "4")
                            {
                                key_6_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_6_conditions_array.Length; i++)
                    {
                        if (key_6_conditions_array[i] == "0" && !key_6_conditions_array.Contains("5"))
                        {
                            key_6_conditions_array[i] = "5";
                        }
                    }
                }
                else
                {
                    if (key_6_conditions_array.Contains("5"))
                    {
                        for (int i = 0; i < key_6_conditions_array.Length; i++)
                        {
                            if (key_6_conditions_array[i] == "5")
                            {
                                key_6_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_6_conditions_array.Length; i++)
                    {
                        if (key_6_conditions_array[i] == "0" && !key_6_conditions_array.Contains("6"))
                        {
                            key_6_conditions_array[i] = "6";
                        }
                    }
                }
                else
                {
                    if (key_6_conditions_array.Contains("6"))
                    {
                        for (int i = 0; i < key_6_conditions_array.Length; i++)
                        {
                            if (key_6_conditions_array[i] == "6")
                            {
                                key_6_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_6_conditions_array.Length; i++)
                    {
                        if (key_6_conditions_array[i] == "0" && !key_6_conditions_array.Contains("7"))
                        {
                            key_6_conditions_array[i] = "7";
                        }
                    }
                }
                else
                {
                    if (key_6_conditions_array.Contains("7"))
                    {
                        for (int i = 0; i < key_6_conditions_array.Length; i++)
                        {
                            if (key_6_conditions_array[i] == "7")
                            {
                                key_6_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_6_conditions_array.Length; i++)
                    {
                        if (key_6_conditions_array[i] == "0" && !key_6_conditions_array.Contains("8"))
                        {
                            key_6_conditions_array[i] = "8";
                        }
                    }
                }
                else
                {
                    if (key_6_conditions_array.Contains("8"))
                    {
                        for (int i = 0; i < key_6_conditions_array.Length; i++)
                        {
                            if (key_6_conditions_array[i] == "8")
                            {
                                key_6_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_tbx.Text != "0")
                {
                    for (int i = 0; i < key_6_conditions_array.Length; i++)
                    {
                        if (key_6_conditions_array[i] == "0" && !key_6_conditions_array.Contains("9"))
                        {
                            key_6_conditions_array[i] = "9";
                        }
                    }
                }
                else
                {
                    if (key_6_conditions_array.Contains("9"))
                    {
                        for (int i = 0; i < key_6_conditions_array.Length; i++)
                        {
                            if (key_6_conditions_array[i] == "9")
                            {
                                key_6_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_6_conditions_array.Length; i++)
                    {
                        if (key_6_conditions_array[i] == "0" && !key_6_conditions_array.Contains("10"))
                        {
                            key_6_conditions_array[i] = "10";
                        }
                    }
                }
                else
                {
                    if (key_6_conditions_array.Contains("10"))
                    {
                        for (int i = 0; i < key_6_conditions_array.Length; i++)
                        {
                            if (key_6_conditions_array[i] == "10")
                            {
                                key_6_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                Properties.Settings.Default.key_using_6_power = special_power_tbx.Text;
                Properties.Settings.Default.key_using_6_power_under = special_power_under_tbx.Text;

                Properties.Settings.Default.key_using_6_conditions_array = ConvertStringArrayToStringJoin(key_6_conditions_array);
            }
            else if (skill_selected == 14) 
            {
                // Over conditions
                Properties.Settings.Default.key_using_7_health_over_cdt = Convert.ToInt32(health_over_tbx.Text);
                Properties.Settings.Default.key_using_7_shield_over_cdt = Convert.ToInt32(shield_over_tbx.Text);
                Properties.Settings.Default.key_using_7_target_health_over_cdt = Convert.ToInt32(target_health_over_tbx.Text);
                Properties.Settings.Default.key_using_7_target_shield_over_cdt = Convert.ToInt32(target_shield_over_tbx.Text);

                // Under conditions
                Properties.Settings.Default.key_using_7_health_cdt = Convert.ToInt32(health_under_tbx.Text);
                Properties.Settings.Default.key_using_7_shield_cdt = Convert.ToInt32(shield_under_tbx.Text);
                Properties.Settings.Default.key_using_7_target_health_cdt = Convert.ToInt32(target_health_under_tbx.Text);
                Properties.Settings.Default.key_using_7_target_shield_cdt = Convert.ToInt32(target_shield_under_tbx.Text);

                Properties.Settings.Default.key_using_7_infight = use_infight_cbx.IsChecked.Value;

                // Conditions arraay
                if (health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_7_conditions_array.Length; i++)
                    {
                        if (key_7_conditions_array[i] == "0" && !key_7_conditions_array.Contains("1"))
                        {
                            key_7_conditions_array[i] = "1";
                        }
                    }
                }
                else
                {
                    if (key_7_conditions_array.Contains("1"))
                    {
                        for (int i = 0; i < key_7_conditions_array.Length; i++)
                        {
                            if (key_7_conditions_array[i] == "1")
                            {
                                key_7_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_7_conditions_array.Length; i++)
                    {
                        if (key_7_conditions_array[i] == "0" && !key_7_conditions_array.Contains("2"))
                        {
                            key_7_conditions_array[i] = "2";
                        }
                    }
                }
                else
                {
                    if (key_7_conditions_array.Contains("2"))
                    {
                        for (int i = 0; i < key_7_conditions_array.Length; i++)
                        {
                            if (key_7_conditions_array[i] == "2")
                            {
                                key_7_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_7_conditions_array.Length; i++)
                    {
                        if (key_7_conditions_array[i] == "0" && !key_7_conditions_array.Contains("3"))
                        {
                            key_7_conditions_array[i] = "3";
                        }
                    }
                }
                else
                {
                    if (key_7_conditions_array.Contains("3"))
                    {
                        for (int i = 0; i < key_7_conditions_array.Length; i++)
                        {
                            if (key_7_conditions_array[i] == "3")
                            {
                                key_7_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_7_conditions_array.Length; i++)
                    {
                        if (key_7_conditions_array[i] == "0" && !key_7_conditions_array.Contains("4"))
                        {
                            key_7_conditions_array[i] = "4";
                        }
                    }
                }
                else
                {
                    if (key_7_conditions_array.Contains("4"))
                    {
                        for (int i = 0; i < key_7_conditions_array.Length; i++)
                        {
                            if (key_7_conditions_array[i] == "4")
                            {
                                key_7_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_7_conditions_array.Length; i++)
                    {
                        if (key_7_conditions_array[i] == "0" && !key_7_conditions_array.Contains("5"))
                        {
                            key_7_conditions_array[i] = "5";
                        }
                    }
                }
                else
                {
                    if (key_7_conditions_array.Contains("5"))
                    {
                        for (int i = 0; i < key_7_conditions_array.Length; i++)
                        {
                            if (key_7_conditions_array[i] == "5")
                            {
                                key_7_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_7_conditions_array.Length; i++)
                    {
                        if (key_7_conditions_array[i] == "0" && !key_7_conditions_array.Contains("6"))
                        {
                            key_7_conditions_array[i] = "6";
                        }
                    }
                }
                else
                {
                    if (key_7_conditions_array.Contains("6"))
                    {
                        for (int i = 0; i < key_7_conditions_array.Length; i++)
                        {
                            if (key_7_conditions_array[i] == "6")
                            {
                                key_7_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_7_conditions_array.Length; i++)
                    {
                        if (key_7_conditions_array[i] == "0" && !key_7_conditions_array.Contains("7"))
                        {
                            key_7_conditions_array[i] = "7";
                        }
                    }
                }
                else
                {
                    if (key_7_conditions_array.Contains("7"))
                    {
                        for (int i = 0; i < key_7_conditions_array.Length; i++)
                        {
                            if (key_7_conditions_array[i] == "7")
                            {
                                key_7_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_7_conditions_array.Length; i++)
                    {
                        if (key_7_conditions_array[i] == "0" && !key_7_conditions_array.Contains("8"))
                        {
                            key_7_conditions_array[i] = "8";
                        }
                    }
                }
                else
                {
                    if (key_7_conditions_array.Contains("8"))
                    {
                        for (int i = 0; i < key_7_conditions_array.Length; i++)
                        {
                            if (key_7_conditions_array[i] == "8")
                            {
                                key_7_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_tbx.Text != "0")
                {
                    for (int i = 0; i < key_7_conditions_array.Length; i++)
                    {
                        if (key_7_conditions_array[i] == "0" && !key_7_conditions_array.Contains("9"))
                        {
                            key_7_conditions_array[i] = "9";
                        }
                    }
                }
                else
                {
                    if (key_7_conditions_array.Contains("9"))
                    {
                        for (int i = 0; i < key_7_conditions_array.Length; i++)
                        {
                            if (key_7_conditions_array[i] == "9")
                            {
                                key_7_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_7_conditions_array.Length; i++)
                    {
                        if (key_7_conditions_array[i] == "0" && !key_7_conditions_array.Contains("10"))
                        {
                            key_7_conditions_array[i] = "10";
                        }
                    }
                }
                else
                {
                    if (key_7_conditions_array.Contains("10"))
                    {
                        for (int i = 0; i < key_7_conditions_array.Length; i++)
                        {
                            if (key_7_conditions_array[i] == "10")
                            {
                                key_7_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                Properties.Settings.Default.key_using_7_power = special_power_tbx.Text;
                Properties.Settings.Default.key_using_7_power_under = special_power_under_tbx.Text;

                Properties.Settings.Default.key_using_7_conditions_array = ConvertStringArrayToStringJoin(key_7_conditions_array);
            }
            else if (skill_selected == 15) 
            {
                // Over conditions
                Properties.Settings.Default.key_using_8_health_over_cdt = Convert.ToInt32(health_over_tbx.Text);
                Properties.Settings.Default.key_using_8_shield_over_cdt = Convert.ToInt32(shield_over_tbx.Text);
                Properties.Settings.Default.key_using_8_target_health_over_cdt = Convert.ToInt32(target_health_over_tbx.Text);
                Properties.Settings.Default.key_using_8_target_shield_over_cdt = Convert.ToInt32(target_shield_over_tbx.Text);

                // Under conditions
                Properties.Settings.Default.key_using_8_health_cdt = Convert.ToInt32(health_under_tbx.Text);
                Properties.Settings.Default.key_using_8_shield_cdt = Convert.ToInt32(shield_under_tbx.Text);
                Properties.Settings.Default.key_using_8_target_health_cdt = Convert.ToInt32(target_health_under_tbx.Text);
                Properties.Settings.Default.key_using_8_target_shield_cdt = Convert.ToInt32(target_shield_under_tbx.Text);

                Properties.Settings.Default.key_using_8_infight = use_infight_cbx.IsChecked.Value;

                // Conditions arraay
                if (health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_8_conditions_array.Length; i++)
                    {
                        if (key_8_conditions_array[i] == "0" && !key_8_conditions_array.Contains("1"))
                        {
                            key_8_conditions_array[i] = "1";
                        }
                    }
                }
                else
                {
                    if (key_8_conditions_array.Contains("1"))
                    {
                        for (int i = 0; i < key_8_conditions_array.Length; i++)
                        {
                            if (key_8_conditions_array[i] == "1")
                            {
                                key_8_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_8_conditions_array.Length; i++)
                    {
                        if (key_8_conditions_array[i] == "0" && !key_8_conditions_array.Contains("2"))
                        {
                            key_8_conditions_array[i] = "2";
                        }
                    }
                }
                else
                {
                    if (key_8_conditions_array.Contains("2"))
                    {
                        for (int i = 0; i < key_8_conditions_array.Length; i++)
                        {
                            if (key_8_conditions_array[i] == "2")
                            {
                                key_8_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_8_conditions_array.Length; i++)
                    {
                        if (key_8_conditions_array[i] == "0" && !key_8_conditions_array.Contains("3"))
                        {
                            key_8_conditions_array[i] = "3";
                        }
                    }
                }
                else
                {
                    if (key_8_conditions_array.Contains("3"))
                    {
                        for (int i = 0; i < key_8_conditions_array.Length; i++)
                        {
                            if (key_8_conditions_array[i] == "3")
                            {
                                key_8_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_8_conditions_array.Length; i++)
                    {
                        if (key_8_conditions_array[i] == "0" && !key_8_conditions_array.Contains("4"))
                        {
                            key_8_conditions_array[i] = "4";
                        }
                    }
                }
                else
                {
                    if (key_8_conditions_array.Contains("4"))
                    {
                        for (int i = 0; i < key_8_conditions_array.Length; i++)
                        {
                            if (key_8_conditions_array[i] == "4")
                            {
                                key_8_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_8_conditions_array.Length; i++)
                    {
                        if (key_8_conditions_array[i] == "0" && !key_8_conditions_array.Contains("5"))
                        {
                            key_8_conditions_array[i] = "5";
                        }
                    }
                }
                else
                {
                    if (key_8_conditions_array.Contains("5"))
                    {
                        for (int i = 0; i < key_8_conditions_array.Length; i++)
                        {
                            if (key_8_conditions_array[i] == "5")
                            {
                                key_8_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_8_conditions_array.Length; i++)
                    {
                        if (key_8_conditions_array[i] == "0" && !key_8_conditions_array.Contains("6"))
                        {
                            key_8_conditions_array[i] = "6";
                        }
                    }
                }
                else
                {
                    if (key_8_conditions_array.Contains("6"))
                    {
                        for (int i = 0; i < key_8_conditions_array.Length; i++)
                        {
                            if (key_8_conditions_array[i] == "6")
                            {
                                key_8_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_8_conditions_array.Length; i++)
                    {
                        if (key_8_conditions_array[i] == "0" && !key_8_conditions_array.Contains("7"))
                        {
                            key_8_conditions_array[i] = "7";
                        }
                    }
                }
                else
                {
                    if (key_8_conditions_array.Contains("7"))
                    {
                        for (int i = 0; i < key_8_conditions_array.Length; i++)
                        {
                            if (key_8_conditions_array[i] == "7")
                            {
                                key_8_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_8_conditions_array.Length; i++)
                    {
                        if (key_8_conditions_array[i] == "0" && !key_8_conditions_array.Contains("8"))
                        {
                            key_8_conditions_array[i] = "8";
                        }
                    }
                }
                else
                {
                    if (key_8_conditions_array.Contains("8"))
                    {
                        for (int i = 0; i < key_8_conditions_array.Length; i++)
                        {
                            if (key_8_conditions_array[i] == "8")
                            {
                                key_8_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_tbx.Text != "0")
                {
                    for (int i = 0; i < key_8_conditions_array.Length; i++)
                    {
                        if (key_8_conditions_array[i] == "0" && !key_8_conditions_array.Contains("9"))
                        {
                            key_8_conditions_array[i] = "9";
                        }
                    }
                }
                else
                {
                    if (key_8_conditions_array.Contains("9"))
                    {
                        for (int i = 0; i < key_8_conditions_array.Length; i++)
                        {
                            if (key_8_conditions_array[i] == "9")
                            {
                                key_8_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_8_conditions_array.Length; i++)
                    {
                        if (key_8_conditions_array[i] == "0" && !key_8_conditions_array.Contains("10"))
                        {
                            key_8_conditions_array[i] = "10";
                        }
                    }
                }
                else
                {
                    if (key_8_conditions_array.Contains("10"))
                    {
                        for (int i = 0; i < key_8_conditions_array.Length; i++)
                        {
                            if (key_8_conditions_array[i] == "10")
                            {
                                key_8_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                Properties.Settings.Default.key_using_8_power = special_power_tbx.Text;
                Properties.Settings.Default.key_using_8_power_under = special_power_under_tbx.Text;

                Properties.Settings.Default.key_using_8_conditions_array = ConvertStringArrayToStringJoin(key_8_conditions_array);
            }
            else if (skill_selected == 16)
            {
                // Over conditions
                Properties.Settings.Default.key_using_9_health_over_cdt = Convert.ToInt32(health_over_tbx.Text);
                Properties.Settings.Default.key_using_9_shield_over_cdt = Convert.ToInt32(shield_over_tbx.Text);
                Properties.Settings.Default.key_using_9_target_health_over_cdt = Convert.ToInt32(target_health_over_tbx.Text);
                Properties.Settings.Default.key_using_9_target_shield_over_cdt = Convert.ToInt32(target_shield_over_tbx.Text);

                // Under conditions
                Properties.Settings.Default.key_using_9_health_cdt = Convert.ToInt32(health_under_tbx.Text);
                Properties.Settings.Default.key_using_9_shield_cdt = Convert.ToInt32(shield_under_tbx.Text);
                Properties.Settings.Default.key_using_9_target_health_cdt = Convert.ToInt32(target_health_under_tbx.Text);
                Properties.Settings.Default.key_using_9_target_shield_cdt = Convert.ToInt32(target_shield_under_tbx.Text);

                Properties.Settings.Default.key_using_9_infight = use_infight_cbx.IsChecked.Value;

                // Conditions arraay
                if (health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_9_conditions_array.Length; i++)
                    {
                        if (key_9_conditions_array[i] == "0" && !key_9_conditions_array.Contains("1"))
                        {
                            key_9_conditions_array[i] = "1";
                        }
                    }
                }
                else
                {
                    if (key_9_conditions_array.Contains("1"))
                    {
                        for (int i = 0; i < key_9_conditions_array.Length; i++)
                        {
                            if (key_9_conditions_array[i] == "1")
                            {
                                key_9_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_9_conditions_array.Length; i++)
                    {
                        if (key_9_conditions_array[i] == "0" && !key_9_conditions_array.Contains("2"))
                        {
                            key_9_conditions_array[i] = "2";
                        }
                    }
                }
                else
                {
                    if (key_9_conditions_array.Contains("2"))
                    {
                        for (int i = 0; i < key_9_conditions_array.Length; i++)
                        {
                            if (key_9_conditions_array[i] == "2")
                            {
                                key_9_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_9_conditions_array.Length; i++)
                    {
                        if (key_9_conditions_array[i] == "0" && !key_9_conditions_array.Contains("3"))
                        {
                            key_9_conditions_array[i] = "3";
                        }
                    }
                }
                else
                {
                    if (key_9_conditions_array.Contains("3"))
                    {
                        for (int i = 0; i < key_9_conditions_array.Length; i++)
                        {
                            if (key_9_conditions_array[i] == "3")
                            {
                                key_9_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_9_conditions_array.Length; i++)
                    {
                        if (key_9_conditions_array[i] == "0" && !key_9_conditions_array.Contains("4"))
                        {
                            key_9_conditions_array[i] = "4";
                        }
                    }
                }
                else
                {
                    if (key_9_conditions_array.Contains("4"))
                    {
                        for (int i = 0; i < key_9_conditions_array.Length; i++)
                        {
                            if (key_9_conditions_array[i] == "4")
                            {
                                key_9_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_9_conditions_array.Length; i++)
                    {
                        if (key_9_conditions_array[i] == "0" && !key_9_conditions_array.Contains("5"))
                        {
                            key_9_conditions_array[i] = "5";
                        }
                    }
                }
                else
                {
                    if (key_9_conditions_array.Contains("5"))
                    {
                        for (int i = 0; i < key_9_conditions_array.Length; i++)
                        {
                            if (key_9_conditions_array[i] == "5")
                            {
                                key_9_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_9_conditions_array.Length; i++)
                    {
                        if (key_9_conditions_array[i] == "0" && !key_9_conditions_array.Contains("6"))
                        {
                            key_9_conditions_array[i] = "6";
                        }
                    }
                }
                else
                {
                    if (key_9_conditions_array.Contains("6"))
                    {
                        for (int i = 0; i < key_9_conditions_array.Length; i++)
                        {
                            if (key_9_conditions_array[i] == "6")
                            {
                                key_9_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_9_conditions_array.Length; i++)
                    {
                        if (key_9_conditions_array[i] == "0" && !key_9_conditions_array.Contains("7"))
                        {
                            key_9_conditions_array[i] = "7";
                        }
                    }
                }
                else
                {
                    if (key_9_conditions_array.Contains("7"))
                    {
                        for (int i = 0; i < key_9_conditions_array.Length; i++)
                        {
                            if (key_9_conditions_array[i] == "7")
                            {
                                key_9_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_9_conditions_array.Length; i++)
                    {
                        if (key_9_conditions_array[i] == "0" && !key_9_conditions_array.Contains("8"))
                        {
                            key_9_conditions_array[i] = "8";
                        }
                    }
                }
                else
                {
                    if (key_9_conditions_array.Contains("8"))
                    {
                        for (int i = 0; i < key_9_conditions_array.Length; i++)
                        {
                            if (key_9_conditions_array[i] == "8")
                            {
                                key_9_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_tbx.Text != "0")
                {
                    for (int i = 0; i < key_9_conditions_array.Length; i++)
                    {
                        if (key_9_conditions_array[i] == "0" && !key_9_conditions_array.Contains("9"))
                        {
                            key_9_conditions_array[i] = "9";
                        }
                    }
                }
                else
                {
                    if (key_9_conditions_array.Contains("9"))
                    {
                        for (int i = 0; i < key_9_conditions_array.Length; i++)
                        {
                            if (key_9_conditions_array[i] == "9")
                            {
                                key_9_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_9_conditions_array.Length; i++)
                    {
                        if (key_9_conditions_array[i] == "0" && !key_9_conditions_array.Contains("10"))
                        {
                            key_9_conditions_array[i] = "10";
                        }
                    }
                }
                else
                {
                    if (key_9_conditions_array.Contains("10"))
                    {
                        for (int i = 0; i < key_9_conditions_array.Length; i++)
                        {
                            if (key_9_conditions_array[i] == "10")
                            {
                                key_9_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                Properties.Settings.Default.key_using_9_power = special_power_tbx.Text;
                Properties.Settings.Default.key_using_9_power_under = special_power_under_tbx.Text;

                Properties.Settings.Default.key_using_9_conditions_array = ConvertStringArrayToStringJoin(key_9_conditions_array);
            }
            else if (skill_selected == 17)
            {
                // Over conditions
                Properties.Settings.Default.key_using_10_health_over_cdt = Convert.ToInt32(health_over_tbx.Text);
                Properties.Settings.Default.key_using_10_shield_over_cdt = Convert.ToInt32(shield_over_tbx.Text);
                Properties.Settings.Default.key_using_10_target_health_over_cdt = Convert.ToInt32(target_health_over_tbx.Text);
                Properties.Settings.Default.key_using_10_target_shield_over_cdt = Convert.ToInt32(target_shield_over_tbx.Text);

                // Under conditions
                Properties.Settings.Default.key_using_10_health_cdt = Convert.ToInt32(health_under_tbx.Text);
                Properties.Settings.Default.key_using_10_shield_cdt = Convert.ToInt32(shield_under_tbx.Text);
                Properties.Settings.Default.key_using_10_target_health_cdt = Convert.ToInt32(target_health_under_tbx.Text);
                Properties.Settings.Default.key_using_10_target_shield_cdt = Convert.ToInt32(target_shield_under_tbx.Text);

                Properties.Settings.Default.key_using_10_infight = use_infight_cbx.IsChecked.Value;

                // Conditions arraay
                if (health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_10_conditions_array.Length; i++)
                    {
                        if (key_10_conditions_array[i] == "0" && !key_10_conditions_array.Contains("1"))
                        {
                            key_10_conditions_array[i] = "1";
                        }
                    }
                }
                else
                {
                    if (key_10_conditions_array.Contains("1"))
                    {
                        for (int i = 0; i < key_10_conditions_array.Length; i++)
                        {
                            if (key_10_conditions_array[i] == "1")
                            {
                                key_10_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_10_conditions_array.Length; i++)
                    {
                        if (key_10_conditions_array[i] == "0" && !key_10_conditions_array.Contains("2"))
                        {
                            key_10_conditions_array[i] = "2";
                        }
                    }
                }
                else
                {
                    if (key_10_conditions_array.Contains("2"))
                    {
                        for (int i = 0; i < key_10_conditions_array.Length; i++)
                        {
                            if (key_10_conditions_array[i] == "2")
                            {
                                key_10_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_10_conditions_array.Length; i++)
                    {
                        if (key_10_conditions_array[i] == "0" && !key_10_conditions_array.Contains("3"))
                        {
                            key_10_conditions_array[i] = "3";
                        }
                    }
                }
                else
                {
                    if (key_10_conditions_array.Contains("3"))
                    {
                        for (int i = 0; i < key_10_conditions_array.Length; i++)
                        {
                            if (key_10_conditions_array[i] == "3")
                            {
                                key_10_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_10_conditions_array.Length; i++)
                    {
                        if (key_10_conditions_array[i] == "0" && !key_10_conditions_array.Contains("4"))
                        {
                            key_10_conditions_array[i] = "4";
                        }
                    }
                }
                else
                {
                    if (key_10_conditions_array.Contains("4"))
                    {
                        for (int i = 0; i < key_10_conditions_array.Length; i++)
                        {
                            if (key_10_conditions_array[i] == "4")
                            {
                                key_10_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_10_conditions_array.Length; i++)
                    {
                        if (key_10_conditions_array[i] == "0" && !key_10_conditions_array.Contains("5"))
                        {
                            key_10_conditions_array[i] = "5";
                        }
                    }
                }
                else
                {
                    if (key_10_conditions_array.Contains("5"))
                    {
                        for (int i = 0; i < key_10_conditions_array.Length; i++)
                        {
                            if (key_10_conditions_array[i] == "5")
                            {
                                key_10_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_health_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_10_conditions_array.Length; i++)
                    {
                        if (key_10_conditions_array[i] == "0" && !key_10_conditions_array.Contains("6"))
                        {
                            key_10_conditions_array[i] = "6";
                        }
                    }
                }
                else
                {
                    if (key_10_conditions_array.Contains("6"))
                    {
                        for (int i = 0; i < key_10_conditions_array.Length; i++)
                        {
                            if (key_10_conditions_array[i] == "6")
                            {
                                key_10_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_over_tbx.Text != "0")
                {
                    for (int i = 0; i < key_10_conditions_array.Length; i++)
                    {
                        if (key_10_conditions_array[i] == "0" && !key_10_conditions_array.Contains("7"))
                        {
                            key_10_conditions_array[i] = "7";
                        }
                    }
                }
                else
                {
                    if (key_10_conditions_array.Contains("7"))
                    {
                        for (int i = 0; i < key_10_conditions_array.Length; i++)
                        {
                            if (key_10_conditions_array[i] == "7")
                            {
                                key_10_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (target_shield_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_10_conditions_array.Length; i++)
                    {
                        if (key_10_conditions_array[i] == "0" && !key_10_conditions_array.Contains("8"))
                        {
                            key_10_conditions_array[i] = "8";
                        }
                    }
                }
                else
                {
                    if (key_10_conditions_array.Contains("8"))
                    {
                        for (int i = 0; i < key_10_conditions_array.Length; i++)
                        {
                            if (key_10_conditions_array[i] == "8")
                            {
                                key_10_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_tbx.Text != "0")
                {
                    for (int i = 0; i < key_10_conditions_array.Length; i++)
                    {
                        if (key_10_conditions_array[i] == "0" && !key_10_conditions_array.Contains("9"))
                        {
                            key_10_conditions_array[i] = "9";
                        }
                    }
                }
                else
                {
                    if (key_10_conditions_array.Contains("9"))
                    {
                        for (int i = 0; i < key_10_conditions_array.Length; i++)
                        {
                            if (key_10_conditions_array[i] == "9")
                            {
                                key_10_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                if (special_power_under_tbx.Text != "0")
                {
                    for (int i = 0; i < key_10_conditions_array.Length; i++)
                    {
                        if (key_10_conditions_array[i] == "0" && !key_10_conditions_array.Contains("100"))
                        {
                            key_10_conditions_array[i] = "100";
                        }
                    }
                }
                else
                {
                    if (key_10_conditions_array.Contains("100"))
                    {
                        for (int i = 0; i < key_10_conditions_array.Length; i++)
                        {
                            if (key_10_conditions_array[i] == "100")
                            {
                                key_10_conditions_array[i] = "0";
                            }
                        }
                    }
                }

                Properties.Settings.Default.key_using_10_power = special_power_tbx.Text;
                Properties.Settings.Default.key_using_10_power_under = special_power_under_tbx.Text;

                Properties.Settings.Default.key_using_10_conditions_array = ConvertStringArrayToStringJoin(key_10_conditions_array);
            }

            // Set Window showed false
            showed = false;
            change_skill_settings = false;
        }
    }
}
