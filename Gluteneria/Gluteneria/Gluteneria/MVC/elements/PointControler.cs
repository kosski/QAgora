using Gluteneria.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace Gluteneria.elements
{
    public class PointControler
    {
        
        public delegate void ChangingHandler(object sender, PointColideArgs ca);    // deklaracja delegata
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")]
        public event ChangingHandler Change;                                        //deklaracja zdarzenia
        
        
        public static List<bonuses> pointKinds = new List<bonuses>();
        public List<PoinT> points=new List<PoinT>();
        public PointControler()
        {
            readPointConf();
            this.newPoints();
        }
    
   
        public void UpdatePoints(List<PoinT> points)
        {
            this.points=points;
        }
        public void newPoints()
        {
                points.Clear();
                int random= (Element.r.Next(125)+25)/25;
                for (int i = 0; i <= random; i++)
                    points.Add(newPoint());
                Debug.WriteLine("Nowe Punkty w ilosci: " + random + " : " + points.Count);
        }
    
        public void removePoint(PoinT point)
        { points.Remove(point); }

        public bonuses pointInRange(Person guy)
        {
            foreach(PoinT point in points.Where(point=>point.inRange(guy)))
                {
                    points.Remove(point);
                    Debug.WriteLine(DateTime.Now.Millisecond+": Zostalo puntow: "+ points.Count);
                    Change(this, new PointColideArgs(point.rect));                        
                    return point.getBonuses();
                }
            return new bonuses();
        }

        public void redraw()
        {
            foreach (PoinT point in points)
            {
                point.redraw();
            }
        }

        private PoinT newPoint()
        {
            return new PoinT (pointKinds[Element.r.Next(pointKinds.Count - 1)]);
        }

        private void readPointConf()
        {
            XDocument xml = XDocument.Load("./PointKinds.xml");
            IEnumerable<XElement> Xresult = xml.Root.Descendants();

            foreach (XElement point in Xresult)
                pointKinds.Add(new bonuses
                    (            
                    Int32.Parse(point.Attribute("value").Value),
                    point.Attribute("brush").Value,
                    double.Parse(point.Attribute("speed").Value),
                    double.Parse(point.Attribute("size").Value),
                    Int32.Parse(point.Attribute("modTime").Value)
                    ));        
        }

    }
}
