using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewPblParser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var path = @"D:\PB\PB125\src\pbobjectparser.pbl";

            //List<LibEntry> libs = new Orca(PbVersion.PB100).DirLibrary(path);

            //Orca orca = new Orca(PbVersion.PB100);
            //var result = orca.SetCodeHeaderAndExportInfo();
            //orca.FillCode(libs[0]);

            var orca = new Orca(new Orca125.Orca125Executor());
            orca.OpenSession();

            List<ObjectInfo> libs;
            orca.GetLibraryDirectory(path, out libs);

            var config = new PBORCA_CONFIG_SESSION();
            config.bExportHeaders = true;
            config.eExportEncoding = PBORCA_ENCODING.PBORCA_UNICODE;
            config.bExportCreateFile = true;
            config.pExportDirectory = @"D:\abc";

            var code = "";
            orca.Export(libs[0], out code, config);
        }
    }
}
