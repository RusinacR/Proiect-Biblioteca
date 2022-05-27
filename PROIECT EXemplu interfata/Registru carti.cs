using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;


namespace PROIECT_EXemplu_interfata
{
    public partial class Form4 : Form
    {
        //static string conString= "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\Catalin\\Desktop\\Programare\\C#(sharp)\\PROIECT EXemplu interfata/carti1.accdb;";
        //static string conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Catalin\\Desktop\\Programare\\C#(sharp)\\PROIECT EXemplu interfata/carti.accdb;";
        static string conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|/carti.accdb;";
        OleDbConnection con = new OleDbConnection(conString);
        OleDbCommand cmd;
        OleDbDataAdapter adaptor;
        DataTable dt = new DataTable();


        public Form4()
        {
            InitializeComponent();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            //selectia
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            
            afiseaza();
            



        }

        //adaugare
        private void adauga(string titlu, string autor,string editura, string status)
        {
           
            //cmd.CommandText = "insert into carti values('" + titlu + "','" + autor + "','" + Editura + "','" + status + "')";
            string sql = "INSERT INTO carti(TC,Autor,Editura,Status) VALUES('" + titlu + "','" + autor + "','" + editura + "','" + status + "')";
            cmd = new OleDbCommand(sql,con);

            

            

            try
            {
                con.Open();
                if(cmd.ExecuteNonQuery()>0)
                {
                    clearTxts();

                    MessageBox.Show("S-a adaugat cu succes");
                }
                con.Close();
                //receptie();
                afiseaza();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
                
            }
        }

        //inserare in Datagrid

        private void insereaza(string id,string titlu,string autor,string editura,string status)
        {
            dataGridView1.Rows.Add(id,titlu, autor, editura, status);
        }

       

        private void afiseaza()
        {
            //con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from carti";
            //cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            OleDbDataAdapter adaptor = new OleDbDataAdapter(cmd);
            adaptor.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();


        }

        //editeaza datagrid-ul
        private void editeaza(int id,string titlu, string autor, string editura, string status)
        {
            
            //string sql = "UPDATE carti SET TC='" + titlu + "',Autor='" + autor + "',Editura='" + editura + "'WHERE Status=" + status + "";
            string sql = "UPDATE carti SET TC='" + titlu + "',Autor='" + autor + "',Editura='" + editura +"',Status='"+status + "' WHERE ID=" + id + "";
            cmd = new OleDbCommand(sql, con);

           

            try
            {
                con.Open();
                adaptor = new OleDbDataAdapter(cmd);
                adaptor.UpdateCommand = con.CreateCommand();
                adaptor.UpdateCommand.CommandText = sql;

                if (adaptor.UpdateCommand.ExecuteNonQuery() > 0)
                {
                    clearTxts();
                    MessageBox.Show("S-a editat cu succes");

                }
                con.Close();

                //refresh

                
                afiseaza();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }

        }
        
            private void sterge(int id)
        {
            
            String sql = "DELETE FROM carti WHERE ID=" + id + "";
            cmd = new OleDbCommand(sql, con);
            
            try
            {
                con.Open();
                adaptor = new OleDbDataAdapter(cmd);
                adaptor.DeleteCommand = con.CreateCommand();
                adaptor.DeleteCommand.CommandText = sql;
                
                if (MessageBox.Show("Sunteti sigur?", "DELETE", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("S-a sters cu succes");
                    }
                }
                con.Close();
                //receptie();
                afiseaza();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

    
        //clear txt
        private void  clearTxts()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.Text = "";

        }


        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            //textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString(); ;
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString(); ;
        }

        private void insert_Click(object sender, EventArgs e)
        {
            adauga(textBox2.Text, textBox3.Text, textBox4.Text, comboBox1.Text);
        }

        private void update_Click(object sender, EventArgs e)
        {
            String selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            int id = Convert.ToInt32(selected);
            editeaza(id, textBox2.Text, textBox3.Text, textBox4.Text,comboBox1.Text);
        }

        private void delete_Click(object sender, EventArgs e)
        {
            String selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            int id = Convert.ToInt32(selected);
            sterge(id);
        }



        //cautare
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
             if(e.KeyChar==(char)13)
             {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from carti where TC='" + textBox1.Text + "'";
                cmd.ExecuteNonQuery();
                //DataTable dt = new DataTable();
                OleDbDataAdapter adaptor = new OleDbDataAdapter(cmd);
                adaptor.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }


        }
        

        /*private void textBox1_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from carti where TC='" + textBox1.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            OleDbDataAdapter adaptor = new OleDbDataAdapter(cmd);
            adaptor.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

            
        }*/

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            //con = new  OleDbConnection(conString);
            con.Open();
            adaptor = new OleDbDataAdapter("select * from carti where TC like '" + textBox1.Text + "%'", con);
            dt = new DataTable();
            adaptor.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            Meniu f1 = new Meniu();
            f1.Show();
            this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        //sterge


    }
    

    //adauga carte

    
    




}
