using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using ProductApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ProductAppWpf.Pages
{
    /// <summary>
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {

        private ObservableCollection<ProductModel> _productEntity = new();
        public ProductPage()
        {
            InitializeComponent();
            PopulateProductCombobox().ConfigureAwait(false);
        }
        private async void btn_product_save_Click(object sender, RoutedEventArgs e)
        {
            using var client = new HttpClient();
            await client.PostAsJsonAsync("https://localhost:7040/api/products", new ProductRequest
            {
                Name = tb_productName.Text,
                Price = decimal.Parse(tb_productPrice.Text),
            });
            tb_productName.Text = string.Empty;
            tb_productPrice.Text = string.Empty;
            PopulateProductCombobox();
        }
        public async Task PopulateProductCombobox()
        {
            var collection = new ObservableCollection<KeyValuePair<Guid, string>>();
            using var client = new HttpClient();

            foreach (var item in await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:7040/api/products"))
            collection.Add(new KeyValuePair<Guid, string>(item.Id, item.Name)); 

            cb_changeProduct.ItemsSource = collection;

        }

        private async void btn_saveProductChange_Click(object sender, RoutedEventArgs e)
        {
            var product = (KeyValuePair<Guid, string>)cb_changeProduct.SelectedItem;
            var productId = product.Key;
            using var client = new HttpClient();
            var result = await client.PutAsJsonAsync("https://localhost:7040/api/products", new ProductModel
            {
                Id = productId,
                Name = tb_changeName.Text,
                Price = decimal.Parse(tb_changePrice.Text)
            });
            if (result is OkResult) { }
            tb_changeName.Text = string.Empty;
            tb_changePrice.Text = string.Empty;
            cb_changeProduct.SelectedIndex = -1;
            PopulateProductCombobox();
        }

        private async void cb_changeProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cb_changeProduct.SelectedIndex != -1)
                {
                    var product = (KeyValuePair<Guid, string>)cb_changeProduct.SelectedItem;
                    var productId = product.Key;
                    List<ProductModel> products = new();
                    using var client = new HttpClient();
                    products.Add(await client.GetFromJsonAsync<ProductModel>($"https://localhost:7040/api/products/{productId}"));
                    var customerName = (KeyValuePair<Guid, string>)cb_changeProduct.SelectedItem;
                    tb_changeName.Text = customerName.Value;
                    foreach (var item in products)
                    {
                        tb_changePrice.Text = Convert.ToString(item.Price);
                    }
                    
                }
            }
            catch
            {

            }
        }
    }
}
