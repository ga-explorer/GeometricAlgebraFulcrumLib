namespace GraphicsComposerLib.Xeogl
{
    partial class FormXeoglRendering
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
            this.panelWebBrowser = new System.Windows.Forms.Panel();
            this.buttonRender = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelWebBrowser
            // 
            this.panelWebBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelWebBrowser.Location = new System.Drawing.Point(12, 12);
            this.panelWebBrowser.Name = "panelWebBrowser";
            this.panelWebBrowser.Size = new System.Drawing.Size(776, 397);
            this.panelWebBrowser.TabIndex = 0;
            // 
            // buttonRender
            // 
            this.buttonRender.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRender.Location = new System.Drawing.Point(12, 415);
            this.buttonRender.Name = "buttonRender";
            this.buttonRender.Size = new System.Drawing.Size(75, 23);
            this.buttonRender.TabIndex = 1;
            this.buttonRender.Text = "Render";
            this.buttonRender.UseVisualStyleBackColor = true;
            this.buttonRender.Click += new System.EventHandler(this.buttonRender_Click);
            // 
            // FormXeoglRendering
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonRender);
            this.Controls.Add(this.panelWebBrowser);
            this.Name = "FormXeoglRendering";
            this.Text = "FormXeoglRendering";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelWebBrowser;
        private System.Windows.Forms.Button buttonRender;
    }
}