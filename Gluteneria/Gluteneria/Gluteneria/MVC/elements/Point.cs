using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Gluteneria.elements
{
    public struct bonuses
    {
        public ImageBrush brush { get; private set; }
        public int value { get; private set; }
        public double speed { get; private set; }
        public double size { get; private set; }
        public int modTime { get; private set; }
        public bonuses(int v,string b,double s ,double si,int mt):this()
        {
            this.brush = new ImageBrush(new BitmapImage(new Uri(b, UriKind.RelativeOrAbsolute)));
            this.value = v;
            this.speed = s;
            this.size = si;
            this.modTime = mt;
        }
    }
    public class PoinT : Element
    {
     private bonuses prop; 

     public PoinT():this(r.Next(55)+4,r.Next(40)+4,new bonuses()) { }

     public PoinT(bonuses prop):this(r.Next(55)+4,r.Next(40)+4,prop) { }

        public PoinT(int x, int y,bonuses prop)
        {
         this.setX(x);
         this.setY(y);
         this.setSize(4);
         this.setValue(prop.value);
         this.prop=prop;
         this.newRect();
         rect.Fill = prop.brush;
         //Debug.WriteLine("new Point | "+this.id+" | " + getX() + " : " + getY());
        }

        public override void must()
        {
            id = 10;
        }

        public bonuses getBonuses()
        { return prop; }    
}
}
