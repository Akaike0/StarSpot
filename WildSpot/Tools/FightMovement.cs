using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace StarSpot
{
    class FightMovement
    {
        // BGW Thread
        private BackgroundWorker bgw = new BackgroundWorker();

        // Random
        RandomNR randomnr = new RandomNR();

        // Stats class
        Stats stats = new Stats();

        // Main
        public FightMovement()
        {
            // Create backgroundworker handle
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
        }

        // Move
        public void move()
        {
            if (!bgw.IsBusy)
            {
                bgw.RunWorkerAsync();
                System.Threading.Thread.Sleep(100);
            }
        }

        // BGW
        public void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            if (stats.player_targetid() != 0)
            {
                if (stats.player_iscasting() == 0) // Only if the player is not casting
                {
                    // Create random NR
                    int nr = randomnr.create(0, 3);

                    if (nr == 0) // Move right
                    {
                        Keysimulation.SimulateKeys.Q(true);
                        System.Threading.Thread.Sleep(420 + randomnr.create(0, 420 + randomnr.create(10, 154)));
                        Keysimulation.SimulateKeys.Q(false);
                    }

                    if (nr == 1) // Move right
                    {
                        Keysimulation.SimulateKeys.E(true);
                        System.Threading.Thread.Sleep(364 + randomnr.create(0, 420 + randomnr.create(30, 144)));
                        Keysimulation.SimulateKeys.E(false);
                    }

                    if (nr == 3) // Move right
                    {
                        Keysimulation.SimulateKeys.Space();
                        System.Threading.Thread.Sleep(230 + randomnr.create(0, 420 + randomnr.create(30, 164)));
                        Keysimulation.SimulateKeys.Space();
                    }
                }

                // To reduce cpu
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}
