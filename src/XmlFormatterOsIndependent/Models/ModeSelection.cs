using PluginFramework.Enums;

namespace XmlFormatterOsIndependent.Models
{
    public class ModeSelection
    {
        public string DisplayName { get; }
        public ModesEnum Value { get; }

        public ModeSelection(string displayName, ModesEnum value)
        {
            DisplayName = displayName;
            Value = value;
        }
    }
}
