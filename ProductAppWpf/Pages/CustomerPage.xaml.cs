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
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        public CustomerPage()
        {
            InitializeComponent();
        }

        private async void btn_customerSave_Click(object sender, RoutedEventArgs e)
        {
            using var client = new HttpClient();

            await client.PostAsJsonAsync("https://localhost:7040/api/customers", new CustomerRequest
            {
                Name = tb_customerName.Text,
            });
            tb_customerName.Text = string.Empty;
        }
    }
}
