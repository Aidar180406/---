using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HozMarketWareHouse
{
    /// <summary>
    /// Логика взаимодействия для ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private ApiService apiService;
        public ProductWindow(string Firstname, string Middlename, string Lastname)
        {
            InitializeComponent();
            apiService = new ApiService();
            Title = $"В работе: {Firstname} {Middlename} {Lastname}";
            Load();
        }

        public async void Load()
        {

            var categories = await apiService.GetCategories();
            foreach (var category in categories)
            {
                CB_Categories.Items.Add(category);
            }
            int selected = CB_Sort.SelectedIndex;
           

            var categoryText = CB_Categories.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(categoryText))
            {
                var products = await apiService.GetProducts();
                icProducts.ItemsSource = products;
            }
            else if (selected == 0)
            {
                var products = await apiService.GetProductsFilter(categoryText);
                icProducts.ItemsSource = products;
            }
            else if (selected != 0 && !string.IsNullOrEmpty(categoryText))
            {
                var products = await apiService.GetProductsSortedAndFilter(selected, categoryText);
                icProducts.ItemsSource = products;
            }
            else if (string.IsNullOrEmpty(categoryText) && selected == 0 && string.IsNullOrEmpty(TB_Search.Text))
            {
                var products = await apiService.GetProducts();
                icProducts.ItemsSource = products;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Load();
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Load();

        }

        private async void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int selected = CB_Sort.SelectedIndex;
            var categoryText = CB_Categories.SelectedItem?.ToString();
            var search_text = TB_Search.Text.Trim().ToLower();

            if (selected == 0)
            {
                var products_without_selected = await apiService.GetProductsSortedAndFilterSearch(-1, categoryText, search_text);
                icProducts.ItemsSource = products_without_selected;
                return;
            }
            else if (selected == 0 && string.IsNullOrEmpty(categoryText))
            {
                var products_without_selected = await apiService.GetProductsSortedAndFilterSearch(-1, null, search_text);
                icProducts.ItemsSource = products_without_selected;
                return;
            }
            var products = await apiService.GetProductsSortedAndFilterSearch(selected, categoryText, search_text);
            icProducts.ItemsSource = products;
            

        }

        public class ProductDTO
        {
            public int IdProduct { get; set; }

            public int Article { get; set; }

            public string Name { get; set; } = null!;

            public decimal Price { get; set; }

            public string? Description { get; set; }

            public string Brand { get; set; } = null!;

            public int CategoryId { get; set; }

            public string Unit { get; set; } = null!;

            public float? Weight { get; set; }
            public float? Volume { get; set; }

            public string Photo { get; set; } = null!;


            public DateTime? CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }

            public int Quantity { get; set; }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var windowEditAdd = new EditAddProductWindow(-1, false);
            windowEditAdd.ShowDialog();
        }
    }
}
