using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSpot
{
    class Looting
    {
        // Stats class
        Stats stats = new Stats();

        BackgroundWorker bgw = new BackgroundWorker();

        RandomNR randomnr = new RandomNR();

        public Looting()
        {
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
        }

        public void loot()
        {
            // Run bgw
            if(!bgw.IsBusy)
            {
                bgw.RunWorkerAsync();
            }
        }

        // BGW
        public void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Properties.Settings.Default.looting_option)
            {
                if (Properties.Settings.Default.mods == "Grinding")
                {
                    if (stats.player_looting() == 1 && stats.player_aggro() == 0 && (Grinding.player_walking | Grinding.player_looting))
                    {
                        // Press key
                        Keysimulation.SimulateKeys.V();

                        System.Threading.Thread.Sleep(410 + randomnr.create(310, 1010));
                    }
                }

                if (Properties.Settings.Default.mods == "Gathering")
                {
                    if (stats.player_looting() == 1 && stats.player_aggro() == 0 && (Gathering.player_walking | Gathering.player_looting))
                    {
                        // Press key
                        Keysimulation.SimulateKeys.V();

                        System.Threading.Thread.Sleep(430 + randomnr.create(320, 1020));
                    }
                }

                if (Properties.Settings.Default.mods == "Combat")
                {
                    if (stats.player_looting() == 1 && stats.player_aggro() == 0)
                    {
                        // Press key
                        Keysimulation.SimulateKeys.V();

                        System.Threading.Thread.Sleep(440 + randomnr.create(298, 1000));
                    }
                }
            }
        }
    }
}
