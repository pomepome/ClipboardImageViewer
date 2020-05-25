using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ClipboardImageViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitComponents(1280,720);
        }

        private void InitComponents(int width, int height)
        {
            Size size = new Size(width, height + menuStrip1.Height);
            ClientSize = size;
        }

        private Image CopyImage(Image image)
        {
            Image newImage = new Bitmap(image.Width, image.Height);
            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, Point.Empty);
            }
            image.Dispose();
            return newImage;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Image clipboard = Clipboard.GetData(DataFormats.Bitmap) as Bitmap;
            pictureBox1.Image?.Dispose();
            if (clipboard != null)
            {
                InitComponents((int)(clipboard.Width / 1.2), (int)(clipboard.Height / 1.2));
                pictureBox1.Image = CopyImage(clipboard);
            }
            else
            {
                pictureBox1.Image = null;
                InitComponents(640, 480);
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                return;
            }
            saveFileDialog1.ShowDialog();
        }

        private void SaveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            pictureBox1.Image?.Save(saveFileDialog1.FileName, ImageFormat.Png);
        }

        private void ClearCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.Bitmap, null);
        }
    }
}
