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
    public partial class arth : Form
    {
        public arth(string input)
        {
            InitializeComponent();
            textBox1.Text = input;
           
        }
        Dictionary<string, int> dictionary = new Dictionary<string, int>();
        Dictionary<string, Tuple<Double, Double>> char_range_from_to = new Dictionary<string, Tuple<Double, Double>>();

        List<List<String>> result = new List<List<string>>();

        private void arth_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "")
            {
                return;
            }
            dictionary.Clear();
            char_range_from_to.Clear();
            for (int i = 0; i < textBox1.Text.Length; i++)
            {
                if (dictionary.ContainsKey(textBox1.Text[i].ToString()))
                {
                    dictionary[textBox1.Text[i].ToString()]++;
                }
                else
                {
                    dictionary[textBox1.Text[i].ToString()] = 1;
                }
            }


            //claculate probability
            List<Tuple<String, Double>> prob = new List<Tuple<String, Double>>();

            for (int i = 0; i < dictionary.Count; i++)
            {
                prob.Add(Tuple.Create(dictionary.ElementAt(i).Key, (double)(dictionary.ElementAt(i).Value) / textBox1.Text.Length));
            }

            //claculate acumilative
            List<double> acum = new List<double>();

            acum.Add(prob[0].Item2);
            for (int i = 1; i < prob.Count; i++)
            {
                acum.Add(prob[i].Item2 + acum[i - 1]);
            }

            //range   sym      from      to
            List<Tuple<string, Double, Double>> range = new List<Tuple<string, Double, Double>>();
            range.Add(Tuple.Create(dictionary.ElementAt(0).Key, (Double)0, acum[0]));
            for (int i = 1; i < acum.Count; i++)
            {
                range.Add(Tuple.Create(dictionary.ElementAt(i).Key, acum[i - 1], acum[i]));
            }

            //add chars to dictionary 

            for (int i = 0; i < range.Count; i++)
            {
                char_range_from_to[range[i].Item1] = Tuple.Create(range[i].Item2, range[i].Item3);
            }
            result.Clear();
            arith_metod(0, 0, 1);




            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            dataGridView2.Columns.Clear();
            dataGridView2.Rows.Clear();



            try
            {


                dataGridView1.RowCount = char_range_from_to.Count;
                dataGridView1.ColumnCount = 4;


                dataGridView1.Columns[0].HeaderText = "Sympole";
                dataGridView1.Columns[1].HeaderText = "From";
                dataGridView1.Columns[2].HeaderText = "To";
                dataGridView1.Columns[3].HeaderText = "Range";


                for (int i = 0; i < result.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = char_range_from_to.ElementAt(i).Key.ToString();
                    dataGridView1.Rows[i].Cells[1].Value = char_range_from_to.ElementAt(i).Value.Item1;
                    dataGridView1.Rows[i].Cells[2].Value = char_range_from_to.ElementAt(i).Value.Item2;
                    dataGridView1.Rows[i].Cells[3].Value = char_range_from_to.ElementAt(i).Value.Item2 - char_range_from_to.ElementAt(i).Value.Item1;

                }
            }
            catch (Exception ee)
            {

                MessageBox.Show(ee.Message.ToString());
            }





            try
            {


                dataGridView2.RowCount = result.Count;
                dataGridView2.ColumnCount = result[0].Count;


                dataGridView2.Columns[0].HeaderText = "Sympole";
                dataGridView2.Columns[1].HeaderText = "From";
                dataGridView2.Columns[2].HeaderText = "To";
                dataGridView2.Columns[2].HeaderText = "Range";


                for (int i = 0; i < result.Count; i++)
                {

                    for (int j = 0; j < result[0].Count; j++)
                    {
                        dataGridView2.Rows[i].Cells[j].Value = result[i][j];
                    }
                }
            }
            catch (Exception ee)
            {

                MessageBox.Show(ee.Message.ToString());
            }

        }

        void arith_metod(int char_index, Double big_from, Double big_to)
        {
            if (char_index >= char_range_from_to.Count)
            {
                return;
            }

            Double current_char_from = char_range_from_to.ElementAt(char_index).Value.Item1;
            Double current_char_to = char_range_from_to.ElementAt(char_index).Value.Item2;

            Double big_from_minos_big_to = big_to - big_from;

            Double char_Start = current_char_from * big_from_minos_big_to;
            Double char_End = current_char_to * big_from_minos_big_to;
            Double char_range = char_End - char_Start;

            List<string> temp = new List<string>();
            temp.Add(char_range_from_to.ElementAt(char_index).Key.ToString());
            temp.Add(char_Start.ToString());
            temp.Add(char_End.ToString());
            temp.Add(char_range.ToString());

            result.Add(temp);


            arith_metod(char_index + 1, char_Start, char_End);


        }
    }
}
