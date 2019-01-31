using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace StarSpot
{
    class CTM_System
    {
        // Character stats class
        Stats stats = new Stats();

        // Waypoint Storage
        public static int waypoint_count = 0;
        public static List<cwaypoint> waypoints_list_temp = new List<cwaypoint>();
        public static List<cwaypoint> waypoints_list_2_temp = new List<cwaypoint>();
        public static List<cwaypoint> waypoints_list_3_temp = new List<cwaypoint>();
        public static List<cwaypoint> waypoints_list = new List<cwaypoint>();
        public static cwaypoint ctm_points = new cwaypoint();
        public static bool walking_loop = false;

        public static int waypoint_death_count = 0;
        public static List<cwaypoint> waypoints_death_list_temp = new List<cwaypoint>();
        public static List<cwaypoint> waypoints_death_list = new List<cwaypoint>();
        public static List<cwaypoint> waypoints_death_list_second_temp = new List<cwaypoint>();
        public static List<cwaypoint> waypoints_death_list_second = new List<cwaypoint>();
        public static cwaypoint ctm_death_points = new cwaypoint();
        public bool walking_dead_loop = false;
        public static bool walking_first_death_list = true;
        public static bool walking_second_death_list = true;

        public static int waypoint_selling_count = 0;
        public static List<cwaypoint> waypoints_selling_list_temp = new List<cwaypoint>();
        public static List<cwaypoint> waypoints_selling_list = new List<cwaypoint>();
        public static cwaypoint ctm_selling_points = new cwaypoint();
        public bool walking_selling_loop = false;

        // Unstucking System
        public static DispatcherTimer unstucking_check_Timer = new DispatcherTimer();
        public static DispatcherTimer unstucking_system_Timer = new DispatcherTimer();
        public static bool unstucking_active = false;

        // Selling returned bool
        public static bool selling_returned = false;

        // Closest spot class
        FindWaypoint findwaypoint = new FindWaypoint();

        // RandomNR
        RandomNR randomnr = new RandomNR();

        public CTM_System()
        {
            // Unstucking check timer
            unstucking_check_Timer.Interval = new TimeSpan(0, 0, 1);
            unstucking_check_Timer.Tick += new EventHandler(unstucking_check_timer_Tick);
            unstucking_check_Timer.Start();


            // Unstucking system timer settings
            if (unstucking_system_Timer.Interval.TotalSeconds != Properties.Settings.Default.unstucking_run_time)
            {
                unstucking_system_Timer.Interval = new TimeSpan(0, 0, Properties.Settings.Default.unstucking_run_time);
            }

            unstucking_system_Timer.Tick += new EventHandler(unstucking_system_Tick);
        }

        public class cwaypoint
        {
            public float X;
            public float Y;
            public float Z;
            public void Clear()
            {
                X = 0;
                Y = 0;
            }
            public void Set(float x, float y, float z)
            {
                X = x;
                Y = y;
            }
        }

        // Normal spots walking function
        public void walking()
        {
            // Set timer
            unstucking_time_set(0);

            stats.player_ctm_distance(1); // Set Distance

            // Mount
            // Call the mount
            if (Properties.Settings.Default.use_grinding_mount && UseMount.use_mount)
            {
                if (stats.player_mountID() == 0)
                {
                    // Stop the character
                    stats.player_ctm_push(4294967295);

                    // Summon the mount
                    Keysimulation.SimulateKeys.Y();
                    System.Threading.Thread.Sleep(2500);
                }
            }
                                   // Check mount
            if (!unstucking_active && ((UseMount.use_mount && stats.player_mountID() != 0) | !UseMount.use_mount))
            {
                if (!walking_loop)
                {
                    try
                    {
                        // Check if autoselling is on
                        if(Properties.Settings.Default.mods == "Grinding" | Properties.Settings.Default.mods == "Gathering")
                        {
                            if(Grinding.player_autoselling | Gathering.player_autoselling)
                            {
                                walking_loop = true;
                            }
                        }

                        // Set the points into the ctm system
                        stats.player_ctm_x(ctm_points.X + randomnr.create(-0.0, 0.42));
                        stats.player_ctm_y(ctm_points.Y + randomnr.create(-0.0, 0.31));
                        stats.player_ctm_z(ctm_points.Z + randomnr.create(-0.0, 0.32));

                        stats.player_ctm_push(0); // Push CTM
                        unstucking_system_Timer.Start(); // Active unstucking system

                        // Switch the spot
                        if (DistanceToPoint2D() <= 3.0f)
                        {
                            // Log
                            MainWindow.log_text = "Walking to spot: " + waypoint_count.ToString() + "\n" + "X: " + ctm_points.X + " Y: " + ctm_points.Y;

                            // Set the next ctm point
                            waypoint_count++;
                            ctm_points = waypoints_list[waypoint_count];

                            // Set the points into the ctm system
                            stats.player_ctm_x(ctm_points.X + randomnr.create(-0.0, 0.12));
                            stats.player_ctm_y(ctm_points.Y + randomnr.create(-0.0, 0.31));
                            stats.player_ctm_z(ctm_points.Z + randomnr.create(-0.0, 0.33));

                            stats.player_ctm_push(0); // Push CTM

                            // Reset the unstucking counter
                            ExitWildstar.unstucking_counter = 0;
                            unstucking_time_set(1); // Reset the timer
                            unstucking_system_Timer.Stop(); // Stop unstucking system
                        }
                    }
                    catch
                    {
                        if (!Properties.Settings.Default.walk_in_circle)
                        {
                            // Enable the loop
                            walking_loop = true;

                            // Set the next ctm point
                            waypoint_count--;
                            ctm_points = waypoints_list[waypoint_count];

                            // Log
                            MainWindow.log_text = "Reached the last spot. Looping on.";
                        }
                        else
                        {
                            // Set the next ctm point
                            waypoint_count = 0;
                            ctm_points = waypoints_list[waypoint_count];

                            // Log
                            MainWindow.log_text = "Reached the last spot. Looping on.";
                        }
                    }
                }
                else
                {
                    try
                    {
                        // Set the points into the ctm system
                        stats.player_ctm_x(ctm_points.X + randomnr.create(-0.0, 0.32));
                        stats.player_ctm_y(ctm_points.Y + randomnr.create(-0.0, 0.33));
                        stats.player_ctm_z(ctm_points.Z + randomnr.create(-0.0, 0.32));

                        unstucking_system_Timer.Start(); // Active unstucking system

                        stats.player_ctm_push(0); // Push CTM

                        if (DistanceToPoint2D() <= 3.0f)
                        {
                            // Log
                            MainWindow.log_text = "Walking to spot: " + waypoint_count.ToString() + "\n" + "X: " + ctm_points.X + " Y: " + ctm_points.Y;

                            // Set the next ctm point
                            waypoint_count--;
                            ctm_points = waypoints_list[waypoint_count];

                            // Set the points into the ctm system
                            stats.player_ctm_x(ctm_points.X + randomnr.create(-0.0, 0.41));
                            stats.player_ctm_y(ctm_points.Y + randomnr.create(-0.0, 0.22));
                            stats.player_ctm_z(ctm_points.Z + randomnr.create(-0.0, 0.32));

                            stats.player_ctm_push(0); // Push CTM

                            // Reset the unstucking counter
                            ExitWildstar.unstucking_counter = 0;
                            unstucking_time_set(1); // R4eset the timer
                            unstucking_system_Timer.Stop(); // Stop unstucking system
                        }
                    }
                    catch
                    {
                        // Disable the loop
                        walking_loop = false;

                        // Log
                        MainWindow.log_text = "Reached the last spot. Looping off.";
                    }
                }
            } // Unstucking active
            else
            {
                unstucking_walking();
            }
        }

        // Death spots walking function
        public void walking_dead()
        {
            // Set timer
            unstucking_time_set(0);

            stats.player_ctm_distance(1); // Set Distance

            // Check which loop it should use
            if (walking_first_death_list && walking_second_death_list && waypoints_death_list_second.Count != 0 && waypoints_death_list.Count != 0)
            {
                if (Properties.Settings.Default.mods == "PVP")
                {
                    System.Threading.Thread.Sleep(12000);
                }
                else
                {
                    System.Threading.Thread.Sleep(6000);
                }

                // Reset wp
                CTM_System.waypoint_count = 0;

                // Calculate the distances
                double first_list_distance = Distance2D(stats.player_position_x(), stats.player_position_y(), waypoints_death_list[0].X, waypoints_death_list[0].Y);
                double second_list_distance = Distance2D(stats.player_position_x(), stats.player_position_y(), waypoints_death_list_second[0].X, waypoints_death_list_second[0].Y);

                // Decide
                if (first_list_distance <= second_list_distance)
                {
                    // Add log
                    MainWindow.log_text = "Chosen the first death spots list.";

                    // Add the first spot into the ctm function
                    ctm_death_points = waypoints_death_list[waypoint_death_count];

                    // Chose
                    walking_first_death_list = true;
                    walking_second_death_list = false;
                }
                else
                {
                    // Add log
                    MainWindow.log_text = "Chosen the second death spots list.";

                    // Add the first spot into the ctm function
                    ctm_death_points = waypoints_death_list_second[waypoint_death_count];

                    // Chose
                    walking_first_death_list = false;
                    walking_second_death_list = true;
                }
            }
            else if (walking_first_death_list && walking_second_death_list && waypoints_death_list_second.Count != 0 && waypoints_death_list.Count == 0)
            {
                // Add log
                MainWindow.log_text = "Chosen the second death spots list.";

                // Add the first spot into the ctm function
                ctm_death_points = waypoints_death_list_second[waypoint_death_count];

                // Chose
                walking_first_death_list = false;
                walking_second_death_list = true;
            }
            else if (walking_first_death_list && walking_second_death_list && waypoints_death_list_second.Count == 0 && waypoints_death_list.Count != 0)
            {
                // Add log
                MainWindow.log_text = "Chosen the first death spots list.";

                // Add the first spot into the ctm function
                ctm_death_points = waypoints_death_list[waypoint_death_count];

                // Chose
                walking_first_death_list = true;
                walking_second_death_list = false;
            }

            // Mount
            // Call the mount
            if (Properties.Settings.Default.use_mount_death_selling)
            {
                if (stats.player_mountID() == 0)
                {
                    // Stop the character
                    stats.player_ctm_push(4294967295);

                    // Summon the mount
                    Keysimulation.SimulateKeys.Y();
                    System.Threading.Thread.Sleep(2500);
                }
            }

            if (!walking_dead_loop && (!Properties.Settings.Default.use_mount_death_selling | stats.player_mountID() != 0))
            {
                try
                {
                    if (walking_first_death_list && !walking_second_death_list) // First list
                    {

                        // Set the points into the ctm system
                        stats.player_ctm_x(ctm_death_points.X);
                        stats.player_ctm_y(ctm_death_points.Y);
                        stats.player_ctm_z(randomnr.create(-100, 1000));

                        unstucking_system_Timer.Start(); // Unstucking system start

                        if (stats.player_ctm_push() != 0) // Push ctm status
                        {
                            stats.player_ctm_push(0);
                        }

                        // Switch the spot
                        if (DistanceToPoint2D() <= 2.5f + randomnr.create(0, 1))
                        {
                            // Log
                            MainWindow.log_text = "Walking to death spot: " + waypoint_death_count.ToString() + "\n" + "X: " + ctm_death_points.X + " Y: " + ctm_death_points.Y;

                            // Set the next ctm point
                            waypoint_death_count++;
                            ctm_death_points = waypoints_death_list[waypoint_death_count];

                            // Set the points into the ctm system
                            stats.player_ctm_x(ctm_death_points.X);
                            stats.player_ctm_y(ctm_death_points.Y);
                            stats.player_ctm_z(randomnr.create(-100, 1000));

                            // Reset the unstucking counter
                            ExitWildstar.unstucking_counter = 0;
                            unstucking_time_set(1); // R4eset the timer
                            unstucking_system_Timer.Stop(); // Reset the unstucking system, stop it
                        }
                    }
                    else if (!walking_first_death_list && walking_second_death_list) // Second  list
                    {

                        // Set the points into the ctm system
                        stats.player_ctm_x(ctm_death_points.X);
                        stats.player_ctm_y(ctm_death_points.Y);
                        stats.player_ctm_z(randomnr.create(-100, 1000));

                        unstucking_system_Timer.Start(); // Unstucking system start

                        if (stats.player_ctm_push() != 0) // Push ctm status
                        {
                            stats.player_ctm_push(0);
                        }

                        // Switch the spot
                        if (DistanceToPoint2D() <= 2.0f + randomnr.create(0, 1))
                        {
                            // Log
                            MainWindow.log_text = "Walking to death spot: " + waypoint_death_count.ToString() + "\n" + "X: " + ctm_death_points.X + " Y: " + ctm_death_points.Y;

                            // Set the next ctm point
                            waypoint_death_count++;
                            ctm_death_points = waypoints_death_list_second[waypoint_death_count];

                            // Set the points into the ctm system
                            stats.player_ctm_x(ctm_death_points.X);
                            stats.player_ctm_y(ctm_death_points.Y);
                            stats.player_ctm_z(randomnr.create(-100, 1000));

                            // Reset the unstucking counter
                            ExitWildstar.unstucking_counter = 0;
                            unstucking_time_set(1); // R4eset the timer
                            unstucking_system_Timer.Stop(); // Reset the unstucking system, stop it
                        }
                    }
                }
                catch
                {
                    // Chose normal spots
                    chose_random_spots();

                    if(Properties.Settings.Default.got_to_vendor_after_death)
                    {
                        if(Properties.Settings.Default.mods == "Grinding")
                        {
                            Grinding.player_autoselling = true;
                        }
                        else if (Properties.Settings.Default.mods == "Gathering")
                        {
                            Gathering.player_autoselling = true;
                        }
                    }

                    // find the closes spot
                    findwaypoint.closes_waypoint();

                    if (findwaypoint.waypoint_found)
                    {
                        // Set this waypoint to the current list
                        CTM_System.ctm_points = CTM_System.waypoints_list[findwaypoint.best_waypoint_index];
                        CTM_System.waypoint_count = findwaypoint.best_waypoint_index;

                        // Set this CTM
                        stats.player_ctm_x(CTM_System.ctm_points.X);
                        stats.player_ctm_y(CTM_System.ctm_points.Y);

                        // Reset waypoint found
                        findwaypoint.waypoint_found = false;

                        //  Reset all the stuff
                        walking_dead_loop = true; // Set the loop to true to stop walking in death mode
                        waypoint_death_count = 0; // Reset death count
                        Grinding.player_ressurection = false; // Reset grinding - > set to the normal walking
                        Gathering.player_ressurection = false;
                        PVP.player_ressurection = false; // Reset PVP
                    }
                }
            }
        }

        // Chose normal spots after death
        public void chose_random_spots()
        {
            if(Properties.Settings.Default.mods != "PVP")
            {
                // Count available lists
                if (waypoints_list_2_temp.Count == 0 && waypoints_list_3_temp.Count == 0)
                {
                    waypoints_list = waypoints_list_temp;
                }
                else if (waypoints_list_2_temp.Count != 0 && waypoints_list_3_temp.Count != 0)
                {
                    Random random = new Random();
                    int random_nr = random.Next(1, 3);

                    if (random_nr == 1)
                    {
                        waypoints_list = waypoints_list_temp;
                    }
                    else if (random_nr == 2)
                    {
                        waypoints_list = waypoints_list_2_temp;
                    }
                    else if (random_nr == 3)
                    {
                        waypoints_list = waypoints_list_3_temp;
                    }
                }
                else if (waypoints_list_2_temp.Count != 0 && waypoints_list_3_temp.Count == 0)
                {
                    Random random = new Random();
                    int random_nr = random.Next(1, 2);

                    if (random_nr == 1)
                    {
                        waypoints_list = waypoints_list_temp;
                    }
                    else if (random_nr == 2)
                    {
                        waypoints_list = waypoints_list_2_temp;
                    }
                }
                else if (waypoints_list_2_temp.Count == 0 && waypoints_list_3_temp.Count != 0)
                {
                    Random random = new Random();
                    int random_nr = random.Next(1, 2);

                    if (random_nr == 1)
                    {
                        waypoints_list = waypoints_list_temp;
                    }
                    else if (random_nr == 2)
                    {
                        waypoints_list = waypoints_list_3_temp;
                    }
                }
            } 
        }

        // Selling spots walking function
        public void walking_selling()
        {
            // Set timer
            unstucking_time_set(0);

            stats.player_ctm_distance(1); // Set Distance

            // Call the mount
            if (Properties.Settings.Default.use_mount_death_selling)
            {
                if(stats.player_mountID() == 0)
                {
                    // Stop the character
                    stats.player_ctm_push(4294967295);

                    // Summon the mount
                    Keysimulation.SimulateKeys.Y();
                    System.Threading.Thread.Sleep(2500);
                }
            }

            if (!Properties.Settings.Default.use_mount_death_selling | stats.player_mountID() != 0)
            {
                if (!walking_selling_loop)
                {
                    try
                    {
                        // Set the points into the ctm system
                        stats.player_ctm_x(ctm_selling_points.X);
                        stats.player_ctm_y(ctm_selling_points.Y);
                        stats.player_ctm_z(randomnr.create(-100, 1000));

                        unstucking_system_Timer.Start(); // Unstucking system start

                        if (stats.player_ctm_push() != 0) // Push ctm status
                        {
                            stats.player_ctm_push(0);
                        }

                        // Switch the spot
                        if (DistanceToPoint2D() <= 2.0f + randomnr.create(0, 1))
                        {
                            // Log
                            MainWindow.log_text = "Walking to selling spot: " + waypoint_selling_count.ToString() + "\n" + "X: " + ctm_selling_points.X + " Y: " + ctm_selling_points.Y;

                            // Set the next ctm point
                            waypoint_selling_count++;
                            ctm_selling_points = waypoints_selling_list[waypoint_selling_count];

                            // Set the points into the ctm system
                            stats.player_ctm_x(ctm_selling_points.X);
                            stats.player_ctm_y(ctm_selling_points.Y);

                            // Reset the unstucking counter
                            ExitWildstar.unstucking_counter = 0;
                            unstucking_time_set(1); // R4eset the timer
                            unstucking_system_Timer.Stop(); // Reset the unstucking system, stop it
                        }
                    }
                    catch
                    {
                        // Sell stuff
                        System.Threading.Thread.Sleep(1000); // Wait one second

                        for (int b = 0; b < 10; b++)
                        {
                            Keysimulation.SimulateKeys.F(); // Click F to open the sell window
                            System.Threading.Thread.Sleep(100); // Wait one second
                        }

                        Random i = new Random(); // To avoid a pattern
                        int random_nr;
                        random_nr = i.Next(500, 1000);

                        System.Threading.Thread.Sleep(4000 + random_nr); // Wait one second + random

                        // Sell stuff
                        for (int c = 0; c < 5; c++)
                        {
                            Keysimulation.SimulateKeys.F(); // Click F to open the sell window
                            System.Threading.Thread.Sleep(100); // Wait one second
                        }

                        walking_selling_loop = true; // Set the loop to true to stop walking in death mode
                    }
                }
                else
                {
                    try
                    {

                        // Set the points into the ctm system
                        stats.player_ctm_x(ctm_selling_points.X);
                        stats.player_ctm_y(ctm_selling_points.Y);
                        stats.player_ctm_z(randomnr.create(-100, 1000));

                        unstucking_system_Timer.Start(); // Unstucking system start

                        if (stats.player_ctm_push() != 0) // Push ctm status
                        {
                            stats.player_ctm_push(0);
                        }

                        // Switch the spot
                        if (DistanceToPoint2D() <= 2.5f)
                        {
                            // Log
                            MainWindow.log_text = "Walking to selling spot: " + waypoint_selling_count.ToString() + "\n" + "X: " + ctm_selling_points.X + " Y: " + ctm_selling_points.Y;

                            // Set the next ctm point
                            waypoint_selling_count--;
                            ctm_selling_points = waypoints_selling_list[waypoint_selling_count];

                            // Set the points into the ctm system
                            stats.player_ctm_x(ctm_selling_points.X);
                            stats.player_ctm_y(ctm_selling_points.Y);

                            // Reset the unstucking counter
                            ExitWildstar.unstucking_counter = 0;
                            unstucking_time_set(1); // R4eset the timer
                            unstucking_system_Timer.Stop(); // Reset the unstucking system, stop it
                        }
                    }
                    catch
                    {
                        walking_selling_loop = false; // Set the loop to true to stop walking in death mode
                        selling_returned = true; // Enable this bool to give the signal that it's back
                    }
                }
            }
        }

        // Reset normal spots function
        public void waypoints_reset()
        {
            try
            {
                if (waypoint_count > 0)
                {
                    ctm_points = waypoints_list[waypoint_count - 1];
                    stats.player_ctm_x(ctm_points.X);
                    stats.player_ctm_y(ctm_points.Y);
                }
                else
                {
                    ctm_points = waypoints_list[0];
                    stats.player_ctm_x(ctm_points.X);
                    stats.player_ctm_y(ctm_points.Y);
                }
            }
            catch { }
        }

        #region Unstucking
        // Set timer if changed
        public void unstucking_time_set(int function)
        {
            if (function == 0)
            {
                // Unstucking system timer settings
                if (unstucking_system_Timer.Interval.TotalSeconds != Properties.Settings.Default.unstucking_run_time)
                {
                    unstucking_system_Timer.Interval = new TimeSpan(0, 0, Properties.Settings.Default.unstucking_run_time);
                }
            }
            else
            {
                // Unstucking system timer settings
                if (unstucking_system_Timer.Interval.Seconds != Properties.Settings.Default.unstucking_run_time)
                {
                    unstucking_system_Timer.Interval = new TimeSpan(0, 0, Properties.Settings.Default.unstucking_run_time);
                }
            }
        }

        // If timer activate this function it walks into a random direction and jumps
        public void unstucking_walking()
        {
            //
            Random randomNr = new Random();
            int number = randomNr.Next(0, 50);

            // Add a log
            MainWindow.log_text = "Stuck! Try to unstuck.";

            // Unstuck
            if (unstucking_active)
            {
                // Check exitwildstar
                ExitWildstar.unstucking_check();

                for (int i = 0; i < 50; i++)
                {
                    Keysimulation.SimulateKeys.Space();

                    if (number >= 0 && number <= 25)
                    {
                        Keysimulation.SimulateKeys.Q();
                    }

                    if (number >= 25 && number <= 35)
                    {
                        Keysimulation.SimulateKeys.E();
                    }

                    if (i > 45)
                    {
                        if (Properties.Settings.Default.mods == "PVP")
                        {
                            // find the closes spot
                            findwaypoint.closes_waypoint();

                            // Set this waypoint to the current list
                            CTM_System.ctm_points = CTM_System.waypoints_list[findwaypoint.best_waypoint_index];
                            CTM_System.waypoint_count = findwaypoint.best_waypoint_index;

                            // Set this CTM
                            stats.player_ctm_x(CTM_System.ctm_points.X);
                            stats.player_ctm_y(CTM_System.ctm_points.Y);

                            // Reset waypoint found
                            findwaypoint.waypoint_found = false;
                        }

                        unstucking_active = false;

                        break;
                    }
                }
            }
        }

        // Unstucking check
        public void unstucking_check_timer_Tick(object sender, EventArgs e)
        {
            if(Properties.Settings.Default.mods == "Grinding")
            {
                if(!Grinding.player_walking)
                {
                    unstucking_system_Timer.Stop();
                }
            }
            else if (Properties.Settings.Default.mods == "Gathering")
            {
                if (!Gathering.player_walking)
                {
                    unstucking_system_Timer.Stop();
                }
            }
            else if (Properties.Settings.Default.mods == "PVP")
            {
                if (!PVP.player_walking)
                {
                    unstucking_system_Timer.Stop();
                }
            }
        }

        // Unstucking system
        private void unstucking_system_Tick(object sender, EventArgs e)
        {
            if (stats.player_aggro() == 0)
            {
                unstucking_active = true;

                unstucking_system_Timer.Stop();
            }
        }
        #endregion Unstucking

        #region Calculations
        // Distance calculation 3D
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

        // Distance calculation 2D
        public double Distance2D(float x1, float y1, float x2, float y2)
        {
            double result = 0;

            double part1 = System.Math.Pow((x2 - x1), 2);

            double part2 = System.Math.Pow((y2 - y1), 2);

            double underRadical = part1 + part2;

            result = System.Math.Sqrt(underRadical);

            return result;
        }

        // Calculate the distance to a spot (CTM)
        public double DistanceToPoint2D()
        {
            try
            {
                double distance = Math.Sqrt(Math.Pow(Math.Abs(stats.player_position_x() - stats.player_ctm_x()), 2) + Math.Pow(Math.Abs(stats.player_position_y() - stats.player_ctm_y()), 2));
                return distance;
            }
            catch
            {
                return 0;
            }
        }

        #endregion Calculations
    }
}
