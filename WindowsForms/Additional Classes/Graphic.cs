using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace WindowsForms
{
    class Graphic
    {
        public static void Redraw(Panel panel, Graphics graphics, List<Circle> circles)
        {
            panel.Refresh();
            foreach (Circle item in circles)
            {
                item.Draw(graphics);
            }
        }      
    }
}
