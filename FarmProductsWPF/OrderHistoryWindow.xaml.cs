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
        private Order _order;
        private readonly IOrderRepo _orderRepo;
        private readonly IOrderDetailRepo _orderDetailRepo;

        public Account CurrentUser
        {
            get { return _user; }
            private set { _user = value; }
        }

        public OrderHistoryWindow(Account account)
        {
            InitializeComponent();
            _user = account;
            _order = new Order();
            this.DataContext = this;
            _orderRepo = new OrderRepo();
            _orderDetailRepo = new OrderDetailRepo();
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

        private void LoadOrderHistoryDataGrid(string searchText)
        {
            lstOrders.ItemsSource = _orderRepo.SearchOrdersHistory(searchText).Select(o => new
            {
                OrderId = o.OrderId,
                OrderDate = o.OrderDate,
                CustomerName = o.Customer?.FullName ?? "Guest",
                CustomerPhoneNumber = o.Customer?.PhoneNumber ?? "N/A",
                TotalAmount = o.TotalAmount,
                OrderDetailsCount = o.OrderDetails.Count,
            }).ToList();
        }

        private void lstOrders_Loaded(object sender, RoutedEventArgs e)
        {
            LoadOrderHistoryDataGrid("");
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            LoadOrderHistoryDataGrid(searchText);
        }

        private void lstOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstOrders.SelectedItem != null)
            {
                dynamic selected = lstOrders.SelectedItem;
                int orderId = selected.OrderId;

                _order = _orderRepo.GetOrderById(orderId);
                if (_order != null)
                {
                    txtSelectedOrderId.Text = string.Format("Order #{0}", _order.OrderId);
                    txtSelectedOrderDate.Text = _order.OrderDate.ToString();
                    txtSelectedCustomerName.Text = _order.Customer?.FullName ?? "Guest";
                    txtSelectedCustomerPhone.Text = _order.Customer?.PhoneNumber ?? "N/A";
                    txtSelectedStaffName.Text = _order.Staff?.FullName;
                    txtSelectedTotalAmount.Text = _order.TotalAmount.ToString("N0") + " đ";

                    lstOrderItems.ItemsSource = _orderDetailRepo.GetOrderDetailsByOrderId(orderId).Select(od => new
                    {
                        DetailProductName = od.Product?.ProductName,
                        DetailTotalPrice = od.Quantity * od.UnitPrice,
                        CategoryName = od.Product?.Category?.CategoryName ?? "No Category",
                        DetailProductUnit = od.Product?.Unit ?? "pcs",
                        DetailQuantity = od.Quantity,
                        DetailUnitPrice = od.UnitPrice,
                        DetailSellingPrice = od.UnitPrice,
                    }).ToList();
                }
                else
                {
                    // Clear details when no order is selected
                    txtSelectedOrderId.Text = string.Empty;
                    txtSelectedOrderDate.Text = string.Empty;
                    txtSelectedCustomerName.Text = string.Empty;
                    txtSelectedCustomerPhone.Text = string.Empty;
                    txtSelectedStaffName.Text = string.Empty;
                    txtSelectedTotalAmount.Text = string.Empty;
                    lstOrderItems.ItemsSource = null;
                }
            }
        }
    }
}
