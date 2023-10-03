using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using IO = System.IO;

namespace PblExporter.export
{
    /// <summary>
    /// pbtファイルの解析を提供します。
    /// </summary>
    public class PbtParser : ParserBase
    {
        /// <summary>
        /// pbtファイルの相対パスを取得します。
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// pbtファイルのフルパスを取得します。
        /// </summary>
        public string FullPath { get; }

        /// <summary>
        /// pbtファイルから見つかったライブラリファイルパス群を取得します。
        /// </summary>
        public LibraryInfo[] LibraryItems { get; private set; } = new LibraryInfo[] { };

        /// <summary>
        /// ライブラリリスト解析用の正規表現。
        /// </summary>
        private static Regex LibListRegex = new Regex(@"^LibList\s""(.+)"";\r$", RegexOptions.Multiline | RegexOptions.Compiled);

        /// <summary>
        /// 新しいインスタンスを生成する。
        /// </summary>
        /// <param name="pbwPath">pbwファイルパス。</param>
        /// <param name="encodedPbtRelativePath">エンコードされたpbtファイルの相対パス。</param>
        private PbtParser(string pbwPath, string encodedPbtRelativePath)
        {
            var normalizePbtPath = NormalizePath(encodedPbtRelativePath);
            var pbtFullPath = GetFullPath(pbwPath, normalizePbtPath);

            var pbtText = ReadPbt(pbtFullPath);
            ParseLibraryList(pbtFullPath, GetLibraryList(pbtText));

            Path = normalizePbtPath;
            FullPath = pbtFullPath;
        }

        /// <summary>
        /// pbtファイルを解析します。
        /// </summary>
        /// <param name="pbwPath">pbwファイルパス。</param>
        /// <param name="encodedPbtRelativePath">エンコードされたpbtファイルの相対パス。</param>
        /// <returns>PblParser。</returns>
        public static PbtParser Parse(string pbwPath, string encodedPbtRelativePath)
        {
            return new PbtParser(pbwPath, encodedPbtRelativePath);
        }

        /// <summary>
        /// pbtファイルを読み込む。
        /// </summary>
        /// <param name="pbtPath">pbtファイルパス。</param>
        /// <returns>pbtファイル内のすべてのテキスト。</returns>
        private string ReadPbt(string pbtPath)
        {
            if (!IO.File.Exists(pbtPath))
            {
                throw new IO.FileNotFoundException("pbtファイルが見つかりませんでした。", pbtPath);
            }

            return IO.File.ReadAllText(pbtPath, Encoding.GetEncoding("Shift_JIS"));
        }

        /// <summary>
        /// ライブラリリスト部を解析して、利用しているライブラリリストファイルパスで取得する。
        /// </summary>
        /// <param name="pbtText">pbtファイル内容。</param>
        private string[] GetLibraryList(string pbtText)
        {
            // ライブラリリスト部を取得
            var matchArea = LibListRegex.Match(pbtText);
            if (!matchArea.Success)
            {
                throw new ArgumentException(@"ライブラリリストを読み込めませんでした。");
            }

            // ライブラリリストパスの記述を取得
            var libraryRawPaths = matchArea.Groups[1].Value.Split(';');

            // NOTE: 全角文字はUnicodeエンコードされているため、デコードしたものを把握する。
            var libraryPaths = new List<string>();
            foreach (var libraryRawPath in libraryRawPaths)
            {
                libraryPaths.Add(libraryRawPath);
            }

            return libraryPaths.ToArray();
        }

        /// <summary>
        /// ライブラリリストファイルを把握する。
        /// </summary>
        /// <param name="pbtPath">pbtファイルパス。</param>
        /// <param name="encodedLibraryRelativePaths">エンコードされたライブラリファイルの相対パス。</param>
        private void ParseLibraryList(string pbtPath, string[] encodedLibraryRelativePaths)
        {
            var libraryItems = new List<LibraryInfo>();
            foreach (var encodedLibraryRelativePath in encodedLibraryRelativePaths)
            {
                libraryItems.Add(new LibraryInfo(pbtPath, encodedLibraryRelativePath));
            }

            LibraryItems = libraryItems.ToArray();
        }
    }
}
