using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSpot
{
    class Hacks
    {
        // Stats class
        Stats stats = new Stats();

        public void start()
        {
            // GPU rendering
            if (Properties.Settings.Default.disable_gpu_rendering)
            {
                if (MainWindow.bot_running)
                {
                    if (stats.camera_views(1, 0) != 0) // Check if view is 0, if not write 0
                    {
                        stats.camera_views(2, 0);
                    }

                    if (stats.camera_views(3, 0) != 1000) // Same with horizontal view
                    {
                        stats.camera_views(4, 1000);
                    }
                }
                else
                {
                    if (stats.camera_views(1, 0) == 0)
                    {
                        stats.camera_views(2, 352);
                    }

                    if (stats.camera_views(3, 0) == 1000)
                    {
                        stats.camera_views(4, 1080);
                    }
                }
            }

            // FPS
            if(Properties.Settings.Default.reduce_fps_hack)
            {
                if(stats.fps() != 5)
                    stats.fps(5);
            }
            else
            {
                if (stats.fps() == 5)
                    stats.fps(60);
            }

            // Zoom
            if (Properties.Settings.Default.enable_maximum_zoom)
            {
                if (stats.camera_views(5, 0) != 1000) // Zoom hack
                {
                    stats.camera_views(6, 1000);
                }
            }
            else
            {
                if (stats.camera_views(5, 0) == 1000)
                {
                    stats.camera_views(6, 32);
                }
            }
        }

    }
}
