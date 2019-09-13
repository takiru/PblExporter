namespace PblExporter
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pblListBox = new System.Windows.Forms.ListBox();
            this.pblAddButton = new System.Windows.Forms.Button();
            this.pblRemoveButton = new System.Windows.Forms.Button();
            this.pblClearButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.objectListView = new System.Windows.Forms.ListView();
            this.objectNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.objectTypeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.updateDateColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.commentColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.outputHeaderCheckBox = new System.Windows.Forms.CheckBox();
            this.selectSaveDirectoryButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.saveDirectoryTextBox = new Metroit.Windows.Forms.MetTextBox();
            this.exportObjectButton = new System.Windows.Forms.Button();
            this.exportPblButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pbSelectComboBox = new Metroit.Windows.Forms.MetComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.saveDirectoryTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pblListBox
            // 
            this.pblListBox.AllowDrop = true;
            this.pblListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pblListBox.FormattingEnabled = true;
            this.pblListBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.pblListBox.ItemHeight = 18;
            this.pblListBox.Location = new System.Drawing.Point(7, 96);
            this.pblListBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pblListBox.Name = "pblListBox";
            this.pblListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.pblListBox.Size = new System.Drawing.Size(276, 688);
            this.pblListBox.TabIndex = 5;
            this.pblListBox.SelectedIndexChanged += new System.EventHandler(this.pblListBox_SelectedIndexChanged);
            this.pblListBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.pblListBox_DragDrop);
            this.pblListBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.pblListBox_DragEnter);
            this.pblListBox.DoubleClick += new System.EventHandler(this.pblListBox_DoubleClick);
            // 
            // pblAddButton
            // 
            this.pblAddButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pblAddButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightCyan;
            this.pblAddButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pblAddButton.Location = new System.Drawing.Point(7, 27);
            this.pblAddButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pblAddButton.Name = "pblAddButton";
            this.pblAddButton.Size = new System.Drawing.Size(87, 34);
            this.pblAddButton.TabIndex = 2;
            this.pblAddButton.Text = "追加(&A)";
            this.pblAddButton.UseVisualStyleBackColor = false;
            this.pblAddButton.Click += new System.EventHandler(this.pblAddButton_Click);
            // 
            // pblRemoveButton
            // 
            this.pblRemoveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pblRemoveButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightCyan;
            this.pblRemoveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pblRemoveButton.Location = new System.Drawing.Point(101, 27);
            this.pblRemoveButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pblRemoveButton.Name = "pblRemoveButton";
            this.pblRemoveButton.Size = new System.Drawing.Size(87, 34);
            this.pblRemoveButton.TabIndex = 3;
            this.pblRemoveButton.Text = "削除(&R)";
            this.pblRemoveButton.UseVisualStyleBackColor = false;
            this.pblRemoveButton.Click += new System.EventHandler(this.pblRemoveButton_Click);
            // 
            // pblClearButton
            // 
            this.pblClearButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pblClearButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightCyan;
            this.pblClearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pblClearButton.Location = new System.Drawing.Point(196, 27);
            this.pblClearButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pblClearButton.Name = "pblClearButton";
            this.pblClearButton.Size = new System.Drawing.Size(87, 34);
            this.pblClearButton.TabIndex = 4;
            this.pblClearButton.Text = "クリア(&C)";
            this.pblClearButton.UseVisualStyleBackColor = false;
            this.pblClearButton.Click += new System.EventHandler(this.pblClearButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 18);
            this.label1.TabIndex = 8;
            this.label1.Text = "バージョン";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.pblListBox);
            this.groupBox1.Controls.Add(this.pblAddButton);
            this.groupBox1.Controls.Add(this.pblRemoveButton);
            this.groupBox1.Controls.Add(this.pblClearButton);
            this.groupBox1.Location = new System.Drawing.Point(16, 62);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(300, 800);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PBL";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(236, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "ダブルクリックでオブジェクト一覧を表示";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.objectListView);
            this.groupBox2.Location = new System.Drawing.Point(323, 62);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(842, 669);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "オブジェクト一覧";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(176, 18);
            this.label4.TabIndex = 12;
            this.label4.Text = "ダブルクリックでコードを表示";
            // 
            // objectListView
            // 
            this.objectListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.objectNameColumnHeader,
            this.objectTypeColumnHeader,
            this.updateDateColumnHeader,
            this.commentColumnHeader});
            this.objectListView.FullRowSelect = true;
            this.objectListView.Location = new System.Drawing.Point(7, 49);
            this.objectListView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.objectListView.Name = "objectListView";
            this.objectListView.Size = new System.Drawing.Size(828, 609);
            this.objectListView.TabIndex = 6;
            this.objectListView.UseCompatibleStateImageBehavior = false;
            this.objectListView.View = System.Windows.Forms.View.Details;
            this.objectListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.objectListView_ColumnClick);
            this.objectListView.DoubleClick += new System.EventHandler(this.objectListView_DoubleClick);
            // 
            // objectNameColumnHeader
            // 
            this.objectNameColumnHeader.Text = "オブジェクト名";
            this.objectNameColumnHeader.Width = 200;
            // 
            // objectTypeColumnHeader
            // 
            this.objectTypeColumnHeader.Text = "オブジェクト形式";
            this.objectTypeColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.objectTypeColumnHeader.Width = 120;
            // 
            // updateDateColumnHeader
            // 
            this.updateDateColumnHeader.Text = "最終更新日時";
            this.updateDateColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.updateDateColumnHeader.Width = 140;
            // 
            // commentColumnHeader
            // 
            this.commentColumnHeader.Text = "コメント";
            this.commentColumnHeader.Width = 350;
            // 
            // outputHeaderCheckBox
            // 
            this.outputHeaderCheckBox.AutoSize = true;
            this.outputHeaderCheckBox.Checked = true;
            this.outputHeaderCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.outputHeaderCheckBox.Location = new System.Drawing.Point(330, 32);
            this.outputHeaderCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.outputHeaderCheckBox.Name = "outputHeaderCheckBox";
            this.outputHeaderCheckBox.Size = new System.Drawing.Size(183, 22);
            this.outputHeaderCheckBox.TabIndex = 11;
            this.outputHeaderCheckBox.TabStop = false;
            this.outputHeaderCheckBox.Text = "ファイルヘッダーを付与する";
            this.outputHeaderCheckBox.UseVisualStyleBackColor = true;
            // 
            // selectSaveDirectoryButton
            // 
            this.selectSaveDirectoryButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.selectSaveDirectoryButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightCyan;
            this.selectSaveDirectoryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectSaveDirectoryButton.Location = new System.Drawing.Point(796, 20);
            this.selectSaveDirectoryButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.selectSaveDirectoryButton.Name = "selectSaveDirectoryButton";
            this.selectSaveDirectoryButton.Size = new System.Drawing.Size(28, 25);
            this.selectSaveDirectoryButton.TabIndex = 8;
            this.selectSaveDirectoryButton.Text = "...";
            this.selectSaveDirectoryButton.UseVisualStyleBackColor = false;
            this.selectSaveDirectoryButton.Click += new System.EventHandler(this.selectSaveDirectoryButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.saveDirectoryTextBox);
            this.groupBox3.Controls.Add(this.exportObjectButton);
            this.groupBox3.Controls.Add(this.exportPblButton);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.selectSaveDirectoryButton);
            this.groupBox3.Location = new System.Drawing.Point(323, 740);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(842, 122);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "エクスポート";
            // 
            // saveDirectoryTextBox
            // 
            this.saveDirectoryTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.saveDirectoryTextBox.CustomAutoCompleteBox.TargetControl = this.saveDirectoryTextBox;
            this.saveDirectoryTextBox.FocusBackColor = System.Drawing.Color.LightCyan;
            this.saveDirectoryTextBox.FocusBorderColor = System.Drawing.Color.MediumAquamarine;
            this.saveDirectoryTextBox.Location = new System.Drawing.Point(50, 20);
            this.saveDirectoryTextBox.Name = "saveDirectoryTextBox";
            this.saveDirectoryTextBox.Size = new System.Drawing.Size(745, 25);
            this.saveDirectoryTextBox.TabIndex = 7;
            // 
            // exportObjectButton
            // 
            this.exportObjectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.exportObjectButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightCyan;
            this.exportObjectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exportObjectButton.Location = new System.Drawing.Point(656, 80);
            this.exportObjectButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.exportObjectButton.Name = "exportObjectButton";
            this.exportObjectButton.Size = new System.Drawing.Size(167, 34);
            this.exportObjectButton.TabIndex = 10;
            this.exportObjectButton.Text = "オブジェクトエクスポート";
            this.exportObjectButton.UseVisualStyleBackColor = false;
            this.exportObjectButton.Click += new System.EventHandler(this.exportObjectButton_Click);
            // 
            // exportPblButton
            // 
            this.exportPblButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.exportPblButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightCyan;
            this.exportPblButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exportPblButton.Location = new System.Drawing.Point(483, 80);
            this.exportPblButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.exportPblButton.Name = "exportPblButton";
            this.exportPblButton.Size = new System.Drawing.Size(167, 34);
            this.exportPblButton.TabIndex = 9;
            this.exportPblButton.Text = "PBLエクスポート";
            this.exportPblButton.UseVisualStyleBackColor = false;
            this.exportPblButton.Click += new System.EventHandler(this.exportPblButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 18);
            this.label2.TabIndex = 14;
            this.label2.Text = "保存先";
            // 
            // pbSelectComboBox
            // 
            this.pbSelectComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.pbSelectComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pbSelectComboBox.FocusBackColor = System.Drawing.Color.LightCyan;
            this.pbSelectComboBox.FocusBorderColor = System.Drawing.Color.MediumAquamarine;
            this.pbSelectComboBox.FormattingEnabled = true;
            this.pbSelectComboBox.Location = new System.Drawing.Point(88, 11);
            this.pbSelectComboBox.Name = "pbSelectComboBox";
            this.pbSelectComboBox.Size = new System.Drawing.Size(140, 26);
            this.pbSelectComboBox.TabIndex = 1;
            this.pbSelectComboBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.pbSelectComboBox_DrawItem);
            this.pbSelectComboBox.SelectedIndexChanged += new System.EventHandler(this.pbSelectComboBox_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 879);
            this.Controls.Add(this.pbSelectComboBox);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.outputHeaderCheckBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PbExporter";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.saveDirectoryTextBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox pblListBox;
        private System.Windows.Forms.Button pblAddButton;
        private System.Windows.Forms.Button pblRemoveButton;
        private System.Windows.Forms.Button pblClearButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView objectListView;
        private System.Windows.Forms.ColumnHeader objectNameColumnHeader;
        private System.Windows.Forms.ColumnHeader objectTypeColumnHeader;
        private System.Windows.Forms.ColumnHeader updateDateColumnHeader;
        private System.Windows.Forms.ColumnHeader commentColumnHeader;
        private System.Windows.Forms.CheckBox outputHeaderCheckBox;
        private System.Windows.Forms.Button selectSaveDirectoryButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button exportObjectButton;
        private System.Windows.Forms.Button exportPblButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private Metroit.Windows.Forms.MetComboBox pbSelectComboBox;
        private Metroit.Windows.Forms.MetTextBox saveDirectoryTextBox;
    }
}