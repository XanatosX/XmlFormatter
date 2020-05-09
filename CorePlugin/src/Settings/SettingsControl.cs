using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PluginFramework.src.DataContainer;

namespace CorePlugin.src.Settings
{
    public partial class SettingsControl : UserControl
    {
        private PluginSettings settings;

        public SettingsControl()
        {
            InitializeComponent();
        }

        public void SetSettings(PluginSettings settings)
        {
            this.settings = settings;
            checkBox1.Checked = settings.GetValue<bool>("test2");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (settings == null)
            {
                return;
            }
            settings.AddValue("test2", checkBox1.Checked, true);
        }
    }
}
