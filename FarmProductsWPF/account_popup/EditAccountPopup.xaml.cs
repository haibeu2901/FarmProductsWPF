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
    /// Interaction logic for EditAccountPopup.xaml
    /// </summary>
    public partial class EditAccountPopup : Window
    {
        private Account _selectedAccount;
        private readonly IAccountRepo _accountRepo;

        public delegate void AccountEditedEventHandler(object sender, EventArgs e);
        public event AccountEditedEventHandler AccountEdited;

        public EditAccountPopup(Account selectedAccount)
        {
            InitializeComponent();
            _selectedAccount = selectedAccount;
            _accountRepo = new AccountRepo();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtSubtitle.Text = $"Update details for \"{_selectedAccount.FullName}\"";
            txtAccountId.Text = $"#{_selectedAccount.AccountId}";
            txtFullName.Text = _selectedAccount.FullName;
            txtUsername.Text = _selectedAccount.Username;
            txtEmail.Text = _selectedAccount.Email;
            txtAddress.Text = _selectedAccount.Address;
            txtPhone.Text = _selectedAccount.PhoneNumber;
            cboRole.SelectedIndex = _selectedAccount.Role switch
            {
                1 => 0, // Owner (Tag="1")
                2 => 1, // Staff (Tag="2")
                3 => 2, // Customer (Tag="3")
                _ => 2  // Default to Customer if unknown
            };
            txtStatus.Text = _selectedAccount.Status.Value ? "Active" : "Inactive";
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnUpdateAccount_Click(object sender, RoutedEventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            if (!IsValidFullName(fullName))
            {
                return;
            }
            _selectedAccount.FullName = fullName;

            string email = txtEmail.Text.Trim();
            _selectedAccount.Email = email;

            string address = txtAddress.Text.Trim();
            _selectedAccount.Address = address;

            string phoneNumber = txtPhone.Text.Trim();
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                MessageBox.Show("Phone number cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _selectedAccount.PhoneNumber = phoneNumber;

            byte role = Convert.ToByte(((ComboBoxItem)cboRole.SelectedItem).Tag);
            if (role == 0)
            {
                MessageBox.Show("Please select a valid role.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _selectedAccount.Role = role;

            Account updatedAccount = _accountRepo.UpdateAccount(_selectedAccount);
            if (updatedAccount != null)
            {
                MessageBox.Show($"Account \"{_selectedAccount.FullName}\" updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                AccountEdited?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("Failed to update account. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Close();
        }

        /// <summary>
        /// Validates if the full name meets requirements:
        /// - Not empty
        /// - Between 3 and 20 characters
        /// - Contains only letters and spaces
        /// - Each word must start with an uppercase letter (Pascal case)
        /// </summary>
        /// <param name="fullName">The full name to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        private bool IsValidFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show("Full name cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (fullName.Length < 3 || fullName.Length > 20)
            {
                MessageBox.Show("Full name must be between 3 and 20 characters long.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(fullName, @"^[a-zA-Z\s]+$"))
            {
                MessageBox.Show("Full name can only contain letters and spaces.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Check if each word starts with an uppercase letter (Pascal case)
            string[] words = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                if (word.Length > 0 && !char.IsUpper(word[0]))
                {
                    MessageBox.Show("Each word in full name must start with an uppercase letter.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }

            return true;
        }
    }
}
