using PluginFramework.DataContainer;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XmlFormatterModel.Update;
using XmlFormatterOsIndependent.Factories;
using XmlFormatterOsIndependent.Update;

namespace XmlFormatterOsIndependent.Commands
{
    class CheckForUpdateCommand : BaseDataCommand
    {
        private VersionCompare data;

        public async override Task AsyncExecute(object parameter)
        {
            DefaultManagerFactory factory = new DefaultManagerFactory();
            IVersionManager versionManager = factory.GetVersionManager();

            data = await versionManager.RemoteVersionIsNewer();
            ExecutionDone();
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            AsyncExecute();
        }

        public override T GetData<T>()
        {
            Type type = typeof(T);
            return type == typeof(VersionCompare) ? (T)Convert.ChangeType(data, typeof(T)) : default;
        }

        public override bool IsExecuted()
        {
            return data != null;
        }
    }
}
