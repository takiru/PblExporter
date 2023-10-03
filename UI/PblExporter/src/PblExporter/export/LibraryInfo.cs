using IO = System.IO;

namespace PblExporter.export
{
    /// <summary>
    /// ライブラリ情報を提供します。
    /// </summary>
    public class LibraryInfo : ParserBase
    {
        /// <summary>
        /// ライブラリのタイプを取得します。
        /// </summary>
        public LibraryType Type { get; }

        /// <summary>
        /// ライブラリのパスを取得します。
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// ライブラリファイルのフルパスを取得します。
        /// </summary>
        public string FullPath { get; }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="pbtPath">pbtファイルパス。</param>
        /// <param name="encodedLibraryRelativePath">エンコードされたライブラリの相対パス。</param>
        public LibraryInfo(string pbtPath, string encodedLibraryRelativePath)
        {
            var normalizeLibraryPath = NormalizePath(encodedLibraryRelativePath);
            var libraryFullPath = GetFullPath(pbtPath, normalizeLibraryPath);

            Type = GetLibraryType(libraryFullPath);
            Path = normalizeLibraryPath;
            FullPath = libraryFullPath;
        }

        /// <summary>
        /// ライブラリの種類を取得します。
        /// </summary>
        /// <param name="path">ライブラリパス。</param>
        /// <returns>LibraryType。</returns>
        public static LibraryType GetLibraryType(string path)
        {
            return IO.Path.GetExtension(path) == ".pbl" ? LibraryType.Pbl : LibraryType.Pbd;
        }
    }
}
