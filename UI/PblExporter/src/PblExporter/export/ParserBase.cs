using System.IO;
using System.Text.RegularExpressions;

namespace PblExporter.export
{
    /// <summary>
    /// pbw/pbt解析を行う基本命令を提供します。
    /// </summary>
    public abstract class ParserBase
    {
        /// <summary>
        /// Unicode デコードして正常化したパスを求めます。
        /// </summary>
        /// <param name="path">Unicode エンコードされているパス。</param>
        /// <returns>Unicode デコードされたパス。</returns>
        protected string NormalizePath(string path)
        {
            return Regex.Unescape(path);
        }

        /// <summary>
        /// 基底となるファイルパスから、ターゲットとなるファイルパスのフルパスを求めます。
        /// </summary>
        /// <param name="basePath">基底となるファイルパス。</param>
        /// <param name="targetPath">ターゲットとなるファイルパス。</param>
        /// <returns>ターゲットとなるファイルパスのフルパス。</returns>
        protected string GetFullPath(string basePath, string targetPath)
        {
            // 相対パス
            if (Path.GetPathRoot(targetPath) == "")
            {

                return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(basePath), targetPath));
            }

            // ドライブレターやネットワークパス
            return targetPath;
        }
    }
}
