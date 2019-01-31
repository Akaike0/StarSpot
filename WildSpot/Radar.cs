using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections;
using System.Windows.Threading;

namespace StarSpot
{
    public partial class Radar : Form
    {
        // Mods Thread
        private BackgroundWorker mods_bgw = new BackgroundWorker();

        // Radar
        Bitmap RadarBitmap = new Bitmap(RadarHeight, RadarWidth);
        Random RNG = new Random();
        ArrayList waypoints = new ArrayList();

        //color definitions
        Color PlayerColor = Color.Blue;
        float RadarZoom = 4.5F;
        int ZoomFactor = 0; //this syncs the radar's zoom with the minimap

        static int RadarHeight = 287;
        static int RadarWidth = 326;

        const float TILE_SCALE_FACTOR = 531.5F;
        const int TILE_HEIGHT = 287;

        // Actors entity class
        ActorsList elist = new ActorsList();

        // For draggin the window
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        // Stats class
        Stats stats = new Stats();

        public Radar()
        {
            InitializeComponent();

            // Mods Backgroundworker
            mods_bgw.DoWork += mods_bgw_DoWork;
        }

        // Draw stuff
        private Bitmap DrawUnit(Bitmap img, Color UnitColor, float XPos, float YPos, string strName)
        {
            Graphics G = Graphics.FromImage(img);

            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            SolidBrush RadarBrush = new SolidBrush(UnitColor);
            Pen RadarPen = new Pen(UnitColor, 2F);

            G.ResetTransform();
            G.TranslateTransform(-XPos, -YPos, System.Drawing.Drawing2D.MatrixOrder.Append);
            G.TranslateTransform(XPos, YPos, System.Drawing.Drawing2D.MatrixOrder.Append);

            try
            {
                G.FillEllipse(RadarBrush, XPos - 5 / 2, YPos - 5 / 2, 5, 5);
                DrawText(img, strName, Convert.ToInt32(XPos) - Convert.ToInt32((strName.Length) * 2.5), Convert.ToInt32(YPos) + 8);
            }
            catch { }

            G.Dispose();
            RadarBrush.Dispose();
            RadarPen.Dispose();

            return img;

        }
        private Bitmap DrawText(Bitmap img, String TextToDraw, int XPos, int YPos)
        {
            Graphics G = Graphics.FromImage(img);
            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Font DrawFont = new Font("Arial", 8);

            G.DrawString(TextToDraw, DrawFont, Brushes.LightGray, new Point(XPos, YPos));

            G.Dispose();
            DrawFont.Dispose();

            return img;
        }
        private void DrawWaypoints()
        {
            Graphics g = Graphics.FromImage(RadarBitmap);

            // Normal waypoints
            if (CTM_System.waypoints_list_temp.Count != 0)
            {
                int c = 0;
                PointF position = new PointF(stats.player_position_x(), stats.player_position_y());

                foreach (CTM_System.cwaypoint waypoint in CTM_System.waypoints_list_temp)
                {
                    // Create the color
                    Color color = new Color();

                    if (CTM_System.ctm_points == waypoint)
                    {
                        color = Color.DeepSkyBlue;
                    }
                    else
                    {
                        color = Color.SkyBlue;
                    }

                    // Draw
                    g.DrawEllipse(new Pen(new SolidBrush(color)), (((int)(stats.player_position_x() - waypoint.X)) * RadarZoom + RadarWidth / 2) - 3, (((int)(stats.player_position_y() - waypoint.Y)) * RadarZoom + RadarWidth / 2) - 3, 2, 2);

                    c++;
                    if (c > 1 && (Math.Abs(waypoint.X - position.X) <= 100) && (Math.Abs(waypoint.Y - position.Y) <= 100))
                    {
                        g.DrawLine(new Pen(new SolidBrush(color)), new Point((int)((((int)(stats.player_position_x() - position.X)) * RadarZoom + RadarWidth / 2) - 3), (int)((((int)(stats.player_position_y() - position.Y)) * RadarZoom + RadarWidth / 2) - 3)), new Point((int)((((int)(stats.player_position_x() - waypoint.X)) * RadarZoom + RadarWidth / 2) - 3), (int)((((int)(stats.player_position_y() - waypoint.Y)) * RadarZoom + RadarWidth / 2) - 3)));
                    }

                    position = new PointF(waypoint.X, waypoint.Y);
                }
            }

            // Second normal spots list
            if (CTM_System.waypoints_list_2_temp.Count != 0)
            {
                int c = 0;
                PointF position = new PointF(stats.player_position_x(), stats.player_position_y());

                foreach (CTM_System.cwaypoint waypoint in CTM_System.waypoints_list_2_temp)
                {
                    // Create the color
                    Color color = new Color();

                    if (CTM_System.ctm_points == waypoint)
                    {
                        color = Color.DeepSkyBlue;
                    }
                    else
                    {
                        color = Color.SkyBlue;
                    }

                    // Draw
                    g.DrawEllipse(new Pen(new SolidBrush(color)), (((int)(stats.player_position_x() - waypoint.X)) * RadarZoom + RadarWidth / 2) - 3, (((int)(stats.player_position_y() - waypoint.Y)) * RadarZoom + RadarWidth / 2) - 3, 2, 2);

                    c++;
                    if (c > 1 && (Math.Abs(waypoint.X - position.X) <= 100) && (Math.Abs(waypoint.Y - position.Y) <= 100))
                    {
                        g.DrawLine(new Pen(new SolidBrush(color)), new Point((int)((((int)(stats.player_position_x() - position.X)) * RadarZoom + RadarWidth / 2) - 3), (int)((((int)(stats.player_position_y() - position.Y)) * RadarZoom + RadarWidth / 2) - 3)), new Point((int)((((int)(stats.player_position_x() - waypoint.X)) * RadarZoom + RadarWidth / 2) - 3), (int)((((int)(stats.player_position_y() - waypoint.Y)) * RadarZoom + RadarWidth / 2) - 3)));
                    }

                    position = new PointF(waypoint.X, waypoint.Y);
                }
            }

            // Third normal spots list
            if (CTM_System.waypoints_list_3_temp.Count != 0)
            {
                int c = 0;
                PointF position = new PointF(stats.player_position_x(), stats.player_position_y());

                foreach (CTM_System.cwaypoint waypoint in CTM_System.waypoints_list_3_temp)
                {
                    // Create the color
                    Color color = new Color();

                    if (CTM_System.ctm_points == waypoint)
                    {
                        color = Color.DeepSkyBlue;
                    }
                    else
                    {
                        color = Color.SkyBlue;
                    }

                    // Draw
                    g.DrawEllipse(new Pen(new SolidBrush(color)), (((int)(stats.player_position_x() - waypoint.X)) * RadarZoom + RadarWidth / 2) - 3, (((int)(stats.player_position_y() - waypoint.Y)) * RadarZoom + RadarWidth / 2) - 3, 2, 2);

                    c++;
                    if (c > 1 && (Math.Abs(waypoint.X - position.X) <= 100) && (Math.Abs(waypoint.Y - position.Y) <= 100))
                    {
                        g.DrawLine(new Pen(new SolidBrush(color)), new Point((int)((((int)(stats.player_position_x() - position.X)) * RadarZoom + RadarWidth / 2) - 3), (int)((((int)(stats.player_position_y() - position.Y)) * RadarZoom + RadarWidth / 2) - 3)), new Point((int)((((int)(stats.player_position_x() - waypoint.X)) * RadarZoom + RadarWidth / 2) - 3), (int)((((int)(stats.player_position_y() - waypoint.Y)) * RadarZoom + RadarWidth / 2) - 3)));
                    }

                    position = new PointF(waypoint.X, waypoint.Y);
                }
            }

            // Death spots
            if (CTM_System.waypoints_death_list_temp.Count != 0)
            {
                int c = 0;
                PointF position = new PointF(stats.player_position_x(), stats.player_position_y());

                foreach (CTM_System.cwaypoint waypoint in CTM_System.waypoints_death_list_temp)
                {
                    // Create the color
                    Color color = new Color();

                    if (CTM_System.ctm_death_points == waypoint)
                    {
                        color = Color.Crimson;
                    }
                    else
                    {
                        color = Color.DeepPink;
                    }

                    // Draw
                    g.DrawEllipse(new Pen(new SolidBrush(color)), (((int)(stats.player_position_x() - waypoint.X)) * RadarZoom + RadarWidth / 2) - 3, (((int)(stats.player_position_y() - waypoint.Y)) * RadarZoom + RadarWidth / 2) - 3, 2, 2);

                    c++;
                    if (c > 1 && (Math.Abs(waypoint.X - position.X) <= 100) && (Math.Abs(waypoint.Y - position.Y) <= 100))
                    {
                        g.DrawLine(new Pen(new SolidBrush(color)), new Point((int)((((int)(stats.player_position_x() - position.X)) * RadarZoom + RadarWidth / 2) - 3), (int)((((int)(stats.player_position_y() - position.Y)) * RadarZoom + RadarWidth / 2) - 3)), new Point((int)((((int)(stats.player_position_x() - waypoint.X)) * RadarZoom + RadarWidth / 2) - 3), (int)((((int)(stats.player_position_y() - waypoint.Y)) * RadarZoom + RadarWidth / 2) - 3)));
                    }

                    position = new PointF(waypoint.X, waypoint.Y);
                }
            }

            // Death spots second
            if (CTM_System.waypoints_death_list_second_temp.Count != 0)
            {
                int c = 0;
                PointF position = new PointF(stats.player_position_x(), stats.player_position_y());

                foreach (CTM_System.cwaypoint waypoint in CTM_System.waypoints_death_list_second_temp)
                {
                    // Create the color
                    Color color = new Color();

                    if (CTM_System.ctm_death_points == waypoint)
                    {
                        color = Color.Crimson;
                    }
                    else
                    {
                        color = Color.DeepPink;
                    }

                    // Draw
                    g.DrawEllipse(new Pen(new SolidBrush(color)), (((int)(stats.player_position_x() - waypoint.X)) * RadarZoom + RadarWidth / 2) - 3, (((int)(stats.player_position_y() - waypoint.Y)) * RadarZoom + RadarWidth / 2) - 3, 2, 2);

                    c++;
                    if (c > 1 && (Math.Abs(waypoint.X - position.X) <= 100) && (Math.Abs(waypoint.Y - position.Y) <= 100))
                    {
                        g.DrawLine(new Pen(new SolidBrush(color)), new Point((int)((((int)(stats.player_position_x() - position.X)) * RadarZoom + RadarWidth / 2) - 3), (int)((((int)(stats.player_position_y() - position.Y)) * RadarZoom + RadarWidth / 2) - 3)), new Point((int)((((int)(stats.player_position_x() - waypoint.X)) * RadarZoom + RadarWidth / 2) - 3), (int)((((int)(stats.player_position_y() - waypoint.Y)) * RadarZoom + RadarWidth / 2) - 3)));
                    }

                    position = new PointF(waypoint.X, waypoint.Y);
                }
            }

            // Selling spots
            if (CTM_System.waypoints_selling_list_temp.Count != 0)
            {
                int c = 0;
                PointF position = new PointF(stats.player_position_x(), stats.player_position_y());

                foreach (CTM_System.cwaypoint waypoint in CTM_System.waypoints_selling_list_temp)
                {
                    // Create the color
                    Color color = new Color();

                    if (CTM_System.ctm_selling_points == waypoint)
                    {
                        color = Color.Yellow;
                    }
                    else
                    {
                        color = Color.Gold;
                    }

                    // Draw
                    g.DrawEllipse(new Pen(new SolidBrush(color)), (((int)(stats.player_position_x() - waypoint.X)) * RadarZoom + RadarWidth / 2) - 3, (((int)(stats.player_position_y() - waypoint.Y)) * RadarZoom + RadarWidth / 2) - 3, 2, 2);

                    c++;
                    if (c > 1 && (Math.Abs(waypoint.X - position.X) <= 100) && (Math.Abs(waypoint.Y - position.Y) <= 100))
                    {
                        g.DrawLine(new Pen(new SolidBrush(color)), new Point((int)((((int)(stats.player_position_x() - position.X)) * RadarZoom + RadarWidth / 2) - 3), (int)((((int)(stats.player_position_y() - position.Y)) * RadarZoom + RadarWidth / 2) - 3)), new Point((int)((((int)(stats.player_position_x() - waypoint.X)) * RadarZoom + RadarWidth / 2) - 3), (int)((((int)(stats.player_position_y() - waypoint.Y)) * RadarZoom + RadarWidth / 2) - 3)));
                    }

                    position = new PointF(waypoint.X, waypoint.Y);
                }
            }
        }

        // Clear the bitmap function
        private void ClearBitmap(ref Bitmap img)
        {
            Graphics G = Graphics.FromImage(img);
            G.Clear(RadarBox.BackColor);
            G.Dispose();
        }

        // Radar timer
        private void RadarTimer_Tick(object sender, EventArgs e)
        {
            if (!mods_bgw.IsBusy)
            {
                mods_bgw.RunWorkerAsync();
            }
        }

        private void mods_bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            if (this.Visible)
            {
                ZoomFactor = (int)(TILE_HEIGHT * (float)(RadarZoom / 0.5f)); // Create the zoomfactor
                ClearBitmap(ref RadarBitmap); // Clear the map every frame

                try
                {
                    // Update the entity list
                    elist.update();

                    // Waypoints
                    try
                    {
                        DrawWaypoints();
                    }
                    catch { }

                    // Draw objects
                    foreach (Actors entity in elist)
                    {
                        // Player
                        RadarBitmap = DrawUnit(RadarBitmap, PlayerColor, (stats.player_position_x() - stats.player_position_x()) * RadarZoom + RadarWidth / 2, (stats.player_position_y() - stats.player_position_y()) * RadarZoom + RadarHeight / 2, "Player");

                        // NPCS
                        if (entity.health > 0 && entity.name != stats.player_name() && entity.position_x != stats.player_position_x() && entity.position_y != stats.player_position_y() && !entity.name.Contains("Generic Invisible"))
                        {
                            RadarBitmap = DrawUnit(RadarBitmap, Color.Khaki, (stats.player_position_x() - entity.position_x) * RadarZoom + RadarWidth / 2, (stats.player_position_y() - entity.position_y) * RadarZoom + RadarHeight / 2, entity.name);
                        }
                    }

                    // Update the radar
                    RadarBox.Image = RadarBitmap;
                    RadarBox.Update();
                }
                catch { }

                // Sleep to reduce the cpu
                System.Threading.Thread.Sleep(100);
            }
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

        // For draggin the window
        private void Radar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void radar_close_btn_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

    }
}
