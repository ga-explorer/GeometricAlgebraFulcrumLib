
namespace TextComposerLib.WinForms
{
    partial class FormGenerateCSharp
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxClassName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxLeftDelimiter = new System.Windows.Forms.TextBox();
            this.textBoxRightDelimiter = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textBoxTemplate = new System.Windows.Forms.TextBox();
            this.textBoxCode = new System.Windows.Forms.TextBox();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Generated Class Name:";
            // 
            // textBoxClassName
            // 
            this.textBoxClassName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxClassName.Location = new System.Drawing.Point(211, 10);
            this.textBoxClassName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxClassName.Name = "textBoxClassName";
            this.textBoxClassName.Size = new System.Drawing.Size(561, 27);
            this.textBoxClassName.TabIndex = 1;
            this.textBoxClassName.Text = "ParametricTextTemplateClass";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Parameter Delimiters:";
            // 
            // textBoxLeftDelimiter
            // 
            this.textBoxLeftDelimiter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLeftDelimiter.Location = new System.Drawing.Point(211, 56);
            this.textBoxLeftDelimiter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxLeftDelimiter.Name = "textBoxLeftDelimiter";
            this.textBoxLeftDelimiter.Size = new System.Drawing.Size(49, 27);
            this.textBoxLeftDelimiter.TabIndex = 3;
            this.textBoxLeftDelimiter.Text = "#";
            // 
            // textBoxRightDelimiter
            // 
            this.textBoxRightDelimiter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRightDelimiter.Location = new System.Drawing.Point(266, 56);
            this.textBoxRightDelimiter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxRightDelimiter.Name = "textBoxRightDelimiter";
            this.textBoxRightDelimiter.Size = new System.Drawing.Size(49, 27);
            this.textBoxRightDelimiter.TabIndex = 4;
            this.textBoxRightDelimiter.Text = "#";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainer1.Location = new System.Drawing.Point(8, 92);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textBoxTemplate);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBoxCode);
            this.splitContainer1.Size = new System.Drawing.Size(764, 419);
            this.splitContainer1.SplitterDistance = 432;
            this.splitContainer1.TabIndex = 5;
            // 
            // textBoxTemplate
            // 
            this.textBoxTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxTemplate.Location = new System.Drawing.Point(0, 0);
            this.textBoxTemplate.Multiline = true;
            this.textBoxTemplate.Name = "textBoxTemplate";
            this.textBoxTemplate.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxTemplate.Size = new System.Drawing.Size(432, 419);
            this.textBoxTemplate.TabIndex = 0;
            this.textBoxTemplate.Text = "private readonly #type_name# _#js_var_name#;\r\npublic #type_name# #cs_var_name#\r\n{" +
    "\r\n    get => _#js_var_name#;\r\n    set => Composer.CodeLine($\"\"{_#js_var_name#.Co" +
    "deText} = {value.CodeText};\"\");\r\n}";
            this.textBoxTemplate.WordWrap = false;
            this.textBoxTemplate.TextChanged += new System.EventHandler(this.textBoxTemplate_TextChanged);
            // 
            // textBoxCode
            // 
            this.textBoxCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCode.Location = new System.Drawing.Point(0, 0);
            this.textBoxCode.Multiline = true;
            this.textBoxCode.Name = "textBoxCode";
            this.textBoxCode.ReadOnly = true;
            this.textBoxCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxCode.Size = new System.Drawing.Size(328, 419);
            this.textBoxCode.TabIndex = 1;
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(39, 520);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(94, 29);
            this.buttonGenerate.TabIndex = 6;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(652, 520);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(94, 29);
            this.buttonClose.TabIndex = 7;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // FormGenerateCSharp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.textBoxRightDelimiter);
            this.Controls.Add(this.textBoxLeftDelimiter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxClassName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FormGenerateCSharp";
            this.Text = "FormGenerateCSharp";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxClassName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxLeftDelimiter;
        private System.Windows.Forms.TextBox textBoxRightDelimiter;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox textBoxTemplate;
        private System.Windows.Forms.TextBox textBoxCode;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.Button buttonClose;
    }
}