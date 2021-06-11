using System.ComponentModel;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace TextComposerLib.WinForms.UserInterface.UI
{
    partial class FormImportTemplateFromText
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImportTemplateFromText));
            this.textBoxTemplateCode = new FastColoredTextBoxNS.FastColoredTextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxTemplateCode)).BeginInit();
            this.SuspendLayout();
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
            this.textBoxTemplateCode.Location = new System.Drawing.Point(12, 12);
            this.textBoxTemplateCode.Name = "textBoxTemplateCode";
            this.textBoxTemplateCode.Paddings = new System.Windows.Forms.Padding(0);
            this.textBoxTemplateCode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.textBoxTemplateCode.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("textBoxTemplateCode.ServiceColors")));
            this.textBoxTemplateCode.Size = new System.Drawing.Size(600, 380);
            this.textBoxTemplateCode.TabIndex = 6;
            this.textBoxTemplateCode.Zoom = 100;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(500, 398);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(112, 32);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(382, 398);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(112, 32);
            this.buttonOK.TabIndex = 9;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // FormImportTemplateFromText
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.textBoxTemplateCode);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "FormImportTemplateFromText";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormImportTemplateFromText";
            ((System.ComponentModel.ISupportInitialize)(this.textBoxTemplateCode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FastColoredTextBox textBoxTemplateCode;
        private Button buttonCancel;
        private Button buttonOK;
    }
}