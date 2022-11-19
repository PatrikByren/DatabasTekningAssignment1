using Microsoft.AspNetCore.Mvc;
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
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        public CustomerPage()
        {
            InitializeComponent();
            PopulateCustomerCombobox().ConfigureAwait(false);
        }

        private async void btn_customerSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using var client = new HttpClient();

                var result = await client.PostAsJsonAsync("https://localhost:7040/api/Customer", new CustomerRequest
                {
                    Name = tb_customerName.Text,
                });
                if (result is OkResult)
                {
                    tb_customerName.Text = "";
                }
                else
                {
                    MessageBox.Show("Not saved");
                    tb_customerName.Text = "";
                }
            }
            catch { MessageBox.Show("Error! Try again!"); }
        }
        public async Task PopulateCustomerCombobox()
        {
            var collection = new ObservableCollection<KeyValuePair<int, string>>();
            using var client = new HttpClient();

            foreach (var item in await client.GetFromJsonAsync<IEnumerable<CustomerModel>>("https://localhost:7040/api/customer"))
                collection.Add(new KeyValuePair<int, string>(item.Id, item.Name));
            cb_changeCustomer.ItemsSource = collection;
        }

        private async void btn_saveCustomerChange_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            var customer = (KeyValuePair<int, string>)cb_changeCustomer.SelectedItem;
            var customerId = customer.Key;
            using var client = new HttpClient();
            var result = await client.PutAsJsonAsync("https://localhost:7040/api/customer", new CustomerModel
            {
                Id = customerId,
                Name = tb_changeName.Text,

            });
            if (result is OkResult) { }
            }
            catch { MessageBox.Show("Error! Try again!"); }
            tb_changeName.Text = string.Empty;
            cb_changeCustomer.SelectedIndex = default;
            PopulateCustomerCombobox();
        }

        private void cb_changeCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cb_changeCustomer.SelectedIndex != -1)
                {
                    var customerName = (KeyValuePair<int, string>)cb_changeCustomer.SelectedItem;
                    tb_changeName.Text = customerName.Value;
                }
            }
            catch
            {
                
            }
        }
    }
}
