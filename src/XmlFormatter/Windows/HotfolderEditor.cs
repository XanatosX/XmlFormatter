using PluginFramework.DataContainer;
using PluginFramework.Enums;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using XmlFormatter.DataContainer;
using XMLFormatterModel.Hotfolder;

namespace XmlFormatter.Windows
{
    /// <summary>
    /// This Window is used to create or edit a hotfolder configuration
    /// </summary>
    public partial class HotfolderEditor : Form
    {
        /// <summary>
        /// Is the editor in the hotfolder edit mode
        /// </summary>
        private readonly bool editMode;

        /// <summary>
        /// Readonly access if something was saved
        /// </summary>
        public bool Saved { get; private set; }

        /// <summary>
        /// Readonly access to the hotfolder configuration
        /// </summary>
        public IHotfolder Hotfolder { get; private set; }

        /// <summary>
        /// The plugin manager to use
        /// </summary>
        private readonly IPluginManager PluginManager;

        /// <summary>
        /// Create a new instance of the editor
        /// </summary>
        /// <param name="hotfolder">The hotfolder configuration to use for editing</param>
        public HotfolderEditor(IPluginManager pluginManager) : this(null, pluginManager)
        {

        }

        /// <summary>
        /// Create a new instance of the editor
        /// </summary>
        /// <param name="hotfolder">The hotfolder configuration to use for editing</param>
        public HotfolderEditor(IHotfolder hotfolder, IPluginManager pluginManager)
        {
            editMode = hotfolder != null;

            InitializeComponent();

            CB_Formatter.Enabled = !editMode;
            Hotfolder = hotfolder;
            PluginManager = pluginManager;
        }


        /// <summary>
        /// Loading the window
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void HotfolderEditor_Load(object sender, EventArgs e)
        {
            List<PluginMetaData> pluginsMetaData = PluginManager.ListPlugins<IFormatter>().ToList();

            foreach (PluginMetaData metaData in pluginsMetaData)
            {
                CB_Formatter.Items.Add(new ComboboxPluginItem(metaData));
            }

            foreach (ModesEnum mode in Enum.GetValues(typeof(ModesEnum)))
            {
                CB_Mode.Items.Add(mode.ToString());
            }

            FillSettingWindow();

        }

        /// <summary>
        /// Fill in the data of the hotfolder configuration
        /// </summary>
        private void FillSettingWindow()
        {
            TB_Filter.Text = "*.*";
            TB_OutputFileScheme.Text = "{inputfile}_{format}.{extension}";

            CB_Formatter.SelectedIndex = 0;
            CB_Mode.SelectedIndex = 0;
            if (editMode)
            {
                bool foundEntry = false;
                for (int i = 0; i < CB_Formatter.Items.Count; i++)
                {
                    if (CB_Formatter.Items[i] is ComboboxPluginItem item)
                    {
                        if (item.Type.FullName == Hotfolder.FormatterToUse.GetType().FullName)
                        {
                            CB_Formatter.SelectedIndex = i;
                            foundEntry = true;
                            break;
                        }
                    }

                }

                for (int i = 0; i < CB_Mode.Items.Count; i++)
                {
                    string item = CB_Mode.Items[i].ToString();
                    if (item == Hotfolder.Mode.ToString())
                    {
                        CB_Mode.SelectedIndex = i;
                        break;
                    }
                }

                TB_WatchedFolder.Text = Hotfolder.WatchedFolder;
                TB_Filter.Text = Hotfolder.Filter;
                TB_OutputFolder.Text = Hotfolder.OutputFolder;
                TB_OutputFileScheme.Text = Hotfolder.OutputFileScheme;

                CB_OnRename.Checked = Hotfolder.OnRename;
                CB_RemoveOld.Checked = Hotfolder.RemoveOld;

                if (!foundEntry)
                {
                    CB_Formatter.Enabled = true;
                    MessageBox.Show(
                        "Missing type " + Hotfolder.FormatterToUse.GetType().FullName + " did you delete the plugin?",
                        "Missing formatter",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                        );
                }
            }
        }

        /// <summary>
        /// Folder selector if we want to set a folder to watch
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void B_OpenWatchFolder_Click(object sender, EventArgs e)
        {
            GetFolderPath(TB_WatchedFolder);
            if (TB_OutputFolder.Text == String.Empty)
            {
                TB_OutputFolder.Text = TB_WatchedFolder.Text + "\\output";
            }
        }

        /// <summary>
        /// Folder selecton if we want to set the folder for output
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void B_OutputFolder_Click(object sender, EventArgs e)
        {
            GetFolderPath(TB_OutputFolder);
        }

        /// <summary>
        /// Get the folder path from a folder selector
        /// </summary>
        /// <param name="targetBox">The text box to write the result into</param>
        private void GetFolderPath(TextBox targetBox)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            DialogResult result = folderBrowser.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            targetBox.Text = folderBrowser.SelectedPath;
        }

        /// <summary>
        /// Save the current configuration as hotfolder
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void B_Save_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(TB_WatchedFolder.Text))
            {
                return;
            }
            if (TB_Filter.Text == String.Empty)
            {
                return;
            }
            if (TB_OutputFolder.Text == String.Empty)
            {
                return;
            }
            if (TB_WatchedFolder.Text == TB_OutputFolder.Text)
            {
                MessageBox.Show(
                    "Watched folder and output folder must differ!",
                    "Same folder selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }
            if (CB_Formatter.SelectedItem is ComboboxPluginItem selectedItem)
            {
                IFormatter plugin = PluginManager.LoadPlugin<IFormatter>(selectedItem.Id);
                Hotfolder = new HotfolderContainer(plugin, TB_WatchedFolder.Text)
                {
                    Mode = (ModesEnum)Enum.Parse(typeof(ModesEnum), CB_Mode.SelectedItem.ToString()),
                    Filter = TB_Filter.Text,
                    OutputFolder = TB_OutputFolder.Text,
                    OnRename = CB_OnRename.Checked,
                    RemoveOld = CB_RemoveOld.Checked
                };
            }

            Saved = true;
            B_Cancel.PerformClick();
        }

        /// <summary>
        /// Close this window without saving anything
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void B_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
