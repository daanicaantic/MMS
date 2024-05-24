using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJEKAT
{
    public class Filters
    {
        public Filters() { }

        public void ApplyEdgeDetection(ref Bitmap bmp)
        {
            int width = bmp.Width;
            int height = bmp.Height;

            // Sobel kernels
            int[,] gx = new int[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] gy = new int[,] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };

            int[,] allPixelsR = new int[width, height];
            int[,] allPixelsG = new int[width, height];
            int[,] allPixelsB = new int[width, height];

            int limit = 128 * 128;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    allPixelsR[i, j] = bmp.GetPixel(i, j).R;
                    allPixelsG[i, j] = bmp.GetPixel(i, j).G;
                    allPixelsB[i, j] = bmp.GetPixel(i, j).B;
                }
            }

            double redX = 0.0;
            double greenX = 0.0;
            double blueX = 0.0;

            double redY = 0.0;
            double greenY = 0.0;
            double blueY = 0.0;

            double redTotal = 0.0;
            double greenTotal = 0.0;
            double blueTotal = 0.0;

            for (int i = 1; i < bmp.Width - 1; i++)
            {
                for (int j = 1; j < bmp.Height - 1; j++)
                {
                    redX = greenX = blueX = 0.0;
                    redY = greenY = blueY = 0.0;
                    //redTotal = greenTotal = blueTotal = 0.0;

                    for (int wi = -1; wi < 2; wi++)
                    {
                        for (int hi = -1; hi < 2; hi++)
                        {
                            redTotal = allPixelsR[i + hi, j + wi];
                            redX += gx[wi + 1, hi + 1] * redTotal;
                            redY += gy[wi + 1, hi + 1] * redTotal;

                            greenTotal = allPixelsG[i + hi, j + wi];
                            greenX += gx[wi + 1, hi + 1] * greenTotal;
                            greenY += gy[wi + 1, hi + 1] * greenTotal;

                            blueTotal = allPixelsB[i + hi, j + wi];
                            blueX += gx[wi + 1, hi + 1] * blueTotal;
                            blueY += gy[wi + 1, hi + 1] * blueTotal;
                        }
                    }

                    if (redX * redX + redY * redY > limit || 
                        greenX * greenX + greenY * greenY > limit || 
                        blueX * blueX + blueY * blueY > limit)
                        bmp.SetPixel(i, j, Color.Black);
                    else
                        bmp.SetPixel(i, j, Color.White);
                }
            }
        }

        public void ApplyGamma(ref Bitmap bmp, double redComponent, double greenComponent, double blueComponent)
        {
            // Validate the gamma components
            if (redComponent < 0.2 || redComponent > 5) return;
            if (greenComponent < 0.2 || greenComponent > 5) return;
            if (blueComponent < 0.2 || blueComponent > 5) return;

            // Lock the bitmap's bits
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            try
            {
                unsafe
                {
                    byte* ptr = (byte*)bmpData.Scan0.ToPointer();
                    int stride = bmpData.Stride;
                    int height = bmpData.Height;
                    int width = bmpData.Width;

                    // Precompute gamma lookup tables for faster processing
                    byte[] gammaR = new byte[256];
                    byte[] gammaG = new byte[256];
                    byte[] gammaB = new byte[256];

                    for (int i = 0; i < 256; i++)
                    {
                        gammaR[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / redComponent)) + 0.5));
                        gammaG[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / greenComponent)) + 0.5));
                        gammaB[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / blueComponent)) + 0.5));
                    }

                    // Apply the gamma correction
                    for (int y = 0; y < height; y++)
                    {
                        byte* row = ptr + (y * stride);
                        for (int x = 0; x < width; x++)
                        {
                            row[x * 3] = gammaB[row[x * 3]];      // Blue
                            row[x * 3 + 1] = gammaG[row[x * 3 + 1]];  // Green
                            row[x * 3 + 2] = gammaR[row[x * 3 + 2]];  // Red
                        }
                    }
                }
            }
            finally
            {
                // Unlock the bits
                bmp.UnlockBits(bmpData);
            }
        }
    }
}
