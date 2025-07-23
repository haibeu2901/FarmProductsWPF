using FarmProductsWPF_BOs;
using FarmProductsWPF_DAOs;
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
    /// Interaction logic for OrderHistoryWindow.xaml
    /// </summary>
    public partial class OrderHistoryWindow : Window
    {
        private Account _user;
        private readonly IOrderRepo _orderRepo;

        public Account CurrentUser
        {
            get { return _user; }
            private set { _user = value; }
        }

        public OrderHistoryWindow(Account account)
        {
            InitializeComponent();
            _user = account;
            this.DataContext = this;
            _orderRepo = new OrderRepo();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            this.Close();
            login.Show();
        }

        private void btnCreateOrderWindow_Click(object sender, RoutedEventArgs e)
        {
            CreateOrderWindow createOrderWindow = new CreateOrderWindow(_user);
            createOrderWindow.Show();
            this.Close();
        }

        private void btnViewStockWindow_Click(object sender, RoutedEventArgs e)
        {
            FarmProductManagementWindow viewStockWindow = new FarmProductManagementWindow(_user);
            viewStockWindow.Show();
            this.Close();
        }

        private void lstOrders_Loaded(object sender, RoutedEventArgs e)
        {
            lstOrders.ItemsSource = _orderRepo.GetOrdersHistory().Select(o => new
            {
                OrderId = o.OrderId,
                OrderDate = o.OrderDate,
                CustomerName = o.Customer?.FullName,
                CustomerPhoneNumber = o.Customer?.PhoneNumber,
                TotalAmount = o.TotalAmount,
                OrderDetailsCount = o.OrderDetails.Count,
            }).ToList();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
