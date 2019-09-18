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
            var objectName = CommandLineOptions.Value.ObjectName;
            var objectType = CommandLineOptions.Value.ObjectType;
            var recursive = CommandLineOptions.Value.Recursive;
            var outputHeader = CommandLineOptions.Value.OutputHeader;
            var outputDirectory = CommandLineOptions.Value.OutputDirectory;

            // プラグインを取得できなかったらウィンドウ表示
            var supporter = Plugins.PluginList.FirstOrDefault((x) => x.Version == version);
            if (supporter == null)
            {
                Application.Run(new MainForm());
                return;
            }

            // PBLが取得できなかったらウィンドウ表示
            if (string.IsNullOrWhiteSpace(pblPath))
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
            bool isPblPathDirectory = false;
            if (Directory.Exists(pblPath))
            {
                isPblPathDirectory = true;
                if (recursive)
                {
                    var pathList = new List<string>();
                    findRecursivePath(pblPath, pathList);
                    pblPaths = pathList.ToArray();
                } else
                {
                    pblPaths = Directory.GetFiles(pblPath, "*.pbl");
                }
                
                objectName = PbSupport.BulkExport;
                objectType = "0";
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
                    outputPath = Path.Combine(outputDirectory, Path.GetFileNameWithoutExtension(path));
                    if (!Directory.Exists(outputPath))
                    {
                        Directory.CreateDirectory(outputPath);
                    }
                }

                supporter.Export(path, objectName, objectType, outputHeader, outputPath);
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
    }
}
