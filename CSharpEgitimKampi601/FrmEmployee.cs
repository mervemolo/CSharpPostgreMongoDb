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
    public partial class FrmEmployee : Form
    {
        public FrmEmployee()
        {
            InitializeComponent();

        }
        string connectionstring = "server=localhost ;port=5432;database=CustomerDb;user Id=postgres;password=1234";
        void EmployeeList()
        {
            var connection = new NpgsqlConnection(connectionstring);
            connection.Open();
            string query = "select * from Employees";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();

        }
        void DepartmentList()
        {
            var connection = new NpgsqlConnection(connectionstring);
            connection.Open();
            string query = "select * from Departments";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            cmbEmployeeDepartment.DisplayMember = "DepartmentName";
            cmbEmployeeDepartment.ValueMember = "DepartmentId";
            cmbEmployeeDepartment.DataSource = dt;
            connection.Close();
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            EmployeeList();
        }

        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            DepartmentList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string EmployeeName = txtEmployeeName.Text;
            string EmployeeSurname = txtEmployeeSurname.Text;
            int EmployeeSalary = int.Parse(txtEmployeeSalary.Text);
            int EmployeeDepartmentId = int.Parse(cmbEmployeeDepartment.SelectedValue.ToString());
            var connection = new NpgsqlConnection(connectionstring);
            connection.Open();
            string query = "insert into Employees(EmployeeName,EmployeeSurname,EmployeeSalary,DepartmentId) values (@name,@surname,@salary,@department)";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", EmployeeName);
            command.Parameters.AddWithValue("@surname", EmployeeSurname);
            command.Parameters.AddWithValue("@salary", EmployeeSalary);
            command.Parameters.AddWithValue("@department", EmployeeDepartmentId);
            command.ExecuteNonQuery();
            MessageBox.Show("Eklendi");
            connection.Close();
            EmployeeList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtEmployeeId.Text);
            var connection = new NpgsqlConnection(connectionstring);
            connection.Open();
            string query = "Delete from Employees where EmployeeId=@id";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            MessageBox.Show("Silindi");
            connection.Close();
            EmployeeList();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtEmployeeId.Text);
            string name = txtEmployeeName.Text;
            decimal salary = decimal.Parse(txtEmployeeSalary.Text);
            string surname = txtEmployeeSurname.Text;
            int departmentId = cmbEmployeeDepartment.SelectedIndex;

            var connection = new NpgsqlConnection(
                connectionstring);
            connection.Open();
            string query = "Update Employees set EmployeeName=@name,EmployeeSurname=@surname,EmployeeSalary=@salary,DepartmentId=@departmentId where EmployeeId=@id";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@surname", surname);
            command.Parameters.AddWithValue("@salary", salary);
            command.Parameters.AddWithValue("@departmentId", departmentId);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            MessageBox.Show("Güncellendi");
            connection.Close();
            EmployeeList();

        }

        private void btnGetById_Click(object sender, EventArgs e)
        {
            int id= int.Parse(txtEmployeeId.Text);
            var connection= new NpgsqlConnection(connectionstring);
            connection.Open();
            string query = "Select * From Employees where EmployeeId=@id";
            var command= new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            var adapter=new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource=dataTable;
            connection.Close();


        }
    }
}
