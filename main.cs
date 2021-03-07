using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace multimedia
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            shanon rlc = new shanon(input.Text);
            rlc.ShowDialog();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            rlc rlc = new rlc(input.Text);
            rlc.ShowDialog();



        }

        private void input_TextChanged(object sender, EventArgs e)
        {
            label2.Text = input.Text.Length.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            rlc_decode rlcd = new rlc_decode(input.Text);
            rlcd.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            huffman rlcd = new huffman(input.Text);
            rlcd.ShowDialog();

        }

        private void button5_Click(object sender, EventArgs e)
        { 

        }

        private void button6_Click(object sender, EventArgs e)
        {
            arth rlcd = new arth(input.Text);
            rlcd.ShowDialog();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            adabtive_hufman rlcd = new adabtive_hufman();
            rlcd.ShowDialog();
        }

        private void main_Load(object sender, EventArgs e)
        {

        }
    }
}
