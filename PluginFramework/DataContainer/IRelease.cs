using System;
using System.Collections.Generic;
using System.Text;

namespace XmlFormatterModel.Update
{
    public interface IRelease
    {
        string Author { get; }
        string Name { get; }
        string Url { get; }
        string TagName { get; }
        IReadOnlyList<IReleaseAsset> Assets { get; }
    }
}
