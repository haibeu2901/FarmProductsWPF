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
    }
}
