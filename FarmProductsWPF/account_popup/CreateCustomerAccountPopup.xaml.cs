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
    /// Interaction logic for CreateCustomerAccountPopup.xaml
    /// </summary>
    public partial class CreateCustomerAccountPopup : Window
    {
        private readonly IAccountRepo _accountRepo;
        private string _phone;

        public delegate void CustomerAccountCreatedEventHandler(object sender, EventArgs e);
        public event CustomerAccountCreatedEventHandler CustomerAccountCreated;

        public CreateCustomerAccountPopup(string phoneNumber)
        {
            InitializeComponent();
            _accountRepo = new AccountRepo();
            _phone = phoneNumber;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            if (!IsValidFullName(fullName))
            {
                return;
            }

            string phoneNumber = txtPhone.Text.Trim();
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                MessageBox.Show("Phone number cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Account account = new Account
            {
                FullName = fullName,
                Username = phoneNumber,
                Password = "1",
                Role = 3,
                Email = "",
                PhoneNumber = phoneNumber,
                Address = "",
                Status = true,
            };

            Account createdAccount = _accountRepo.AddAccount(account);
            if (createdAccount == null)
            {
                MessageBox.Show("Failed to create account. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show($"Account \"{createdAccount.Username}\" created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                CustomerAccountCreated?.Invoke(this, new CustomerAccountCreatedEventArgs(createdAccount));
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtPhone.Text = _phone;
        }
    }

    public class CustomerAccountCreatedEventArgs : EventArgs
    {
        public Account CreatedAccount { get; }
        public CustomerAccountCreatedEventArgs(Account createdAccount)
        {
            CreatedAccount = createdAccount;
        }
    }
}
