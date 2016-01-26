using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace Gluteneria
{
    /// <summary>
    /// Logika interakcji dla klasy addNewUser.xaml
    /// </summary>
    /// 
    public class users
    {
        public int id;
        public string name, pass;
        public users(int id,string name)
        {
            this.id = id;
            this.name = name;
            this.pass = "spierdalaj";
        }
        public users(int id, string name,string pass)
        {
            this.id = id;
            this.name = name;
            this.pass = pass;
        }
    }
    public partial class addNewUser : Window
    {

        public addNewUser()
        {
            InitializeComponent();  
           
        }
        
        private void table()
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
