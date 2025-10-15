using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _20360859011_finalsinavi;

namespace _20360859011_finalsinavi
{
    public partial class Form1 : Form
    {
        private List<Sefer> seferler; // seferler koleksiyonunu tanımladım

        public Form1()
        {
            InitializeComponent();

            seferler = new List<Sefer>(); // seferler koleksiyonunu başlattım

            button1.Click += new EventHandler(button1_Click);
            listBox1.SelectedIndexChanged += new EventHandler(listBox1_SelectedIndexChanged);

            // DataGridView bileşenini oluştur ve form'a ekle
            dataGridView1 = new DataGridView
            {
                Location = new Point(10, 200),
                Size = new Size(500, 200)
            };
            Controls.Add(dataGridView1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                string selectedSefer = listBox1.SelectedItem.ToString();
                Sefer sefer = seferler.FirstOrDefault(s => $"{s.sefernumarasi} - {s.kalkis_sehri} - {s.varis_sehri} - {s.kalkis_saati}" == selectedSefer);

                if (sefer != null)
                {
                    dataGridView1.DataSource = sefer.Yolcular.Select(y => new
                    {
                        y.Ad,
                        y.Soyad,
                        y.Telefon,
                        y.Cinsiyet,
                        y.KoltukNo
                    }).ToList();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) &&
                 !string.IsNullOrWhiteSpace(textBox2.Text) &&
                 !string.IsNullOrWhiteSpace(textBox3.Text) &&
                 !string.IsNullOrWhiteSpace(textBox4.Text))
            {
                Sefer sefer = new Sefer
                {
                    sefernumarasi = textBox1.Text,
                    kalkis_sehri = textBox2.Text,
                    varis_sehri = textBox3.Text,
                    kalkis_saati = textBox4.Text
                };
                seferler.Add(sefer);
                listBox1.Items.Add($"{sefer.sefernumarasi} - {sefer.kalkis_sehri} - {sefer.varis_sehri} - {sefer.kalkis_saati}");
            }
            else
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
