using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PblExporter.Core;
using PblExporter.Core.Orca;

namespace PblExportTools100
{
    [Export(typeof(IPbSupporter))]
    public class ExportTools : IPbSupporter
    {
        public decimal Version => 10;
        public string PbVersion => "PB10.0";

        /// <summary>
        /// PBLオブジェクト一覧を取得するための実行ファイル名。
        /// </summary>
        public string ObjectListExecuteFileName => "";

        /// <summary>
        /// PBLオブジェクトコードを取得するための実行ファイル名。
        /// </summary>
        public string ObjectCodeExecuteFileName => "";

        /// <summary>
        /// オブジェクト一覧やオブジェクトコードのファイルエンコーディング。
        /// </summary>
        public Encoding FileEncoding => new UnicodeEncoding(false, true);

        /// <summary>
        /// ORCA実行オブジェクト。
        /// </summary>
        private OrcaCommand orca = new OrcaCommand(new OrcaExecutor());

        /// <summary>
        /// PBL内のオブジェクトを取得します。
        /// </summary>
        /// <param name="pblFilePath">PBLファイルパス。</param>
        /// <param name="outputDirectory">出力ファイルパス。</param>
        /// <returns>PBL内のオブジェクト一覧。</returns>
        public List<ObjectInfo> GetObjectList(string pblFilePath, string outputDirectory = "")
        {
            var objectList = new List<ObjectInfo>();
            orca.OpenSession();
            orca.GetLibraryDirectory(pblFilePath, out objectList);
            orca.CloseSession();

            return objectList;
        }

        /// <summary>
        /// PBLの対処オブジェクトのコードを出力します。
        /// </summary>
        /// <param name="pblFilePath">PBLファイルパス。</param>
        /// <param name="objectName">オブジェクト名。</param>
        /// <param name="entryType">エントリータイプ名。</param>
        /// <param name="outputHeader">ファイルヘッダーを出力するかどうか。</param>
        /// <param name="outputDirectory">出力フォルダパス。</param>
        public void Export(string pblFilePath, string objectName, EntryType entryType, bool outputHeader, string outputDirectory = "")
        {
            var code = "";

            orca.OpenSession();

            // ファイル出力設定
            var config = new PBORCA_CONFIG_SESSION();
            config.bDebug = false;
            config.bExportCreateFile = true;
            config.bExportHeaders = outputHeader;
            config.bExportIncludeBinary = false;
            config.eClobber = PBORCA_ENUM_FILEWRITE_OPTION.PBORCA_CLOBBER;
            config.eExportEncoding = PBORCA_ENCODING.PBORCA_UNICODE;
            config.pExportDirectory = outputDirectory;
            orca.SetSessionConfig(config);

            // 一括出力の場合
            if (objectName == PbSupport.BulkExport)
            {
                var objectList = new List<ObjectInfo>();
                orca.GetLibraryDirectory(pblFilePath, out objectList);
                foreach (var objectData in objectList)
                {
                    orca.Export(objectData.PblPath, objectData.ObjectName, objectData.EntryType, out code);
                }
            }
            else
            {
                orca.Export(pblFilePath, objectName, entryType, out code);
            }
            orca.CloseSession();
        }
    }
}
