using FarmProductsWPF_BOs;
using FarmProductsWPF_Repositories.Implements;
using FarmProductsWPF_Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for CreateOrderWindow.xaml
    /// </summary>
    public partial class CreateOrderWindow : Window
    {
        private Account _user;
        private readonly IStockRepo _stockRepo;
        private readonly IAccountRepo _accountRepo;
        private Account? _selectedCustomer;
        private Order _order;
        private readonly IOrderRepo _orderRepo;
        private readonly IOrderDetailRepo _orderDetailRepo;

        public Account CurrentUser
        {
            get { return _user; }
            private set { _user = value; }
        }

        public CreateOrderWindow(Account account)
        {
            InitializeComponent();
            _user = account;
            this.DataContext = this;
            _stockRepo = new StockRepo();
            _accountRepo = new AccountRepo();
            _orderRepo = new OrderRepo();
            _orderDetailRepo = new OrderDetailRepo();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }

        private void btnViewStockWindow_Click(object sender, RoutedEventArgs e)
        {
            FarmProductManagementWindow farmProductManagementWindow = new FarmProductManagementWindow(_user);
            farmProductManagementWindow.Show();
            this.Close();
        }

        private void dtgProducts_Loaded(object sender, RoutedEventArgs e)
        {
            var stocks = _stockRepo.GetAllStocks();
            
            dtgProducts.ItemsSource = stocks.Select(s => new ProductViewModel
            {
                Stock = s,
                ProductName = s.Product.ProductName,
                CategoryName = s.Product?.Category?.CategoryName,
                PriceDisplay = s.Product?.SellingPrice,
                StockQuantity = s.Quantity,
                StockDisplay = s.Quantity > 25 ? "In Stock" : (s.Quantity > 0 ? "Low Stock" : "Out of Stock"),
            }).ToList();
        }

        private void btnProductSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtProductSearch.Text.Trim();
            var stocks = _stockRepo.SearchStock(searchText);
            
            dtgProducts.ItemsSource = stocks.Select(s => new ProductViewModel
            {
                Stock = s,
                ProductName = s.Product.ProductName,
                CategoryName = s.Product?.Category?.CategoryName,
                PriceDisplay = s.Product?.SellingPrice,
                StockQuantity = s.Quantity,
                StockDisplay = s.Quantity > 25 ? "In Stock" : (s.Quantity > 0 ? "Low Stock" : "Out of Stock"),
            }).ToList();
        }

        private void btnCustomerSearch_Click(object sender, RoutedEventArgs e)
        {
            string customerSearchText = txtCustomerSearch.Text.Trim();

            _selectedCustomer = _accountRepo.GetCustomerByPhoneNumber(customerSearchText);

            if (_selectedCustomer != null)
            {
                // Show customer info in the panel
                txtCustomerName.Text = _selectedCustomer.FullName;
                txtCustomerPhone.Text = _selectedCustomer.PhoneNumber;
                customerInfoPanel.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Customer not found. Please check the phone number and try again.");
                customerInfoPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void btnClearCustomer_Click(object sender, RoutedEventArgs e)
        {
            // Clear the selected customer
            _selectedCustomer = null;

            // Hide the customer info panel
            customerInfoPanel.Visibility = Visibility.Collapsed;

            // Clear the search box
            txtCustomerSearch.Clear();
        }

        private decimal CalculateTotalPrice()
        {
            decimal totalPrice = 0;

            if (_order == null || _order.OrderDetails == null)
                return 0;

            foreach (var orderDetail in _order.OrderDetails)
            {
                if (orderDetail.Product == null)
                    continue;
                    
                // Don't modify UnitPrice, just calculate the total
                orderDetail.Total = orderDetail.UnitPrice * orderDetail.Quantity;
                totalPrice += orderDetail.Total ?? 0;
            }
            return totalPrice;
        }

        private void btnAddProductToCart_Click(object sender, RoutedEventArgs e)
        {
            if (dtgProducts.SelectedItem is ProductViewModel selectedProductVM)
            {
                Stock? selectedProduct = selectedProductVM.Stock;
                
                if (selectedProduct?.Quantity <= 0)
                {
                    MessageBox.Show("Selected product is out of stock.");
                    return;
                }

                if (_order == null)
                {
                    _order = new Order
                    {
                        StaffId = _user.AccountId,
                        CustomerId = _selectedCustomer?.AccountId,
                        OrderDate = DateTime.Now,
                        OrderDetails = new List<OrderDetail>()
                    };
                }

                var existingOrderDetail = _order.OrderDetails.FirstOrDefault(od => od.ProductId == selectedProduct?.ProductId);
                if (existingOrderDetail != null)
                {
                    existingOrderDetail.Quantity += 1;
                    existingOrderDetail.Total = existingOrderDetail.UnitPrice * existingOrderDetail.Quantity;
                }
                else
                {
                    var orderDetail = new OrderDetail
                    {
                        // Remove this line: OrderId = _order.OrderId,
                        ProductId = selectedProduct?.ProductId,
                        Product = selectedProduct?.Product,
                        Quantity = 1,
                        UnitPrice = selectedProduct?.Product?.SellingPrice ?? 0,
                        Total = selectedProduct?.Product?.SellingPrice ?? 0,
                    };
                    
                    _order.OrderDetails.Add(orderDetail);
                }

                _order.TotalAmount = CalculateTotalPrice();
                txtOrderTotalPrice.Text = string.Format("{0:#,##0}₫", _order.TotalAmount);
                dtgOrderCart_Loaded(sender, e);
            }
        }

        private void dtgOrderCart_Loaded(object sender, RoutedEventArgs e)
        {
            dtgOrderCart.ItemsSource = _order?.OrderDetails?.Select(od => new
            {
                OrderDetailName = od.Product?.ProductName,
                OrderDetailPrice = od.UnitPrice,
                OrderDetailQuantity = od.Quantity,
            }).ToList();
        }

        private void btnDecreaseItem_Click(object sender, RoutedEventArgs e)
        {
            if (dtgOrderCart.SelectedItem != null)
            {
                dynamic selectedItem = dtgOrderCart.SelectedItem;
                var orderDetail = _order.OrderDetails.FirstOrDefault(od => od.Product?.ProductName == selectedItem.OrderDetailName);
                
                if (orderDetail != null)
                {
                    if (orderDetail.Quantity > 1)
                    {
                        orderDetail.Quantity -= 1;
                        orderDetail.Total = orderDetail.UnitPrice * orderDetail.Quantity;
                    }
                    else
                    {
                        _order.OrderDetails.Remove(orderDetail);
                    }
                    _order.TotalAmount = CalculateTotalPrice();
                    txtOrderTotalPrice.Text = string.Format("{0:#,##0}₫", _order.TotalAmount);
                    dtgOrderCart_Loaded(sender, e);
                }
            }
        }

        private void btnIncreaseItem_Click(object sender, RoutedEventArgs e)
        {
            if (dtgOrderCart.SelectedItem != null)
            {
                dynamic selectedItem = dtgOrderCart.SelectedItem;
                var orderDetail = _order.OrderDetails.FirstOrDefault(od => od.Product?.ProductName == selectedItem.OrderDetailName);
                
                if (orderDetail != null)
                {
                    orderDetail.Quantity += 1;
                    orderDetail.Total = orderDetail.UnitPrice * orderDetail.Quantity;
                    _order.TotalAmount = CalculateTotalPrice();
                    txtOrderTotalPrice.Text = string.Format("{0:#,##0}₫", _order.TotalAmount);
                    dtgOrderCart_Loaded(sender, e);
                }
            }
        }

        private void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (dtgOrderCart.SelectedItem != null)
            {
                dynamic selectedItem = dtgOrderCart.SelectedItem;
                var orderDetail = _order.OrderDetails.FirstOrDefault(od => od.Product?.ProductName == selectedItem.OrderDetailName);
                
                if (orderDetail != null)
                {
                    _order.OrderDetails.Remove(orderDetail);
                    _order.TotalAmount = CalculateTotalPrice();
                    txtOrderTotalPrice.Text = string.Format("{0:#,##0}₫", _order.TotalAmount);
                    dtgOrderCart_Loaded(sender, e);
                }
            }
        }

        private void btnCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            if (_order == null || _order.OrderDetails.Count == 0)
            {
                MessageBox.Show("Please add products to the order before creating it.");
                return;
            }
            _order.CustomerId = _selectedCustomer?.AccountId;

            var orderToCreated = new Order
            {
                CustomerId = _order.CustomerId,
                StaffId = _user.AccountId,
                OrderDate = DateTime.Now,
                TotalAmount = _order.TotalAmount,
            };
            var createdOrder = _orderRepo.CreateOrder(orderToCreated);
            if (createdOrder != null)
            {
                var orderDetails = _order.OrderDetails.Select(od => new OrderDetail
                {
                    OrderId = createdOrder.OrderId,
                    ProductId = od.ProductId,
                    Quantity = od.Quantity,
                    UnitPrice = od.UnitPrice,
                    Total = od.Total
                }).ToList();
                var createdOrderDetails = _orderDetailRepo.CreateOrderDetails(createdOrder.OrderId, orderDetails);
                if (createdOrderDetails == null || createdOrderDetails.Count == 0)
                {
                    MessageBox.Show("Failed to create order & order details. Please try again.");
                    return;
                }
            }
            MessageBox.Show("Order created successfully!");
            // Clear the order and UI
            dtgOrderCart.ItemsSource = null;
            _order = null;
            txtOrderTotalPrice.Text = "0₫";
            txtCustomerSearch.Clear();
            txtCustomerName.Text = string.Empty;
            txtCustomerPhone.Text = string.Empty;
            customerInfoPanel.Visibility = Visibility.Collapsed;
            
            // Refresh the stock data displayed in the products grid
            RefreshProductsGrid();
        }

        // Add this new method to refresh the products grid
        private void RefreshProductsGrid()
        {
            var stocks = _stockRepo.GetAllStocks();
            
            dtgProducts.ItemsSource = stocks.Select(s => new ProductViewModel
            {
                Stock = s,
                ProductName = s.Product.ProductName,
                CategoryName = s.Product?.Category?.CategoryName,
                PriceDisplay = s.Product?.SellingPrice,
                StockQuantity = s.Quantity,
                StockDisplay = s.Quantity > 25 ? "In Stock" : (s.Quantity > 0 ? "Low Stock" : "Out of Stock"),
            }).ToList();
        }

        private void btnOrderHistoryWindow_Click(object sender, RoutedEventArgs e)
        {
            OrderHistoryWindow orderHistoryWindow = new OrderHistoryWindow(_user);
            orderHistoryWindow.Show();
            this.Close();
        }
    }

    public class ProductViewModel
    {
        public Stock? Stock { get; set; }
        public string? ProductName { get; set; }
        public string? CategoryName { get; set; }
        public int? StockQuantity { get; set; }
        public decimal? PriceDisplay { get; set; }
        public string? StockDisplay { get; set; }
        public string CombinedStockDisplay => $"{StockQuantity} {StockDisplay}";
    }
}
