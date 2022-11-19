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
        decimal totalPrice;

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

        private async void btn_addProductToCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var product = (KeyValuePair<Guid, string>)cb_product.SelectedItem;
                var productId = product.Key;
                var productValue = product.Value;
                using var client = new HttpClient();
                _productModel.Add(await client.GetFromJsonAsync<ProductModel>($"https://localhost:7040/api/products/{productId}"));
                totalPrice = 0;
                foreach (var item in _productModel)
                {
                    totalPrice += item.Price;
                }
                lvProducts.ItemsSource = _productModel;
                tbl_totalPrice.Text = totalPrice.ToString();
                cb_product.SelectedIndex = -1;


            }
            catch { MessageBox.Show("No products choosen in chart"); }
        }

        private async void btn_makeOrder_Click(object sender, RoutedEventArgs e)
        {
            {
                try
                {
                    ICollection<ProductModel> productEntities = lvProducts.ItemsSource as ICollection<ProductModel>;
                    var customer = (KeyValuePair<int, string>)cb_customer.SelectedItem;
                    var customerId = customer.Key;
                    using var client = new HttpClient();

                    var order = new OrderRequest
                    {
                        CustomersId = customer.Key,
                        CustomerName = customer.Value,
                        Products = productEntities,
                        TotalPrice = decimal.Parse(tbl_totalPrice.Text)
                    };
                    var result = await client.PostAsJsonAsync("https://localhost:7040/api/Order", order);
                    _productModel = new();
                    lvProducts.ItemsSource = _productModel;
                    cb_customer.SelectedIndex = -1;
                    cb_product.SelectedIndex = -1;
                    tbl_totalPrice.Text = String.Empty;
                    if (result is BadRequestResult)
                    {
                        MessageBox.Show("Error!");
                    }
                }
                catch (Exception ex) { Debug.WriteLine(ex.Message); MessageBox.Show("Error!"); }
            }
        }

    }
}
