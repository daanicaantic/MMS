using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJEKAT
{
    public class Format
    {
        int w;
        int h;

        Bitmap bitmap;
        Shannon_Fano sf;

        byte[,] r;
        byte[,] g;
        byte[,] b;

        double[,] y;
        double[,] u;
        double[,] v;

        public Format()
        {

        }

        public Format(Bitmap bmp)
        {
            bitmap = bmp;
            sf = new Shannon_Fano();
            w = bmp.Width;
            h = bmp.Height;

            r = new byte[bitmap.Width, bitmap.Height];
            g = new byte[bitmap.Width, bitmap.Height];
            b = new byte[bitmap.Width, bitmap.Height];

            y = new double[bitmap.Width, bitmap.Height];
            u = new double[bitmap.Width, bitmap.Height];
            v = new double[bitmap.Width, bitmap.Height];
        }

        private void RGB()
        {
            for (int y = 0; y < w; y++)
            {
                for (int x = 0; x < h; x++)
                {
                    Color c = bitmap.GetPixel(y, x);
                    r[y, x] = c.R;
                    g[y, x] = c.G;
                    b[y, x] = c.B;
                }
            }
        }

        private Bitmap setPixel()
        {
            for (int y = 0; y < w; y++)
            {
                for (int x = 0; x < h; x++)
                {
                    Color c = new Color();
                    c = Color.FromArgb(r[y, x], g[y, x], b[y, x]);
                    bitmap.SetPixel(y, x, c);
                }
            }

            return bitmap;
        }

        private void RgbToYuv()
        {
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    y[i, j] = r[i, j] * 0.299 + g[i, j] * 0.587 + b[i, j] * 0.114;
                    u[i, j] = (-0.147) * r[i, j] - g[i, j] * 0.289 + b[i, j] * 0.436;
                    v[i, j] = r[i, j] * 0.615 - g[i, j] * 0.515 - b[i, j] * 0.100;
                }
            }
        }

        private void YuvToRgb()
        {
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    r[i, j] = (byte)(y[i, j] + 1.140 * v[i, j]);
                    g[i, j] = (byte)(y[i, j] - 0.395 * (u[i, j]) - (0.581 * v[i, j]));
                    b[i, j] = (byte)(y[i, j] + 2.032 * u[i, j]);
                }
            }
        }

        private double[,] Downsampling(double[,] matrix)
        {
            double[,] res = new double[w / 2, h / 2];
            int x = 0;
            int y = 0;

            for (int i = 0; i < w - 1; i += 2)
            {
                y = 0;
                for (int j = 0; j < h - 1; j += 2)
                {
                    double sum = 0.0;
                    sum += matrix[i, j] + matrix[i + 1, j] + matrix[i, j + 1] + matrix[i + 1, j + 1];
                    res[x, y] = sum / 4.0;
                    y++;
                }
                x++;
            }

            return res;
        }

        private double[,] loadValues(double[,] matrix)
        {
            double[,] res = new double[w, h];
            int x = 0;
            int y = 0;

            for (int i = 0; i < w / 2; i++)
            {
                y = 0;
                for (int j = 0; j < h / 2; j++)
                {
                    res[x, y] = matrix[i, j];
                    res[x + 1, y] = matrix[i, j];
                    res[x, y + 1] = matrix[i, j];
                    res[x + 1, y + 1] = matrix[i, j];

                    y += 2;
                }
                x += 2;
            }

            return res;
        }

        public void SaveImage(string file)
        {
            this.RGB();
            this.RgbToYuv();
            double[,] uD = this.Downsampling(u);
            double[,] vD = this.Downsampling(v);

            using (FileStream fs = File.Open(file, FileMode.Open, FileAccess.Write, FileShare.None))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(w + " " + h);

                    for (int i = 0; i < bitmap.Width; i++)
                    {
                        string line = "";
                        for (int j = 0; j < bitmap.Height; j++)
                        {
                            line += y[i, j];
                            line += " ";
                        }
                        sw.WriteLine(line);
                    }

                    for (int i = 0; i < bitmap.Width / 2; i++)
                    {
                        string line = "";
                        for (int j = 0; j < bitmap.Height / 2; j++)
                        {
                            line += uD[i, j];
                            line += " ";
                        }
                        sw.WriteLine(line);
                    }

                    for (int i = 0; i < bitmap.Width / 2; i++)
                    {
                        string line = "";
                        for (int j = 0; j < bitmap.Height / 2; j++)
                        {
                            line += vD[i, j];
                            line += " ";
                        }
                        sw.WriteLine(line);
                    }

                    sw.Close();
                }
                fs.Close();
            }
        }

        public Bitmap ReadImage(string file)
        {
            StreamReader sr = new StreamReader(file);
            string dim = sr.ReadLine();
            string[] hw = dim.Split(' ');
            this.w = Convert.ToInt32(hw[0]);
            this.h = Convert.ToInt32(hw[1]);

            r = new byte[this.w, this.h];
            g = new byte[this.w, this.h];
            b = new byte[this.w, this.h];

            y = new double[this.w, this.h];
            u = new double[this.w, this.h];
            v = new double[this.w, this.h];

            double[,] uD = new double[w / 2, h / 2];
            double[,] vD = new double[w / 2, h / 2];

            bitmap = new Bitmap(w, h);

            for (int i = 0; i < w; i++)
            {
                string line = sr.ReadLine();
                string[] mat = line.Split(' ');
                for (int j = 0; j < h; j++)
                {
                    y[i, j] = Convert.ToDouble(mat[j]);
                }
            }

            for (int i = 0; i < w / 2; i++)
            {
                string line = sr.ReadLine();
                string[] mat = line.Split(' ');

                for (int j = 0; j < h / 2; j++)
                {
                    uD[i, j] = Convert.ToDouble(mat[j]);
                }
            }

            for (int i = 0; i < w / 2; i++)
            {
                string line = sr.ReadLine();
                string[] mat = line.Split(' ');

                for (int j = 0; j < h / 2; j++)
                {
                    vD[i, j] = Convert.ToDouble(mat[j]);
                }
            }

            sr.Close();
            u = loadValues(uD);
            v = loadValues(vD);
            YuvToRgb();

            bitmap = setPixel();
            return bitmap;
        }

        public void SaveImageWithCompression(string file)
        {
            this.RGB();
            this.RgbToYuv();
            double[,] uD = this.Downsampling(u);
            double[,] vD = this.Downsampling(v);

            int[] array = new int[256];
            double[] yF = new double[h];
            double[] uF = new double[h / 2];
            double[] vF = new double[h / 2];

            using (FileStream fs = File.Open(file, FileMode.Open, FileAccess.Write, FileShare.None))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    for (int j = 0; j < w; j++)
                    {
                        for (int k = 0; k < 256; k++)
                        {
                            array[k] = 0;
                        }

                        for (int i = 0; i < h; i++)
                            yF[i] = y[j, i];

                        sf.identifyDifferents(array, yF, sw);
                    }

                    for (int j = 0; j < w / 2; j++)
                    {
                        for (int k = 0; k < 256; k++)
                        {
                            array[k] = 0;
                        }

                        for (int i = 0; i < h / 2; i++)
                            uF[i] = uD[j, i];

                        sf.identifyDifferents(array, uF, sw);
                    }

                    for (int j = 0; j < w / 2; j++)
                    {
                        for (int k = 0; k < 256; k++)
                        {
                            array[k] = 0;
                        }

                        for (int i = 0; i < h / 2; i++)
                            vF[i] = vD[j, i];

                        sf.identifyDifferents(array, vF, sw);
                    }

                    sw.Close();
                }
                fs.Close();
            }
        }
    }
}
