using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using System.Xml.Linq;
using GameEngine.Events;
using GameEngine.Game;
using GameEngine.Game.Interfaces;

namespace GameEngine.elements
{
    public class PointControler : IController<Apple>
    {
        public delegate void ChangingHandler(object sender, PointColideArgs ca);    // deklaracja delegata
        public event ChangingHandler AddOnCanvas;                                        //deklaracja zdarzenia       
        public static List<Bonuses> PointKinds = new List<Bonuses>();
        public List<Apple> ControledElements { get; set; }
        public PointControler(ISettnigs conf)
        {
            ReadPointConf();
            ControllerStart(conf);
        }

        public void UpdatePoints(List<Apple> points, ISettnigs conf)
        {
            ReadPointConf();
            this.ControledElements=points;
            ControllerStart(conf);
        }
        public void ControllerStart(ISettnigs conf)
        {
            ControledElements=new List<Apple>();
            NewPoints();
            //TODO
        }

        public void Tick(object sender, EventArgs eventArgs)
        {
            if (ControledElements.Count == 0)
            {
                NewPoints();
                SingleplayerEngine.Instance.EventsController.RaiseEvent<object,List<Element>>("NewPoints",this,ControledElements.ToList<Element>());
            }
            Redraw();
        }

        public void ConnectEvents(EventsController eventsController)
        {
            eventsController.ConnectEvent<object,List<Element>>("GetPoint",EventsControllerOnGetPoint);
          // eventsController.GetPoint+= EventsControllerOnGetPoint;
        }

        public void EventsControllerOnGetPoint(object sender, List<Element> ca)
        {
            foreach (Apple apple in ca)
            {
                //Change(this, new PointColideArgs(apple.Rect));
                ControledElements.Remove(apple);
                
            }
        }

        public void NewPoints()
        {
                ControledElements.Clear();
                int random= (Element.R.Next(125)+25)/25;
                for (int i = 0; i <= random-1; i++)
                    ControledElements.Add(NewPoint());
               // Debug.WriteLine("Nowe Punkty w ilosci: " + random + " : " + ControledElements.Count);
        }
    
        public void RemovePoint(Apple point)
        { ControledElements.Remove(point); }

        public Bonuses PointInRange(AbstractPlayer guy)
        {
            foreach (var point in ControledElements.Where(point=>point.InRange(guy)).Cast<Apple>())
            {
                Debug.WriteLine(ControledElements.IndexOf(point) + point.X + " : " + point.Y+" | Zostalo puntow: " + ControledElements.Count);
                ControledElements.Remove(point);
                    
                //Change(this, new PointColideArgs(point.Rect));                        
                return point.GetBonuses();
            }
            return new Bonuses();
        }

        public void Redraw()
        {
            foreach (Apple point in ControledElements)
            {
                point.Redraw();
            }
        }

        private Apple NewPoint()
        {
            return new Apple (PointKinds[Element.R.Next(PointKinds.Count - 1)]);
        }

        private void ReadPointConf()
        {
            XDocument xml = XDocument.Load("./PointKinds.xml");
            IEnumerable<XElement> xresult = xml.Root.Descendants();

            foreach (XElement point in xresult)
                PointKinds.Add(new Bonuses
                    (            
                    Int32.Parse(point.Attribute("value").Value),
                    point.Attribute("brush").Value,
                    double.Parse(point.Attribute("speed").Value),
                    double.Parse(point.Attribute("size").Value),
                    Int32.Parse(point.Attribute("modTime").Value)
                    ));        
        }

        #region IEnumerable
        public IEnumerator<IApple> GetEnumerator()
        {
            return ControledElements.GetEnumerator();
        }

        #endregion
    }
}
