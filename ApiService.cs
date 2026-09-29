using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HozMarketWareHouse
{
    public class ApiService
    {
        public HttpClient httpClient;
        public ApiService()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:8080");
            httpClient.Timeout = TimeSpan.FromSeconds(30);

        }

        public async Task<UserDTO> GetLogin(string Login, string Password)
        {
            try
            {
                var response = await httpClient.GetAsync($"/User/api/GetLogin?Login={Login}&Password={Password}");

                if (response.IsSuccessStatusCode)
                {
                    var user = await response.Content.ReadFromJsonAsync<UserDTO>();
                    return user;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка входа: {error}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }



        }
        /*https://localhost:7006/Product/api/UpdateQuantity?operation=%2B&id=9 */
        public async Task UpdateQuantity(string operation, int id)
        {
            try
            {
                var encodedOperation = Uri.EscapeDataString(operation);
                var response = await httpClient.PutAsync(
                    $"/Product/api/UpdateQuantity?operation={encodedOperation}&id={id}", null
                );

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Успех ", "Уведомление",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка обработки: {error}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public async Task<List<ProductDTO>> GetProducts()
        {
            try
            {
                var response = await httpClient.GetAsync($"/Product/api/GetProducts");

                if (response.IsSuccessStatusCode)
                {
                    var products = await response.Content.ReadFromJsonAsync<List<ProductDTO>>();
                    return products;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка загрузки: {error}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }


        }

        public async Task<ProductDTO> GetProductsByID(int idProduct)
        {
            try
            {
                var response = await httpClient.GetAsync($"/Product/api/GetProductsByID?idProduct={idProduct}");

                if (response.IsSuccessStatusCode)
                {
                    var products = await response.Content.ReadFromJsonAsync<ProductDTO>();
                    return products;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка загрузки: {error}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }


        }

        public async Task<List<CategoryDTO>> GetCategories()
        {
            try
            {
                var response = await httpClient.GetAsync($"/Categories/api/GetCategories");

                if (response.IsSuccessStatusCode)
                {
                    var categories = await response.Content.ReadFromJsonAsync< List<CategoryDTO>>();
                    return categories;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка загрузки: {error}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }


        }

        public async Task<List<ProductDTO>> GetProductsFilter(string category)
        {
            try
            {
                var response = await httpClient.GetAsync($"/Product/api/GetProductsFilter?category={category}");

                if (response.IsSuccessStatusCode)
                {
                    var products = await response.Content.ReadFromJsonAsync<List<ProductDTO>>();
                    return products;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка загрузки: {error}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }


        }

        public async Task<List<ProductDTO>> GetProductsSortedAndFilter(int sorted, string category)
        {
            try
            {
                var response = await httpClient.GetAsync($"/Product/api/GetProductsSortedAndFilter?sorted={sorted}&category={category}");

                if (response.IsSuccessStatusCode)
                {
                    var products = await response.Content.ReadFromJsonAsync<List<ProductDTO>>();
                    return products;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка загрузки: {error}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }


        }
        public async Task<List<ProductDTO>> GetProductsSortedAndFilterSearch(int sorted, string category, string search)
        {
            try
            { 

                if (string.IsNullOrEmpty(search))
                {
                    return null;
                }
                if (string.IsNullOrEmpty(category))
                {
                    return null;
                }
                var response = await httpClient.GetAsync($"/Product/api/GetProductsSortedAndFilterSearch?sorted={sorted}&category={category}&search={search}");

                if (response.IsSuccessStatusCode)
                {
                    var products = await response.Content.ReadFromJsonAsync<List<ProductDTO>>();
                    return products;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка загрузки: {error}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }


        }
        public async Task<List<ProductDTO>> GetProductsSorted(int sorted)
        {
            try
            {
                var response = await httpClient.GetAsync($"/Product/api/GetProductsSorted?sorted={sorted}");

                if (response.IsSuccessStatusCode)
                {
                    var products = await response.Content.ReadFromJsonAsync<List<ProductDTO>>();
                    return products;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка загрузки: {error}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }


        }

        public async Task<List<ProductDTO>> GetProductsSearch(string search)
        {
            try
            {
                var response = await httpClient.GetAsync($"/Product/api/GetProductsSearch?search={search}");

                if (response.IsSuccessStatusCode)
                {
                    var products = await response.Content.ReadFromJsonAsync<List<ProductDTO>>();
                    return products;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка загрузки: {error}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }


        }
        public async Task UpdateProduct(int idProduct, ProductDTOEditor product)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync($"/Product/api/UpdateProduct/{idProduct}", product);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Успех ", "Уведомление",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка обработки: {error}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        public async Task CreateProduct(ProductDTOEditor product)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync($"/Product/api/AddProduct", product);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Успех ", "Уведомление",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка обработки: {error}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        public class ProductDTOEditor
        {
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


        }
        public class UserDTO
        {

            public int IdUser { get; set; }

            public string Email { get; set; } = null!;
            public string FirstName { get; set; } = null!;
            public string LastName { get; set; } = null!;
            public string? MiddleName { get; set; }

        }

        public class CategoryDTO
        {
            public string NameCategory { get; set; } = null!;
            public override string ToString() => NameCategory;

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

            public string? Photo { get; set; }

            public string? StatusAvailability { get; set; }

            public DateTime? CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }

            public int Quantity { get; set; }

            public string FullPhoto
            {
                get
                {
                    if (string.IsNullOrEmpty(Photo))
                    {
                        return "Images/noimage.jpg";
                    }
                    else
                    {
                        return $"Images/{Photo}";
                    }
                }
            }

            public string BackgroundColorQuantity
            {
                get
                {
                    if (Quantity == 0)
                    {
                        return "red";
                    }
                    else if (Quantity >= 1 && Quantity <= 5)
                    {
                        return "Orange";
                    }
                    else
                    {
                        return "White";
                    }
                }
            }

            public string StatusQuantity
            {
                get
                {
                    if (Quantity == 0)
                    {
                        return "Нет в наличии";
                    }
                    if (Quantity >= 1 && Quantity <= 5)
                    {
                        return "Мало";
                    }
                    else
                    {
                        return "Достаточно";
                    }
                }
            }

        }
    }
}
