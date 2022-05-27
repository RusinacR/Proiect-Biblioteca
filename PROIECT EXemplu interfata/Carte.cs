//Rusinac Ruben 3122B
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
    class Carte
    {
        static string conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|/carti.accdb;";
        OleDbConnection con = new OleDbConnection(conString);
        OleDbCommand cmd;
        OleDbDataAdapter adaptor;
        DataTable dt = new DataTable();

        TextBox t1;
        TextBox t2;
        TextBox t3;
        TextBox t4;
        ComboBox c1;
        DataGridView g1;

        public void afiseaza(DataGridView g)
        {
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from carti";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            OleDbDataAdapter adaptor = new OleDbDataAdapter(cmd);
            adaptor.Fill(dt);
            g.DataSource = dt;

            con.Close();


        }


        public void adauga1(DataGridView dt, string titlu, string autor, string editura, string status)
        {


            string sql = "INSERT INTO carti(TC,Autor,Editura,Status) VALUES('" + titlu + "','" + autor + "','" + editura + "','" + status + "')";
            cmd = new OleDbCommand(sql, con);
            try
            {
                con.Open();
                if ((cmd.ExecuteNonQuery() > 0))
                {


                    MessageBox.Show("S-a adaugat cu succes");
                }
                con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();

            }
        }

        public void adauga(DataGridView dt, string titlu, string autor, string editura, string status)
        {
            //sql STMT
            //cmd.CommandText = "insert into carti values('" + titlu + "','" + autor + "','" + Editura + "','" + status + "')";
            string sql = "INSERT INTO carti(TC,Autor,Editura,Status) VALUES('" + titlu + "','" + autor + "','" + editura + "','" + status + "')";
            cmd = new OleDbCommand(sql, con);

            //adauga param

            //cmd.Parameters.AddWithValue("@TITLU", titlu);
            //cmd.Parameters.AddWithValue("@AUTOR", autor);
            //cmd.Parameters.AddWithValue("@EDITURA",editura);
            //cmd.Parameters.AddWithValue("@STATUS", status);

            //deschide conexiunea si executa adaugarea

            try
            {
                con.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    //clearTxts();

                    MessageBox.Show("S-a adaugat cu succes");
                }
                con.Close();
                //receptie();
                afiseaza(g1);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();

            }
        }
        public void editeaza(int id, string titlu, string autor, string editura, string status)
        {
            //sql stmt
            //string sql = "UPDATE carti SET TC='" + titlu + "',Autor='" + autor + "',Editura='" + editura + "'WHERE Status=" + status + "";
            string sql = "UPDATE carti SET TC='" + titlu + "',Autor='" + autor + "',Editura='" + editura + "',Status='" + status + "' WHERE ID=" + id + "";
            cmd = new OleDbCommand(sql, con);

            //OPEN CON,UPDATE,RETRIEVE DGVIEW

            try
            {
                con.Open();
                adaptor = new OleDbDataAdapter(cmd);
                adaptor.UpdateCommand = con.CreateCommand();
                adaptor.UpdateCommand.CommandText = sql;

                if (adaptor.UpdateCommand.ExecuteNonQuery() > 0)
                {
                    //clearTxts();
                    MessageBox.Show("S-a editat cu succes");

                }
                con.Close();

                //refresh

                //receptie();
                //afiseaza(g1);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }

        }
        private void clearTxts(TextBox t1,TextBox t2,TextBox t3,TextBox t4,ComboBox c1)
        {
            t1.Text = "";
            t2.Text = "";
            t3.Text = "";
            t4.Text = "";
            c1.Text = "";

        }
    }
}
