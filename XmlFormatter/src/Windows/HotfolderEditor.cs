using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XmlFormatter.src.Enums;
using XmlFormatter.src.Hotfolder;
using XmlFormatter.src.Interfaces.Formatter;
using XmlFormatter.src.Interfaces.Hotfolder;

namespace XmlFormatter.src.Windows
{
    public partial class HotfolderEditor : Form
    {
        private bool editMode;

        private bool saved;
        public bool Saved => saved;

        private Dictionary<string, Type> formatters;

        private IHotfolder hotfolder;
        public IHotfolder Hotfolder => hotfolder;

        public HotfolderEditor(IHotfolder hotfolder)
        {
            editMode = hotfolder == null ? false : true;
            formatters = new Dictionary<string, Type>();
           
            InitializeComponent();

            CB_Formatter.Enabled = !editMode;
            TB_Filter.Text = "*.*";
            TB_OutputFileScheme.Text = "{inputfile}_{format}.{extension}";
            this.hotfolder = hotfolder;
        }


        private void HotfolderEditor_Load(object sender, EventArgs e)
        {
            Type type = typeof(IFormatter);
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
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

            CB_Formatter.SelectedIndex = 0;
            CB_Mode.SelectedIndex = 0;
        }

        private void B_OpenWatchFolder_Click(object sender, EventArgs e)
        {
            GetFolderPath(TB_WatchedFolder);
            if (TB_OutputFolder.Text == String.Empty)
            {
                TB_OutputFolder.Text = TB_WatchedFolder.Text + "\\output";
            }
        }

        private void B_OutputFolder_Click(object sender, EventArgs e)
        {
            GetFolderPath(TB_OutputFolder);
        }

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
            Type formatterType = formatters[CB_Formatter.SelectedItem.ToString()];
            IFormatter formatter = (IFormatter)Activator.CreateInstance(formatterType);
            hotfolder = hotfolder ?? new HotfolderContainer(formatter, TB_WatchedFolder.Text);
            hotfolder.Mode = (ModesEnum)Enum.Parse(typeof(ModesEnum), CB_Mode.SelectedItem.ToString());
            hotfolder.Filter = TB_Filter.Text;
            hotfolder.OutputFolder = TB_OutputFolder.Text;
            hotfolder.OnRename = CB_OnRename.Checked;
            hotfolder.RemoveOld = CB_RemoveOld.Checked;

            saved = true;
            B_Cancel.PerformClick();
        }

        private void B_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
