using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PblExporter
{
    /// <summary>
    /// コマンドラインオプション。
    /// </summary>
    class CommandLineOptions
    {
        /// <summary>
        /// コマンドラインデータ。
        /// </summary>
        public static CommandLineOptions Value { get; set; } = null;

        /// <summary>
        /// PBバージョン。
        /// </summary>
        [Option("ver", Required = true, Default = -1)]
        public decimal Version { get; set; }

        /// <summary>
        /// PBL名。
        /// </summary>
        [Option("pbl", Required = true)]
        public string PblPath { get; set; }

        /// <summary>
        /// オブジェクト名。
        /// </summary>
        [Option("name", Required = false)]
        public string ObjectName { get; set; }

        /// <summary>
        /// オブジェクトタイプ名。
        /// </summary>
        [Option("type", Required = false)]
        public string ObjectType { get; set; }

        /// <summary>
        /// ディレクトリの再帰的読み込み。
        /// </summary>
        [Option("recursive", Required = false, Default = false)]
        public bool Recursive { get; set; }

        /// <summary>
        /// ヘッダー出力をするかどうか。
        /// </summary>
        [Option("header", Required = false, Default = false)]
        public bool OutputHeader { get; set; }

        /// <summary>
        /// 出力パス。
        /// </summary>
        [Option("out", Required = false, Default = null)]
        public string OutputDirectory { get; set; }

        /// <summary>
        /// PBL名がディレクトリ指定だった時、出力パスにPBLのディレクトリ構成を保持するかどうか。
        /// </summary>
        [Option("preserve", Required = false, Default = false)]
        public bool Preserve { get; set; }
    }
}
