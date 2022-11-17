using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductApp.Models;
using ProductApp.Models.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
using ProductModel = ProductApp.Models.ProductModel;

namespace ProductAppWpf.Pages
{
    /// <summary>
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        private ObservableCollection<ProductModel> _productModel = new();
        private ObservableCollection<ProductModel> _productEntity = new();

        public OrderPage()
        {
            InitializeComponent();
            PopulateProductCombobox().ConfigureAwait(false);
        }

        public async Task PopulateProductCombobox()
        {
            var collection = new ObservableCollection<KeyValuePair<Guid, string>>();
            using var client = new HttpClient();

            foreach (var item in await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:7040/api/products"))
                collection.Add(new KeyValuePair<Guid, string>(item.Id, item.Name));

            cb_product.ItemsSource = collection;
            await PopulateCustomerCombobox().ConfigureAwait(false);
        }
        public async Task PopulateCustomerCombobox()
        {
            var c_collection = new ObservableCollection<KeyValuePair<int, string>>();
            using var client = new HttpClient();

            foreach (var c in await client.GetFromJsonAsync<IEnumerable<CustomerModel>>("https://localhost:7040/api/Customer"))
                c_collection.Add(new KeyValuePair<int, string>(c.Id, c.Name));

            cb_customer.ItemsSource = c_collection;
             
        }

        private void btn_addProductToCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var product = (KeyValuePair<Guid, string>)cb_product.SelectedItem;
                var productId = product.Key;
                var productValue = product.Value;
                _productModel.Add(new ProductModel { Id = productId, Name = productValue });
                cb_product.SelectedItem = null!;
                lvProducts.ItemsSource = _productModel;

            }
            catch { MessageBox.Show("No products choosen in chart"); }

        }

        private async void btn_makeOrder_Click(object sender, RoutedEventArgs e)
        {
            {
                try
                {
                    ICollection<ProductModel> productEntities = lvProducts.ItemsSource as ICollection<ProductModel>;
                    decimal totalPrice = 0;
                    var customer = (KeyValuePair<int, string>)cb_customer.SelectedItem;
                    var customerId = customer.Key;
                    using var client = new HttpClient();

                    foreach (var item in _productModel)
                    {
                        _productEntity.Add(await client.GetFromJsonAsync<ProductModel>($"https://localhost:7040/api/products/{item.Id}"));
                    }
                    foreach (var item in _productEntity)
                    {
                        totalPrice += item.Price;
                    }


                    var order = new OrderRequest
                    {
                        CustomersId = customer.Key,
                        CustomerName = customer.Value,
                        Products = productEntities,
                        TotalPrice = totalPrice
                    };
                    var json = JsonConvert.SerializeObject(order);

                    var result = await client.PostAsJsonAsync("https://localhost:7040/api/Order", order);
                    if (result is OkResult)
                    {
                        cb_customer.SelectedItem = null!;
                        cb_product.SelectedItem = null!;
                        //MessageBox.Show("Saved");
                    }
                    else
                    {
                        cb_customer.SelectedItem = null!;
                        cb_product.SelectedItem = null!;
                        //MessageBox.Show("Not Saved");
                    }
                }
                catch (Exception ex) { Debug.WriteLine(ex.Message); MessageBox.Show("Error!"); }
            }
        }
    }
}
