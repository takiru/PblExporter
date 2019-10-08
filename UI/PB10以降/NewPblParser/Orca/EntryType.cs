using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPblParser
{
    /// <summary>
    /// PBORCAで認識するオブジェクトを定義します。
    /// </summary>
    public enum EntryType
    {
        PBORCA_APPLICATION,
        PBORCA_DATAWINDOW,
        PBORCA_FUNCTION,
        PBORCA_MENU,
        PBORCA_QUERY,
        PBORCA_STRUCTURE,
        PBORCA_USEROBJECT,
        PBORCA_WINDOW,
        PBORCA_PIPELINE,
        PBORCA_PROJECT,
        PBORCA_PROXYOBJECT,
        PBORCA_BINARY
    }
}
