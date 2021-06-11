using System.ComponentModel;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace CodeComposerLib.WinForms.GraphViz.UserInterface
{
    partial class FormGraphVizEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGraphVizEditor));
            this.buttonClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxRenderMethod = new System.Windows.Forms.ComboBox();
            this.comboBoxOutputFormat = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageDotSource = new System.Windows.Forms.TabPage();
            this.textBoxDotSource = new FastColoredTextBoxNS.FastColoredTextBox();
            this.tabPageRendererOutput = new System.Windows.Forms.TabPage();
            this.textBoxRendererOutput = new System.Windows.Forms.TextBox();
            this.buttonRender = new System.Windows.Forms.Button();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.checkBoxVerbose = new System.Windows.Forms.CheckBox();
            this.checkBoxSaveToFile = new System.Windows.Forms.CheckBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxBinFolder = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPageDotSource.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxDotSource)).BeginInit();
            this.tabPageRendererOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(519, 403);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(93, 27);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "&Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Render Method:";
            // 
            // comboBoxRenderMethod
            // 
            this.comboBoxRenderMethod.FormattingEnabled = true;
            this.comboBoxRenderMethod.Location = new System.Drawing.Point(158, 39);
            this.comboBoxRenderMethod.Name = "comboBoxRenderMethod";
            this.comboBoxRenderMethod.Size = new System.Drawing.Size(141, 27);
            this.comboBoxRenderMethod.TabIndex = 2;
            // 
            // comboBoxOutputFormat
            // 
            this.comboBoxOutputFormat.FormattingEnabled = true;
            this.comboBoxOutputFormat.Location = new System.Drawing.Point(158, 72);
            this.comboBoxOutputFormat.Name = "comboBoxOutputFormat";
            this.comboBoxOutputFormat.Size = new System.Drawing.Size(141, 27);
            this.comboBoxOutputFormat.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "Output Format:";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPageDotSource);
            this.tabControl1.Controls.Add(this.tabPageRendererOutput);
            this.tabControl1.Location = new System.Drawing.Point(0, 105);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(621, 292);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPageDotSource
            // 
            this.tabPageDotSource.Controls.Add(this.textBoxDotSource);
            this.tabPageDotSource.Location = new System.Drawing.Point(4, 31);
            this.tabPageDotSource.Name = "tabPageDotSource";
            this.tabPageDotSource.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDotSource.Size = new System.Drawing.Size(613, 257);
            this.tabPageDotSource.TabIndex = 0;
            this.tabPageDotSource.Text = "Dot Code";
            this.tabPageDotSource.UseVisualStyleBackColor = true;
            // 
            // textBoxDotSource
            // 
            this.textBoxDotSource.AutoCompleteBracketsList = new char[] {
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
            this.textBoxDotSource.AutoScrollMinSize = new System.Drawing.Size(27, 17);
            this.textBoxDotSource.BackBrush = null;
            this.textBoxDotSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxDotSource.CharHeight = 17;
            this.textBoxDotSource.CharWidth = 8;
            this.textBoxDotSource.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxDotSource.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.textBoxDotSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxDotSource.Font = new System.Drawing.Font("Consolas", 11.25F);
            this.textBoxDotSource.IsReplaceMode = false;
            this.textBoxDotSource.Location = new System.Drawing.Point(3, 3);
            this.textBoxDotSource.Name = "textBoxDotSource";
            this.textBoxDotSource.Paddings = new System.Windows.Forms.Padding(0);
            this.textBoxDotSource.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.textBoxDotSource.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("textBoxDotSource.ServiceColors")));
            this.textBoxDotSource.Size = new System.Drawing.Size(607, 251);
            this.textBoxDotSource.TabIndex = 1;
            this.textBoxDotSource.Zoom = 100;
            // 
            // tabPageRendererOutput
            // 
            this.tabPageRendererOutput.Controls.Add(this.textBoxRendererOutput);
            this.tabPageRendererOutput.Location = new System.Drawing.Point(4, 25);
            this.tabPageRendererOutput.Name = "tabPageRendererOutput";
            this.tabPageRendererOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRendererOutput.Size = new System.Drawing.Size(613, 263);
            this.tabPageRendererOutput.TabIndex = 1;
            this.tabPageRendererOutput.Text = "Renderer Output";
            this.tabPageRendererOutput.UseVisualStyleBackColor = true;
            // 
            // textBoxRendererOutput
            // 
            this.textBoxRendererOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxRendererOutput.Location = new System.Drawing.Point(3, 3);
            this.textBoxRendererOutput.MaxLength = 0;
            this.textBoxRendererOutput.Multiline = true;
            this.textBoxRendererOutput.Name = "textBoxRendererOutput";
            this.textBoxRendererOutput.ReadOnly = true;
            this.textBoxRendererOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxRendererOutput.Size = new System.Drawing.Size(607, 257);
            this.textBoxRendererOutput.TabIndex = 0;
            this.textBoxRendererOutput.WordWrap = false;
            // 
            // buttonRender
            // 
            this.buttonRender.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRender.Location = new System.Drawing.Point(519, 72);
            this.buttonRender.Name = "buttonRender";
            this.buttonRender.Size = new System.Drawing.Size(93, 27);
            this.buttonRender.TabIndex = 6;
            this.buttonRender.Text = "&Render";
            this.buttonRender.UseVisualStyleBackColor = true;
            this.buttonRender.Click += new System.EventHandler(this.buttonRender_Click);
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowse.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonBrowse.Location = new System.Drawing.Point(519, 6);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(93, 27);
            this.buttonBrowse.TabIndex = 7;
            this.buttonBrowse.Text = "Browse";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Visible = false;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // checkBoxVerbose
            // 
            this.checkBoxVerbose.AutoSize = true;
            this.checkBoxVerbose.Location = new System.Drawing.Point(305, 74);
            this.checkBoxVerbose.Name = "checkBoxVerbose";
            this.checkBoxVerbose.Size = new System.Drawing.Size(129, 23);
            this.checkBoxVerbose.TabIndex = 8;
            this.checkBoxVerbose.Text = "&Verbose Output";
            this.checkBoxVerbose.UseVisualStyleBackColor = true;
            // 
            // checkBoxSaveToFile
            // 
            this.checkBoxSaveToFile.AutoSize = true;
            this.checkBoxSaveToFile.Location = new System.Drawing.Point(305, 43);
            this.checkBoxSaveToFile.Name = "checkBoxSaveToFile";
            this.checkBoxSaveToFile.Size = new System.Drawing.Size(102, 23);
            this.checkBoxSaveToFile.TabIndex = 9;
            this.checkBoxSaveToFile.Text = "&Save to File";
            this.checkBoxSaveToFile.UseVisualStyleBackColor = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 19);
            this.label3.TabIndex = 10;
            this.label3.Text = "GraphViz Bin Folder:";
            // 
            // textBoxBinFolder
            // 
            this.textBoxBinFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBinFolder.Location = new System.Drawing.Point(158, 6);
            this.textBoxBinFolder.Name = "textBoxBinFolder";
            this.textBoxBinFolder.Size = new System.Drawing.Size(355, 27);
            this.textBoxBinFolder.TabIndex = 11;
            this.textBoxBinFolder.Text = "C:\\GraphViz\\bin";
            this.textBoxBinFolder.TextChanged += new System.EventHandler(this.textBoxBinFolder_TextChanged);
            // 
            // FormGraphVizEditor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.textBoxBinFolder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBoxSaveToFile);
            this.Controls.Add(this.checkBoxVerbose);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.buttonRender);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.comboBoxOutputFormat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxRenderMethod);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonClose);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "FormGraphVizEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GraphViz UI";
            this.tabControl1.ResumeLayout(false);
            this.tabPageDotSource.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textBoxDotSource)).EndInit();
            this.tabPageRendererOutput.ResumeLayout(false);
            this.tabPageRendererOutput.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button buttonClose;
        private Label label1;
        private ComboBox comboBoxRenderMethod;
        private ComboBox comboBoxOutputFormat;
        private Label label2;
        private TabControl tabControl1;
        private TabPage tabPageDotSource;
        private TabPage tabPageRendererOutput;
        private TextBox textBoxRendererOutput;
        private Button buttonRender;
        private Button buttonBrowse;
        private SaveFileDialog saveFileDialog;
        private CheckBox checkBoxVerbose;
        private CheckBox checkBoxSaveToFile;
        private OpenFileDialog openFileDialog;
        private Label label3;
        private TextBox textBoxBinFolder;
        private FastColoredTextBox textBoxDotSource;
    }
}

