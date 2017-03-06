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

namespace ZH_2_1_rajzolós
{
    public partial class Form1 : Form
    {       
        int ox;
        int oy;
        int ex;
        int ey;
        bool lenyomva = false;
        Graphics g;
        Pen toll = new Pen(Color.Black,2);
        List<vonal> vonalak = new List<vonal>();
        public Form1()
        {
            InitializeComponent();
            g = this.CreateGraphics();
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseUp += Form1_MouseUp;
        }

        void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            lenyomva = false;
        }

        void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!lenyomva) return;
            vonal új = new vonal(ox,oy,e.X,e.Y);
            rajzol(új);
            vonalak.Add(új);
            ox = e.X;
            oy = e.Y;
        }

        private void rajzol(vonal új)
        {
            g.DrawLine(toll, új.ox, új.oy, új.ex, új.ey);
        }

        void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            lenyomva = true;
            ox = e.X;
            oy = e.Y;
        }

        private void Savebtn_Click(object sender, EventArgs e)
        {   
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory=Application.StartupPath;
            sfd.DefaultExt="csv";
            sfd.FileName="myrajz";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(sfd.FileName);
                foreach (vonal element in vonalak)
                {
                    vonal sor = (vonal)element;
                    sw.WriteLine(sor.ox.ToString() + ';' + sor.oy.ToString() + ';' + sor.ex.ToString() + ';' + sor.ey.ToString());
                }
            }
        }

        private void Loadbtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Application.StartupPath;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                vonalak.Clear();
                StreamReader sr = new StreamReader(ofd.FileName);
                while (!sr.EndOfStream)
                {   
                    string text = sr.ReadLine();
                    string[] szamok = text.Split(';');
                    vonal újvonal = new vonal(Convert.ToInt32(szamok[0]), Convert.ToInt32(szamok[1]), Convert.ToInt32(szamok[2]), Convert.ToInt32(szamok[3]));
                    vonalak.Add(újvonal);
                    rajzol(újvonal);
                }
            }
        }
    }
}
