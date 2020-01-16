using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace lab1
{
    public class DataBase
    {
        SqlConnection sqlConnection;

        public void Sqlcon(string dataSourse)
        {
            string ConnectionString = @"Data Source=" + dataSourse + ";Initial Catalog=laba3;Integrated Security=True";
            sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
        }

        public void AutrizationUser(string username, string password, string dataSourse)
        {
            Sqlcon(dataSourse);
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Users] WHERE username = @username and password = @password", sqlConnection);
            command.Parameters.AddWithValue("username", username);
            command.Parameters.AddWithValue("password", password);
            try
            {
                sqlReader = command.ExecuteReader();
                while (sqlReader.Read())
                {
                    GetID.UserExists = 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        public void LoadingFromBD(string DataSource, string NameText)
        {
            Sqlcon(DataSource);

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("select [text] from [dbo].[Text] where Name = @name", sqlConnection);
            command.Parameters.AddWithValue("@name", NameText);
            string Text = "";
            try
            {
                sqlReader = command.ExecuteReader();
                while (sqlReader.Read())
                {
                    Text = (Convert.ToString(sqlReader["text"]));
                }
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
            GetID.Text = Text;
        }

        public void SaveInBD(string DataSource, string NameText, string Text)
        {
            Sqlcon(DataSource);
            SqlCommand command = new SqlCommand("insert into [dbo].Text(Name, Text) values(@Name, @text)", sqlConnection);
            command.Parameters.AddWithValue("@Name", NameText);
            command.Parameters.AddWithValue("@text", Text);
            command.ExecuteNonQuery();
        }
    }
}
