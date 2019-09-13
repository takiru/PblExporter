using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PblExporter.Core;

namespace PblExportTools120
{
    [Export(typeof(IPbSupporter))]
    public class ExportTools : IPbSupporter
    {
        public decimal Version => 12.0m;

        public string PbVersion => "PB12.0";

        /// <summary>
        /// PBLオブジェクト一覧を取得するための実行ファイル名。
        /// </summary>
        public string ObjectListExecuteFileName => "pbobjectgetter120.exe";

        /// <summary>
        /// PBLオブジェクトコードを取得するための実行ファイル名。
        /// </summary>
        public string ObjectCodeExecuteFileName => "pbobjectparser120.exe";

        /// <summary>
        /// オブジェクト一覧やオブジェクトコードのファイルエンコーディング。
        /// </summary>
        public Encoding FileEncoding => new UnicodeEncoding(false, true);

        /// <summary>
        /// PBL内のオブジェクトを取得します。
        /// </summary>
        /// <param name="pblFilePath">PBLファイルパス。</param>
        /// <param name="outputDirectory">出力ファイルパス。</param>
        /// <returns>PBL内のオブジェクト一覧。</returns>
        public List<PblObjectData> GetObjectList(string pblFilePath, string outputDirectory = "")
        {
            return PbSupport.GetObjectList(FileEncoding, ObjectListExecuteFileName, pblFilePath, outputDirectory);
        }

        /// <summary>
        /// PBLの対処オブジェクトのコードを出力します。
        /// </summary>
        /// <param name="pblFilePath">PBLファイルパス。</param>
        /// <param name="objectName">オブジェクト名。</param>
        /// <param name="objectType">オブジェクトタイプ名。</param>
        /// <param name="outputHeader">ファイルヘッダーを出力するかどうか。</param>
        /// <param name="outputDirectory">出力フォルダパス。</param>
        public void Export(string pblFilePath, string objectName, string objectType, bool outputHeader, string outputDirectory = "")
        {
            PbSupport.Export(ObjectCodeExecuteFileName, pblFilePath, objectName, objectType, outputHeader, outputDirectory);
        }
    }
}
