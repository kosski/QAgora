using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using GameEngine.Game;
using GameEngine.Game.Interfaces;

namespace GameEngine.elements
{
    public class Bot : AbstractPlayer
    {
        private int _count = 0;
        IElement _target = null;

        public Bot()
        {
            Dir=new Direction(new List<Key> { Key.Up, Key.Left, Key.Down, Key.Right },true);
            this.Dir.SetDir(Key.Up);
        }

        public Bot(double x, double y, double size, int site)
        {
            this.X = x;
            this.Y = y;
            this.Size = size;
        }

        public override void Must()
        {
            Graphs = Images.PacmanDark;
            Id = 3;
        }

        public override void MoveUpdate()
        {
            NextMove();
            ModTimeToNormal();
            this.X += this.Dir.DirX * GetSpeed();
            this.Y += this.Dir.DirY * GetSpeed();
            if (Counter++ != 15) return;
            Counter = 0;
            this.SizeMinus();
        }

        public void NextMove()
        {
            List<AbstractPlayer> players = SingleplayerEngine.Instance.GetElementsOf<AbstractPlayer>();
            List<Apple> points = SingleplayerEngine.Instance.GetElementsOf<Apple>();
            List<Brick> bricks = SingleplayerEngine.Instance.GetElementsOf<Brick>();
              
            if (_target == null || (!players.Contains(_target) && !points.Contains(_target)))
            {
                IElement nearestToEat = players
                    .Where(player => player.Size*2 <= this.GetSize())
                    .OrderBy(distanceFrom)
                    .FirstOrDefault();
                _target = nearestToEat != null? distanceFrom(nearestToEat) < distanceFrom(_target)
                    ? nearestToEat ?? points.OrderBy(distanceFrom).First()
                    : points.OrderBy(distanceFrom).First(): points.OrderBy(distanceFrom).First();
                                 
            }
            if (_count++ == 1)
                this.CatchTarget(_target, bricks);
            if (_count > 20) _count = 0;

        }

        private void CatchTarget(IElement target, List<Brick> bricks)
        {
            try
            {
                if (target != null)
                    if (horizonDist(target) > verticalDist(target))
                        HorizontalMove(target.X);
                    else VerticalMove(target.Y);
                //if (!brickOnRoad(bricks)) return;
                //Debug.WriteLine("Omijam " + X + " : " + Y);
                //CatchTarget(FindAlternativePath(target, bricks), bricks);
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void HorizontalMove(double eX)
        {
            this.Dir.SetDir(eX >= this.X ? Key.Right : Key.Left);
        }

        private void VerticalMove(double eY)
        {
            this.Dir.SetDir(eY >= this.Y ? Key.Down : Key.Up);
        }

        private IElement FindAlternativePath(IElement target, List<Brick> bricks)
        {
            for (var i = 5; i <= 50; i += 5)
                if (GameWindow.InArena(XShiftPlus(i), YShiftPlus(i)) &&
                    !brickOnRoad(bricks, XShiftPlus(i), YShiftPlus(i)))
                {
                    target = new Apple(XShiftPlus(i) + Dir.DirX * Size, YShiftPlus(i) + Dir.DirY * Size); break;
                }
                else if (GameWindow.InArena(XShiftMinus(i), YShiftMinus(i)) &&
                         !brickOnRoad(bricks, XShiftMinus(i), YShiftMinus(i)))
                {
                    target = new Apple(XShiftMinus(i) + Dir.DirX * Size, YShiftMinus(i) + Dir.DirY * Size); break;
                }
            Debug.WriteLine("Route to " + target.X + " : " + target.Y);
            return target;
        }

        private double XShiftPlus(int i) { return X + i * Math.Abs(Dir.DirY); }
        private double XShiftMinus(int i) { return X - i * Math.Abs(Dir.DirY); }

        private double YShiftPlus(int i) { return Y + i * Math.Abs(Dir.DirX); }
        private double YShiftMinus(int i) { return Y - i * Math.Abs(Dir.DirX); }


        private bool brickOnRoad(List<Brick> bricks)
        {
            return brickOnRoad(bricks, X, Y);
        }

        private bool brickOnRoad(List<Brick> bricks, double eX, double eY)
        {
        
            return bricks.Any(brick => brick.InField(eX, eX + Dir.DirX * 7, eY, eY + Dir.DirY * 7, Size));
        }

        #region wyliczenia

        private double horizonDist(IElement point)
        { return Math.Abs(point.X - this.X); }
        private double verticalDist(IElement point)
        { return Math.Abs(point.Y - this.Y); }
        private double distanceFrom(IElement point)
        {
            return Math.Sqrt(Math.Pow(horizonDist(point), 2) + Math.Pow(verticalDist(point), 2));
        }

        private double horizonDist(double x)
        { return Math.Abs(x - this.X); }
        private double verticalDist(double y)
        { return Math.Abs(y - this.Y); }
        private double distanceFrom(double eX, double eY)
        {
            return Math.Sqrt(Math.Pow(horizonDist(eX), 2) + Math.Pow(verticalDist(eY), 2));
        }
        #endregion
    }
}
