using System;
using System.Windows.Forms;

namespace TextComposerLib.WinForms.UserInterface.UI
{
    public partial class FormImportTemplateFromText : Form
    {
        internal string ImportedText = String.Empty;


        public FormImportTemplateFromText()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            ImportedText = textBoxTemplateCode.Text;
            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            ImportedText = String.Empty;
            DialogResult = DialogResult.Cancel;
        }
    }
}
