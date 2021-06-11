using System;
using System.Drawing;
using System.Windows.Forms;

namespace CodeComposerLib.WinForms.GraphViz.UserInterface
{
    public partial class FormImageDisplay : Form
    {
        private Point _mouseDownPoint;


        public FormImageDisplay()
        {
            InitializeComponent();
        }

        private int MaxWidthDiff => Math.Max(0, panelImage.Width - ClientSize.Width);

        private int MaxHeightDiff => Math.Max(0, panelImage.Height - ClientSize.Height);

        public FormImageDisplay SetImage(Image image)
        {
            panelImage.BackgroundImage = image;

            panelImage.Location = new Point(0, 0);

            panelImage.Width = image.Width;

            panelImage.Height = image.Height;

            return this;
        }

        private void panelImage_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            _mouseDownPoint = e.Location;
        }

        private void panelImage_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            var offsetX = e.Location.X - _mouseDownPoint.X;
            var offsetY = e.Location.Y - _mouseDownPoint.Y;

            var newX = panelImage.Location.X + offsetX;
            var newY = panelImage.Location.Y + offsetY;

            panelImage.Location = new Point(
                Math.Max(-MaxWidthDiff, Math.Min(0, newX)),
                Math.Max(-MaxHeightDiff, Math.Min(0, newY))
                );
        }

        private void FormImageDisplay_Resize(object sender, EventArgs e)
        {
            panelImage.Location = new Point(0, 0);
        }
    }
}
