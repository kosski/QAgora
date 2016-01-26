using Gluteneria.Game;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Gluteneria
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        string name;

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            name=User.GetLineText(0);

            Animation(Men);
        }

        private void Animation(Grid grd)
        {
            ThicknessAnimation ta = new ThicknessAnimation();
            ta.From = grd.Margin;
            ta.To = new Thickness(-500, 0, 0, 0);
            ta.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            grd.BeginAnimation(Grid.MarginProperty, ta);
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            Game.GameWindow game = new Game.GameWindow(new UserInfo(name,3));
            game.Show();
        }

        private void Highscores_Click(object sender, RoutedEventArgs e)
        {

        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void HowPlay_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
