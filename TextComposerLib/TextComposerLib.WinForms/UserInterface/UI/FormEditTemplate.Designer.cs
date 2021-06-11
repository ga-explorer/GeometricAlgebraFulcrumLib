using System.ComponentModel;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace TextComposerLib.WinForms.UserInterface.UI
{
    partial class FormEditTemplate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditTemplate));
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxLeftDelimiter = new System.Windows.Forms.TextBox();
            this.textBoxRightDelimiter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxParameters = new System.Windows.Forms.ListBox();
            this.textBoxTemplateCode = new FastColoredTextBoxNS.FastColoredTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonDone = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxTemplateCode)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Delimiters:";
            // 
            // textBoxLeftDelimiter
            // 
            this.textBoxLeftDelimiter.Location = new System.Drawing.Point(95, 6);
            this.textBoxLeftDelimiter.Name = "textBoxLeftDelimiter";
            this.textBoxLeftDelimiter.Size = new System.Drawing.Size(100, 23);
            this.textBoxLeftDelimiter.TabIndex = 1;
            this.textBoxLeftDelimiter.TextChanged += new System.EventHandler(this.textBoxLeftDelimiter_TextChanged);
            // 
            // textBoxRightDelimiter
            // 
            this.textBoxRightDelimiter.Location = new System.Drawing.Point(201, 6);
            this.textBoxRightDelimiter.Name = "textBoxRightDelimiter";
            this.textBoxRightDelimiter.Size = new System.Drawing.Size(100, 23);
            this.textBoxRightDelimiter.TabIndex = 2;
            this.textBoxRightDelimiter.TextChanged += new System.EventHandler(this.textBoxRightDelimiter_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Parameters:";
            // 
            // listBoxParameters
            // 
            this.listBoxParameters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxParameters.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxParameters.FormattingEnabled = true;
            this.listBoxParameters.IntegralHeight = false;
            this.listBoxParameters.ItemHeight = 18;
            this.listBoxParameters.Location = new System.Drawing.Point(12, 56);
            this.listBoxParameters.Name = "listBoxParameters";
            this.listBoxParameters.Size = new System.Drawing.Size(183, 340);
            this.listBoxParameters.Sorted = true;
            this.listBoxParameters.TabIndex = 4;
            // 
            // textBoxTemplateCode
            // 
            this.textBoxTemplateCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTemplateCode.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.textBoxTemplateCode.AutoIndent = false;
            this.textBoxTemplateCode.AutoIndentChars = false;
            this.textBoxTemplateCode.AutoIndentExistingLines = false;
            this.textBoxTemplateCode.AutoScrollMinSize = new System.Drawing.Size(27, 17);
            this.textBoxTemplateCode.BackBrush = null;
            this.textBoxTemplateCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxTemplateCode.CharHeight = 17;
            this.textBoxTemplateCode.CharWidth = 8;
            this.textBoxTemplateCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxTemplateCode.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.textBoxTemplateCode.Font = new System.Drawing.Font("Consolas", 11.25F);
            this.textBoxTemplateCode.IsReplaceMode = false;
            this.textBoxTemplateCode.Location = new System.Drawing.Point(201, 56);
            this.textBoxTemplateCode.Name = "textBoxTemplateCode";
            this.textBoxTemplateCode.Paddings = new System.Windows.Forms.Padding(0);
            this.textBoxTemplateCode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.textBoxTemplateCode.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("textBoxTemplateCode.ServiceColors")));
            this.textBoxTemplateCode.Size = new System.Drawing.Size(411, 340);
            this.textBoxTemplateCode.TabIndex = 5;
            this.textBoxTemplateCode.Zoom = 100;
            this.textBoxTemplateCode.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.textBoxTemplateCode_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(198, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Text:";
            // 
            // buttonDone
            // 
            this.buttonDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDone.Location = new System.Drawing.Point(429, 402);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(183, 32);
            this.buttonDone.TabIndex = 7;
            this.buttonDone.Text = "Update and &Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLoad.Location = new System.Drawing.Point(429, 18);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(183, 32);
            this.buttonLoad.TabIndex = 8;
            this.buttonLoad.Text = "&Load Text from File";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonUpdate.Location = new System.Drawing.Point(12, 402);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(183, 32);
            this.buttonUpdate.TabIndex = 9;
            this.buttonUpdate.Text = "&Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FormEditTemplate
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(624, 446);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxTemplateCode);
            this.Controls.Add(this.listBoxParameters);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxRightDelimiter);
            this.Controls.Add(this.textBoxLeftDelimiter);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "FormEditTemplate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormEditTemplate";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEditTemplate_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.textBoxTemplateCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox textBoxLeftDelimiter;
        private TextBox textBoxRightDelimiter;
        private Label label2;
        private ListBox listBoxParameters;
        private FastColoredTextBox textBoxTemplateCode;
        private Label label3;
        private Button buttonDone;
        private Button buttonLoad;
        private Button buttonUpdate;
        private OpenFileDialog openFileDialog1;
    }
}