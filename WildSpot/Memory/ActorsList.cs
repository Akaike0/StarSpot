using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
using System.ComponentModel;
using System.Windows.Threading;

namespace StarSpot
{
    class ActorsList : IEnumerable<Actors>
    {
        private Dictionary<UInt64, Actors> entities = new Dictionary<UInt64, Actors>(); // Create an entity dictionary

        public UInt64 game_manager; // Gamemanager PTR
        Int64 z = 0x6440;//0x8eb8;
        public UInt64 game_manager_x86; // Gamemanager PTR

        // Mods timer
        DispatcherTimer entity_update_timer = new DispatcherTimer();

        private HashSet<UInt64> found = null;

        public ActorsList()
        {
            found = new HashSet<UInt64>();

            entity_update_timer.Tick += new EventHandler(entity_update_timer_Tick);

            if (entity_update_timer.Interval.TotalMilliseconds != 1000)
            {
                entity_update_timer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            }

            //entity_update_timer.Start();
        }

        public void entity_update_timer_Tick(object sender, EventArgs e)
        {
            // Create a new hash set
            found = new HashSet<UInt64>();
            entities.Clear();
        }

        public void update()
        {
            if (MainWindow.client_selected == "x64")
            {
                // Create a new hash set
                found = new HashSet<UInt64>();

                // Get the current objects 75e8
                try
                {
                    game_manager = ProcessReader.readUInt64((long)ProcessReader.readUInt64((long)ProcessReader.base_adress + Stats.game_manager) + 0x6488);
                }
                catch { }

                while (game_manager != 0) // If object isn't null, add it
                {
                    try
                    {
                        if (!found.Contains(game_manager))
                        {
                            entities[game_manager] = new Actors(game_manager); // Add the object to the entities dictionary
                            found.Add(game_manager);
                        }

                        game_manager = ProcessReader.readUInt64((long)game_manager + 0x60); // Go to the next object
                    }
                    catch { }
                }

                // Remove entities not present in memory anymore
                // Update entities still present
                // Add new entities and update them
                List<UInt64> entitiesToRemove = new List<UInt64>();

                foreach (var entity in entities.Values)
                {
                    if (found.Contains(entity.PtrEntity))
                    {
                        entity.update();
                    }
                    else
                    {
                        entitiesToRemove.Add(entity.PtrEntity);
                    }
                }

                foreach (var entityPtr in entitiesToRemove)
                {
                    entities.Remove(entityPtr);
                    found.Remove(entityPtr);
                }

                foreach (var newEntityPtr in found.Except(entities.Keys))
                {
                    entities[newEntityPtr] = new Actors(newEntityPtr);
                }

                //System.Threading.Thread.Sleep(100);
            }
            else if (MainWindow.client_selected == "x86")
            {
                // Get the current objects
                try
                {
                    game_manager_x86 = ProcessReader.readUInt(ProcessReader.readUInt(ProcessReader.base_adress_x86 + Stats.game_manager_x86) + 0x5d78);
                }
                catch { }

                while (game_manager_x86 != 0) // If object isn't null, add it
                {
                    try
                    {
                        entities[game_manager_x86] = new Actors(game_manager_x86); // Add the object to the entities dictionary
                        game_manager_x86 = ProcessReader.readUInt((long)game_manager_x86 + 0x48); // Go to the next object
                    }
                    catch { }
                }
            }
        }

        // Actors class
        public Actors this[UInt64 entityPtr]
        {
            get
            {
                return entities[entityPtr];
            }
        } // Entity class

        #region IEnumerable<Actors> Members

        public IEnumerator<Actors> GetEnumerator()
        {
            return entities.Values.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
