using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace WindowsForms
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private Circle CircleToDraw;
        private Circle activeCircle;
        private List<Circle> circles;
        private bool isFigureChecked;
        private static int doubleClickCounter;

        public Form1()
        {
            InitializeComponent();
            graphics = panelForPainting.CreateGraphics();
            circles = new List<Circle>();
            CircleToDraw = new Circle();
            isFigureChecked = false;
            doubleClickCounter = 0;
            Hints.Text = UI.StartHints;
        }
        private void Open_Click(object sender, EventArgs e)
        {
            openFileDialog1 = UI.CreateOpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Reset();
                circles = CircleBL.DeserializeList(openFileDialog1.FileName);
                Graphic.Redraw(panelForPainting, graphics, circles);
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            saveFileDialog1 = UI.CreateSaveFileDialog();

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                CircleBL.SerializeList(circles, saveFileDialog1.FileName);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void sahpesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sahpesToolStripMenuItem.DropDownItems.Clear();
            List<ToolStripMenuItem> ul = new List<ToolStripMenuItem>();
            foreach (Circle circl in circles)
            {
                ToolStripMenuItem li = new ToolStripMenuItem(circl.Color.ToString() + "-" + circl.GetRadius());
                li.Click += new EventHandler(sahpesToolStripMenuItemDropDown_Click);
                ul.Add(li);
            }
            sahpesToolStripMenuItem.DropDownItems.AddRange(ul.ToArray());
        }

        private void sahpesToolStripMenuItemDropDown_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsim = sender as ToolStripMenuItem;
            var circl = circles.Find(p => p.Color.ToString()+"-"+p.GetRadius() == tsim.Text);
            circles.Remove(circl);
            circles.Add(circl);
            Graphic.Redraw(panelForPainting, graphics, circles);
            activeCircle = circl;
            isFigureChecked = true;
            Hints.Text = UI.StartHints + UI.ChooseCircle + UI.ActionsWithCircle;
            UI.Show(ChooseColor, DeleteCircle);
            this.Invalidate();
        }


        private void ChooseColor_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = false;
            MyDialog.ShowHelp = true;
            MyDialog.Color = ChooseColor.ForeColor;
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                circles.Remove(activeCircle);
                activeCircle.Color = MyDialog.Color;
                circles.Add(activeCircle);
                Graphic.Redraw(panelForPainting, graphics, circles);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panelForPainting_Paint(object sender, PaintEventArgs e)
        {
            

        }

        private void panelForPainting_DoubleClick(object sender, EventArgs e)
        {
            MouseEventArgs mouseEvent = e as MouseEventArgs;
            if (mouseEvent.Button == MouseButtons.Left)
            {
                Point point = new Point(mouseEvent.Location.X, mouseEvent.Location.Y);
                if (doubleClickCounter == 0)
                {
                    CircleToDraw.Center = panelForPainting.PointToClient(Cursor.Position);
                    doubleClickCounter++;
                }
                else if (doubleClickCounter == 1)
                {
                    CircleToDraw.PointOnCircleOutline = panelForPainting.PointToClient(Cursor.Position);
                    circles.Add(CircleToDraw);
                    CircleToDraw = new Circle();
                    Graphic.Redraw(panelForPainting, graphics, circles);
                    Hints.Text = UI.StartHints+UI.ChooseCircle;
                    doubleClickCounter = 0;
                }
            }
        }

       

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset();
            Hints.Text = UI.StartHints;
        }

        private void panelForPainting_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouseEvent = e as MouseEventArgs;
            if (mouseEvent.Button == MouseButtons.Right)
            {
                Point point = new Point(mouseEvent.Location.X, mouseEvent.Location.Y);

                if (!isFigureChecked)
                {
                    activeCircle = circles.FirstOrDefault(p =>
                        {
                            if(Circle.GetDistanseBetweenPoints(p.Center,p.PointOnCircleOutline)>Circle.GetDistanseBetweenPoints(p.Center,point))
                            {
                                return true;
                            }
                            return false;
                    });
                    if (activeCircle != null)
                    {
                        isFigureChecked = true;
                        Hints.Text = UI.StartHints + UI.ChooseCircle + UI.ActionsWithCircle;
                        UI.Show(ChooseColor,DeleteCircle);
                    }
                }
                else
                {
                    if (activeCircle == null)
                    {
                        throw new ApplicationException("error, this figure does not exist ... ");
                    }
                    circles.Remove(activeCircle);
                    activeCircle.Move(point);
                    circles.Add(activeCircle);
                    Graphic.Redraw(panelForPainting, graphics, circles);
                }
            }
            else
            {
                if(activeCircle != null)
                {
                    Hints.Text = UI.StartHints + UI.ChooseCircle;
                }
                activeCircle = null;
                isFigureChecked = false;
                UI.Hide(ChooseColor,DeleteCircle);
            }
        }

        private void DeleteCircle_Click(object sender, EventArgs e)
        {
            if (activeCircle != null)
            {
                circles.Remove(activeCircle);
                activeCircle = null;
                isFigureChecked = false;
                UI.Hide(ChooseColor, DeleteCircle);
                Graphic.Redraw(panelForPainting, graphics, circles);
                if(circles.Count==0)
                {
                    Hints.Text = UI.StartHints;
                }
                else { Hints.Text = UI.StartHints + UI.ChooseCircle; }
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string caption = "Information box";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBox.Show(UI.INFORMATION_MESSAGE, caption, buttons);
        }
    }
}
