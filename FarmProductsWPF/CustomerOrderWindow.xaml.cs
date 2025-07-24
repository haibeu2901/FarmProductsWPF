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
        private Account _user;
        private Order _order;
        private readonly IOrderRepo _orderRepo;
        private readonly IOrderDetailRepo _orderDetailRepo;
        
        // Add public property for binding
        public Account CurrentUser
        {
            get { return _user; }
            private set { _user = value; }
        }

        public Order SelectedOrder
        {
            get { return _order; }
            private set { _order = value; }
        }

        public CustomerOrderWindow(Account account)
        {
            InitializeComponent();
            _user = account;
            _order = new Order();
            // Set the DataContext to this window instance
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

        private void LoadOrderDataGrid(string searchText)
        {
            dtgOrders.ItemsSource = _orderRepo.SearchOrdersByAccountId(_user.AccountId, searchText).Select(o => new
            {
                o.OrderId,
                StaffName = o.Staff.FullName,
                o.OrderDate,
                o.TotalAmount,
                TotalItems = o.OrderDetails.Count
            }).ToList();
        }

        private void dtgOrders_Loaded(object sender, RoutedEventArgs e)
        {
            LoadOrderDataGrid(string.Empty);
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadOrderDataGrid(txtSearch.Text);
        }

        private void dtgOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgOrders.SelectedItem != null)
            {
                dynamic selected = dtgOrders.SelectedItem;
                int orderId = selected.OrderId;

                _order = _orderRepo.GetOrderById(orderId);
                if (_order != null)
                {
                    txtOrderId.Text = _order.OrderId.ToString();
                    txtOrderDate.Text = _order.OrderDate.ToString();
                    txtStaffName.Text = _order.Staff.FullName;
                    txtTotalAmount.Text = _order.TotalAmount.ToString("N0") + " VND";

                    // Set dtgOrderDetail.ItemsSource
                    dtgOrderDetail.ItemsSource = _orderDetailRepo.GetOrderDetailsByOrderId(orderId)
                    .Select(od => new
                    {
                        ProductName = od.Product?.ProductName ?? "Unknown Product",
                        CategoryName = od.Product?.Category?.CategoryName ?? "No Category",
                        Quantity = od.Quantity,
                        Unit = od.Product?.Unit ?? "pcs",
                        UnitPrice = od.Product?.SellingPrice ?? 0,
                        SellingPrice = od.Quantity * (od.Product?.SellingPrice ?? 0)
                        }).ToList();
                    }
                }
                else
                {
                    // Clear details when no order is selected
                    txtOrderId.Text = string.Empty;
                    txtOrderDate.Text = string.Empty;
                    txtStaffName.Text = string.Empty;
                    txtTotalAmount.Text = string.Empty;
                    dtgOrderDetail.ItemsSource = null;
                }
        }
    }
}
