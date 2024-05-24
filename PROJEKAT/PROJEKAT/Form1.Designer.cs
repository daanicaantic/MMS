namespace PROJEKAT
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveDownsampledToolStripMenuItem = new ToolStripMenuItem();
            openDownsampledToolStripMenuItem = new ToolStripMenuItem();
            saveWithCompressionToolStripMenuItem = new ToolStripMenuItem();
            groupBox1 = new GroupBox();
            pbImage = new PictureBox();
            gbFilters = new GroupBox();
            btnFilter = new Button();
            rbOriginal = new RadioButton();
            rbGamma = new RadioButton();
            rbEdgeDetection = new RadioButton();
            menuStrip1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbImage).BeginInit();
            gbFilters.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1080, 28);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, saveDownsampledToolStripMenuItem, openDownsampledToolStripMenuItem, saveWithCompressionToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(240, 26);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // saveDownsampledToolStripMenuItem
            // 
            saveDownsampledToolStripMenuItem.Name = "saveDownsampledToolStripMenuItem";
            saveDownsampledToolStripMenuItem.Size = new Size(240, 26);
            saveDownsampledToolStripMenuItem.Text = "SaveDownsampled";
            saveDownsampledToolStripMenuItem.Click += saveDownsampledToolStripMenuItem_Click;
            // 
            // openDownsampledToolStripMenuItem
            // 
            openDownsampledToolStripMenuItem.Name = "openDownsampledToolStripMenuItem";
            openDownsampledToolStripMenuItem.Size = new Size(240, 26);
            openDownsampledToolStripMenuItem.Text = "OpenDownsampled";
            openDownsampledToolStripMenuItem.Click += openDownsampledToolStripMenuItem_Click;
            // 
            // saveWithCompressionToolStripMenuItem
            // 
            saveWithCompressionToolStripMenuItem.Name = "saveWithCompressionToolStripMenuItem";
            saveWithCompressionToolStripMenuItem.Size = new Size(240, 26);
            saveWithCompressionToolStripMenuItem.Text = "SaveWithCompression";
            saveWithCompressionToolStripMenuItem.Click += saveWithCompressionToolStripMenuItem_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(pbImage);
            groupBox1.Controls.Add(gbFilters);
            groupBox1.Location = new Point(12, 31);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1056, 489);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            // 
            // pbImage
            // 
            pbImage.Location = new Point(155, 26);
            pbImage.Name = "pbImage";
            pbImage.Size = new Size(895, 457);
            pbImage.TabIndex = 1;
            pbImage.TabStop = false;
            // 
            // gbFilters
            // 
            gbFilters.Controls.Add(btnFilter);
            gbFilters.Controls.Add(rbOriginal);
            gbFilters.Controls.Add(rbGamma);
            gbFilters.Controls.Add(rbEdgeDetection);
            gbFilters.Location = new Point(10, 15);
            gbFilters.Name = "gbFilters";
            gbFilters.Size = new Size(139, 177);
            gbFilters.TabIndex = 0;
            gbFilters.TabStop = false;
            gbFilters.Text = "Filters";
            // 
            // btnFilter
            // 
            btnFilter.Location = new Point(15, 139);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(111, 29);
            btnFilter.TabIndex = 2;
            btnFilter.Text = "Apply Filter";
            btnFilter.UseVisualStyleBackColor = true;
            btnFilter.Click += btnFilter_Click;
            // 
            // rbOriginal
            // 
            rbOriginal.AutoSize = true;
            rbOriginal.Location = new Point(6, 97);
            rbOriginal.Name = "rbOriginal";
            rbOriginal.Size = new Size(83, 24);
            rbOriginal.TabIndex = 2;
            rbOriginal.TabStop = true;
            rbOriginal.Text = "Original";
            rbOriginal.UseVisualStyleBackColor = true;
            // 
            // rbGamma
            // 
            rbGamma.AutoSize = true;
            rbGamma.Location = new Point(6, 65);
            rbGamma.Name = "rbGamma";
            rbGamma.Size = new Size(82, 24);
            rbGamma.TabIndex = 1;
            rbGamma.TabStop = true;
            rbGamma.Text = "Gamma";
            rbGamma.UseVisualStyleBackColor = true;
            rbGamma.CheckedChanged += radioButton1_CheckedChanged_1;
            // 
            // rbEdgeDetection
            // 
            rbEdgeDetection.AutoSize = true;
            rbEdgeDetection.Location = new Point(6, 35);
            rbEdgeDetection.Name = "rbEdgeDetection";
            rbEdgeDetection.Size = new Size(129, 24);
            rbEdgeDetection.TabIndex = 0;
            rbEdgeDetection.TabStop = true;
            rbEdgeDetection.Text = "EdgeDetection";
            rbEdgeDetection.UseVisualStyleBackColor = true;
            rbEdgeDetection.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1080, 532);
            Controls.Add(groupBox1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "MMS";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbImage).EndInit();
            gbFilters.ResumeLayout(false);
            gbFilters.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private GroupBox groupBox1;
        private GroupBox gbFilters;
        private PictureBox pbImage;
        private RadioButton rbEdgeDetection;
        private RadioButton rbGamma;
        private Button btnFilter;
        private RadioButton rbOriginal;
        private ToolStripMenuItem openDownsampledToolStripMenuItem;
        private ToolStripMenuItem saveDownsampledToolStripMenuItem;
        private ToolStripMenuItem saveWithCompressionToolStripMenuItem;
    }
}
