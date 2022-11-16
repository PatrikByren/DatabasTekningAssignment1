using ProductApp.Models;
using ProductApp.Services;
using ProductAppWpf.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ProductAppWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new OrderPage();
        }

        private void btn_productPage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ProductPage();
        }

        private void btn_customerPage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new CustomerPage();
        }

        private void btn_orderPage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new OrderPage();
        }
    }
}
