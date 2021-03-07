using mmultimedia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using WindowsFormsApplication3;

namespace multimedia
{
    public partial class shanon : Form
    {
        public shanon(string input)
        {
            InitializeComponent();
            textBox1.Text = input;
           
        }
        private SolidBrush blackBrush;

        int totalNodes = 0, maxTreeHeight = 0;
        nodes  the_tree = null;
        private Pen blackPen;
        Dictionary<char, int> repetation_chars = new Dictionary<char, int>();

        List<List<String>> Encode_Bits = new List<List<string>>();


        private void shanon_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            panel1.Paint += new PaintEventHandler(panel1_Paint);
            panel1.SizeChanged += new EventHandler(panel1_SizeChanged);
            panel1.Font = new Font("SansSerif", 20.0f, FontStyle.Bold);
            blackPen = new Pen(Color.Black);
            blackBrush = new SolidBrush(Color.Black);

        }
        private int TreeHeight(nodes t)
        {
            if (t == null) return -1;
            else return 1 + Math.Max(TreeHeight(t.Left), TreeHeight(t.Right));
        }


        private void button2_Click(object sender, EventArgs e)
        {
            data_show data = new data_show(Encode_Bits);
            data.Show();
        }

        nodes generate_Tree(List<Tuple<int, char>> list)
        {
            if (list.Count <= 1)
            {
                nodes ret = new nodes();
                ret.Left = null;
                ret.Right = null;
                ret.sympol = list[0].Item2.ToString();
                return ret;
            }
            List<Tuple<int, char>> temp_list = new List<Tuple<int, char>>();
            int all_sympol_count = 0;
            temp_list = list;
            for (int i = 0; i < temp_list.Count; i++)
            {
                all_sympol_count += temp_list[i].Item1;
            }
            List<Tuple<int, char>> right_list = new List<Tuple<int, char>>();
            List<Tuple<int, char>> left_list = new List<Tuple<int, char>>();

            int sum_to_hulf = 0;
            for (int i = 0; i < temp_list.Count; i++)
            {
                if (sum_to_hulf < all_sympol_count / 2)
                {
                    right_list.Add(temp_list[i]);
                    sum_to_hulf += temp_list[i].Item1;
                }
                else
                {
                    left_list.Add(temp_list[i]);
                }
            }
            nodes right_node = new nodes();
            nodes left_node = new nodes();

            right_node = generate_Tree(right_list);
            left_node = generate_Tree(left_list);

            right_node.bit = "1";
            left_node.bit = "0";

            nodes nnn = new nodes();
            nnn.Left = left_node;
            nnn.Right = right_node;
            return nnn;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            repetation_chars.Clear();
            if (textBox1.Text == "")
            {
                return;
            }
            String code = textBox1.Text;
            for (int i = 0; i < code.Length; i++)
            {
                if (repetation_chars.ContainsKey(code[i]))
                {
                    repetation_chars[code[i]]++;
                }
                else repetation_chars[code[i]] = 1;
            }
            List<Tuple<int, char>> rep = new List<Tuple<int, char>>();

            var arrayOfAllKeys = repetation_chars.Keys.ToArray();

            //var arrayOfAllValues = repetation_chars.Values.ToArray();

            for (int i = 0; i < arrayOfAllKeys.Length; i++)
            {
                Tuple<int, char> ttt = Tuple.Create(repetation_chars[arrayOfAllKeys[i]], arrayOfAllKeys[i]);

                rep.Add(ttt);
            }
            rep.Sort((x, y) => y.Item1.CompareTo(x.Item1));
            nodes c = new nodes();
            //MessageBox.Show(c.ToString());
            //int g = 3;

            the_tree = generate_Tree(rep);

            Encode_Bits.Clear();

            List<String> temp1 = new List<string>();
            temp1.Add("Sympol");
            temp1.Add("bits");
            temp1.Add("counter");
            Encode_Bits.Add(temp1);

            calculate_bits(the_tree, "");


            int depth = 1;
            totalNodes = 0;
            InorderTraversal(the_tree, depth);
            maxTreeHeight = TreeHeight(the_tree);
            rename_nodes(the_tree);
            set_prob(the_tree);
            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (the_tree != null)
                DrawTree(the_tree, e.Graphics);
        }
        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            panel1.Invalidate();
        }
        private void InorderTraversal(nodes t, int depth)
        {
            if (t != null)
            {
                InorderTraversal(t.Left, depth + 1); //add 1 to depth (y coordinate) 
                t.Xpos = totalNodes++ + 1; //x coord is node number in inorder traversal
                t.Ypos = depth - 1; // mark y coord as depth
                InorderTraversal(t.Right, depth + 1);
            }
        }


        public void DrawTree(nodes root, Graphics g)
        {
            try
            {
                panel1.Width = ClientSize.Width - 8;
                panel1.Height = ClientSize.Height - 8;

                int Width = panel1.Width;
                int Height = panel1.Height;
                int dx = 0, dy = 0, dx2 = 0, dy2 = 0, ys = 20;
                int XSCALE, YSCALE;
                int treeHeight = TreeHeight(root);

                XSCALE = (int)(Width / totalNodes); //scale x by total nodes in tree
                YSCALE = (int)((Height - ys) / (maxTreeHeight + 1)); //scale y by tree height

                if (root != null)
                {
                    // inorder traversal to draw each node
                    DrawTree(root.Left, g); // do left side of inorder traversal 
                    dx = root.Xpos * XSCALE; // get x,y coords., and scale them 
                    dy = root.Ypos * YSCALE;
                    string s = root.sympol.ToString(); //get the word at this node
                    g.DrawString(s, panel1.Font, blackBrush, new PointF(dx - ys, dy));
                    // this draws the lines from a node to its children, if any
                    if (root.Left != null)
                    {
                        //draws the line to left child if it exists
                        dx2 = root.Left.Xpos * XSCALE;
                        dy2 = root.Left.Ypos * YSCALE;
                        g.DrawLine(blackPen, dx, dy, dx2, dy2);
                    }

                    if (root.Right != null)
                    {
                        //draws the line to right child if it exists
                        dx2 = root.Right.Xpos * XSCALE;//get right child x,y scaled position
                        dy2 = root.Right.Ypos * YSCALE;
                        g.DrawLine(blackPen, dx, dy, dx2, dy2);
                    }

                    DrawTree(root.Right, g); //now do right side of inorder traversal 
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message.ToString());
            }
        }


        public String rename_nodes(nodes tree)
        {
            if (tree == null) return "";
            if (tree.Left == null && tree.Right == null)
                return tree.sympol;

            String right_name = rename_nodes(tree.Right);
            String left_name = rename_nodes(tree.Left);

            if (tree.sympol == "#")
            {
                tree.sympol = right_name + " , " + left_name;
            }
            return tree.sympol;
        }

        public int set_prob(nodes tree)
        {
            if (tree == null)
            {
                return 0;
            }
            else if (tree.Left == null && tree.Right == null)
            {
                tree.prob = 1;
                return 1;
            }
            int left = set_prob(tree.Left);
            int right = set_prob(tree.Right);

            tree.prob = left + right;
            the_tree = tree;
            return 1;
        }

        public string calculate_bits(nodes root, string code)
        {
            if (root.Left == null && root.Right == null)
            {
                List<String> temp = new List<string>();
                temp.Add(root.sympol);
                temp.Add(code);
                temp.Add(root.prob.ToString());
                Encode_Bits.Add(temp);
                return "";
            }

            calculate_bits(root.Left, code + "0");
            calculate_bits(root.Right, code + "1");


            return "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label2.Text = textBox1.Text.Length.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

    }
}
