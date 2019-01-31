using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSpot
{
    class PlayerBots
    {
        // Bot amount
        public static UInt64 bots_amount = 0;

        // Key clicking
        private int clicking_amount = 0;

        // Stats classs
        Stats stats = new Stats();

        public void summon_bots()
        {
            // Check if they are alive
            check_bots_alive();

            // Summon bots
            if(stats.bots_amount() != bots_amount)
            {
                if(Properties.Settings.Default.bot_1_key != "" && clicking_amount == 0)
                {
                    // Summon if there is no cooldown
                    key_switcher(Convert.ToInt32(Properties.Settings.Default.bot_1_key)); 
                }

                if (Properties.Settings.Default.bot_2_key != "" && clicking_amount == 1)
                {
                    // Summon if there is no cooldown
                    key_switcher(Convert.ToInt32(Properties.Settings.Default.bot_2_key));
                }
            }
        }

        private void key_switcher(int key)
        {
            switch(key)
            {
                case 1:
                    if(stats.actionbar_slot_1_cooldown() == 0)
                    {
                        // Click the key
                        Keysimulation.SimulateKeys.KeySwitch(Convert.ToInt32(key) - 1);
                        Keysimulation.SimulateKeys.KeySwitch(Convert.ToInt32(key) - 1);

                        if(key == Convert.ToInt32(Properties.Settings.Default.bot_1_key))
                        {
                            // Add amount to clicking amount to click the next key
                            clicking_amount = 1;
                        }
                        else if (key == Convert.ToInt32(Properties.Settings.Default.bot_2_key))
                        {
                            // Add amount to clicking amount to click the next key
                            clicking_amount = 0;
                        }
                    }
                    break;
                case 2:
                    if (stats.actionbar_slot_2_cooldown() == 0)
                    {
                        // Click the key
                        Keysimulation.SimulateKeys.KeySwitch(Convert.ToInt32(key) - 1);
                        Keysimulation.SimulateKeys.KeySwitch(Convert.ToInt32(key) - 1);

                        if (key == Convert.ToInt32(Properties.Settings.Default.bot_1_key))
                        {
                            // Add amount to clicking amount to click the next key
                            clicking_amount = 1;
                        }
                        else if (key == Convert.ToInt32(Properties.Settings.Default.bot_2_key))
                        {
                            // Add amount to clicking amount to click the next key
                            clicking_amount = 0;
                        }
                    }
                    break;
                case 3:
                    if (stats.actionbar_slot_3_cooldown() == 0)
                    {
                        // Click the key
                        Keysimulation.SimulateKeys.KeySwitch(Convert.ToInt32(key) - 1);
                        Keysimulation.SimulateKeys.KeySwitch(Convert.ToInt32(key) - 1);

                        if (key == Convert.ToInt32(Properties.Settings.Default.bot_1_key))
                        {
                            // Add amount to clicking amount to click the next key
                            clicking_amount = 1;
                        }
                        else if (key == Convert.ToInt32(Properties.Settings.Default.bot_2_key))
                        {
                            // Add amount to clicking amount to click the next key
                            clicking_amount = 0;
                        }
                    }
                    break;
                case 4:
                    if (stats.actionbar_slot_4_cooldown() == 0)
                    {
                        // Click the key
                        Keysimulation.SimulateKeys.KeySwitch(Convert.ToInt32(key) - 1);
                        Keysimulation.SimulateKeys.KeySwitch(Convert.ToInt32(key) - 1);

                        if (key == Convert.ToInt32(Properties.Settings.Default.bot_1_key))
                        {
                            // Add amount to clicking amount to click the next key
                            clicking_amount = 1;
                        }
                        else if (key == Convert.ToInt32(Properties.Settings.Default.bot_2_key))
                        {
                            // Add amount to clicking amount to click the next key
                            clicking_amount = 0;
                        }
                    }
                    break;
                case 5:
                    if (stats.actionbar_slot_5_cooldown() == 0)
                    {
                        // Click the key
                        Keysimulation.SimulateKeys.KeySwitch(Convert.ToInt32(key) - 1);
                        Keysimulation.SimulateKeys.KeySwitch(Convert.ToInt32(key) - 1);

                        if (key == Convert.ToInt32(Properties.Settings.Default.bot_1_key))
                        {
                            // Add amount to clicking amount to click the next key
                            clicking_amount = 1;
                        }
                        else if (key == Convert.ToInt32(Properties.Settings.Default.bot_2_key))
                        {
                            // Add amount to clicking amount to click the next key
                            clicking_amount = 0;
                        }
                    }
                    break;
                case 6:
                    if (stats.actionbar_slot_6_cooldown() == 0)
                    {
                        // Click the key
                        Keysimulation.SimulateKeys.KeySwitch(Convert.ToInt32(key) - 1);
                        Keysimulation.SimulateKeys.KeySwitch(Convert.ToInt32(key) - 1);

                        if (key == Convert.ToInt32(Properties.Settings.Default.bot_1_key))
                        {
                            // Add amount to clicking amount to click the next key
                            clicking_amount = 1;
                        }
                        else if (key == Convert.ToInt32(Properties.Settings.Default.bot_2_key))
                        {
                            // Add amount to clicking amount to click the next key
                            clicking_amount = 0;
                        }
                    }
                    break;
                case 7:
                    if (stats.actionbar_slot_7_cooldown() == 0)
                    {
                        // Click the key
                        Keysimulation.SimulateKeys.KeySwitch(Convert.ToInt32(key) - 1);
                        Keysimulation.SimulateKeys.KeySwitch(Convert.ToInt32(key) - 1);

                        if (key == Convert.ToInt32(Properties.Settings.Default.bot_1_key))
                        {
                            // Add amount to clicking amount to click the next key
                            clicking_amount = 1;
                        }
                        else if (key == Convert.ToInt32(Properties.Settings.Default.bot_2_key))
                        {
                            // Add amount to clicking amount to click the next key
                            clicking_amount = 0;
                        }
                    }
                    break;
                case 8:
                    if (stats.actionbar_slot_8_cooldown() == 0)
                    {
                        // Click the key
                        Keysimulation.SimulateKeys.KeySwitch(Convert.ToInt32(key) - 1);
                        Keysimulation.SimulateKeys.KeySwitch(Convert.ToInt32(key) - 1);

                        if (key == Convert.ToInt32(Properties.Settings.Default.bot_1_key))
                        {
                            // Add amount to clicking amount to click the next key
                            clicking_amount = 1;
                        }
                        else if (key == Convert.ToInt32(Properties.Settings.Default.bot_2_key))
                        {
                            // Add amount to clicking amount to click the next key
                            clicking_amount = 0;
                        }
                    }
                    break;
            }
        }

        public void check_bots_alive()
        {
            if ((Properties.Settings.Default.bot_1_use && !Properties.Settings.Default.bot_2_use) | (!Properties.Settings.Default.bot_1_use && Properties.Settings.Default.bot_2_use))
            {
                bots_amount = 1;
            }
            else if (Properties.Settings.Default.bot_1_use && Properties.Settings.Default.bot_2_use)
            {
                bots_amount = 2;
            }
            else if (!Properties.Settings.Default.bot_1_use && !Properties.Settings.Default.bot_2_use)
            {
                bots_amount = 0;
            }
        }
    }
}
