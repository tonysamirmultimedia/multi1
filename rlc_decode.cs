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
    public partial class rlc_decode : Form
    {
        public rlc_decode(string input)
        {
            InitializeComponent();
            inc.Text = input;
            rlc_dec();
        }

        private void inc_TextChanged(object sender, EventArgs e)
        {
            label2.Text = inc.Text.Length.ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label3.Text = textBox2.Text.Length.ToString();
        }
        private void rlc_dec()
        {
            try
            {

                String code = inc.Text;
                String Solution = "";
                for (int i = 0; i < code.Length; i += 2)
                {
                    int num = int.Parse(code[i].ToString());
                    char sympol = code[i + 1];
                    for (int j = 0; j < num; j++)
                    {
                        Solution += sympol;
                    }
                }
                textBox2.Text = Solution;
            }
            catch (Exception ee)
            {

                MessageBox.Show(ee.Message.ToString());
            }

        }

        private void rlc_decode_Load(object sender, EventArgs e)
        {

        }
    }
}
