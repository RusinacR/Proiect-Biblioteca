using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace PROIECT_EXemplu_interfata
{
    
    public partial class Alege_carte_Retur : Form
    {
        string t2;
        string tent = "Rescue me";
        static string conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|/carti.accdb;";
        OleDbConnection con = new OleDbConnection(conString);
        OleDbCommand cmd;
        OleDbDataAdapter adaptor;
        DataTable dt = new DataTable();

        public Alege_carte_Retur()
        {
            InitializeComponent();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            //selectia
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            
        }
        private void afiseaza()
        {
            con.Open();
            adaptor = new OleDbDataAdapter("select * from imprumuturi where Carte like '" + tent + "%'", con);
            dt = new DataTable();
            adaptor.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

           


        }
        private void clearTxts()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            //textBox4.Text = "";
            //textBox5.Text = "";
            //comboBox1.Text = "";

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            //textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString(); ;
           // textBox5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString(); ;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            adaptor = new OleDbDataAdapter("select * from imprumuturi where Carte like '" + textBox1.Text + "%'", con);
            dt = new DataTable();
            adaptor.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Returneaza.s1 = textBox2.Text;
            Returneaza.s2 = textBox3.Text;
            this.Close();
        }

        private void Alege_carte_Retur_Load(object sender, EventArgs e)
        {
            t2 = Returneaza.text;
            textBox2.Text = Returneaza.text;
            con.Open();
            adaptor = new OleDbDataAdapter("select * from imprumuturi where Nume like '" + t2 + "%'", con);
            dt = new DataTable();
            adaptor.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
    }
}
