using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alrgo_02
{
    public partial class Form2 : Form
    {
        public bool IsCancel=false;
        public Form2()
        {
           InitializeComponent();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            IsCancel = false;
            node.Edge_value = Convert.ToInt32(textBox1.Text);
            textBox1.Text = null;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IsCancel = true;
            textBox1.Text = null;
            Close();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        
    }
}
