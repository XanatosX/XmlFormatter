using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace XmlFormatter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

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
            saveFile.Filter = "XML files (*.xml)|*.xml";
            DialogResult result = saveFile.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }

            XElement fileToConvert = XElement.Load(TB_SelectedXml.Text);
            fileToConvert.Save(saveFile.FileName, SaveOptions.None);
        }
    }
}
