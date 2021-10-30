using System;
using System.Windows.Forms;
using TextComposerLib.Text.Parametric;

namespace TextComposerLib.WinForms
{
    public partial class FormGenerateCSharp : Form
    {
        public FormGenerateCSharp()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            var text = textBoxClassName.Text.Trim();
            var className = 
                string.IsNullOrEmpty(text) ? "ParametricTextTemplateClass" : text;

            text = textBoxLeftDelimiter.Text.Trim();
            var leftDelimiter =
                string.IsNullOrEmpty(text) ? "#" : text;

            text = textBoxRightDelimiter.Text.Trim();
            var rightDelimiter =
                string.IsNullOrEmpty(text) ? "#" : text;

            var templateText = 
                textBoxTemplate.Text.Trim();

            var parametricTextTemplate =
                new ParametricTextComposer(
                    leftDelimiter,
                    rightDelimiter,
                    templateText
                );

            textBoxCode.Text = 
                parametricTextTemplate.GenerateCSharpClass(className);
        }

        private void textBoxTemplate_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxCode.Text))
                textBoxCode.Text = string.Empty;
        }
    }
}
