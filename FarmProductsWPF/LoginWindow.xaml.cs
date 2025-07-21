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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IAccountRepo _accountRepo;

        public LoginWindow()
        {
            InitializeComponent();
            _accountRepo = new AccountRepo();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWin = new MainWindow();
            mainWin.Show();
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = pwdPassword.Password.Trim();

            var account = _accountRepo.GetAccountByLogin(username, password);
            if (account != null)
            {
                // Login successful
                if (account.Role == 1 || account.Role == 2)
                {
                    MessageBox.Show("Login successful!");
                }
                else if (account.Role == 3)
                {
                    // Open Customer Window for role 3
                    CustomerOrderWindow customerWindow = new CustomerOrderWindow(account);
                    customerWindow.Show();
                }
                this.Close();
            }
            else
            {
                // Login failed
                MessageBox.Show("Invalid username or password.");
            }
        }
    }
}
