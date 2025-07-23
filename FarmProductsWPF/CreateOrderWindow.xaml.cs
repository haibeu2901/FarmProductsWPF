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
        private readonly IAccountRepo _accountRepo;
        private Account? _selectedCustomer;

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
            _accountRepo = new AccountRepo();
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
                CategoryName = s.Product?.Category?.CategoryName,
                PriceDisplay = s.Product?.SellingPrice,
                StockDisplay = s.Quantity > 25 ? "In Stock" : (s.Quantity > 0 ? "Low Stock" : "Out of Stock"),
            });
        }

        private void btnProductSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtProductSearch.Text.Trim();
            dtgProducts.ItemsSource = _stockRepo.SearchStock(searchText).Select(s => new
            {
                ProductName = s.Product.ProductName,
                CategoryName = s.Product?.Category?.CategoryName,
                PriceDisplay = s.Product?.SellingPrice,
                StockDisplay = s.Quantity > 25 ? "In Stock" : (s.Quantity > 0 ? "Low Stock" : "Out of Stock"),
            }).ToList();
        }

        private void btnCustomerSearch_Click(object sender, RoutedEventArgs e)
        {
            string customerSearchText = txtCustomerSearch.Text.Trim();
            
            _selectedCustomer = _accountRepo.GetCustomerByPhoneNumber(customerSearchText);
            
            if (_selectedCustomer != null)
            {
                // Show customer info in the panel
                txtCustomerName.Text = _selectedCustomer.FullName;
                txtCustomerPhone.Text = _selectedCustomer.PhoneNumber;
                customerInfoPanel.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Customer not found. Please check the phone number and try again.");
                customerInfoPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void btnClearCustomer_Click(object sender, RoutedEventArgs e)
        {
            // Clear the selected customer
            _selectedCustomer = null;
            
            // Hide the customer info panel
            customerInfoPanel.Visibility = Visibility.Collapsed;
            
            // Clear the search box
            txtCustomerSearch.Clear();
        }
    }
}
