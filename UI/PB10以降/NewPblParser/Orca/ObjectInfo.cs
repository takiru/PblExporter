using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPblParser
{
    /// <summary>
    /// オブジェクト情報。
    /// </summary>
    public class ObjectInfo
    {
        public string PblPath { get; private set; }

        public DateTime CreateDateTime { get; private set; }
        public string Comment { get; private set; }
        public string ObjectName { get; private set; }
        public int Size { get; private set; }
        public EntryType EntryType { get; private set; }

        public ObjectInfo(string pblPath, string objectName, EntryType entryType, DateTime createDateTime, int size, string comment)
        {
            this.PblPath = pblPath;
            this.CreateDateTime = createDateTime;
            this.Comment = comment;
            this.ObjectName = objectName;
            this.Size = size;
            this.EntryType = entryType;
        }
    }
}
