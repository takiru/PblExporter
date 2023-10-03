using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using PblExporter.Core;
using PblExporter.Core.Orca;

namespace PblExporter.export
{
    /// <summary>
    /// pblをエクスポートする命令を提供します。
    /// </summary>
    public class PblExporter
    {
        /// <summary>
        /// PBサポーター。
        /// </summary>
        private IPbSupporter Supporter { get; }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="supporter">PBサポーター。</param>
        public PblExporter(IPbSupporter supporter)
        {
            Supporter = supporter;
        }

        /// <summary>
        /// エクスポートの直前準備。
        /// </summary>
        /// <param name="exportDirectory">出力ディレクトリ。</param>
        /// <param name="outputDelete">exportDirectory の中身をすべて削除するかどうか。</param>
        /// <param name="targetPath">出力対象としたディレクトリまたはファイルパス。</param>
        private void prepareExport(string exportDirectory, bool outputDelete, string targetPath)
        {
            // 出力先ディレクトリの中身をすべて削除する
            if (outputDelete)
            {
                var targetDirectory = targetPath;
                if (File.Exists(targetPath))
                {
                    targetDirectory = Path.GetDirectoryName(targetPath);
                }

                // 出力先ディレクトリが読み込み対象となったディレクトリ/ファイルパスと異なる場合のみ実施する
                if (exportDirectory != targetDirectory)
                {
                    DirectoryInfo directory = new DirectoryInfo(exportDirectory);
                    directory.EnumerateFiles().ToList().ForEach(f => f.Delete());
                    directory.EnumerateDirectories().ToList().ForEach(d => d.Delete(true));
                }
            }
        }

        /// <summary>
        /// pblから指定したオブジェクトをエクスポートします。
        /// </summary>
        /// <param name="pblPath">pblファイルパス。</param>
        /// <param name="objectName">オブジェクト名。</param>
        /// <param name="objectType">オブジェクトタイプ。</param>
        /// <param name="outputHeader">ヘッダーを出力するかどうか。</param>
        /// <param name="exportDirectory">出力ディレクトリ。</param>
        public void ExportByFile(string pblPath, string objectName, EntryType objectType, bool outputHeader, string exportDirectory)
        {
            if (!Directory.Exists(exportDirectory))
            {
                Directory.CreateDirectory(exportDirectory);
            }
            Supporter.Export(pblPath, objectName, objectType, outputHeader, exportDirectory);
        }

        /// <summary>
        /// pblから指定したオブジェクトをエクスポートします。
        /// </summary>
        /// <param name="pblPath">pblファイルパス。</param>
        /// <param name="objectName">オブジェクト名。</param>
        /// <param name="objectType">オブジェクトタイプ。</param>
        /// <param name="outputHeader">ヘッダーを出力するかどうか。</param>
        /// <param name="exportDirectory">出力ディレクトリ。</param>
        public void ExportByFile(string pblPath, string objectName, string objectType, bool outputHeader, string exportDirectory)
        {
            ExportByFile(pblPath, objectName, PbSupport.GetEntryType(objectType), outputHeader, exportDirectory);
        }

        /// <summary>
        /// pblの中身をすべてエクスポートします。
        /// 出力ディレクトリの直下にpbl名のディレクトリが作られ、その中にエクスポートされます。
        /// </summary>
        /// <param name="pblPath">pblファイルパス。</param>
        /// <param name="outputHeader">ヘッダーを出力するかどうか。</param>
        /// <param name="exportDirectory">出力ディレクトリ。</param>
        /// <param name="outputDelete">exportDirectory の中身をすべて削除するかどうか。</param>
        public void ExportByPbl(string pblPath, bool outputHeader, string exportDirectory, bool outputDelete)
        {
            prepareExport(exportDirectory, outputDelete, pblPath);
            ExportByPbl(pblPath, outputHeader, exportDirectory);
        }

        /// <summary>
        /// pbl名のフォルダを用意して、pblの中身をすべてエクスポートする。
        /// </summary>
        /// <param name="pblPath">pblファイルパス。</param>
        /// <param name="outputHeader">ヘッダーを出力するかどうか。</param>
        /// <param name="exportDirectory">出力ディレクトリ。</param>
        /// <param name="outputDelete">exportDirectory の中身をすべて削除するかどうか。</param>
        private void ExportByPbl(string pblPath, bool outputHeader, string exportDirectory)
        {
            var outputDirectory = Path.Combine(exportDirectory, Path.GetFileNameWithoutExtension(pblPath));
            ExportByFile(pblPath, PbSupport.BulkExport, EntryType.None, outputHeader, outputDirectory);
        }

        /// <summary>
        /// ディレクトリ内のすべてのpblの中身をすべてエクスポートします。
        /// </summary>
        /// <param name="pblDirectory">pblファイルのディレクトリパス。</param>
        /// <param name="outputHeader">ヘッダーを出力するかどうか。</param>
        /// <param name="exportDirectory">出力ディレクトリ。</param>
        /// <param name="recursive">pblDirectory の配下の階層をすべて処理するかどうか。</param>
        /// <param name="preserve">exportDirectory 内に、元のディレクトリ階層を保持してエクスポートするかどうか。</param>
        /// <param name="outputDelete">exportDirectory の中身をすべて削除するかどうか。</param>
        public void ExportByDirectory(string pblDirectory, bool outputHeader, string exportDirectory, bool recursive, bool preserve, bool outputDelete)
        {
            string[] pblPaths;
            if (recursive)
            {
                pblPaths = findRecursivePblPaths(pblDirectory);
            }
            else
            {
                pblPaths = findPblPaths(pblDirectory);
            }

            if (pblPaths.Length == 0)
            {
                return;
            }

            prepareExport(exportDirectory, outputDelete, pblDirectory);

            foreach (var pblPath in pblPaths)
            {
                var outputDirectory = exportDirectory;
                if (preserve)
                {
                    outputDirectory = Path.Combine(outputDirectory, Path.GetDirectoryName(pblPath).Replace(pblDirectory, "").Trim(Path.DirectorySeparatorChar));
                }
                ExportByPbl(pblPath, outputHeader, outputDirectory);
            }
        }

        /// <summary>
        /// pbwファイルからpbtライブラリリストに登録されているすべてのpblの中身をすべてエクスポートします。
        /// 
        /// 出力ディレクトリ直下にpbt名のディレクトリが作られ、更にその直下にpbtファイルから見た元のディレクトリ階層を
        /// 保持した上でpbl名のディレクトリが作られ、その中にエクスポートされます。
        /// pblファイルとpbtファイルが同一階層だった時、2階層目にpbtファイルが含まれるディレクトリ名のディレクトリが作られます。
        /// 
        /// 複数のpbtファイルによって参照されているpblは MultiPbtReference ディレクトリが作られ、更にその直下に
        /// pbwファイルから見た元のディレクトリ階層を保持した上でpbl名のディレクトリが作られ、その中にエクスポートされます。
        /// pblファイルとpbwファイルが同一階層だった時、2階層目にpbwファイルが含まれるディレクトリ名のディレクトリが作られます。
        /// </summary>
        /// <param name="version">ワークスペースのPBバージョン。</param>
        /// <param name="pbwPath">pbwファイルパス。</param>
        /// <param name="outputHeader">ヘッダーを出力するかどうか。</param>
        /// <param name="exportDirectory">出力ディレクトリ。</param>
        /// <param name="outputDelete">exportDirectory の中身をすべて削除するかどうか。</param>
        public void ExportByWorkspace(decimal version, string pbwPath, bool outputHeader, string exportDirectory, bool outputDelete)
        {
            if (version < 8)
            {
                throw new ArgumentException("PB8未満はワークスペースをサポートしていません。");
            }

            var parser = PbwParser.Parse(pbwPath);

            if (parser.PbtItems.Length == 0)
            {
                return;
            }

            prepareExport(exportDirectory, outputDelete, pbwPath);

            // 全pbtをまたいで共通利用されているライブラリを求める
            var multiPbtReferenceLibraryPaths = parser.PbtItems
                .SelectMany(a => a.LibraryItems)
                .GroupBy(b => b.FullPath)
                .Where(c => c.Count() > 1)
                .Select(d => d.Key)
                .ToArray();

            // 共通利用されているpblは MultiPbtReference フォルダを作って、そこに階層を維持して入れる
            foreach (var duplicatedLibraryPath in multiPbtReferenceLibraryPaths)
            {
                ExportByPblLibraryInPbt(pbwPath, duplicatedLibraryPath, outputHeader, exportDirectory, @"MultiPbtReference");
            }

            foreach (var pbt in parser.PbtItems)
            {
                foreach (var library in pbt.LibraryItems.Where(x => !multiPbtReferenceLibraryPaths.Contains(x.FullPath)))
                {
                    ExportByPblLibraryInPbt(pbt.FullPath, library.FullPath, outputHeader, exportDirectory, Path.GetFileNameWithoutExtension(pbt.Path));
                }
            }
        }

        /// <summary>
        /// pbt内に含まれているpblの中身をすべてエクスポートする。
        /// </summary>
        /// <param name="basePath">pbw/pbtファイルパス。</param>
        /// <param name="libraryPath">ライブラリファイルパス。</param>
        /// <param name="outputHeader">ヘッダーを出力するかどうか。</param>
        /// <param name="exportDirectory">出力ディレクトリ。</param>
        /// <param name="topDirectoryName">最上位ディレクトリ名。</param>
        private void ExportByPblLibraryInPbt(string basePath, string libraryPath, bool outputHeader, string exportDirectory, string topDirectoryName)
        {
            if (LibraryInfo.GetLibraryType(libraryPath) == LibraryType.Pbd)
            {
                return;
            }

            var outputPath = Path.Combine(exportDirectory, topDirectoryName);

            var baseDirectory = Path.GetDirectoryName(basePath);
            var basePathRoot = Path.GetPathRoot(basePath);

            // 基底パスを基準として相対パスを求める
            var baseUri = new Uri(baseDirectory);
            var libraryUri = new Uri(Path.GetDirectoryName(libraryPath));
            var relativeLibraryPath = baseUri.MakeRelativeUri(libraryUri).ToString().Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

            // pblの相対パスが基底パスと同一の場合、基底パスファイルの存在するフォルダを出力先の相対パスとする
            if (relativeLibraryPath == "")
            {
                var parent = baseDirectory;
                if (parent == basePathRoot || Path.GetDirectoryName(parent) == basePathRoot)
                {
                    baseUri = new Uri(basePathRoot);
                }
                else
                {
                    baseUri = new Uri(Path.GetDirectoryName(parent) + Path.DirectorySeparatorChar);
                }

                relativeLibraryPath = baseUri.MakeRelativeUri(libraryUri).ToString()
                    .Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            }

            var pathRoot = string.IsNullOrEmpty(relativeLibraryPath) ? "" : Path.GetPathRoot(relativeLibraryPath);

            // 相対パスの場合
            if (string.IsNullOrEmpty(pathRoot))
            {
                outputPath = Path.GetFullPath(Path.Combine(outputPath, relativeLibraryPath));
                outputPath = Path.Combine(outputPath, Path.GetFileNameWithoutExtension(libraryPath));
            }
            else
            {
                // 絶対パスの場合
                var uri = new Uri(pathRoot);
                if (uri.IsUnc)
                {
                    // ルートがネットワークパスの場合、ネットワークパスからのフォルダを作る
                    outputPath = Path.Combine(outputPath, Path.GetFileNameWithoutExtension(libraryPath).Substring(2));
                }
                else
                {
                    // 異なるドライブレターのpblの場合、ドライブレターからのフォルダを作る
                    outputPath = Path.Combine(outputPath, pathRoot[0].ToString(),
                        Path.GetDirectoryName(libraryPath).Substring(3), Path.GetFileNameWithoutExtension(libraryPath));
                }
            }

            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            Supporter.Export(libraryPath, PbSupport.BulkExport, EntryType.None, outputHeader, outputPath);
        }

        /// <summary>
        /// 再帰的にディレクトリを操作してすべてのpblパスを得る。
        /// </summary>
        /// <param name="pblDirectory">pblファイルのディレクトリパス。</param>
        /// <returns>pblファイルパス群。</returns>
        private string[] findRecursivePblPaths(string pblDirectory)
        {
            var result = new List<string>();

            findRecursivePblPaths(pblDirectory, result);

            return result.ToArray();
        }

        /// <summary>
        /// 再帰的にディレクトリを操作してすべてのpblパスを得る。
        /// 隠しフォルダ、システムフォルダは除外する。
        /// </summary>
        /// <param name="pblDirectory">pblファイルのディレクトリパス。</param>
        /// <param name="pblPaths">pblファイルパス群。</param>
        private void findRecursivePblPaths(string pblDirectory, List<string> pblPaths)
        {
            pblPaths.AddRange(findPblPaths(pblDirectory));

            var directories = Directory.GetDirectories(pblDirectory, "*", SearchOption.AllDirectories);
            foreach (var directory in directories)
            {
                var di = new DirectoryInfo(directory);
                if ((di.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                {
                    continue;
                }
                if ((di.Attributes & FileAttributes.System) == FileAttributes.System)
                {
                    continue;
                }

                findRecursivePblPaths(directory, pblPaths);
            }
        }

        /// <summary>
        /// ディレクトリ内のすべてのpblパスを得る。
        /// 隠しファイルは除外する。
        /// </summary>
        /// <param name="pblDirectory">pblファイルのディレクトリパス。</param>
        /// <returns>pblファイルパス群。</returns>
        private string[] findPblPaths(string pblDirectory)
        {
            var result = new List<string>();

            var files = Directory.GetFiles(pblDirectory, "*.pbl");
            foreach (var file in files)
            {
                var fi = new FileInfo(file);
                if ((fi.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                {
                    continue;
                }

                result.Add(file);
            }

            return result.ToArray();
        }
    }
}
