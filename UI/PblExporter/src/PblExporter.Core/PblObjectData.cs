using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PblExporter.Core
{
    /// <summary>
    /// PBLのオブジェクトデータ。
    /// </summary>
    public class PblObjectData
    {
        /// <summary>
        /// オブジェクト名。
        /// </summary>
        public string ObjectName { get; private set; }

        /// <summary>
        /// オブジェクトタイプ。
        /// </summary>
        public string ObjectType { get; private set; }

        /// <summary>
        /// 最終更新日時。
        /// </summary>
        public DateTime UpdateDate { get; private set; }

        /// <summary>
        /// コメント。
        /// </summary>
        public string Comment { get; private set; }

        /// <summary>
        /// 新しいPblObjectDataインスタンスを生成します。
        /// </summary>
        /// <param name="lineValue">1行のオブジェクト情報。</param>
        public PblObjectData(string lineValue)
        {
            var value = lineValue;
            var length = -1;

            // オブジェクト名
            length = value.IndexOf("\t");
            ObjectName = value.Substring(0, length).Trim();
            value = value.Substring(length + 1);

            // 最終更新日時
            length = value.IndexOf("\t");
            UpdateDate = DateTime.Parse(value.Substring(0, length).Trim());
            value = value.Substring(length + 1);

            // コメント
            length = value.IndexOf("\t");
            Comment = value.Substring(0, length).Trim();
            value = value.Substring(length + 1);

            // オブジェクトタイプ名
            ObjectType = value;
        }
    }
}
