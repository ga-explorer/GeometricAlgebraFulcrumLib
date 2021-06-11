using System;
using System.Linq;
using System.Windows.Forms;
using TextComposerLib.Files;

namespace TextComposerLib.WinForms.UserInterface.UI
{
    public partial class FormFilesComposer : Form
    {
        private readonly TextFilesComposer _filesComposer;


        public FormFilesComposer(TextFilesComposer filesComposer)
        {
            InitializeComponent();

            _filesComposer = filesComposer;

            listBoxFiles.Items.AddRange(
                _filesComposer
                .Keys
                .Cast<object>()
                .ToArray()
                );
        }


        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            toolStripStatusLabel.Text = @"Saving Generated Files to Disk...";
            Application.DoEvents();

            _filesComposer.SaveToFolder(folderBrowserDialog1.SelectedPath);

            toolStripStatusLabel.Text = @"Ready";
        }

        private void listBoxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            var logPath = listBoxFiles.SelectedItem as string;

            if (string.IsNullOrEmpty(logPath))
                return;

            textBoxGeneratedText.Text = _filesComposer.GetFileFinalContents(logPath);
        }

        private void buttonStatistics_Click(object sender, EventArgs e)
        {
            var msg = _filesComposer.GenerateStatistics();

            var formMessage = new FormNotificationMessage(msg);
            formMessage.ShowDialog(this);
        }
    }
}
