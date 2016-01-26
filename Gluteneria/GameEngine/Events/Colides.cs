using Windows.UI.Xaml.Shapes;
using GameEngine.elements;

namespace GameEngine.Events
{
    public class WallColideArgs: System.EventArgs
    {
        public Rectangle GuyRect{get; set;}
        public int Id { get; set; }
        public WallColideArgs(Element player)
        {
            this.GuyRect=player.Rect;
            this.Id = player.GetValue();
        }

    }
    public class PointColideArgs : System.EventArgs
    {
        public Rectangle PointRect { get; set; }
        public PointColideArgs(Rectangle pointRect)
        {
            this.PointRect = pointRect;
        }

    }

    public class RectAnimationArgs : System.EventArgs
    {
        public Rectangle PointRect { get; set; }
        public RectAnimationArgs(Rectangle pointRect)
        {
            this.PointRect = pointRect;
        }

    }


}
