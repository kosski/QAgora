using System;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using GameEngine.Game.Interfaces;

namespace GameEngine.elements
{
    public struct Bonuses
    {
        public ImageBrush Brush { get;  set; }
        public int Value { get;  set; }
        public double Speed { get;  set; }
        public double Size { get;  set; }
        public int ModTime { get;  set; }
        public Bonuses(int v,string b,double s ,double si,int mt):this()
        {
            this.Brush = new ImageBrush(new BitmapImage(new Uri(b, UriKind.RelativeOrAbsolute)));
            this.Value = v;
            this.Speed = s;
            this.Size = si;
            this.ModTime = mt;
        }
    }



    public class Apple : Element, IApple
    {
     private Bonuses _prop; 

     public Apple():this(Element.R.Next(55)+4,Element.R.Next(40)+4,new Bonuses()) { }

     public Apple(Bonuses prop):this(Element.R.Next(55)+4,Element.R.Next(40)+4,prop) { }

     public Apple(double x, double y) : this(x, y, new Bonuses()) { }

     public Apple(double x, double y, Bonuses prop): base(x, y)
     {
         this.Size = 4;
         this._prop=prop;
         this.NewRect();
         Rect.Fill = prop.Brush;
         //Debug.WriteLine("new Point | "+this.id+" | " + X + " : " + Y);
        }

        public override void Must()
        {
            Id = 10;
        }

        public Bonuses GetBonuses()
        { return _prop; }    
}
}
