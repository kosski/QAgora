using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using GameEngine.Events;
using GameEngine.Game.Interfaces;

namespace GameEngine.elements
{

    public class Brick : Element, IBrick
    {

        public Brick(int x, int y) : base(x, y)
        {
            this.Size=2;
            this.NewRect();
            Rect.Fill = Brush;

        }
        public override void Must()
        {
            Brush = Images.BrickNormal;
            Id = 1;
        }

        public bool InField(double eX, double stopX, double eY, double stopY, double size)
        {
            if (eX == stopX)
                return (eX <= this.X && eX + size >= this.X) &&
                       (eY > stopY ? stopY < this.Y && eY > this.Y : eY < this.Y && stopY > this.Y);
            else
                return (eX > stopX ? stopX < this.X && eX > this.X : eX < this.X && stopX > this.X) &&
                       (eY <= this.Y && eY + size >= this.Y);
        }

    }



    public class Walls : IController<Brick>
    {
        public List<Brick> ControledElements { get; set; }

        public delegate void ChangingHandler(object sender, WallColideArgs ca); // deklaracja delegata
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")]
        public event ChangingHandler Change;                                    //deklaracja zdarzenia

        private double arenaWidth, arenaHeight;
        public Walls(ISettnigs conf)
        {
            ControledElements=new List<Brick>();
            ControllerStart(conf);
            int wallsInWidth = (int)(arenaWidth / (Game.SingleplayerEngine.Size)) - 2;
            int wallsInHeight = (int)(arenaHeight / (Game.SingleplayerEngine.Size)) - 2;

            NewWall(0, (int)arenaHeight / (Game.SingleplayerEngine.Size) - 2, 1, wallsInHeight);
            NewWall(0, 0, 2, wallsInWidth);
            NewWall((int)arenaWidth / (Game.SingleplayerEngine.Size) - 2, 0, 3, wallsInHeight);
            NewWall((int)arenaWidth / (Game.SingleplayerEngine.Size) - 2, (int)(arenaHeight / (Game.SingleplayerEngine.Size)) - 2, 4, wallsInWidth);

            //this.ControledElements = new List<Brick>();
            //foreach (var wall in conf.WallsMap)
            //{
            //    this.NewWall(wall[0], wall[1], wall[2], wall[3]);
            //}
            
            
        }


        public void ControllerStart(ISettnigs conf)
        {
            arenaHeight = conf.ArenaHeight;
            arenaWidth = conf.ArenaWidth;
        }

        public void Tick(object sender, EventArgs eventArgs)
        {
            
        }

        public void ConnectEvents(EventsController eventsController)
        {
           
        }

        public Walls(List<Brick> controledElements, ISettnigs conf)
        {
            this.ControledElements = new List<Brick>(controledElements);
        }
        public Walls()
        {
            int wallsInWidth = (int)(arenaWidth / (Game.SingleplayerEngine.Size)) - 2;
            int wallsInHeight = (int)(arenaHeight / (Game.SingleplayerEngine.Size)) - 2;

            NewWall(0, (int)arenaHeight / (Game.SingleplayerEngine.Size) - 2, 1, wallsInHeight);
            NewWall(0, 0, 2, wallsInWidth);
            NewWall((int)arenaWidth / (Game.SingleplayerEngine.Size * 2), 0, 3, wallsInHeight);
            NewWall((int)arenaWidth / (Game.SingleplayerEngine.Size * 2), (int)(arenaHeight / (Game.SingleplayerEngine.Size)) - 2, 4, wallsInWidth);
            NewWall(25, 15, 3, 33);
        }

        private void NewWall(int x, int y, int site, int length)
        {
            var wall = new List<Brick>();
            for (var i = 0; i < length; i += 2)
            {
                switch (site)
                {
                    case 1: wall.Add(new Brick(x, y - i)); break;
                    case 2: wall.Add(new Brick(x + i, y)); break;
                    case 3: wall.Add(new Brick(x, y + i)); break;
                    case 4: wall.Add(new Brick(x - i, y)); break;
                }

            }
            ControledElements.AddRange(wall);
        }

        public List<Brick> GetWalls()
        {
            return ControledElements;
        }

        public void Redraw()
        {
            foreach (Brick wall in ControledElements)
                wall.Redraw();
        }

        public bool InRange(Element s)
        {
            if (ControledElements.Where(item => item.InRange(s)).Cast<Brick>().Any())
            {
                Change(this, new WallColideArgs(s));
                return true;
            }
            return false;
        }

        public List<Brick> GetControledElements()
        {
            return ControledElements;
        }

    }
}
