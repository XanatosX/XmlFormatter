using System;
using System.Collections.Generic;
using System.Text;

namespace XmlFormatterModel.Update
{
    public interface IReleaseAsset
    {
        string Url { get; }
        int Id { get; }
        string Name { get; }
        string Label { get; }
        int Size { get; }
    }
}
