using System.Windows;
using Gluteneria.Data;
using System.Windows.Documents;
using System.Data.Linq;
using System.Linq;
using Gluteneria.Models;

namespace Gluteneria
{
    /// <summary>
    /// Logika interakcji dla klasy addNewUser.xaml
    /// </summary>
    /// \
    public partial class AddNewUser : Window
    {

        public AddNewUser()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Pass.Password == Repeat.Password)
                using (GluteneriaEntities db = new GluteneriaEntities())
                {
                    Users user = new Users { Name = Login.Text, Pass = Pass.Password, Nick = Nick.Text };
                    db.Users.Add(user);
                    db.SaveChanges();
                    this.Close();
                    MessageBox.Show("Użytkownik Zarejestrowany!");
                }
        }
    }

}
