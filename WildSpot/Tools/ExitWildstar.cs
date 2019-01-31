using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSpot
{
    class ExitWildstar
    {
        // Wildstar client process nr
        public static int process_nr = 0;

        // Unstucking exit counter
        public static int unstucking_counter = 0;

        // Check method
        public static void unstucking_check()
        {
            if(Properties.Settings.Default.exit_wildstar_after_x_attempts != 0)
            {
                // Count the unstucking try
                unstucking_counter++;

                if (unstucking_counter >= Properties.Settings.Default.exit_wildstar_after_x_attempts)
                {
                    // Add a log
                    MainWindow.log_text = "Stucked! Exit Wildstar.";

                    // Turn off the bot
                    MainWindow.bot_running = false;

                    // Exit Wildstar
                    exit_wildstar();
                }
            }
        }

        // Exit wildstar method
        public static void exit_wildstar()
        {
            MainWindow.Wildstar[process_nr].Kill();
        }
    }
}
