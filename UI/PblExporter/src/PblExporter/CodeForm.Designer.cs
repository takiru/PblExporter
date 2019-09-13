namespace PblExporter
{
    partial class CodeForm
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
            this.codeMetTextBox = new Metroit.Windows.Forms.MetTextBox();
            this.copyButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.codeMetTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // codeMetTextBox
            // 
            this.codeMetTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.codeMetTextBox.BaseBackColor = System.Drawing.SystemColors.Window;
            this.codeMetTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.codeMetTextBox.CustomAutoCompleteBox.TargetControl = this.codeMetTextBox;
            this.codeMetTextBox.FocusBackColor = System.Drawing.Color.LightCyan;
            this.codeMetTextBox.FocusBorderColor = System.Drawing.Color.MediumAquamarine;
            this.codeMetTextBox.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.codeMetTextBox.Location = new System.Drawing.Point(12, 12);
            this.codeMetTextBox.MaxLength = 2147483647;
            this.codeMetTextBox.Multiline = true;
            this.codeMetTextBox.MultilineSelectAll = true;
            this.codeMetTextBox.Name = "codeMetTextBox";
            this.codeMetTextBox.ReadOnly = true;
            this.codeMetTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.codeMetTextBox.Size = new System.Drawing.Size(920, 493);
            this.codeMetTextBox.TabIndex = 0;
            this.codeMetTextBox.WordWrap = false;
            // 
            // copyButton
            // 
            this.copyButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.copyButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.copyButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightCyan;
            this.copyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.copyButton.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.copyButton.Location = new System.Drawing.Point(435, 511);
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(75, 34);
            this.copyButton.TabIndex = 1;
            this.copyButton.Text = "コピー(&C)";
            this.copyButton.UseVisualStyleBackColor = false;
            this.copyButton.Click += new System.EventHandler(this.copyButton_Click);
            // 
            // CodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 557);
            this.Controls.Add(this.copyButton);
            this.Controls.Add(this.codeMetTextBox);
            this.MinimizeBox = false;
            this.Name = "CodeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "コード";
            ((System.ComponentModel.ISupportInitialize)(this.codeMetTextBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Metroit.Windows.Forms.MetTextBox codeMetTextBox;
        private System.Windows.Forms.Button copyButton;
    }
}