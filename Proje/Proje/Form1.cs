using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Google.OrTools.Graph;

namespace Proje
{
    public partial class Form1 : Form
    {
        public int texts = 0;
        public int numArcs;
        public int numNodes;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Visible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            int locateX = 150;
            int locateY = 80;
            int locate = 20;
            int locateEnd = 70;
            int locateCap = 140;
            texts = Convert.ToInt32(textBox1.Text);
            numNodes = texts;
            numArcs = Convert.ToInt32(textBox2.Text);
            for (int i = 0; i < numArcs; i++)
            {
                TextBox start = new TextBox();
                start.Name = "start_" + i;
                start.Width = 50;
                start.Height = 40;
                start.Location = new Point(locateX, locateY + locate);
                Controls.Add(start);

                TextBox end = new TextBox();
                end.Name = "end_" + i;
                end.Width = 50;
                end.Height = 40;
                end.Location = new Point(locateX + locateEnd, locateY + locate);
                Controls.Add(end);

                TextBox capacity = new TextBox();
                capacity.Name = "capacities_" + i;
                capacity.Width = 50;
                capacity.Height = 40;
                capacity.Location = new Point(locateX + locateCap, locateY + locate);
                Controls.Add(capacity);
                locate += 30;
            }
            button2.Visible = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form2 = new Form2();
            int[] startNode = new int[numArcs];
            int[] endNode = new int[numArcs];
            int[] capacities = new int[numArcs];

            for (int i = 0; i < numArcs; i++)
            {
                startNode[i] = Convert.ToInt32(((TextBox)Controls["start_" + (i).ToString()]).Text);
                endNode[i] = Convert.ToInt32(((TextBox)Controls["end_" + (i).ToString()]).Text);
                capacities[i] = Convert.ToInt32(((TextBox)Controls["capacities_" + (i).ToString()]).Text);
            }
            form2.numArcs = numArcs;
            form2.numNodes = numNodes;
            form2.start = startNode;
            form2.end = endNode;
            form2.capacities = capacities;
            this.Hide();
            form2.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
