using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PblExporter.Core
{
    /// <summary>
    /// PB操作用クラス。
    /// </summary>
    public static class PbSupport
    {
        /// <summary>
        /// PBL一括エクスポート時のキーワード。
        /// </summary>
        public const string BulkExport = @"bulk";

        /// <summary>
        /// PBL内のオブジェクトを取得します。
        /// </summary>
        /// <param name="encoding">ファイルエンコーディング。</param>
        /// <param name="executeFileName">実行ファイル名。</param>
        /// <param name="pblFilePath">PBLファイルパス。</param>
        /// <param name="outputFilePath">出力ファイルパス。</param>
        /// <returns>PBL内のオブジェクト一覧。</returns>
        public static List<PblObjectData> GetObjectList(Encoding encoding, string executeFileName, string pblFilePath, string outputFilePath = "")
        {
            var filePath = outputFilePath;
            if (string.IsNullOrWhiteSpace(filePath))
            {
                filePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                filePath = Path.Combine(filePath, "PbObjects.txt");
            }

            var executeFilePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().Location), executeFileName);
            var psi = new ProcessStartInfo(executeFilePath);
            psi.Arguments = "\"" + pblFilePath + "\" \"" + filePath + "\"";
            psi.CreateNoWindow = true;

            var p = Process.Start(psi);
            p.WaitForExit();

            // UTF-16(BOMあり)で読み込み、情報を取得
            var values = File.ReadAllLines(filePath, encoding);

            var result = new List<PblObjectData>();
            foreach (var value in values)
            {
                result.Add(new PblObjectData(value));
            }

            try
            {
                File.Delete(filePath);
            }
            catch
            {
            }

            return result;
        }

        /// <summary>
        /// PBLの対処オブジェクトのコードを出力します。
        /// </summary>
        /// <param name="executeFileName">実行ファイル名。</param>
        /// <param name="pblFilePath">PBLファイルパス。</param>
        /// <param name="objectName">オブジェクト名。</param>
        /// <param name="objectType">オブジェクトタイプ名。</param>
        /// <param name="outputHeader">ファイルヘッダーを出力するかどうか。</param>
        /// <param name="outputDirectory">出力フォルダパス。</param>
        public static void Export(string executeFileName, string pblFilePath, string objectName, string objectType, bool outputHeader, string outputDirectory = "")
        {
            var header = "1";
            if (!outputHeader)
            {
                header = "0";
            }

            var executeFilePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().Location), executeFileName);
            var psi = new ProcessStartInfo(executeFilePath);
            psi.Arguments = "\"" + pblFilePath + "\" \"" + objectName + "\" \"" + objectType + "\" \"" + header + "\" \"" + outputDirectory + "\"";
            psi.CreateNoWindow = true;

            var p = Process.Start(psi);
            p.WaitForExit();
        }

        /// <summary>
        /// オブジェクトタイプから拡張子を取得します。
        /// </summary>
        /// <param name="objectType">オブジェクトタイプ。</param>
        /// <returns>拡張子。</returns>
        public static string GetExtension(string objectType)
        {
            switch (objectType)
            {
                case "Application":
                    return ".sra";
                case "Window":
                    return ".srw";
                case "DataWindow":
                    return ".srd";
                case "Menu":
                    return ".srm";
                case "Query":
                    return ".srq";
                case "Structure":
                    return ".srs";
                case "UserObject":
                    return ".sru";
                case "Pipeline":
                    return ".srp";
                case "Project":
                    return ".srj";
                case "Function":
                    return ".srf";
            }

            return null;
        }
    }
}
