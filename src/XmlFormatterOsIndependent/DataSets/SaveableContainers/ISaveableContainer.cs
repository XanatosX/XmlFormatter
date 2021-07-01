using System;
using System.Collections.Generic;
using System.Text;
using XmlFormatterModel.Setting;

namespace XmlFormatterOsIndependent.DataSets.SaveableContainers
{
    public interface ISaveableContainer<T>
    {
        List<ISettingPair> GetSettingPairs();

        T GetLoadedInstance(List<ISettingPair> settingsPairs);
        T GetStoredInstance();
    }
}
