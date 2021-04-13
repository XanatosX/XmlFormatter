using System;

namespace XmlFormatterModel.Setting
{
    /// <inheritdoc/>
    public class SettingPair : ISettingPair
    {
        /// <summary>
        /// The key/name of this setting pair
        /// </summary>
        private readonly string name;

        /// <inheritdoc/>
        public string Name => name;

        /// <summary>
        /// The type of the value stored in this key value pair
        /// </summary>
        private Type type;

        /// <inheritdoc/>
        public Type Type => type;

        /// <summary>
        /// The value object of this setting pair
        /// </summary>
        private object value;

        /// <inheritdoc/>
        public object Value => value;

        /// <summary>
        /// Create a new setting pair
        /// </summary>
        /// <param name="name">The key/name of the new setting</param>
        public SettingPair(string name)
        {
            this.name = name;
        }

        /// <inheritdoc/>
        public void SetValue<T>(T value)
        {
            this.value = value;
            type = value.GetType();
        }

        /// <inheritdoc/>
        public T GetValue<T>()
        {
            try
            {
                T returnValue = (T)value;
                return returnValue;
            }
            catch (Exception)
            {
                //The value is not set yet or something went wrong by setting it
            }

            return default(T);
        }
    }
}
