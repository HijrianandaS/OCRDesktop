
namespace OCRDesktop
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnSelectImage;
        private System.Windows.Forms.Button btnExtractText;
        private System.Windows.Forms.TextBox txtOCRResult;
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnSelectImage = new System.Windows.Forms.Button();
            this.btnExtractText = new System.Windows.Forms.Button();
            this.txtOCRResult = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.AccessibleName = "btnExtractText";
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(11, 99);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(413, 270);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // btnSelectImage
            // 
            this.btnSelectImage.AccessibleName = "btnExtractText";
            this.btnSelectImage.Location = new System.Drawing.Point(12, 34);
            this.btnSelectImage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSelectImage.Name = "btnSelectImage";
            this.btnSelectImage.Size = new System.Drawing.Size(88, 42);
            this.btnSelectImage.TabIndex = 1;
            this.btnSelectImage.Text = "Select Image";
            this.btnSelectImage.UseVisualStyleBackColor = true;
            this.btnSelectImage.Click += new System.EventHandler(this.btnSelectImage_Click);
            // 
            // btnExtractText
            // 
            this.btnExtractText.AccessibleName = "btnExtractText";
            this.btnExtractText.Location = new System.Drawing.Point(160, 34);
            this.btnExtractText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExtractText.Name = "btnExtractText";
            this.btnExtractText.Size = new System.Drawing.Size(92, 42);
            this.btnExtractText.TabIndex = 2;
            this.btnExtractText.Text = "Extract Text";
            this.btnExtractText.UseVisualStyleBackColor = true;
            this.btnExtractText.Click += new System.EventHandler(this.btnExtractText_Click);
            // 
            // txtOCRResult
            // 
            this.txtOCRResult.AccessibleName = "btnExtractText";
            this.txtOCRResult.Location = new System.Drawing.Point(454, 47);
            this.txtOCRResult.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtOCRResult.Multiline = true;
            this.txtOCRResult.Name = "txtOCRResult";
            this.txtOCRResult.Size = new System.Drawing.Size(341, 389);
            this.txtOCRResult.TabIndex = 3;
            this.txtOCRResult.TextChanged += new System.EventHandler(this.txtOCRResult_TextChanged);
            // 
            // textBox1
            // 
            this.textBox1.AccessibleName = "btnExtractText";
            this.textBox1.Location = new System.Drawing.Point(12, 392);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(243, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 562);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtOCRResult);
            this.Controls.Add(this.btnExtractText);
            this.Controls.Add(this.btnSelectImage);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "KTP OCR Reader";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
    }
}

