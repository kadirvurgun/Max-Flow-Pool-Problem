using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.OrTools.Graph;
using Google.OrTools.Algorithms;
using Google.Protobuf;


namespace Proje
{
    public partial class Form2 : Form
    {
        public static Form1 form1 = new Form1();
        public String[] nodeLoc=new string[20];
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Step = 1;
        }
        public void SolveMaxFlow(int sourceS, int sinkS)
        {
            Rectangle rectangle = new Rectangle(0, 0, 597, 454);
            Graphics g;
            g = this.CreateGraphics();
            PaintEventArgs e = new PaintEventArgs(g, rectangle);
            GraphicsPath capPath = new GraphicsPath();
            progressBar1.Value += 10;
            MaxFlow maxFlow = new MaxFlow();
            for (int i = 0; i < numArcs; ++i)
            {
                int arc = maxFlow.AddArcWithCapacity(start[i], end[i],
                                                     capacities[i]);
                if (arc != i) throw new Exception("HATA");
            }
            int source = Convert.ToInt32(textBox1.Text);
            int sink = Convert.ToInt32(textBox2.Text);
            progressBar1.Value += 10;

            listBox1.Items.Add("Max Flow işlemi yapılıyor: " + numNodes + " düğüm , " + numArcs + " kenar , Kaynak=" + source + ", Rota=" + sink);
            int solveStatus = Convert.ToInt32(maxFlow.Solve(source, sink));
            if (solveStatus == Convert.ToInt32(MaxFlow.Status.OPTIMAL))
            {
                listBox1.Items.Add("Max. flow: " + maxFlow.OptimalFlow());
                listBox1.Items.Add("");
                listBox1.Items.Add("  Kenar     Akış / Kapasite");
                progressBar1.Value += 50;
                for (int i = 0; i < numArcs; ++i)
                {
                    listBox1.Items.Add(maxFlow.Tail(i) + " -> " +
                                      maxFlow.Head(i) + "    " +
                                      string.Format("{0,3}", maxFlow.Flow(i)) + "  /  " +
                                      string.Format("{0,3}", maxFlow.Capacity(i)));

                    
                    var first = nodeLoc[maxFlow.Tail(i)].Split(' ').Select(Int32.Parse).ToList();
                    var sec = nodeLoc[maxFlow.Head(i)].Split(' ').Select(Int32.Parse).ToList();
                    Pen blackPen = new Pen(Color.Black, 3);
                    PointF point1 = new PointF(first[0], first[1]);
                    PointF point2 = new PointF(sec[0], sec[1]);
                    capPath.AddLine(-2, 0, 2, 0);
                    capPath.AddLine(-2, 0, 0, 2);
                    capPath.AddLine(0, 2, 2, 0);
                    blackPen.CustomEndCap = new CustomLineCap(null, capPath);
                    e.Graphics.DrawLine(blackPen, point1, point2);
                }

            }
            else
            {
                listBox1.Items.Add("Max Flow problemini çözerken bir hata oluştu. Çözücü statüsü " +
                                  solveStatus);
            }
        }
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Dispose();
        }
        public int numNodes { get; set; }
        public int numArcs { get; set; }
        public int[] start { get; set; }
        public int[] end { get; set; }
        public int[] capacities { get; set; }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 10;

            int source = 0, sink = numNodes - 1;
            if (!String.IsNullOrEmpty(textBox1.Text) && !String.IsNullOrEmpty(textBox2.Text))
            {
                source = Convert.ToInt32(textBox1.Text);
                sink = Convert.ToInt32(textBox2.Text);
                progressBar1.Value += 10;
                SolveMaxFlow(source, sink);
            }
            else
            {
                SolveMaxFlow(source, sink);
            }
            progressBar1.Value += 10;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            int locY = 200, locX = 30, i = 0,nodes=numNodes;
            using (Pen pen = new Pen(Color.Red, 10))
            {
                locY += 150;
                Rectangle rc = new Rectangle(locX, locY, 10, 10);
                e.Graphics.DrawEllipse(pen, rc);
                nodeLoc[i] = locX.ToString()+" "+locY.ToString();
                SizeF szF = e.Graphics.MeasureString(i.ToString(), this.Font);
                Rectangle rc2 = new Rectangle(new Point(rc.Left + rc.Width / 2, rc.Top + rc.Height / 2), new Size(1, 1));
                rc2.Inflate((int)szF.Width, (int)szF.Height);
                e.Graphics.DrawString(i.ToString(), this.Font, Brushes.Black, rc2, sf);
                locY -= 150;
                i++;
                if (nodes%1==0)
                {
                    nodes += 1;
                }
                for (int k = 0; k < (nodes-2)/2; k++)
                {
                    locX += 100;
                    for (int j = 0; j < 2; j++)
                    {
                        if (nodes-2 == i)
                        {
                            break;
                        }
                        locY += 100;
                        rc = new Rectangle(locX, locY, 10, 10);
                        e.Graphics.DrawEllipse(pen, rc);
                        nodeLoc[i] = locX.ToString() + " " + locY.ToString();

                        szF = e.Graphics.MeasureString(i.ToString(), this.Font);
                        rc2 = new Rectangle(new Point(rc.Left + rc.Width / 2, rc.Top + rc.Height / 2), new Size(1, 1));
                        rc2.Inflate((int)szF.Width, (int)szF.Height);
                        e.Graphics.DrawString(i.ToString(), this.Font, Brushes.Black, rc2, sf);
                        i++;
                        
                    }
                    locY = 200;
                }
                locX += 100;
                locY += 150;
                rc = new Rectangle(locX, locY, 10, 10);
                e.Graphics.DrawEllipse(pen, rc);
                nodeLoc[i] = locX.ToString() + " " + locY.ToString();

                szF = e.Graphics.MeasureString(i.ToString(), this.Font);
                rc2 = new Rectangle(new Point(rc.Left + rc.Width / 2, rc.Top + rc.Height / 2), new Size(1, 1));
                rc2.Inflate((int)szF.Width, (int)szF.Height);
                e.Graphics.DrawString(i.ToString(), this.Font, Brushes.Black, rc2, sf);
                locY -= 150;
            }
        }
    }
}
