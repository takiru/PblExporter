using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using PblExporter.Core.Orca;

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
        /// オブジェクトのコメントに含まれる改行文字。
        /// </summary>
        public const string ObjectCommentNewLineSign = @"~r~n";

        /// <summary>
        /// PBL内のオブジェクトを取得します。
        /// </summary>
        /// <param name="encoding">ファイルエンコーディング。</param>
        /// <param name="executeFileName">実行ファイル名。</param>
        /// <param name="pblFilePath">PBLファイルパス。</param>
        /// <param name="outputFilePath">出力ファイルパス。</param>
        /// <returns>PBL内のオブジェクト一覧。</returns>
        public static List<ObjectInfo> GetObjectList(Encoding encoding, string executeFileName, string pblFilePath, string outputFilePath = "")
        {
            var filePath = outputFilePath;
            if (string.IsNullOrWhiteSpace(filePath))
            {
                filePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                filePath = Path.Combine(filePath, "PblObjects.txt");
            }

            var executeFilePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().Location), executeFileName);
            var psi = new ProcessStartInfo(executeFilePath);
            psi.Arguments = "\"" + pblFilePath + "\" \"" + filePath + "\"";
            psi.CreateNoWindow = true;

            var p = Process.Start(psi);
            p.WaitForExit();

            // UTF-16(BOMあり)で読み込み、情報を取得
            var values = File.ReadAllLines(filePath, encoding);

            var result = new List<ObjectInfo>();
            foreach (var value in values)
            {
                var objectData = new PblObjectData(value);
                result.Add(new ObjectInfo(pblFilePath, objectData.ObjectName, PbSupport.GetEntryType(objectData.ObjectType), objectData.UpdateDate, 0, objectData.Comment));
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

        /// <summary>
        /// オブジェクトタイプからエントリータイプを取得します。
        /// </summary>
        /// <param name="typeName">オブジェクトタイプ名。</param>
        /// <returns>エントリータイプ。</returns>
        public static EntryType GetEntryType(string typeName)
        {
            switch (typeName)
            {
                case "Application":
                    return EntryType.PBORCA_APPLICATION;
                case "Window":
                    return EntryType.PBORCA_WINDOW;
                case "DataWindow":
                    return EntryType.PBORCA_DATAWINDOW;
                case "Menu":
                    return EntryType.PBORCA_MENU;
                case "Query":
                    return EntryType.PBORCA_QUERY;
                case "Structure":
                    return EntryType.PBORCA_STRUCTURE;
                case "UserObject":
                    return EntryType.PBORCA_USEROBJECT;
                case "Pipeline":
                    return EntryType.PBORCA_PIPELINE;
                case "Project":
                    return EntryType.PBORCA_PROJECT;
                case "Function":
                    return EntryType.PBORCA_FUNCTION;
                case "ProxyObject":
                    return EntryType.PBORCA_PROXYOBJECT;
                case "Binary":
                    return EntryType.PBORCA_BINARY;
                case "0":
                    return EntryType.None;
            }
            return EntryType.None;
        }

        /// <summary>
        /// エントリータイプからオブジェクトタイプを取得します。
        /// </summary>
        /// <param name="entryType">エントリータイプ。。</param>
        /// <returns>オブジェクトタイプ名。</returns>
        public static string GetObjectType(EntryType entryType)
        {
            switch (entryType)
            {
                case EntryType.PBORCA_APPLICATION:
                    return "Application";
                case EntryType.PBORCA_WINDOW:
                    return "Window";
                case EntryType.PBORCA_DATAWINDOW:
                    return "DataWindow";
                case EntryType.PBORCA_MENU:
                    return "Menu";
                case EntryType.PBORCA_QUERY:
                    return "Query";
                case EntryType.PBORCA_STRUCTURE:
                    return "Structure";
                case EntryType.PBORCA_USEROBJECT:
                    return "UserObject";
                case EntryType.PBORCA_PIPELINE:
                    return "Pipeline";
                case EntryType.PBORCA_PROJECT:
                    return "Project";
                case EntryType.PBORCA_FUNCTION:
                    return "Function";
                case EntryType.PBORCA_PROXYOBJECT:
                    return "ProxyObject";
                case EntryType.PBORCA_BINARY:
                    return "Binary";
                case EntryType.None:
                    return "0";
            }
            return null;
        }
    }
}
