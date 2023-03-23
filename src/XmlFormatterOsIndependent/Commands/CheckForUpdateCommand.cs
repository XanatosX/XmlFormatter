using PluginFramework.DataContainer;
using System;
using System.Threading.Tasks;
using XmlFormatterModel.Update;
using XmlFormatterOsIndependent.Factories;

namespace XmlFormatterOsIndependent.Commands
{
    /// <summary>
    /// This command will check if there is a update available
    /// </summary>
    class CheckForUpdateCommand : BaseDataCommand
    {
        /// <summary>
        /// The version compare data to use
        /// </summary>
        private VersionCompare data;

        /// <inheritdoc/>
        public async override Task AsyncExecute(object parameter)
        {
            DefaultManagerFactory factory = new DefaultManagerFactory();
            IVersionManager versionManager = factory.GetVersionManager();

            data = await versionManager.RemoteVersionIsNewerAsync();
            ExecutionDone();
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            AsyncExecute();
        }

        /// <inheritdoc/>
        public override T GetData<T>()
        {
            Type type = typeof(T);
            return type == typeof(VersionCompare) ? (T)Convert.ChangeType(data, typeof(T)) : default;
        }

        /// <inheritdoc/>
        public override bool IsExecuted()
        {
            return data != null;
        }
    }
}
