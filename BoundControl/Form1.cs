using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace BoundControl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Connection();
            BindingSourceProperties();
            ComboBoxProperties();
        }
        void Connection()
        {
            string sConnectionStr = "Server=localhost; Port=5432; User Id=postgres; Password=1234765; Database=lab4uchbd";
            NpgsqlConnection sConnection = new NpgsqlConnection(sConnectionStr);
            sConnection.Open();
            DataSetFill(sConnection);
            sConnection.Close();
        }

        void DataSetFill(NpgsqlConnection sConnection)
        {
            string uSelect = "SELECT * FROM universities";
            string fSelect = "SELECT * FROM faculties";
            string sSelect = "SELECT * FROM students";
            NpgsqlDataAdapter uAdapter = new NpgsqlDataAdapter(uSelect, sConnection);
            NpgsqlDataAdapter fAdapter = new NpgsqlDataAdapter(fSelect, sConnection);
            NpgsqlDataAdapter sAdapter = new NpgsqlDataAdapter(sSelect, sConnection);
            uAdapter.Fill(ds, "universities");
            fAdapter.Fill(ds, "faculties");
            ds.Relations.Add("uni_off_fk", ds.Tables["universities"].Columns["id"], ds.Tables["faculties"].Columns["u_id"]);
            sAdapter.Fill(ds, "students");
            ds.Relations.Add("fac_emp_fk", ds.Tables["faculties"].Columns["id"], ds.Tables["students"].Columns["f_id"]);
        }

        void BindingSourceProperties()
        {
            uSource.DataSource = ds;
            uSource.DataMember = "universities";
            fSource.DataSource = uSource;
            fSource.DataMember = "uni_off_fk";
            sSource.DataSource = fSource;
            sSource.DataMember = "fac_emp_fk";
        }

        void ComboBoxProperties()
        {
            comboBox1.DataSource = uSource;
            comboBox1.DisplayMember = "name_uni";
            comboBox2.DataSource = fSource;
            comboBox2.DisplayMember = "name_fac";
            comboBox3.DataSource = sSource;
            comboBox3.DisplayMember = "name_st";
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
