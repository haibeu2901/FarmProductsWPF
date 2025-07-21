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
    /// Interaction logic for CustomerOrderWindow.xaml
    /// </summary>
    public partial class CustomerOrderWindow : Window
    {
        private Account user;
        private readonly IOrderRepo _orderRepo;
        
        // Add public property for binding
        public Account CurrentUser
        {
            get { return user; }
            private set { user = value; }
        }

        public CustomerOrderWindow(Account account)
        {
            InitializeComponent();
            user = account;
            // Set the DataContext to this window instance
            this.DataContext = this;
            _orderRepo = new OrderRepo();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            this.Close();
            login.Show();
        }

        private void dtgOrders_Loaded(object sender, RoutedEventArgs e)
        {
            dtgOrders.ItemsSource = _orderRepo.GetOrdersByAccountId(user.AccountId).Select(o => new
            {
                o.OrderId,
                StaffName = o.Staff.FullName,
                o.OrderDate,
                o.TotalAmount,
                TotalItems = o.OrderDetails.Count
            }).ToList();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
