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
    /// Interaction logic for FarmProductManagementWindow.xaml
    /// </summary>
    public partial class FarmProductManagementWindow : Window
    {
        private Account _user;
        private readonly IStockRepo _stockRepo;

        public Account CurrentUser
        {
            get { return _user; }
            private set { _user = value; }
        }

        public FarmProductManagementWindow(Account account)
        {
            InitializeComponent();
            _user = account;
            this.DataContext = this;
            _stockRepo = new StockRepo();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            this.Close();
            login.Show();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            LoadDataGrid(searchText);
        }

        private void LoadDataGrid(string searchText)
        {
            dtgStock.ItemsSource = _stockRepo.SearchStock(searchText).Select(s => new
            {
                s.ProductId,
                s.Product.ProductName,
                s.Product?.Category?.CategoryName,
                s.Product?.Unit,
                s.Product?.SellingPrice,
                s.Quantity,
                Status = s.Quantity > 25 ? "In Stock" : (s.Quantity > 0 ? "Low Stock" : "Out of Stock"),
            }).ToList();
        }

        private void dtgStock_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataGrid("");
        }

        private void btnCreateOrderWindow_Click(object sender, RoutedEventArgs e)
        {
            CreateOrderWindow createOrderWindow = new CreateOrderWindow(_user);
            createOrderWindow.Show();
            this.Close();
        }

        private void btnOrderHistoryWindow_Click(object sender, RoutedEventArgs e)
        {
            OrderHistoryWindow orderHistoryWindow = new OrderHistoryWindow(_user);
            orderHistoryWindow.Show();
            this.Close();
        }
    }
}
