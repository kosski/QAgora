using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gluteneria.Game
{
    /// <summary>
    /// Logika interakcji dla klasy EndScreen.xaml
    /// </summary>
    public partial class EndScreen : UserControl
    {
        public EndScreen(string player1, string score1, string player2, string score2, bool winner)
        {
            InitializeComponent();
            this.Player1Name.Content = player1;
            this.Player1Score.Content = score1;

            this.Player2Name.Content = player2;
            this.Player2Score.Content = score2;

            switch(winner)
            {
                case true: WinnerInfo.Content = player1; break;
                case false: WinnerInfo.Content = player2; break;
                default: WinnerInfo.Content = "Draw!"; break;
            }
        }
    }
}
