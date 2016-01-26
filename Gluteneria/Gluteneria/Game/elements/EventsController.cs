using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gluteneria.Game;
using Gluteneria.Game.Interfaces;
using System.Reflection;
namespace Gluteneria.MVC.elements
{
    public class EventsController // : IController<EventBox>
    {
        //public delegate void ChangingHandler(object sender, List<Element> ca);
        public Dictionary<string,object> ControledElements = new Dictionary<string, object>();


        public void ControllerStart(ISettnigs conf)
        {
            throw new NotImplementedException();
        }

        public void Tick(object sender, EventArgs eventArgs)
        {
            DetectPointGrabbed();
            DetectplayersCrash();
            DetectPlayersColisions();
        }




        public EventBox<T, A> GetEventBoxOf<T, A>(string eventName)
        {
            return (EventBox<T, A>) ControledElements[eventName];
        } 

        public void ConnectEvents(EventsController eventsController)
        {
            
        }

        //public event ChangingHandler playersMeet;
        //public event ChangingHandler GetPoint;
        //public event ChangingHandler playersCrash;

        public void DetectPlayersColisions()
        {
            Dictionary<AbstractPlayer, AbstractPlayer> colidedPlayer = new Dictionary<AbstractPlayer, AbstractPlayer>();
            foreach (var player in SingleplayerEngine.Instance.GetElementsOf<AbstractPlayer>())
            {
                List<AbstractPlayer> colisionsElements =SingleplayerEngine.Instance.GetElementsOf<AbstractPlayer>()
                        .Where(
                            p =>
                                !p.Equals(player) && p.InRange(player) &&
                                (!colidedPlayer.ContainsKey(p) || !colidedPlayer[p].Equals(player)))
                                .ToList();
                if (colisionsElements.Count <= 0) continue;
                foreach (var colision in colisionsElements)
                {
                    colidedPlayer.Add(player, colision);
                }
            }
            if (colidedPlayer.Count <= 0) return;
            foreach (var element in colidedPlayer)
            {
                //playersMeet?.Invoke(this, new List<Element> {element.Key,element.Value} );
                RaiseEvent<object, List<Element>>("playersMeet", this, new List<Element> { element.Key, element.Value });

            }
        }

        public void DetectplayersCrash()
        {
            List<AbstractPlayer> player =
                SingleplayerEngine.Instance.GetElementsOf<AbstractPlayer>().Where(
                    p => SingleplayerEngine.Instance.GetElementsOf<Brick>()
                        .Any(brick => brick.InRange(p)))
                        .ToList();

            if (player.Count > 0)
            {
                //playersCrash?.Invoke(this, player.ToList<Element>());
                RaiseEvent<object, List<Element>>("playersCrash", this, player.ToList<Element>());

            }
        }

        public void DetectPointGrabbed()
        {
            foreach (var player in SingleplayerEngine.Instance.GetElementsOf<AbstractPlayer>())
            {

                List<Apple> grabbedPoints =
                    SingleplayerEngine.Instance.GetElementsOf<Apple>()
                        .Where(point => point.InRange(player)).ToList();
                if (grabbedPoints.Count > 0)
                {
                    RaiseEvent<object, List<Element>>("GetPoint",player, grabbedPoints.ToList<Element>());
                }
            }
        }

        public void ConnectEvent<T,A>(string eventName,EventBox<T,A>.ChangingHandler eventDelegate)
        {
            if(ControledElements.ContainsKey(eventName))
                GetEventBoxOf<T,A>(eventName).AddHandler(eventDelegate);
            else
                CreateEvent<T,A>(eventName,eventDelegate);
        }

        public void CreateEvent<T, A>(string eventName, EventBox<T, A>.ChangingHandler eventDelegate)
        {
            ControledElements.Add(eventName, new EventBox<T, A>(eventDelegate));
        }

        public void RaiseEvent<T, A>(string eventName, T sender, A args)
        {
            GetEventBoxOf<T, A>(eventName).RaiseEvent(sender, args);
        }
    }
}
