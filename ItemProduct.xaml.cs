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

namespace HozMarketWareHouse
{
    /// <summary>
    /// Логика взаимодействия для ItemProduct.xaml
    /// </summary>
    public partial class ItemProduct : UserControl
    {
        public ApiService ApiService;
        
        public ItemProduct()
        {
            InitializeComponent();
            ApiService = new ApiService();
        }

        private async void Button_Click(object sender, RoutedEventArgs e) /* - */
        {
            var window = Application.Current.Windows.OfType<ProductWindow>().FirstOrDefault();
            int id = int.Parse(idProductText.Text);

            await ApiService.UpdateQuantity("-", id);
            window.Load();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var window = Application.Current.Windows.OfType<ProductWindow>().FirstOrDefault();
            int id = int.Parse(idProductText.Text);

            

            await ApiService.UpdateQuantity("+", id);
            window.Load();
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(idProductText.Text);
            var windowEditAdd = new EditAddProductWindow(id, true);
            
            windowEditAdd.ShowDialog();
            
        }
    }
}
