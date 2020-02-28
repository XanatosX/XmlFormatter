﻿using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading.Tasks;
using XmlFormatter.src.Manager;
using XmlFormatter.src.DataContainer;
using XmlFormatter.src.Settings;
using XmlFormatter.src.Settings.DataStructure;
using XmlFormatter.src.Settings.Adapter;
using XmlFormatter.src.Settings.Provider.Factories;
using XmlFormatter.src.Interfaces.Settings;
using XmlFormatter.src.Interfaces.Settings.DataStructure;
using XmlFormatter.src.Formatter;
using XmlFormatter.src.Enums;
using XmlFormatter.src.Interfaces.Formatter;
using XmlFormatter.src.EventMessages;
using XmlFormatter.src.Hotfolder;
using XmlFormatter.src.Interfaces.Hotfolder;
using XmlFormatter.src.Settings.Hotfolder;

namespace XmlFormatter.src.Windows
{
    /// <summary>
    /// This class is representing the main form of the application
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// The default status to display at the end of the main window
        /// </summary>
        private readonly string defaultStatus;

        /// <summary>
        /// Path to the folder to save settings in
        /// </summary>
        private readonly string settingPath;

        /// <summary>
        /// Path to the default settings file
        /// </summary>
        private readonly string settingFile;

        /// <summary>
        /// Instance for managing the settings
        /// </summary>
        private readonly ISettingsManager settingManager;

        /// <summary>
        /// The formatter to use
        /// </summary>
        private IFormatter formatterToUse;

        private IHotfolderManager hotfolderManager;

        /// <summary>
        /// New delegate to set a label text from another thread
        /// </summary>
        /// <param name="label">The label to change</param>
        /// <param name="newText">The new text to use</param>
        private delegate void UpdateThreadSafeConvertStatus(
            Label label,
            string newText
        );

        /// <summary>
        /// Constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            defaultStatus = "Status: ";
            VersionManager versionManager = new VersionManager();
            string currentVersion = versionManager.GetStringVersion(versionManager.GetApplicationVersion());
            Properties.Settings.Default.ApplicationVersion = currentVersion;

            settingPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + "XmlFormatter\\";
            settingFile = settingPath + "settings.set";

            settingManager = new SettingsManager();
            settingManager.SetPersistendFactory(new XmlProviderFactory());
            settingManager.Load(settingFile);

            ISettingScope resourceScope = settingManager.GetScope("Default");
            if (resourceScope == null)
            {
                resourceScope = new PropertyAdapter();
                settingManager.AddScope(resourceScope);
            }

            foreach (SettingPair settingPair in resourceScope.GetSettings())
            {
                Properties.Settings.Default[settingPair.Name] = settingPair.Value;
            }

            settingManager.Save(settingFile);
            
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

            if (Properties.Settings.Default.SearchUpdateOnStartup)
            {
                CheckForUpdatedVersion(true);
            }

            SetFormatter(new XmlFormatterProvider());
            SetupHotFolder();
        }

        private void SetupHotFolder()
        {
            if (!Properties.Settings.Default.HotfolderActive)
            {
                if (hotfolderManager != null)
                {
                    hotfolderManager.ResetManager();
                    hotfolderManager = null;
                }
                
                return;
            }
            hotfolderManager = hotfolderManager ?? new HotfolderManager();
            hotfolderManager.ResetManager();

            HotfolderExtension hotfolderExtension = new HotfolderExtension(settingManager);
            foreach (IHotfolder hotfolder in hotfolderExtension.GetHotFoldersFromSettings())
            { 
                hotfolderManager.AddHotfolder(hotfolder);
            }
        }

        /// <summary>
        /// Set the formatter for the main application
        /// </summary>
        /// <param name="formatter">The new formatter to use</param>
        private void SetFormatter(IFormatter formatter)
        {
            if (formatterToUse != null)
            {
                formatterToUse.StatusChanged -= FormatterToUse_StatusChanged;    
            }
            formatterToUse = formatter;
            formatterToUse.StatusChanged += FormatterToUse_StatusChanged;
        }

        /// <summary>
        /// Status of the formatting did change
        /// </summary>
        /// <param name="sender">The sender of the message</param>
        /// <param name="e">The new status</param>
        private void FormatterToUse_StatusChanged(object sender, BaseEventArgs e)
        {
            UpdateLabelTextThreadSafe(L_Status, e.Message);
        }

        /// <summary>
        /// Update the text of a given label thread safe
        /// </summary>
        /// <param name="label">The label to update</param>
        /// <param name="newText">The text to set the label text to</param>
        public void UpdateLabelTextThreadSafe(Label label, string newText)
        {
            string textToUse = defaultStatus + newText;
            if (label.InvokeRequired)
            {
                label.Invoke(new UpdateThreadSafeConvertStatus(UpdateLabelTextThreadSafe), new object[] { label, textToUse });
                return;
            }

            label.Text = textToUse;
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
            ModesEnum currentEnum = formatted ? ModesEnum.Formatted : ModesEnum.Flat;

            bool success = formatterToUse.ConvertToFormat(inputFilePath, outputFilePath, currentEnum);
             
            SwitchFormMode(true);
            return success;
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
        private void MI_CheckForUpdate_Click(object sender, EventArgs e)
        {
            CheckForUpdatedVersion();
        }

        /// <summary>
        /// This method will check if there is an updated version available and show an text box if there is something new.
        /// </summary>
        private async void CheckForUpdatedVersion()
        {
            CheckForUpdatedVersion(false);
        }

        /// <summary>
        /// This method will check if there is an updated version available and shouw you the information as text box
        /// </summary>
        /// <param name="onlyShowNewBox">If this is active there will be no text box if the version is up to date</param>
        private async void CheckForUpdatedVersion(bool onlyShowNewBox)
        {
            VersionManager manager = new VersionManager();
            manager.Error += Manager_Error;
            VersionCompare versionCompare = await manager.GitHubVersionIsNewer();

            bool forceShow = false;

            string text = "Your version is up to date";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            if (versionCompare.GitHubIsNewer)
            {
                text = "There is a newer version available";

                text += "\n\nYour version: " + manager.GetStringVersion(versionCompare.LocalVersion);
                text += "\nGitHub version: " + manager.GetStringVersion(versionCompare.GitHubVersion);

                text += "\n\nDo you want to upgrade now?";
                buttons = MessageBoxButtons.YesNo;
                forceShow = true;
            }

            if (forceShow || !onlyShowNewBox)
            {
                DialogResult result = MessageBox.Show(text, "Version status", buttons, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    Process.Start("https://github.com/XanatosX/XmlFormatter/releases/tag/" + versionCompare.LatestRelease.TagName);
                }
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
                    "The application got minimized to the tray icon, click on it to reopen",
                    ToolTipIcon.Info
                );

                Properties.Settings.Default.FirstTimeTray = false;
                settingManager.Save(settingFile);
            }
            TopMost = true;
            TopMost = false;
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
            Settings settings = new Settings(settingManager, settingFile);
            settings.ShowDialog();
            SetupHotFolder();
        }
    }
}
