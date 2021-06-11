using System;
using System.Windows.Forms;

namespace TextComposerLib.WinForms
{
    public partial class FormNotificationMessage : Form
    {
        private readonly string _message;


        private void DisplayMessage()
        {
            if (_message != null)
            {
                textBoxMessage.Text = _message;
            }
        }

        public FormNotificationMessage(string message)
        {
            InitializeComponent();

            _message = message;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void formNotificationMessage_Shown(object sender, EventArgs e)
        {
            DisplayMessage();
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBoxMessage.Text);
        }
    }
}
