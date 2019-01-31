using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace StarSpot
{
    class Update
    {
        // Current app version
        Version current_version;

        // XML reader
        XmlTextReader reader;
        Version newVersion = null;
        // URL string
        private string url;

        // Check version method
        public void check_update()
        {
            try
            {
                string xmlURL = "https://bot.mycode.io/star/last_version.xml";
                reader = new XmlTextReader(xmlURL);
                reader.MoveToContent();
                string elementName = "";

                if ((reader.NodeType == XmlNodeType.Element) &&
                    (reader.Name == "ourfancyapp"))
                {
                    while (reader.Read())
                    {
                        // when we find an element node,  
                        // we remember its name  
                        if (reader.NodeType == XmlNodeType.Element)
                            elementName = reader.Name;
                        else
                        {
                            // for text nodes...  
                            if ((reader.NodeType == XmlNodeType.Text) &&
                                (reader.HasValue))
                            {
                                // we check what the name of the node was  
                                switch (elementName)
                                {
                                    case "version":
                                        // thats why we keep the version info  
                                        // in xxx.xxx.xxx.xxx format  
                                        // the Version class does the  
                                        // parsing for us  
                                        newVersion = new Version(reader.Value);
                                        break;
                                    case "url":
                                        url = reader.Value;
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            catch { }

            // Set the current version
            current_version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            if (current_version.CompareTo(newVersion) < 0)  
            {  
            string title = "Update";
            string question = "Download the new version?";

            if (DialogResult.Yes ==
                  MessageBox.Show(question, title,
                  MessageBoxButtons.YesNo,
                  MessageBoxIcon.Question))
            {
                // Navigate to the download link
                System.Diagnostics.Process.Start(url);
                // Close the app
                Process.GetCurrentProcess().Kill();
            }  
            }
        }
    }
}
