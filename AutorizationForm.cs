using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace lab1
{
    public partial class AutorizationForm : Form
    {
        public AutorizationForm()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            string ServerName = textBox3.Text;
            GetID.ServerName = ServerName;

            DataBase autorization = new DataBase();
            autorization.AutrizationUser(username, password, ServerName);

            if (GetID.UserExists == 1)
            {
                MDI mdi = new MDI();
                this.Hide();
                mdi.Show();
            }
            else
            {
                label1.Visible = true;
            }
        }
    }
}
