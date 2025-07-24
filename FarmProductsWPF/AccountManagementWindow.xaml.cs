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
    /// Interaction logic for AccountManagementWindow.xaml
    /// </summary>
    public partial class AccountManagementWindow : Window
    {
        private Account _user;
        private readonly IAccountRepo _accountRepo;

        public Account CurrentUser
        {
            get { return _user; }
            private set { _user = value; }
        }

        public AccountManagementWindow(Account account)
        {
            InitializeComponent();
            _user = account;
            this.DataContext = this;
            _accountRepo = new AccountRepo();
        }

        private void btnViewStockWindow_Click(object sender, RoutedEventArgs e)
        {
            FarmProductManagementWindow farmProductManagementWindow = new FarmProductManagementWindow(_user);
            farmProductManagementWindow.Show();
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

        private void btnCategoriesWindow_Click(object sender, RoutedEventArgs e)
        {
            CategoryManagementWindow categoryManagementWindow = new CategoryManagementWindow(_user);
            categoryManagementWindow.Show();
            this.Close();
        }

        private void btnProductsManagementWindow_Click(object sender, RoutedEventArgs e)
        {
            ProductManagementWindow productManagementWindow = new ProductManagementWindow(_user);
            productManagementWindow.Show();
            this.Close();
        }

        private void btnStockManagementWindow_Click(object sender, RoutedEventArgs e)
        {
            StockManagementWindow stockManagementWindow = new StockManagementWindow(_user);
            stockManagementWindow.Show();
            this.Close();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            this.Close();
            login.Show();
        }

        private void LoadDataGrid(string searchText)
        {
            dtgAccounts.ItemsSource = _accountRepo.SearchAccounts(searchText).Select(a => new
            {
                a.AccountId,
                a.FullName,
                a.Username,
                Role = a.Role == 1 ? "Owner" : (a.Role == 2 ? "Staff" : "Customer"),
                a.Email,
                a.PhoneNumber,
                Status = a.Status.Value ? "Active" : "Inactive"
            }).ToList();
        }

        private void dtgAccounts_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataGrid(string.Empty);
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text;
            LoadDataGrid(searchText);
        }

        private void btnAddAccount_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnToggleAccountStatus_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
