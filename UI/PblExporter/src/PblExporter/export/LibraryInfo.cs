using IO = System.IO;

namespace PblExporter.export
{
    /// <summary>
    /// ライブラリ情報を提供します。
    /// </summary>
    public class LibraryInfo
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
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="path">ライブラリパス。</param>
        public LibraryInfo(string path)
        {
            Type = IO.Path.GetExtension(path) == ".pbl" ? LibraryType.Pbl : LibraryType.Pbd;
            Path = path;
        }
    }
}
