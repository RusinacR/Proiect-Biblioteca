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
using System.Collections;
namespace PROIECT_EXemplu_interfata
{
    public partial class Form6 : Form
    {
        string nrc;
        string colName = "Nume";
        public static TextBox t1, t2;
        public static string s1, s2;
        Alege_carte f1 = new Alege_carte();
        static string conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|/carti.accdb;";
        OleDbConnection con = new OleDbConnection(conString);
        OleDbCommand cmd,cmd1;
        OleDbDataAdapter adaptor;
        DataTable dt = new DataTable();


        public Form6()
        {
            
            InitializeComponent();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            comboBox2.SelectedIndex = 2;
            //apelare functie de aafisare
            afiseaza();
            RemoveDuplicateRows(dt, colName);
            dataGridView1.Columns[5].Visible = false;
            //textBox5.Text = s1;
            //textBox6.Text = s2;

        }
        private string numara(string nume, string prenume)
        {
            OleDbConnection conexiune = new OleDbConnection(conString);
            string Nume_intreg = nume + " " + prenume;
            string imp = "SELECT COUNT(*) from imprumuturi WHERE ([Nume]='" + Nume_intreg +  "')";
            conexiune.Open();
           
            
            cmd = new OleDbCommand(imp, conexiune);
            
            int result = ((int)cmd.ExecuteScalar());
            result++;
            string resultString = result.ToString();
            
            conexiune.Close();
            return resultString;
            

        }
        private void adauga(string nume, string prenume, string CNP, int Cmax,string carte,string autor,string nrc)
        {
            string Nume_intreg=nume+" "+prenume;
            string imp= "INSERT INTO imprumuturi(Nume,Carte,Autor) VALUES('" + Nume_intreg +  "','" + carte + "','" + autor + "')";
            
            //cmd.CommandText = "insert into carti values('" + titlu + "','" + autor + "','" + Editura + "','" + status + "')";
            string sql = "INSERT INTO persoane(Nume,Prenume,CNP,Cmax,Carti,NrC) VALUES('" + nume + "','" + prenume + "','" + CNP + "','" + Cmax +"','"+carte +"','"+nrc+"')";
            cmd = new OleDbCommand(sql, con);
            cmd1 = new OleDbCommand(imp, con);
            //adauga param

           

            try
            {
                con.Open();
                if ((cmd.ExecuteNonQuery() > 0)&&(cmd1.ExecuteNonQuery() > 0))
                {
                    clearTxts();

                    MessageBox.Show("S-a adaugat cu succes");
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

        //inserare in Datagrid

        private void insereaza(string id, string nume, string prenume, string CNP, int Cmax,string carte)
        {
            dataGridView1.Rows.Add(id, nume, prenume, CNP, Cmax,carte);
        }
  
        private void afiseaza()
        {
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "select * from persoane";
            cmd.CommandText = "select * from persoane order by (ID) desc";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            OleDbDataAdapter adaptor = new OleDbDataAdapter(cmd);
            adaptor.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();
            RemoveDuplicateRows(dt, colName);

        }


        private void editeaza(int id, string nume, string prenume, string CNP, int Cmax,string carti)
        {
            
            //string sql = "UPDATE carti SET TC='" + titlu + "',Autor='" + autor + "',Editura='" + editura + "'WHERE Status=" + status + "";
            string sql = "UPDATE persoane SET Nume='" + nume + "',Prenume='" + prenume + "',CNP='" + CNP + "',Cmax='" + Cmax +"',Carti='"+carti+ "' WHERE ID=" + id + "";
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
            
            String sql = "DELETE FROM persoane WHERE ID=" + id + "";
            cmd = new OleDbCommand(sql, con);
            
            try
            {
                con.Open();
                adaptor = new OleDbDataAdapter(cmd);
                adaptor.DeleteCommand = con.CreateCommand();
                adaptor.DeleteCommand.CommandText = sql;
                
                if (MessageBox.Show("Sigur ??", "DELETE", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("S-a sters cu succes");
                    }
                }
                con.Close();
                
                afiseaza();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }


        private void clearTxts()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.Text = "";

        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            //textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString(); ;
            comboBox2.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString(); ;
        
    }

        private void butonadg_Click(object sender, EventArgs e)
        {
            int cmax = Int32.Parse(comboBox2.Text);
            int nrct = Int32.Parse(nrc);
            if (nrct > cmax)
                MessageBox.Show("AI depasit limita");
            else
                adauga(textBox2.Text, textBox3.Text, textBox4.Text, Int32.Parse(comboBox2.Text), textBox5.Text,textBox6.Text,nrc);
            
            
        }

        private void update_Click(object sender, EventArgs e)
        {
            String selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            int id = Convert.ToInt32(selected);
            editeaza(id, textBox2.Text, textBox3.Text, textBox4.Text,Int32.Parse(comboBox2.Text),textBox5.Text);
        }

        private void delete_Click(object sender, EventArgs e)
        {
            String selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            int id = Convert.ToInt32(selected);
            sterge(id);
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            
           // this.imprumuturiTableAdapter.Fill(this.cartiDataSet.imprumuturi);

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

            OleDbConnection conexiune = new OleDbConnection(conString);
            string nume_intreg=textBox2.Text+" "+textBox3.Text;
            comboBox1.Items.Clear();

            conexiune.Open();
            adaptor = new OleDbDataAdapter("select * from imprumuturi where Nume like '" + nume_intreg + "%'", conexiune);
            dt = new DataTable();
            adaptor.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                comboBox1.Items.Add(dr["Carte"].ToString());
            }
            //dataGridView1.DataSource = dt;
            conexiune.Close();
            //textBox5.Text=numara(textBox2.Text,textBox3.Text);






            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Meniu f1 = new Meniu();
            f1.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Alege_carte f1 = new Alege_carte();
            f1.Show();
            //f1.FormClosed += new FormClosedEventHandler(Form_Closed);


            //textBox5.Text = s1;
            //textBox6.Text = s2;


            f1.FormClosed += F1_FormClosed;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
            nrc = numara(textBox2.Text,textBox3.Text);
           
        }

        //private void textBox3_TextChanged(object sender, EventArgs e)
        //{
        //    textBox5.Text = numara(textBox2.Text, textBox3.Text);
        //}

        private void F1_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show("Ati parasit fereastra.");
            textBox5.Text = s1;
            textBox6.Text = s2;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //con = new  OleDbConnection(conString);
            con.Open();
            adaptor = new OleDbDataAdapter("select * from persoane where Nume like '" + textBox1.Text + "%'", con);
            dt = new DataTable();
            adaptor.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        public DataTable RemoveDuplicateRows(DataTable dTable, string colName)
        {
            //colName = "Nume";
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

            //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
            //And add duplicate item value in arraylist.
            foreach (DataRow drow in dTable.Rows)
            {
                if (hTable.Contains(drow[colName]))
                    duplicateList.Add(drow);
                else
                    hTable.Add(drow[colName], string.Empty);
            }

            //Removing a list of duplicate items from datatable.
            foreach (DataRow dRow in duplicateList)
                dTable.Rows.Remove(dRow);

            //Datatable which contains unique records will be return as output.
            return dTable;
        }
    }
}
