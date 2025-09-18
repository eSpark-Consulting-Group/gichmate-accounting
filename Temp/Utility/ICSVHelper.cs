using System.Collections.Generic;
using System.IO;

namespace AccountsPRO.Utility
{
    public interface ICSVHelper
    {
        List<double> ReadCSV_DoubleData(FileInfo filename);
    }
}