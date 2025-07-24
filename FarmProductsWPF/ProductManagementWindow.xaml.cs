using FarmProductsWPF.product_popup;
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
    /// Interaction logic for ProductManagementWindow.xaml
    /// </summary>
    public partial class ProductManagementWindow : Window
    {
        private Account _user;
        private readonly IProductRepo _productRepo;

        public Account CurrentUser
        {
            get { return _user; }
            private set { _user = value; }
        }

        public ProductManagementWindow(Account account)
        {
            InitializeComponent();
            _user = account;
            this.DataContext = this;
            _productRepo = new ProductRepo();
        }

        private void btnViewStockWindow_Click(object sender, RoutedEventArgs e)
        {
            FarmProductManagementWindow farmProductManagementWindow = new FarmProductManagementWindow(_user);
            farmProductManagementWindow.Show();
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

        private void btnCategoriesWindow_Click(object sender, RoutedEventArgs e)
        {
            CategoryManagementWindow categoryManagementWindow = new CategoryManagementWindow(_user);
            categoryManagementWindow.Show();
            this.Close();
        }

        private void btnStockManagementWindow_Click(object sender, RoutedEventArgs e)
        {
            StockManagementWindow stockManagementWindow = new StockManagementWindow(_user);
            stockManagementWindow.Show();
            this.Close();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            this.Close();
            login.Show();
        }

        private void LoadDataGrid(string searchText)
        {
            dtgProducts.ItemsSource = _productRepo.SearchProduct(searchText).Select(p => new
            {
                p.ProductId,
                p.ProductName,
                p.Category?.CategoryName,
                p.Unit,
                p.SellingPrice,
                QuantityInStock = p.Stock?.Quantity ?? 0,
                Status = p.Stock?.Quantity > 25 ? "In Stock" : (p.Stock?.Quantity > 0 ? "Low Stock" : "Out of Stock"),
            }).ToList();
        }

        private void dtgProducts_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataGrid(string.Empty);
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text;
            LoadDataGrid(searchText);
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            CreateProductPopup createProductPopup = new CreateProductPopup();
            createProductPopup.ProductCreated += (s, args) => 
            {
                LoadDataGrid(string.Empty);
            };
            createProductPopup.Show();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dtgProducts.SelectedItem != null)
            {
                dynamic selectedProduct = dtgProducts.SelectedItem;
                if (selectedProduct != null)
                {
                    Product product = _productRepo.GetProductById(selectedProduct.ProductId);
                    if (product != null)
                    {
                        EditProductPopup editProductPopup = new EditProductPopup(product);
                        editProductPopup.ProductEdited += (s, args) =>
                        {
                            LoadDataGrid(product.ProductName);
                        };
                        editProductPopup.Show();
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dtgProducts.SelectedItem != null)
            {
                dynamic selectedProduct = dtgProducts.SelectedItem;
                if (selectedProduct != null)
                {
                    Product product = _productRepo.GetProductById(selectedProduct.ProductId);
                    if (product != null)
                    {
                        DeleteProductPopup deleteProductPopup = new DeleteProductPopup(product);
                        deleteProductPopup.ProductDeleted += (s, args) =>
                        {
                            LoadDataGrid(string.Empty);
                        };
                        deleteProductPopup.Show();
                    }
                }
            }
        }
    }
}
