using FarmProductsWPF.account_popup;
using FarmProductsWPF_BOs;
using FarmProductsWPF_Repositories.Implements;
using FarmProductsWPF_Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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
            CreateAccountPopup createAccountPopup = new CreateAccountPopup();
            createAccountPopup.AccountCreated += (s, args) =>
            {
                LoadDataGrid(string.Empty);
            };
            createAccountPopup.ShowDialog();
        }

        private void btnToggleAccountStatus_Click(object sender, RoutedEventArgs e)
        {
            if (dtgAccounts.SelectedItem != null)
            {
                dynamic selectedAccount = dtgAccounts.SelectedItem;
                if (selectedAccount != null)
                {
                    int accountId = selectedAccount.AccountId;
                    Account account = _accountRepo.GetAccountById(accountId);
                    if (account != null)
                    {
                        account.Status = !account.Status; // Toggle status
                        _accountRepo.UpdateAccount(account);
                        LoadDataGrid(string.Empty);
                        MessageBox.Show($"Account status updated to {(account.Status.Value ? "Active" : "Inactive")}.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Account not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an account to toggle status.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dtgAccounts.SelectedItem != null)
            {
                dynamic selectedAccount = dtgAccounts.SelectedItem;
                if (selectedAccount != null)
                {
                    int accountId = selectedAccount.AccountId;
                    Account account = _accountRepo.GetAccountById(accountId);
                    if (account != null)
                    {
                        EditAccountPopup editAccountPopup = new EditAccountPopup(account);
                        editAccountPopup.AccountEdited += (s, args) =>
                        {
                            LoadDataGrid(string.Empty);
                        };
                        editAccountPopup.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Account not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dtgAccounts.SelectedItem != null)
            {
                dynamic selectedAccount = dtgAccounts.SelectedItem;
                if (selectedAccount != null)
                {
                    int accountId = selectedAccount.AccountId;
                    Account account = _accountRepo.GetAccountById(accountId);
                    if (account != null)
                    {
                        DeleteAccountPopup deleteAccountPopup = new DeleteAccountPopup(account);
                        deleteAccountPopup.AccountDeleted += (s, args) =>
                        {
                            LoadDataGrid(string.Empty);
                        };
                        deleteAccountPopup.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Account not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an account to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
