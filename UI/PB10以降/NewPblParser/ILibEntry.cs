using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPblParser
{
    public interface ILibEntry
    {
        DateTime Createtime { get; }
        string Comment { get; }
        string Name { get; }
        int Size { get; }
        EntryType Type { get; }
        string Library { get; }
        string Source { set; get; }
    }
}
