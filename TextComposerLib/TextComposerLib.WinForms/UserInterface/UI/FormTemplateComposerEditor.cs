using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TextComposerLib.Text.Parametric;

namespace TextComposerLib.WinForms.UserInterface.UI
{
    public partial class FormTemplateComposerEditor : Form
    {
        internal ParametricTextComposerCollection ComposerCollection = 
            new ParametricTextComposerCollection();

        private readonly Dictionary<string, FormEditTemplate> _openedTemplates =
            new Dictionary<string, FormEditTemplate>();

        private bool _dataNotSaved;


        public FormTemplateComposerEditor()
        {
            InitializeComponent();

            UpdateMenues();
        }


        /// <summary>
        /// Ask user to save work if not yer saved. 
        /// If user selects "cancel" this returns false. 
        /// If user selects "no" this does not save and returns true.
        /// If user selects "yes" this saves and returns the save result (true for success and false for failure)
        /// </summary>
        /// <returns>False to signal cancel calling operation and true to signal go ahead with calling operation</returns>
        private bool AskToSave()
        {
            if (_openedTemplates.Count > 0)
            {
                MessageBox.Show(
                    @"Please close all opened template editors first",
                    "",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                    );

                return false;
            }

            if (_dataNotSaved == false) return true;

            var result =
                MessageBox.Show(
                    @"Save Previous Work First?",
                    @"Save",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question
                    );

            if (result == DialogResult.Cancel) return false;

            return result == DialogResult.No || Save();
        }

        private bool Save()
        {
            if (_dataNotSaved == false) return true;

            try
            {
                ComposerCollection.SaveToFile(textBoxFile.Text);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            SetDataNotSaved(false);

            return true;
        }

        private void AddTemplatesFromText(string text)
        {
            //TODO: Parse templates into a different collection to ensure unique template names and report errors
            //without affecting existing data
            ComposerCollection.Parse(text);
        }

        private void AddTemplatesFromFile(string fileName)
        {
            //TODO: Parse templates into a different collection to ensure unique template names and report errors
            //without affecting existing data
            ComposerCollection.ParseFile(fileName);
        }

        private void AddTemplatesFromFiles(IEnumerable<string> fileNames)
        {
            foreach (var fileName in fileNames)
                AddTemplatesFromFile(fileName);
        }

        private void UpdateMenues()
        {
            bool collectionOpened = !String.IsNullOrEmpty(textBoxFile.Text);

            menuItemFile_Import.Enabled = collectionOpened;
            menuItemFile_Save.Enabled = collectionOpened;
            menuItemTemplate_Edit.Enabled = collectionOpened;
            menuItemTemplate_New.Enabled = collectionOpened;
            menuItemTemplate_Remove.Enabled = collectionOpened;
        }

        private void UpdateTemplatesList()
        {
            textBoxTemplateCode.Text = String.Empty;

            listBoxTemplates.Sorted = true;
            listBoxTemplates.Items.Clear();

            foreach (var pair in ComposerCollection)
                listBoxTemplates.Items.Add(pair.Key);

            if (listBoxTemplates.Items.Count > 0)
                listBoxTemplates.SelectedIndex = 0;
        }

        private void SetDataNotSaved(bool flag)
        {
            _dataNotSaved = flag;

            Text = flag ? "Templates Editor (*)" : "Templates Editor";
        }

        internal void TemplateEditorUpdated(string templateName)
        {
            SetDataNotSaved(true);
        }

        internal void TemplateEditorClosed(string templateName)
        {
            _openedTemplates.Remove(templateName);
        }


        private void menuItemFile_New_Click(object sender, EventArgs e)
        {
            if (AskToSave() == false) return;

            saveFileDialog1.AddExtension = true;
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "ptc";
            saveFileDialog1.Filter = @"Parametric Text Composer File|*.ptc|All Files|*.*";
            saveFileDialog1.FilterIndex = 0;
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = @"New Parametric Text Composer File";

            if (saveFileDialog1.ShowDialog(this) == DialogResult.Cancel)
                return;

            ComposerCollection.Clear();

            textBoxFile.Text = saveFileDialog1.FileName;

            Save();

            UpdateTemplatesList();

            UpdateMenues();
        }

        private void menuItemFile_Open_Click(object sender, EventArgs e)
        {
            if (AskToSave() == false) return;

            openFileDialog1.AddExtension = true;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.DefaultExt = "ptc";
            openFileDialog1.Filter = @"Parametric Text Composer File|*.ptc|All Files|*.*";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.Multiselect = false;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.ShowReadOnly = false;

            if (openFileDialog1.ShowDialog(this) == DialogResult.Cancel)
                return;

            ComposerCollection.Clear();

            textBoxFile.Text = openFileDialog1.FileName;

            AddTemplatesFromFile(textBoxFile.Text);

            SetDataNotSaved(false);

            UpdateTemplatesList();

            UpdateMenues();
        }

        private void menuItemFile_Save_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void menuItemFile_Import_FromFile_Click(object sender, EventArgs e)
        {
            if (AskToSave() == false) return;

            openFileDialog1.AddExtension = true;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.DefaultExt = "ptc";
            openFileDialog1.Filter = @"Parametric Text Composer File|*.ptc|All Files|*.*";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.Multiselect = true;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.ShowReadOnly = false;

            if (openFileDialog1.ShowDialog(this) == DialogResult.Cancel)
                return;

            AddTemplatesFromFiles(saveFileDialog1.FileNames);

            SetDataNotSaved(true);

            UpdateTemplatesList();
        }

        private void menuItemFile_Import_FromText_Click(object sender, EventArgs e)
        {
            if (AskToSave() == false) return;

            var importForm = new FormImportTemplateFromText();

            if (importForm.ShowDialog(this) == DialogResult.Cancel)
                return;

            AddTemplatesFromText(importForm.ImportedText);

            SetDataNotSaved(true);

            UpdateTemplatesList();
        }

        private void menuItemFile_Close_Click(object sender, EventArgs e)
        {
            if (AskToSave() == false) return;

            Close();
        }

        private void menuItemTemplate_New_Click(object sender, EventArgs e)
        {
            var newTemplateForm = new FormNewTemplate();

            if (newTemplateForm.ShowDialog(this) == DialogResult.Cancel)
                return;

            ComposerCollection.Add(
                newTemplateForm.TemplateName, 
                new ParametricTextComposer(newTemplateForm.LeftDelimiter, newTemplateForm.RightDelimiter)
                );

            SetDataNotSaved(true);

            UpdateTemplatesList();
        }

        private void menuItemTemplate_Edit_Click(object sender, EventArgs e)
        {
            var templateName = listBoxTemplates.SelectedItem as string;

            if (String.IsNullOrEmpty(templateName))
            {
                MessageBox.Show(@"Please select a template to be edited");
                return;
            }

            if (_openedTemplates.TryGetValue(templateName, out var templateEditorForm) == false)
            {
                templateEditorForm = new FormEditTemplate(templateName, ComposerCollection[templateName])
                {
                    Text = @"Template <" + templateName + @">"
                };

                _openedTemplates.Add(templateName, templateEditorForm);
            }

            templateEditorForm.Show(this);
        }

        private void menuItemTemplate_Remove_Click(object sender, EventArgs e)
        {
            var templateName = listBoxTemplates.SelectedItem as string;

            if (String.IsNullOrEmpty(templateName))
            {
                MessageBox.Show(@"Please select a template to be removed");
                return;
            }

            if (_openedTemplates.ContainsKey(templateName))
            {
                MessageBox.Show(@"Please close template editor first");
                return;
            }

            if  (
                MessageBox.Show(
                    @"Remove template " + templateName + @"?", 
                    @"Remove Template", 
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                    ) == DialogResult.No
                )
                return;

            ComposerCollection.Remove(templateName);

            SetDataNotSaved(true);

            UpdateTemplatesList();
        }


        private void listBoxTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            var templateName = listBoxTemplates.SelectedItem as string;

            if (String.IsNullOrEmpty(templateName))
            {
                textBoxTemplateCode.Text = String.Empty;
                return;
            }

            textBoxTemplateCode.Text = ComposerCollection[templateName].TemplateText;
        }

        private void FormTemplateComposerEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AskToSave() == false)
                e.Cancel = true;
        }
    }
}
