using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Gluteneria.MVC.elements;

namespace Gluteneria.Game.Interfaces
{
    public class EventBox<T,A>
    {
        public delegate void ChangingHandler(T sender, A Args);

        public event ChangingHandler BoxedEvent;

        public EventBox(ChangingHandler eventDelegate)
        {
            AddHandler(eventDelegate);
        }

        public void AddHandler(ChangingHandler eventDelegate)
        {
            BoxedEvent += eventDelegate;
        }
        public void RaiseEvent(T sender, A args)
        {
            BoxedEvent(sender, args);
        }
    }
    public interface ISettnigs
    {
        int HumanPlayers { get; set; }  
        int ComputerPlayers { get; set; }
        int Lives { get; set; }
        int HowManyApples { get; set; }
        List<List<int>> WallsMap{ get; set; } 
        List<List<Key>> PlayersControlKeys { get; set; }
        double ArenaWidth { get; set; }
        double ArenaHeight { get; set; }
    }

    public interface IEventer
    {
        void ConnectEvents(EventsController eventsController);
    }

    public interface IController<T> : IEventer where T : IElement
    {
        List<T> ControledElements { get;set;}

        void ControllerStart(ISettnigs conf);
        void Tick(object sender, EventArgs eventArgs);
    }
    
    public interface IElement
    {
        double X { get; }
        double Y { get; }
        double Size { get; }
        Rectangle Rect { get; set; }
        ImageBrush Brush { get; }
        void Must();
        void Redraw();
        bool InRange(Element e);
        int GetValue();
        double GetSize();
    }
    public interface IApple 
    { 
        Bonuses GetBonuses();
        int GetValue();
        double GetSize();
    }
    public interface IPlayer 
    {
        void MoveUpdate();
        int Lives { get; }
        int Score { get; }
        void SizePlus();
        void SizeMinus();
        void Dead();
        void SetModTime();
        void GiveBonus(Bonuses profit);
    }
    public interface IBrick 
    {
        bool InField(double eX, double stopX, double eY, double stopY, double size);
    }


    #region Singleton
    public abstract class SingletonBase<T> where T : Singleton<T>, new()
    {
        protected static Singleton<T> instance;

        protected SingletonBase()
        {
        }

        public abstract void Start(GameSettings conf);
    }

    public class Singleton<T> : SingletonBase<T> where T : Singleton<T>, new()
    {
        public static T Instance => (T)(instance ?? (instance = new T()));

        protected Singleton()
        {
        }

        public override void Start(GameSettings conf)
        {
        }
    } 
    #endregion

}
