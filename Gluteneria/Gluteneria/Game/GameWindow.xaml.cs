using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
//using Gluteneria.Game.Interfaces;
//using Gluteneria.MVC.elements;
using System.Reflection;
using System.Windows.Documents;
using Gluteneria.Game.Interfaces;
using Gluteneria.MVC.elements;

namespace Gluteneria.Game
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public static DispatcherTimer Timer;
        public static int Count = 0;

        public delegate void ChangingHandler(object sender, Key key);
        public event ChangingHandler KeyEvent;
        public static event ChangingHandler RedrawAll;
        public GameWindow(UserInfo playerInfo)
        {
            InitializeComponent();
            this.InitGame();
            this.EventStart();
            InitTimer();
        }

        void EventStart()
        {
            //((Walls)SingleplayerEngine.Instance.ControllerContainer["Walls"]).Change += new Walls.ChangingHandler(wall_colide);
            //_engine.points.Change += new PointControler.ChangingHandler(point_grabbed);

            SingleplayerEngine.Instance.EventsController.ConnectEvent<object,List<Element>> ("GetPoint",point_grabbed); 
            SingleplayerEngine.Instance.EventsController.ConnectEvent<object,List<Element>> ("NewPoints",new_points);

            foreach (AbstractPlayer abstractPlayer in SingleplayerEngine.Instance.GetElementsOf<AbstractPlayer>())
            {
                abstractPlayer.deadEvent += Player_Dead;
                KeyEvent += new ChangingHandler(abstractPlayer.KeyPressed);
            }
            SingleplayerEngine.Instance.EventsController.ConnectEvent<object, EventArgs>("GameOver", end_Game);
        }

        private void new_points(object sender, List<Element> Args)
        {
            AddRectsOf<Apple>();
        }

        private void end_Game(object sender, EventArgs ca)
        {
            Timer.Stop();
            // MainGrid.Children.Add(new EndScreen(_player.PlayerName.Content.ToString(), _player.Score.Content.ToString(), 
           //                                      _enemy.PlayerName.Content.ToString(), _enemy.Score.Content.ToString(), _engine.player.score>_engine.enemy.score));
        }



        //private void wall_colide(object sender, Events.WallColideArgs ca)
        //{
        //    GameGrid.Children.Add(Images.PREPARE_GRAVE(ca.GuyRect.Margin.Left, ca.GuyRect.Margin.Top));
        //    GameGrid.Children.Remove(ca.GuyRect); 
        //    InitPlayers(ca.Id==2,ca.Id==3);
        //}



        private void point_grabbed(object sender, List<Element> elements)
        {
                foreach (IElement element in elements)
                {
                    GameGrid.Children.Remove(element.Rect);
                }
        }

        public void InitGame()
        {
            SingleplayerEngine.Instance.Start(new GameSettings
            {
                ComputerPlayers = 1,
                HumanPlayers = 1,
                Lives = 3,
                HowManyApples = 7,
                WallsMap = new List<List<int>>
                {
                    new List<int> {0, 77, 1, 78},
                    new List<int> {0, 0, 2, 58},
                    new List<int> {56, 0, 3, 78},
                    new List<int> {56, 77, 4, 58}
                },
                ArenaHeight = GameGrid.Height,
                ArenaWidth = GameGrid.Width,

                PlayersControlKeys = new List<List<Key>>
                {
                    new List<Key> {Key.Up,Key.Left,Key.Down,Key.Right }
                }
            }
            );
            //InitWalls();
            //InitPlayers();
            //InitPoints();
            //GameWindow.RedrawAll(this, Key.A);
            InitElements();
        }

        void InitTimer()
        {
            Timer = new DispatcherTimer();
            //Timer.Tick += new EventHandler(TimerTick);

            Timer.Tick += new EventHandler(SingleplayerEngine.Instance.GetControllerOf<AbstractPlayer>().Tick);
            Timer.Tick += new EventHandler(SingleplayerEngine.Instance.GetControllerOf<Brick>().Tick);
            Timer.Tick += new EventHandler(SingleplayerEngine.Instance.EventsController.Tick);
            Timer.Tick += new EventHandler(SingleplayerEngine.Instance.GetControllerOf<Apple>().Tick);



            Timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            Timer.Start();
        }

        void TimerTick(object sender, EventArgs e)
        {
            //InfoUpdate();
        }
        private void WindowKeyDown(object sender, KeyEventArgs key)
        {
            if (key.Key == Key.Escape)
            {
                Timer.Stop();
                this.Close();
            }
            KeyEvent(this, key.Key);
        }

        //public void InitPlayers()
        //{
        //    foreach (AbstractPlayer player in SingleplayerEngine.Instance.GetElementsOf<AbstractPlayer>())
        //    {
        //        GameGrid.Children.Add(player.Rect);
        //        player.Redraw();
        //    }

        //    //    if(player){ _engine.PlayerController.InitHumanPlayer(); GameGrid.Children.Add(_engine.PlayerController.GuyRect()); }
        //    //    if(bot)   {_engine.PlayerController.InitComputerPlayer();   GameGrid.Children.Add(_engine.EnemyRect()); }
        //}


        //public void InitWalls()
        //{
        //    foreach (Brick wall in SingleplayerEngine.Instance.GetElementsOf<Brick>())
        //    {
        //        GameGrid.Children.Add(wall.Rect);
        //        wall.Redraw();

        //    }

        //}

        //private void InitPoints()
        //{
        //    foreach (Apple rect in SingleplayerEngine.Instance.GetElementsOf<Apple>())
        //    {
        //        GameGrid.Children.Add(rect.Rect);
        //    }

        //}
        private void AddRectsOf<T>() where T : IElement
        {
            foreach (T item in SingleplayerEngine.Instance.GetElementsOf<T>())
            {
                GameGrid.Children.Add(item.Rect);
                item.Redraw();
            }

        }
        private void Player_Dead(object sender,ref Rectangle PlayerRect)
        {
            MainGrid.Children.Remove(PlayerRect);
            MainGrid.Children.Add(((AbstractPlayer)sender).Rect);
        }

        private void start_game(object o)
        {

            DateTime gameStart = DateTime.Now.AddSeconds(10);
            while (gameStart >= DateTime.Now) { Debug.WriteLine(DateTime.Now.Second + " : " + gameStart.Second); }
            MainGrid.Children.Remove((UIElement)o);
            InitTimer();
        }
        private void InitElements()
        {
            AddRectsOf<Brick>();
            AddRectsOf<Apple>();
            AddRectsOf<AbstractPlayer>();
        }

        //public void InfoUpdate()
        //{
        //    _player.LiveNumber.Content = _engine.player.lives;
        //    _enemy.LiveNumber.Content = _engine.enemy.lives;

        //    _player.Score.Content = _engine.player.score;
        //    _enemy.Score.Content = _engine.enemy.score;

        //}

        public static bool InArena(double x, double y)
        {
            return (x > 0 && y > 0 && x < 50 && y < 65);
        }

    }
}
