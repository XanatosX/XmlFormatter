using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Octokit;
using System.Diagnostics;
using System.Threading.Tasks;

namespace XmlFormatter
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

        /// <summary>
        /// This method is the click event for the about menu
        /// </summary>
        /// <param name="sender">The control sending the click event</param>
        /// <param name="e">The event arguments</param>
        private void MI_About_Click(object sender, EventArgs e)
        {
            Version version = GetApplicationVersion();
            MessageBox.Show(GetStringVersion(version), "Version", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// This method will return you the current version of the application
        /// </summary>
        /// <returns>The current version of the application</returns>
        private Version GetApplicationVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string versionString = "0.0.0";
            using (Stream stream = assembly.GetManifestResourceStream("XmlFormatter.Version.txt"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    versionString = reader.ReadToEnd();
                }
            }

            return ConvertInnerFormatToProperVersion(versionString);
        }

        /// <summary>
        /// This method will convert a inner string version to a proper version instance
        /// </summary>
        /// <param name="stringVersion">The string version to use for converting</param>
        /// <returns>A proper version class</returns>
        private Version ConvertInnerFormatToProperVersion(string stringVersion)
        {
            stringVersion += ".0";
            Regex regex = new Regex(@"[0-9]{1,}.[0-9]{1,}.[0-9]{1,}.[0-9]{1,}");
            if (!regex.IsMatch(stringVersion))
            {
                stringVersion = "0.0.0.0";
            }
            Version version = new Version(stringVersion);

            return version;
        }

        /// <summary>
        /// This method will return you the string of a given version
        /// </summary>
        /// <param name="version">The version to get the string for</param>
        /// <returns>A string representatiion of the given version</returns>
        private string GetStringVersion(Version version)
        {
            return version.Major + "." + version.Minor + "." + version.Build;
        }


        /// <summary>
        /// This method will check if there is newer version for download
        /// </summary>
        /// <param name="sender">The control triggering the event</param>
        /// <param name="e">The event data</param>
        private async void MI_CheckForUpdate_Click(object sender, EventArgs e)
        {
            GitHubClient client = new GitHubClient(new ProductHeaderValue("XanatosX"));
            var releases = await client.Repository.Release.GetAll("XanatosX", "XmlFormatter");
            if (releases.Count == 0)
            {
                MessageBox.Show("Could't not find a version onn GitHub", "No version found");
            }
            Release latestRelease = releases[0];
            string latest = latestRelease.Name;
            Regex regex = new Regex(@"([0-9]{1,}.[0-9]{1,}.[0-9]{1,})");
            Version gitHubVersion = null;
            foreach (Match match in regex.Matches(latest))
            {
                gitHubVersion = ConvertInnerFormatToProperVersion(match.Value);
            }

            int compareResult = GetApplicationVersion().CompareTo(gitHubVersion);
            string text = "Your version is up to date";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            if (compareResult < 0)
            {
                text = "There is a newer version available";

                text += "\n\nYour version: " + GetStringVersion(GetApplicationVersion());
                text += "\nGitHub version: " + GetStringVersion(gitHubVersion);

                text += "\n\nDo you want to upgrade now?";
                buttons = MessageBoxButtons.YesNo;
            }

            DialogResult result = MessageBox.Show(text, "Version status", buttons, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                Process.Start("https://github.com/XanatosX/XmlFormatter/releases/tag/" + latestRelease.TagName);
            }
        }

        private void MI_ReportIssue_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/XanatosX/XmlFormatter/issues");
        }
    }
}
