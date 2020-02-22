using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlFormatter.src.Interfaces.Hotfolder
{
    /// <summary>
    /// This interface will define a hotfolder manager
    /// </summary>
    interface IHotfolderManager
    {
        /// <summary>
        /// Add a new hotfolder to the manager
        /// </summary>
        /// <param name="newHotfolder"></param>
        /// <returns></returns>
        bool AddHotfolder(IHotfolder newHotfolder);

        /// <summary>
        /// Get all the hotfolders in the manager
        /// </summary>
        /// <returns>A list with all the hotfolder configurations</returns>
        List<IHotfolder> GetHotfolders();
    }
}
