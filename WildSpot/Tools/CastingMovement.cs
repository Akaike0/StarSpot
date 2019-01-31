using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.ComponentModel;

namespace StarSpot
{
    class CastingMovement
    {
        // Random nr for random movement
        public Random casting_nr = new Random();
        public int random_nr;
        public bool cast_avoiding_used = false; // To make sure the bot click only once a key

        // Stats class
        Stats stats = new Stats();

        // BGW Thread
        private BackgroundWorker bgw = new BackgroundWorker();

        // RandomNR
        RandomNR randomnr = new RandomNR();

        // Main 
        public CastingMovement()
        {
            // Create backgroundworker handle
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
        }

        // Movement method
        public void move()
        {
            if(!bgw.IsBusy)
            {
                bgw.RunWorkerAsync();
                System.Threading.Thread.Sleep(100);
            }
        }

        // BGW
        public void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            if (TargetInfo.target_id != 0)
            {
                if (TargetInfo.target_iscasting != 0 && stats.player_aggro() == 1 && !cast_avoiding_used && ((Properties.Settings.Default.mods == "Grinding" | Properties.Settings.Default.mods == "Gathering") | (stats.player_iscasting() == 0 && Properties.Settings.Default.mods != "Grinding" && Properties.Settings.Default.mods != "Gathering")))
                {
                    // Create a random NR
                    random_nr = randomnr.create(-5, 10);

                    if (random_nr < 0) // Move to the right
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            System.Threading.Thread.Sleep(randomnr.create(330, 1150));

                            // Disable that!
                            cast_avoiding_used = true;

                            Keysimulation.SimulateKeys.E();
                            System.Threading.Thread.Sleep(210);
                            Keysimulation.SimulateKeys.E();

                            if (i == 1)
                            {
                                break;
                            }
                        }

                        // Disable that!
                        cast_avoiding_used = true;
                    }

                    if (random_nr >= 0 && random_nr <= 5)
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            System.Threading.Thread.Sleep(randomnr.create(500, 1320));

                            // Disable that!
                            cast_avoiding_used = true;

                            Keysimulation.SimulateKeys.Q();
                            System.Threading.Thread.Sleep(230);
                            Keysimulation.SimulateKeys.Q();

                            if (i == 1)
                            {
                                break;
                            }
                        }


                        // Disable that!
                        cast_avoiding_used = true;
                    }

                    if (random_nr > 5 && random_nr <= 10)
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            System.Threading.Thread.Sleep(randomnr.create(290, 1100));

                            // Disable that!
                            cast_avoiding_used = true;

                            Keysimulation.SimulateKeys.S();
                            System.Threading.Thread.Sleep(200);
                            Keysimulation.SimulateKeys.S();

                            if (i == 1)
                            {
                                break;
                            }
                        }

                        // Disable that!
                        cast_avoiding_used = true;
                    }

                    // Sleep to reduce cpu
                    System.Threading.Thread.Sleep(103);
                }

                if (TargetInfo.target_iscasting == 0)
                {
                    // Activate that function again if the mob stopped casting
                    cast_avoiding_used = false;
                }
            }
        }
    }
}
