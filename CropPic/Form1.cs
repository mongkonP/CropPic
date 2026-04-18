using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace CropPic
{
    public partial class Form1 : Form
    {
        private string filePic = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.webp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePic = openFileDialog.FileName;
                    CropPicAndSave(filePic);
                    this.Text = $"ไฟล์ต้นฉบับ: {Path.GetFileName(filePic)}";
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.webp";
                openFileDialog.Title = "เลือกรูปภาพทั้งหมดที่ต้องการ Batch Crop";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    button3.Enabled = false;
                    int count = 0;

                    try
                    {
                        foreach (string file in openFileDialog.FileNames)
                        {
                            CropPicAndSave(file);
                            count++;

                            Application.DoEvents();
                        }

                        MessageBox.Show($"จัดการสำเร็จทั้งหมด {count} ไฟล์เรียบร้อยแล้ว!", "Batch Success",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"เกิดข้อผิดพลาดระหว่างจัดการ: {ex.Message}", "Error");
                    }
                    finally
                    {
                        button3.Enabled = true;
                    }
                }
            }
        }

        private void CropPicAndSave(string targetFile, double topPercent = 0.11, double bottomPercent = 0.118)
        {
            if (string.IsNullOrEmpty(targetFile) || !File.Exists(targetFile)) return;

            string oldFileToDelete = targetFile;

            try
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                }

                string dir = Path.GetDirectoryName(targetFile);
                string name = Path.GetFileNameWithoutExtension(targetFile);
                string ext = Path.GetExtension(targetFile);
                string newPath = Path.Combine(dir, $"{name}_CROP_{DateTime.Now:yyyyMMdd_HHmmss}{ext}");

                using (var image = SixLabors.ImageSharp.Image.Load(targetFile))
                {
                    int topPx = (int)(image.Height * topPercent);
                    int bottomPx = (int)(image.Height * bottomPercent);
                    int finalHeight = image.Height - (topPx + bottomPx);

                    if (finalHeight <= 0)
                        throw new Exception("สัดส่วนการตัดมากเกินขนาดรูปภาพ");

                    image.Mutate(ctx =>
                        ctx.Crop(new SixLabors.ImageSharp.Rectangle(0, topPx, image.Width, finalHeight))
                    );

                    image.Save(newPath);
                }

                if (File.Exists(oldFileToDelete) && oldFileToDelete != newPath)
                {
                    try
                    {
                        GC.Collect();
                        GC.WaitForPendingFinalizers();

                        File.Delete(oldFileToDelete);
                    }
                    catch { }
                }

                filePic = newPath;
                ShowImage(filePic);
                this.Text = $"จัดการเสร็จแล้ว: {Path.GetFileName(filePic)}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"การตัดรูปขัดข้อง: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowImage(string path)
        {
            if (pictureBox1.Image != null)
                pictureBox1.Image.Dispose();

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var ms = new MemoryStream();
                stream.CopyTo(ms);
                ms.Position = 0;
                pictureBox1.Image = System.Drawing.Image.FromStream(ms);
            }

            if (label1 != null)
                label1.Text = "Path: " + path;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.webp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    button4.Enabled = false;
                    int count = 0;

                    try
                    {
                        foreach (string file in openFileDialog.FileNames)
                        {
                            CropPicAndSave(filePic, 0.070, 0.125);
                            count++;

                            Application.DoEvents();
                        }

                        MessageBox.Show($"จัดการสำเร็จทั้งหมด {count} ไฟล์เรียบร้อยแล้ว!", "Batch Success",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"เกิดข้อผิดพลาดระหว่างจัดการ: {ex.Message}", "Error");
                    }
                    finally
                    {
                        button4.Enabled = true;
                    }
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.webp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePic = openFileDialog.FileName;
                    CropPicAndSave(filePic, 0.050, 0.00);
                    this.Text = $"ไฟล์ต้นฉบับ: {Path.GetFileName(filePic)}";
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUp.Text) || string.IsNullOrWhiteSpace(txtDown.Text))
            {
                MessageBox.Show("กรุณากรอกค่าให้ครบ");
                return;
            }
            double numUp = double.Parse(txtUp.Text);
            double numDwn = double.Parse(txtDown.Text);
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.webp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePic = openFileDialog.FileName;
                    CropPicAndSave(filePic, numUp, numDwn);
                    this.Text = $"ไฟล์ต้นฉบับ: {Path.GetFileName(filePic)}";
                }
            }
        }
    }
}
