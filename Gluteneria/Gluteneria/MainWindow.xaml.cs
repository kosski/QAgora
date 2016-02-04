using Gluteneria.Data;
using Gluteneria.Game;
using System;
using System.Data.Linq;
using System.Linq;
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

        string _name;

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {

            _name = User.GetLineText(0);
            if (Connect())
                Animation(Men);
            else
                MessageBox.Show("Login Denied");

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
            Game.GameWindow game = new Game.GameWindow(new UserInfo(_name, 3));
            game.Show();
        }


        private bool Connect()
        {
            using (GluteneriaEntities db = new GluteneriaEntities())
            {
                Users user = db.Users.FirstOrDefault(u => u.Nick == User.Text);
                if (user == null)
                {
                    MessageBox.Show("User does not exist");
                    return false;
                }
                return user.Pass == Pass.Password;
            }


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

        private void reg_Click(object sender, RoutedEventArgs e)
        {
            new AddNewUser().Show();
        }
    }
}
