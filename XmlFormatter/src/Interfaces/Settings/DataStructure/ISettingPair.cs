using System;

namespace XmlFormatter.src.Interfaces.Settings.DataStructure
{
    public interface ISettingPair
    {
        string Name { get; }
        Type Type { get; }
        object Value { get; }

        T GetValue<T>();
        void SetValue<T>(T value);
    }
}