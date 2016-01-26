using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Gluteneria.elements
{
    public static class IMAGES
    {

        //PLAYERS
        public static readonly Dictionary<int, Brush> PACMAN_YELLOW = new Dictionary<int, Brush>()
            {
                {0,new ImageBrush(new BitmapImage(new Uri("Images/pacman/N.png", UriKind.RelativeOrAbsolute)))},
                {1,new ImageBrush(new BitmapImage(new Uri("Images/pacman/E.png", UriKind.RelativeOrAbsolute)))},
                {2,new ImageBrush(new BitmapImage(new Uri("Images/pacman/S.png", UriKind.RelativeOrAbsolute)))},
                {3, new ImageBrush(new BitmapImage(new Uri("Images/pacman/W.png", UriKind.RelativeOrAbsolute)))},

                {4,new ImageBrush(new BitmapImage(new Uri("Images/pacman/Nz.png", UriKind.RelativeOrAbsolute)))},
                {5, new ImageBrush(new BitmapImage(new Uri("Images/pacman/Ez.png", UriKind.RelativeOrAbsolute)))},
                {6, new ImageBrush(new BitmapImage(new Uri("Images/pacman/Sz.png", UriKind.RelativeOrAbsolute)))},
                {7, new ImageBrush(new BitmapImage(new Uri("Images/pacman/Wz.png", UriKind.RelativeOrAbsolute)))}
            };
        public static readonly Dictionary<int, Brush> PACMAN_DARK = new Dictionary<int, Brush>()
            {
                {0,new ImageBrush(new BitmapImage(new Uri("Images/pacman/ON.png", UriKind.RelativeOrAbsolute)))},
                {1,new ImageBrush(new BitmapImage(new Uri("Images/pacman/OE.png", UriKind.RelativeOrAbsolute)))},
                {2,new ImageBrush(new BitmapImage(new Uri("Images/pacman/OS.png", UriKind.RelativeOrAbsolute)))},
                {3, new ImageBrush(new BitmapImage(new Uri("Images/pacman/OW.png", UriKind.RelativeOrAbsolute)))},

                {4,new ImageBrush(new BitmapImage(new Uri("Images/pacman/ONz.png", UriKind.RelativeOrAbsolute)))},
                {5, new ImageBrush(new BitmapImage(new Uri("Images/pacman/OEz.png", UriKind.RelativeOrAbsolute)))},
                {6, new ImageBrush(new BitmapImage(new Uri("Images/pacman/OSz.png", UriKind.RelativeOrAbsolute)))},
                {7, new ImageBrush(new BitmapImage(new Uri("Images/pacman/OWz.png", UriKind.RelativeOrAbsolute)))}
            };
        //BRICKS
        public static readonly ImageBrush BRICK_NORMAL = new ImageBrush(new BitmapImage(new Uri("./Images/brick.bmp", UriKind.RelativeOrAbsolute)));
        //POINTS
        public static readonly ImageBrush POINT_APPLE = new ImageBrush(new BitmapImage(new Uri("./Images/apple.gif", UriKind.RelativeOrAbsolute)));

        public static readonly ImageBrush GRAVE = new ImageBrush(new BitmapImage(new Uri("./Images/grave.gif", UriKind.RelativeOrAbsolute)));

        public static Rectangle PREPARE_GRAVE(double X,double Y)
        {
            Rectangle result = new Rectangle();
            Canvas.SetTop(result, (int)Y * Game.GEngine.SIZE);
            Canvas.SetLeft(result, (int)X * Game.GEngine.SIZE);
            result.Fill = GRAVE;
            return result;
        }
    

    }
}
