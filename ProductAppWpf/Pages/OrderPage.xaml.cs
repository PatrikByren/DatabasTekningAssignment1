using Microsoft.AspNetCore.Mvc;
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
        private ObservableCollection<ProductEntity> _productEntity = new();
        public OrderPage()
        {
            InitializeComponent();
            PopulateProductCombobox().ConfigureAwait(false);
        }

        public async Task PopulateProductCombobox()
        {
            var collection = new ObservableCollection<KeyValuePair<int, string>>();
            using var client = new HttpClient();

            foreach (var item in await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:7040/api/products"))
                collection.Add(new KeyValuePair<int, string>(item.Id, item.Name));

            cb_product.ItemsSource = collection;
            await PopulateCustomerCombobox().ConfigureAwait(false);
        }
        public async Task PopulateCustomerCombobox()
        {
            var collection = new ObservableCollection<KeyValuePair<int, string>>();
            using var client = new HttpClient();

            foreach (var item in await client.GetFromJsonAsync<IEnumerable<CustomerModel>>("https://localhost:7040/api/customers"))
                collection.Add(new KeyValuePair<int, string>(item.Id, item.Name));

            cb_product.ItemsSource = collection;
             
        }

        private void btn_addProductToCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var product = (KeyValuePair<int, string>)cb_product.SelectedItem;
                var productId = product.Key;
                var productValue = product.Value;
                _productModel.Add(new ProductModel { Id = productId, Name = productValue });
                MessageBox.Show("Product added to chart");
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
                    decimal totalPrice = 0;
                    var customer = (KeyValuePair<int, string>)cb_customer.SelectedItem;
                    var customerId = customer.Key;
                    using var client = new HttpClient();

                    //foreach (var item in _productModel) 
                    ////{
                    ////  _productEntity.Add(await client.GetFromJsonAsync("https://localhost:7040/api/products", item.Id));
                    ////}
                    foreach (var item in _productEntity)
                    {
                        totalPrice += item.Price;
                    }
                    var result = await client.PostAsJsonAsync("https://localhost:7040/api/products", new OrderRequest
                    {
                        CustomersId = customer.Key,
                        CustomerName = customer.Value,
                        Products = _productEntity,
                        TotalPrice = totalPrice                        
                    });
                    if (result is OkResult)
                    {
                        cb_customer.SelectedItem = null!;
                        cb_product.SelectedItem = null!;
                    }
                }
                catch (Exception ex) { Debug.WriteLine(ex.Message); MessageBox.Show("Error!"); }
            }
        }
    }
}
