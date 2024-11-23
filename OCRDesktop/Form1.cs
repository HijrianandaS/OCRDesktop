using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;
using System.Text.RegularExpressions;
using System.Linq;

namespace OCRDesktop
{
    public partial class Form1 : Form
    {
        private string selectedImagePath;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedImagePath = openFileDialog.FileName;

                    // Bersihkan PictureBox sebelum menampilkan gambar baru
                    pictureBox1.Image?.Dispose();
                    pictureBox1.Image = Image.FromFile(selectedImagePath);
                }
            }
        }

        private string CreateTemporaryCopy(string originalPath)
        {
            string tempPath = Path.Combine(Path.GetTempPath(), Path.GetFileName(originalPath));
            File.Copy(originalPath, tempPath, true);  // Salin gambar ke lokasi sementara
            return tempPath;
        }

        private async void btnExtractText_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedImagePath))
            {
                MessageBox.Show("Please select an image first.");
                return;
            }

            try
            {
                //string tessDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tessdata");
                string tessDataPath = Properties.Settings.Default.pathtessdata;
                //MessageBox.Show($"Using tessdata from: {tessDataPath}");
                string tempImagePath = CreateTemporaryCopy(selectedImagePath);  // Buat salinan sementara

                string ocrResult = await Task.Run(() => ExtractTextFromImage(tempImagePath, tessDataPath));
                txtOCRResult.Text = ocrResult;

                string nik = ExtractNIK(ocrResult);
                string name = ExtractName(ocrResult);
                string pob = ExtractPlaceOfBirth(ocrResult);
                string dob = ExtractDateOfBirth(ocrResult);
                string gender = ExtractGender(ocrResult);
                string bloodType = ExtractBloodType(ocrResult);
                string agama = ExtractAgama(ocrResult);
                string status = ExtractPerkawinan(ocrResult);
                string kerja = ExtractPekerjaan(ocrResult);
                string warga = ExtractKewarganegaraan(ocrResult);
                string berlaku = ExtractMasaBerlaku(ocrResult);
                string alamat = ExtractAlamat(ocrResult);

                //MessageBox.Show($"NIK: {nik}");
                //MessageBox.Show($"{nik}");

                txtOCRResult.Text = $"NIK: {nik}\r\nNama: {name}\r\nTempat Lahir: {pob}\r\nTanggal Lahir: {dob}\r\n" +
                                    $"Jenis Kelamin : {gender}\r\nAlamat: {alamat}\r\nGolongan Darah: {bloodType}\r\n" +
                                    $"Agama: {agama}\r\nStatus Perkawinan: {status}\r\nPekerjaan: {kerja}\r\n" +
                                    $"Kewarganegaraan: {warga}\r\nBerlaku Hingga: {berlaku}\r\n\r\n{ocrResult}";

                File.Delete(tempImagePath);  // Hapus salinan sementara setelah OCR selesai
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during OCR: {ex.Message}");
            }
        }

        // Metode untuk ekstraksi teks dengan Tesseract
        private string ExtractTextFromImage(string imagePath, string tessDataPath)
        {
            using (var engine = new TesseractEngine(tessDataPath, "eng+ind", EngineMode.Default))
            {
                engine.SetVariable("tessedit_char_whitelist", "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ");
                engine.SetVariable("tessedit_pageseg_mode", "6");

                using (var img = Pix.LoadFromFile(imagePath))
                using (var page = engine.Process(img))
                {
                    return page.GetText();
                }
            }
        }

        // Event untuk memilih gambar
        /*private void btnSelectImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedImagePath = openFileDialog.FileName;
                    pictureBox1.Image = Image.FromFile(selectedImagePath);
                }
            }
        }

        private void btnExtractText_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedImagePath))
            {
                MessageBox.Show("Please select an image first.");
                return;
            }

            try
            {
                // Path ke folder tessdata
                string tessDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tessdata");
                MessageBox.Show($"Using tessdata from: {tessDataPath}");

                // Cek apakah folder dan file trained data ada
                if (!Directory.Exists(tessDataPath))
                {
                    MessageBox.Show("Tessdata folder not found.");
                    return;
                }

                if (!File.Exists(Path.Combine(tessDataPath, "eng.traineddata")) ||
                    !File.Exists(Path.Combine(tessDataPath, "ind.traineddata")))
                {
                    MessageBox.Show("Trained data files not found.");
                    return;
                }

                // Inisialisasi Tesseract Engine
                using (var engine = new TesseractEngine(tessDataPath, "eng+ind", EngineMode.Default))
                {
                    engine.SetVariable("tessedit_char_whitelist", "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ");
                    engine.SetVariable("tessedit_pageseg_mode", "6");

                    using (var img = Pix.LoadFromFile(selectedImagePath))
                    using (var page = engine.Process(img))
                    {
                        txtOCRResult.Text = page.GetText();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during OCR: {ex.Message}");
            }
        }*/

        private string ExtractNIK(string ocrText)
        {
            // Regex pattern untuk NIK (16 digit angka)
            string pattern = @"\b\d{16}\b";
            Match match = Regex.Match(ocrText, pattern);

            return match.Success ? match.Value : "NIK tidak ditemukan";
        }

        private string ExtractName(string ocrText)
        {
            //string pattern = @"Nama\s+([A-Za-z\s]+)";
            //string pattern = @"Nama\s+([A-Za-z]+\s[A-Za-z]+(?:\s[A-Za-z]+)?)";
            string pattern = @"Nama\s+([A-Za-z\s]+?)(?=\sTempat)";

            Match match = Regex.Match(ocrText, pattern, RegexOptions.IgnoreCase);

            return match.Success ? match.Groups[1].Value.Trim() : "Nama tidak ditemukan";
        }

        private string ExtractPlaceOfBirth(string ocrText)
        {
            // Pattern untuk mencari "Tempat/Tgl Lahir" diikuti oleh tempat lahir
            //string pattern = @"Tempat(?:|Tgl)? Lahir\s*([A-Za-z\s]+)";
            string pattern = @"Lahir\s*([A-Za-z\s]+)";

            Match match = Regex.Match(ocrText, pattern);

            return match.Success ? match.Groups[1].Value.Trim() : "Tempat lahir tidak ditemukan";
        }

        private string ExtractDateOfBirth(string ocrText)
        {
            //string pattern = @"\b\d{2}-\d{2}-\d{4}\b";
            //string pattern = @"\b\d{2}\d{2}\d{4}\b";
            string pattern = @"\b\d{2}-\d{2}-\d{4}\b|\b\d{2}\d{2}\d{4}\b";
            Match match = Regex.Match(ocrText, pattern);

            return match.Success ? match.Value : "Tanggal lahir tidak ditemukan";
        }

        private string ExtractGender(string ocrText)
        {
            string pattern = @"Jenis Kelamin\s*(Laki-Laki|Lakilaki|Perempuan)";

            Match match = Regex.Match(ocrText, pattern, RegexOptions.IgnoreCase);

            return match.Success ? match.Groups[1].Value.Trim() : "Jenis kelamin tidak ditemukan";
        }

        private string ExtractBloodType(string ocrText)
        {
            string pattern = @"Gol Darah\s*(A|B|AB|O)";

            Match match = Regex.Match(ocrText, pattern, RegexOptions.IgnoreCase);

            return match.Success ? match.Groups[1].Value.Trim() : "Golongan darah tidak ditemukan";
        }

        private string ExtractAgama(string ocrText)
        {
            string pattern = @"Agama\s*(Islam|Kristen Protestan|Kristen Katolik|Hindu|Budha|Konghucu)";

            Match match = Regex.Match(ocrText, pattern, RegexOptions.IgnoreCase);

            return match.Success ? match.Groups[1].Value.Trim() : "Agama tidak ditemukan";
        }

        private string ExtractPerkawinan(string ocrText)
        {
            string pattern = @"Status Perkawinan\s*(Belum kawin|Kawin|Kawin belum tercatat|Cerai hidup|Cerai mati)";

            Match match = Regex.Match(ocrText, pattern, RegexOptions.IgnoreCase);

            return match.Success ? match.Groups[1].Value.Trim() : "Status perkawinan tidak ditemukan";
        }

        private string ExtractPekerjaan(string ocrText)
        {
            /*string pattern = @"Pekerjaan\s*(Pegawai negeri sipil|Tentara Nasional Indonesia|Kepolisian RI|Karyawan swasta|Karyawan BUMN" +
                             @"Karyawan BUMD|Buruh harian lepas|Buruh tani/perkebunan|Buruh nelayan/perikanan|Tukang kayu)";*/
            string pattern = @"Pekerjaan\s+([A-Za-z\s]+?)(?=\sKewarganegaraan)";

            Match match = Regex.Match(ocrText, pattern, RegexOptions.IgnoreCase);

            return match.Success ? match.Groups[1].Value.Trim() : "Pekerjaan tidak ditemukan";
        }

        private string ExtractKewarganegaraan(string ocrText)
        {
            string pattern = @"Kewarganegaraan\s*(WNI|WNA)";

            Match match = Regex.Match(ocrText, pattern, RegexOptions.IgnoreCase);

            return match.Success ? match.Groups[1].Value.Trim() : "Kewarganegaraan tidak ditemukan";
        }

        private string ExtractMasaBerlaku(string ocrText)
        {
            if (ocrText.IndexOf("SEUMUR HIDUP", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return "SEUMUR HIDUP";
            }

            string pattern = @"Berlaku Hingga\s*(\d{2}-\d{2}-\d{4}|\d{2}\d{2}\d{4})";

            Match match = Regex.Match(ocrText, pattern, RegexOptions.IgnoreCase);

            return match.Success ? match.Groups[1].Value.Trim() : "Tanggal berlaku hingga tidak ditemukan";
        }

        private string ExtractAlamat(string ocrText)
        {
            // Pattern untuk mencari "Alamat" diikuti oleh teks sampai sebelum "RT/RW", "Kel/Desa", atau "Kecamatan"
            //string pattern = @"Alamat\s*:\s*([A-Za-z0-9\s\.,\/]+?)(?=\s+(RT\/RW|Kel\/Desa|Kecamatan))";
            //string pattern = @"Pekerjaan\s+([A-Za-z\s]+?)(?=\sKewarganegaraan)";
            //string jl = @"Alamat\s+([A-Za-z\s]+)";
            string jl = @"Alamat\s+([A-Za-z0-9\s]+)(?=\sAgama)";
            //string pattern = @"Alamat\s*:\s*([A-Za-z0-9\s\.,\/]+)";
            //string rtrw = @"Rt/Rw\s+([A-Za-z0-9\s]+)";

            Match match = Regex.Match(ocrText, jl, RegexOptions.IgnoreCase);

            return match.Success ? match.Groups[1].Value.Trim() : "Alamat tidak ditemukan";
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void txtOCRResult_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
