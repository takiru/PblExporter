using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using PblExporter.Core;
using System.Text.RegularExpressions;

namespace PblExporter
{
    /// <summary>
    /// スタートアップ。
    /// </summary>
    class Startup
    {
        /// <summary>
        /// スタートアップ。
        /// </summary>
        /// <param name="args"></param>
        public static void Start(string[] args)
        {
            var result = Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed(opt =>
                {
                    CommandLineOptions.Value = opt;
                });

            // コマンドラインがパースできなかった場合はウィンドウ表示
            if (CommandLineOptions.Value == null)
            {
                Application.Run(new MainForm());
                return;
            }

            var version = CommandLineOptions.Value.Version;
            var pblPath = CommandLineOptions.Value.PblPath;
            var workspacePath = CommandLineOptions.Value.WorkspacePath;
            var objectName = CommandLineOptions.Value.ObjectName;
            var objectType = CommandLineOptions.Value.ObjectType;
            var recursive = CommandLineOptions.Value.Recursive;
            var outputHeader = CommandLineOptions.Value.OutputHeader;
            var outputDirectory = CommandLineOptions.Value.OutputDirectory;
            var preserve = CommandLineOptions.Value.Preserve;
            var outputDelete = CommandLineOptions.Value.OutputDelete;

            // プラグインを取得できなかったらウィンドウ表示
            var supporter = Plugins.PluginList.FirstOrDefault((x) => x.Version == version);
            if (supporter == null)
            {
                Application.Run(new MainForm());
                return;
            }

            // PBLもワークスペースも選択されてこなかったらウィンドウ表示
            if (string.IsNullOrEmpty(pblPath) && string.IsNullOrEmpty(workspacePath))
            {
                Application.Run(new MainForm());
                return;
            }

            // 出力先が指定されていない場合は現行ディレクトリを対象とする
            if (string.IsNullOrWhiteSpace(outputDirectory))
            {
                outputDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }

            string outputPath = "";
            string[] pblPaths = new string[] { };
            bool isPblPathDirectory = false;
            if (!string.IsNullOrEmpty(pblPath))
            {
                if (Path.GetPathRoot(pblPath) == "")
                {
                    pblPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), pblPath);
                }

                // PBLがファイル指定だった場合は出力して終わり
                if (File.Exists(pblPath))
                {
                    pblPaths = new string[] { pblPath };
                    outputPath = Path.Combine(outputDirectory, Path.GetFileNameWithoutExtension(pblPath));
                    if (!Directory.Exists(outputPath))
                    {
                        Directory.CreateDirectory(outputPath);
                    }
                    if (string.IsNullOrWhiteSpace(objectName) || string.IsNullOrWhiteSpace(objectType))
                    {
                        objectName = PbSupport.BulkExport;
                        objectType = "0";
                    }
                }

                // PBLがディレクトリ指定だった場合はディレクトリ内のPBLをすべて出力
                if (Directory.Exists(pblPath))
                {
                    isPblPathDirectory = true;
                    if (recursive)
                    {
                        var pathList = new List<string>();
                        findRecursivePath(pblPath, pathList);
                        pblPaths = pathList.ToArray();
                    }
                    else
                    {
                        pblPaths = Directory.GetFiles(pblPath, "*.pbl");
                    }

                    objectName = PbSupport.BulkExport;
                    objectType = "0";
                }
            }

            if (!string.IsNullOrEmpty(workspacePath))
            {
                var exporter = new export.PblExporter(supporter);
                exporter.ExportByWorkspace(version, workspacePath, outputHeader, outputDirectory, outputDelete);
                return;

                if (version < 8)
                {
                    throw new Exception("PB8未満はワークスペースをサポートしていません。");
                }

                objectName = PbSupport.BulkExport;
                objectType = "0";

                var pbtPaths = findPbtPathsByWorkspace(workspacePath);
                var pblPaths2 = new List<string>();
                foreach (var pbtPath in pbtPaths)
                {
                    pblPaths2.AddRange(findPblPathsByPbt(pbtPath).Where(x => !pblPaths2.Contains(x)));
                }

                pblPaths = pblPaths2.ToArray();

                if (pblPaths.Length == 0)
                {
                    Application.Run(new MainForm());
                    return;
                }

                // 出力先ディレクトリの中身をすべて削除する
                if (outputDelete)
                {
                    DirectoryInfo directory = new DirectoryInfo(outputDirectory);
                    directory.EnumerateFiles().ToList().ForEach(f => f.Delete());
                    directory.EnumerateDirectories().ToList().ForEach(d => d.Delete(true));
                }

                foreach (var path in pblPaths)
                {
                    outputPath = outputDirectory;
                    var relativePath = new Uri(Path.GetDirectoryName(workspacePath)).MakeRelativeUri(new Uri(Path.GetDirectoryName(path))).ToString().Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
                    // pblの相対パスがワークスペースと同一の場合、ワークスペースの存在するフォルダとする
                    if (relativePath == "")
                    {
                        relativePath = new Uri(Path.GetDirectoryName(Path.GetDirectoryName(workspacePath)) + Path.DirectorySeparatorChar).MakeRelativeUri(new Uri(Path.GetDirectoryName(path))).ToString().Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
                    }

                    outputPath = Path.Combine(outputPath, relativePath);
                    outputPath = Path.Combine(outputPath, Path.GetFileNameWithoutExtension(path));
                    if (!Directory.Exists(outputPath))
                    {
                        Directory.CreateDirectory(outputPath);
                    }

                    supporter.Export(path, objectName, PbSupport.GetEntryType(objectType), outputHeader, outputPath);
                }
                return;
            }





            if (pblPaths.Length == 0)
            {
                Application.Run(new MainForm());
                return;
            }
            foreach (var path in pblPaths)
            {
                if (isPblPathDirectory)
                {
                    outputPath = outputDirectory;
                    if (preserve)
                    {
                        outputPath = Path.Combine(outputPath, Path.GetDirectoryName(path).Replace(pblPath, "").Trim(Path.DirectorySeparatorChar));
                    }
                    outputPath = Path.Combine(outputPath, Path.GetFileNameWithoutExtension(path));
                    if (!Directory.Exists(outputPath))
                    {
                        Directory.CreateDirectory(outputPath);
                    }
                }

                supporter.Export(path, objectName, PbSupport.GetEntryType(objectType), outputHeader, outputPath);
            }
        }

        /// <summary>
        /// 再帰的にディレクトリを操作してすべてのpblパスを得る。
        /// </summary>
        /// <param name="path"></param>
        /// <param name="paths"></param>
        private static void findRecursivePath(string path, List<string> paths)
        {
            if (Path.GetExtension(path) == ".pbl")
            {
                paths.Add(path);
            }

            if (Directory.Exists(path))
            {
                var files = Directory.GetFiles(path, "*.pbl");
                foreach (var filePath in files)
                {
                    findRecursivePath(filePath, paths);
                }

                var directories = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);
                foreach (var directory in directories)
                {
                    findRecursivePath(directory, paths);
                }
            }
        }

        /// <summary>
        /// ワークスペースからpbtのパスを求める。
        /// </summary>
        /// <param name="workspacePath"></param>
        /// <returns></returns>
        private static List<string> findPbtPathsByWorkspace(string workspacePath)
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
                // ワークスペースのパスからのpbtのフルパスを求める
                // NOTE: 全角文字はUnicodeエンコードされているため、デコードしたものを把握する
                result.Add(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(workspacePath), Regex.Unescape(matchPblPath.Groups[2].Value))));
            }

            return result;
        }

        /// <summary>
        /// pbtからpblのパスを求める。
        /// </summary>
        /// <param name="pbtPath"></param>
        /// <returns></returns>
        private static List<string> findPblPathsByPbt(string pbtPath)
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
                // pbtのパスからのpblのフルパスを求める
                // NOTE: 全角文字はUnicodeエンコードされているため、デコードしたものを把握する
                result.Add(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(pbtPath), Regex.Unescape(pblRelativePath))));
            }

            return result;
        }
    }
}
