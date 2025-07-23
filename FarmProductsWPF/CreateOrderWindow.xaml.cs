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
    /// Interaction logic for CreateOrderWindow.xaml
    /// </summary>
    public partial class CreateOrderWindow : Window
    {
        private Account _user;
        private readonly IStockRepo _stockRepo;
        private readonly IAccountRepo _accountRepo;
        private Account? _selectedCustomer;
        private Order _order;

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
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }

        private void btnViewStock_Click(object sender, RoutedEventArgs e)
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
                orderDetail.UnitPrice = orderDetail.Product.SellingPrice * orderDetail.Quantity;
                totalPrice += orderDetail.UnitPrice;
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
                        OrderId = _order.OrderId,
                        ProductId = selectedProduct?.ProductId,
                        Product = selectedProduct?.Product,
                        Quantity = 1,
                        UnitPrice = selectedProduct?.Product?.SellingPrice ?? 0,
                        Total = selectedProduct?.Product?.SellingPrice * 1 ?? 0,
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
    }

    public class ProductViewModel
    {
        public Stock? Stock { get; set; }
        public string? ProductName { get; set; }
        public string? CategoryName { get; set; }
        public decimal? PriceDisplay { get; set; }
        public string? StockDisplay { get; set; }
    }
}
