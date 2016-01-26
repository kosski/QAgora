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
    /// Logika interakcji dla klasy UserInfo.xaml
    /// </summary>
    public partial class UserInfo : UserControl
    {
        public UserInfo()
        {
            InitializeComponent();
        }
        public UserInfo(string name,int lives)
        {
            InitializeComponent();
            PlayerName.Content = name;
            LiveNumber.Content = lives;
            Score.Content = 0;
            
        }
    }
}
