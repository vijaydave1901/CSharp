using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Lab4Dataset
{
    public partial class Form1 : Form
    {
        DataSet dsCollegeDB;
        DataTable dtStudents;
        SqlDataAdapter da;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection connDB = new SqlConnection("data source = . ; database=sample ; Integrated Security = SSPI");
            dsCollegeDB = new DataSet("CollegeDB");
            dtStudents = new DataTable("Students");
            dtStudents.Columns.Add("StudentId", typeof(Int32));
            dtStudents.Columns.Add("FirstName", typeof(string));
            dtStudents.Columns.Add("LastName", typeof(string));
            dtStudents.Columns.Add("Email", typeof(string));
            dtStudents.PrimaryKey = new DataColumn[] { dtStudents.Columns["StudentId"] };
            dsCollegeDB.Tables.Add(dtStudents);
            da = new SqlDataAdapter("select * from Students", connDB);
            da.Fill(dsCollegeDB.Tables["Students"]);
            dataGridView1.DataSource = dtStudents;
             
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlConnection connDB = new SqlConnection("data source = . ; database=sample ; Integrated Security = SSPI");
            int studentId = Convert.ToInt32(txtStudentId.Text);
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string email = txtEmail.Text;
            dtStudents.Rows.Add(studentId, firstName, lastName, email);
            string xx = string.Format("Insert into students values({0},'{1}','{2}','{3}')", studentId, firstName, lastName, email);
            da.InsertCommand = new SqlCommand(xx, connDB);
            da.Update(dsCollegeDB, "Students");
            connDB.Close();
      

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txtStudentId.Text = row.Cells["StudentId"].Value.ToString();
                txtFirstName.Text = row.Cells["FirstName"].Value.ToString();
                txtLastName.Text = row.Cells["LastName"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
            }
        }
    }
}
