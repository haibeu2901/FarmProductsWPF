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
    /// Interaction logic for StockManagementWindow.xaml
    /// </summary>
    public partial class StockManagementWindow : Window
    {
        private Account _user;
        private readonly IStockRepo _stockRepo;

        public Account CurrentUser
        {
            get { return _user; }
            private set { _user = value; }
        }

        public StockManagementWindow(Account account)
        {
            InitializeComponent();
            _user = account;
            this.DataContext = this;
            _stockRepo = new StockRepo();
        }

        private void btnViewStockWindow_Click(object sender, RoutedEventArgs e)
        {
            FarmProductManagementWindow viewStockWindow = new FarmProductManagementWindow(_user);
            viewStockWindow.Show();
            this.Close();
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

        public void LoadDataGrid(string searchText)
        {
            dtgStock.ItemsSource = _stockRepo.SearchStock(searchText).Select(s => new
            {
                s.ProductId,
                s.Product.ProductName,
                s.Product?.Category?.CategoryName,
                s.Product?.Unit,
                s.Quantity,
                s.LastUpdated,
                s.Notes,
                Status = s.Quantity > 25 ? "In Stock" : (s.Quantity > 0 ? "Low Stock" : "Out of Stock"),
            }).ToList();
        }

        private void dtgStock_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataGrid(string.Empty);
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            LoadDataGrid(searchText);
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            this.Close();
            login.Show();
        }

        private void UpdateStock_Click(object sender, RoutedEventArgs e)
        {
            if (dtgStock.SelectedItem != null)
            {
                dynamic selectedStock = dtgStock.SelectedItem;
                if (selectedStock != null)
                {
                    Stock stockProduct = _stockRepo.GetStockByProductId(selectedStock.ProductId);
                    if (stockProduct != null)
                    {
                        UpdateStockPopup updateStockPopup = new UpdateStockPopup(_user, stockProduct);
                        updateStockPopup.StockUpdated += (s, args) =>
                        {
                            LoadDataGrid(stockProduct.Product.ProductName);
                        };
                        updateStockPopup.Show();
                    }
                    else
                    {
                        MessageBox.Show("Selected stock product not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void btnCategoriesWindow_Click(object sender, RoutedEventArgs e)
        {
            CategoryManagementWindow categoryManagementWindow = new CategoryManagementWindow(_user);
            categoryManagementWindow.Show();
            this.Close();
        }

        private void btnProductsWindow_Click(object sender, RoutedEventArgs e)
        {
            ProductManagementWindow productManagementWindow = new ProductManagementWindow(_user);
            productManagementWindow.Show();
            this.Close();
        }
    }
}
