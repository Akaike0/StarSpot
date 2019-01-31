using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.ComponentModel;

namespace StarSpot
{
    class Tabbing
    {
        // BGW
        private BackgroundWorker bgw = new BackgroundWorker();

        // Random Nr
        RandomNR randomnr = new RandomNR();


        public Tabbing()
        {
            // Create BGW
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
        }

        public void search()
        {
            if(!bgw.IsBusy)
            {
                bgw.RunWorkerAsync();
            }
        }

        // Backgroundworker
        public void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            // Tabbing!
            Keysimulation.SimulateKeys.Tab(); // Tab
            System.Threading.Thread.Sleep(792 + randomnr.create(13,112));
        }
    }
}
