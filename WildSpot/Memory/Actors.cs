using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel;
using System.Windows.Threading;

namespace StarSpot
{
    class Actors : System.IDisposable
    {
        // Stats class
        Stats stats = new Stats();

        // Entity stats
        public UInt64 id;
        public string name;
        public UInt64 level;
        public UInt64 typ;
        public UInt64 health;
        public UInt64 health_inp;
        public UInt64 shield;
        public UInt64 shield_inp;
        public UInt64 target_target_id;
        public UInt64 iscasting;
        public UInt64 unitowner;
        public UInt64 aggro;
        public float position_x;
        public float position_y;
        public float position_z;

        // PTR 
        public UInt64 PtrEntity;
        public UInt64 _PtrEntity;

        public Actors()
        {
            
        }

        public Actors(UInt64 ptr)
        {
            this.PtrEntity = ptr;
            SetZero();
            this.update();
        }

        void IDisposable.Dispose() 
        {
            this.Dispose(false);
            System.GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            this.name = null;
            this.id = 0;
            this.health = 0;
            this.shield = 0;
            this.typ = 0;
            this.target_target_id = 0;
            this.position_x = 0;
            this.position_y = 0;
            GC.Collect();
        }

        // Update the actors list with stats
        public void update()
        {
            try
            {
                // Enter entity information
                id = objc_id();
                name = objc_name();
                level = objc_level();
                typ = objc_typ();
                health = objc_health();
                health_inp = objc_health_inp();
                shield = objc_shield();
                shield_inp = objc_shield_inp();
                iscasting = objc_iscasting();
                unitowner = objc_unitowner();
                aggro = objc_aggro();
                target_target_id = objc_targetid();
                position_x = objc_position_x();
                position_y = objc_position_y();
                position_z = objc_position_z();
            }
            catch { }

            try
            {
                if (stats.player_targetid() != 0 && stats.player_aggro() != 0)
                {
                    if (id == stats.player_targetid() && id != stats.player_id() && (stats.player_targetclass() != 2 | Properties.Settings.Default.aim_friendly_target | Properties.Settings.Default.mods == "Gathering"))
                    {
                        if (typ != 0)
                        {
                            objc_typ_writx();
                        }
                    }
                }
            }
            catch { }
        }

        // Memory reading
        private string objc_name()
        {
            string name = "";

            try
            {
                if (MainWindow.client_selected == "x64")
                {
                    string character_name = ProcessReader.readString((long)ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)PtrEntity + 0x60) + 0x10) + 0x0);

                    name = character_name;
                }
                else if (MainWindow.client_selected == "x86")
                {
                    string character_name = ProcessReader.readString(ProcessReader.readUInt(ProcessReader.readUInt((long)PtrEntity + 0x48) + 0x8) + 0x0);

                    name = character_name;
                }
            }
            catch { }

            return name;
        }
        private UInt64 objc_id()
        {
            UInt64 id = 0;

            try
            {
                if (MainWindow.client_selected == "x64")
                {
                    UInt64 target_id = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)PtrEntity + 0x60) + 0x8);

                    id = target_id;
                }
                else if (MainWindow.client_selected == "x86")
                {
                    UInt64 target_id = ProcessReader.readUInt(ProcessReader.readUInt((long)PtrEntity + 0x48) + 0x4);

                    id = target_id;
                }
            }
            catch { }

            return id;
        }
        private UInt64 objc_level()
        {
            UInt64 level = 0;

            try
            {
                if (MainWindow.client_selected == "x64")
                {
                    UInt64 memory = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)PtrEntity + 0x60) + 0x38);

                    level = memory;
                }
                else if (MainWindow.client_selected == "x86")
                {
                    UInt64 memory = ProcessReader.readUInt(ProcessReader.readUInt((long)PtrEntity + 0x48) + 0x28);

                    level = memory;
                }
            }
            catch { }

            return level;
        } 
        private UInt64 objc_typ()
        {
            UInt64 klass = 0;

            try
            {
                if (MainWindow.client_selected == "x64")
                {
                    UInt64 memory = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)PtrEntity + 0x60) + 0x80);

                    klass = memory;
                }
                else if (MainWindow.client_selected == "x86")
                {
                    UInt64 memory = ProcessReader.readUInt(ProcessReader.readUInt((long)PtrEntity + 0x48) + 0x58);

                    klass = memory;
                }
            }
            catch { }

            return klass;
        }
        private void objc_typ_writx()
        {
            try
            {
                if (MainWindow.client_selected == "x64")
                {
                    ProcessReader.WriteUInt(ProcessReader.readUInt64((long)PtrEntity + 0x60) + 0x80, 0);
                }
                else if (MainWindow.client_selected == "x86")
                {
                    ProcessReader.WriteUInt(ProcessReader.readUInt((long)PtrEntity + 0x48) + 0x58, 0);
                }
            }
            catch { }
        }
        private UInt64 objc_health()
        {
            UInt64 health = 0;

            try
            {
                if (MainWindow.client_selected == "x64")
                {
                    UInt64 memory = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)PtrEntity + 0x60) + 0x40);

                    health = memory;
                }
                else if (MainWindow.client_selected == "x86")
                {
                    UInt64 memory = ProcessReader.readUInt(ProcessReader.readUInt((long)PtrEntity + 0x48) + 0x30);

                    health = memory;
                }
            }
            catch { }

            return health;
        }
        private UInt64 objc_health_inp()
        {
            UInt64 health = 0;

            try
            {
                if (MainWindow.client_selected == "x64")
                {
                    UInt64 character_health = objc_health();
                    float character_health_max = ProcessReader.readFloat((long)ProcessReader.readUInt64((long)PtrEntity + 0x60) + 0x5fc);

                    if (character_health_max != 0)
                    {
                        health = (character_health * 100) / Convert.ToUInt64(character_health_max);
                    }
                }
                else if (MainWindow.client_selected == "x86")
                {
                    UInt64 character_health = objc_health();
                    float character_health_max = ProcessReader.readFloat(ProcessReader.readUInt((long)PtrEntity + 0x48) + 0x400);

                    if (character_health_max != 0)
                    {
                        health = (character_health * 100) / Convert.ToUInt64(character_health_max);
                    }
                }
            }
            catch { }

            return health;
        }
        private UInt64 objc_shield()
        {
            UInt64 shield = 0;

            try
            {
                if (MainWindow.client_selected == "x64")
                {
                    UInt64 memory = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)PtrEntity + 0x60) + 0x48);

                    shield = memory;
                }
                else if (MainWindow.client_selected == "x86")
                {
                    UInt64 memory = ProcessReader.readUInt(ProcessReader.readUInt((long)PtrEntity + 0x48) + 0x38);

                    shield = memory;
                }
            }
            catch { }

            return shield;
        }
        private UInt64 objc_shield_inp()
        {
            UInt64 shield_power = 0;

            try
            {
                if (MainWindow.client_selected == "x64")
                {
                    UInt64 character_shield = objc_shield();
                    float character_shield_max = ProcessReader.readFloat((long)ProcessReader.readUInt64((long)PtrEntity + 0x60) + 0x70c);

                    if (character_shield_max != 0)
                    {
                        shield_power = (character_shield * 100) / Convert.ToUInt64(character_shield_max);
                    }
                }
                else if (MainWindow.client_selected == "x86")
                {
                    UInt64 character_shield = objc_health();
                    float character_shield_max = ProcessReader.readFloat(ProcessReader.readUInt((long)PtrEntity + 0x48) + 0x514);

                    if (character_shield_max != 0)
                    {
                        shield_power = (character_shield * 100) / Convert.ToUInt64(character_shield_max);
                    }
                }
            }
            catch { }

            return shield_power;
        }
        private UInt64 objc_targetid()
        {
            UInt64 id = 0;

            try
            {
                if (MainWindow.client_selected == "x64")
                {
                    UInt64 memory = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)PtrEntity + 0x60) + 0x108);

                    id = memory;
                }
                else if (MainWindow.client_selected == "x86")
                {
                    UInt64 memory = ProcessReader.readUInt(ProcessReader.readUInt((long)PtrEntity + 0x48) + 0xb0);

                    id = memory;
                }
            }
            catch { }

            return id;
        }
        private UInt64 objc_targetclass()
        {
            UInt64 id = 0;

            try
            {
                if (MainWindow.client_selected == "x64")
                {
                    UInt64 memory = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)PtrEntity + 0x60) + 0x10c);

                    id = memory;
                }
                else if (MainWindow.client_selected == "x86")
                {
                    UInt64 memory = ProcessReader.readUInt(ProcessReader.readUInt((long)PtrEntity + 0x48) + 0xb4);

                    id = memory;
                }
            }
            catch { }

            return id;
        }
        private UInt64 objc_iscasting()
        {
            UInt64 casting = 0;

            try
            {
                if (MainWindow.client_selected == "x64")
                {
                    UInt64 memory = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)PtrEntity + 0x60) + 0x15e0);

                    casting = memory;
                }
                else if (MainWindow.client_selected == "x86")
                {
                    UInt64 memory = ProcessReader.readUInt(ProcessReader.readUInt((long)PtrEntity + 0x48) + 0x126c); 

                    casting = memory;
                }
            }
            catch { }

            return casting;
        }
        private UInt64 objc_unitowner()
        {
            UInt64 owner = 0;

            try
            {
                if (MainWindow.client_selected == "x64")
                {
                    UInt64 memory = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)PtrEntity + 0x60) + 0x2b0);

                    //if (memory == 4706249)
                    //{
                    //    owner = 1;
                    //}

                    owner = memory;
                }
                else if (MainWindow.client_selected == "x86")
                {
                    UInt64 memory = ProcessReader.readUInt(ProcessReader.readUInt((long)PtrEntity + 0x48) + 0x1F0);

                    owner = memory;
                }
            }
            catch { }

            return owner;
        } 
        private UInt64 objc_aggro()
        {
            UInt64 aggro = 0;

            try
            {
                if (MainWindow.client_selected == "x64")
                {
                    UInt64 memory = ProcessReader.readUInt((long)ProcessReader.readUInt64((long)PtrEntity + 0x60) + 0x2ac);

                    aggro = memory;
                }
                else if (MainWindow.client_selected == "x86")
                {
                    UInt64 memory = ProcessReader.readUInt(ProcessReader.readUInt((long)PtrEntity + 0x48) + 0x1ec);

                    aggro = memory;
                }
            }
            catch { }

            return aggro;
        }
        private float objc_position_x()
        {
            float position = 0;

            try
            {
                if (MainWindow.client_selected == "x64")
                {
                    float x = ProcessReader.readFloat((long)ProcessReader.readUInt64((long)PtrEntity + 0x60) + 0xf60);

                    position = x;
                }
                else if (MainWindow.client_selected == "x86")
                {
                    float x = ProcessReader.readFloat(ProcessReader.readUInt((long)PtrEntity + 0x48) + 0xf10);

                    position = x;
                }
            }
            catch { }

            return position;
        }
        private float objc_position_y()
        {
            float position = 0;

            try
            {
                if (MainWindow.client_selected == "x64")
                {
                    float y = ProcessReader.readFloat((long)ProcessReader.readUInt64((long)PtrEntity + 0x60) + 0xf68);

                    position = y;
                }
                else if (MainWindow.client_selected == "x86")
                {
                    float y = ProcessReader.readFloat(ProcessReader.readUInt((long)PtrEntity + 0x48) + 0xf18);

                    position = y;
                }
            }
            catch { }

            return position;
        }
        private float objc_position_z()
        {
            float position = 0;

            try
            {
                if (MainWindow.client_selected == "x64")
                {
                    float y = ProcessReader.readFloat((long)ProcessReader.readUInt64((long)PtrEntity + 0x60) + 0xf64);

                    position = y;
                }
                else if (MainWindow.client_selected == "x86")
                {
                    float y = ProcessReader.readFloat(ProcessReader.readUInt((long)PtrEntity + 0x48) + 0xf14);

                    position = y;
                }
            }
            catch { }

            return position;
        }
        private void SetZero()
        {
            this._PtrEntity = 0;
            this.name = "";
            this.id = 0;
            this.health = 0;
            this.shield = 0;
            this.typ = 0;
            this.target_target_id = 0;
            this.position_x = 0;
            this.position_y = 0;
        }
    }
}
