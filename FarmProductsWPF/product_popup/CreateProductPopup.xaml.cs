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
    /// Interaction logic for CreateProductPopup.xaml
    /// </summary>
    public partial class CreateProductPopup : Window
    {
        public delegate void ProductCreatedEventHandler(object sender, EventArgs e);
        public event ProductCreatedEventHandler ProductCreated;

        private readonly IProductRepo _productRepo;
        private readonly ICategoryRepo _categoryRepo;

        public CreateProductPopup()
        {
            InitializeComponent();
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

        private void btnCreateProduct_Click(object sender, RoutedEventArgs e)
        {
            string unit = txtUnit.Text.Trim();
            if (string.IsNullOrWhiteSpace(unit))
            {
                MessageBox.Show("Unit cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string productName = txtProductName.Text.Trim();
            if (string.IsNullOrWhiteSpace(productName)) 
            {
                MessageBox.Show("Product name cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            decimal sellingPrice;
            if (!decimal.TryParse(txtPrice.Text.Trim(), out sellingPrice) || sellingPrice <= 0)
            {
                MessageBox.Show("Please enter a valid price.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string description = txtDescription.Text.Trim();

            if (cboCategory.SelectedItem == null || cboCategory.SelectedValue == null)
            {
                MessageBox.Show("Please select a category.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int categoryId = (int)cboCategory.SelectedValue;
            if (categoryId <= 0)
            {
                MessageBox.Show("Invalid category selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            Product newProduct = new Product
            {
                ProductName = productName,
                Unit = unit,
                SellingPrice = sellingPrice,
                Description = description,
                CategoryId = categoryId
            };
            Product createdProduct = _productRepo.AddProduct(newProduct);
            if (createdProduct == null)
            {
                MessageBox.Show("Failed to create product. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show($"Product \"{createdProduct.ProductName}\" created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            ProductCreated?.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cboCategory.Items.Clear();
            cboCategory.ItemsSource = _categoryRepo.GetAllCategories();
            cboCategory.DisplayMemberPath = "CategoryName";
            cboCategory.SelectedValuePath = "CategoryId";
        }
    }
}
