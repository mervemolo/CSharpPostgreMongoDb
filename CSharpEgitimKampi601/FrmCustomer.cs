using MongoDB.Driver.Core.Events;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpEgitimKampi601
{
    public partial class FrmCustomer : Form
    {
        public FrmCustomer()
        {
            InitializeComponent();
        }
        string connectionString = "Server=localhost;port=5432;Database=CustomerDb;user Id=postgres;Password=1234";
        void GetAllCustomers()
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "Select * From Customers";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            GetAllCustomers();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string customerName = txtCustomerName.Text;
            string customerSurname = txtCustomerSurname.Text;
            string customerCity = txtCustomerCity.Text;
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "insert into Customers(customerName,customerSurname,customerCity) values (@a,@b,@c)";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@a", customerName);
            command.Parameters.AddWithValue("@b", customerSurname);
            command.Parameters.AddWithValue("@c", customerCity);
            command.ExecuteNonQuery();
            MessageBox.Show("Eklendi");
            connection.Close();
            GetAllCustomers();



        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id=int.Parse(txtCustomerId.Text);
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "delete from customers where customerId=@id";
            var command=new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            MessageBox.Show("Silindi");
            connection.Close();
            GetAllCustomers();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtCustomerId.Text);
            string customerName = txtCustomerName.Text;
            string customerSurname=txtCustomerSurname.Text;
            string customerCity=txtCustomerCity.Text;
            var connection = new NpgsqlConnection(
                connectionString);
            connection.Open();
            var query = "update customers set customerName=@name,customerSurname=@surname,customerCity=@city where customerId=@id";
            var command= new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@city",customerCity);
            command.Parameters.AddWithValue("@name",customerName);
            command.Parameters.AddWithValue("@surname", customerSurname);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            MessageBox.Show("Güncellendi");
            connection.Close();
            GetAllCustomers();

        }

        private void btnGetById_Click(object sender, EventArgs e)
        {
            int id=int.Parse(txtCustomerId.Text);
            var connection=new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "select * from customers where customerId=@id";
            var command=new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }
    }
}
