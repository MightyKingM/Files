using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PathFinder
{
    public partial class MainWindow : Form
    {
        Bitmap bitmap;
        Graphics gfx;
        Map map;
        Path path;

        public MainWindow()
        {
            InitializeComponent();
            map = Map.FromFile(Environment.CurrentDirectory + "\\map.txt");
            this.WindowState = FormWindowState.Maximized;
            bitmap = new Bitmap(Width, Height);
            gfx = Graphics.FromImage(bitmap);
            path = PathPlotter.FindFastestPathToDestination(map);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            gfx.Clear(BackColor);
            map.Draw(gfx);
            path.Draw(gfx);
            pictureBox.Image = bitmap;
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouse = (MouseEventArgs)e;
            foreach(Node node in map.ComputeAllNodes())
            {
                Rectangle rect = new Rectangle(node.x * 100, node.y * 100, 99, 99);
                if(rect.IntersectsWith(new Rectangle(mouse.X,mouse.Y,1,1)))
                {
                    bool isinpath = path.nodes.Contains(node);
                    MessageBox.Show("Type: "+node.GetNodeType()+"\nNodeValue: "+node.value+"\nPart of Path: "+isinpath+"\nX: "+node.x+"\nY: "+node.y,"Node Info");
                }
            }
        }
    }
}
