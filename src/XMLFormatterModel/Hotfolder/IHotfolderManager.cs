using System.Collections.Generic;
using XMLFormatterModel.Hotfolder;

namespace XmlFormatterModel.Hotfolder
{
    /// <summary>
    /// This interface will define a hotfolder manager
    /// </summary>
    public interface IHotfolderManager
    {
        /// <summary>
        /// Add a new hotfolder to the manager
        /// </summary>
        /// <param name="newHotfolder"></param>
        /// <returns></returns>
        bool AddHotfolder(IHotfolder newHotfolder);

        /// <summary>
        /// Remove a hotfolder from the configuration
        /// </summary>
        /// <param name="hotfolderToRemove">The hotfolder to remove</param>
        /// <returns>True if removing was successful</returns>
        bool RemoveHotfolder(IHotfolder hotfolderToRemove);

        /// <summary>
        /// Get all the hotfolders in the manager
        /// </summary>
        /// <returns>A list with all the hotfolder configurations</returns>
        List<IHotfolder> GetHotfolders();

        /// <summary>
        /// Remove all the hotfolders from the configuration
        /// </summary>
        void ResetManager();
    }
}
