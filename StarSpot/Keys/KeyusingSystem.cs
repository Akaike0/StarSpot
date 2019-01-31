using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace StarSpot
{
    class KeyusingSystem
    {
        // Stats class
        Stats stats = new Stats();

        // Skills timer
        DispatcherTimer skills_1_timer = new DispatcherTimer();
        DispatcherTimer skills_2_timer = new DispatcherTimer();
        DispatcherTimer skills_3_timer = new DispatcherTimer();
        DispatcherTimer skills_4_timer = new DispatcherTimer();
        DispatcherTimer skills_5_timer = new DispatcherTimer();
        DispatcherTimer skills_6_timer = new DispatcherTimer();
        DispatcherTimer skills_7_timer = new DispatcherTimer();
        DispatcherTimer skills_8_timer = new DispatcherTimer();
        DispatcherTimer skills_9_timer = new DispatcherTimer();
        DispatcherTimer skills_10_timer = new DispatcherTimer();

        // Skills booleans
        public static bool skill_1_enabled = true;
        public static bool skill_2_enabled = true;
        private bool skill_3_enabled = true;
        private bool skill_4_enabled = true;
        private bool skill_5_enabled = true;
        private bool skill_6_enabled = true;
        private bool skill_7_enabled = true;
        private bool skill_8_enabled = true;
        private bool skill_9_enabled = true;
        private bool skill_10_enabled = true;

        // Skill 1 array
        public string[] skill_1_array;
        public string[] skill_2_array;
        public string[] skill_3_array;
        public string[] skill_4_array;
        public string[] skill_5_array;
        public string[] skill_6_array;
        public string[] skill_7_array;
        public string[] skill_8_array;
        public string[] skill_9_array;
        public string[] skill_10_array;

        // Skills Counter
        public static int skills_counter = 1;

        public KeyusingSystem()
        {
            // Set skill timer
            skills_1_timer.Tick += new EventHandler(skills_1_timer_Tick);
            skills_2_timer.Tick += new EventHandler(skills_2_timer_Tick);
            skills_3_timer.Tick += new EventHandler(skills_3_timer_Tick);
            skills_4_timer.Tick += new EventHandler(skills_4_timer_Tick);
            skills_5_timer.Tick += new EventHandler(skills_5_timer_Tick);
            skills_6_timer.Tick += new EventHandler(skills_6_timer_Tick);
            skills_7_timer.Tick += new EventHandler(skills_7_timer_Tick);
            skills_8_timer.Tick += new EventHandler(skills_8_timer_Tick);
            skills_9_timer.Tick += new EventHandler(skills_9_timer_Tick);
            skills_10_timer.Tick += new EventHandler(skills_10_timer_Tick);
        }

        // Skills timer
        private void skills_set_timer()
        {
            if (skills_1_timer.Interval.TotalSeconds != Properties.Settings.Default.key_using_1_cooldown)
            {
                skills_1_timer.Interval = new TimeSpan(0, 0, Properties.Settings.Default.key_using_1_cooldown);
            }
            else if (Properties.Settings.Default.key_using_1_cooldown == 0)
            {
                Properties.Settings.Default.key_using_1_cooldown = 1;
            }

            if (skill_1_enabled == false && Properties.Settings.Default.key_using_1_key != "")
            {
                skills_1_timer.Start(); // Start the timer
            }
            else if (skill_1_enabled )
            {
                skills_1_timer.Stop(); // Stop the timer
            }

            if (skills_2_timer.Interval.TotalSeconds != Properties.Settings.Default.key_using_2_cooldown && Properties.Settings.Default.key_using_2_cooldown != 0)
            {
                skills_2_timer.Interval = new TimeSpan(0, 0, Properties.Settings.Default.key_using_2_cooldown);
            }
            else if (Properties.Settings.Default.key_using_2_cooldown == 0)
            {
                Properties.Settings.Default.key_using_2_cooldown = 1;
            }

            if (skill_2_enabled == false && Properties.Settings.Default.key_using_2_key != "")
            {
                skills_2_timer.Start(); // Start the timer
            }
            else if (skill_2_enabled )
            {
                skills_2_timer.Stop(); // Stop the timer
            }

            if (skills_3_timer.Interval.TotalSeconds != Properties.Settings.Default.key_using_3_cooldown)
            {
                skills_3_timer.Interval = new TimeSpan(0, 0, Properties.Settings.Default.key_using_3_cooldown);
            }
            else if (Properties.Settings.Default.key_using_3_cooldown == 0)
            {
                Properties.Settings.Default.key_using_3_cooldown = 1;
            }

            if (skill_3_enabled == false && Properties.Settings.Default.key_using_3_key != "")
            {
                skills_3_timer.Start(); // Start the timer
            }
            else if (skill_3_enabled )
            {
                skills_3_timer.Stop(); // Stop the timer
            }

            if (skills_4_timer.Interval.TotalSeconds != Properties.Settings.Default.key_using_4_cooldown)
            {
                skills_4_timer.Interval = new TimeSpan(0, 0, Properties.Settings.Default.key_using_4_cooldown);
            }
            else if (Properties.Settings.Default.key_using_4_cooldown == 0)
            {
                Properties.Settings.Default.key_using_4_cooldown = 1;
            }

            if (skill_4_enabled == false && Properties.Settings.Default.key_using_4_key != "")
            {
                skills_4_timer.Start(); // Start the timer
            }
            else if (skill_4_enabled )
            {
                skills_4_timer.Stop(); // Stop the timer
            }

            if (skills_5_timer.Interval.TotalSeconds != Properties.Settings.Default.key_using_5_cooldown)
            {
                skills_5_timer.Interval = new TimeSpan(0, 0, Properties.Settings.Default.key_using_5_cooldown);
            }
            else if (Properties.Settings.Default.key_using_5_cooldown == 0)
            {
                Properties.Settings.Default.key_using_5_cooldown = 1;
            }

            if (skill_5_enabled == false && Properties.Settings.Default.key_using_5_key != "")
            {
                skills_5_timer.Start(); // Start the timer
            }
            else if (skill_5_enabled )
            {
                skills_5_timer.Stop(); // Stop the timer
            }

            if (skills_6_timer.Interval.TotalSeconds != Properties.Settings.Default.key_using_6_cooldown)
            {
                skills_6_timer.Interval = new TimeSpan(0, 0, Properties.Settings.Default.key_using_6_cooldown);
            }
            else if (Properties.Settings.Default.key_using_6_cooldown == 0)
            {
                Properties.Settings.Default.key_using_6_cooldown = 1;
            }

            if (skill_6_enabled == false && Properties.Settings.Default.key_using_6_key != "")
            {
                skills_6_timer.Start(); // Start the timer
            }
            else if (skill_6_enabled )
            {
                skills_6_timer.Stop(); // Stop the timer
            }

            if (skills_7_timer.Interval.TotalSeconds != Properties.Settings.Default.key_using_7_cooldown)
            {
                skills_7_timer.Interval = new TimeSpan(0, 0, Properties.Settings.Default.key_using_7_cooldown);
            }
            else if (Properties.Settings.Default.key_using_7_cooldown == 0)
            {
                Properties.Settings.Default.key_using_7_cooldown = 1;
            }

            if (skill_7_enabled == false && Properties.Settings.Default.key_using_7_key != "")
            {
                skills_7_timer.Start(); // Start the timer
            }
            else if (skill_7_enabled )
            {
                skills_7_timer.Stop(); // Stop the timer
            }

            if (skills_8_timer.Interval.TotalSeconds != Properties.Settings.Default.key_using_8_cooldown)
            {
                skills_8_timer.Interval = new TimeSpan(0, 0, Properties.Settings.Default.key_using_8_cooldown);
            }
            else if (Properties.Settings.Default.key_using_8_cooldown == 0)
            {
                Properties.Settings.Default.key_using_8_cooldown = 1;
            }

            if (skill_8_enabled == false && Properties.Settings.Default.key_using_8_key != "")
            {
                skills_8_timer.Start(); // Start the timer
            }
            else if (skill_8_enabled )
            {
                skills_8_timer.Stop(); // Stop the timer
            }

            if (skills_9_timer.Interval.TotalSeconds != Properties.Settings.Default.key_using_9_cooldown)
            {
                skills_9_timer.Interval = new TimeSpan(0, 0, Properties.Settings.Default.key_using_9_cooldown);
            }
            else if (Properties.Settings.Default.key_using_9_cooldown == 0)
            {
                Properties.Settings.Default.key_using_9_cooldown = 1;
            }

            if (skill_9_enabled == false && Properties.Settings.Default.key_using_9_key != "")
            {
                skills_9_timer.Start(); // Start the timer
            }
            else if (skill_9_enabled )
            {
                skills_9_timer.Stop(); // Stop the timer
            }

            if (skills_10_timer.Interval.TotalSeconds != Properties.Settings.Default.key_using_10_cooldown)
            {
                skills_10_timer.Interval = new TimeSpan(0, 0, Properties.Settings.Default.key_using_10_cooldown);
            }
            else if (Properties.Settings.Default.key_using_10_cooldown == 0)
            {
                Properties.Settings.Default.key_using_10_cooldown = 1;
            }

            if (skill_10_enabled == false && Properties.Settings.Default.key_using_10_key != "")
            {
                skills_10_timer.Start(); // Start the timer
            }
            else if (skill_10_enabled )
            {
                skills_10_timer.Stop(); // Stop the timer
            }
        }

        private void skills_1_timer_Tick(object sender, EventArgs e)
        {
            skill_1_enabled = true;
        }
        private void skills_2_timer_Tick(object sender, EventArgs e)
        {
            skill_2_enabled = true;
        }
        private void skills_3_timer_Tick(object sender, EventArgs e)
        {
            skill_3_enabled = true;
        }
        private void skills_4_timer_Tick(object sender, EventArgs e)
        {
            skill_4_enabled = true;
        }
        private void skills_5_timer_Tick(object sender, EventArgs e)
        {
            skill_5_enabled = true;
        }
        private void skills_6_timer_Tick(object sender, EventArgs e)
        {
            skill_6_enabled = true;
        }
        private void skills_7_timer_Tick(object sender, EventArgs e)
        {
            skill_7_enabled = true;
        }
        private void skills_8_timer_Tick(object sender, EventArgs e)
        {
            skill_8_enabled = true;
        }
        private void skills_9_timer_Tick(object sender, EventArgs e)
        {
            skill_9_enabled = true;
        }
        private void skills_10_timer_Tick(object sender, EventArgs e)
        {
            skill_10_enabled = true;
        }

        // Key using method
        public void key_using_infight()
        {
            // Skills
            if (Properties.Settings.Default.key_using_1_key != "" && skills_counter == 1)
            {
                // Reset the conditions bool
                stats_bool = true;

                if (
                        skill_1_enabled
                    )
                {
                    conditions("1");

                    if (stats_bool && Properties.Settings.Default.key_using_1_infight)
                    {

                        Keysimulation.SimulateKeys.KeyUsing(Properties.Settings.Default.key_using_1_key);

                        System.Threading.Thread.Sleep(Properties.Settings.Default.key_using_1_ct + 400);

                        skill_1_enabled = false;
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
            else if (Properties.Settings.Default.key_using_1_key == "" && skills_counter == 1)
            {
                skills_counter = 2;
            }

            if (Properties.Settings.Default.key_using_2_key != "" && skills_counter == 2)
            {
                // Reset the conditions bool
                stats_bool = true;

                if (
                        skill_2_enabled
                    )
                {
                    conditions("2");

                    if (stats_bool && Properties.Settings.Default.key_using_2_infight)
                    {

                        Keysimulation.SimulateKeys.KeyUsing(Properties.Settings.Default.key_using_2_key);

                        System.Threading.Thread.Sleep(Properties.Settings.Default.key_using_2_ct + 400);

                        skill_2_enabled = false;
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
            else if (Properties.Settings.Default.key_using_2_key == "" && skills_counter == 2)
            {
                skills_counter = 3;
            }

            if (Properties.Settings.Default.key_using_3_key != "" && skills_counter == 3)
            {
                // Reset the conditions bool
                stats_bool = true;

                if (
                        skill_3_enabled
                    )
                {
                    conditions("3");

                    if (stats_bool && Properties.Settings.Default.key_using_3_infight)
                    {

                        Keysimulation.SimulateKeys.KeyUsing(Properties.Settings.Default.key_using_3_key);

                        System.Threading.Thread.Sleep(Properties.Settings.Default.key_using_3_ct + 400);

                        skill_3_enabled = false;
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
            else if (Properties.Settings.Default.key_using_3_key == "" && skills_counter == 3)
            {
                skills_counter = 4;
            }

            if (Properties.Settings.Default.key_using_4_key != "" && skills_counter == 4)
            {
                // Reset the conditions bool
                stats_bool = true;

                if (
                        skill_4_enabled
                    )
                {
                    conditions("4");

                    if (stats_bool && Properties.Settings.Default.key_using_4_infight)
                    {

                        Keysimulation.SimulateKeys.KeyUsing(Properties.Settings.Default.key_using_4_key);

                        System.Threading.Thread.Sleep(Properties.Settings.Default.key_using_4_ct + 400);

                        skill_4_enabled = false;
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
            else if (Properties.Settings.Default.key_using_4_key == "" && skills_counter == 4)
            {
                skills_counter = 5;
            }

            if (Properties.Settings.Default.key_using_5_key != "" && skills_counter == 5)
            {
                // Reset the conditions bool
                stats_bool = true;

                if (
                        skill_5_enabled
                    )
                {
                    conditions("5");

                    if (stats_bool && Properties.Settings.Default.key_using_5_infight)
                    {

                        Keysimulation.SimulateKeys.KeyUsing(Properties.Settings.Default.key_using_5_key);

                        System.Threading.Thread.Sleep(Properties.Settings.Default.key_using_5_ct + 400);

                        skill_5_enabled = false;
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
            else if (Properties.Settings.Default.key_using_5_key == "" && skills_counter == 5)
            {
                skills_counter = 6;
            }

            if (Properties.Settings.Default.key_using_6_key != "" && skills_counter == 6)
            {
                // Reset the conditions bool
                stats_bool = true;

                if (
                        skill_6_enabled
                    )
                {
                    conditions("6");

                    if (stats_bool && Properties.Settings.Default.key_using_6_infight)
                    {

                        Keysimulation.SimulateKeys.KeyUsing(Properties.Settings.Default.key_using_6_key);

                        System.Threading.Thread.Sleep(Properties.Settings.Default.key_using_6_ct + 400);

                        skill_6_enabled = false;
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
            else if (Properties.Settings.Default.key_using_6_key == "" && skills_counter == 6)
            {
                skills_counter = 7;
            }

            if (Properties.Settings.Default.key_using_7_key != "" && skills_counter == 7)
            {
                // Reset the conditions bool
                stats_bool = true;

                if (
                        skill_7_enabled
                    )
                {
                    conditions("7");

                    if (stats_bool && Properties.Settings.Default.key_using_7_infight)
                    {

                        Keysimulation.SimulateKeys.KeyUsing(Properties.Settings.Default.key_using_7_key);

                        System.Threading.Thread.Sleep(Properties.Settings.Default.key_using_7_ct + 400);

                        skill_7_enabled = false;
                    }
                    else
                    {
                        skills_counter = 8;
                    }
                }
                else
                {
                    skills_counter = 8;
                }
            }
            else if (Properties.Settings.Default.key_using_7_key == "" && skills_counter == 7)
            {
                skills_counter = 8;
            }

            if (Properties.Settings.Default.key_using_8_key != "" && skills_counter == 8)
            {
                // Reset the conditions bool
                stats_bool = true;

                if (
                        skill_8_enabled
                    )
                {
                    conditions("8");

                    if (stats_bool && Properties.Settings.Default.key_using_8_infight)
                    {

                        Keysimulation.SimulateKeys.KeyUsing(Properties.Settings.Default.key_using_8_key);

                        System.Threading.Thread.Sleep(Properties.Settings.Default.key_using_8_ct + 400);

                        skill_8_enabled = false;
                    }
                    else
                    {
                        skills_counter = 9;
                    }
                }
                else
                {
                    skills_counter = 9;
                }
            }
            else if (Properties.Settings.Default.key_using_8_key == "" && skills_counter == 8)
            {
                skills_counter = 9;
            }

            if (Properties.Settings.Default.key_using_9_key != "" && skills_counter == 9)
            {
                // Reset the conditions bool
                stats_bool = true;

                if (
                        skill_9_enabled
                    )
                {
                    conditions("9");

                    if (stats_bool && Properties.Settings.Default.key_using_9_infight)
                    {

                        Keysimulation.SimulateKeys.KeyUsing(Properties.Settings.Default.key_using_9_key);

                        System.Threading.Thread.Sleep(Properties.Settings.Default.key_using_9_ct + 400);

                        skill_9_enabled = false;
                    }
                    else
                    {
                        skills_counter = 10;
                    }
                }
                else
                {
                    skills_counter = 10;
                }
            }
            else if (Properties.Settings.Default.key_using_9_key == "" && skills_counter == 9)
            {
                skills_counter = 10;
            }


            if (Properties.Settings.Default.key_using_10_key != "" && skills_counter == 10)
            {
                // Reset the conditions bool
                stats_bool = true;

                if (
                        skill_10_enabled
                    )
                {
                    conditions("10");

                    if (stats_bool && Properties.Settings.Default.key_using_10_infight)
                    {

                        Keysimulation.SimulateKeys.KeyUsing(Properties.Settings.Default.key_using_10_key);

                        System.Threading.Thread.Sleep(Properties.Settings.Default.key_using_10_ct + 400);

                        skill_10_enabled = false;
                    }
                    else
                    {
                        skills_counter = 11;
                    }
                }
                else
                {
                    skills_counter = 11;
                }
            }
            else if (Properties.Settings.Default.key_using_10_key == "" && skills_counter == 10)
            {
                skills_counter = 11;
            }

            if (skills_counter > 10)
            {
                skills_counter = 1; // Reset the skill switch counter
            }
        }
        public void key_using()
        {
            // Skills
            if (Properties.Settings.Default.key_using_1_key != "" && skills_counter == 1)
            {
                // Reset the conditions bool
                stats_bool = true;

                if (
                        skill_1_enabled
                    )
                {
                    conditions("1");

                    if (stats_bool && !Properties.Settings.Default.key_using_1_infight)
                    {

                        Keysimulation.SimulateKeys.KeyUsing(Properties.Settings.Default.key_using_1_key);

                        System.Threading.Thread.Sleep(Properties.Settings.Default.key_using_1_ct + 400);

                        skill_1_enabled = false;
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
            else if (Properties.Settings.Default.key_using_1_key == "" && skills_counter == 1)
            {
                skills_counter = 2;
            }

            if (Properties.Settings.Default.key_using_2_key != "" && skills_counter == 2)
            {
                // Reset the conditions bool
                stats_bool = true;

                if (
                        skill_2_enabled
                    )
                {
                    conditions("2");

                    if (stats_bool && !Properties.Settings.Default.key_using_2_infight)
                    {

                        Keysimulation.SimulateKeys.KeyUsing(Properties.Settings.Default.key_using_2_key);

                        System.Threading.Thread.Sleep(Properties.Settings.Default.key_using_2_ct + 400);

                        skill_2_enabled = false;
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
            else if (Properties.Settings.Default.key_using_2_key == "" && skills_counter == 2)
            {
                skills_counter = 3;
            }

            if (Properties.Settings.Default.key_using_3_key != "" && skills_counter == 3)
            {
                // Reset the conditions bool
                stats_bool = true;

                if (
                        skill_3_enabled
                    )
                {
                    conditions("3");

                    if (stats_bool && !Properties.Settings.Default.key_using_3_infight)
                    {

                        Keysimulation.SimulateKeys.KeyUsing(Properties.Settings.Default.key_using_3_key);

                        System.Threading.Thread.Sleep(Properties.Settings.Default.key_using_3_ct + 400);

                        skill_3_enabled = false;
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
            else if (Properties.Settings.Default.key_using_3_key == "" && skills_counter == 3)
            {
                skills_counter = 4;
            }

            if (Properties.Settings.Default.key_using_4_key != "" && skills_counter == 4)
            {
                // Reset the conditions bool
                stats_bool = true;

                if (
                        skill_4_enabled
                    )
                {
                    conditions("4");

                    if (stats_bool && !Properties.Settings.Default.key_using_4_infight)
                    {

                        Keysimulation.SimulateKeys.KeyUsing(Properties.Settings.Default.key_using_4_key);

                        System.Threading.Thread.Sleep(Properties.Settings.Default.key_using_4_ct + 400);

                        skill_4_enabled = false;
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
            else if (Properties.Settings.Default.key_using_4_key == "" && skills_counter == 4)
            {
                skills_counter = 5;
            }

            if (Properties.Settings.Default.key_using_5_key != "" && skills_counter == 5)
            {
                // Reset the conditions bool
                stats_bool = true;

                if (
                        skill_5_enabled
                    )
                {
                    conditions("5");

                    if (stats_bool && !Properties.Settings.Default.key_using_5_infight)
                    {

                        Keysimulation.SimulateKeys.KeyUsing(Properties.Settings.Default.key_using_5_key);

                        System.Threading.Thread.Sleep(Properties.Settings.Default.key_using_5_ct + 400);

                        skill_5_enabled = false;
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
            else if (Properties.Settings.Default.key_using_5_key == "" && skills_counter == 5)
            {
                skills_counter = 6;
            }

            if (Properties.Settings.Default.key_using_6_key != "" && skills_counter == 6)
            {
                // Reset the conditions bool
                stats_bool = true;

                if (
                        skill_6_enabled
                    )
                {
                    conditions("6");

                    if (stats_bool && !Properties.Settings.Default.key_using_6_infight)
                    {

                        Keysimulation.SimulateKeys.KeyUsing(Properties.Settings.Default.key_using_6_key);

                        System.Threading.Thread.Sleep(Properties.Settings.Default.key_using_6_ct + 400);

                        skill_6_enabled = false;
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
            else if (Properties.Settings.Default.key_using_6_key == "" && skills_counter == 6)
            {
                skills_counter = 7;
            }

            if (Properties.Settings.Default.key_using_7_key != "" && skills_counter == 7)
            {
                // Reset the conditions bool
                stats_bool = true;

                if (
                        skill_7_enabled
                    )
                {
                    conditions("7");

                    if (stats_bool && !Properties.Settings.Default.key_using_7_infight)
                    {

                        Keysimulation.SimulateKeys.KeyUsing(Properties.Settings.Default.key_using_7_key);

                        System.Threading.Thread.Sleep(Properties.Settings.Default.key_using_7_ct + 400);

                        skill_7_enabled = false;
                    }
                    else
                    {
                        skills_counter = 8;
                    }
                }
                else
                {
                    skills_counter = 8;
                }
            }
            else if (Properties.Settings.Default.key_using_7_key == "" && skills_counter == 7)
            {
                skills_counter = 8;
            }

            if (Properties.Settings.Default.key_using_8_key != "" && skills_counter == 8)
            {
                // Reset the conditions bool
                stats_bool = true;

                if (
                        skill_8_enabled
                    )
                {
                    conditions("8");

                    if (stats_bool && !Properties.Settings.Default.key_using_8_infight)
                    {

                        Keysimulation.SimulateKeys.KeyUsing(Properties.Settings.Default.key_using_8_key);

                        System.Threading.Thread.Sleep(Properties.Settings.Default.key_using_8_ct + 400);

                        skill_8_enabled = false;
                    }
                    else
                    {
                        skills_counter = 9;
                    }
                }
                else
                {
                    skills_counter = 9;
                }
            }
            else if (Properties.Settings.Default.key_using_8_key == "" && skills_counter == 8)
            {
                skills_counter = 9;
            }

            if (Properties.Settings.Default.key_using_9_key != "" && skills_counter == 9)
            {
                // Reset the conditions bool
                stats_bool = true;

                if (
                        skill_9_enabled
                    )
                {
                    conditions("9");

                    if (stats_bool && !Properties.Settings.Default.key_using_9_infight)
                    {

                        Keysimulation.SimulateKeys.KeyUsing(Properties.Settings.Default.key_using_9_key);

                        System.Threading.Thread.Sleep(Properties.Settings.Default.key_using_9_ct + 400);

                        skill_9_enabled = false;
                    }
                    else
                    {
                        skills_counter = 10;
                    }
                }
                else
                {
                    skills_counter = 10;
                }
            }
            else if (Properties.Settings.Default.key_using_9_key == "" && skills_counter == 9)
            {
                skills_counter = 10;
            }


            if (Properties.Settings.Default.key_using_10_key != "" && skills_counter == 10)
            {
                // Reset the conditions bool
                stats_bool = true;

                if (
                        skill_10_enabled
                    )
                {
                    conditions("10");

                    if (stats_bool && !Properties.Settings.Default.key_using_10_infight)
                    {

                        Keysimulation.SimulateKeys.KeyUsing(Properties.Settings.Default.key_using_10_key);

                        System.Threading.Thread.Sleep(Properties.Settings.Default.key_using_10_ct + 400);

                        skill_10_enabled = false;
                    }
                    else
                    {
                        skills_counter = 11;
                    }
                }
                else
                {
                    skills_counter = 11;
                }
            }
            else if (Properties.Settings.Default.key_using_10_key == "" && skills_counter == 10)
            {
                skills_counter = 11;
            }

            if (skills_counter > 10)
            {
                skills_counter = 1; // Reset the skill switch counter
            }
        }

        // Skill aktivation
        public void skill_activation()
        {
            // Set key settings
            skills_set_timer();

            // Load settings
            load_conditions();

            // Execute
            if ((Grinding.player_attacking | PVP.player_attacking | (Properties.Settings.Default.mods == "PVP" && stats.player_aggro() == 1) | (Properties.Settings.Default.mods == "Combat" && stats.player_aggro() == 1)) && Properties.Settings.Default.key_using_1_infight | Properties.Settings.Default.key_using_2_infight | Properties.Settings.Default.key_using_3_infight 
                | Properties.Settings.Default.key_using_4_infight | Properties.Settings.Default.key_using_5_infight | Properties.Settings.Default.key_using_6_infight 
                | Properties.Settings.Default.key_using_7_infight | Properties.Settings.Default.key_using_8_infight | Properties.Settings.Default.key_using_9_infight 
                | Properties.Settings.Default.key_using_10_infight )
            {
                if (stats.player_targetid() != 0 && stats.player_aggro() == 1) // Useeee a key >_>
                {
                    key_using_infight();
                }
            }
            else if (((Grinding.player_buffing | Grinding.player_healing) && !Grinding.player_attacking && Properties.Settings.Default.mods != "PVP") | ((Gathering.player_buffing | Gathering.player_healing) && !Gathering.player_attacking && Properties.Settings.Default.mods != "PVP") | ((PVP.player_buffing | PVP.player_healing) && !PVP.player_attacking && Properties.Settings.Default.mods == "PVP") | (Properties.Settings.Default.mods == "Combat" && stats.player_aggro() == 0))
            {
                if (stats.player_health() != 0)
                {
                    if (stats.player_aggro() == 0)
                    {
                        key_using();
                    }
                }
                else
                {
                    stats_bool = true;
                }
            } // # Player buffing
        }

        // Condition settings
        public void load_conditions()
        {
            skill_1_array = Properties.Settings.Default.key_using_1_conditions_array.Split(',');
            skill_2_array = Properties.Settings.Default.key_using_2_conditions_array.Split(',');
            skill_3_array = Properties.Settings.Default.key_using_3_conditions_array.Split(',');
            skill_4_array = Properties.Settings.Default.key_using_4_conditions_array.Split(',');
            skill_5_array = Properties.Settings.Default.key_using_5_conditions_array.Split(',');
            skill_6_array = Properties.Settings.Default.key_using_6_conditions_array.Split(',');
            skill_7_array = Properties.Settings.Default.key_using_7_conditions_array.Split(',');
            skill_8_array = Properties.Settings.Default.key_using_8_conditions_array.Split(',');
            skill_9_array = Properties.Settings.Default.key_using_9_conditions_array.Split(',');
            skill_10_array = Properties.Settings.Default.key_using_10_conditions_array.Split(',');
        }

        public void conditions(string c)
        {
            switch (c)
            {
                case "1":
                    skill_1(skill_1_array);
                    break;
                case "2":
                    skill_2(skill_2_array);
                    break;
                case "3":
                    skill_3(skill_3_array);
                    break;
                case "4":
                    skill_4(skill_4_array);
                    break;
                case "5":
                    skill_5(skill_5_array);
                    break;
                case "6":
                    skill_6(skill_6_array);
                    break;
                case "7":
                    skill_7(skill_7_array);
                    break;
                case "8":
                    skill_8(skill_8_array);
                    break;
                case "9":
                    skill_9(skill_9_array);
                    break;
                case "10":
                    skill_10(skill_10_array);
                    break;
            }
        }

        // Stats bool
        private bool stats_bool;

        public void skill_1(string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                switch (array[i])
                {
                    case "1":
                        if (stats.player_health_inp() > Convert.ToUInt64(Properties.Settings.Default.key_using_1_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "2":
                        if (stats.player_health_inp() < Convert.ToUInt64(Properties.Settings.Default.key_using_1_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "3":
                        if (stats.player_shield_inp() > Convert.ToUInt64(Properties.Settings.Default.key_using_1_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "4":
                        if (stats.player_shield_inp() < Convert.ToUInt64(Properties.Settings.Default.key_using_1_shield_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "5":
                        if (TargetInfo.target_health_inp > Convert.ToUInt64(Properties.Settings.Default.key_using_1_target_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "6":
                        if (TargetInfo.target_health_inp < Convert.ToUInt64(Properties.Settings.Default.key_using_1_target_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "7":
                        if (TargetInfo.target_shield_inp > Convert.ToUInt64(Properties.Settings.Default.key_using_1_target_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "8":
                        if (TargetInfo.target_shield_inp < Convert.ToUInt64(Properties.Settings.Default.key_using_1_target_shield_cdt))
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
                            if (stats.player_otherpowers() > Convert.ToUInt64(Properties.Settings.Default.key_using_1_power))
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
                            if (stats.player_suitpower() > Convert.ToUInt64(Properties.Settings.Default.key_using_1_power))
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
                            if (stats.player_spellpower() > Convert.ToUInt64(Properties.Settings.Default.key_using_1_power))
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
                            if (stats.player_otherpowers() < Convert.ToUInt64(Properties.Settings.Default.key_using_1_power_under))
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
                            if (stats.player_suitpower() < Convert.ToUInt64(Properties.Settings.Default.key_using_1_power_under))
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
                            if (stats.player_spellpower() < Convert.ToUInt64(Properties.Settings.Default.key_using_1_power_under))
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
                        if (stats.player_health_inp() > Convert.ToUInt64(Properties.Settings.Default.key_using_2_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "2":
                        if (stats.player_health_inp() < Convert.ToUInt64(Properties.Settings.Default.key_using_2_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "3":
                        if (stats.player_shield_inp() > Convert.ToUInt64(Properties.Settings.Default.key_using_2_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "4":
                        if (stats.player_shield_inp() < Convert.ToUInt64(Properties.Settings.Default.key_using_2_shield_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "5":
                        if (TargetInfo.target_health_inp > Convert.ToUInt64(Properties.Settings.Default.key_using_2_target_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "6":
                        if (TargetInfo.target_health_inp < Convert.ToUInt64(Properties.Settings.Default.key_using_2_target_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "7":
                        if (TargetInfo.target_shield_inp > Convert.ToUInt64(Properties.Settings.Default.key_using_2_target_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "8":
                        if (TargetInfo.target_shield_inp < Convert.ToUInt64(Properties.Settings.Default.key_using_2_target_shield_cdt))
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
                            if (stats.player_otherpowers() > Convert.ToUInt64(Properties.Settings.Default.key_using_2_power))
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
                            if (stats.player_suitpower() > Convert.ToUInt64(Properties.Settings.Default.key_using_2_power))
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
                            if (stats.player_spellpower() > Convert.ToUInt64(Properties.Settings.Default.key_using_2_power))
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
                            if (stats.player_otherpowers() < Convert.ToUInt64(Properties.Settings.Default.key_using_2_power_under))
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
                            if (stats.player_suitpower() < Convert.ToUInt64(Properties.Settings.Default.key_using_2_power_under))
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
                            if (stats.player_spellpower() < Convert.ToUInt64(Properties.Settings.Default.key_using_2_power_under))
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
                        if (stats.player_health_inp() > Convert.ToUInt64(Properties.Settings.Default.key_using_3_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "2":
                        if (stats.player_health_inp() < Convert.ToUInt64(Properties.Settings.Default.key_using_3_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "3":
                        if (stats.player_shield_inp() > Convert.ToUInt64(Properties.Settings.Default.key_using_3_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "4":
                        if (stats.player_shield_inp() < Convert.ToUInt64(Properties.Settings.Default.key_using_3_shield_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "5":
                        if (TargetInfo.target_health_inp > Convert.ToUInt64(Properties.Settings.Default.key_using_3_target_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "6":
                        if (TargetInfo.target_health_inp < Convert.ToUInt64(Properties.Settings.Default.key_using_3_target_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "7":
                        if (TargetInfo.target_shield_inp > Convert.ToUInt64(Properties.Settings.Default.key_using_3_target_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "8":
                        if (TargetInfo.target_shield_inp < Convert.ToUInt64(Properties.Settings.Default.key_using_3_target_shield_cdt))
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
                            if (stats.player_otherpowers() > Convert.ToUInt64(Properties.Settings.Default.key_using_3_power))
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
                            if (stats.player_suitpower() > Convert.ToUInt64(Properties.Settings.Default.key_using_3_power))
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
                            if (stats.player_spellpower() > Convert.ToUInt64(Properties.Settings.Default.key_using_3_power))
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
                            if (stats.player_otherpowers() < Convert.ToUInt64(Properties.Settings.Default.key_using_3_power_under))
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
                            if (stats.player_suitpower() < Convert.ToUInt64(Properties.Settings.Default.key_using_3_power_under))
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
                            if (stats.player_spellpower() < Convert.ToUInt64(Properties.Settings.Default.key_using_3_power_under))
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
                        if (stats.player_health_inp() > Convert.ToUInt64(Properties.Settings.Default.key_using_4_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "2":
                        if (stats.player_health_inp() < Convert.ToUInt64(Properties.Settings.Default.key_using_4_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "3":
                        if (stats.player_shield_inp() > Convert.ToUInt64(Properties.Settings.Default.key_using_4_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "4":
                        if (stats.player_shield_inp() < Convert.ToUInt64(Properties.Settings.Default.key_using_4_shield_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "5":
                        if (TargetInfo.target_health_inp > Convert.ToUInt64(Properties.Settings.Default.key_using_4_target_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "6":
                        if (TargetInfo.target_health_inp < Convert.ToUInt64(Properties.Settings.Default.key_using_4_target_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "7":
                        if (TargetInfo.target_shield_inp > Convert.ToUInt64(Properties.Settings.Default.key_using_4_target_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "8":
                        if (TargetInfo.target_shield_inp < Convert.ToUInt64(Properties.Settings.Default.key_using_4_target_shield_cdt))
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
                            if (stats.player_otherpowers() > Convert.ToUInt64(Properties.Settings.Default.key_using_4_power))
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
                            if (stats.player_suitpower() > Convert.ToUInt64(Properties.Settings.Default.key_using_4_power))
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
                            if (stats.player_spellpower() > Convert.ToUInt64(Properties.Settings.Default.key_using_4_power))
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
                            if (stats.player_otherpowers() < Convert.ToUInt64(Properties.Settings.Default.key_using_4_power_under))
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
                            if (stats.player_suitpower() < Convert.ToUInt64(Properties.Settings.Default.key_using_4_power_under))
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
                            if (stats.player_spellpower() < Convert.ToUInt64(Properties.Settings.Default.key_using_4_power_under))
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
                        if (stats.player_health_inp() > Convert.ToUInt64(Properties.Settings.Default.key_using_5_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "2":
                        if (stats.player_health_inp() < Convert.ToUInt64(Properties.Settings.Default.key_using_5_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "3":
                        if (stats.player_shield_inp() > Convert.ToUInt64(Properties.Settings.Default.key_using_5_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "4":
                        if (stats.player_shield_inp() < Convert.ToUInt64(Properties.Settings.Default.key_using_5_shield_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "5":
                        if (TargetInfo.target_health_inp > Convert.ToUInt64(Properties.Settings.Default.key_using_5_target_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "6":
                        if (TargetInfo.target_health_inp < Convert.ToUInt64(Properties.Settings.Default.key_using_5_target_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "7":
                        if (TargetInfo.target_shield_inp > Convert.ToUInt64(Properties.Settings.Default.key_using_5_target_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "8":
                        if (TargetInfo.target_shield_inp < Convert.ToUInt64(Properties.Settings.Default.key_using_5_target_shield_cdt))
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
                            if (stats.player_otherpowers() > Convert.ToUInt64(Properties.Settings.Default.key_using_5_power))
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
                            if (stats.player_suitpower() > Convert.ToUInt64(Properties.Settings.Default.key_using_5_power))
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
                            if (stats.player_spellpower() > Convert.ToUInt64(Properties.Settings.Default.key_using_5_power))
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
                            if (stats.player_otherpowers() < Convert.ToUInt64(Properties.Settings.Default.key_using_5_power_under))
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
                            if (stats.player_suitpower() < Convert.ToUInt64(Properties.Settings.Default.key_using_5_power_under))
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
                            if (stats.player_spellpower() < Convert.ToUInt64(Properties.Settings.Default.key_using_5_power_under))
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
                        if (stats.player_health_inp() > Convert.ToUInt64(Properties.Settings.Default.key_using_6_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "2":
                        if (stats.player_health_inp() < Convert.ToUInt64(Properties.Settings.Default.key_using_6_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "3":
                        if (stats.player_shield_inp() > Convert.ToUInt64(Properties.Settings.Default.key_using_6_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "4":
                        if (stats.player_shield_inp() < Convert.ToUInt64(Properties.Settings.Default.key_using_6_shield_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "5":
                        if (TargetInfo.target_health_inp > Convert.ToUInt64(Properties.Settings.Default.key_using_6_target_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "6":
                        if (TargetInfo.target_health_inp < Convert.ToUInt64(Properties.Settings.Default.key_using_6_target_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "7":
                        if (TargetInfo.target_shield_inp > Convert.ToUInt64(Properties.Settings.Default.key_using_6_target_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "8":
                        if (TargetInfo.target_shield_inp < Convert.ToUInt64(Properties.Settings.Default.key_using_6_target_shield_cdt))
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
                            if (stats.player_otherpowers() > Convert.ToUInt64(Properties.Settings.Default.key_using_6_power))
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
                            if (stats.player_suitpower() > Convert.ToUInt64(Properties.Settings.Default.key_using_6_power))
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
                            if (stats.player_spellpower() > Convert.ToUInt64(Properties.Settings.Default.key_using_6_power))
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
                            if (stats.player_otherpowers() < Convert.ToUInt64(Properties.Settings.Default.key_using_6_power_under))
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
                            if (stats.player_suitpower() < Convert.ToUInt64(Properties.Settings.Default.key_using_6_power_under))
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
                            if (stats.player_spellpower() < Convert.ToUInt64(Properties.Settings.Default.key_using_6_power_under))
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
                        if (stats.player_health_inp() > Convert.ToUInt64(Properties.Settings.Default.key_using_7_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "2":
                        if (stats.player_health_inp() < Convert.ToUInt64(Properties.Settings.Default.key_using_7_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "3":
                        if (stats.player_shield_inp() > Convert.ToUInt64(Properties.Settings.Default.key_using_7_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "4":
                        if (stats.player_shield_inp() < Convert.ToUInt64(Properties.Settings.Default.key_using_7_shield_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "5":
                        if (TargetInfo.target_health_inp > Convert.ToUInt64(Properties.Settings.Default.key_using_7_target_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "6":
                        if (TargetInfo.target_health_inp < Convert.ToUInt64(Properties.Settings.Default.key_using_7_target_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "7":
                        if (TargetInfo.target_shield_inp > Convert.ToUInt64(Properties.Settings.Default.key_using_7_target_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "8":
                        if (TargetInfo.target_shield_inp < Convert.ToUInt64(Properties.Settings.Default.key_using_7_target_shield_cdt))
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
                            if (stats.player_otherpowers() > Convert.ToUInt64(Properties.Settings.Default.key_using_7_power))
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
                            if (stats.player_suitpower() > Convert.ToUInt64(Properties.Settings.Default.key_using_7_power))
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
                            if (stats.player_spellpower() > Convert.ToUInt64(Properties.Settings.Default.key_using_7_power))
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
                            if (stats.player_otherpowers() < Convert.ToUInt64(Properties.Settings.Default.key_using_7_power_under))
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
                            if (stats.player_suitpower() < Convert.ToUInt64(Properties.Settings.Default.key_using_7_power_under))
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
                            if (stats.player_spellpower() < Convert.ToUInt64(Properties.Settings.Default.key_using_7_power_under))
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
                        if (stats.player_health_inp() > Convert.ToUInt64(Properties.Settings.Default.key_using_8_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "2":
                        if (stats.player_health_inp() < Convert.ToUInt64(Properties.Settings.Default.key_using_8_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "3":
                        if (stats.player_shield_inp() > Convert.ToUInt64(Properties.Settings.Default.key_using_8_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "4":
                        if (stats.player_shield_inp() < Convert.ToUInt64(Properties.Settings.Default.key_using_8_shield_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "5":
                        if (TargetInfo.target_health_inp > Convert.ToUInt64(Properties.Settings.Default.key_using_8_target_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "6":
                        if (TargetInfo.target_health_inp < Convert.ToUInt64(Properties.Settings.Default.key_using_8_target_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "7":
                        if (TargetInfo.target_shield_inp > Convert.ToUInt64(Properties.Settings.Default.key_using_8_target_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "8":
                        if (TargetInfo.target_shield_inp < Convert.ToUInt64(Properties.Settings.Default.key_using_8_target_shield_cdt))
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
                            if (stats.player_otherpowers() > Convert.ToUInt64(Properties.Settings.Default.key_using_8_power))
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
                            if (stats.player_suitpower() > Convert.ToUInt64(Properties.Settings.Default.key_using_8_power))
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
                            if (stats.player_spellpower() > Convert.ToUInt64(Properties.Settings.Default.key_using_8_power))
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
                            if (stats.player_otherpowers() < Convert.ToUInt64(Properties.Settings.Default.key_using_8_power_under))
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
                            if (stats.player_suitpower() < Convert.ToUInt64(Properties.Settings.Default.key_using_8_power_under))
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
                            if (stats.player_spellpower() < Convert.ToUInt64(Properties.Settings.Default.key_using_8_power_under))
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
        public void skill_9(string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                switch (array[i])
                {
                    case "0":
                        break;
                    case "1":
                        if (stats.player_health_inp() > Convert.ToUInt64(Properties.Settings.Default.key_using_9_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "2":
                        if (stats.player_health_inp() < Convert.ToUInt64(Properties.Settings.Default.key_using_9_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "3":
                        if (stats.player_shield_inp() > Convert.ToUInt64(Properties.Settings.Default.key_using_9_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "4":
                        if (stats.player_shield_inp() < Convert.ToUInt64(Properties.Settings.Default.key_using_9_shield_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "5":
                        if (TargetInfo.target_health_inp > Convert.ToUInt64(Properties.Settings.Default.key_using_9_target_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "6":
                        if (TargetInfo.target_health_inp < Convert.ToUInt64(Properties.Settings.Default.key_using_9_target_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "7":
                        if (TargetInfo.target_shield_inp > Convert.ToUInt64(Properties.Settings.Default.key_using_9_target_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "8":
                        if (TargetInfo.target_shield_inp < Convert.ToUInt64(Properties.Settings.Default.key_using_9_target_shield_cdt))
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
                            if (stats.player_otherpowers() > Convert.ToUInt64(Properties.Settings.Default.key_using_9_power))
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
                            if (stats.player_suitpower() > Convert.ToUInt64(Properties.Settings.Default.key_using_9_power))
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
                            if (stats.player_spellpower() > Convert.ToUInt64(Properties.Settings.Default.key_using_9_power))
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
                            if (stats.player_otherpowers() < Convert.ToUInt64(Properties.Settings.Default.key_using_9_power_under))
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
                            if (stats.player_suitpower() < Convert.ToUInt64(Properties.Settings.Default.key_using_9_power_under))
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
                            if (stats.player_spellpower() < Convert.ToUInt64(Properties.Settings.Default.key_using_9_power_under))
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
        public void skill_10(string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                switch (array[i])
                {
                    case "0":
                        break;
                    case "1":
                        if (stats.player_health_inp() > Convert.ToUInt64(Properties.Settings.Default.key_using_10_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "2":
                        if (stats.player_health_inp() < Convert.ToUInt64(Properties.Settings.Default.key_using_10_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "3":
                        if (stats.player_shield_inp() > Convert.ToUInt64(Properties.Settings.Default.key_using_10_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "4":
                        if (stats.player_shield_inp() < Convert.ToUInt64(Properties.Settings.Default.key_using_10_shield_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "5":
                        if (TargetInfo.target_health_inp > Convert.ToUInt64(Properties.Settings.Default.key_using_10_target_health_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "6":
                        if (TargetInfo.target_health_inp < Convert.ToUInt64(Properties.Settings.Default.key_using_10_target_health_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "7":
                        if (TargetInfo.target_shield_inp > Convert.ToUInt64(Properties.Settings.Default.key_using_10_target_shield_over_cdt))
                        {
                            //stats_bool = true;
                        }
                        else
                        {
                            stats_bool = false;
                        }
                        break;
                    case "8":
                        if (TargetInfo.target_shield_inp < Convert.ToUInt64(Properties.Settings.Default.key_using_10_target_shield_cdt))
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
                            if (stats.player_otherpowers() > Convert.ToUInt64(Properties.Settings.Default.key_using_10_power))
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
                            if (stats.player_suitpower() > Convert.ToUInt64(Properties.Settings.Default.key_using_10_power))
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
                            if (stats.player_spellpower() > Convert.ToUInt64(Properties.Settings.Default.key_using_10_power))
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
                            if (stats.player_otherpowers() < Convert.ToUInt64(Properties.Settings.Default.key_using_10_power_under))
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
                            if (stats.player_suitpower() < Convert.ToUInt64(Properties.Settings.Default.key_using_10_power_under))
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
                            if (stats.player_spellpower() < Convert.ToUInt64(Properties.Settings.Default.key_using_10_power_under))
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
    }
}
