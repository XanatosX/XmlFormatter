using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using XmlFormatter.src.Enums;
using XmlFormatter.src.Hotfolder;
using XmlFormatter.src.Interfaces.Formatter;
using XmlFormatter.src.Interfaces.Hotfolder;

namespace XmlFormatter.src.Windows
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
        /// Was something saved
        /// </summary>
        private bool saved;

        /// <summary>
        /// Readonly access if something was saved
        /// </summary>
        public bool Saved => saved;

        /// <summary>
        /// A lookup for the formatters
        /// </summary>
        private readonly Dictionary<string, Type> formatters;

        /// <summary>
        /// The current hotfolder configuration
        /// </summary>
        private IHotfolder hotfolder;

        /// <summary>
        /// Readonly access to the hotfolder configuration
        /// </summary>
        public IHotfolder Hotfolder => hotfolder;

        /// <summary>
        /// Create a new instance of the editor
        /// </summary>
        /// <param name="hotfolder">The hotfolder configuration to use for editing</param>
        public HotfolderEditor(IHotfolder hotfolder)
        {
            editMode = hotfolder != null;
            formatters = new Dictionary<string, Type>();
           
            InitializeComponent();

            CB_Formatter.Enabled = !editMode;
            this.hotfolder = hotfolder;
        }


        /// <summary>
        /// Loading the window
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void HotfolderEditor_Load(object sender, EventArgs e)
        {
            Type type = typeof(IFormatter);
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            var formatterTypes = types.Where(currentType => currentType.GetInterfaces().Contains(type));

            foreach (Type currentType in formatterTypes)
            {
                formatters.Add(currentType.Name, currentType);
                CB_Formatter.Items.Add(currentType.Name);
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
                for (int i = 0; i < CB_Formatter.Items.Count; i++)
                {
                    string item = CB_Formatter.Items[i].ToString();
                    if (item == hotfolder.FormatterToUse.GetType().Name)
                    {
                        CB_Formatter.SelectedIndex = i;
                        break;
                    }
                }

                for (int i = 0; i < CB_Mode.Items.Count; i++)
                {
                    string item = CB_Mode.Items[i].ToString();
                    if (item == hotfolder.Mode.ToString())
                    {
                        CB_Mode.SelectedIndex = i;
                        break;
                    }
                }

                TB_WatchedFolder.Text = hotfolder.WatchedFolder;
                TB_Filter.Text = hotfolder.Filter;
                TB_OutputFolder.Text = hotfolder.OutputFolder;
                TB_OutputFileScheme.Text = hotfolder.OutputFileScheme;

                CB_OnRename.Checked = hotfolder.OnRename;
                CB_RemoveOld.Checked = hotfolder.RemoveOld;
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
            Type formatterType = formatters[CB_Formatter.SelectedItem.ToString()];
            IFormatter formatter = (IFormatter)Activator.CreateInstance(formatterType);
            hotfolder = new HotfolderContainer(formatter, TB_WatchedFolder.Text)
            {
                Mode = (ModesEnum)Enum.Parse(typeof(ModesEnum), CB_Mode.SelectedItem.ToString()),
                Filter = TB_Filter.Text,
                OutputFolder = TB_OutputFolder.Text,
                OnRename = CB_OnRename.Checked,
                RemoveOld = CB_RemoveOld.Checked
            };

            saved = true;
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
