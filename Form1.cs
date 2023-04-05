using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicOperations
{
    public partial class Form1 : Form
    {
        public string Dir
        {
            set {label2.Text = value; }
        }

        public bool SubDirs
        {
            get { return radioButton2.Checked; }
        }
        
        public bool DirName
        {
            get { return chbx_adddirname.Checked; }
        }

        public decimal StartValue
        {
            get { return numericUpDown1.Value; }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            numericUpDown1.Minimum =  decimal.MinValue;
            numericUpDown1.Maximum = decimal.MaxValue;
            Dir = Environment.CurrentDirectory; 
        }

        private void btn_chosedir_Click(object sender, EventArgs e)
        {
            Program.ChoosePath();
        }

        private void btn_rename_Click(object sender, EventArgs e)
        {
            Program.Rename();
        }
    }
}
