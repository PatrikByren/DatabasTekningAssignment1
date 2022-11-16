using ProductApp.Models;
using System;
using System.Collections.Generic;
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
        public ProductPage()
        {
            InitializeComponent();
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
        }
    }
}
