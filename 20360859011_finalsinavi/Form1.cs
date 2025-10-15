using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace _20360859011_finalsinavi
{
    public partial class Form1 : Form
    {
        private List<Sefer> seferler;
        private const string ConnectionString = "Server=DESKTOP-0O4CJLI\\MSSQLSERVER01;Database=SeferYonetim;Trusted_Connection=True;";

        public Form1()
        {
            InitializeComponent();

            seferler = new List<Sefer>();
            button1.Click += button1_Click;
            button6.Click += button6_Click;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;

            // DataGridView bileşenini oluştur ve form'a ekle
            dataGridView1 = new DataGridView
            {
                Location = new Point(10, 200),
                Size = new Size(500, 200)
            };
            Controls.Add(dataGridView1);

            // ComboBox'a 1-42 arasında numaralar ekle
            for (int i = 1; i <= 42; i++)
            {
                comboBox1.Items.Add(i.ToString());
            }

            // ListBox'a * öğesini ekle
            listBox1.Items.Add("*");

            // Program açıldığında verileri yükle
            LoadData();

            // Silme butonunu ekle
            Button deleteButton = new Button
            {
                Text = "Tüm Yolcuları ve Seferleri Sil",
                Location = new Point(10, 420),
                Size = new Size(200, 30)
            };
            deleteButton.Click += deleteButton_Click;
            Controls.Add(deleteButton);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            DeleteAllYolcular();
            DeleteAllSeferler();
            seferler.Clear();
            UpdateListBox();
            dataGridView1.DataSource = null;
            MessageBox.Show("Tüm yolcular ve seferler silindi.");
        }

        private void DeleteAllYolcular()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM Yolcular";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void DeleteAllSeferler()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM Seferler";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string selectedSefer = listBox1.SelectedItem.ToString();

                if (selectedSefer == "*")
                {
                    var allYolcular = seferler.SelectMany(sefer => sefer.Yolcular.Select(y => new
                    {
                        SeferNumarasi = sefer.SeferNumarasi,
                        sefer.KalkisSehri,
                        sefer.VarisSehri,
                        y.YolcuAdi,
                        y.TelefonNumarasi,
                        y.Cinsiyet,
                        y.KoltukNumarasi
                    })).ToList();

                    dataGridView1.DataSource = allYolcular;
                }
                else
                {
                    var sefer = seferler.FirstOrDefault(s => $"{s.SeferNumarasi} - {s.KalkisSehri} - {s.VarisSehri} - {s.KalkisSaati}" == selectedSefer);
                    if (sefer != null)
                    {
                        var yolcular = sefer.Yolcular.Select(y => new
                        {
                            SeferNumarasi = sefer.SeferNumarasi,
                            sefer.KalkisSehri,
                            sefer.VarisSehri,
                            y.YolcuAdi,
                            y.TelefonNumarasi,
                            y.Cinsiyet,
                            y.KoltukNumarasi
                        }).ToList();

                        dataGridView1.DataSource = yolcular;
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateSeferInputs())
            {
                string seferNumarasi = textBox1.Text;
                if (IsSeferNumarasiAlinmis(seferNumarasi))
                {
                    MessageBox.Show("Bu sefer numarası zaten alınmış. Lütfen başka bir sefer numarası girin.");
                    return;
                }

                Sefer sefer = new Sefer
                {
                    SeferNumarasi = seferNumarasi,
                    KalkisSehri = textBox2.Text,
                    VarisSehri = textBox3.Text,
                    KalkisSaati = textBox4.Text
                };
                seferler.Add(sefer);
                listBox1.Items.Add($"{sefer.SeferNumarasi} - {sefer.KalkisSehri} - {sefer.VarisSehri} - {sefer.KalkisSaati}");
                SaveSefer(sefer);
            }
            else
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
            }
        }

        private bool IsSeferNumarasiAlinmis(string seferNumarasi)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Seferler WHERE SeferNumarasi = @SeferNumarasi";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SeferNumarasi", seferNumarasi);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                    return true; // Hata durumunda sefer numarasının alınmış olduğunu varsayalım
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (ValidateYolcuInputs())
            {
                string seferNumarasi = textBox9.Text;
                var sefer = seferler.FirstOrDefault(s => s.SeferNumarasi == seferNumarasi);

                if (sefer != null)
                {
                    var selectedKoltukNumarasi = comboBox1.SelectedItem.ToString();
                    if (IsKoltukDolu(sefer.SeferID, selectedKoltukNumarasi))
                    {
                        MessageBox.Show($"Bu koltuk numarası ({selectedKoltukNumarasi}) zaten dolu. Lütfen başka bir koltuk numarası seçin.");
                        return;
                    }

                    Yolcu yolcu = new Yolcu
                    {
                        YolcuAdi = textBox5.Text,
                        TelefonNumarasi = textBox6.Text,
                        Cinsiyet = checkBox1.Checked ? "Kadın" : "Erkek",
                        SeferID = sefer.SeferID,
                        KoltukNumarasi = selectedKoltukNumarasi
                    };

                    sefer.Yolcular.Add(yolcu);
                    SaveYolcu(yolcu);
                    UpdateYolcular(sefer);

                    MessageBox.Show("Yolcu başarıyla eklendi.");
                }
                else
                {
                    MessageBox.Show("Geçerli bir sefer numarası girin.");
                }
            }
            else
            {
                MessageBox.Show("Lütfen tüm alanları doldurun ve bir koltuk numarası seçin.");
            }
        }

        private bool IsKoltukDolu(int seferID, string selectedKoltukNumarasi)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Yolcular WHERE SeferID = @SeferID AND KoltukNumarasi = @KoltukNumarasi";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SeferID", seferID);
                        cmd.Parameters.AddWithValue("@KoltukNumarasi", selectedKoltukNumarasi);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                    return true; 
                }
            }
        }

        private void UpdateYolcular(Sefer sefer)
        {
            var yolcular = sefer.Yolcular.Select(y => new
            {
                SeferNumarasi = sefer.SeferNumarasi,
                sefer.KalkisSehri,
                sefer.VarisSehri,
                y.YolcuAdi,
                y.TelefonNumarasi,
                y.Cinsiyet,
                y.KoltukNumarasi
            }).ToList();

            dataGridView1.DataSource = yolcular;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                var selectedYolcuAdi = selectedRow.Cells["YolcuAdi"].Value.ToString();
                var selectedSeferNumarasi = selectedRow.Cells["SeferNumarasi"].Value.ToString();

                var sefer = seferler.FirstOrDefault(s => s.SeferNumarasi == selectedSeferNumarasi);
                if (sefer != null)
                {
                    var yolcu = sefer.Yolcular.FirstOrDefault(y => y.YolcuAdi == selectedYolcuAdi);
                    if (yolcu != null)
                    {
                        var newKoltukNumarasi = comboBox1.SelectedItem.ToString();
                        if (IsKoltukDolu(sefer.SeferID, newKoltukNumarasi))
                        {
                            MessageBox.Show($"Bu koltuk numarası ({newKoltukNumarasi}) zaten dolu. Lütfen başka bir koltuk numarası seçin.");
                            return;
                        }

                        yolcu.KoltukNumarasi = newKoltukNumarasi;
                        UpdateYolcu(yolcu);
                        UpdateYolcular(sefer);
                        MessageBox.Show("Koltuk numarası başarıyla güncellendi.");
                    }
                }
            }
        }



        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                UpdateYolcuCinsiyet("Kadın");
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                UpdateYolcuCinsiyet("Erkek");
            }
        }

        private void UpdateYolcuCinsiyet(string cinsiyet)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                var selectedYolcuAdi = selectedRow.Cells["YolcuAdi"].Value.ToString();
                var selectedSeferNumarasi = selectedRow.Cells["SeferNumarasi"].Value.ToString();

                var sefer = seferler.FirstOrDefault(s => s.SeferNumarasi == selectedSeferNumarasi);
                if (sefer != null)
                {
                    var yolcu = sefer.Yolcular.FirstOrDefault(y => y.YolcuAdi == selectedYolcuAdi);
                    if (yolcu != null)
                    {
                        yolcu.Cinsiyet = cinsiyet;
                        UpdateYolcu(yolcu);
                        UpdateYolcular(sefer);
                        MessageBox.Show("Yolcu cinsiyeti başarıyla güncellendi.");
                    }
                }
            }
        }



        private void SaveSefer(Sefer sefer)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Seferler (SeferNumarasi, KalkisSehri, VarisSehri, KalkisSaati) VALUES (@SeferNumarasi, @KalkisSehri, @VarisSehri, @KalkisSaati)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SeferNumarasi", sefer.SeferNumarasi);
                        cmd.Parameters.AddWithValue("@KalkisSehri", sefer.KalkisSehri);
                        cmd.Parameters.AddWithValue("@VarisSehri", sefer.VarisSehri);
                        cmd.Parameters.AddWithValue("@KalkisSaati", sefer.KalkisSaati);
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "SELECT @@IDENTITY";
                        sefer.SeferID = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void SaveYolcu(Yolcu yolcu)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Yolcular (SeferID, YolcuAdi, TelefonNumarasi, Cinsiyet, KoltukNumarasi) VALUES (@SeferID, @YolcuAdi, @TelefonNumarasi, @Cinsiyet, @KoltukNumarasi)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SeferID", yolcu.SeferID);
                        cmd.Parameters.AddWithValue("@YolcuAdi", yolcu.YolcuAdi);
                        cmd.Parameters.AddWithValue("@TelefonNumarasi", yolcu.TelefonNumarasi);
                        cmd.Parameters.AddWithValue("@Cinsiyet", yolcu.Cinsiyet);
                        cmd.Parameters.AddWithValue("@KoltukNumarasi", yolcu.KoltukNumarasi);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void UpdateYolcu(Yolcu yolcu)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Yolcular SET KoltukNumarasi = @KoltukNumarasi WHERE YolcuID = @YolcuID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@KoltukNumarasi", yolcu.KoltukNumarasi);
                        cmd.Parameters.AddWithValue("@YolcuID", yolcu.YolcuID);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    LoadSeferData(conn);
                    LoadYolcuData(conn);
                    UpdateListBox();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void LoadSeferData(SqlConnection conn)
        {
            string seferQuery = "SELECT * FROM Seferler";
            using (SqlCommand seferCmd = new SqlCommand(seferQuery, conn))
            {
                using (SqlDataReader seferReader = seferCmd.ExecuteReader())
                {
                    while (seferReader.Read())
                    {
                        Sefer sefer = new Sefer
                        {
                            SeferID = seferReader.GetInt32(0),
                            SeferNumarasi = seferReader.GetString(1),
                            KalkisSehri = seferReader.GetString(2),
                            VarisSehri = seferReader.GetString(3),
                            KalkisSaati = seferReader.GetString(4)
                        };
                        seferler.Add(sefer);
                    }
                }
            }
        }

        private void LoadYolcuData(SqlConnection conn)
        {
            string yolcuQuery = "SELECT * FROM Yolcular";
            using (SqlCommand yolcuCmd = new SqlCommand(yolcuQuery, conn))
            {
                using (SqlDataReader yolcuReader = yolcuCmd.ExecuteReader())
                {
                    while (yolcuReader.Read())
                    {
                        Yolcu yolcu = new Yolcu
                        {
                            YolcuID = yolcuReader.GetInt32(0),
                            SeferID = yolcuReader.GetInt32(1),
                            YolcuAdi = yolcuReader.GetString(2),
                            TelefonNumarasi = yolcuReader.GetString(3),
                            Cinsiyet = yolcuReader.GetString(4),
                            KoltukNumarasi = yolcuReader.GetString(5)
                        };
                        var sefer = seferler.FirstOrDefault(s => s.SeferID == yolcu.SeferID);
                        if (sefer != null)
                        {
                            sefer.Yolcular.Add(yolcu);
                        }
                    }
                }
            }
        }

        private void UpdateListBox()
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("*");
            foreach (var sefer in seferler)
            {
                listBox1.Items.Add($"{sefer.SeferNumarasi} - {sefer.KalkisSehri} - {sefer.VarisSehri} - {sefer.KalkisSaati}");
            }
        }

        private bool ValidateSeferInputs()
        {
            return !string.IsNullOrWhiteSpace(textBox1.Text) &&
                   !string.IsNullOrWhiteSpace(textBox2.Text) &&
                   !string.IsNullOrWhiteSpace(textBox3.Text) &&
                   !string.IsNullOrWhiteSpace(textBox4.Text);
        }

        private bool ValidateYolcuInputs()
        {
            return !string.IsNullOrWhiteSpace(textBox5.Text) &&
                   !string.IsNullOrWhiteSpace(textBox6.Text) &&
                   !string.IsNullOrWhiteSpace(textBox8.Text) &&
                   !string.IsNullOrWhiteSpace(textBox9.Text) &&
                   comboBox1.SelectedItem != null;
        }
    }
}

