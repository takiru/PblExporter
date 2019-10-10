using PblExporter.Core;
using PblExporter.Core.Orca;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PblExporter
{
    /// <summary>
    /// コード表示ウィンドウ。
    /// </summary>
    public partial class CodeForm : Form
    {
        public CodeForm()
        {
            InitializeComponent();
        }

        public DialogResult ShowDialog(IPbSupporter supporter, PblData pblData, ObjectInfo ObjectInfo, bool outputHeader)
        {
            ReadCode(supporter, pblData, ObjectInfo, outputHeader);
            codeMetTextBox.SelectionStart = 0;
            return this.ShowDialog();
        }

        /// <summary>
        /// コードを読み込む。
        /// </summary>
        /// <param name="supporter"></param>
        /// <param name="pblData"></param>
        /// <param name="objectData"></param>
        /// <param name="outputHeader"></param>
        private void ReadCode(IPbSupporter supporter, PblData pblData, ObjectInfo ObjectInfo, bool outputHeader)
        {
            var outputDirectory = Path.GetTempPath();
            supporter.Export(pblData.FilePath, ObjectInfo.ObjectName, ObjectInfo.EntryType, outputHeader, outputDirectory);
            var outputFilePath = Path.Combine(outputDirectory, ObjectInfo.ObjectName + PbSupport.GetExtension(PbSupport.GetObjectType(ObjectInfo.EntryType)));
            var code = File.ReadAllText(outputFilePath, supporter.FileEncoding);
            try
            {
                File.Delete(outputFilePath);
            }
            catch
            {
            }

            codeMetTextBox.Text = code;
        }

        /// <summary>
        /// クリップボードにコピーする。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(codeMetTextBox.Text);
            MessageBox.Show("クリップボードにコピーしました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
