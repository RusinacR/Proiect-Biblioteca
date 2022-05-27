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
    public partial class Alege_carte : Form
    {
        //public static TextBox t1, t2;
        public string t1, t2;
        static string conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|/carti.accdb;";
        OleDbConnection con = new OleDbConnection(conString);
        OleDbCommand cmd;
        OleDbDataAdapter adaptor;
        DataTable dt = new DataTable();


        public Alege_carte()
        {
            InitializeComponent();

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            //selectia
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
           
            afiseaza();
        }
        private void afiseaza()
        {
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from carti";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            OleDbDataAdapter adaptor = new OleDbDataAdapter(cmd);
            adaptor.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();


        }
        private void clearTxts()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            //comboBox1.Text = "";

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            //textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString(); ;
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString(); ;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //con = new  OleDbConnection(conString);
            con.Open();
            adaptor = new OleDbDataAdapter("select * from carti where TC like '" + textBox1.Text + "%'", con);
            dt = new DataTable();
            adaptor.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Form6.s1 = textBox2.Text;
            Form6.s2 = textBox3.Text;
            
            //t1 = textBox2.Text;
            //t2 = textBox3.Text;
           
            //Form6.t1.Text = textBox2.Text;
            //Form6.t2.Text = textBox3.Text;



            //Form6.textBox5.Text = textBox2.Text;
            //Form6.textBox6.Text = textBox3.Text;
            this.Close();
        }
    }
}
