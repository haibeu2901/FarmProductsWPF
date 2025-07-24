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

namespace FarmProductsWPF.product_popup
{
    /// <summary>
    /// Interaction logic for DeleteProductPopup.xaml
    /// </summary>
    public partial class DeleteProductPopup : Window
    {
        private Product _selectedProduct;
        private readonly IProductRepo _productRepo;
        private readonly IStockRepo _stockRepo;

        public delegate void ProductDeletedEventHandler(object sender, EventArgs e);
        public event ProductDeletedEventHandler ProductDeleted;

        public DeleteProductPopup(Product product)
        {
            InitializeComponent();
            _selectedProduct = product;
            _productRepo = new ProductRepo();
            _stockRepo = new StockRepo();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show($"Do you really want to delete the product \"{_selectedProduct.ProductName}\"?", "Confirm Delete", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    int id = _selectedProduct.ProductId;
                    
                    var stock = _stockRepo.GetStockByProductId(id);
                    if (stock != null && stock.Quantity > 0)
                    {
                        MessageBox.Show($"Cannot delete this product because it still has {stock.Quantity} units in stock. Please update the stock quantity to 0 first.", 
                            "Stock Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    
                    // Delete product (this should handle deleting the stock as well)
                    bool productDeleted = _productRepo.DeleteProduct(id);
                    
                    if (productDeleted)
                    {
                        MessageBox.Show("Product deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        ProductDeleted?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete product. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    this.Close();
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtConfirmation.Text = $"Are you sure you want to delete the product: {_selectedProduct.ProductName}?";
            lblProductName.Text = _selectedProduct.ProductName;
            lblCategory.Text = _selectedProduct.Category?.CategoryName;
            lblUnit.Text = _selectedProduct.Unit;
            lblPrice.Text = string.Format("{0:#,##0}₫", _selectedProduct.SellingPrice);
            lblStockQuantity.Text = _selectedProduct.Stock?.Quantity.ToString();
            lblStockUnit.Text = _selectedProduct.Unit;
            int stockQuantity = _selectedProduct.Stock?.Quantity ?? 0;
            txtStockStatus.Text = stockQuantity > 25 ? "In Stock" : (stockQuantity > 0 ? "Low Stock" : "Out of Stock");
            lblDescription.Text = _selectedProduct.Description;
            lblStockValueWarning.Text = $"This product has {stockQuantity} units in stock.";
        }
    }
}
