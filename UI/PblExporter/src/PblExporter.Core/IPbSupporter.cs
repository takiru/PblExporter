using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PblExporter.Core
{
    public interface IPbSupporter
    {
        /// <summary>
        /// PBバージョン。
        /// </summary>
        string PbVersion { get; }

        /// <summary>
        /// PBLオブジェクト一覧を取得するための実行ファイル名。
        /// </summary>
        string ObjectListExecuteFileName { get; }

        /// <summary>
        /// PBLオブジェクトコードを取得するための実行ファイル名。
        /// </summary>
        string ObjectCodeExecuteFileName { get; }

        /// <summary>
        /// オブジェクト一覧、オブジェクトコードのファイルエンコーディング。
        /// </summary>
        Encoding FileEncoding { get; }

        /// <summary>
        /// PBL内のオブジェクトを取得します。
        /// </summary>
        /// <param name="pblFilePath">PBLファイルパス。</param>
        /// <param name="outputFilePath">出力ファイルパス。</param>
        /// <returns>PBL内のオブジェクト一覧。</returns>
        List<PblObjectData> GetObjectList(string pblFilePath, string outputFilePath = "");

        /// <summary>
        /// PBLの対処オブジェクトのコードを出力します。
        /// </summary>
        /// <param name="pblFilePath">PBLファイルパス。</param>
        /// <param name="objectName">オブジェクト名。</param>
        /// <param name="objectType">オブジェクトタイプ名。</param>
        /// <param name="outputHeader">ファイルヘッダーを出力するかどうか。</param>
        /// <param name="outputDirectory">出力フォルダパス。</param>
        void Export(string pblFilePath, string objectName, string objectType, bool outputHeader, string outputDirectory = "");

    }
}
