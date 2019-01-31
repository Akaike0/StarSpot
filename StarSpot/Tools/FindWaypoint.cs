using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSpot
{
    class FindWaypoint
    {
        // Find the closes waypoint
        public double dist = double.PositiveInfinity;
        public double temp_dist;
        public int best_waypoint_index;
        public bool waypoint_found = false;

        // Current wp count
        public int current_count = 0;

        // Stats class
        Stats stats = new Stats();

        //  Normal waypoint
        public void closes_waypoint()
        {
            // Reset the distance
            dist = double.PositiveInfinity;

            // Set the current count
            current_count = CTM_System.waypoint_count;

            if (Properties.Settings.Default.mods != "PVP")
            {
                if (CTM_System.waypoint_count != 0)
                {
                    if (!CTM_System.walking_loop)
                    {
                        for (int i = 0; i < current_count + 25; i++) // Search only on the same spots line
                        {
                            // The distance to the current loop wp
                            try
                            {
                                temp_dist = Distance3D(stats.player_position_x(), stats.player_position_y(), stats.player_position_z(), CTM_System.waypoints_list[i].X, CTM_System.waypoints_list[i].Y, CTM_System.waypoints_list[i].Z);

                                if (temp_dist < dist)
                                {
                                    dist = temp_dist;
                                    best_waypoint_index = i;
                                }

                                if (i > current_count + 25 - 2)
                                {
                                    waypoint_found = true;
                                }
                            }
                            catch { waypoint_found = true; break; }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < current_count; i++) // Search only on the same spots line
                        {
                            // The distance to the current loop wp
                            try
                            {
                                temp_dist = Distance3D(stats.player_position_x(), stats.player_position_y(), stats.player_position_z(), CTM_System.waypoints_list[current_count - i].X, CTM_System.waypoints_list[current_count - i].Y, CTM_System.waypoints_list[current_count - i].Z);

                                if (temp_dist < dist)
                                {
                                    dist = temp_dist;
                                    best_waypoint_index = current_count - i;
                                }

                                if (i > current_count - 2)
                                {
                                    waypoint_found = true;
                                }
                            }
                            catch { waypoint_found = true; break; }
                        }
                    }
                }
                else
                {
                    choose_wplist();
                }
            }
            else
            {
                if (CTM_System.waypoint_count != 0)
                {
                    for (int i = 0; i < CTM_System.waypoints_list.Count; i++) // Random for PVP
                    {
                        // The distance to the current loop wp
                        try
                        {
                            temp_dist = Distance3D(stats.player_position_x(), stats.player_position_y(), stats.player_position_z(), CTM_System.waypoints_list[i].X, CTM_System.waypoints_list[i].Y, CTM_System.waypoints_list[i].Z);

                            if (temp_dist < dist)
                            {
                                dist = temp_dist;
                                best_waypoint_index = i;
                            }

                            if (i > CTM_System.waypoints_list.Count - 2)
                            {
                                waypoint_found = true;
                            }
                        }
                        catch { waypoint_found = true; break; }
                    }
                }
                else
                {
                    choose_wplist();
                }
            }
        }

        private void choose_wplist()
        {
            int wp_nr = 0;

            if (CTM_System.waypoints_list_2_temp.Count != 0)
            {
                for (int i = 0; i < CTM_System.waypoints_list_2_temp.Count; i++) // Random for PVP
                {
                    // The distance to the current loop wp
                    try
                    {
                        temp_dist = Distance3D(stats.player_position_x(), stats.player_position_y(), stats.player_position_z(), CTM_System.waypoints_list_2_temp[i].X, CTM_System.waypoints_list_2_temp[i].Y, CTM_System.waypoints_list_2_temp[i].Z);

                        if (temp_dist < dist)
                        {
                            dist = temp_dist;
                            wp_nr = 1;
                            best_waypoint_index = i;
                        }
                    }
                    catch { break; }
                }
            }

            if (CTM_System.waypoints_list_3_temp.Count != 0)
            {
                for (int i = 0; i < CTM_System.waypoints_list_3_temp.Count; i++) // Random for PVP
                {
                    // The distance to the current loop wp
                    try
                    {
                        temp_dist = Distance3D(stats.player_position_x(), stats.player_position_y(), stats.player_position_z(), CTM_System.waypoints_list_3_temp[i].X, CTM_System.waypoints_list_3_temp[i].Y, CTM_System.waypoints_list_3_temp[i].Z);

                        if (temp_dist < dist)
                        {
                            dist = temp_dist;
                            wp_nr = 2;
                            best_waypoint_index = i;
                        }
                    }
                    catch { break; }
                }
            }

            if (CTM_System.waypoints_list_temp.Count != 0)
            {

                for (int i = 0; i < CTM_System.waypoints_list_temp.Count; i++) // Random for PVP
                {
                    // The distance to the current loop wp
                    try
                    {
                        temp_dist = Distance3D(stats.player_position_x(), stats.player_position_y(), stats.player_position_z(), CTM_System.waypoints_list_temp[i].X, CTM_System.waypoints_list_temp[i].Y, CTM_System.waypoints_list_temp[i].Z);

                        if (temp_dist < dist)
                        {
                            dist = temp_dist;
                            wp_nr = 0;
                            best_waypoint_index = i;
                        }
                    }
                    catch { break; }
                }
            }

                    if(wp_nr == 0)
                    {
                        CTM_System.waypoints_list = CTM_System.waypoints_list_temp;
                    }
                    else if (wp_nr == 1)
                    {
                        CTM_System.waypoints_list = CTM_System.waypoints_list_2_temp;
                    }
                     else if (wp_nr == 2)
                    {
                        CTM_System.waypoints_list = CTM_System.waypoints_list_3_temp;
                    }

                    waypoint_found = true;
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
