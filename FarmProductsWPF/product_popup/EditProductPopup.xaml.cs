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
    /// Interaction logic for EditProductPopup.xaml
    /// </summary>
    public partial class EditProductPopup : Window
    {
        private Product _selectedProduct;
        private readonly IProductRepo _productRepo;
        private readonly ICategoryRepo _categoryRepo;

        public delegate void ProductEditedEventHandler(object sender, EventArgs e);
        public event ProductEditedEventHandler ProductEdited;

        public EditProductPopup(Product product)
        {
            InitializeComponent();
            _selectedProduct = product;
            _productRepo = new ProductRepo();
            _categoryRepo = new CategoryRepo();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            string productName = txtProductName.Text.Trim();
            if (string.IsNullOrWhiteSpace(productName))
            {
                MessageBox.Show("Product name cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _selectedProduct.ProductName = productName;

            int categoryId = (int)cmbCategory.SelectedValue;
            if (categoryId <= 0)
            {
                MessageBox.Show("Please select a valid category.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _selectedProduct.CategoryId = categoryId;

            string unit = txtUnit.Text.Trim();
            if (string.IsNullOrWhiteSpace(unit))
            {
                MessageBox.Show("Unit cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _selectedProduct.Unit = unit;

            decimal sellingPrice = decimal.Parse(txtSellingPrice.Text.Trim());
            if (sellingPrice <= 0)
            {
                MessageBox.Show("Selling price must be greater than zero.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _selectedProduct.SellingPrice = sellingPrice;

            string description = txtDescription.Text.Trim();
            _selectedProduct.Description = description;

            Product updatedProduct = _productRepo.UpdateProduct(_selectedProduct);
            if (updatedProduct != null)
            {
                MessageBox.Show($"Product \"{_selectedProduct.ProductName}\" updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                ProductEdited?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("Failed to update product. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbCategory.Items.Clear();
            cmbCategory.ItemsSource = _categoryRepo.GetAllCategories();
            cmbCategory.DisplayMemberPath = "CategoryName";
            cmbCategory.SelectedValuePath = "CategoryId";

            txtUpdateDetails.Text = $"Update details for \"{_selectedProduct.ProductName}\"";
            lblProductId.Text = $"#{_selectedProduct.ProductId}";
            txtProductName.Text = _selectedProduct.ProductName;
            cmbCategory.SelectedValue = _selectedProduct.Category?.CategoryId;
            txtUnit.Text = _selectedProduct.Unit;
            txtSellingPrice.Text = _selectedProduct.SellingPrice.ToString();
            txtDescription.Text = _selectedProduct.Description;
            int stockQuantity = _selectedProduct.Stock?.Quantity ?? 0;
            lblStockQuantity.Text = stockQuantity.ToString();
            lblStockUnit.Text = _selectedProduct.Unit;
            txtStockStatus.Text = stockQuantity > 25 ? "In Stock" : (stockQuantity > 0 ? "Low Stock" : "Out of Stock");
        }
    }
}
