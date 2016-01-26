using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gluteneria.Game;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Diagnostics;

namespace Gluteneria.elements
{

    public class Person : Element
    {
        protected Dictionary<int, Brush> Graphs= IMAGES.PACMAN_YELLOW;
        public int lives { get; private set; }
        public int score { get; private set; }
        private int counter=0, modTime=0 ;
        private double Speed;
        public direction dir = new direction(Key.Up);


        public Person() : this(r.Next(40) + 5, r.Next(40) + 5, 4, 1) {  }
    
    public Person(int X, int Y): this(X, Y, 4, 1) {   }

    public Person(double X, double Y, double size , int site) :base(X,Y)
    {
        this.setSize(size);
        this.setSize(4);
        this.newRect();
        lives = 4;  
        Speed=0.4;
    }

    public override void must()
    {
        id = 2;
    }
    public override void redraw()
    {
        rectFill();
        //this.Animation();
        base.redraw();
    }

    private void rectFill() 
    {
        if (counter > 7)
            rect.Fill = Graphs[dir.side - 1];
        else rect.Fill = Graphs[dir.side + 3];
    }

    public void moveUpdate()
     {
      modTimeToNormal();
      this.setX(this.getX() + this.dir.dirX * getSpeed());
      this.setY(this.getY() + this.dir.dirY * getSpeed());
      if (counter++ == 15)
       {
        counter = 0;
        this.sizeMinus();
       }
     }

    public void setSide(Key key)
     {
        dir.setDir(key);
     }

     private double getSpeed()
    { return 2*Speed / getSize(); }

    public void sizePlus()
    {
        this.setSize(this.getSize() + 0.7);
        rect.Width = rect.Height = (int)(getSize() * Game.GEngine.SIZE);
    }
    public void sizeMinus()
    {
        this.setSize(this.getSize() - 0.1);
        rect.Width = rect.Height = (int)(getSize() * 10);
    }

    public void dead()
    {
        this.updatePosition(r.Next(40) + 5, r.Next(40) + 5, 4);
        this.newRect();        
        if(lives!=0)score/=this.lives--;
    }

   

    #region metodyPrywatne

    protected void updatePosition(double X, double Y, double size)
    {
        this.setX(X);
        this.setY(Y);
        this.setSize(size);
    }

    private bool NoLives()
    { return lives > 0; }

    private void modTimeToNormal()
    {
        if (modTime <= 0)
            modReset();
        if(modTime>0)
            modTime--;
    }//modTime do dupy
    private void modReset()
    {
        modTime = 0;
        Speed = 0.4;
    }

    #endregion


    public void setModTime()
    {
        this.modTime = 0;
    } 
    public void giveBonus(bonuses profit)
    {
        if(profit.value!=-1)
        {
            this.setSize(getSize() + profit.size);
            Speed += profit.speed;
            score += profit.value;
            modTime += profit.modTime;         
        }
    }
    }
}
