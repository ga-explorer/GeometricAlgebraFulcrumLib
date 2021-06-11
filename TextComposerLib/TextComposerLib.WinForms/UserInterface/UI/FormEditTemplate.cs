using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using TextComposerLib.Text.Parametric;

namespace TextComposerLib.WinForms.UserInterface.UI
{
    public partial class FormEditTemplate : Form
    {
        private readonly string _templateName;

        private readonly ParametricTextComposer _template;

        private bool _updateRequired;


        public FormEditTemplate(string templateName, ParametricTextComposer template)
        {
            InitializeComponent();

            _templateName = templateName;

            _template = template;

            textBoxLeftDelimiter.Text = _template.LeftDelimiter;
            textBoxRightDelimiter.Text = _template.RightDelimiter;
            textBoxTemplateCode.Text = _template.TemplateText;

            foreach (var parameter in _template.Parameters)
                listBoxParameters.Items.Add(parameter);

            _updateRequired = false;
        }

        private bool UpdateTemplate()
        {
            if (_updateRequired == false)
                return true;

            var leftDel = textBoxLeftDelimiter.Text.Trim();

            if (leftDel.Any(System.Char.IsWhiteSpace))
            {
                MessageBox.Show(@"Left delimiter may contain no white spaces", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            var rightDel = textBoxRightDelimiter.Text.Trim();

            if (rightDel.Any(System.Char.IsWhiteSpace))
            {
                MessageBox.Show(@"Right delimiter may contain no white spaces", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            _template.SetTemplate(leftDel, rightDel, textBoxTemplateCode.Text);

            foreach (var parameter in _template.Parameters)
                listBoxParameters.Items.Add(parameter);

            var parent = (FormTemplateComposerEditor) Owner;

            parent.TemplateEditorUpdated(_templateName);

            _updateRequired = false;

            return true;
        }


        private void buttonDone_Click(object sender, EventArgs e)
        {
            if (UpdateTemplate() == false)
                return;

            Close();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            UpdateTemplate();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.DereferenceLinks = true;
            openFileDialog1.Filter = @"All Files|*.*";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.Multiselect = false;
            openFileDialog1.Title = @"Load Template Text From File";

            if (openFileDialog1.ShowDialog(this) == DialogResult.Cancel)
                return;

            try
            {
                textBoxTemplateCode.Text = File.ReadAllText(openFileDialog1.FileName);
            }
            catch (Exception er)
            {
                MessageBox.Show(
                    er.Message,
                    @"Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }

            _updateRequired = true;
        }

        private void textBoxLeftDelimiter_TextChanged(object sender, EventArgs e)
        {
            _updateRequired = true;
        }

        private void textBoxRightDelimiter_TextChanged(object sender, EventArgs e)
        {
            _updateRequired = true;
        }

        private void textBoxTemplateCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            _updateRequired = true;
        }

        private void FormEditTemplate_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (UpdateTemplate() == false)
            {
                e.Cancel = true;
                return;
            }

            var parent = (FormTemplateComposerEditor)Owner;

            parent.TemplateEditorClosed(_templateName);
        }
    }
}
