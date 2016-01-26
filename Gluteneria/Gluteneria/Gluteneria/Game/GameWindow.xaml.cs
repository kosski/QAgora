using Gluteneria.elements;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Gluteneria.Game
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public static DispatcherTimer timer;
        public static int count = 0;
        GEngine engine;
        UserInfo player,enemy=new UserInfo("Janusz",3);

        public GameWindow(UserInfo playerInfo)
        {
            InitializeComponent();
            engine = new GEngine(GameGrid.Width, GameGrid.Height);
            this.eventStart();
            this.initGame();
            this.player = playerInfo;
            infoUpdate();
            InfoGrid.Children.Add(this.player);
            InfoGrid.Children.Add(this.enemy);
            InitTimer();
        }

        void eventStart()
        {
            engine.walls.Change += new Walls.ChangingHandler(wall_colide);
            engine.points.Change += new PointControler.ChangingHandler(point_grabbed);
            engine.exitEvent += new GEngine.ChangingHandler(end_Game);
        }



        private void start_game(object o)
        {
            
            DateTime gameStart = DateTime.Now.AddSeconds(10);
            while (gameStart >= DateTime.Now) { Debug.WriteLine(DateTime.Now.Second + " : " + gameStart.Second); }
            MainGrid.Children.Remove((UIElement)o);
            InitTimer();
        }

        private void end_Game(object sender, EventArgs ca)
        {
            timer.Stop();
            MainGrid.Children.Add(new EndScreen(player.PlayerName.Content.ToString(), player.Score.Content.ToString(), 
                                                enemy.PlayerName.Content.ToString(), enemy.Score.Content.ToString(), engine.player.score>engine.enemy.score));
        }

        

        private void wall_colide(object sender, Events.WallColideArgs ca)
        {
            GameGrid.Children.Add(IMAGES.PREPARE_GRAVE(ca.guyRect.Margin.Left, ca.guyRect.Margin.Top));
            GameGrid.Children.Remove(ca.guyRect); 
            InitPlayers(ca.id==2,ca.id==3);
        }

        

        private void point_grabbed(object sender, Events.PointColideArgs ca)
        {
            GameGrid.Children.RemoveAt(GameGrid.Children.IndexOf(ca.PointRect));
            if (engine.getCountPoints() < 1)
                initPoints();      
        }

        void initGame()
        {
            initWalls();
            InitPlayers(true,true);
            initPoints();
        }

        void InitTimer()
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timer.Start();
        }

        void timerTick(object sender, EventArgs e)
        {
            infoUpdate();
            engine.move();
        }
        private void WindowKeyDown(object sender, KeyEventArgs key)
        {
            if (key.Key == Key.Escape)
            {
                timer.Stop();             
                this.Close();
            }
                
            engine.player.setSide(key.Key);
        }

        public void InitPlayers(bool player,bool bot)
        {
            if(player){ engine.InitPlayer(); GameGrid.Children.Add(engine.guyRect()); }
            if(bot)   {engine.InitEnemy();   GameGrid.Children.Add(engine.enemyRect()); }
        }

        
        public void initWalls()
        {
                foreach (Rectangle wall in engine.getWallRects())
                    GameGrid.Children.Add(wall);
                engine.walls.redraw();
        }

        private void initPoints()
        {
            engine.points.newPoints();
            foreach (Rectangle rect in engine.getPointsRect())
                GameGrid.Children.Add(rect);
            engine.points.redraw();
        }

        public void infoUpdate()
        {
            player.LiveNumber.Content = engine.player.lives;
            enemy.LiveNumber.Content = engine.enemy.lives;

            player.Score.Content = engine.player.score;
            enemy.Score.Content = engine.enemy.score;

        }

    }
}
