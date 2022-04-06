using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResimliKayıt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti;
        OleDbCommand komut;
        OleDbDataAdapter da;

        void listele()
        {
            baglanti = new OleDbConnection("Provider = Microsoft.ACE.OleDb.12.0; Data Source = data.accdb");
            baglanti.Open();
            da = new OleDbDataAdapter("Select *From data", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            pictureBox1.ImageLocation = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
           string sorgu = "Insert into data (ad,soyad,resim) values (@Ad,@Soyad,@Resim)";
            komut = new OleDbCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@Ad", textBox1.Text);
            komut.Parameters.AddWithValue("@Soyad", textBox2.Text);
            komut.Parameters.AddWithValue("@Resim", textBox3.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            listele();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = "Update data Set ad=@Ad,soyad=@Soyad,resim=@Resim Where id=@Id";
            komut = new OleDbCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@Ad", textBox1.Text);
            komut.Parameters.AddWithValue("@Soyad", textBox2.Text);
            komut.Parameters.AddWithValue("@Resim", textBox3.Text);
            komut.Parameters.AddWithValue("@Id", (dataGridView1.CurrentRow.Cells[0].Value));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
           listele();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();

        }
        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu = "Delete From data Where id=@Id";
            komut = new OleDbCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@Id", (dataGridView1.CurrentRow.Cells[0].Value));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            listele();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();

        }
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyası|*.jpg;*.png| Tüm Dosyalar|*-*";
            dosya.ShowDialog();
            string dosyayolu = dosya.FileName;
            textBox3.Text = dosyayolu;
            pictureBox1.ImageLocation = dosyayolu;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult sonuc;
            sonuc = MessageBox.Show("Çıkmak İstediğinizden Emin misiniz ?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (sonuc == DialogResult.No)
            {
                MessageBox.Show("İptal Edildi","İşlem Başarısız");
            }
            if (sonuc == DialogResult.Yes)
            {
                this.Close();
                Application.Exit();
            }
        }
    }
}
