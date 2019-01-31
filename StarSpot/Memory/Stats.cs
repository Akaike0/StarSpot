using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSpot
{
    class Stats
    {
        public static UInt32 game_manager = 0xC65898; //0xC5E5D8; //0xC2FDA0; 0xC27B60; 0xC27B40;
        UInt32 player_ctm = 0xC658B0; //0xC5E5F8; //0xC2FDC0; //0xC27B78; //0xC27B60;
        UInt32 actionbar_r_slot = 0xC89D90; //0xC82AE0;
        UInt32 pvp_match_status_x64 = 0xC63B50 + 0x2018; //0xC63B50; //0xC5E8D0; //0xC27E38; 0xC27E18; /<-
        UInt32 logincursor = 0xC67088; //0xC5FDC8; //0xC29328; //0xC29308;

        // Hacks
        UInt32 camera_view = 0xC46434;
        UInt32 camera_horizontal_view = 0xC46764;
        UInt32 camera_zoom = 0xC4DE54;
        UInt32 fps_address = 0xC3FEC4;

        public static UInt32 manager_base_x86 = 0x870C34;
        public static UInt32 game_manager_x86 = 0x870058;
        UInt32 player_ctm_x86 = 0x87011C;
        UInt32 actionbar_r_slot_x86 = 0x8C7C90;
        UInt32 pvp_match_status_x86 = 0x870168;

        public UInt64 player_id()
        {
            UInt64 id = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 player_id = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0x78) + 0x8);

                    id = player_id;
                }
                catch { }

                return id;
            }
            else if (MainWindow.client_selected == "x86")
            {
                try
                {
                    UInt64 player_id = ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.base_adress_x86 + manager_base_x86) + 0x34) + 0x3c) + 0x4);

                    id = player_id;
                }
                catch { }

                return id;
            }

            return id;
        }

        public UInt64 player_classes()
        {
            UInt64 id = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 player_id = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0x78) + 0xdc);

                    id = player_id;
                }
                catch { }

                return id;
            }
            else if (MainWindow.client_selected == "x86")
            {
                try
                {
                    UInt64 player_id = ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.base_adress_x86 + manager_base_x86) + 0x34) + 0x3c) + 0x90);

                    id = player_id;
                }
                catch { }

                return id;
            }

            return id;
        }

        public string player_name()
        {
            string name = "";

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    string character_name = ProcessReader.readString((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0x78) + 0x10) + 0x0);

                    name = character_name;
                }
                catch
                {
                    name = "";
                }

                return name;
            }
            else if (MainWindow.client_selected == "x86")
            {
                try
                {
                    string character_name = ProcessReader.readString(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.base_adress_x86 + manager_base_x86) + 0x34) + 0x3c) + 0x8) + 0x0);

                    name = character_name;
                }
                catch { }

                return name;
            }

            return name;
        }

        public UInt64 player_mountID()
        {
            UInt64 memory = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 result = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0x78) + 0xc0);

                    memory = result;
                }
                catch { }

                return memory;
            }

            return memory;
        }

        public UInt64 player_level()
        {
            UInt64 level = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 character_level = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0x78) + 0x38);

                    level = character_level;
                }
                catch { }

                return level;
            }
            else if (MainWindow.client_selected == "x86")
            {
                try
                {
                    UInt64 character_level = ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.base_adress_x86 + manager_base_x86) + 0x34) + 0x3c) + 0x28);

                    level = character_level;
                }
                catch { }

                return level;
            }

            return level;
        }

        public UInt64 player_health()
        {
            UInt64 health = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 character_health = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0x78) + 0x40);

                    health = character_health;
                }
                catch { }

                return health;
            }
            else if (MainWindow.client_selected == "x86")
            {
                try
                {
                    UInt64 character_health = ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.base_adress_x86 + manager_base_x86) + 0x34) + 0x3c) + 0x30);

                    health = character_health;
                }
                catch { }

                return health;
            }

            return health;
        }

        public UInt64 player_health_inp()
        {
            UInt64 health_inp = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 health_max = 0;

                    UInt64 character_health = player_health();
                    float character_health_inp = ProcessReader.readFloat((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0x78) + 0x5fc);

                    if (character_health_inp != 0)
                    {
                        health_max = (character_health * 100) / Convert.ToUInt64(character_health_inp); // Calculate the percent
                    }

                    health_inp = health_max;
                }
                catch { }

                return health_inp;
            }
            else if (MainWindow.client_selected == "x86")
            {
                try
                {
                    UInt64 health_max = 0;

                    UInt64 character_health = player_health();
                    float character_health_inp = ProcessReader.readFloat(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.base_adress_x86 + manager_base_x86) + 0x34) + 0x3c) + 0x400);

                    if (character_health_inp != 0)
                    {
                        health_max = (character_health * 100) / Convert.ToUInt64(character_health_inp); // Calculate the percent
                    }

                    health_inp = health_max;
                }
                catch { }

                return health_inp;
            }

            return health_inp;
        }

        public UInt64 player_shield()
        {
            UInt64 shield_power = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 character_shield = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0x78) + 0x48);

                    shield_power = character_shield;
                }
                catch { }

                return shield_power;
            }
            else if (MainWindow.client_selected == "x86")
            {
                try
                {
                    UInt64 character_shield = ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.base_adress_x86 + manager_base_x86) + 0x34) + 0x3c) + 0x38);

                    shield_power = character_shield;
                }
                catch { }

                return shield_power;
            }

            return shield_power;
        }

        public UInt64 player_shield_inp()
        {
            UInt64 shield_power = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 character_shield = player_shield();
                    float character_shield_max = ProcessReader.readFloat((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0x78) + 0x70c);

                    if (character_shield_max != 0)
                    {
                        shield_power = (character_shield * 100) / Convert.ToUInt64(character_shield_max);
                    }
                    else
                    {
                        shield_power = 100;
                    }
                }
                catch { }

                return shield_power;
            }
            else if (MainWindow.client_selected == "x86")
            {
                try
                {
                    UInt64 character_shield = player_shield();
                    float character_shield_max = ProcessReader.readFloat(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.base_adress_x86 + manager_base_x86) + 0x34) + 0x3c) + 0x514);

                    if (character_shield_max != 0)
                    {
                        shield_power = (character_shield * 100) / Convert.ToUInt64(character_shield_max);
                    }
                    else
                    {
                        shield_power = 100;
                    }
                }
                catch { }

                return shield_power;
            }

            return shield_power;
        }

        public UInt64 player_spellpower()
        {
            UInt64 result = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 memory = Convert.ToUInt32(ProcessReader.readFloat((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0x78) + 0x22c));

                    result = memory;
                }
                catch { }

                return result;
            }
            else if (MainWindow.client_selected == "x86")
            {
                try
                {
                    UInt64 memory = ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.base_adress_x86 + manager_base_x86) + 0x34) + 0x3c) + 0x190);

                    result = memory;
                }
                catch { }

                return result;
            }

            return result;
        }

        public UInt64 player_suitpower()
        {
            UInt64 result = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 memory = Convert.ToUInt32(ProcessReader.readFloat((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0x78) + 0x228));

                    result = memory;
                }
                catch { }

                return result;
            }
            else if (MainWindow.client_selected == "x86")
            {
                try
                {
                    UInt64 memory = ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.base_adress_x86 + manager_base_x86) + 0x34) + 0x3c) + 0x18c);

                    result = memory;
                }
                catch { }

                return result;
            }

            return result;
        }

        public UInt64 player_otherpowers()
        {
            UInt64 result = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 memory = Convert.ToUInt32(ProcessReader.readFloat((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0x78) + 0x220));

                    result = memory;
                }
                catch { }

                return result;
            }
            else if (MainWindow.client_selected == "x86")
            {
                try
                {
                    UInt64 memory = ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.base_adress_x86 + manager_base_x86) + 0x34) + 0x3c) + 0x184);

                    result = memory;
                }
                catch { }

                return result;
            }

            return result;
        }

        public UInt64 player_targetid()
        {
            UInt64 id = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 target_id = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0x78) + 0x108);

                    id = target_id;
                }
                catch { }

                return id;
            }
            else if (MainWindow.client_selected == "x86")
            {
                try
                {
                    UInt64 target_id = ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.base_adress_x86 + manager_base_x86) + 0x34) + 0x3c) + 0xb0);

                    id = target_id;
                }
                catch { }

                return id;
            }

            return id;
        }

        public void player_targetid(uint id)
        {
            if (MainWindow.client_selected == "x64")
            {
                ProcessReader.WriteUInt(ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0x78) + 0x108, id);
            }
            else if (MainWindow.client_selected == "x86")
            {
                ProcessReader.WriteUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.base_adress_x86 + manager_base_x86) + 0x34) + 0x3c) + 0xb0, id);
            }
        }

        public void player_targetHandle(float handle)
        {
            //if (MainWindow.client_selected == "x64")
            //{
            //    ProcessReader.writeFloat((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0x58) + 0x20) + 0x6580, handle);
            //}
        }

        public UInt64 player_class()
        {
            UInt64 class_id = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 target_class = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0x78) + 0x80);

                    class_id = target_class;
                }
                catch { }

                return class_id;
            }
            else if (MainWindow.client_selected == "x86")
            {
                try
                {
                    UInt64 target_class = ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.base_adress_x86 + manager_base_x86) + 0x34) + 0x3c) + 0x70);

                    class_id = target_class;
                }
                catch { }

                return class_id;
            }

            return class_id;
        }

        public UInt64 player_targetclass()
        {
            UInt64 class_id = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 target_class = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0x78) + 0x10c);

                    class_id = target_class;
                }
                catch { }

                return class_id;
            }
            else if (MainWindow.client_selected == "x86")
            {
                try
                {
                    UInt64 target_class = ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.base_adress_x86 + manager_base_x86) + 0x34) + 0x3c) + 0xb4);

                    class_id = target_class;
                }
                catch { }

                return class_id;
            }

            return class_id;
        }

        public UInt64 player_aggro()
        {
            UInt64 aggro = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 player_aggro_status = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0x78) + 0x2ac);

                    aggro = player_aggro_status;
                }
                catch { }

                return aggro;
            }
            else if (MainWindow.client_selected == "x86")
            {
                try
                {
                    UInt64 player_aggro_status = ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.base_adress_x86 + manager_base_x86) + 0x34) + 0x3c) + 0x1ec);

                    aggro = player_aggro_status;
                }
                catch { }

                return aggro;
            }

            return aggro;
        }

        public UInt64 player_iscasting()
        {
            UInt64 casting = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 player_casting_status = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0x78) + 0x15e0); // 1560

                    casting = player_casting_status;
                }
                catch { }

                return casting;
            }
            else if (MainWindow.client_selected == "x86")
            {
                try
                {
                    UInt64 player_casting_status = ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.base_adress_x86 + manager_base_x86) + 0x34) + 0x3c) + 0x126c);

                    casting = player_casting_status;
                }
                catch { }

                return casting;
            }

            return casting;
        }

        public UInt64 player_looting()
        {
            UInt64 result = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 memory = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0x58) + 0x20) + 0x6690); // 0x6860

                    result = memory;
                }
                catch { }

                return result;
            }

            return result;
        }

        public UInt64 pvp_match_status()
        {
            UInt64 status = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 memory = ProcessReader.readUInt((long)ProcessReader.readUInt64(ProcessReader.base_adress + pvp_match_status_x64) + 0x114);

                    status = memory;
                }
                catch { }

                return status;
            }
            else if (MainWindow.client_selected == "x86")
            {
                try
                {
                    UInt64 memory = ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.base_adress_x86 + pvp_match_status_x86) + 0x94);

                    status = memory;
                }
                catch { }

                return status;
            }

            return status;
        }

        public UInt64 pvp_match_ready()
        {
            UInt64 status = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 memory = ProcessReader.readUInt((long)ProcessReader.readUInt64(ProcessReader.base_adress + pvp_match_status_x64) + 0xD0);

                    status = memory;
                }
                catch { }

                return status;
            }
            else if (MainWindow.client_selected == "x86")
            {
                try
                {
                    UInt64 memory = ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.base_adress_x86 + pvp_match_status_x86) + 0x94);

                    status = memory;
                }
                catch { }

                return status;
            }

            return status;
        }

        public UInt64 pvp_match_ingame()
        {
            UInt64 status = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 memory = ProcessReader.readUInt((long)ProcessReader.readUInt64(ProcessReader.base_adress + pvp_match_status_x64) + 0x150);

                    status = memory;
                }
                catch { }

                return status;
            }
            else if (MainWindow.client_selected == "x86")
            {
                try
                {
                    UInt64 memory = ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.base_adress_x86 + pvp_match_status_x86) + 0x94);

                    status = memory;
                }
                catch { }

                return status;
            }

            return status;
        }

        //public void cursor_y(uint position)
        //{
        //    try
        //    {
        //        ProcessReader.WriteUInt(ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64(ProcessReader.base_adress + game_manager) + 0x10158) + 0x48) + 0xe8c, position);
        //    }
        //    catch { }
        //}
        //public UInt64 cursor_y()
        //{
        //    UInt64 status = 0;

        //    if (MainWindow.client_selected == "x64")
        //    {
        //        try
        //        {
        //            UInt64 memory = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64(ProcessReader.base_adress + game_manager) + 0x10158) + 0x48) + 0xe8c);

        //            status = memory;
        //        }
        //        catch { }

        //        return status;
        //    }

        //    return status;
        //}
        //public void cursor_x(uint position)
        //{
        //    try
        //    {
        //        ProcessReader.WriteUInt(ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64(ProcessReader.base_adress + game_manager) + 0x10158) + 0x48) + 0xe90, position);
        //    }
        //    catch { }
        //}
        //public UInt64 cursor_x()
        //{
        //    UInt64 status = 0;

        //    if (MainWindow.client_selected == "x64")
        //    {
        //        try
        //        {
        //            UInt64 memory = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64(ProcessReader.base_adress + game_manager) + 0x10158) + 0x48) + 0xe90);

        //            status = memory;
        //        }
        //        catch { }

        //        return status;
        //    }

        //    return status;
        //}

        public UInt64 cursor_login_x()
        {
            UInt64 status = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 memory = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + logincursor) + 0x40) + 0x2cc);

                    status = memory;
                }
                catch { }

                return status;
            }

            return status;
        }
        public void cursor_login_x(uint position)
        {
            try
            {
                ProcessReader.WriteUInt(ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + logincursor) + 0x40) + 0x2cc, position);
            }
            catch { }
        }
        public UInt64 cursor_login_y()
        {
            UInt64 status = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 memory = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + logincursor) + 0x40) + 0x2d0);

                    status = memory;
                }
                catch { }

                return status;
            }

            return status;
        }
        public void cursor_login_y(uint position)
        {
            try
            {
                ProcessReader.WriteUInt(ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + logincursor) + 0x40) + 0x2d0, position);
            }
            catch { }
        }

        public UInt64 actionbar_slot_r_cooldown()
        {
            UInt64 cooldown = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 memory = ProcessReader.readUInt((long)ProcessReader.readUInt64(ProcessReader.base_adress + actionbar_r_slot) + 0x128);

                    cooldown = memory;
                }
                catch { }

                return cooldown;
            }
            else if (MainWindow.client_selected == "x86")
            {
                try
                {
                    UInt64 memory = ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.base_adress_x86 + actionbar_r_slot_x86) + 0x68);

                    cooldown = memory;
                }
                catch { }

                return cooldown;
            }

            return cooldown;
        }
        public UInt64 actionbar_slot_1_cooldown()
        {
            UInt64 casting = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 player_casting_status;

                    if (Properties.Settings.Default.action_set_selection == "Set 1")
                    {
                        player_casting_status = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xAC0) + 0x128);
                    }
                    else
                    {
                        player_casting_status = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xC40) + 0x128);
                    }

                    casting = player_casting_status;
                }
                catch { }

                return casting;
            }

            return casting;
        }
        public UInt64 actionbar_slot_2_cooldown()
        {
            UInt64 casting = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 player_casting_status;

                    if (Properties.Settings.Default.action_set_selection == "Set 1")
                    {
                        player_casting_status = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xAC8) + 0x128);
                    }
                    else
                    {
                        player_casting_status = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xC48) + 0x128);
                    }

                    casting = player_casting_status;
                }
                catch { }

                return casting;
            }

            return casting;
        }
        public UInt64 actionbar_slot_3_cooldown()
        {
            UInt64 casting = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 player_casting_status;

                    if (Properties.Settings.Default.action_set_selection == "Set 1")
                    {
                        player_casting_status = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xAD0) + 0x128);
                    }
                    else
                    {
                        player_casting_status = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xC50) + 0x128);
                    }

                    casting = player_casting_status;
                }
                catch { }

                return casting;
            }

            return casting;
        }
        public UInt64 actionbar_slot_4_cooldown()
        {
            UInt64 casting = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 player_casting_status;

                    if (Properties.Settings.Default.action_set_selection == "Set 1")
                    {
                        player_casting_status = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xAD8) + 0x128);
                    }
                    else
                    {
                        player_casting_status = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xC58) + 0x128);
                    }

                    casting = player_casting_status;
                }
                catch { }

                return casting;
            }

            return casting;
        }
        public UInt64 actionbar_slot_5_cooldown()
        {
            UInt64 casting = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 player_casting_status;

                    if (Properties.Settings.Default.action_set_selection == "Set 1")
                    {
                        player_casting_status = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xAE0) + 0x128);
                    }
                    else
                    {
                        player_casting_status = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xC60) + 0x128);
                    }

                    casting = player_casting_status;
                }
                catch { }

                return casting;
            }

            return casting;
        }
        public UInt64 actionbar_slot_6_cooldown()
        {
            UInt64 casting = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 player_casting_status;

                    if (Properties.Settings.Default.action_set_selection == "Set 1")
                    {
                        player_casting_status = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xAE8) + 0x128);
                    }
                    else
                    {
                        player_casting_status = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xC68) + 0x128);
                    }

                    casting = player_casting_status;
                }
                catch { }

                return casting;
            }

            return casting;
        }
        public UInt64 actionbar_slot_7_cooldown()
        {
            UInt64 casting = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 player_casting_status;

                    if (Properties.Settings.Default.action_set_selection == "Set 1")
                    {
                        player_casting_status = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xAF0) + 0x128);
                    }
                    else
                    {
                        player_casting_status = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xC70) + 0x128);
                    }

                    casting = player_casting_status;
                }
                catch { }

                return casting;
            }

            return casting;
        }
        public UInt64 actionbar_slot_8_cooldown()
        {
            UInt64 casting = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 player_casting_status;

                    if (Properties.Settings.Default.action_set_selection == "Set 1")
                    {
                        player_casting_status = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xAF8) + 0x128);
                    }
                    else
                    {
                        player_casting_status = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xC78) + 0x128);
                    }

                    casting = player_casting_status;
                }
                catch { }

                return casting;
            }

            return casting;
        }
        public UInt64 actionbar_slots_id(int slot)
        {
            UInt64 id = 0;

            try
            {
                switch (slot)
                {
                    default:
                        return 0;
                    case 1:
                        if (Properties.Settings.Default.action_set_selection == "Set 1")
                        {
                            id = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xAC0) + 0x118);
                        }
                        else
                        {
                            id = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xC40) + 0x118);
                        }
                        return id;
                    case 2:
                        if (Properties.Settings.Default.action_set_selection == "Set 1")
                        {
                            id = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xAC8) + 0x118);
                        }
                        else
                        {
                            id = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xC48) + 0x118);
                        }
                        return id;
                    case 3:
                        if (Properties.Settings.Default.action_set_selection == "Set 1")
                        {
                            id = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xAD0) + 0x118);
                        }
                        else
                        {
                            id = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xC50) + 0x118);
                        }
                        return id;
                    case 4:
                        if (Properties.Settings.Default.action_set_selection == "Set 1")
                        {
                            id = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xAD8) + 0x118);
                        }
                        else
                        {
                            id = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xC58) + 0x118);
                        }
                        return id;
                    case 5:
                        if (Properties.Settings.Default.action_set_selection == "Set 1")
                        {
                            id = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xAE0) + 0x118);
                        }
                        else
                        {
                            id = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xC60) + 0x118);
                        }
                        return id;
                    case 6:
                        if (Properties.Settings.Default.action_set_selection == "Set 1")
                        {
                            id = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xAE8) + 0x118);
                        }
                        else
                        {
                            id = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xC68) + 0x118);
                        }
                        return id;
                    case 7:
                        if (Properties.Settings.Default.action_set_selection == "Set 1")
                        {
                            id = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xAF0) + 0x118);
                        }
                        else
                        {
                            id = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xC70) + 0x118);
                        }
                        return id;
                    case 8:
                        if (Properties.Settings.Default.action_set_selection == "Set 1")
                        {
                            id = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xAF8) + 0x118);
                        }
                        else
                        {
                            id = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + game_manager) + 0xC78) + 0x118);
                        }
                        return id;
                }
            }
            catch
            {
                return 0;
            }
        }

        public float player_position_x()
        {
            float position = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    float player_position = ProcessReader.readFloat((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + player_ctm) + 0x20) + 0xD00);

                    position = player_position;
                }
                catch { }

                return position;
            }

            return position;
        }
        public float player_position_y()
        {
            float position = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    float player_position = ProcessReader.readFloat((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + player_ctm) + 0x20) + 0xD08);

                    position = player_position;
                }
                catch { }

                return position;
            }

            return position;
        }
        public float player_position_z()
        {
            float position = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    float player_position = ProcessReader.readFloat((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + player_ctm) + 0x20) + 0xD04);

                    position = player_position;
                }
                catch { }

                return position;
            }

            return position;
        }

        public float player_ctm_x()
        {
            float position = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    float player_position = ProcessReader.readFloat((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + player_ctm) + 0x20) + 0x410);

                    position = player_position;
                }
                catch { }

                return position;
            }

            return position;
        }
        public float player_ctm_y()
        {
            float position = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    float player_position = ProcessReader.readFloat((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + player_ctm) + 0x20) + 0x418);

                    position = player_position;
                }
                catch { }

                return position;
            }

            return position;
        }

        public void player_ctm_x(float cordinates)
        {
            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    ProcessReader.writeFloat((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + player_ctm) + 0x20) + 0x410, cordinates);
                }
                catch { }
            }
            else if (MainWindow.client_selected == "x86")
            {
                try
                {
                    ProcessReader.writeFloat(ProcessReader.readUInt(ProcessReader.base_adress_x86 + player_ctm_x86) + 0x5e8, cordinates);
                }
                catch { }
            }
        }
        public void player_ctm_y(float cordinates)
        {
            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    ProcessReader.writeFloat((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + player_ctm) + 0x20) + 0x418, cordinates);
                }
                catch { }
            }
            else if (MainWindow.client_selected == "x86")
            {
                try
                {
                    ProcessReader.writeFloat(ProcessReader.readUInt(ProcessReader.base_adress_x86 + player_ctm_x86) + 0x5f0, cordinates);
                }
                catch { }
            }
        }
        public void player_ctm_z(float cordinates)
        {
            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    ProcessReader.writeFloat((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + player_ctm) + 0x20) + 0x414, cordinates);
                }
                catch { }
            }
            else if (MainWindow.client_selected == "x86")
            {
                try
                {
                    ProcessReader.writeFloat(ProcessReader.readUInt(ProcessReader.base_adress_x86 + player_ctm_x86) + 0x5f0, cordinates);
                }
                catch { }
            }
        }

        public UInt64 player_ctm_push()
        {
            UInt64 status = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 player_ctm_status = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + player_ctm) + 0x20) + 0x3F4);

                    status = player_ctm_status;
                }
                catch { }

                return status;
            }
            else if (MainWindow.client_selected == "x86")
            {
                try
                {
                    UInt64 player_ctm_status = ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.base_adress_x86 + player_ctm_x86) + 0x5cc);

                    status = player_ctm_status;
                }
                catch { }

                return status;
            }

            return status;
        }
        public void player_ctm_push(uint newstatus)
        {
            try
            {
                if (MainWindow.client_selected == "x64")
                {
                    ProcessReader.WriteUInt(ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + player_ctm) + 0x20) + 0x3F4, newstatus);
                }
                else if (MainWindow.client_selected == "x86")
                {
                    ProcessReader.WriteUInt(ProcessReader.readUInt(ProcessReader.base_adress_x86 + player_ctm_x86) + 0x5cc, newstatus);
                }
            }
            catch { }
        }

        public UInt64 player_ctm_interact()
        {
            UInt64 interact = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 player_ctm_status = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + player_ctm) + 0x20) + 0x444);

                    interact = player_ctm_status;
                }
                catch { }

                return interact;
            }
            else if (MainWindow.client_selected == "x86")
            {
                try
                {
                    UInt64 player_ctm_status = ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.base_adress_x86 + player_ctm_x86) + 0x61c);

                    interact = player_ctm_status;
                }
                catch { }

                return interact;
            }

            return interact;
        }
        public void player_ctm_interact(uint newstatus)
        {
            try
            {
                if (MainWindow.client_selected == "x64")
                {
                    ProcessReader.WriteUInt(ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + player_ctm) + 0x20) + 0x444, newstatus);
                }
                else if (MainWindow.client_selected == "x86")
                {
                    ProcessReader.WriteUInt(ProcessReader.readUInt(ProcessReader.base_adress_x86 + player_ctm_x86) + 0x61c, newstatus);
                }
            }
            catch { }
        }

        public float player_ctm_distance()
        {
            float distance = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    float ctm_distance = ProcessReader.readFloat((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + player_ctm) + 0x20) + 0x42C);

                    distance = ctm_distance;
                }
                catch { }

                return distance;
            }
            else if (MainWindow.client_selected == "x86")
            {
                try
                {
                    float ctm_distance = ProcessReader.readFloat(ProcessReader.readUInt(ProcessReader.base_adress_x86 + player_ctm_x86) + 0x604);

                    distance = ctm_distance;
                }
                catch { }

                return distance;
            }

            return distance;
        }
        public void player_ctm_distance(float newstatus)
        {
            try
            {
                if (MainWindow.client_selected == "x64")
                {
                    ProcessReader.writeFloat((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + player_ctm) + 0x20) + 0x42C, newstatus);
                }
                else if (MainWindow.client_selected == "x86")
                {
                    ProcessReader.writeFloat(ProcessReader.readUInt(ProcessReader.base_adress_x86 + player_ctm_x86) + 0x604, newstatus);
                }
            }
            catch { }
        }
        // Energy 1575
        public UInt64 bots_amount()
        {
            UInt64 amount = 0;

            if (MainWindow.client_selected == "x64")
            {
                try
                {
                    UInt64 current_amount = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + player_ctm) + 0x20) + 0x2F8);

                    amount = current_amount;
                }
                catch { }

                return amount;
            }
            else if (MainWindow.client_selected == "x86")
            {
                try
                {
                    UInt64 current_amount = ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.base_adress_x86 + player_ctm_x86) + 0x4f4);

                    amount = current_amount;
                }
                catch { }

                return amount;
            }

            return amount;
        }

        // Hacks
        public float camera_views(int nr, int avalue)
        {
            // Value
            float value = 0;

            switch (nr)
            {
                case 1: // View
                    value = ProcessReader.readFloat(ProcessReader.base_adress + camera_view);
                    break;
                case 2:
                    ProcessReader.writeFloat(ProcessReader.base_adress + camera_view, avalue);
                    break;
                case 3: // Horizontal view
                    value = ProcessReader.readFloat(ProcessReader.base_adress + camera_horizontal_view);
                    break;
                case 4:
                    ProcessReader.writeFloat(ProcessReader.base_adress + camera_horizontal_view, avalue);
                    break;
                case 5:
                    value = ProcessReader.readFloat(ProcessReader.base_adress + camera_zoom);
                    break;
                case 6:
                    ProcessReader.writeFloat(ProcessReader.base_adress + camera_zoom, avalue);
                    break;
            }

            // Return
            return value;
        }
        public void fps(uint max)
        {
            try
            {
                ProcessReader.WriteUInt((ulong)ProcessReader.base_adress + fps_address, max);
            }
            catch { }
        }
        public UInt64 fps()
        {
            UInt64 result = 0;

            try
            {
                result = ProcessReader.readUInt((long)ProcessReader.base_adress + fps_address);
            }
            catch { }

            return result;
        }
    }
}
