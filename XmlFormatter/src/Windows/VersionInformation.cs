using System;
using System.Diagnostics;
using System.Windows.Forms;
using XmlFormatter.src.Manager;

namespace XmlFormatter.src.Windows
{
    /// <summary>
    /// Version information windows
    /// </summary>
    public partial class VersionInformation : Form
    {
        /// <summary>
        /// Index of the last selected third party
        /// </summary>
        private int lastSelectedThirdParty;

        /// <summary>
        /// Creating a new instance of this window
        /// </summary>
        public VersionInformation()
        {
            InitializeComponent();
            lastSelectedThirdParty = -1;
            MinimizeBox = false;
            MaximizeBox = false;
            Text = "Version Information";
        }

        /// <summary>
        /// Load event
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void VersionInformation_Load(object sender, EventArgs e)
        {
            VersionManager manager = new VersionManager();
            Version version = manager.GetApplicationVersion();
            L_VersionNumber.Text = manager.GetStringVersion(version);

            LV_ThirdPartyLibraries.Columns[0].Width = LV_ThirdPartyLibraries.Width - 4;
        }

        /// <summary>
        /// List view selection changed
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void LV_ThirdPartyLibraries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ListView listView)
            {
                if (listView.SelectedItems.Count > 0
                    && listView.SelectedItems[0] is ListViewItem listViewItem
                    && lastSelectedThirdParty != listView.SelectedIndices[0])
                {
                    Process.Start(listViewItem.Tag.ToString());
                    lastSelectedThirdParty = listView.SelectedIndices[0];
                }
            }

        }

        /// <summary>
        /// Event if link was pressed
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event parameter</param>
        private void OpenLinkLabel_Click(object sender, EventArgs e)
        {
            if (sender is LinkLabel linkLabel)
            {
                Process.Start(linkLabel.Tag.ToString());
            }
        }
    }
}
