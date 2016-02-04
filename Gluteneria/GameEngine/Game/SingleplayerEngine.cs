using GameEngine.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
//using System.Windows.Shapes;
using GameEngine.Game.Interfaces;
using GameEngine.elements;

namespace GameEngine.Game
{
    public sealed class SingleplayerEngine : Singleton<SingleplayerEngine>
    {
        #region elements
       // public Dictionary<string, object> ElementContainer { get; private set; }
        public Dictionary<string, object> ControllerContainer { get; private set; }
        public EventsController EventsController= new EventsController();
        public GameSettings conf;
        public static readonly int Size = 10;

        #endregion
        public SingleplayerEngine() : base()
        {
            
        }


        #region Overrides

        public override void Start(GameSettings conf)
        {
            ControllerContainer = new Dictionary<string, object>();
            SetControllerOf<Brick>(new Walls(conf));
            SetControllerOf<Apple>(new PointControler(conf));
            SetControllerOf<AbstractPlayer>(new PlayerController(conf));

            //SetControllerOf<EventBox>(new EventsController());
            ConnectEvents();
            //ElementContainer = new Dictionary<string, object>();
            //
            //{
            //    {"Players", GetControllerOf<IPlayer>().ControledElements},
            //    { "Points", ControllerContainer["Points"].ControledElements},
            //    { "Walls",  ControllerContainer["Walls"].ControledElements}
            //};

        }

        public void SetElementsOf<T>(T element) where T : IElement
        {
            GetControllerOf<T>().ControledElements.Add(element);
        }
        public List<T> GetElementsOf<T>() where T : IElement
        {
            return GetControllerOf<T>().ControledElements;
        }

        public void SetControllerOf<T>(IController<T> controller) where T:IElement
        {
            ControllerContainer.Add(typeof(T).ToString(),controller);
        }

        public IController<T> GetControllerOf<T>() where T : IElement
        {
            return (IController<T>) ControllerContainer[typeof (T).ToString()];
        }
        #endregion

        public void ConnectEvents()
        {
            foreach (var controller in ControllerContainer.Values.Cast<IEventer>())
            {
                controller.ConnectEvents(EventsController);
            }
        }

      
        //public List<Rectangle> GetWallRects()
        //{
        //    List<Rectangle> result = new List<Rectangle>();
        //    foreach (Brick brick in walls.getWalls())
        //        result.Add(brick.Rect);
        //    return result;
        //}

        //public Rectangle EnemyRect()
        //{
        //    return enemy.rect;
        //}

        //public IEnumerable<Rectangle> GetPointsRect()
        //{
        //    return from point in points.points select point.rect;
        //}

        //funkcje zwracajace kwadraty kazdego obiektu
        //TODO

    }
}
