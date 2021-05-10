using PluginFramework.Enums;

namespace XmlFormatterOsIndependent.MVVM.Models
{
    /// <summary>
    /// Model used for mode selection dropdown
    /// </summary>
    public class ModeSelection
    {
        /// <summary>
        /// The name to display on the combo box
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// The real value used for conversion
        /// </summary>
        public ModesEnum Value { get; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="displayName">The name showen on the combobox</param>
        /// <param name="value">The value for the selection</param>
        public ModeSelection(string displayName, ModesEnum value)
        {
            DisplayName = displayName;
            Value = value;
        }
    }
}
