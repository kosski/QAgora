using System;
using System.Collections.Generic;
//using System.Windows.Controls;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace GameEngine.elements
{
    public static class Images
    {

        //PLAYERS
        public static readonly Dictionary<int, Brush> PacmanYellow = new Dictionary<int, Brush>()
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
        public static readonly Dictionary<int, Brush> PacmanDark = new Dictionary<int, Brush>()
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
        public static readonly ImageBrush BrickNormal = new ImageBrush(new BitmapImage(new Uri("./Images/brick.bmp", UriKind.RelativeOrAbsolute)));
        //POINTS
        public static readonly ImageBrush PointApple = new ImageBrush(new BitmapImage(new Uri("./Images/apple.gif", UriKind.RelativeOrAbsolute)));

        public static readonly ImageBrush Grave = new ImageBrush(new BitmapImage(new Uri("./Images/grave.gif", UriKind.RelativeOrAbsolute)));

        public static Rectangle PREPARE_GRAVE(double x,double y)
        {
            Rectangle result = new Rectangle();
            Canvas.SetTop(result, (int)y * Game.SingleplayerEngine.Size);
            Canvas.SetLeft(result, (int)x * Game.SingleplayerEngine.Size);
            result.Fill = Grave;
            return result;
        }
    

    }
}
