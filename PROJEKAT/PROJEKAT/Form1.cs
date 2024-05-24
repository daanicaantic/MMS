using static System.Windows.Forms.DataFormats;

namespace PROJEKAT
{
    public partial class Form1 : Form
    {
        private Bitmap original;
        private Filters filters;

        public Form1()
        {
            InitializeComponent();
            filters = new Filters();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AutoSize = true;
            btnFilter.Enabled = false;

            pbImage.SizeMode = PictureBoxSizeMode.AutoSize;
            groupBox1.AutoSize = true;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.bmp; *.gif)|*.jpg; *.jpeg; *.bmp; *.gif";

            if (open.ShowDialog() == DialogResult.OK)
            {
                btnFilter.Enabled = true;
                pbImage.Image = new Bitmap(open.FileName);
                original = new Bitmap(pbImage.Image);
            }
        }

        private void saveDownsampledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Format format = new Format(new Bitmap(pbImage.Image));
                format.SaveImage(saveFileDialog1.FileName);
            }
        }

        private void openDownsampledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "TXT files|*.txt";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                Format open = new Format();
                Bitmap bmp = open.ReadImage(theDialog.FileName);
                pbImage.Image = bmp;
                original = bmp;
            }
        }

        private void saveWithCompressionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Format format = new Format(new Bitmap(pbImage.Image));
                format.SaveImageWithCompression(saveFileDialog1.FileName);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (rbGamma.Checked)
            {
                Bitmap gamma = new Bitmap(pbImage.Image);
                filters.ApplyGamma(ref gamma, 2.6, 0.7, 1.8);
                pbImage.Image = gamma;
            }
            else if (rbEdgeDetection.Checked)
            {
                Bitmap edgeDetection = new Bitmap(pbImage.Image);
                filters.ApplyEdgeDetection(ref edgeDetection);
                pbImage.Image = edgeDetection;
            }
            else if (rbOriginal.Checked)
            {
                pbImage.Image = original;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

    }
}
