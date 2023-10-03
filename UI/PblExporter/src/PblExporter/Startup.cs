using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CommandLine;
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

            // サイレント実行できない場合はウィンドウ表示
            IPbSupporter supporter;
            if (!CanSilentExecute(CommandLineOptions.Value, out supporter))
            {
                Application.Run(new MainForm());
                return;
            }

            ExecuteSilent(CommandLineOptions.Value, supporter);
        }

        /// <summary>
        /// サイレント実行が可能かどうかを取得する。
        /// </summary>
        /// <param name="options">コマンドラインオプション。</param>
        /// <param name="supporter">PBサポーター。</param>
        /// <returns>true:実行可能, false:実行不可。</returns>
        private static bool CanSilentExecute(CommandLineOptions options, out IPbSupporter supporter)
        {
            supporter = null;

            // コマンドラインオプションが解析できない
            if (options == null)
            {
                return false;
            }

            // プラグインを取得できない
            supporter = Plugins.PluginList.FirstOrDefault((x) => x.Version == options.Version);
            if (supporter == null)
            {
                return false;
            }

            // PBLもワークスペースも選択されてきていない
            if (string.IsNullOrEmpty(options.PblPath) && string.IsNullOrEmpty(options.WorkspacePath))
            {
                return false;
            }

            // 指定されたワークスペースが存在しない
            if (!string.IsNullOrEmpty(options.WorkspacePath) && !File.Exists(options.WorkspacePath))
            {
                return false;
            }

            // 指定されたpblフォルダ／ファイルが存在しない
            if (!Directory.Exists(options.PblPath) && !File.Exists(options.PblPath))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// サイレント実行を行う。
        /// </summary>
        /// <param name="options">コマンドラインオプション。</param>
        /// <param name="supporter">PBサポーター。</param>
        private static void ExecuteSilent(CommandLineOptions options, IPbSupporter supporter)
        {
            var exporter = new export.PblExporter(supporter);
            var outputDirectory = options.OutputDirectory;

            // 出力先が指定されていない場合は現行ディレクトリを対象とする
            if (string.IsNullOrWhiteSpace(outputDirectory))
            {
                outputDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }

            // ワークスペースからのエクスポート
            if (!string.IsNullOrEmpty(options.WorkspacePath))
            {
                exporter.ExportByWorkspace(options.Version, options.WorkspacePath, options.OutputHeader, outputDirectory, options.OutputDelete);
                return;
            }

            // pblディレクトリからのエクスポート
            if (Directory.Exists(options.PblPath))
            {
                exporter.ExportByDirectory(options.PblPath, options.OutputHeader, outputDirectory, options.Recursive, options.Preserve, options.OutputDelete);
                return;
            }

            // pblファイルからのエクスポート
            if (string.IsNullOrEmpty(options.ObjectName) && string.IsNullOrEmpty(options.ObjectType))
            {
                exporter.ExportByPbl(options.PblPath, options.OutputHeader, outputDirectory, options.OutputDelete);
            }
            else
            {
                exporter.ExportByFile(options.PblPath, options.ObjectName, options.ObjectType, options.OutputHeader, outputDirectory);
            }
        }
    }
}
