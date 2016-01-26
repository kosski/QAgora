using Gluteneria.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Gluteneria.elements
{
    public class Bot  : Person
{
    private int count=0;
    public Bot(){this.setSide(Key.Up);}

    public Bot(double X, double Y, double size , int site) 
    {
        this.setX(X);
        this.setY(Y);
        this.setSize(size);       
    }

    public override void must()
    {
        Graphs = IMAGES.PACMAN_DARK;
        id = 3;
    }
 
 public void nextMove(Person player, List<PoinT> points)
 {
     if(count++==1)
     {
      Element target = null;
      foreach(PoinT point in points){
          if (target == null ? true : distanceFrom(point) < distanceFrom(target))
              if (count != 4) target = point; }     
     if(player.getSize()*2<=this.getSize() && distanceFrom(player)<distanceFrom(target)) target=player;     
     this.catchTarget(target);
     }
     if(count>20)count=0;
 }
 
 private void catchTarget(Element target)
 {
               if(target!=null)
                 if(horizonDist(target)>verticalDist(target))
                     if(target.getX()>=this.getX())
                          this.setSide(Key.Right); 
                     else this.setSide(Key.Left);
                 else
                    if(target.getY()>=this.getY())
                         this.setSide(Key.Down); 
                    else this.setSide(Key.Up); 
 }

 private void senseWalls()
 {

 }
    #region wyliczenia

    private double horizonDist(Element point)
    { return Math.Abs(point.getX() - this.getX()); }
    private double verticalDist(Element point)
    { return Math.Abs(point.getY() - this.getY()); }
    private double distanceFrom(Element point)
    {
        return Math.Sqrt(Math.Pow(horizonDist(point), 2) + Math.Pow(verticalDist(point), 2));
    }

    #endregion
}
}
