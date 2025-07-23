using FarmProductsWPF_BOs;
using FarmProductsWPF_Repositories.Implements;
using FarmProductsWPF_Repositories.Interfaces;
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
using System.Windows.Shapes;

namespace FarmProductsWPF
{
    /// <summary>
    /// Interaction logic for CreateOrderWindow.xaml
    /// </summary>
    public partial class CreateOrderWindow : Window
    {
        private Account _user;
        private readonly IStockRepo _stockRepo;

        public Account CurrentUser
        {
            get { return _user; }
            private set { _user = value; }
        }

        public CreateOrderWindow(Account account)
        {
            InitializeComponent();
            _user = account;
            this.DataContext = this;
            _stockRepo = new StockRepo();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }

        private void btnViewStock_Click(object sender, RoutedEventArgs e)
        {
            FarmProductManagementWindow farmProductManagementWindow = new FarmProductManagementWindow(_user);
            farmProductManagementWindow.Show();
            this.Close();
        }

        private void dtgProducts_Loaded(object sender, RoutedEventArgs e)
        {
            dtgProducts.ItemsSource = _stockRepo.GetAllStocks().Select(s => new
            {
                ProductName = s.Product.ProductName,
                CategoryName = s.Product.Category.CategoryName,
                PriceDisplay = s.Product.SellingPrice,
                StockDisplay = s.Quantity > 25 ? "In Stock" : (s.Quantity > 0 ? "Low Stock" : "Out of Stock"),
            });
        }

        private void btnProductSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtProductSearch.Text.Trim();
            dtgProducts.ItemsSource = _stockRepo.SearchStock(searchText).Select(s => new
            {
                ProductName = s.Product.ProductName,
                CategoryName = s.Product.Category.CategoryName,
                PriceDisplay = s.Product.SellingPrice,
                StockDisplay = s.Quantity > 25 ? "In Stock" : (s.Quantity > 0 ? "Low Stock" : "Out of Stock"),
            }).ToList();
        }
    }
}
