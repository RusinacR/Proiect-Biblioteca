using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROIECT_EXemplu_interfata
{
    public partial class Meniu : Form
    {
        public Meniu()
        {
            InitializeComponent();
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            Form4 f1 = new Form4();
            f1.Show();
            this.Hide();

        }

        private void button2_MouseClick(object sender, MouseEventArgs e)
        {
            Form6 f1 = new Form6();
            f1.Show();
            this.Hide();
        }


        //redirectare catre forma returneaza carte
        private void button3_MouseClick(object sender, MouseEventArgs e)
        {
            Returneaza f1 = new Returneaza();
            f1.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
