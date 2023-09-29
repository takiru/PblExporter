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
    public class PbtParser
    {
        /// <summary>
        /// pbtファイルパスを取得します。
        /// </summary>
        public string Path { get; }

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
        /// <param name="pbtPath">pbtファイルパス。</param>
        private PbtParser(string pbtPath)
        {
            var pbtText = ReadPbt(pbtPath);
            ParseLibraryList(pbtPath, pbtText);

            Path = pbtPath;
        }

        /// <summary>
        /// pbtファイルを解析します。
        /// </summary>
        /// <param name="pbtPath">pbtファイルパス。</param>
        /// <returns>PblParser。</returns>
        public static PbtParser Parse(string pbtPath)
        {
            return new PbtParser(pbtPath);
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
        /// ライブラリリストを解析して、利用しているpbl, pbdをフルパスで把握する。
        /// </summary>
        /// <param name="pbtPath">pbtファイルパス。</param>
        /// <param name="pbtText">pbtファイル内容。</param>
        private void ParseLibraryList(string pbtPath, string pbtText)
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
            var libraryItems = new List<LibraryInfo>();
            foreach (var libraryRawPath in libraryRawPaths)
            {
                string libraryPath;
                if (IO.Path.GetPathRoot(libraryRawPath) == "")
                {
                    // 相対パス
                    libraryPath = IO.Path.GetFullPath(IO.Path.Combine(IO.Path.GetDirectoryName(pbtPath), Regex.Unescape(libraryRawPath)));
                }
                else
                {
                    // ドライブレターやネットワークパス
                    libraryPath = Regex.Unescape(libraryRawPath);
                }

                libraryItems.Add(new LibraryInfo(libraryPath));
            }

            LibraryItems = libraryItems.ToArray();
        }
    }
}
