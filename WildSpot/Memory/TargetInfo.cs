using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSpot
{
    class TargetInfo
    {
        // Backgroundworker for target
        BackgroundWorker target_bgw = new BackgroundWorker();

        // Entity list
        ActorsList elist = new ActorsList(); // Entity list class

        // Stats class
        Stats stats = new Stats();

        // Target stats
        public static UInt64 target_id;
        public static double target_distance;
        public static double target_distance_toplayer;
        public static string target_name;
        public static UInt64 target_level;
        public static UInt64 target_typ;
        public static UInt64 target_health;
        public static UInt64 target_health_inp;
        public static UInt64 target_shield;
        public static UInt64 target_shield_inp;
        public static UInt64 target_tid;
        public static UInt64 target_owner;
        public static UInt64 target_iscasting;
        public static UInt64 target_aggro;
        public static float target_position_x;
        public static float target_position_y;
        public static float target_position_z;

        public static UInt64 target_targets_id;
        public static string target_target_name;
        public static UInt64 target_target_level;
        public static UInt64 target_target_typ;
        public static UInt64 target_target_health;
        public static UInt64 target_target_shield;
        public static UInt64 target_target_tid;
        public static UInt64 target_target_aggro;
        public static float target_target_position_x;
        public static float target_target_position_y;

        public TargetInfo()
        {
            // Target bgw
            target_bgw.DoWork += new DoWorkEventHandler(target_bgw_DoWork);
        }

        // Find the target
        public void find_target()
        {
            if (!target_bgw.IsBusy)
            {
                target_bgw.RunWorkerAsync();
            }
        }

        // Find target background worker
        private void target_bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            // Search for the targets
            try
            {
                elist.update(); // Update the entity list

                foreach (Actors entity in elist)
                {
                    if (entity.id == stats.player_targetid()) // Find the target
                    {
                        // Enter target information
                        target_id = entity.id;
                        target_owner = entity.unitowner;
                        target_name = entity.name;
                        target_level = entity.level;
                        target_typ = entity.typ;
                        target_health = entity.health;
                        target_health_inp = entity.health_inp;
                        target_shield = entity.shield;
                        target_shield_inp = entity.shield_inp;
                        target_tid = entity.target_target_id;
                        target_iscasting = entity.iscasting;
                        target_aggro = entity.aggro;
                        target_position_x = entity.position_x;
                        target_position_y = entity.position_y;
                        target_position_z = entity.position_z;

                        // Target Distance
                        target_distance = Distance3D(CTM_System.ctm_points.X, CTM_System.ctm_points.Y, CTM_System.ctm_points.Z, entity.position_x, entity.position_y, entity.position_z); // CTM Distance
                        target_distance_toplayer = Distance3D(stats.player_position_x(), stats.player_position_y(), stats.player_position_z(), entity.position_x, entity.position_y, entity.position_z); // Distance to player
                    }

                    if (entity.id == target_tid) // Find the target
                    {
                        // Test
                        // MainWindow.test_string = target_tid.ToString() + " " + target_typ.ToString() + " " + target_id.ToString();

                        // Enter target information
                        target_targets_id = entity.id;
                        target_target_name = entity.name;
                        target_target_level = entity.level;
                        target_target_typ = entity.typ;
                        target_target_health = entity.health;
                        target_target_shield = entity.shield;
                        target_target_tid = entity.target_target_id;
                        target_target_aggro = entity.aggro;
                        target_target_position_x = entity.position_x;
                        target_target_position_y = entity.position_y;
                    }
                }

                // Sleep to reduce cpu
                System.Threading.Thread.Sleep(100);
            }
            catch { }
        }

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
