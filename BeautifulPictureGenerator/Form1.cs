using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;


namespace BeautifulPictureGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.TopMost = true;
            this.WindowState = FormWindowState.Maximized;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void Generate_Click(object sender, EventArgs e)
        {
            label1.Text = null;
            if (string.IsNullOrEmpty(textBox1.Text)) MessageBox.Show("Bạn chưa nhập từ khóa, vui lòng thử lại.");
            else
            {
                HttpClient client = new HttpClient();

                string url = $"https://api.unsplash.com/photos/random/?client_id=EnterYourApiHere&query={textBox1.Text}";

                try
                {
                    string response = await client.GetStringAsync(url);

                    var jsonObj = (JObject)JsonConvert.DeserializeObject(response);

                    string image = (string)jsonObj["urls"]["regular"];

                    pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                    pictureBox1.LoadAsync(image);

                    folderBrowserDialog1.Tag = (string)jsonObj["alt_description"];
                }
                catch (Exception)
                {
                    MessageBox.Show("Từ khóa bạn tìm kiếm không tồn tại, vui lòng thử lại.");
                    MessageBox.Show("Hoặc API Call đã đạt giới hạn, vui lòng thử lại sau.");
                }
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) MessageBox.Show("Bạn chưa tìm kiếm tấm ảnh nào, vui lòng thử lại.");
            else
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image.Save(folderBrowserDialog1.SelectedPath + (string)folderBrowserDialog1.Tag + ".jpg");
                    MessageBox.Show("Ảnh của bạn đã được lưu.");
                }
            }
        }
    }
}
