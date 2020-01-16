using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace lab1
{
    public partial class Child : Form
    {
        public Child()
        {
            InitializeComponent();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog o1 = new OpenFileDialog();
            o1.Filter = "INI File |*.ini";
            if (o1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                IniFiles ini = new IniFiles(o1.FileName);
                string reini = ini.ReadINI("App", "Value");
                string fini = ini.ReadINI("App", "FileRoute");
                int i = int.Parse(reini);
                trackBar1.Value = i;
                richTextBox1.LoadFile(fini);
            }
        }

        private void Child_Load(object sender, EventArgs e)
        {
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].HeaderText = "Длина слова";
            dataGridView1.Columns[1].HeaderText = "Кол-во слов";
            dataGridView1.Columns[2].HeaderText = "Частота встречи, %";
            dataGridView1.Columns[2].DefaultCellStyle.Format = "n2";
        }

        string MyFName = "";
        string content;
        public void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Текстовые файлы (*.rtf; *.txt; *.dat) | *.rtf; *.txt; *.dat";
            openFileDialog1.Title = "Выбирите файл";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                MyFName = openFileDialog1.FileName;
                try
                {
                    richTextBox1.LoadFile(MyFName);
                    content = richTextBox1.Text;
                }
                catch { richTextBox1.Text = File.ReadAllText(MyFName); }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                string message = "Невозможно сохранить пустой файл.";
                string caption = "Error";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);
            }
            else
            {
                saveFileDialog1.Filter = "Текстовые файлы (*.rtf; *.txt; *.dat) | *.rtf; *.txt; *.dat";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    MyFName = saveFileDialog1.FileName;
                    richTextBox1.SaveFile(MyFName);
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {

                string message = "Загрузите файл.";
                string caption = "Error";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);
            }
            else
            {
                button3.Enabled = false;
                for (int x = 0; x < trackBar1.Value; x++)
                {
                    dataGridView1.Rows.Add();
                }

                for (int x = 0, y = 1; x < trackBar1.Value; x++)
                {
                    dataGridView1.Rows[x].Cells[0].Value = y;
                    y++;
                }
                for (int x = 0, y = 1; y <= trackBar1.Value;)
                {
                    var count = CountWordsByLength.GetCountWordsByLength(richTextBox1.Text, y);
                    dataGridView1.Rows[x].Cells[1].Value = count;
                    y++;
                    x++;
                }

                int arrSize = 0;
                double textSize = 0;
                for (int x = 0; x < trackBar1.Value;)
                {
                    if (Convert.ToInt32(dataGridView1.Rows[x].Cells[1].Value) != 0)
                    {
                        arrSize++;
                        textSize += Convert.ToDouble(dataGridView1.Rows[x].Cells[1].Value);
                        x++;
                    }
                    else
                    {
                        x++;
                    }
                }

                for (int x = 0; x < trackBar1.Value; x++)
                {
                    dataGridView1.Rows[x].Cells[2].Value = ((Convert.ToDouble(dataGridView1.Rows[x].Cells[1].Value) * 100) / textSize);
                }


                int[] arrSourse = new int[arrSize];
                string[] yValues = new string[arrSize];
                for (int x = 0, y = 0; x < trackBar1.Value && y < arrSize;)
                {
                    if (Convert.ToInt32(dataGridView1.Rows[x].Cells[1].Value) != 0)
                    {
                        arrSourse[y] = Convert.ToInt32(dataGridView1.Rows[x].Cells[1].Value);
                        yValues[y] = Convert.ToString(dataGridView1.Rows[x].Cells[0].Value);
                        x++;
                        y++;
                    }
                    else
                    {
                        x++;
                    }
                }

                for (int x = 0; x < trackBar1.Value; x++)
                {
                    dataGridView1.Rows[x].Cells[2].Value = ((Convert.ToDouble(dataGridView1.Rows[x].Cells[1].Value) * 100) / textSize);
                }

                for (int x = 0, y = 0; x < 10 && y < arrSize;)
                {
                    if (Convert.ToInt32(dataGridView1.Rows[x].Cells[1].Value) != 0)
                    {
                        arrSourse[y] = Convert.ToInt32(dataGridView1.Rows[x].Cells[1].Value);
                        yValues[y] = Convert.ToString(dataGridView1.Rows[x].Cells[0].Value);
                        x++;
                        y++;
                    }
                    else
                    {
                        x++;
                    }
                }
                chart1.Series[0].Points.DataBindXY(yValues, arrSourse);
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            richTextBox1.Clear();
            dataGridView1.Rows.Clear();
            chart1.Series[0].Points.Clear();
            button3.Enabled = true;
            button5.Enabled = true;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            List<Profile> Stat = new List<Profile>();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                DataGridViewRow _row = dataGridView1.Rows[i];

                Profile p = new Profile();
                if (_row.Cells[0].Value != null)
                    p.LongOfWord = (int)_row.Cells[0].Value;

                if (_row.Cells[1].Value != null)
                    p.Count = (int)_row.Cells[1].Value;

                if (_row.Cells[2].Value != null)
                    p.Percent = ((double)_row.Cells[2].Value);
                Stat.Add(p);
            }
            Serializer.Save<List<Profile>>(Stat, "Statistic.xml");
            button5.Enabled = false;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveINI = new SaveFileDialog();
            saveINI.Filter = "INI File |*.ini";
            if (saveINI.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                IniFiles ini = new IniFiles(saveINI.FileName);
                ini.Write("App", "Value", trackBar1.Value.ToString());
                ini.Write("App", "FileRoute", MyFName);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            content = richTextBox1.Text;
            ClassTest test1 = new ClassTest();
            trackBar1.Maximum = test1.LenghtMax(content) - 1;
            if (richTextBox1.Text == "")
            {

                string message = "Загрузите файл.";
                string caption = "Error";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);
            }
            else
            {
                button3.Enabled = false;
                for (int x = 0; x < trackBar1.Value; x++)
                {
                    dataGridView1.Rows.Add();
                }

                for (int x = 0, y = 1; x < trackBar1.Value; x++)
                {
                    dataGridView1.Rows[x].Cells[0].Value = y;
                    y++;
                }
                for (int x = 0, y = 1; y <= trackBar1.Value;)
                {
                    var count = CountWordsByLength.GetCountWordsByLength(richTextBox1.Text, y);
                    dataGridView1.Rows[x].Cells[1].Value = count;
                    y++;
                    x++;
                }

                int arrSize = 0;
                double textSize = 0;
                for (int x = 0; x < trackBar1.Value;)
                {
                    if (Convert.ToInt32(dataGridView1.Rows[x].Cells[1].Value) != 0)
                    {
                        arrSize++;
                        textSize += Convert.ToDouble(dataGridView1.Rows[x].Cells[1].Value);
                        x++;
                    }
                    else
                    {
                        x++;
                    }
                }

                for (int x = 0; x < trackBar1.Value; x++)
                {
                    dataGridView1.Rows[x].Cells[2].Value = ((Convert.ToDouble(dataGridView1.Rows[x].Cells[1].Value) * 100) / textSize);
                }


                int[] arrSourse = new int[arrSize];
                string[] yValues = new string[arrSize];
                for (int x = 0, y = 0; x < trackBar1.Value && y < arrSize;)
                {
                    if (Convert.ToInt32(dataGridView1.Rows[x].Cells[1].Value) != 0)
                    {
                        arrSourse[y] = Convert.ToInt32(dataGridView1.Rows[x].Cells[1].Value);
                        yValues[y] = Convert.ToString(dataGridView1.Rows[x].Cells[0].Value);
                        x++;
                        y++;
                    }
                    else
                    {
                        x++;
                    }
                }

                for (int x = 0; x < trackBar1.Value; x++)
                {
                    dataGridView1.Rows[x].Cells[2].Value = ((Convert.ToDouble(dataGridView1.Rows[x].Cells[1].Value) * 100) / textSize);
                }

                for (int x = 0, y = 0; x < 10 && y < arrSize;)
                {
                    if (Convert.ToInt32(dataGridView1.Rows[x].Cells[1].Value) != 0)
                    {
                        arrSourse[y] = Convert.ToInt32(dataGridView1.Rows[x].Cells[1].Value);
                        yValues[y] = Convert.ToString(dataGridView1.Rows[x].Cells[0].Value);
                        x++;
                        y++;
                    }
                    else
                    {
                        x++;
                    }
                }
                chart1.Series[0].Points.DataBindXY(yValues, arrSourse);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DataBase database = new DataBase();
            database.LoadingFromBD(GetID.ServerName, textBox1.Text);
            richTextBox1.Text = GetID.Text;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DataBase database = new DataBase();
            database.SaveInBD(GetID.ServerName, textBox2.Text, richTextBox1.Text);
        }
    }
}
