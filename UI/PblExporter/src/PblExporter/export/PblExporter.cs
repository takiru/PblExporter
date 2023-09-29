using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PblExporter.Core;
using PblExporter.Core.Orca;

namespace PblExporter.export
{
    class PblExporter
    {
        private IPbSupporter Supporter { get; }

        public PblExporter(IPbSupporter supporter)
        {
            Supporter = supporter;
        }

        private void prepareExport(string exportDirectory, bool outputDelete)
        {
            // 出力先ディレクトリの中身をすべて削除する
            if (outputDelete)
            {
                DirectoryInfo directory = new DirectoryInfo(exportDirectory);
                directory.EnumerateFiles().ToList().ForEach(f => f.Delete());
                directory.EnumerateDirectories().ToList().ForEach(d => d.Delete(true));
            }
        }

        /// <summary>
        /// pblから指定したオブジェクトをエクスポートします。
        /// </summary>
        /// <param name="pblPath"></param>
        /// <param name="objectName"></param>
        /// <param name="objectType"></param>
        /// <param name="outputHeader"></param>
        /// <param name="exportDirectory"></param>
        public void ExportByFile(string pblPath, string objectName, EntryType objectType, bool outputHeader, string exportDirectory)
        {
            Supporter.Export(pblPath, objectName, objectType, outputHeader, exportDirectory);
        }

        /// <summary>
        /// pblから指定したオブジェクトをエクスポートします。
        /// </summary>
        /// <param name="pblPath"></param>
        /// <param name="objectName"></param>
        /// <param name="objectType"></param>
        /// <param name="outputHeader"></param>
        /// <param name="exportDirectory"></param>
        public void ExportByFile(string pblPath, string objectName, string objectType, bool outputHeader, string exportDirectory)
        {
            ExportByFile(pblPath, objectName, PbSupport.GetEntryType(objectType), outputHeader, exportDirectory);
        }

        /// <summary>
        /// pblの中身をすべてエクスポートします。
        /// </summary>
        /// <param name="pblPath"></param>
        /// <param name="outputHeader"></param>
        /// <param name="exportDirectory"></param>
        /// <param name="outputDelete"></param>
        public void ExportByPbl(string pblPath, bool outputHeader, string exportDirectory, bool outputDelete)
        {
            prepareExport(exportDirectory, outputDelete);
            ExportByPbl(pblPath, outputHeader, exportDirectory);
        }

        /// <summary>
        /// pblの中身をすべてエクスポートします。
        /// </summary>
        /// <param name="pblPath"></param>
        /// <param name="outputHeader"></param>
        /// <param name="exportDirectory"></param>
        /// <param name="outputDelete"></param>
        private void ExportByPbl(string pblPath, bool outputHeader, string exportDirectory)
        {
            var outputDirectory = Path.Combine(exportDirectory, Path.GetFileNameWithoutExtension(pblPath));
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            ExportByFile(pblPath, PbSupport.BulkExport, EntryType.None, outputHeader, outputDirectory);
        }

        /// <summary>
        /// ディレクトリ内のすべてのpblの中身をすべてエクスポートします。
        /// </summary>
        /// <param name="pblPaths"></param>
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

            prepareExport(exportDirectory, outputDelete);

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
        /// ワークスペースからライブラリリストに登録されているすべてのpblの中身をすべてエクスポートします。
        /// </summary>
        /// <param name="pblPaths"></param>
        public void ExportByWorkspace(decimal version, string pbwPath, bool outputHeader, string exportDirectory, bool outputDelete)
        {
            if (version < 8)
            {
                throw new Exception("PB8未満はワークスペースをサポートしていません。");
            }

            var parser = PbwParser.Parse(pbwPath);

            if (parser.PbtItems.Length == 0)
            {
                return;
            }


            //var pbtPaths = findPbtPathsByWorkspace(pbwPath);
            //if (pbtPaths.Length == 0)
            //{
            //    return;
            //}

            prepareExport(exportDirectory, outputDelete);


            // 全pbtをまたいで共通利用されているpblを求める
            var duplicatedLibraryPaths = parser.PbtItems
                .SelectMany(a => a.LibraryItems)
                .Where(b => b.Type == LibraryType.Pbl)
                .GroupBy(c => c.Path)
                .Where(d => d.Count() > 1)
                .Select(e => e.Key)
                .ToArray();

            // 共通利用されているpblは MultiPbtReference フォルダを作って、そこに階層を維持して入れる
            foreach (var duplicatedLibraryPath in duplicatedLibraryPaths)
            {
                var outputPath = Path.Combine(exportDirectory, "MultiPbtReference");


                // ワークスペースパスを基準として相対パスを求める
                var pbwUri = new Uri(Path.GetDirectoryName(pbwPath));
                var libraryUri = new Uri(Path.GetDirectoryName(duplicatedLibraryPath));
                var relativeLibraryPath = pbwUri.MakeRelativeUri(libraryUri).ToString().Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

                // pblの相対パスがワークスペースと同一の場合、ワークスペースの存在するフォルダとする
                if (relativeLibraryPath == "")
                {
                    var parent = Path.GetDirectoryName(pbwPath);
                    if (parent == Path.GetPathRoot(pbwPath) || Path.GetDirectoryName(parent) == Path.GetPathRoot(pbwPath))
                    {
                        pbwUri = new Uri(Path.GetPathRoot(pbwPath));
                    }
                    else
                    {
                        parent = Path.GetDirectoryName(parent);
                        pbwUri = new Uri(parent + Path.DirectorySeparatorChar);
                    }

                    relativeLibraryPath = pbwUri.MakeRelativeUri(libraryUri).ToString().Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
                }

                var pathRoot = string.IsNullOrEmpty(relativeLibraryPath) ? "" : Path.GetPathRoot(relativeLibraryPath);

                // 相対パスの場合
                if (pathRoot == "")
                {
                    outputPath = Path.GetFullPath(Path.Combine(outputPath, relativeLibraryPath));
                    outputPath = Path.Combine(outputPath, Path.GetFileNameWithoutExtension(duplicatedLibraryPath));

                    if (!Directory.Exists(outputPath))
                    {
                        Directory.CreateDirectory(outputPath);
                    }
                }
                else
                {
                    // 絶対パスの場合
                    var uri = new Uri(pathRoot);
                    if (uri.IsUnc)
                    {
                        // ルートがネットワークパスの場合、ネットワークパスからのフォルダを作る
                        outputPath = Path.Combine(outputPath, Path.GetFileNameWithoutExtension(duplicatedLibraryPath).Substring(2));
                    }
                    else
                    {
                        // 異なるドライブレターのpblの場合、ドライブレターからのフォルダを作る
                        outputPath = Path.Combine(outputPath, pathRoot[0].ToString(), Path.GetDirectoryName(duplicatedLibraryPath).Substring(3), Path.GetFileNameWithoutExtension(duplicatedLibraryPath));
                    }
                }

                if (!Directory.Exists(outputPath))
                {
                    Directory.CreateDirectory(outputPath);
                }

                Supporter.Export(duplicatedLibraryPath, PbSupport.BulkExport, EntryType.None, outputHeader, outputPath);
            }

            foreach (var pbt in parser.PbtItems)
            {

            }

            ////var pblPaths = new List<string>();
            //foreach (var pbtPath in pbtPaths)
            //{
            //    // これやめる
            //    //pblPaths.AddRange(findPblPathsByPbt(pbtPath).Where(x => !pblPaths.Contains(x)));



            //    // ワークスペースを対象に出力先を指定するから、出力先＝ワークスペース扱い
            //    // 出力先の下にpbtフォルダができて、その中にpbtからの相対パスができて、その中にpblフォルダができる？
            //    var pblPaths = findPblPathsByPbt(pbtPath);

            //    // 複数のpbtから参照しているpblの場合、MultiPbtReference フォルダを作って、そこに階層を維持して入れる


            //    var outputPath = Path.Combine(exportDirectory, Path.GetFileNameWithoutExtension(pbtPath));
            //    foreach (var pblPath in pblPaths)
            //    {
            //        // 異なるドライブレターのpblの場合、ドライブレターからのフォルダを作る
            //        var pathRoot = Path.GetPathRoot(pblPath);
            //        if (pathRoot != "")
            //        {
            //            // ルートがネットワークパスの場合、ネットワークパスからのフォルダを作る
            //            var uri = new Uri(pathRoot);
            //            if (uri.IsUnc)
            //            {

            //            }
            //        }

            //        //var relativePblPath = pblPath.Replace()
            //    }

            //}





            //prepareExport(exportDirectory, outputDelete);

            //foreach (var path in pblPaths)
            //{
            //    var outputPath = exportDirectory;
            //    var relativePath = new Uri(Path.GetDirectoryName(workspacePath)).MakeRelativeUri(new Uri(Path.GetDirectoryName(path))).ToString().Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            //    // pblの相対パスがワークスペースと同一の場合、ワークスペースの存在するフォルダとする
            //    if (relativePath == "")
            //    {
            //        relativePath = new Uri(Path.GetDirectoryName(Path.GetDirectoryName(workspacePath)) + Path.DirectorySeparatorChar).MakeRelativeUri(new Uri(Path.GetDirectoryName(path))).ToString().Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            //    }

            //    outputPath = Path.Combine(outputPath, relativePath);
            //    outputPath = Path.Combine(outputPath, Path.GetFileNameWithoutExtension(path));
            //    if (!Directory.Exists(outputPath))
            //    {
            //        Directory.CreateDirectory(outputPath);
            //    }

            //    Supporter.Export(path, objectName, objectType, outputHeader, outputPath);
            //}
        }

        /// <summary>
        /// 再帰的にディレクトリを操作してすべてのpblパスを得る。
        /// </summary>
        /// <param name="path"></param>
        /// <param name="paths"></param>
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
        /// <param name="path"></param>
        /// <param name="paths"></param>
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
        /// <param name="path"></param>
        /// <param name="paths"></param>
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

        /// <summary>
        /// ワークスペースからpbtのパスを求める。
        /// </summary>
        /// <param name="workspacePath"></param>
        /// <returns></returns>
        private static string[] findPbtPathsByWorkspace(string workspacePath)
        {
            var wsText = File.ReadAllText(workspacePath, Encoding.GetEncoding("Shift_JIS"));

            // @begin Targets - @end; の範囲を取得
            var matchArea = Regex.Match(wsText, @"(?<=@begin Targets\r\n).+(?=\r\n@end;)", RegexOptions.Singleline);
            if (!matchArea.Success)
            {
                throw new ArgumentException(@"ワークスペースを読み込めませんでした。");
            }

            // pbtパスの記述を取得
            var matchPblPaths = Regex.Matches(matchArea.Value, @"^(\s[0-9]{1,}\s"")(.+)(?="";)", RegexOptions.Multiline);
            if (matchPblPaths.Count == 0)
            {
                throw new ArgumentException(@"ワークスペースを読み込めませんでした。");
            }

            var result = new List<string>();
            foreach (Match matchPblPath in matchPblPaths)
            {
                // ワークスペースとは異なるドライブレター、ネットワークパスの場合は、そのまま絶対パスとして受け入れる
                if (Path.GetPathRoot(matchPblPath.Groups[2].Value) != "")
                {
                    result.Add(Regex.Unescape(matchPblPath.Groups[2].Value));
                    continue;
                }

                // ワークスペースのパスからのpbtのフルパスを求める
                // NOTE: 全角文字はUnicodeエンコードされているため、デコードしたものを把握する
                result.Add(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(workspacePath), Regex.Unescape(matchPblPath.Groups[2].Value))));
            }

            return result.ToArray();
        }

        /// <summary>
        /// pbtからpblのパスを求める。
        /// </summary>
        /// <param name="pbtPath"></param>
        /// <returns></returns>
        private static string[] findPblPathsByPbt(string pbtPath)
        {
            var wsText = File.ReadAllText(pbtPath, Encoding.GetEncoding("Shift_JIS"));

            // ライブラリリストを取得
            var matchArea = Regex.Match(wsText, @"^LibList\s""(.+)"";\r$", RegexOptions.Multiline);
            if (!matchArea.Success)
            {
                throw new ArgumentException(@"pbtを読み込めませんでした。");
            }

            // pblパスの記述を取得
            var pblRelativePaths = matchArea.Groups[1].Value.Split(';');

            var result = new List<string>();
            foreach (var pblRelativePath in pblRelativePaths)
            {
                // pbd は無視
                if (Path.GetExtension(pblRelativePath) == "pbd")
                {
                    continue;
                }

                // pbtのパスからのpblのフルパスを求める
                // NOTE: 全角文字はUnicodeエンコードされているため、デコードしたものを把握する
                result.Add(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(pbtPath), Regex.Unescape(pblRelativePath))));
            }

            return result.ToArray();
        }
    }
}
