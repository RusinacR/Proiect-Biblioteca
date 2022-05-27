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
    public partial class Returneaza : Form
    {
        string colName = "Nume";
        public static string text= "";
        public static string s1, s2;
        static string conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|/carti.accdb;";
        OleDbConnection con = new OleDbConnection(conString);
        OleDbCommand cmd, cmd1;
        OleDbDataAdapter adaptor;
        DataTable dt = new DataTable();
        public Returneaza()
        {
            
            InitializeComponent();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            //comboBox2.SelectedIndex = 2;
            //apelare functie de aafisare
            afiseaza();
            RemoveDuplicateRows(dt, colName);
        }
        private void insereaza(string id, string nume, string prenume, string CNP, int Cmax, string carte)
        {
            dataGridView1.Rows.Add(id, nume, prenume, CNP, Cmax, carte);
        }
        private void afiseaza()
        {
            //con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "select * from persoane";
            cmd.CommandText = "select * from persoane order by (ID) desc";
            //cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            OleDbDataAdapter adaptor = new OleDbDataAdapter(cmd);
            adaptor.Fill(dt);
            dataGridView1.DataSource = dt;
            RemoveDuplicateRows(dt, colName);
            con.Close();


        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString(); ;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Meniu f1 = new Meniu();
            f1.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            text = textBox2.Text;
            Alege_carte_Retur f1 = new Alege_carte_Retur();
            f1.Show();
            f1.FormClosed += F1_FormClosed;
        }

        private void Returneaza_Load(object sender, EventArgs e)
        {

        }

        private void F1_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show("Ati parasit fereastra.");
            textBox5.Text = s1;
            textBox6.Text = s2;
        }
        private void sterge(string nume,string prenume,string carte)
        {
            //SQL STMT
            string nume_intreg = nume + " " + prenume;
            String sql = "DELETE FROM persoane WHERE Nume='" + nume + "' AND Prenume='"+prenume+"' AND Carti='"+carte+"'";
            String sql1= "DELETE FROM imprumuturi WHERE Nume = '" + nume_intreg + "' AND Carte = '"+carte+"'";
            cmd = new OleDbCommand(sql, con);
            cmd1 = new OleDbCommand(sql1, con);
            //'OPEN CON,EXECUTE DELETE,CLOSE CON
            try
            {
                con.Open();
                adaptor = new OleDbDataAdapter(cmd);
                adaptor.DeleteCommand = con.CreateCommand();
                adaptor.DeleteCommand.CommandText = sql;
                //PROMPT FOR CONFIRMATION
                if (MessageBox.Show("Sigur ??", "DELETE", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    if ((cmd.ExecuteNonQuery() > 0) && (cmd1.ExecuteNonQuery() > 0))
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

        private void button3_Click(object sender, EventArgs e)
        {
            sterge(textBox2.Text, textBox3.Text, textBox5.Text);
    }

        private void clearTxts()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            //comboBox1.Text = "";

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
