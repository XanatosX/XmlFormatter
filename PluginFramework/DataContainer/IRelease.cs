using System;
using System.Collections.Generic;
using System.Text;

namespace XmlFormatterModel.Update
{
    /// <summary>
    /// This interface represents a release from any given platform
    /// </summary>
    public interface IRelease
    {
        /// <summary>
        /// Name of the author
        /// </summary>
        string Author { get; }

        /// <summary>
        /// Name of the release
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Url to the release
        /// </summary>
        string Url { get; }

        /// <summary>
        /// Tag of the release
        /// </summary>
        string TagName { get; }

        /// <summary>
        /// All the assets in the release
        /// </summary>
        IReadOnlyList<IReleaseAsset> Assets { get; }
    }
}
