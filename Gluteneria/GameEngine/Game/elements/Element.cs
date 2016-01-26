using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using GameEngine.Game.Interfaces;

namespace GameEngine.elements
{


    public abstract class Element : IElement
    {
        public static Random R = new Random(DateTime.Now.Millisecond);
        public double X { get; protected set; }
        public double Y { get; protected set; }
        public double Size { get; protected set; }
        protected int Id;
        public Rectangle Rect { get; set; }
        public ImageBrush Brush { get; protected set; }
        

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        protected Element()
        {
            Must();
            Rect = new Rectangle();
        }

        protected Element(double x, double y) : this() { this.X = x; this.Y = y;  }

        public abstract void Must();
        
        public virtual void Redraw()
        {
            ThicknessAnimation ta = new ThicknessAnimation
            {
                From = Rect.Margin,
                To = new Thickness(X*Game.SingleplayerEngine.Size, Y*Game.SingleplayerEngine.Size, 0, 0),
                Duration = new Duration(TimeSpan.FromMilliseconds(2))
            };
            Rect.BeginAnimation(Rectangle.MarginProperty, ta);
        }

        public bool InRange(Element e)
        {
            return ColideCir(this, e);
        }

        private bool ColideCir(Element a, Element b)
        {
            var minDistance=(a.Size/2)+(b.Size/2);
            return ( minDistance>Math.Sqrt(Math.Pow(a.GetCenterX()-b.GetCenterX(),2)+Math.Pow(a.GetCenterY()-b.GetCenterY(),2)));
        }
        private bool ColideRect(Element a,Element b)
        {
            return ((a.X <= b.X && b.X <= a.getE_X()) && (b.Y <= a.getS_Y() && (a.Y <= b.getS_Y() ? true : false)));
        }

        protected void NewRect()
        {
            Rect.Width = Rect.Height = GetSize() * Game.SingleplayerEngine.Size;
        }

        private double getS_Y() { return this.Y + this.Size; }
        private double getE_X() { return this.X + this.Size; }

        double GetCenterX()
        {
            return X + (Size / 2);
        }

        double GetCenterY()
        {
            return Y + (Size / 2);
        }

        public int GetValue() { return Id; }

        public double GetSize() { return Size; }
        
    }
}
