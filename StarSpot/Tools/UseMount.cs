using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace StarSpot
{
    class UseMount
    {
        // Timer
        DispatcherTimer timer = new DispatcherTimer();

        // Stats class
        Stats stats = new Stats();

        // Use mount bool
        public static bool use_mount = false;

        public UseMount()
        {
            // Setup timer
            timer.Interval = new TimeSpan(0, 0, 10 + randomnr.create(0, 3));
            timer.Tick += new EventHandler(timer_Tick);
        }

        // Random
        RandomNR randomnr = new RandomNR();

        // Start
        public void start()
        {
            if (Properties.Settings.Default.use_grinding_mount)
            {
                // Check if player has aggro or not and if it's walking
                if (MainWindow.bot_running && ((Grinding.player_walking && Properties.Settings.Default.mods == "Grinding") | (Gathering.player_walking && Properties.Settings.Default.mods == "Gathering")) && stats.player_aggro() == 0)
                {
                    // Start the timer
                    timer.Start();
                }
                else
                {
                    // Stop the timer if player do something else
                    timer.Stop();

                    // Disable use mount
                    use_mount = false;

                    // Reset timer
                    if (timer.Interval.Seconds != 10 + randomnr.create(0, 3))
                    {
                        timer.Interval = new TimeSpan(0, 0, 10 + randomnr.create(0,3));
                    }
                }
            }
            else
            {
                // Disable use mount
                use_mount = false;
            }
        }

        // Timer tick
        public void timer_Tick(object sender, EventArgs e)
        {
            // Enable mounting
            use_mount = true;
        }
    }
}
