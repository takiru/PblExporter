using PblExporter.Core;
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

        public DialogResult ShowDialog(IPbSupporter supporter, PblData pblData, PblObjectData objectData, bool outputHeader)
        {
            ReadCode(supporter, pblData, objectData, outputHeader);
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
        private void ReadCode(IPbSupporter supporter, PblData pblData, PblObjectData objectData, bool outputHeader)
        {
            var outputDirectory = Path.GetTempPath();
            supporter.Export(pblData.FilePath, objectData.ObjectName, objectData.ObjectType, outputHeader, outputDirectory);
            var outputFilePath = Path.Combine(outputDirectory, objectData.ObjectName + PbSupport.GetExtension(objectData.ObjectType));
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
