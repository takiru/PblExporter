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
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Reflection;
using System.Collections;

namespace PblExporter
{
    public partial class MainForm : Form
    {
        private BindingSource bindingSource = new BindingSource();
        private List<PblData> pblListBoxItem = new List<PblData>();

        public MainForm()
        {
            InitializeComponent();

            // PBバージョンのバインド
            pbSelectComboBox.DataSource = Plugins.PluginList;
            pbSelectComboBox.DisplayMember = "PbVersion";
            pbSelectComboBox.SelectedItem = null;

            // リストボックスのバインド
            bindingSource.DataSource = pblListBoxItem;
            pblListBox.DataSource = bindingSource;
            pblListBox.DisplayMember = "FileName";
            pblListBox.ValueMember = "FilePath";
        }

        /// <summary>
        /// PBLファイルをリストに追加する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pblAddButton_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog()
            {
                Filter = "(*.pbl)|*.pbl",
                Multiselect = true,
                RestoreDirectory = true
            };
            if (ofd.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            foreach (var fileName in ofd.FileNames)
            {
                // 既に登録されている場合は何もしない
                var count = pblListBoxItem.Count((x) => x.FilePath == fileName);
                if (count > 0)
                {
                    return;
                }

                pblListBoxItem.Add(new PblData(fileName));
            }

            pblListBoxItem.Sort((a, b) => string.Compare(a.FileName, b.FileName));
            bindingSource.ResetBindings(false);
        }

        /// <summary>
        /// PBLファイルをリストから削除する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pblRemoveButton_Click(object sender, EventArgs e)
        {
            foreach (var item in pblListBox.SelectedItems)
            {
                pblListBoxItem.Remove((PblData)item);
            }
            bindingSource.ResetBindings(false);
        }

        /// <summary>
        /// PBLファイルをクリアする。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pblClearButton_Click(object sender, EventArgs e)
        {
            pblListBoxItem.Clear();
            bindingSource.ResetBindings(false);
        }

        /// <summary>
        /// pblファイルのドラッグを許可。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pblListBox_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            var files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (var file in files)
            {
                if (Path.GetExtension(file) == ".pbl")
                {
                    e.Effect = DragDropEffects.All;
                    return;
                }
            }
            e.Effect = DragDropEffects.None;
        }

        /// <summary>
        /// ドラッグしたpblファイルをリストに追加。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pblListBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (var file in files)
            {
                if (Path.GetExtension(file) != ".pbl")
                {
                    continue;
                }

                // 既に登録されている場合は何もしない
                var count = pblListBoxItem.Count((x) => x.FilePath == file);
                if (count > 0)
                {
                    continue;
                }

                pblListBoxItem.Add(new PblData(file));

            }
            pblListBoxItem.Sort((a, b) => string.Compare(a.FileName, b.FileName));
            bindingSource.ResetBindings(false);
        }

        private void pblListBox_DoubleClick(object sender, EventArgs e)
        {
            if (pbSelectComboBox.SelectedItem == null)
            {
                return;
            }

            var supporter = (IPbSupporter)pbSelectComboBox.SelectedItem;

            var pblData = (PblData)pblListBox.SelectedItem;
            var result = supporter.GetObjectList(pblData.FilePath);

            objectListView.Items.Clear();
            foreach (var item in result)
            {
                var li = new ListViewItem(item.ObjectName);
                li.SubItems.Add(new ListViewItem.ListViewSubItem(li, item.ObjectType));
                li.SubItems.Add(new ListViewItem.ListViewSubItem(li, item.UpdateDate.ToString("yyyy/MM/dd HH:mm:ss")));
                li.SubItems.Add(new ListViewItem.ListViewSubItem(li, item.Comment));
                li.Tag = item;
                objectListView.Items.Add(li);
            }
            objectListView_ColumnClick(objectListView, new ColumnClickEventArgs(0));
        }

        /// <summary>
        /// 保存先フォルダを指定する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectSaveDirectoryButton_Click(object sender, EventArgs e)
        {
            var cofd = new CommonOpenFileDialog();
            cofd.IsFolderPicker = true;
            cofd.EnsureReadOnly = false;
            cofd.AllowNonFileSystemItems = false;
            if (string.IsNullOrWhiteSpace(saveDirectoryTextBox.Text))
            {
                cofd.InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
            cofd.RestoreDirectory = true;
            cofd.EnsurePathExists = true;

            if (cofd.ShowDialog() == CommonFileDialogResult.Cancel)
            {
                return;
            }
            saveDirectoryTextBox.Text = cofd.FileName;
        }

        /// <summary>
        /// PBLファイルの中身をすべてエクスポートする。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportPblButton_Click(object sender, EventArgs e)
        {
            if (!ValidateExport(false))
            {
                return;
            }

            var supporter = (IPbSupporter)pbSelectComboBox.SelectedItem;

            foreach (var pblData in pblListBox.SelectedItems.OfType<PblData>())
            {
                var outputDirectory = Path.Combine(saveDirectoryTextBox.Text, pblData.FileName);
                if (!Directory.Exists(outputDirectory))
                {
                    Directory.CreateDirectory(outputDirectory);
                }
                supporter.Export(pblData.FilePath, PbSupport.BulkExport, "0", outputHeaderCheckBox.Checked, outputDirectory);
            }
        }

        /// <summary>
        /// 対象オブジェクトをすべてエクスポートする。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportObjectButton_Click(object sender, EventArgs e)
        {
            if (!ValidateExport(true))
            {
                return;
            }

            var supporter = (IPbSupporter)pbSelectComboBox.SelectedItem;

            var pblData = (PblData)pblListBox.SelectedItem;
            var outputDirectory = Path.Combine(saveDirectoryTextBox.Text, pblData.FileName);
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }
            foreach (var row in objectListView.SelectedItems.OfType<ListViewItem>())
            {
                var objectData = (PblObjectData)row.Tag;
                supporter.Export(pblData.FilePath, objectData.ObjectName, objectData.ObjectType, outputHeaderCheckBox.Checked, outputDirectory);
            }
        }

        private bool ValidateExport(bool objectExport)
        {
            if (pbSelectComboBox.SelectedItem == null)
            {
                MessageBox.Show("対象バージョンを選択してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                pbSelectComboBox.Focus();
                return false;
            }

            if (pblListBox.SelectedItems.Count == 0)
            {
                MessageBox.Show("PBLファイルを選択してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                pblListBox.Focus();
                return false;
            }

            if (objectExport)
            {
                if (objectListView.SelectedItems.Count == 0)
                {
                    MessageBox.Show("オブジェクトを選択してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    objectListView.Focus();
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(saveDirectoryTextBox.Text))
            {
                MessageBox.Show("保存先を入力してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                saveDirectoryTextBox.Focus();
                return false;
            }

            if (MessageBox.Show("実行します。よろしいですか？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return false;
            }

            return true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (ActiveControl == pblListBox || ActiveControl == objectListView)
            {
                if (keyData == (Keys.Control | Keys.A))
                {
                    AllSelect();
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// 全選択を行う。
        /// </summary>
        private void AllSelect()
        {
            if (ActiveControl == objectListView)
            {
                objectListView.Items[0].Focused = true;
                objectListView.Items[0].Selected = true;
            }
            SendKeys.SendWait("{HOME}+{END}");
        }

        /// <summary>
        /// オブジェクトの中身のコードを取得する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void objectListView_DoubleClick(object sender, EventArgs e)
        {
            var supporter = (IPbSupporter)pbSelectComboBox.SelectedItem;
            var pblData = (PblData)pblListBox.SelectedItem;
            var objectData = (PblObjectData)objectListView.SelectedItems[0].Tag;

            var form = new CodeForm();
            form.ShowDialog(supporter, pblData, objectData, outputHeaderCheckBox.Checked);
        }

        private bool asc = true;

        /// <summary>
        /// 並び替えを行う。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void objectListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // 初めての列は昇順、同じ列は順序切り替え
            if (objectListView.Tag == null || (int)objectListView.Tag != e.Column)
            {
                asc = true;
            }
            else
            {
                asc = !asc;
            }

            objectListView.ListViewItemSorter = new ListViewItemComparer(e.Column, asc);
            objectListView.Tag = e.Column;
        }

        /// <summary>
        /// PBバージョンを変えた時はオブジェクト一覧を消去。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbSelectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            objectListView.Items.Clear();
        }
    }
}
