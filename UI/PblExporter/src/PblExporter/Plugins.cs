using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PblExporter.Core;
using System.IO;
using System.Windows.Forms;

namespace PblExporter
{
    class Plugins
    {
        private static DirectoryCatalog mefs = null;
        private static AggregateCatalog catalogs = null;
        private static CompositionContainer container = null;

        /// <summary>
        /// プラグイン情報を取得します。
        /// </summary>
        public static List<IPbSupporter> PluginList { get; private set; }

        /// <summary>
        /// プラグインを有するディレクトリを取得します。
        /// </summary>
        public static string PluginDirectory { get; private set; }

        /// <summary>
        /// プラグインを読み込みます。
        /// </summary>
        public static bool Load()
        {
            // プラグイン読み込み準備
            if (!Prepare())
            {
                return false;
            }

            // プラグインの読み込み
            PluginList = container.GetExportedValues<IPbSupporter>().ToList();
            PluginList.Sort((a, b) => (int)(a.Version - b.Version));
            return true;
        }

        /// <summary>
        /// プラグインの読み込み準備を行う。
        /// </summary>
        private static bool Prepare()
        {
            if (mefs != null)
            {
                return true;
            }

            try
            {
                // プラグインを読み込む
                PluginDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                PluginDirectory = Path.Combine(PluginDirectory, "Plugins");

                if (!Directory.Exists(PluginDirectory))
                {
                    Directory.CreateDirectory(PluginDirectory);
                }

                mefs = new DirectoryCatalog(PluginDirectory, "*.dll");
                catalogs = new AggregateCatalog(mefs);
                container = new CompositionContainer(catalogs);
                return true;

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
