using System.Windows;
using Gluteneria.Data;
using System.Windows.Documents;
using System.Data.Linq;
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
            string connection= "Server=USER\\SQLEXPRESS;Database=Portfolio;User Id=portfolio;Password=1qaz!QAZ;";
            portfolioDataContext data= new portfolioDataContext(connection);
            Table<Ussers> lista = data.GetTable<Ussers>();

            Ussers newUser = new Ussers { name = Name.Text, password = Password.Password };
            lista.InsertOnSubmit(newUser);
            data.SubmitChanges();
        }
    }

}
