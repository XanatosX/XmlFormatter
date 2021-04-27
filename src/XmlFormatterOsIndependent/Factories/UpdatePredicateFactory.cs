using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using XmlFormatterModel.Update;

namespace XmlFormatterOsIndependent.Factories
{
    /// <summary>
    /// Factory to get predicate for the correct os
    /// </summary>
    class UpdatePredicateFactory
    {
        /// <summary>
        /// Get the right predicate
        /// </summary>
        /// <returns>The matching filter required for updating</returns>
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

        /// <summary>
        /// Get the filter for windows
        /// </summary>
        /// <returns>windows filter</returns>
        private Predicate<IReleaseAsset> GetWindowsFilter()
        {
            return (asset) => asset.Name.Contains("WindowsAvalonia");
        }

        /// <summary>
        /// Get the filter for linux
        /// </summary>
        /// <returns>Linux filter</returns>
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
