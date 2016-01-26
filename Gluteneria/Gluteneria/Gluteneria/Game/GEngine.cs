using Gluteneria.elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Gluteneria.Game
{
    #region struct dir
    public struct direction
    {
        public int dirX;
        public int dirY;
        public int side;
        public direction(Key key):this(){setDir(key);}
        
        public void setDir(Key key)
        {
            switch (key)
            {
                case Key.Up: if (side != 3) { dirX = 0; dirY = -1; side = 1; } else setDir(Key.Left); break;
                case Key.Left: if (side != 2) { dirX = -1; dirY = 0; side = 4; } else setDir(Key.Down); break;
                case Key.Down: if (side != 1) { dirX = 0; dirY = 1; side = 3; } else setDir(Key.Right);  break;
                case Key.Right: if (side != 4) { dirX = 1; dirY = 0; side = 2; } else setDir(Key.Up); break;
                default: { dirX = 0; dirY = -1; side = 1; break; }
            }
        }
        public int getSide()
        {
            return side;
        }
        public Key getKey()
        {
            switch(side)
            {
                case 1:return Key.Up;
                case 2:return Key.Right;
                case 3:return Key.Down;
                case 4:return Key.Left;
            }
            return Key.Up;
        }
        public void rewerse(int s)
        {
            switch(s)
            {
                case 1:setDir(Key.Down);break;
                case 2:setDir(Key.Left); break;
                case 3:setDir(Key.Up); break;
                case 4:setDir(Key.Right); break;
            }
            
        }
    }
    #endregion
    class GEngine
    {
        #region elements
        public Walls walls { get; private set; }
        public Person player { get; private set; }
        public Bot enemy { get; private set; }
        public PointControler points = new PointControler();
        public static readonly int SIZE = 10;

        // deklaracja delegata
        public delegate void ChangingHandler(object sender, EventArgs ca);

        //deklaracja zdarzenia
        public event ChangingHandler exitEvent;

        #endregion

        public GEngine(double arenaWidth, double arenaHeight)
        {
            this.walls = new Walls(arenaWidth,arenaHeight);
            this.points = new PointControler();
            this.player = new Person();
            this.enemy = new Bot();
        }

        public void InitPlayer()
        {
            this.player.dead();
        }

        public void InitEnemy()
        {
            this.enemy.dead();
        }

        public void move()
        {
            enemy.nextMove(player,points.points);
            playerMove(player);
            playerMove(enemy);
            playersMeet();
        }

        private void playerMove(Person guy)
        {
            try{
                guy.moveUpdate();
                if (guy.getSize() <= 1) guy.dead();
                if (walls.inRange(guy) && guy.lives < 1)
                    guy = null;
                guy.giveBonus(points.pointInRange(guy));
                guy.redraw();
            }catch (Exception) { exitEvent(this, EventArgs.Empty); }
        }

        private void playersMeet()
        {
            if (player.inRange(enemy))
            {
                if ((enemy.getSize() * 2 <= player.getSize())) { enemy.dead(); }
                else if (player.getSize() * 2 <= enemy.getSize()) player.dead();
                else pingPong();
            }
        }

        private void pingPong()
        {
                if(Math.Abs(player.getX()-enemy.getX())<Math.Abs(player.getY()-enemy.getY()))
                    if (player.getX() > enemy.getX()) 
                         { player.setSide(Key.Right); enemy.setSide(Key.Left); }
                    else { player.setSide(Key.Left); enemy.setSide(Key.Right); }
                else 
                    if (player.getY() > enemy.getY())
                         { player.setSide(Key.Down); enemy.setSide(Key.Up); }
                    else { player.setSide(Key.Up); enemy.setSide(Key.Down); }
        }
        //walls
        public List<Rectangle> getWallRects()
        {
            List<Rectangle> result=new List<Rectangle>();
            foreach (List<Brick> wall in walls.getWalls())
                foreach (Brick brick in wall)
                    result.Add(brick.rect);
            return result;
        }

        public Rectangle guyRect()
        {
            return player.rect;
        }

        public Rectangle enemyRect()
        {
            return enemy.rect;
        }

        public IEnumerable<Rectangle> getPointsRect()
        {
            return from point in points.points select point.rect;
        }

        //funkcje zwracajace kwadraty kazdego obiektu

        public int getCountPoints()
        {
            return points.points.Count;
        }

        
    }
}
