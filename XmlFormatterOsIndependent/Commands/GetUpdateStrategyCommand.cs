﻿using PluginFramework.DataContainer;
using PluginFramework.Interfaces.PluginTypes;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using XmlFormatterModel.Setting;
using XmlFormatterOsIndependent.DataSets;

namespace XmlFormatterOsIndependent.Commands
{
    class GetUpdateStrategyCommand : BaseDataCommand
    {
        IUpdateStrategy strategy;

        public async override Task AsyncExecute(object parameter)
        {
            Execute();
        }

        public override bool CanExecute(object parameter)
        {
            return parameter is PluginManagmentData;
        }

        public override void Execute(object parameter)
        {
            strategy = null;

            if (parameter is PluginManagmentData data)
            {
                ISettingScope scope = data.SettingsManager.GetScope("Default");
                if (scope == null)
                {
                    return;
                }
                ISettingPair strategyPair = scope.GetSetting("UpdateStrategy");
                if (strategyPair == null)
                {
                    return;
                }

                string name = strategyPair.GetValue<string>();
                List<PluginMetaData> plugins = data.PluginManager.ListPlugins<IUpdateStrategy>();
                if (name == string.Empty)
                {
                    if (plugins.Count == 0)
                    {
                        return;
                    }
                    name = plugins[0].Type.ToString();
                }
                PluginMetaData metaData = plugins.Find((plugin) => plugin.Type.ToString() == name);
                if (metaData == null)
                {
                    return;
                }

                strategy = data.PluginManager.LoadPlugin<IUpdateStrategy>(metaData);
            }
        }

        public override T GetData<T>()
        {
            Type type = typeof(T);
            return type == typeof(IUpdateStrategy) && IsExecuted() ? (T)strategy : default;
        }

        public override bool IsExecuted()
        {
            return strategy != null;
        }
    }
}