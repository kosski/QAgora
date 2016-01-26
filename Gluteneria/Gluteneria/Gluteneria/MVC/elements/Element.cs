using Gluteneria.Events;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Gluteneria.elements
{
    public abstract class Element
    {
        private double X, Y, size;
        protected int id;
        public Rectangle rect { get; set; }
        public ImageBrush brush { get; protected set; }
        public static Random r = new Random(DateTime.Now.Millisecond);
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        public Element() { must(); rect = new Rectangle(); }

        public Element(double X, double Y):this() { this.X = X; this.Y = Y; }

        public abstract void must();
        
        public virtual void redraw()
        {
            ThicknessAnimation ta = new ThicknessAnimation();
            ta.From = rect.Margin;
            ta.To = new Thickness(X * Game.GEngine.SIZE, Y * Game.GEngine.SIZE, 0, 0);
            ta.Duration = new Duration(TimeSpan.FromMilliseconds(2));
            rect.BeginAnimation(Rectangle.MarginProperty, ta);
            //Canvas.SetTop(this.rect, (int)this.getY() * Game.GEngine.SIZE);
            // Canvas.SetLeft(this.rect, (int)this.getX() * Game.GEngine.SIZE);
        }

        public bool inRange(Element e)
        {
            return colide(e,this) || e.colide(this,e);
        }

        private bool colide(Element a,Element b)
        {
            return ((a.X <= b.X && b.X <= a.getE_X()) ? (b.Y <= a.getS_Y() ? a.Y <= b.getS_Y() ? true : false : false) : false);
        }

        protected void newRect() { rect.Width = rect.Height = getSize() * Game.GEngine.SIZE; }

        private double getS_Y() { return this.Y + this.size; }
        private double getE_X() { return this.X + this.size; }

        public double getX() { return X; }

        public double getY() { return Y; }

        public int getValue() { return id; }

        public double getSize() { return size; }

        public void setX(double X) { this.X = X; }

        public void setY(double Y) { this.Y = Y; }

        public void setValue(int Value) { this.id = Value; }

        public void setSize(double size) { this.size = size; }
    }
}
