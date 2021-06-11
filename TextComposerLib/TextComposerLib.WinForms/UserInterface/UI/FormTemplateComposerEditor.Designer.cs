using System.ComponentModel;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace TextComposerLib.WinForms.UserInterface.UI
{
    partial class FormTemplateComposerEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTemplateComposerEditor));
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listBoxTemplates = new System.Windows.Forms.ListBox();
            this.textBoxTemplateCode = new FastColoredTextBoxNS.FastColoredTextBox();
            this.textBoxFile = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFile_New = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFile_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFile_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemFile_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTemplate_New = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemTemplate_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemTemplate_Remove = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuItemFile_Import = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFile_Import_FromText = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFile_Import_FromFile = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxTemplateCode)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Location = new System.Drawing.Point(12, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 374);
            this.panel1.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listBoxTemplates);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBoxTemplateCode);
            this.splitContainer1.Size = new System.Drawing.Size(600, 374);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.TabIndex = 0;
            // 
            // listBoxTemplates
            // 
            this.listBoxTemplates.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxTemplates.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxTemplates.FormattingEnabled = true;
            this.listBoxTemplates.IntegralHeight = false;
            this.listBoxTemplates.ItemHeight = 18;
            this.listBoxTemplates.Location = new System.Drawing.Point(0, 0);
            this.listBoxTemplates.Name = "listBoxTemplates";
            this.listBoxTemplates.Size = new System.Drawing.Size(200, 374);
            this.listBoxTemplates.TabIndex = 0;
            this.listBoxTemplates.SelectedIndexChanged += new System.EventHandler(this.listBoxTemplates_SelectedIndexChanged);
            // 
            // textBoxTemplateCode
            // 
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
            this.textBoxTemplateCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxTemplateCode.Font = new System.Drawing.Font("Consolas", 11.25F);
            this.textBoxTemplateCode.IsReplaceMode = false;
            this.textBoxTemplateCode.Location = new System.Drawing.Point(0, 0);
            this.textBoxTemplateCode.Name = "textBoxTemplateCode";
            this.textBoxTemplateCode.Paddings = new System.Windows.Forms.Padding(0);
            this.textBoxTemplateCode.ReadOnly = true;
            this.textBoxTemplateCode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.textBoxTemplateCode.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("textBoxTemplateCode.ServiceColors")));
            this.textBoxTemplateCode.Size = new System.Drawing.Size(396, 374);
            this.textBoxTemplateCode.TabIndex = 0;
            this.textBoxTemplateCode.Zoom = 100;
            // 
            // textBoxFile
            // 
            this.textBoxFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxFile.Location = new System.Drawing.Point(12, 27);
            this.textBoxFile.Name = "textBoxFile";
            this.textBoxFile.ReadOnly = true;
            this.textBoxFile.Size = new System.Drawing.Size(600, 23);
            this.textBoxFile.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile,
            this.menuItemTemplate});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(624, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuItemFile
            // 
            this.menuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile_New,
            this.menuItemFile_Open,
            this.menuItemFile_Save,
            this.toolStripSeparator3,
            this.menuItemFile_Import,
            this.toolStripSeparator4,
            this.menuItemFile_Close});
            this.menuItemFile.Name = "menuItemFile";
            this.menuItemFile.Size = new System.Drawing.Size(37, 20);
            this.menuItemFile.Text = "&File";
            // 
            // menuItemFile_New
            // 
            this.menuItemFile_New.Name = "menuItemFile_New";
            this.menuItemFile_New.Size = new System.Drawing.Size(168, 22);
            this.menuItemFile_New.Text = "&New...";
            this.menuItemFile_New.Click += new System.EventHandler(this.menuItemFile_New_Click);
            // 
            // menuItemFile_Open
            // 
            this.menuItemFile_Open.Name = "menuItemFile_Open";
            this.menuItemFile_Open.Size = new System.Drawing.Size(168, 22);
            this.menuItemFile_Open.Text = "&Open...";
            this.menuItemFile_Open.Click += new System.EventHandler(this.menuItemFile_Open_Click);
            // 
            // menuItemFile_Save
            // 
            this.menuItemFile_Save.Name = "menuItemFile_Save";
            this.menuItemFile_Save.Size = new System.Drawing.Size(168, 22);
            this.menuItemFile_Save.Text = "&Save";
            this.menuItemFile_Save.Click += new System.EventHandler(this.menuItemFile_Save_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(165, 6);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(165, 6);
            // 
            // menuItemFile_Close
            // 
            this.menuItemFile_Close.Name = "menuItemFile_Close";
            this.menuItemFile_Close.Size = new System.Drawing.Size(168, 22);
            this.menuItemFile_Close.Text = "&Close";
            this.menuItemFile_Close.Click += new System.EventHandler(this.menuItemFile_Close_Click);
            // 
            // menuItemTemplate
            // 
            this.menuItemTemplate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemTemplate_New,
            this.toolStripSeparator1,
            this.menuItemTemplate_Edit,
            this.toolStripSeparator2,
            this.menuItemTemplate_Remove});
            this.menuItemTemplate.Name = "menuItemTemplate";
            this.menuItemTemplate.Size = new System.Drawing.Size(69, 20);
            this.menuItemTemplate.Text = "&Template";
            // 
            // menuItemTemplate_New
            // 
            this.menuItemTemplate_New.Name = "menuItemTemplate_New";
            this.menuItemTemplate_New.Size = new System.Drawing.Size(170, 22);
            this.menuItemTemplate_New.Text = "New Template...";
            this.menuItemTemplate_New.Click += new System.EventHandler(this.menuItemTemplate_New_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(167, 6);
            // 
            // menuItemTemplate_Edit
            // 
            this.menuItemTemplate_Edit.Name = "menuItemTemplate_Edit";
            this.menuItemTemplate_Edit.Size = new System.Drawing.Size(170, 22);
            this.menuItemTemplate_Edit.Text = "Edit Template...";
            this.menuItemTemplate_Edit.Click += new System.EventHandler(this.menuItemTemplate_Edit_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(167, 6);
            // 
            // menuItemTemplate_Remove
            // 
            this.menuItemTemplate_Remove.Name = "menuItemTemplate_Remove";
            this.menuItemTemplate_Remove.Size = new System.Drawing.Size(170, 22);
            this.menuItemTemplate_Remove.Text = "Remove Template";
            this.menuItemTemplate_Remove.Click += new System.EventHandler(this.menuItemTemplate_Remove_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // menuItemFile_Import
            // 
            this.menuItemFile_Import.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile_Import_FromText,
            this.menuItemFile_Import_FromFile});
            this.menuItemFile_Import.Name = "menuItemFile_Import";
            this.menuItemFile_Import.Size = new System.Drawing.Size(168, 22);
            this.menuItemFile_Import.Text = "&Import Templates";
            // 
            // menuItemFile_Import_FromText
            // 
            this.menuItemFile_Import_FromText.Name = "menuItemFile_Import_FromText";
            this.menuItemFile_Import_FromText.Size = new System.Drawing.Size(152, 22);
            this.menuItemFile_Import_FromText.Text = "From Text...";
            this.menuItemFile_Import_FromText.Click += new System.EventHandler(this.menuItemFile_Import_FromText_Click);
            // 
            // menuItemFile_Import_FromFile
            // 
            this.menuItemFile_Import_FromFile.Name = "menuItemFile_Import_FromFile";
            this.menuItemFile_Import_FromFile.Size = new System.Drawing.Size(152, 22);
            this.menuItemFile_Import_FromFile.Text = "From File...";
            this.menuItemFile_Import_FromFile.Click += new System.EventHandler(this.menuItemFile_Import_FromFile_Click);
            // 
            // FormTemplateComposerEditor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.textBoxFile);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "FormTemplateComposerEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormParametricComposerEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTemplateComposerEditor_FormClosing);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textBoxTemplateCode)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private SplitContainer splitContainer1;
        private TextBox textBoxFile;
        private ListBox listBoxTemplates;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuItemFile;
        private ToolStripMenuItem menuItemFile_New;
        private ToolStripMenuItem menuItemFile_Save;
        private ToolStripMenuItem menuItemFile_Close;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem menuItemTemplate;
        private ToolStripMenuItem menuItemTemplate_New;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem menuItemTemplate_Edit;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem menuItemTemplate_Remove;
        private ToolStripMenuItem menuItemFile_Open;
        private FastColoredTextBox textBoxTemplateCode;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem menuItemFile_Import;
        private ToolStripMenuItem menuItemFile_Import_FromText;
        private ToolStripMenuItem menuItemFile_Import_FromFile;
    }
}