using PluginFramework.DataContainer;
using PluginFramework.Enums;
using PluginFramework.EventMessages;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using PluginFramework.LoadStrategies;
using PluginFramework.Manager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using XmlFormatter.DataContainer;
using XmlFormatter.Settings.Adapter;
using XmlFormatter.Update;
using XmlFormatterModel.Enums;
using XmlFormatterModel.Hotfolder;
using XmlFormatterModel.Logging;
using XmlFormatterModel.Logging.Strategy;
using XmlFormatterModel.Logging.Strategy.Format;
using XmlFormatterModel.Setting;
using XmlFormatterModel.Setting.Hotfolder;
using XmlFormatterModel.Update;
using XMLFormatterModel.Hotfolder;
using XMLFormatterModel.Setting.InputOutput;

namespace XmlFormatter.Windows
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
        /// Instance of the update manager
        /// </summary>
        private readonly IUpdater updateManager;

        /// <summary>
        /// Instance to manage plugins
        /// </summary>
        private readonly IPluginManager pluginManager;

        /// <summary>
        /// The formatter to use
        /// </summary>
        private IFormatter formatterToUse;

        /// <summary>
        /// Instance of the hotfolder manager
        /// </summary>
        private IHotfolderManager hotfolderManager;

        /// <summary>
        /// Instance of the logging manager to use
        /// </summary>
        private ILoggingManager loggingManager;

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
            IVersionManagerFactory factory = new VersionManagerFactory();
            IVersionManager versionManager = factory.GetVersionManager();
            TaskAwaiter<Version> currentVersion = versionManager.GetLocalVersionAsync().GetAwaiter();
            currentVersion.OnCompleted(() =>
            {
                string stringVersion = versionManager.GetStringVersion(currentVersion.GetResult());
                Properties.Settings.Default.ApplicationVersion = stringVersion;
                settingManager.Load(settingFile);
                settingManager.Save(settingFile);
            });

            NI_Notification.Text = this.Text;

            settingPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + "XmlFormatter\\";
            settingFile = settingPath + "settings.set";
            Properties.Settings.Default.LoggingFolder = settingPath + "logs\\";
            if (!Directory.Exists(Properties.Settings.Default.LoggingFolder))
            {
                Directory.CreateDirectory(Properties.Settings.Default.LoggingFolder);
            }

            settingManager = new SettingsManager();
            settingManager.SetPersistendFactory(new XmlProviderFactory());
            settingManager.Load(settingFile);

            SetupLogging();

            ISettingScope resourceScope = settingManager.GetScope("Default");
            if (resourceScope == null)
            {
                resourceScope = new PropertyAdapter();
                settingManager.AddScope(resourceScope);
            }

            foreach (SettingPair settingPair in resourceScope.GetSettings())
            {
                try
                {
                    Properties.Settings.Default[settingPair.Name] = settingPair.Value;
                }
                catch (Exception)
                {
                    // Prevent crash even if shit happens
                }

            }

            pluginManager = new DefaultManager();
            pluginManager.SetDefaultLoadStrategy(new PluginFolder(AppDomain.CurrentDomain.BaseDirectory + "\\Plugins"));

            settingManager.Save(settingFile);
            updateManager = new UpdateManager();
            SetUpdateStrategy();
        }

        /// <summary>
        /// Setup the logging manager
        /// </summary>
        private void SetupLogging()
        {
            if (loggingManager != null && !Properties.Settings.Default.LoggingEnabled)
            {
                loggingManager.Dispose();
                loggingManager = null;
            }
            if (loggingManager == null && Properties.Settings.Default.LoggingEnabled)
            {
                loggingManager = new LoggingManager();
                string timeStamp = DateTime.Now.ToString("yyyyMMdd");
                string logFile = Properties.Settings.Default.LoggingFolder + timeStamp + "_hotfolder.log";
                ILogger hotfolderLogger = new Logger(
                    new SimpleFileLogStrategy(logFile, true),
                    new SimpleFileFormatStrategy(50)
                );
                hotfolderLogger.AddScope(LogScopesEnum.Hotfolder);
                loggingManager.AddLogger(hotfolderLogger);
            }
        }

        /// <summary>
        /// Set the strategy to use for updating
        /// </summary>
        private void SetUpdateStrategy()
        {
            IUpdateStrategy updateStrategy = null;
            try
            {
                string type = Properties.Settings.Default.UpdateStrategy;
                updateStrategy = pluginManager.LoadPlugin<IUpdateStrategy>(type);
            }
            catch (Exception)
            {
            }
            updateManager.SetStrategy(updateStrategy);
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

            SetupHotFolder();
            SetupFormatterSelection();
        }

        /// <summary>
        /// Setup the formatter selection combobox
        /// </summary>
        private void SetupFormatterSelection()
        {
            CB_Formatter.Items.Clear();
            List<PluginMetaData> formatters = pluginManager.ListPlugins<IFormatter>().ToList();
            foreach (PluginMetaData metaData in formatters)
            {
                CB_Formatter.Items.Add(new ComboboxPluginItem(metaData));
            }
            if (CB_Formatter.Items.Count > 0)
            {
                CB_Formatter.SelectedIndex = 0;
                ComboboxPluginItem item = CB_Formatter.SelectedItem as ComboboxPluginItem;
                SetFormatter(pluginManager.LoadPlugin<IFormatter>(item.Id));
            }
            if (CB_Formatter.Items.Count == 1)
            {
                CB_Formatter.Visible = false;
            }
        }

        /// <summary>
        /// Setup the hot folder
        /// </summary>
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
            if (hotfolderManager is ILoggable loggable)
            {
                loggable.SetLoggingManager(loggingManager);
            }
            hotfolderManager.ResetManager();

            HotfolderExtension hotfolderExtension = new HotfolderExtension(settingManager, pluginManager);
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
            L_SelectedPath.Text = "Selected " + formatterToUse.Extension.ToUpper() + "-file path";
            B_Select.Text = "Select " + formatterToUse.Extension.ToUpper();
        }

        /// <summary>
        /// Status of the formatting did change
        /// </summary>
        /// <param name="sender">The sender of the message</param>
        /// <param name="e">The new status</param>
        private void FormatterToUse_StatusChanged(object sender, PluginFramework.EventMessages.BaseEventArgs e)
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
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = formatterToUse.Extension.ToUpper() + " files (*." + formatterToUse.Extension + ")|*." + formatterToUse.Extension
            };
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

            saveFile.Filter = formatterToUse.Extension.ToUpper() + " files (*." + formatterToUse.Extension + ")|*." + formatterToUse.Extension;
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
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && fi.Extension.ToLower() == "." + formatterToUse.Extension)
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
            VersionInformation versionInformation = new VersionInformation();
            versionInformation.ShowDialog();
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
            IVersionManagerFactory factory = new VersionManagerFactory();
            IVersionManager manager = factory.GetVersionManager();
            manager.Error += Manager_Error;
            VersionCompare versionCompare = await manager.RemoteVersionIsNewerAsync();

            bool forceShow = false;

            string text = "Your version is up to date";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            if (versionCompare.GitHubIsNewer)
            {
                text = "There is a newer version available";

                text += "\n\nYour version: " + manager.GetStringVersion(versionCompare.LocalVersion);
                text += "\nGitHub version: " + manager.GetStringVersion(versionCompare.GitHubVersion);

                if (updateManager.IsStrategySet)
                {
                    text += "\n\nDo you want to upgrade now?";
                    buttons = MessageBoxButtons.YesNo;
                    forceShow = true;
                }
            }

            if (forceShow || !onlyShowNewBox)
            {
                DialogResult result = MessageBox.Show(text, "Version status", buttons, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    if (!updateManager.UpdateApplication(versionCompare, (asset) => asset.Name.Contains("WinForms")))
                    {
                        //@TODO: Show some kind of error message
                    }
                }
            }
        }

        /// <summary>
        /// Manage the error throwen by the version manager
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The arguments provided by the sender</param>
        private void Manager_Error(object sender, BaseEventArgs e)
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
            Settings settings = new Settings(settingManager, settingFile, pluginManager);
            settings.ShowDialog();
            SetupLogging();
            SetupHotFolder();
            SetUpdateStrategy();
        }

        /// <summary>
        /// Selected formatter did change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CB_Formatter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox box && box.SelectedItem is ComboboxPluginItem item)
            {
                IFormatter formatter = pluginManager.LoadPlugin<IFormatter>(item.Id);
                SetFormatter(formatter);
                TB_SelectedXml.Text = string.Empty;
            }
        }

        /// <summary>
        /// Event if plugin dialog is clicked
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void pluginsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PluginManager pluginManagerWindow = new PluginManager(pluginManager, settingManager, settingFile);
            pluginManagerWindow.ShowDialog();

        }
    }
}
