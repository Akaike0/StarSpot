using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace StarSpot
{
    class RandomJumpingSystem
    {
        // Timer
        DispatcherTimer jump_timer = new DispatcherTimer();

        // Stats Class
        Stats stats = new Stats();

        public RandomJumpingSystem()
        {
            // Timer settings
            jump_timer.Interval = new TimeSpan(0, 0, Properties.Settings.Default.random_jumping_timer);
            jump_timer.Tick += new EventHandler(jump_timer_Tick);
        }

        public void random_jump()
        {
            if (stats.player_aggro() == 0)
            {
                jump_timer.Start(); // Jump tiiiiimmmee!!
            }
            else
            {
                jump_timer.Stop();
            }
        }

        private void jump_timer_Tick(object sender, EventArgs e)
        {
            if (!AutoLogin.loggedout)
            {
                // Jump
                for (int i = 0; i < 26; i++)
                {
                    if (stats.player_iscasting() == 0)
                    {
                        Keysimulation.SimulateKeys.Space();
                    }
                }

                // Create a random number for interval
                Random randomNr = new Random();
                int number = randomNr.Next(0, 8);

                jump_timer.Interval = new TimeSpan(0, 0, Properties.Settings.Default.random_jumping_timer + number);

                jump_timer.Stop(); // Stop itself
            }
        }
    }
}
