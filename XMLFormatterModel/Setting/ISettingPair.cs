using System;

namespace XmlFormatterModel.Setting
{
    /// <summary>
    /// This interface is a single setting
    /// </summary>
    public interface ISettingPair
    {
        /// <summary>
        /// The key or name of the setting
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The data type of the stored value
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// The setting value
        /// </summary>
        object Value { get; }

        /// <summary>
        /// Get the value in a specific format from the setting container
        /// </summary>
        /// <typeparam name="T">The type the setting should be casted to</typeparam>
        /// <returns></returns>
        T GetValue<T>();

        /// <summary>
        /// Set or change the value of this setting
        /// </summary>
        /// <typeparam name="T">The type of the data to add</typeparam>
        /// <param name="value">The value of the dataset</param>
        void SetValue<T>(T value);
    }
}