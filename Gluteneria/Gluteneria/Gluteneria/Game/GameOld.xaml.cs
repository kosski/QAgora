using Gluteneria.elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Gluteneria.Game
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game1 : Window
    {
        private direction dir = new direction(Key.Right);
        DispatcherTimer timer;
        public static int count=0;
        #region elements
        
        Walls walls;
        Person guy;
        PointControler points = new PointControler();
        #endregion
        //public static readonly int SIZE = 10;

        public Game1()
        {
            InitializeComponent();
            initPoints();
            InitPlayer();
            initWalls();
            InitTimer();
            

        }

        private void WindowKeyDown(object sender, KeyEventArgs key)
        {
            if (key.Key == Key.Escape)
                this.Close();
            dir.setDir(key.Key);           
        }

        public void InitPlayer()
        {
            guy = new Person();
            guy.redraw();
            Can.Children.Add(guy.rect);
            
        }

        public void initWalls()
        {
            walls = new Walls();
            foreach (List<Wall> ws in walls.getWalls())
            {
                foreach (Wall wall in ws)
                {
                    Can.Children.Add(wall.rect);
                }
            }

            walls.redraw();
            
        }

        private void initPoints()
        {           
            points.redraw();
            foreach (PoinT point in points.points)
                Can.Children.Add(point.rect);

            
        }
        public void move()
        {
            guy.setX(guy.getX() + dir.dirX * guy.getSpeed());
            guy.setY(guy.getY() + dir.dirY * guy.getSpeed());
            guy.redraw();            
        }

        private void pointsEvent()
        {
            PoinT colide= points.pointInRange(guy);
            if (colide !=null)
            {
                guy.sizePlus();
                Can.Children.Remove(colide.rect);
                if (points.points.Count < 1)
                {
                    points.newPoints();
                    initPoints();
                }
            }
            points.redraw();
        }

        void InitTimer()
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timer.Start();
        }

        void timerTick(object sender, EventArgs e)
        {
            move();
            pointsEvent();
            if (count++ == 35)
            {
                count = 0;
                guy.sizeMinus();
            }
            
             if (walls.inRange(guy))
            {
                Can.Children.Remove(guy.rect);
                InitPlayer();
         
            }
        }

    }
}
