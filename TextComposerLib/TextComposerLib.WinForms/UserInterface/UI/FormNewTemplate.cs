using System;
using System.Linq;
using System.Windows.Forms;

namespace TextComposerLib.WinForms.UserInterface.UI
{
    public partial class FormNewTemplate : Form
    {
        internal string LeftDelimiter;
        internal string RightDelimiter;
        internal string TemplateName;


        public FormNewTemplate()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            var leftDel = textBoxLeftDelimiter.Text.Trim();
            var rightDel = textBoxRightDelimiter.Text.Trim();
            var templateName = textBoxTemplateName.Text.Trim();

            if (leftDel.Any(Char.IsWhiteSpace))
            {
                MessageBox.Show(@"Left delimiter may contain no whitespaces");
                return;
            }

            if (rightDel.Any(char.IsWhiteSpace))
            {
                MessageBox.Show(@"Right delimiter may contain no whitespaces");
                return;
            }

            var parent = (FormTemplateComposerEditor)Owner;

            if (parent.ComposerCollection.ContainsKey(templateName))
            {
                MessageBox.Show(@"Template name already used in composers collection");
                return;
            }

            LeftDelimiter = leftDel;
            RightDelimiter = rightDel;
            TemplateName = templateName;

            DialogResult = DialogResult.OK;
        }
    }
}
