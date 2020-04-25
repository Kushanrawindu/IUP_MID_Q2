using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace ImageContrastEnhancement
{
    public partial class Form1 : Form
    {
        Bitmap newBitmap;
        Image file;
        Boolean opened = false;
        float contrast = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = openFileDialog1.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                file = Image.FromFile(openFileDialog1.FileName);
                newBitmap = new Bitmap(openFileDialog1.FileName);
                pictureBox1.Image = file;
                opened = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = saveFileDialog1.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                if (file != null)
                {
                    if (saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.Length - 3).ToLower() == "bmp")
                    {
                        file.Save(saveFileDialog1.FileName, ImageFormat.Bmp);
                    }

                    if (saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.Length - 3).ToLower() == "jpg")
                    {
                        file.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
                    }

                    if (saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.Length - 3).ToLower() == "png")
                    {
                        file.Save(saveFileDialog1.FileName, ImageFormat.Png);
                    }
                }
                else
                {
                    MessageBox.Show("You need to open an image first!");
                }
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = trackBar1.Value.ToString();

            contrast = 0.04f * trackBar1.Value;

            Bitmap bitmap = new Bitmap(newBitmap.Width, newBitmap.Height);

            Graphics graphics = Graphics.FromImage(bitmap);
            ImageAttributes iattr = new ImageAttributes();
  
            ColorMatrix color = new ColorMatrix(new float[][]{
                                    new float[]{contrast,0f,0f,0f,0f},
                                    new float[]{0f,contrast,0f,0f,0f},
                                    new float[]{0f,0f,contrast,0f,0f},
                                    new float[]{0f,0f,0f,1f,0f},
                                    new float[]{0.001f,0.001f,0.001f,0f,1f}});

            iattr.SetColorMatrix(color);
            

            graphics.DrawImage(newBitmap,new Rectangle(0, 0, newBitmap.Width, newBitmap.Height), 0, 0, newBitmap.Width, newBitmap.Height, GraphicsUnit.Pixel, iattr);
            graphics.Dispose();
            iattr.Dispose();
            pictureBox1.Image = bitmap;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int k = 0; k < newBitmap.Width; k++)
            {
                for (int i = 0; i < newBitmap.Height; i++)
                {
                    Color originalColor = newBitmap.GetPixel(k, i);

                    int gray = (int)((originalColor.R * .3) + (originalColor.G * .59) + (originalColor.B * .11));

                    Color changedColor = Color.FromArgb(gray, gray, gray);

                    newBitmap.SetPixel(k, i, changedColor);
                }
            }

            pictureBox1.Image = newBitmap;
        }

        
    }
}
