using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HozMarketWareHouse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ApiService apiService;
        public MainWindow()
        {
            InitializeComponent();
            apiService = new ApiService();
            
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var login = TLogin.Text.Trim();
            var password = Tpassword.Password.Trim();
            var user = await apiService.GetLogin(login, password);
            if (user == null)
            {
                MessageBox.Show("Неверный пароль или логин", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                var window = new ProductWindow(user.FirstName, user.MiddleName, user.LastName);
                window.Show();
                this.Close();
            }
        }
    }
}