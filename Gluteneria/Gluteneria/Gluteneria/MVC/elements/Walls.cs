using System.Collections.Generic;
using Gluteneria.Events;
using System.Linq;
namespace Gluteneria.elements
{ 
    public class Brick : Element
     {
         
         public Brick(int x, int y):base(x,y)
         {
             this.setSize(2);
             this.newRect();
             rect.Fill = brush;

         }   
        public override void must() 
        {
            brush = IMAGES.BRICK_NORMAL;
            id = 1;
        }

        
     }



public class Walls
{     
    private List<List<Brick>> walls=new List<List<Brick>>();
   
    public delegate void ChangingHandler(object sender, WallColideArgs ca); // deklaracja delegata
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")]
    public event ChangingHandler Change;                                    //deklaracja zdarzenia
    
  public Walls()
  {
      this.newWall(0, 77, 1, 78);
      this.newWall(0, 0, 2, 58);
      this.newWall(56, 0, 3, 78);
      this.newWall(56, 77, 4, 58);
  }

  public Walls(double arenaWidth,double arenaHeight)
  {
      int wallsInWidth = (int)(arenaWidth / (Game.GEngine.SIZE))-2;
      int wallsInHeight = (int)(arenaHeight /(Game.GEngine.SIZE))-2;

      newWall(0, (int)arenaHeight / (Game.GEngine.SIZE) - 2, 1, wallsInHeight);
      newWall(0, 0, 2, wallsInWidth);
      newWall((int)arenaWidth / (Game.GEngine.SIZE) - 2, 0, 3, wallsInHeight);
      newWall((int)arenaWidth / (Game.GEngine.SIZE) - 2, (int)(arenaHeight / (Game.GEngine.SIZE)) - 4, 4, wallsInWidth);

  }
 private void newWall(int x,int y,int site,int length)
 {
     List<Brick> wall = new List<Brick>();
     for(int i=0; i<length;i+=2)
     {
         switch(site)
         {
             case 1: wall.Add(new Brick(x, y - i)); break;
             case 2: wall.Add(new Brick(x + i, y)); break;
             case 3: wall.Add(new Brick(x, y + i)); break;
             case 4: wall.Add(new Brick(x - i, y)); break;
         }
     
     }
     walls.Add(wall); 
 }

 public List<List<Brick>> getWalls()
 {
     return walls;
 }

     public void redraw()
        {
            foreach (List<Brick> ws in walls)
                foreach(Brick wall in ws)
                    wall.redraw();
        }

 public bool anyWall(int dirx,int diry,int range)
  {

    //  foreach (List<Brick> wall in walls)
    //      if((from brick in ))
      return false;
  }
 
 
 public bool inRange(Element s)
 {
     foreach(List<Brick> wall in walls)
         foreach (Brick brick in wall.Where(item=>item.inRange(s))) 
              {
                 Change(this, new WallColideArgs(s));
                 return true;
             }
     return false;        
 }
}
}
