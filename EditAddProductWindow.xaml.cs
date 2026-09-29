using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HozMarketWareHouse
{
    /// <summary>
    /// Логика взаимодействия для EditAddProductWindow.xaml
    /// </summary>
    public partial class EditAddProductWindow : Window
    {
        public ApiService ApiService;
        public int _ID;
        public bool _IsEdit;
        public EditAddProductWindow(int idProduct, bool isEdit)
        {
            InitializeComponent();
            
            ApiService = new ApiService();
            _ID = idProduct;
            LoadCategories();
            if (isEdit)
            {
                _IsEdit = true;
                Title = "Редактирование продукта";
                Load();
            }
            else
            {
                Title = "Создание нового продукта";
            }
            
        }
        public async void LoadCategories()
        {
            var categories = await ApiService.GetCategories();
            foreach (var category in categories)
            {

                CB_Category.Items.Add(category);
            }
        }

        public async void Load()
        {
            
            var product = await ApiService.GetProductsByID(_ID);
            TB_Name.Text = product.Name;    
            TB_Description.Text = product.Description;
            TB_Price.Text = product.Price.ToString();
            TB_Article.Text = product.Article.ToString();
            TB_Image.Text = product.Photo;
            CB_Category.SelectedIndex = product.CategoryId;
            TB_Brand.Text = product.Brand;
            TB_Unit.Text = product.Unit;
            TB_Weight.Text = product.Weight.ToString();
            TB_Volume.Text = product.Volume.ToString();
        }

        private void Btn_load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images | *.jpg; *.png;";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == true)
            {
                TB_Image.Text = openFileDialog.FileName;
            }

        }


        private void RB_Checked_1(object sender, RoutedEventArgs e)
        {
            Panel_volume.Visibility = Visibility.Visible;
        }

        private void RB_Unchecked(object sender, RoutedEventArgs e)
        {
            Panel_volume.Visibility = Visibility.Collapsed;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_Name.Text))
            {
                MessageBox.Show("Уведомление", "Введите все поля", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (string.IsNullOrEmpty(TB_Unit.Text))
            {
                MessageBox.Show("Уведомление", "Введите все поля", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if(string.IsNullOrEmpty(TB_Weight.Text))
            {
                MessageBox.Show("Уведомление", "Введите все поля", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if(string.IsNullOrEmpty(TB_Price.Text))
            {
                MessageBox.Show("Уведомление", "Введите все поля", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if(string.IsNullOrEmpty(TB_Article.Text))
            {
                MessageBox.Show("Уведомление", "Введите все поля", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if(string.IsNullOrEmpty(TB_Brand.Text))
            {
                MessageBox.Show("Уведомление", "Введите все поля", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (CB_Category.SelectedIndex == 0)
            {
                MessageBox.Show("Уведомление", "Введите все поля", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (_IsEdit)
            {
                float? volume = 0.0f;
                if (string.IsNullOrWhiteSpace(TB_Volume.Text))
                {
                    volume = null;
                }
                var updatedProduct = new ApiService.ProductDTOEditor
                {
                    Article = int.Parse(TB_Article.Text),
                    Name = TB_Name.Text,
                    Price = decimal.Parse(TB_Price.Text),
                    Description = TB_Description.Text,
                    Brand = TB_Brand.Text,
                    CategoryId = CB_Category.SelectedIndex,
                    Unit = TB_Unit.Text,
                    Weight = float.Parse(TB_Weight.Text),
                    Volume = volume,
                    Photo = TB_Image.Text
                };
                await ApiService.UpdateProduct(_ID, updatedProduct);
                var window = Application.Current.Windows.OfType<ProductWindow>().FirstOrDefault();
                window.Load();
                this.Close();
            }
            else
            {
                float? volume = 0.0f;
                if (string.IsNullOrWhiteSpace(TB_Volume.Text))
                {
                    volume = null;
                }
                var updatedProduct = new ApiService.ProductDTOEditor
                {
                    Article = int.Parse(TB_Article.Text),
                    Name = TB_Name.Text,
                    Price = decimal.Parse(TB_Price.Text),
                    Description = TB_Description.Text,
                    Brand = TB_Brand.Text,
                    CategoryId = CB_Category.SelectedIndex,
                    Unit = TB_Unit.Text,
                    Weight = float.Parse(TB_Weight.Text),
                    Volume = volume,
                    Photo = TB_Image.Text
                };
                await ApiService.CreateProduct(updatedProduct);
                var window = Application.Current.Windows.OfType<ProductWindow>().FirstOrDefault();
                window.Load();
                this.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TB_Article_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TB_Volume_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[0-9]+([,][0-9]*)?$");

            TextBox tb = sender as TextBox;
            string newText = tb.Text.Insert(tb.CaretIndex, e.Text);

            e.Handled = !regex.IsMatch(newText);
        }
    }
}
