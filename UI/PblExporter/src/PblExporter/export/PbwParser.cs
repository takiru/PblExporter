using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using IO = System.IO;

namespace PblExporter.export
{
    /// <summary>
    /// ワークスペース（pbwファイル）の解析を提供します。
    /// </summary>
    public class PbwParser
    {
        /// <summary>
        /// pbwファイルパスを取得します。
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// pbwファイルから見つかったpbtファイルパス群を取得します。
        /// </summary>
        public PbtParser[] PbtItems { get; private set; }

        /// <summary>
        /// pbt記載領域解析用の正規表現。
        /// </summary>
        private static Regex PbtGroupRegex = new Regex(@"(?<=@begin Targets\r\n).+(?=\r\n@end;)", RegexOptions.Singleline | RegexOptions.Compiled);

        /// <summary>
        /// pbtパス解析用の正規表現。
        /// </summary>
        private static Regex PbtListRegex = new Regex(@"^(\s[0-9]{1,}\s"")(.+)(?="";)", RegexOptions.Multiline | RegexOptions.Compiled);

        /// <summary>
        /// 新しいインスタンスを生成する。
        /// </summary>
        /// <param name="pbwPath">pbwファイルパス。</param>
        private PbwParser(string pbwPath)
        {
            var pbwText = ReadPbw(pbwPath);
            ParsePbtList(GetPbtList(pbwPath, pbwText));

            Path = pbwPath;
        }

        /// <summary>
        /// pbtファイルを解析します。
        /// </summary>
        /// <param name="pbwPath">pbwファイルパス。</param>
        /// <returns>PblParser。</returns>
        public static PbwParser Parse(string pbwPath)
        {
            return new PbwParser(pbwPath);
        }

        /// <summary>
        /// pbwファイルを読み込む。
        /// </summary>
        /// <param name="pbwPath">pbwファイルパス。</param>
        /// <returns>pbtファイル内のすべてのテキスト。</returns>
        private string ReadPbw(string pbwPath)
        {
            if (!IO.File.Exists(pbwPath))
            {
                throw new IO.FileNotFoundException("pbwファイルが見つかりませんでした。", pbwPath);
            }

            return IO.File.ReadAllText(pbwPath, Encoding.GetEncoding("Shift_JIS"));
        }

        /// <summary>
        /// pbt記載領域を解析して、利用しているpbtをフルパスで取得する。
        /// </summary>
        /// <param name="pbwPath">pbwファイルパス。</param>
        /// <param name="pbwText">pbwファイル内容。</param>
        private string[] GetPbtList(string pbwPath, string pbwText)
        {
            // pbt記載領域部を取得
            var matchArea = PbtGroupRegex.Match(pbwText);
            if (!matchArea.Success)
            {
                throw new ArgumentException(@"pbt記載領域を読み込めませんでした。");
            }

            // pbtパスの記述を取得
            var matchMbtRawPaths = PbtListRegex.Matches(matchArea.Value);
            if (matchMbtRawPaths.Count == 0)
            {
                return new string[] { };
            }

            var pbtPaths = new List<string>();

            // NOTE: 全角文字はUnicodeエンコードされているため、デコードしたものを把握する。
            foreach (Match matchPbtRawPath in matchMbtRawPaths)
            {
                var pbtRawPath = matchPbtRawPath.Groups[2].Value;

                if (IO.Path.GetPathRoot(pbtRawPath) == "")
                {
                    // 相対パス
                    pbtPaths.Add(IO.Path.GetFullPath(IO.Path.Combine(IO.Path.GetDirectoryName(pbwPath), Regex.Unescape(pbtRawPath))));
                }
                else
                {
                    // ドライブレターやネットワークパス
                    pbtPaths.Add(Regex.Unescape(pbtRawPath));
                }
            }

            return pbtPaths.ToArray();
        }

        /// <summary>
        /// pbtファイルを解析して把握する。
        /// </summary>
        /// <param name="pbtList">pbtファイルパス群。</param>
        private void ParsePbtList(string[] pbtList)
        {
            var pbtItems = new List<PbtParser>();
            foreach (var pbtPath in pbtList)
            {
                pbtItems.Add(PbtParser.Parse(pbtPath));
            }

            PbtItems = pbtItems.ToArray();
        }
    }
}
