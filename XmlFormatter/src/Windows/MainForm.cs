using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using XmlFormatter.src.Manager;
using XmlFormatter.src.DataContainer;

namespace XmlFormatter.src.Windows
{
    /// <summary>
    /// This class is representing the main form of the application
    /// </summary>
    public partial class MainForm : Form
    {
        readonly string defaultStatus;

        private delegate void SetControlEnableStatus(Control control);
        private delegate void SetControlText(Control control);

        /// <summary>
        /// Constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            defaultStatus = "Status: ";
        }


        /// <summary>
        /// Loading the form event
        /// </summary>
        /// <param name="sender">The control triggering the method</param>
        /// <param name="e">The event arguments</param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            CB_Mode.SelectedIndex = 0;
            AllowDrop = true;
            MaximizeBox = false;
            L_Status.Text = defaultStatus;
        }

        /// <summary>
        /// This method will allow you to select the xml file you want to convert
        /// </summary>
        /// <param name="sender">Which control did call this method</param>
        /// <param name="e">The event args given by the control</param>
        private void B_Select_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "XML files (*.xml)|*.xml";
            DialogResult result = dialog.ShowDialog();

            if (result != DialogResult.OK || !File.Exists(dialog.FileName))
            {
                return;
            }

            TB_SelectedXml.Text = dialog.FileName;
        }

        /// <summary>
        /// This method will load the xml mentioned in the textbox and save it to a given location
        /// </summary>
        /// <param name="sender">Which control did call this method</param>
        /// <param name="e">The event args given by the control</param>
        private void B_Save_Click(object sender, EventArgs e)
        {
            if (TB_SelectedXml.Text == String.Empty)
            {
                return;
            }
            if (!File.Exists(TB_SelectedXml.Text))
            {
                MessageBox.Show(
                    "Could not find or read file " + TB_SelectedXml.Text,
                    "Could not find file",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                return;

            }

            SaveFileDialog saveFile = new SaveFileDialog();
            FileInfo fi = new FileInfo(TB_SelectedXml.Text);
            string name = fi.Name.Replace(fi.Extension, "");
            saveFile.FileName = name + "_" + CB_Mode.SelectedItem.ToString() + fi.Extension;
            saveFile.Filter = "XML files (*.xml)|*.xml";
            DialogResult result = saveFile.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }


            SwitchFormMode(false);
            string inputFile = TB_SelectedXml.Text;
            bool formattedMode = CB_Mode.SelectedIndex == 0;
            SaveFormattedFile(inputFile, saveFile.FileName, formattedMode);
            
            return;
        }

        /// <summary>
        /// This method will async save the file given input file to the given output file
        /// </summary>
        /// <param name="inputFilePath">The path to the input file</param>
        /// <param name="outputFilePath">The path to the output file</param>
        /// <param name="formatted">Should be formatted (true) or flat (false)</param>
        /// <returns></returns>
        private async Task<bool> SaveFormattedFile(string inputFilePath, string outputFilePath, bool formatted)
        {
            SaveOptions options = SaveOptions.DisableFormatting;
            if (formatted)
            {
                options = SaveOptions.None;
            }
            L_Status.Text = defaultStatus + "Loading ...";

            XElement fileToConvert = await Task<XElement>.Run(() =>
            {
                return XElement.Load(inputFilePath);
            });

            L_Status.Text = defaultStatus + "Saving ...";
            await Task.Run(() => fileToConvert.Save(outputFilePath, options));

            L_Status.Text = defaultStatus + "Saving done!";

            SwitchFormMode(true);

            return true;
        }

        /// <summary>
        /// This method will switch the form to a different state
        /// </summary>
        /// <param name="enabled">The state is either true for useable or false for blocked</param>
        private void SwitchFormMode(bool enabled)
        {
            B_Save.Enabled = enabled;
            TB_SelectedXml.Enabled = enabled;
            B_Select.Enabled = enabled;
            AllowDrop = enabled;
        }

        /// <summary>
        /// This method is the drag enter event
        /// </summary>
        /// <param name="sender">The control sending the drag enter event</param>
        /// <param name="e">The event arguments</param>
        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length != 1)
            {
                return;
            }
            string fileName = files[0];
            FileInfo fi = new FileInfo(fileName);
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && fi.Extension.ToLower() == ".xml")
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        /// <summary>
        /// This event will allow you to drag and drop a file
        /// </summary>
        /// <param name="sender">The control sending the drag and drop event</param>
        /// <param name="e">The event arguments</param>
        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length != 1)
            {
                return;
            }
            TB_SelectedXml.Text = files[0];
        }

        /// <summary>
        /// This method is the click event for the about menu
        /// </summary>
        /// <param name="sender">The control sending the click event</param>
        /// <param name="e">The event arguments</param>
        private void MI_About_Click(object sender, EventArgs e)
        {
            VersionManager manager = new VersionManager();
            Version version = manager.GetApplicationVersion();
            MessageBox.Show(manager.GetStringVersion(version), "Version", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// This method will check if there is newer version for download
        /// </summary>
        /// <param name="sender">The control triggering the event</param>
        /// <param name="e">The event data</param>
        private async void MI_CheckForUpdate_Click(object sender, EventArgs e)
        {
            VersionManager manager = new VersionManager();
            manager.Error += Manager_Error;
            VersionCompare versionCompare = await manager.GitHubVersionIsNewer();
            string text = "Your version is up to date";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            if (versionCompare.GitHubIsNewer)
            {
                text = "There is a newer version available";

                text += "\n\nYour version: " + manager.GetStringVersion(versionCompare.LocalVersion);
                text += "\nGitHub version: " + manager.GetStringVersion(versionCompare.GitHubVersion);

                text += "\n\nDo you want to upgrade now?";
                buttons = MessageBoxButtons.YesNo;
            }

            DialogResult result = MessageBox.Show(text, "Version status", buttons, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                Process.Start("https://github.com/XanatosX/XmlFormatter/releases/tag/" + versionCompare.LatestRelease.TagName);
            }
        }

        /// <summary>
        /// Manage the error throwen by the version manager
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The arguments provided by the sender</param>
        private void Manager_Error(object sender, src.EventMessages.BaseEventArgs e)
        {
            MessageBox.Show(e.Message, e.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Click on the report bug button
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The arguments from the sender</param>
        private void MI_ReportIssue_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/XanatosX/XmlFormatter/issues");
        }

        /// <summary>
        /// The close event of the form
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The arguments from the sender</param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Properties.Settings.Default.AskBeforeClosing)
            {
                DialogResult result = MessageBox.Show(
                    "Do you really want to close this application?",
                    "Close the application",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }

            }
        }

        /// <summary>
        /// Hide the application if the setting to minimize it is set
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The arguments from the sender</param>
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized && Properties.Settings.Default.MinimizeToTray)
            {
                HideToTray();
            }
        }

        /// <summary>
        /// Hide the application to the tray icon
        /// </summary>
        private void HideToTray()
        {
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
            NI_Notification.Visible = true;
            if (Properties.Settings.Default.FirstTimeTray)
            {
                NI_Notification.ShowBalloonTip(
                    5000,
                    "Application in tray",
                    "The application got minimized to the an tray icon, click it to reopen",
                    ToolTipIcon.Info
                );

                Properties.Settings.Default.FirstTimeTray = false;
                Properties.Settings.Default.Save();
            }
            TopMost = true;
        }

        /// <summary>
        /// Clicking the notification Icon
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The arguments sendet by the event caller</param>
        private void NI_Notification_Click(object sender, EventArgs e)
        {
            ShowInTaskbar = true;
            NI_Notification.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        /// <summary>
        /// The click on the hide button
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The arguments provided by the sender</param>
        private void MI_HideToTray_Click(object sender, EventArgs e)
        {
            HideToTray();
        }

        /// <summary>
        /// Event for clicking the settings button
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The arguments provided by the sender</param>
        private void MI_Settings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }
    }
}
