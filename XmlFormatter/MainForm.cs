using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Reflection;

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
        /// Loading the form event
        /// </summary>
        /// <param name="sender">The control triggering the method</param>
        /// <param name="e">The event arguments</param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            CB_Mode.SelectedIndex = 0;
            AllowDrop = true;
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

        /// <summary>
        /// This method will save the file mentioned in the text box to the given output path as well formatted
        /// </summary>
        /// <param name="outputPath">The path to save the output file in</param>
        private void SaveFormatted(string outputPath)
        {
            XElement fileToConvert = XElement.Load(TB_SelectedXml.Text);
            fileToConvert.Save(outputPath, SaveOptions.None);
        }

        /// <summary>
        /// This method will save the file mentioned in the text box to the given output path as flat formatted
        /// </summary>
        /// <param name="outputPath">The path to save the output file in</param>
        private void SaveFlat(string outputPath)
        {
            XElement fileToConvert = XElement.Load(TB_SelectedXml.Text);
            fileToConvert.Save(outputPath, SaveOptions.DisableFormatting);
        }

        /// <summary>
        /// This method is the click event for the help menu
        /// </summary>
        /// <param name="sender">The control sending the click event</param>
        /// <param name="e">The event arguments</param>
        private void MI_Help_Click(object sender, EventArgs e)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("XmlFormatter.Version.txt"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    MessageBox.Show(reader.ReadToEnd(), "Version", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
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
        /// 
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
    }
}
