using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using XmlFormatterModel.Update;

namespace XmlFormatterOsIndependent.Factories
{
    class UpdatePredicateFactory
    {
        public Predicate<IReleaseAsset> GetFilter()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return GetWindowsFilter();
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return GetLinuxFilter();
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return GetDarwinFilter();
            }
            return (asset) => false;
        }

        private Predicate<IReleaseAsset> GetWindowsFilter()
        {
            return (asset) => asset.Name.Contains("WindowsAvalonia");
        }

        private Predicate<IReleaseAsset> GetLinuxFilter()
        {
            return (asset) => asset.Name == "LinuxAvalonia";
        }

        private Predicate<IReleaseAsset> GetDarwinFilter()
        {
            return (asset) => asset.Name == "DarwinAvalonia";
        }
    }
}
