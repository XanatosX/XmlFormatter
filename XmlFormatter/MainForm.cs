using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace XmlFormatter
{
    /// <summary>
    /// This class is representing the main form of the application
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
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

            if (CB_Mode.SelectedIndex == 0)
            {
                SaveFormatted(saveFile.FileName);
                return;
            }
            SaveFlat(saveFile.FileName);

        }

        private void SaveFormatted(string outputPath)
        {
            XElement fileToConvert = XElement.Load(TB_SelectedXml.Text);
            fileToConvert.Save(outputPath, SaveOptions.None);
        }

        private void SaveFlat(string outputPath)
        {
            XElement fileToConvert = XElement.Load(TB_SelectedXml.Text);
            fileToConvert.Save(outputPath, SaveOptions.DisableFormatting);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CB_Mode.SelectedIndex = 0;
        }
    }
}
