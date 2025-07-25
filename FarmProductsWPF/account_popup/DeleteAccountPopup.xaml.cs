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

namespace FarmProductsWPF.account_popup
{
    /// <summary>
    /// Interaction logic for DeleteAccountPopup.xaml
    /// </summary>
    public partial class DeleteAccountPopup : Window
    {
        private Account _selectedAccount;
        private readonly IAccountRepo _accountRepo;
        private readonly IOrderRepo _orderRepo;

        public delegate void AccountDeletedEventHandler(object sender, EventArgs e);
        public event AccountDeletedEventHandler AccountDeleted;

        public DeleteAccountPopup(Account selectedAccount)
        {
            InitializeComponent();
            _selectedAccount = selectedAccount;
            _accountRepo = new AccountRepo();
            _orderRepo = new OrderRepo();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtConfirmation.Text = $"Are you sure you want to delete the account: {_selectedAccount.Username}?";
            lblAccount.Text = _selectedAccount.Username;
            lblUsername.Text = _selectedAccount.Username;
            lblRole.Text = _selectedAccount.Role == 1 ? "Owner" : (_selectedAccount.Role == 2 ? "Staff" : "Customer");
            lblEmail.Text = _selectedAccount.Email;
            lblAddress.Text = _selectedAccount.Address;
            lblStatus.Text = _selectedAccount.Status.Value ? "Active" : "Inactive";
            lblOrderWarning.Text = $"This account has {_orderRepo.GetOrdersByAccountId(_selectedAccount.AccountId).Count} orders associated with it.";
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show($"Do you really want to delete the account: {_selectedAccount.Username}?", "Confirm Delete", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                int id = _selectedAccount.AccountId;
                _accountRepo.DeleteAccount(id);
                if (_accountRepo.GetAccountById(id) == null)
                {
                    MessageBox.Show($"Account \"{_selectedAccount.FullName}\" deleted successfully.", "Success", MessageBoxButton.OK);
                    AccountDeleted?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    MessageBox.Show($"Failed to delete category \"{_selectedAccount.FullName}\".", "Error", MessageBoxButton.OK);
                }
                this.Close();
            }
        }
    }
}
