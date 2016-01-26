using Gluteneria.elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Gluteneria.Events
{
    public class WallColideArgs: System.EventArgs
    {
        public Rectangle guyRect{get; set;}
        public int id { get; set; }
        public WallColideArgs(Element player)
        {
            this.guyRect=player.rect;
            this.id = player.getValue();
        }

    }
    public class PointColideArgs : System.EventArgs
    {
        public Rectangle PointRect { get; set; }
        public PointColideArgs(Rectangle PointRect)
        {
            this.PointRect = PointRect;
        }

    }

    public class RectAnimationArgs : System.EventArgs
    {
        public Rectangle PointRect { get; set; }
        public RectAnimationArgs(Rectangle PointRect)
        {
            this.PointRect = PointRect;
        }

    }


}
