using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
//using System.Windows.Shapes;
using GameEngine.Game;
using GameEngine.Game.Interfaces;

namespace GameEngine.elements
{
    public sealed class PlayerController : IController<AbstractPlayer>
    {
        private SingleplayerEngine _singleplayerEngine;
        public List<AbstractPlayer> ControledElements { get; set; }


        public PlayerController(ISettnigs conf)
        {
            _singleplayerEngine = SingleplayerEngine.Instance;
            ControllerStart(conf);
        }
        public void ControllerStart(ISettnigs conf)
        {
            ControledElements= new List<AbstractPlayer>();
            for (int player=0; player<conf.HumanPlayers;player++)
                ControledElements.Add(new Person(conf.PlayersControlKeys[player],conf.Lives));

            for (int player = 0; player < conf.ComputerPlayers; player++)
                ControledElements.Add(new Bot());
        }

        public void Tick(object sender, EventArgs eventArgs)
        {
            Move();
        }

        public void ConnectEvents(EventsController eventsController)
        {
            SingleplayerEngine.Instance.EventsController.ConnectEvent<object, List<Element>>("GetPoint", EventsControllerOnGetPoint);
            SingleplayerEngine.Instance.EventsController.ConnectEvent<object, List<Element>>("playersMeet", EventsControllerOnPlayersMeet);
            SingleplayerEngine.Instance.EventsController.ConnectEvent<object, List<Element>>("playersCrash", EventsControllerOnPlayersCrash);

            //eventsController.GetPoint+= EventsControllerOnGetPoint;
            //eventsController.playersMeet+= EventsControllerOnPlayersMeet ;
            //eventsController.playersCrash+= EventsControllerOnPlayersCrash;
        }

        #region Events
        private void EventsControllerOnPlayersMeet(object sender, List<Element> ca)
        {
            PlayersMeet((AbstractPlayer)ca[0], (AbstractPlayer)ca[1]);
        }


        private void EventsControllerOnPlayersCrash(object sender, List<Element> ca)
        {
            foreach (IPlayer player in ca)
            {
                player.Dead();
            }
        }

        private void EventsControllerOnGetPoint(object sender, List<Element> ca)
        {
            AbstractPlayer player = (AbstractPlayer)sender;
            foreach (Apple apple in ca)
            {
                player.GiveBonus(apple.GetBonuses());
            }
        }

        #endregion
        public void InitPlayer<T>() where T : AbstractPlayer, new()
        {
            ControledElements.RemoveAll(p => p.GetType().IsEquivalentTo(AbstractPlayer.GiveType<T>()));
            ControledElements.Add(new T());
        }

        public void Move()
        {
            PlayerMove();
        }

        private void PlayerMove()
        {
            ControledElements.ForEach( player =>
            {
                    player.MoveUpdate();
                    if (player.GetSize() <= 1) player.Dead();
                    //if (walls.inRange(playerA) && playerA.Lives < 1)
                    //    playerA = null;
                    //playerA.GiveBonus(points.pointInRange(playerA));

                    //TODO do przeniesienia
                    player.Redraw();
            });
        }

        private void PlayersMeet(AbstractPlayer playerA, AbstractPlayer playerB)
        {
            if (!playerA.InRange(playerB)) return;
            if ((playerB.Size * 2 <= playerA.Size)) { playerB.Dead(); }
            else if (playerA.Size * 2 <= playerB.Size) playerA.Dead();
            else PingPong(playerA,playerB);
        }

        private void PingPong(AbstractPlayer playerA, AbstractPlayer playerB)
        {
            if (Math.Abs(playerA.X - playerB.X) < Math.Abs(playerA.Y - playerB.Y))
                if (playerA.X > playerB.X)
                { playerA.Dir.SetDir( playerA.Dir.controlKeys[3]); playerB.Dir.SetDir( playerB.Dir.controlKeys[1]); }
                else { playerA.Dir.SetDir( playerA.Dir.controlKeys[1]); playerB.Dir.SetDir(playerB.Dir.controlKeys[3]); }
            else
                if (playerA.Y > playerB.Y)
            { playerA.Dir.SetDir(playerA.Dir.controlKeys[2]); playerB.Dir.SetDir(playerB.Dir.controlKeys[0]); }
            else { playerA.Dir.SetDir(playerA.Dir.controlKeys[0]); playerB.Dir.SetDir(playerB.Dir.controlKeys[2]); }
        }


    }
}