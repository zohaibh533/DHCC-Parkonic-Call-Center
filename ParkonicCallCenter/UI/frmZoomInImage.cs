using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ParkonicCallCenter.UI
{
    public partial class frmZoomInImage : DevExpress.XtraEditors.XtraForm
    {
        Bitmap bitmap;
        public frmZoomInImage()
        {
            InitializeComponent();
        }

        public void Show(Image img)
        {
            picMain.BackgroundImage = img;
            bitmap = (Bitmap)img;
            this.ShowDialog();
        }

        private void frmLiveExitImage_Load(object sender, EventArgs e)
        {
            picMain.Cursor = new Cursor(Properties.Resources.ZoomOut.GetHicon());
            picMain.Height = this.Height - tbSlider.Height - 5;
        }

        private void picMain_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void frmLiveExitImage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space || e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void picMain_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbSlider_EditValueChanged(object sender, EventArgs e)
        {
            picMain.BackgroundImage = ChangeBrightness(bitmap, (float)(tbSlider.Value / 100.0));
        }

        private Bitmap ChangeBrightness(Image image, float brightness)
        {
            try
            {
                // Make the ColorMatrix.
                float b = brightness;
                ColorMatrix cm = new ColorMatrix(new float[][]
                    {
                    new float[] {b, 0, 0, 0, 0},
                    new float[] {0, b, 0, 0, 0},
                    new float[] {0, 0, b, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1},
                    });
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(cm);

                // Draw the image onto the new bitmap while applying the new ColorMatrix.
                Point[] points =
                {
                new Point(0, 0),
                new Point(image.Width, 0),
                new Point(0, image.Height),
            };
                Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

                // Make the result bitmap.
                Bitmap bm = new Bitmap(image.Width, image.Height);
                using (Graphics gr = Graphics.FromImage(bm))
                {
                    gr.DrawImage(image, points, rect, GraphicsUnit.Pixel, attributes);
                }

                // Return the result.
                return bm;

            }
            catch (Exception ee)
            {
                MessageBox.Show("Error : " + ee.Message);
                return (Bitmap)image;
            }
        }

    }
}
