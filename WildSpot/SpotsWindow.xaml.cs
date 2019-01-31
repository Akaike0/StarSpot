using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Globalization;
using System.Windows.Forms;
using System.IO;
using System.Collections.Specialized;

namespace StarSpot
{
    /// <summary>
    /// Interaction logic for SpotsWindow.xaml
    /// </summary>
    public partial class SpotsWindow
    {

        // Classes
        Stats stats = new Stats();

        // Waypoints Array
        List<CTM_System.cwaypoint> waypointlist = new List<CTM_System.cwaypoint>();
        List<CTM_System.cwaypoint> deathlist = new List<CTM_System.cwaypoint>();

        // Timer
        DispatcherTimer wp_auto_add = new DispatcherTimer();
        DispatcherTimer wp_death_auto_add = new DispatcherTimer();
        DispatcherTimer wp_selling_auto_add = new DispatcherTimer();

        // UI Timer
        DispatcherTimer ui_timer = new DispatcherTimer();

        // Window visible bool
        public static bool spots_window_visible = false;

        public SpotsWindow()
        {
            InitializeComponent();

            // Timer Set
            wp_auto_add.Tick += new EventHandler(wp_auto_add_Tick);
            wp_auto_add.Interval = new TimeSpan(0, 0, 0, 0, 100);

            wp_death_auto_add.Tick += new EventHandler(wp_death_auto_add_Tick);
            wp_death_auto_add.Interval = new TimeSpan(0, 0, 0, 0, 100);

            wp_selling_auto_add.Tick += new EventHandler(wp_selling_auto_add_Tick);
            wp_selling_auto_add.Interval = new TimeSpan(0, 0, 0, 0, 100);

            // UI timer
            ui_timer.Tick += new EventHandler(ui_timer_Tick);
            ui_timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            ui_timer.Start();

            // Load old spots
            try // Normal spots
            {
                foreach (CTM_System.cwaypoint wpoint in CTM_System.waypoints_list)
                {
                    string x, y, z, fullstring;

                    x = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.X);
                    y = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.Y);
                    z = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.Z);
                    fullstring = y + " | " + x + " | " + z;

                    if (waypoint_normal_list.Items.IndexOf(fullstring) != waypoint_normal_list.Items.Count)
                    {
                        waypoint_normal_list.Items.Add(fullstring);
                    }
                }

                foreach (CTM_System.cwaypoint wpoint in CTM_System.waypoints_list_2_temp)
                {
                    string x, y, z, fullstring;

                    x = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.X);
                    y = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.Y);
                    z = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.Z);
                    fullstring = y + " | " + x + " | " + z;

                    if (waypoint_normal_list_2.Items.IndexOf(fullstring) != waypoint_normal_list_2.Items.Count)
                    {
                        waypoint_normal_list_2.Items.Add(fullstring);
                    }
                }

                foreach (CTM_System.cwaypoint wpoint in CTM_System.waypoints_list_3_temp)
                {
                    string x, y, z, fullstring;

                    x = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.X);
                    y = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.Y);
                    z = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.Z);
                    fullstring = y + " | " + x + " | " + z;

                    if (waypoint_normal_list_3.Items.IndexOf(fullstring) != waypoint_normal_list_3.Items.Count)
                    {
                        waypoint_normal_list_3.Items.Add(fullstring);
                    }
                }
            }
            catch { }

            // Death spots
            try
            {
                foreach (CTM_System.cwaypoint wpoint in CTM_System.waypoints_death_list) // Death Spots
                {
                    string x, y, fullstring;

                    x = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.X);
                    y = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.Y);
                    fullstring = y + " | " + x;

                    if (waypoint_death_list.Items.IndexOf(fullstring) != waypoint_death_list.Items.Count)
                    {
                        waypoint_death_list.Items.Add(fullstring);
                    }
                }
            }
            catch { }

            try
            {
                foreach (CTM_System.cwaypoint wpoint in CTM_System.waypoints_death_list_second) // Death Spots
                {
                    string x, y, fullstring;

                    x = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.X);
                    y = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.Y);
                    fullstring = y + " | " + x;

                    if (waypoint_death_second_list.Items.IndexOf(fullstring) != waypoint_death_second_list.Items.Count)
                    {
                        waypoint_death_second_list.Items.Add(fullstring);
                    }
                }
            }
            catch { }

            // Selling spots
            try
            {
                foreach (CTM_System.cwaypoint wpoint in CTM_System.waypoints_selling_list) // Death Spots
                {
                    string x, y, fullstring;

                    x = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.X);
                    y = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.Y);
                    fullstring = y + " | " + x;

                    if (waypoint_selling_list.Items.IndexOf(fullstring) != waypoint_selling_list.Items.Count)
                    {
                        waypoint_selling_list.Items.Add(fullstring);
                    }
                }
            }
            catch { }
        }

        // Load spots from settings
        public static void load_spots_settings()
        {
            // Load normal spots
            if (Properties.Settings.Default.normal_spots_1_filepath != "")
            {
                try
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(Properties.Settings.Default.normal_spots_1_filepath);

                    if (File.Exists(Properties.Settings.Default.normal_spots_1_filepath))
                    {
                        CTM_System.waypoints_list.Clear();
                        CTM_System.waypoints_list_temp.Clear();
                    }

                    while (!sr.EndOfStream)
                    {
                        string waypointline = sr.ReadLine();
                        if (waypointline.Contains('|'))
                        {
                            string[] coords = waypointline.Split('|');
                            CTM_System.cwaypoint here = new CTM_System.cwaypoint();
                            here.Z = Convert.ToSingle(coords[2], CultureInfo.InvariantCulture);
                            here.X = Convert.ToSingle(coords[1], CultureInfo.InvariantCulture);
                            here.Y = Convert.ToSingle(coords[0], CultureInfo.InvariantCulture);

                            CTM_System.waypoints_list.Add(here);
                            CTM_System.waypoints_list_temp.Add(here);
                        }

                    }
                    sr.Close();
                }
                catch { }
            }

            if (Properties.Settings.Default.normal_spots_2_filepath != "")
            {
                try
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(Properties.Settings.Default.normal_spots_2_filepath);

                    if (File.Exists(Properties.Settings.Default.normal_spots_2_filepath))
                    {
                        CTM_System.waypoints_list_2_temp.Clear();
                    }

                    while (!sr.EndOfStream)
                    {
                        string waypointline = sr.ReadLine();
                        if (waypointline.Contains('|'))
                        {
                            string[] coords = waypointline.Split('|');
                            CTM_System.cwaypoint here = new CTM_System.cwaypoint();
                            here.Z = Convert.ToSingle(coords[2], CultureInfo.InvariantCulture);
                            here.X = Convert.ToSingle(coords[1], CultureInfo.InvariantCulture);
                            here.Y = Convert.ToSingle(coords[0], CultureInfo.InvariantCulture);

                            CTM_System.waypoints_list_2_temp.Add(here);
                        }

                    }
                    sr.Close();
                }
                catch { }
            }

            if (Properties.Settings.Default.normal_spots_3_filepath != "")
            {
                try
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(Properties.Settings.Default.normal_spots_3_filepath);

                    if (File.Exists(Properties.Settings.Default.normal_spots_3_filepath))
                    {
                        CTM_System.waypoints_list_3_temp.Clear();
                    }

                    while (!sr.EndOfStream)
                    {
                        string waypointline = sr.ReadLine();
                        if (waypointline.Contains('|'))
                        {
                            string[] coords = waypointline.Split('|');
                            CTM_System.cwaypoint here = new CTM_System.cwaypoint();
                            here.Z = Convert.ToSingle(coords[2], CultureInfo.InvariantCulture);
                            here.X = Convert.ToSingle(coords[1], CultureInfo.InvariantCulture);
                            here.Y = Convert.ToSingle(coords[0], CultureInfo.InvariantCulture);

                            CTM_System.waypoints_list_3_temp.Add(here);
                        }

                    }
                    sr.Close();
                }
                catch { }
            }

            // Death spots
            if (Properties.Settings.Default.death_spots_1_filepath != "")
            {
                try
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(Properties.Settings.Default.death_spots_1_filepath);

                    if (File.Exists(Properties.Settings.Default.death_spots_1_filepath))
                    {
                        CTM_System.waypoints_death_list.Clear();
                        CTM_System.waypoints_death_list_temp.Clear();
                    }

                    while (!sr.EndOfStream)
                    {
                        string waypointline = sr.ReadLine();
                        if (waypointline.Contains('|'))
                        {
                            string[] coords = waypointline.Split('|');
                            CTM_System.cwaypoint here = new CTM_System.cwaypoint();
                            here.X = Convert.ToSingle(coords[1], CultureInfo.InvariantCulture);
                            here.Y = Convert.ToSingle(coords[0], CultureInfo.InvariantCulture);

                            CTM_System.waypoints_death_list.Add(here);
                            CTM_System.waypoints_death_list_temp.Add(here);
                        }

                    }
                    sr.Close();
                }
                catch { }
            }

            if (Properties.Settings.Default.death_spots_2_filepath != "")
            {
                try
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(Properties.Settings.Default.death_spots_2_filepath);

                    if (File.Exists(Properties.Settings.Default.death_spots_2_filepath))
                    {
                        CTM_System.waypoints_death_list_second.Clear();
                        CTM_System.waypoints_death_list_second_temp.Clear();
                    }

                    while (!sr.EndOfStream)
                    {
                        string waypointline = sr.ReadLine();
                        if (waypointline.Contains('|'))
                        {
                            string[] coords = waypointline.Split('|');
                            CTM_System.cwaypoint here = new CTM_System.cwaypoint();
                            here.X = Convert.ToSingle(coords[1], CultureInfo.InvariantCulture);
                            here.Y = Convert.ToSingle(coords[0], CultureInfo.InvariantCulture);

                            CTM_System.waypoints_death_list_second.Add(here);
                            CTM_System.waypoints_death_list_second_temp.Add(here);
                        }

                    }
                    sr.Close();
                }
                catch { }
            }

            // Selling spots
            if (Properties.Settings.Default.selling_spots_filepath != "")
            {
                try
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(Properties.Settings.Default.selling_spots_filepath);

                    if (File.Exists(Properties.Settings.Default.selling_spots_filepath))
                    {
                        CTM_System.waypoints_selling_list.Clear();
                        CTM_System.waypoints_selling_list_temp.Clear();
                    }

                    while (!sr.EndOfStream)
                    {
                        string waypointline = sr.ReadLine();
                        if (waypointline.Contains('|'))
                        {
                            string[] coords = waypointline.Split('|');
                            CTM_System.cwaypoint here = new CTM_System.cwaypoint();
                            here.X = Convert.ToSingle(coords[1], CultureInfo.InvariantCulture);
                            here.Y = Convert.ToSingle(coords[0], CultureInfo.InvariantCulture);

                            CTM_System.waypoints_selling_list.Add(here);
                            CTM_System.waypoints_selling_list_temp.Add(here);
                        }

                    }
                    sr.Close();
                }
                catch { }
            }
        }

        // Normal spots
        private void add_normal_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!MainWindow.bot_running)
            {
                if (add_normal_btn.Content.ToString() == "Add")
                {
                    // Change Button label
                    add_normal_btn.Content = "Stop";

                    // Clear old stuff
                    if (normal_spots_1_cbx.IsChecked.Value)
                    {
                        CTM_System.waypoints_list.Clear();
                    }
                    else if (normal_spots_2_cbx.IsChecked.Value)
                    {
                        CTM_System.waypoints_list_2_temp.Clear();
                    }
                    else if (normal_spots_3_cbx.IsChecked.Value)
                    {
                        CTM_System.waypoints_list_3_temp.Clear();
                    }

                    // Start automatical adding timer
                    wp_auto_add.Start();
                }
                else
                {
                    // Change Button label
                    add_normal_btn.Content = "Add";

                    // Stop automatical adding timer
                    wp_auto_add.Stop();
                }
            }
            else
            {
                //Error
                System.Windows.MessageBox.Show("Please stop StarSpot first.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void delete_normal_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!MainWindow.bot_running)
            {
                if (normal_spots_1_cbx.IsChecked.Value)
                {
                    CTM_System.waypoints_list_temp.Clear(); // Delete temp stuff
                    waypoint_normal_list.Items.Clear(); // Delete Content
                    // Delete settings path
                    Properties.Settings.Default.normal_spots_1_filepath = "";
                }
                else if (normal_spots_2_cbx.IsChecked.Value)
                {
                    CTM_System.waypoints_list_2_temp.Clear(); // Delete temp stuff
                    waypoint_normal_list_2.Items.Clear(); // Delete Content
                    // Delete settings path
                    Properties.Settings.Default.normal_spots_2_filepath = "";
                }
                else if (normal_spots_3_cbx.IsChecked.Value)
                {
                    CTM_System.waypoints_list_3_temp.Clear(); // Delete temp stuff
                    waypoint_normal_list_3.Items.Clear(); // Delete Content
                    // Delete settings path
                    Properties.Settings.Default.normal_spots_3_filepath = "";
                }
            }
            else
            {
                //Error
                System.Windows.MessageBox.Show("Please stop StarSpot first.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void normal_save_btn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog savewp_dialog = new SaveFileDialog();

            savewp_dialog.Filter = "sp (*.sp)|*.sp";
            savewp_dialog.FilterIndex = 1;
            savewp_dialog.RestoreDirectory = true;

            if (normal_spots_1_cbx.IsChecked.Value)
            {
                if (savewp_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    StreamWriter writer = new StreamWriter(savewp_dialog.FileName);
                    for (int i = 0; i < waypoint_normal_list.Items.Count; i++)
                    {
                        writer.WriteLine(waypoint_normal_list.Items[i].ToString());
                    }
                    writer.Close();

                    // Save the file path to load the spots again
                    Properties.Settings.Default.normal_spots_1_filepath = savewp_dialog.FileName;
                }
            }
            else if (normal_spots_2_cbx.IsChecked.Value)
            {
                if (savewp_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    StreamWriter writer = new StreamWriter(savewp_dialog.FileName);
                    for (int i = 0; i < waypoint_normal_list_2.Items.Count; i++)
                    {
                        writer.WriteLine(waypoint_normal_list_2.Items[i].ToString());
                    }
                    writer.Close();

                    // Save the file path to load the spots again
                    Properties.Settings.Default.normal_spots_2_filepath = savewp_dialog.FileName;
                }
            }
            else if (normal_spots_3_cbx.IsChecked.Value)
            {
                if (savewp_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    StreamWriter writer = new StreamWriter(savewp_dialog.FileName);
                    for (int i = 0; i < waypoint_normal_list_3.Items.Count; i++)
                    {
                        writer.WriteLine(waypoint_normal_list_3.Items[i].ToString());
                    }
                    writer.Close();

                    // Save the file path to load the spots again
                    Properties.Settings.Default.normal_spots_3_filepath = savewp_dialog.FileName;
                }
            }
        }
        private void normal_load_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!MainWindow.bot_running)
            {
                // Clear
                if (normal_spots_1_cbx.IsChecked.Value)
                {
                    // Clear
                    CTM_System.waypoints_list.Clear();
                    waypoint_normal_list.Items.Clear();
                }
                else if (normal_spots_2_cbx.IsChecked.Value)
                {
                    // Clear
                    CTM_System.waypoints_list_2_temp.Clear();
                    waypoint_normal_list_2.Items.Clear();
                }
                else if (normal_spots_3_cbx.IsChecked.Value)
                {
                    // Clear
                    CTM_System.waypoints_list_3_temp.Clear();
                    waypoint_normal_list_3.Items.Clear();
                }

                // Load
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.FileName = "";
                openFileDialog1.DefaultExt = "sp";
                openFileDialog1.AddExtension = true;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.Filter = "Waypoint Files (*.sp)|*.sp";

                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                    //string wtype = "";
                    //if (sr.EndOfStream != true) type = sr.ReadLine().Trim(); //eat first line 
                    while (sr.EndOfStream != true)
                    {
                        string waypointline = sr.ReadLine();
                        if (waypointline.Contains('|'))
                        {
                            string[] coords = waypointline.Split('|');
                            CTM_System.cwaypoint here = new CTM_System.cwaypoint();
                            here.Z = Convert.ToSingle(coords[2], CultureInfo.InvariantCulture);
                            here.X = Convert.ToSingle(coords[1], CultureInfo.InvariantCulture);
                            here.Y = Convert.ToSingle(coords[0], CultureInfo.InvariantCulture);

                            if (normal_spots_1_cbx.IsChecked.Value)
                            {
                                CTM_System.waypoints_list.Add(here);

                                // Save the file path to load the spots again
                                Properties.Settings.Default.normal_spots_1_filepath = openFileDialog1.FileName;
                            }
                            else if (normal_spots_2_cbx.IsChecked.Value)
                            {
                                CTM_System.waypoints_list_2_temp.Add(here);

                                // Save the file path to load the spots again
                                Properties.Settings.Default.normal_spots_2_filepath = openFileDialog1.FileName;
                            }
                            else if (normal_spots_3_cbx.IsChecked.Value)
                            {
                                CTM_System.waypoints_list_3_temp.Add(here);

                                // Save the file path to load the spots again
                                Properties.Settings.Default.normal_spots_3_filepath = openFileDialog1.FileName;
                            }
                        }

                    }
                    sr.Close();
                }
                try
                {
                    if (normal_spots_1_cbx.IsChecked.Value)
                    {
                        foreach (CTM_System.cwaypoint wpoint in CTM_System.waypoints_list)
                        {
                            string x, y, z, fullstring;

                            x = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.X);
                            y = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.Y);
                            z = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.Z);
                            fullstring = y + " | " + x + " | " + z;
                            waypoint_normal_list.Items.Add(fullstring);
                        }
                    }
                    else if (normal_spots_2_cbx.IsChecked.Value)
                    {
                        foreach (CTM_System.cwaypoint wpoint in CTM_System.waypoints_list_2_temp)
                        {
                            string x, y, z, fullstring;

                            x = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.X);
                            y = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.Y);
                            z = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.Z);
                            fullstring = y + " | " + x + " | " + z;
                            waypoint_normal_list_2.Items.Add(fullstring);
                        }
                    }
                    else if (normal_spots_3_cbx.IsChecked.Value)
                    {
                        foreach (CTM_System.cwaypoint wpoint in CTM_System.waypoints_list_3_temp)
                        {
                            string x, y, z, fullstring;

                            x = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.X);
                            y = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.Y);
                            z = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.Z);
                            fullstring = y + " | " + x + " | " + z;
                            waypoint_normal_list_3.Items.Add(fullstring);
                        }
                    }
                }
                catch { }
            }
            else
            {
                //Error
                System.Windows.MessageBox.Show("Please stop StarSpot first.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void wp_auto_add_Tick(object sender, EventArgs e)
        {
            // Add new spots
            CTM_System.cwaypoint here = new CTM_System.cwaypoint();

            here.X = stats.player_position_x();
            here.Y = stats.player_position_y();
            here.Z = stats.player_position_z();

            string x, y, z, fullstring;
            x = string.Format(CultureInfo.InvariantCulture, "{0:f2}", here.X);
            y = string.Format(CultureInfo.InvariantCulture, "{0:f2}", here.Y);
            z = string.Format(CultureInfo.InvariantCulture, "{0:f2}", here.Z);
            fullstring = y + " | " + x + " | " + z;

            if (normal_spots_1_cbx.IsChecked.Value)
            {
                // Distance to last wp
                double distance;

                try
                {
                    distance = Distance3D(stats.player_position_x(), stats.player_position_y(), stats.player_position_z(), CTM_System.waypoints_list_temp[CTM_System.waypoints_list_temp.Count - 1].X, CTM_System.waypoints_list_temp[CTM_System.waypoints_list_temp.Count - 1].Y, CTM_System.waypoints_list_temp[CTM_System.waypoints_list_temp.Count - 1].Z);
                }
                catch { distance = 10; }

                if (distance > 6.5f)
                {
                    if (!waypoint_normal_list.Items.Contains(fullstring))
                    {
                        CTM_System.waypoints_list_temp.Add(here); // Add temp waypoints for the radar
                        waypoint_normal_list.Items.Add(fullstring); // Add the waypoints to the list_box
                    }
                }
            }
            else if (normal_spots_2_cbx.IsChecked.Value)
            {
                // Distance to last wp
                double distance;

                try
                {
                    distance = Distance3D(stats.player_position_x(), stats.player_position_y(), stats.player_position_z(), CTM_System.waypoints_list_2_temp[CTM_System.waypoints_list_2_temp.Count - 1].X, CTM_System.waypoints_list_2_temp[CTM_System.waypoints_list_2_temp.Count - 1].Y, CTM_System.waypoints_list_2_temp[CTM_System.waypoints_list_2_temp.Count - 1].Z);
                }
                catch { distance = 10; }

                if (distance > 6.5f)
                {
                    if (!waypoint_normal_list_2.Items.Contains(fullstring))
                    {
                        CTM_System.waypoints_list_2_temp.Add(here); // Add temp waypoints for the radar
                        waypoint_normal_list_2.Items.Add(fullstring); // Add the waypoints to the list_box
                    }
                }
            }
            else if (normal_spots_3_cbx.IsChecked.Value)
            {
                // Distance to last wp
                double distance;

                try
                {
                    distance = Distance3D(stats.player_position_x(), stats.player_position_y(), stats.player_position_z(), CTM_System.waypoints_list_3_temp[CTM_System.waypoints_list_3_temp.Count - 1].X, CTM_System.waypoints_list_3_temp[CTM_System.waypoints_list_3_temp.Count - 1].Y, CTM_System.waypoints_list_3_temp[CTM_System.waypoints_list_3_temp.Count - 1].Z);
                }
                catch { distance = 10; }

                if (distance > 6.5f)
                {
                    if (!waypoint_normal_list_3.Items.Contains(fullstring))
                    {
                        CTM_System.waypoints_list_3_temp.Add(here); // Add temp waypoints for the radar
                        waypoint_normal_list_3.Items.Add(fullstring); // Add the waypoints to the list_box
                    }
                }
            }
        }

        // Check boxes
        private void normal_spots_1_cbx_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                normal_spots_2_cbx.IsChecked = false;
                normal_spots_3_cbx.IsChecked = false;
            }
            catch { }
        }
        private void normal_spots_2_cbx_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                normal_spots_1_cbx.IsChecked = false;
                normal_spots_3_cbx.IsChecked = false;
            }
            catch { }
        }
        private void normal_spots_3_cbx_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                normal_spots_1_cbx.IsChecked = false;
                normal_spots_2_cbx.IsChecked = false;
            }
            catch { }
        }

        // Death spots
        private void add_death_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!MainWindow.bot_running)
            {
                if (add_death_btn.Content.ToString() == "Add")
                {
                    // Change Button label
                    add_death_btn.Content = "Stop";

                    // Clear old wps
                    if (death_respawn_1_cbx.IsChecked.Value)
                    {
                        CTM_System.waypoints_death_list.Clear();
                    }
                    else if (death_respawn_2_cbx.IsChecked.Value)
                    {
                        CTM_System.waypoints_death_list_second.Clear();
                    }

                    // Start automatical adding timer
                    wp_death_auto_add.Start();
                }
                else
                {
                    // Change Button label
                    add_death_btn.Content = "Add";

                    // Stop automatical adding timer
                    wp_death_auto_add.Stop();
                }
            }
            else
            {
                //Error
                System.Windows.MessageBox.Show("Please stop StarSpot first.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void delete_death_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!MainWindow.bot_running)
            {
                if (death_respawn_1_cbx.IsChecked.Value)
                {
                    CTM_System.waypoints_death_list_temp.Clear(); // Delete temp death spots
                    waypoint_death_list.Items.Clear(); // Delete spots from list
                    // Delete settings path
                    Properties.Settings.Default.death_spots_1_filepath = "";
                }
                else if (death_respawn_2_cbx.IsChecked.Value)
                {
                    CTM_System.waypoints_death_list_second_temp.Clear(); // Delete temp death spots
                    waypoint_death_second_list.Items.Clear(); // Delete spots from list
                    // Delete settings path
                    Properties.Settings.Default.death_spots_2_filepath = "";
                }
            }
            else
            {
                //Error
                System.Windows.MessageBox.Show("Please stop StarSpot first.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void death_save_btn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog savewp_dialog = new SaveFileDialog();

            savewp_dialog.Filter = "sdp (*.sdp)|*.sdp";
            savewp_dialog.FilterIndex = 1;
            savewp_dialog.RestoreDirectory = true;

            if (death_respawn_1_cbx.IsChecked.Value)
            {
                if (savewp_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    StreamWriter writer = new StreamWriter(savewp_dialog.FileName);
                    for (int i = 0; i < waypoint_death_list.Items.Count; i++)
                    {
                        writer.WriteLine(waypoint_death_list.Items[i].ToString());
                    }
                    writer.Close();

                    // Save the file path to load the spots again
                    Properties.Settings.Default.death_spots_1_filepath = savewp_dialog.FileName;
                }
            }
            else if (death_respawn_2_cbx.IsChecked.Value)
            {
                if (savewp_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    StreamWriter writer = new StreamWriter(savewp_dialog.FileName);
                    for (int i = 0; i < waypoint_death_second_list.Items.Count; i++)
                    {
                        writer.WriteLine(waypoint_death_second_list.Items[i].ToString());
                    }
                    writer.Close();

                    // Save the file path to load the spots again
                    Properties.Settings.Default.death_spots_2_filepath = savewp_dialog.FileName;
                }
            }
        }
        private void death_load_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!MainWindow.bot_running)
            {
                // Load
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.FileName = "";
                openFileDialog1.DefaultExt = "sdp";
                openFileDialog1.AddExtension = true;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.Filter = "Waypoint Files (*.sdp)|*.sdp";

                if (death_respawn_1_cbx.IsChecked.Value)
                {
                    // Clear
                    CTM_System.waypoints_death_list.Clear();
                    CTM_System.waypoint_death_count = 0;
                    waypoint_death_list.Items.Clear();

                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                        //string wtype = "";
                        //if (sr.EndOfStream != true) type = sr.ReadLine().Trim(); //eat first line 
                        while (sr.EndOfStream != true)
                        {
                            string waypointline = sr.ReadLine();
                            if (waypointline.Contains('|'))
                            {
                                string[] coords = waypointline.Split('|');
                                CTM_System.cwaypoint here = new CTM_System.cwaypoint();
                                here.X = Convert.ToSingle(coords[1], CultureInfo.InvariantCulture);
                                here.Y = Convert.ToSingle(coords[0], CultureInfo.InvariantCulture);
                                CTM_System.waypoints_death_list.Add(here);
                            }

                        }
                        sr.Close();

                        // Save the file path to load the spots again
                        Properties.Settings.Default.death_spots_1_filepath = openFileDialog1.FileName;
                    }
                    try
                    {
                        foreach (CTM_System.cwaypoint wpoint in CTM_System.waypoints_death_list)
                        {
                            string x, y, fullstring;

                            x = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.X);
                            y = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.Y);
                            fullstring = y + " | " + x;
                            waypoint_death_list.Items.Add(fullstring);
                        }
                    }
                    catch { }
                }
                else if (death_respawn_2_cbx.IsChecked.Value)
                {
                    // Clear
                    CTM_System.waypoints_death_list_second.Clear();
                    CTM_System.waypoint_death_count = 0;
                    waypoint_death_second_list.Items.Clear();

                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                        //string wtype = "";
                        //if (sr.EndOfStream != true) type = sr.ReadLine().Trim(); //eat first line 
                        while (sr.EndOfStream != true)
                        {
                            string waypointline = sr.ReadLine();
                            if (waypointline.Contains('|'))
                            {
                                string[] coords = waypointline.Split('|');
                                CTM_System.cwaypoint here = new CTM_System.cwaypoint();
                                here.X = Convert.ToSingle(coords[1], CultureInfo.InvariantCulture);
                                here.Y = Convert.ToSingle(coords[0], CultureInfo.InvariantCulture);
                                CTM_System.waypoints_death_list_second.Add(here);
                            }

                        }
                        sr.Close();

                        // Save the file path to load the spots again
                        Properties.Settings.Default.death_spots_2_filepath = openFileDialog1.FileName;
                    }
                    try
                    {
                        foreach (CTM_System.cwaypoint wpoint in CTM_System.waypoints_death_list_second)
                        {
                            string x, y, fullstring;

                            x = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.X);
                            y = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.Y);
                            fullstring = y + " | " + x;
                            waypoint_death_second_list.Items.Add(fullstring);
                        }
                    }
                    catch { }
                }
            }
            else
            {
                //Error
                System.Windows.MessageBox.Show("Please stop StarSpot first.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void wp_death_auto_add_Tick(object sender, EventArgs e)
        {
            CTM_System.cwaypoint here = new CTM_System.cwaypoint();

            here.X = stats.player_position_x();
            here.Y = stats.player_position_y();
            string x, y, fullstring;
            x = string.Format(CultureInfo.InvariantCulture, "{0:f2}", here.X);
            y = string.Format(CultureInfo.InvariantCulture, "{0:f2}", here.Y);
            fullstring = y + " | " + x;

            if (death_respawn_1_cbx.IsChecked.Value)
            {
                // Distance to last wp
                double distance;

                try
                {
                    distance = Distance2D(stats.player_position_x(), stats.player_position_y(), CTM_System.waypoints_death_list_temp[CTM_System.waypoints_death_list_temp.Count - 1].X, CTM_System.waypoints_death_list_temp[CTM_System.waypoints_death_list_temp.Count - 1].Y);
                }
                catch { distance = 10; }

                if (distance > 6.5f)
                {
                    if (!waypoint_death_list.Items.Contains(fullstring))
                    {
                        CTM_System.waypoints_death_list_temp.Add(here); // Add temp waypoints for the radar
                        waypoint_death_list.Items.Add(fullstring); // Add the waypoints to the list_box
                    }
                }
            }
            else if (death_respawn_2_cbx.IsChecked.Value)
            {
                // Distance to last wp
                double distance;

                try
                {
                    distance = Distance2D(stats.player_position_x(), stats.player_position_y(), CTM_System.waypoints_death_list_second_temp[CTM_System.waypoints_death_list_second_temp.Count - 1].X, CTM_System.waypoints_death_list_second_temp[CTM_System.waypoints_death_list_second_temp.Count - 1].Y);
                }
                catch { distance = 1000; }

                if (distance > 6.5f)
                {
                    if (!waypoint_death_second_list.Items.Contains(fullstring))
                    {
                        CTM_System.waypoints_death_list_second_temp.Add(here); // Add temp waypoints for the radar
                        waypoint_death_second_list.Items.Add(fullstring); // Add the waypoints to the list_box
                    }
                }
            }
        }

        // Check boxes for death spots
        private void death_respawn_1_cbx_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                death_respawn_2_cbx.IsChecked = false;
            }
            catch { }
        }
        private void death_respawn_2_cbx_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                death_respawn_1_cbx.IsChecked = false;
            }
            catch { }
        }

        // Selling Spots
        private void add_selling_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!MainWindow.bot_running)
            {
                if (add_selling_btn.Content.ToString() == "Add")
                {
                    // Change Button label
                    add_selling_btn.Content = "Stop";

                    // Delete old wps
                    CTM_System.waypoints_selling_list.Clear();

                    // Start automatical adding timer
                    wp_selling_auto_add.Start();
                }
                else
                {
                    // Change Button label
                    add_selling_btn.Content = "Add";

                    // Stop automatical adding timer
                    wp_selling_auto_add.Stop();
                }
            }
            else
            {
                //Error
                System.Windows.MessageBox.Show("Please stop StarSpot first.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void delete_selling_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!MainWindow.bot_running)
            {
                CTM_System.waypoints_selling_list_temp.Clear(); // Delete temp selling spots
                waypoint_selling_list.Items.Clear(); // Delete spots from list
                // Delete settings path
                Properties.Settings.Default.selling_spots_filepath = "";
            }
            else
            {
                //Error
                System.Windows.MessageBox.Show("Please stop StarSpot first.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void selling_save_btn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog savewp_dialog = new SaveFileDialog();

            savewp_dialog.Filter = "snp (*.snp)|*.snp";
            savewp_dialog.FilterIndex = 1;
            savewp_dialog.RestoreDirectory = true;

            if (savewp_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                StreamWriter writer = new StreamWriter(savewp_dialog.FileName);
                for (int i = 0; i < waypoint_selling_list.Items.Count; i++)
                {
                    writer.WriteLine(waypoint_selling_list.Items[i].ToString());
                }
                writer.Close();

                // Save the file path to load the spots again
                Properties.Settings.Default.selling_spots_filepath = savewp_dialog.FileName;
            }
        }
        private void selling_load_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!MainWindow.bot_running)
            {
                // Clear
                CTM_System.waypoints_selling_list.Clear();
                CTM_System.waypoint_selling_count = 0;
                waypoint_selling_list.Items.Clear();

                // Load
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.FileName = "";
                openFileDialog1.DefaultExt = "sdp";
                openFileDialog1.AddExtension = true;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.Filter = "Waypoint Files (*.snp)|*.snp";

                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                    //string wtype = "";
                    //if (sr.EndOfStream != true) type = sr.ReadLine().Trim(); //eat first line 
                    while (sr.EndOfStream != true)
                    {
                        string waypointline = sr.ReadLine();
                        if (waypointline.Contains('|'))
                        {
                            string[] coords = waypointline.Split('|');
                            CTM_System.cwaypoint here = new CTM_System.cwaypoint();
                            here.X = Convert.ToSingle(coords[1], CultureInfo.InvariantCulture);
                            here.Y = Convert.ToSingle(coords[0], CultureInfo.InvariantCulture);
                            CTM_System.waypoints_selling_list.Add(here);
                        }

                    }
                    sr.Close();

                    // Save the file path to load the spots again
                    Properties.Settings.Default.selling_spots_filepath = openFileDialog1.FileName;
                }
                try
                {
                    foreach (CTM_System.cwaypoint wpoint in CTM_System.waypoints_selling_list)
                    {
                        string x, y, fullstring;

                        x = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.X);
                        y = string.Format(CultureInfo.InvariantCulture, "{0:f2}", wpoint.Y);
                        fullstring = y + " | " + x;
                        waypoint_selling_list.Items.Add(fullstring);
                    }
                }
                catch { }
            }
            else
            {
                //Error
                System.Windows.MessageBox.Show("Please stop StarSpot first.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void wp_selling_auto_add_Tick(object sender, EventArgs e)
        {
            CTM_System.cwaypoint here = new CTM_System.cwaypoint();

            here.X = stats.player_position_x();
            here.Y = stats.player_position_y();
            string x, y, fullstring;
            x = string.Format(CultureInfo.InvariantCulture, "{0:f2}", here.X);
            y = string.Format(CultureInfo.InvariantCulture, "{0:f2}", here.Y);
            fullstring = y + " | " + x;

            // Distance to last wp
            double distance;

            try
            {
                distance = Distance2D(stats.player_position_x(), stats.player_position_y(), CTM_System.waypoints_selling_list_temp[CTM_System.waypoints_selling_list_temp.Count - 1].X, CTM_System.waypoints_selling_list_temp[CTM_System.waypoints_selling_list_temp.Count - 1].Y);
            }
            catch { distance = 10; }

            if (distance > 5.0f)
            {
                if (!waypoint_selling_list.Items.Contains(fullstring))
                {
                    CTM_System.waypoints_selling_list_temp.Add(here); // Add temp waypoints for the radar
                    waypoint_selling_list.Items.Add(fullstring); // Add the waypoints into the list_box
                }
            }
        }

        // UI timer
        private void ui_timer_Tick(object sender, EventArgs e)
        {
            // Change the death spots list if user chosen other location
            if (death_respawn_1_cbx.IsChecked.Value)
            {
                waypoint_death_list.Visibility = Visibility.Visible;
                waypoint_death_second_list.Visibility = Visibility.Collapsed;

            }
            else if (death_respawn_2_cbx.IsChecked.Value)
            {
                waypoint_death_list.Visibility = Visibility.Collapsed;
                waypoint_death_second_list.Visibility = Visibility.Visible;
            }

            // Normal spots
            if (normal_spots_1_cbx.IsChecked.Value)
            {
                waypoint_normal_list.Visibility = Visibility.Visible;
                waypoint_normal_list_2.Visibility = Visibility.Collapsed;
                waypoint_normal_list_3.Visibility = Visibility.Collapsed;
            }
            else if (normal_spots_2_cbx.IsChecked.Value)
            {
                waypoint_normal_list.Visibility = Visibility.Collapsed;
                waypoint_normal_list_2.Visibility = Visibility.Visible;
                waypoint_normal_list_3.Visibility = Visibility.Collapsed;
            }
            else if (normal_spots_3_cbx.IsChecked.Value)
            {
                waypoint_normal_list.Visibility = Visibility.Collapsed;
                waypoint_normal_list_2.Visibility = Visibility.Collapsed;
                waypoint_normal_list_3.Visibility = Visibility.Visible;
            }
        }

        #region Calculations
        // Distance calculation 3D
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

        // Distance calculation 2D
        public double Distance2D(float x1, float y1, float x2, float y2)
        {
            double result = 0;

            double part1 = System.Math.Pow((x2 - x1), 2);

            double part2 = System.Math.Pow((y2 - y1), 2);

            double underRadical = part1 + part2;

            result = System.Math.Sqrt(underRadical);

            return result;
        }
        #endregion Calculation

        // Windows closing
        private void save_btn_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 5; i++) // Clear the old stuff
            {
                CTM_System.waypoint_count = 0;
                CTM_System.waypoints_list.Clear();
                CTM_System.waypoints_list_temp.Clear();
                CTM_System.waypoints_list_2_temp.Clear();
                CTM_System.waypoints_list_3_temp.Clear();
                CTM_System.waypoints_death_list.Clear();
                CTM_System.waypoints_death_list_second.Clear();
                CTM_System.waypoints_selling_list.Clear();
            }

            // Save normal spots into ctm class
            foreach (string newloc in waypoint_normal_list.Items)
            {
                string[] theitem = newloc.Split('|');
                CTM_System.cwaypoint wpoint = new CTM_System.cwaypoint();
                wpoint.Z = Convert.ToSingle(theitem[2], CultureInfo.InvariantCulture);
                wpoint.X = Convert.ToSingle(theitem[1], CultureInfo.InvariantCulture);
                wpoint.Y = Convert.ToSingle(theitem[0], CultureInfo.InvariantCulture);
                CTM_System.waypoints_list.Add(wpoint);
                CTM_System.waypoints_list_temp.Add(wpoint);
            }

            foreach (string newloc in waypoint_normal_list_2.Items)
            {
                string[] theitem = newloc.Split('|');
                CTM_System.cwaypoint wpoint = new CTM_System.cwaypoint();
                wpoint.Z = Convert.ToSingle(theitem[2], CultureInfo.InvariantCulture);
                wpoint.X = Convert.ToSingle(theitem[1], CultureInfo.InvariantCulture);
                wpoint.Y = Convert.ToSingle(theitem[0], CultureInfo.InvariantCulture);
                CTM_System.waypoints_list_2_temp.Add(wpoint);
            }

            foreach (string newloc in waypoint_normal_list_3.Items)
            {
                string[] theitem = newloc.Split('|');
                CTM_System.cwaypoint wpoint = new CTM_System.cwaypoint();
                wpoint.Z = Convert.ToSingle(theitem[2], CultureInfo.InvariantCulture);
                wpoint.X = Convert.ToSingle(theitem[1], CultureInfo.InvariantCulture);
                wpoint.Y = Convert.ToSingle(theitem[0], CultureInfo.InvariantCulture);
                CTM_System.waypoints_list_3_temp.Add(wpoint);
            }

            // Save death spots into ctm class
            foreach (string newloce in waypoint_death_list.Items)
            {
                string[] theitem = newloce.Split('|');
                CTM_System.cwaypoint wpointd = new CTM_System.cwaypoint();
                wpointd.X = Convert.ToSingle(theitem[1], CultureInfo.InvariantCulture);
                wpointd.Y = Convert.ToSingle(theitem[0], CultureInfo.InvariantCulture);
                CTM_System.waypoints_death_list.Add(wpointd);
            }

            foreach (string newloce in waypoint_death_second_list.Items)
            {
                string[] theitem = newloce.Split('|');
                CTM_System.cwaypoint wpointd = new CTM_System.cwaypoint();
                wpointd.X = Convert.ToSingle(theitem[1], CultureInfo.InvariantCulture);
                wpointd.Y = Convert.ToSingle(theitem[0], CultureInfo.InvariantCulture);
                CTM_System.waypoints_death_list_second.Add(wpointd);
            }


            // Save selling spots into ctm class
            foreach (string newloce in waypoint_selling_list.Items)
            {
                string[] theitem = newloce.Split('|');
                CTM_System.cwaypoint wpoints = new CTM_System.cwaypoint();
                wpoints.X = Convert.ToSingle(theitem[1], CultureInfo.InvariantCulture);
                wpoints.Y = Convert.ToSingle(theitem[0], CultureInfo.InvariantCulture);
                CTM_System.waypoints_selling_list.Add(wpoints);
            }

            // Disable visibility for opening
            spots_window_visible = false;

            // Stop timer
            wp_auto_add.Stop();
            wp_death_auto_add.Stop();
            wp_selling_auto_add.Stop();

            // Save temp spots
            save_temp();

            // Save settings
            Properties.Settings.Default.Save();

            // Close Spots Menu
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Stop timer
            wp_auto_add.Stop();
            wp_death_auto_add.Stop();
            wp_selling_auto_add.Stop();

            // Disable visibility for opening
            spots_window_visible = false;
        }

        // Save temp. files
        public void save_temp()
        {
            // Create a new folder
            Directory.CreateDirectory(Environment.CurrentDirectory + "/spots");

            // Normal spots
            if (Properties.Settings.Default.normal_spots_1_filepath == "" | Properties.Settings.Default.normal_spots_1_filepath == Environment.CurrentDirectory + @"\spots\normal_spots_autosave.sp")
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(Environment.CurrentDirectory + @"\spots\normal_spots_autosave.sp"))
                {
                    // If the line doesn't contain the word 'Second', write the line to the file. 
                    for (int i = 0; i < waypoint_normal_list.Items.Count; i++)
                    {
                        file.WriteLine(waypoint_normal_list.Items[i].ToString());
                    }
                }

                Properties.Settings.Default.normal_spots_1_filepath = Environment.CurrentDirectory + @"\spots\normal_spots_autosave.sp";
            }

            if (Properties.Settings.Default.normal_spots_2_filepath == "" | Properties.Settings.Default.normal_spots_2_filepath == Environment.CurrentDirectory + @"\spots\normal_2_spots_autosave.sp")
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(Environment.CurrentDirectory + @"\spots\normal_2_spots_autosave.sp"))
                {
                    // If the line doesn't contain the word 'Second', write the line to the file. 
                    for (int i = 0; i < waypoint_normal_list_2.Items.Count; i++)
                    {
                        file.WriteLine(waypoint_normal_list_2.Items[i].ToString());
                    }
                }

                Properties.Settings.Default.normal_spots_2_filepath = Environment.CurrentDirectory + @"\spots\normal_2_spots_autosave.sp";
            }

            if (Properties.Settings.Default.normal_spots_3_filepath == "" | Properties.Settings.Default.normal_spots_3_filepath == Environment.CurrentDirectory + @"\spots\normal_3_spots_autosave.sp")
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(Environment.CurrentDirectory + @"\spots\normal_3_spots_autosave.sp"))
                {
                    // If the line doesn't contain the word 'Second', write the line to the file. 
                    for (int i = 0; i < waypoint_normal_list_3.Items.Count; i++)
                    {
                        file.WriteLine(waypoint_normal_list_3.Items[i].ToString());
                    }
                }

                Properties.Settings.Default.normal_spots_3_filepath = Environment.CurrentDirectory + @"\spots\normal_3_spots_autosave.sp";
            }

            // Death spots
            if (Properties.Settings.Default.death_spots_1_filepath == "" | Properties.Settings.Default.death_spots_1_filepath == Environment.CurrentDirectory + @"\spots\death_spots_autosave.snp")
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(Environment.CurrentDirectory + @"\spots\death_spots_autosave.sdp"))
                {
                    // If the line doesn't contain the word 'Second', write the line to the file. 
                    for (int i = 0; i < waypoint_death_list.Items.Count; i++)
                    {
                        file.WriteLine(waypoint_death_list.Items[i].ToString());
                    }
                }

                Properties.Settings.Default.death_spots_1_filepath = Environment.CurrentDirectory + @"\spots\death_spots_autosave.snp";
            }

            if (Properties.Settings.Default.death_spots_2_filepath == "" | Properties.Settings.Default.death_spots_2_filepath == Environment.CurrentDirectory + @"\spots\death_second_spots_autosave.snp")
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(Environment.CurrentDirectory + @"\spots\death_second_spots_autosave.sdp"))
                {
                    // If the line doesn't contain the word 'Second', write the line to the file. 
                    for (int i = 0; i < waypoint_death_second_list.Items.Count; i++)
                    {
                        file.WriteLine(waypoint_death_second_list.Items[i].ToString());
                    }
                }

                Properties.Settings.Default.death_spots_2_filepath = Environment.CurrentDirectory + @"\spots\death_second_spots_autosave.snp";
            }

            // Selling spot
            if (Properties.Settings.Default.selling_spots_filepath == "" | Properties.Settings.Default.selling_spots_filepath == Environment.CurrentDirectory + @"\spots\selling_spots_autosave.snp")
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(Environment.CurrentDirectory + @"\spots\selling_spots_autosave.snp"))
                {
                    // If the line doesn't contain the word 'Second', write the line to the file. 
                    for (int i = 0; i < waypoint_selling_list.Items.Count; i++)
                    {
                        file.WriteLine(waypoint_selling_list.Items[i].ToString());
                    }
                }

                Properties.Settings.Default.selling_spots_filepath = Environment.CurrentDirectory + @"\spots\selling_spots_autosave.snp";
            }
        }
    }
}
