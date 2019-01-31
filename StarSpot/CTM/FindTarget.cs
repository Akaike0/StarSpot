using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSpot
{
    class FindTarget
    {
        // Entity class
        ActorsList elist = new ActorsList();

        // Stats class
        Stats stats = new Stats();

        // Target ID
        public static UInt64 target_id;

        // Distance to target
        public double distance_to_entity;

        public void next_target()
        {
            try
            {
                elist.update(); // Update entity list
            }
            catch { }

            if (target_id == 0)
            {
                try
                {
                    foreach (Actors entity in elist)
                    {
                        if (entity.health != 0 && entity.name != "")
                        {
                            distance_to_entity = Distance3D(CTM_System.ctm_points.X, CTM_System.ctm_points.Y, CTM_System.ctm_points.Z, entity.position_x, entity.position_y, entity.position_z);

                            if (Blacklist.blacklist_npc_list.Contains(entity.name) && Blacklist.blacklist_npc_list.Count() != 0 && Properties.Settings.Default.mods == "Gathering")
                            {
                                if (distance_to_entity <= Properties.Settings.Default.search_range)
                                {
                                    // Reduce the distance to the next position for ctm
                                    stats.player_ctm_distance(Properties.Settings.Default.attack_range);

                                    // Enter the ID
                                    if (target_id == 0 && entity.health != 0)
                                    {
                                        target_id = entity.id;
                                    }
                                }
                            }
                            else if (Properties.Settings.Default.mods != "Gathering")
                            {
                                if (Blacklist.blacklist_npc_list.Contains(entity.name) && Blacklist.blacklist_npc_list.Count() != 0 && Properties.Settings.Default.whitelist_enabled)
                                {
                                    if (distance_to_entity <= Properties.Settings.Default.search_range && TargetInfo.target_level <= Convert.ToUInt64(Properties.Settings.Default.level_range) && entity.health != 0)
                                    {
                                        // Reduce the distance to the next position for ctm
                                        stats.player_ctm_distance(Properties.Settings.Default.attack_range);

                                        // Enter the ID
                                        if (target_id == 0 && entity.health != 0)
                                        {
                                            target_id = entity.id;
                                        }
                                    }
                                }
                                else if (!Blacklist.blacklist_npc_list.Contains(entity.name) && entity.typ == 0 && entity.name != stats.player_name() && !Properties.Settings.Default.whitelist_enabled)
                                {
                                    if (distance_to_entity <= Properties.Settings.Default.search_range && TargetInfo.target_level <= Convert.ToUInt64(Properties.Settings.Default.level_range) && entity.health != 0)
                                    {
                                        // Reduce the distance to the next position for ctm
                                        stats.player_ctm_distance(Properties.Settings.Default.attack_range);

                                        // Enter the ID
                                        if (target_id == 0 && entity.health != 0)
                                        {
                                            target_id = entity.id;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch { }
            }
            else if (target_id != 0)
            {
                if (stats.player_aggro() == 0) // Only if player has no aggro
                {
                    if (stats.player_targetid() != target_id)
                    {
                        // Add target handle settings
                        stats.player_targetHandle(128);

                        // Add the new target
                        stats.player_targetid((uint)target_id);

                        target_id = 0;
                    }
                }
            }
        }

        // Distance calculation
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
    }
}
