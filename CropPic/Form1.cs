using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Threading.Tasks;
using TORServices.Forms;

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

        private async void button3_Click(object sender, EventArgs e)
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
                        await Task.Run(async () =>
                        {
                            myProgressBar1.SetMinMax(0, openFileDialog.FileNames.Count(),0);
                            foreach (string file in openFileDialog.FileNames)
                            {
                                await   CropPicAndSaveAsync(file);
                                count++;
                                myProgressBar1.AddValue();
                                //Application.DoEvents();
                            }

                        }); 
                        

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
        private async Task CropPicAndSaveAsync(string targetFile,
            double topPercent = 0.11,
            double bottomPercent = 0.118)
        {
            if (string.IsNullOrEmpty(targetFile) || !File.Exists(targetFile)) return;

            try
            {
                await Task.Run(() =>
                {
                    pictureBox1.Invoke(new Action(() => {
                        if (pictureBox1.Image != null)
                        {
                            pictureBox1.Image.Dispose();
                            pictureBox1.Image = null;
                        }
                    }));

                    using (var image = SixLabors.ImageSharp.Image.Load(targetFile))
                    {
                        int width = image.Width;
                        int height = image.Height;

                        int topPx = (int)(height * topPercent);
                        int bottomPx = (int)(height * bottomPercent);
                        int finalHeight = height - (topPx + bottomPx);

                        if (finalHeight <= 0)
                            throw new Exception("สัดส่วนการตัดมากเกินขนาดรูปภาพ");

                        using (var cropped = image.Clone(ctx =>
                            ctx.Crop(new SixLabors.ImageSharp
                            .Rectangle(0, topPx, width, finalHeight))
                        ))
                        {
                            using (var finalImage =
                            new SixLabors
                            .ImageSharp
                            .Image<SixLabors.ImageSharp.PixelFormats
                            .Rgba32>(width, height, SixLabors.ImageSharp.Color.Black))
                            {
                                finalImage.Mutate(ctx =>
                                    ctx.DrawImage(cropped,
                                    new SixLabors.ImageSharp.Point(0, topPx), 1f)
                                );

                                if (checkBox1.Checked) // ใช้ค่าจากตัวแปรที่เก็บไว้
                                {
                                    finalImage.Save(targetFile);
                                    filePic = targetFile;
                                }
                                else //บันทึกแยกไฟล์ใหม่
                                {
                                    string dir = Path.GetDirectoryName(targetFile) ?? "";
                                    string fileName =
                                    Path.GetFileNameWithoutExtension(targetFile);
                                    string ext = Path.GetExtension(targetFile);
                                    string newFile =
                                    Path.Combine(dir,
                                    $"{fileName}_{DateTime.Now:yyyyMMdd_HHmmss}{ext}");

                                    finalImage.Save(newFile);
                                    filePic = newFile;
                                }
                            }
                        }
                    }
                });
               
                await ShowImageAsync(filePic);

                this.Invoke(new Action(() =>
                this.Text = $"จัดการเสร็จแล้ว: {Path.GetFileName(filePic)}"));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"การตัดรูปขัดข้อง: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task ShowImageAsync(string path)
        {
            await Task.Run(async() => {
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    var ms = new MemoryStream();
                    stream.CopyTo(ms);
                    ms.Position = 0;
                    pictureBox1.Invoke(new Action(() =>
                    {
                        if (pictureBox1.Image != null)
                            pictureBox1.Image.Dispose();
                        pictureBox1.Image = System.Drawing.Image.FromStream(ms);
                    }));
                }
                label1.Invoke(new Action(() =>
                {
                    if (label1 != null)
                        label1.Text = "Path: " + path;
                }));
                await Task.Delay(2000);//2 วิ
            });

        }
        


        private void CropPicAndSave(string targetFile,
           double topPercent = 0.11,
           double bottomPercent = 0.118)
        {
            if (string.IsNullOrEmpty(targetFile) || !File.Exists(targetFile)) return;
            try
            {
                //ตัดการทำงานของ pictureBox1 เพื่อไม่ให้นำรูปไปใช้
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                }

                using (var image = SixLabors.ImageSharp.Image.Load(targetFile))
                {
                    int width = image.Width;
                    int height = image.Height;

                    int topPx = (int)(height * topPercent);
                    int bottomPx = (int)(height * bottomPercent);
                    int finalHeight = height - (topPx + bottomPx);

                    if (finalHeight <= 0)
                        throw new Exception("สัดส่วนการตัดมากเกินขนาดรูปภาพ");
                    // 1. Crop เฉพาะส่วนกลางก่อน
                    using (var cropped = image.Clone(ctx =>
                        ctx.Crop(new SixLabors.ImageSharp.Rectangle(0, topPx, width, finalHeight))
                    ))
                    {
                        // 2. สร้างภาพใหม่ขนาดเท่าเดิม (พื้นหลังสีดำ)
                        using (var finalImage =
                            new SixLabors.ImageSharp
                            .Image<SixLabors
                            .ImageSharp
                            .PixelFormats.Rgba32>(width, height, SixLabors.ImageSharp.Color.Black))
                        {
                            // 3. วางภาพที่ crop ลงตรงกลาง
                            finalImage.Mutate(ctx =>
                                ctx.DrawImage(cropped, new SixLabors.ImageSharp.Point(0, topPx), 1f)
                            );
                            if (checkBox1.Checked)//บันทึกทับแทนไฟล์เดิม
                            {
                                finalImage.Save(targetFile);
                                filePic = targetFile;
                            }
                            else//บันทึกแยกไฟล์ใหม่
                            {
                                string dir = Path.GetDirectoryName(targetFile) ?? "";
                                string fileName = Path.GetFileNameWithoutExtension(targetFile);
                                string ext = Path.GetExtension(targetFile);

                                string newFile = Path.Combine(
                                    dir,
                                    $"{fileName}_{DateTime.Now:yyyyMMdd_HHmmss}{ext}"
                                );

                                finalImage.Save(newFile);
                                filePic = newFile;

                            }
                        }
                    }
                }


                ShowImage(filePic);
                this.Invoke(new Action(() => this.Text = $"จัดการเสร็จแล้ว: {Path.GetFileName(filePic)}"));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"การตัดรูปขัดข้อง: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ShowImage(string path)
        {
           
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    var ms = new MemoryStream();
                    stream.CopyTo(ms);
                    ms.Position = 0;
                    pictureBox1.Invoke(new Action(() =>
                    {
                        if (pictureBox1.Image != null)
                            pictureBox1.Image.Dispose();
                        pictureBox1.Image = System.Drawing.Image.FromStream(ms);
                    }));
                }
                label1.Invoke(new Action(() =>
                {
                    if (label1 != null)
                        label1.Text = "Path: " + path;
                }));

        }

        private async void button4_Click(object sender, EventArgs e)
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

                        myProgressBar1.SetMinMax(0, openFileDialog.FileNames.Count(), 0);

                        foreach (string file in openFileDialog.FileNames)
                        {
                            // เรียกใช้ตัว Async และรอ (await) ให้เสร็จทีละไฟล์
                            await CropPicAndSaveAsync(file, 0.070, 0.120);

                            count++;
                            myProgressBar1.AddValue();
                        }

                        MessageBox.Show($"จัดการสำเร็จทั้งหมด {count} ไฟล์เรียบร้อยแล้ว!", "Batch Success");
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
