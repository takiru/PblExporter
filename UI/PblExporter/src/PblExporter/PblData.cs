using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PblExporter
{
    /// <summary>
    /// PBLファイルの1データ情報。
    /// </summary>
    public class PblData
    {
        /// <summary>
        /// ファイル名。
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// ファイルパス。
        /// </summary>
        public string FilePath { get; private set; }

        /// <summary>
        /// 新しいPblDataインスタンスを生成します。
        /// </summary>
        /// <param name="filePath">PBLファイルパス。</param>
        public PblData(string filePath)
        {
            FileName = Path.GetFileName(filePath);
            this.FilePath = filePath;
        }
    }
}
