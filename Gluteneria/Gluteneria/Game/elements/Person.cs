using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Gluteneria.Game;
using Gluteneria.Game.Interfaces;

namespace Gluteneria.MVC.elements
{


    public abstract class AbstractPlayer : Element, IPlayer
    {
        protected Dictionary<int, Brush> Graphs = Images.PacmanYellow;
        public int Lives { get; protected set; }
        public int Score { get; protected set; }
        protected int Counter = 0, ModTime = 0;
        protected double Speed;
        public Direction Dir { get;  set; }
        public delegate void ChangingHandler(object sender,ref Rectangle PlayerRect);

        public event ChangingHandler deadEvent;


        protected AbstractPlayer() : this(Element.R.Next(40) + 5, Element.R.Next(40) + 5, 4, 1) { }

        protected AbstractPlayer(int x, int y) : this(x, y, 4, 1) { }

        protected AbstractPlayer(double x, double y, double size, int site) : base(x, y)
        {
            Size = size;
            this.NewRect();
            Lives = 4;
            Speed = 0.6;
        }

        public override void Must()
        {
            Id = 2;
        }
        public override void Redraw()
        {
            RectFill();
            base.Redraw();
        }
        private void RectFill()
        {
            Rect.Fill = Counter > 7 ? Graphs[Dir.Side - 1] : Graphs[Dir.Side + 3];
        }

        public abstract void MoveUpdate();


   



        public void SizePlus()
        {
            Size += 0.7;
            Rect.Width = Rect.Height = (int)(GetSize() * Game.SingleplayerEngine.Size);
        }
        public void SizeMinus()
        {
            Size -= 0.1;
            Rect.Width = Rect.Height = (int)(GetSize() * 10);
        }

        public void Dead()
        {
            try
            {

                if (Lives != 0) Score /= this.Lives--;
                else this.Rect = null;
                Rectangle oldRectangle = this.Rect;
                this.ModReset();
                this.UpdatePosition(Element.R.Next(40) + 5, Element.R.Next(40) + 5, 4);
                this.NewRect();

            }
            catch (Exception)
            {
                SingleplayerEngine.Instance.EventsController.RaiseEvent<object,EventArgs>("GameOver",this,new EventArgs());
            }//deadEvent(this, ref oldRectangle);
            
        }



        #region metodyPrywatne
        protected double GetSpeed()
        { return 2 * Speed / GetSize(); }
        protected void UpdatePosition(double x, double y, double size)
        {
            this.X = x;
            this.Y = y;
            Size = size;
        }

        protected bool NoLives()
        { return Lives > 0; }

        protected void ModTimeToNormal()
        {
            if (ModTime <= 0)
                ModReset();
            if (ModTime > 0)
                ModTime--;
        }//modTime do dupy
        protected void ModReset()
        {
            ModTime = 0;
            Speed = 0.4;
        }

        #endregion


        public void SetModTime()
        {
            this.ModTime = 0;
        }
        public void GiveBonus(Bonuses profit)
        {
            if (profit.Value != -1)
            {
                Size += profit.Size;
                Speed += profit.Speed;
                Score += profit.Value;
                ModTime += profit.ModTime;
            }
        }

        public static Type GiveType<T>() where T: AbstractPlayer
        {
            return typeof(T);
        }


        public void KeyPressed(object sender, Key key)
        {
            if(!Dir.isBot)
                this.Dir.SetDir(key);
        }
    }
    public class Person : AbstractPlayer
    {
        public Person(List<Key> controlKeys ,int lives):base()
        {
            Dir=new Direction(controlKeys);
            Lives = lives;
        }
        public override void MoveUpdate()
        {
            ModTimeToNormal();
            this.X += this.Dir.DirX * GetSpeed();
            this.Y += this.Dir.DirY * GetSpeed();
            if (Counter++ != 15) return;
            Counter = 0;
            this.SizeMinus();
            
        }


    }
}
