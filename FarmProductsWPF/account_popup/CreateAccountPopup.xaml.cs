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
    /// Interaction logic for CreateAccountPopup.xaml
    /// </summary>
    public partial class CreateAccountPopup : Window
    {
        public delegate void AccountCreatedEventHandler(object sender, EventArgs e);
        public event AccountCreatedEventHandler AccountCreated;

        private readonly IAccountRepo _accountRepo;

        public CreateAccountPopup()
        {
            InitializeComponent();
            _accountRepo = new AccountRepo();
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

            string userName = txtUsername.Text.Trim();
            if (!IsValidUsername(userName))
            {
                return;
            }

            string password = txtPassword.Password.Trim();
            if (!IsValidPassword(password))
            {
                return;
            }

            byte role = Convert.ToByte(((ComboBoxItem)cboRole.SelectedItem).Tag);
            if (role == 0)
            {
                MessageBox.Show("Please select a valid role.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string email = txtEmail.Text.Trim();

            string phoneNumber = txtPhone.Text.Trim();
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                MessageBox.Show("Phone number cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string address = txtAddress.Text.Trim();

            Account account = new Account
            {
                FullName = fullName,
                Username = userName,
                Password = password,
                Role = role,
                Email = email,
                PhoneNumber = phoneNumber,
                Address = address,
                Status = true // Default to active status
            };

            Account createdAccount = _accountRepo.AddAccount(account);
            if (createdAccount == null)
            {
                MessageBox.Show("Failed to create account. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show($"Account \"{createdAccount.FullName}\" created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                AccountCreated?.Invoke(this, EventArgs.Empty);
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
        
        /// <summary>
        /// Validates if the username meets requirements:
        /// - Not empty
        /// - Between 5 and 15 characters
        /// - Contains only letters, numbers, and underscores
        /// - Starts with a letter
        /// </summary>
        /// <param name="username">The username to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        private bool IsValidUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Username cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (username.Length < 5 || username.Length > 15)
            {
                MessageBox.Show("Username must be between 5 and 15 characters long.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (!System.Text.RegularExpressions.Regex.IsMatch(username, @"^[a-zA-Z][a-zA-Z0-9_]*$"))
            {
                MessageBox.Show("Username can only contain letters, numbers, and underscores, and must start with a letter.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            return true;
        }
        
        /// <summary>
        /// Validates if the password meets requirements:
        /// - Not empty
        /// - At least 8 characters long
        /// - Contains at least one uppercase letter, one lowercase letter, one number, and one special character
        /// </summary>
        /// <param name="password">The password to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Password cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (password.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters long.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"[A-Z]"))
            {
                MessageBox.Show("Password must contain at least one uppercase letter.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"[a-z]"))
            {
                MessageBox.Show("Password must contain at least one lowercase letter.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"[0-9]"))
            {
                MessageBox.Show("Password must contain at least one number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]"))
            {
                MessageBox.Show("Password must contain at least one special character.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            return true;
        }
    }
}
