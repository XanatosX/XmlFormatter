using PluginFramework.Enums;
using System;
using System.Collections.Generic;
using System.Text;

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
