using CsvHelper.Configuration;
using PblExporter.Core.Orca;

namespace PblExporter
{
    class CsvMap : ClassMap<ObjectInfo>
    {
        public CsvMap()
        {
            Map(m => m.PblPath).Index(0).Name("PBLパス");
            Map(m => m.EntryType).Index(1).Name("オブジェクト種類");
            Map(m => m.ObjectName).Index(2).Name("オブジェクト名");
            Map(m => m.Comment).Index(3).Name("コメント").TypeConverter<NewLineConverter>();
            Map(m => m.Size).Index(4).Name("サイズ");
            Map(m => m.CreateDateTime).Index(5).Name("最終更新日時");
        }
    }
}
