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

namespace FarmProductsWPF.category_popup
{
    /// <summary>
    /// Interaction logic for EditCategoryPopup.xaml
    /// </summary>
    public partial class EditCategoryPopup : Window
    {
        private ProductCategory _selectedCategory;
        private readonly ICategoryRepo _categoryRepo;

        public delegate void CategoryEditedEventHandler(object sender, EventArgs e);
        public event CategoryEditedEventHandler CategoryEdited;

        public EditCategoryPopup(ProductCategory selectedCategory)
        {
            InitializeComponent();
            _selectedCategory = selectedCategory;
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

        private void btnUpdateCategory_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCategoryName.Text) || string.IsNullOrEmpty(txtDescription.Text))
            {
                MessageBox.Show("Category name and description cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Update the selected category with new values
            _selectedCategory.CategoryName = txtCategoryName.Text.Trim();
            _selectedCategory.Description = txtDescription.Text.Trim();

            ProductCategory updatedCategory = _categoryRepo.UpdateCategory(_selectedCategory);
            if (updatedCategory != null)
            {
                MessageBox.Show($"Category \"{_selectedCategory.CategoryName}\" updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                CategoryEdited?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("Failed to update category. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUpdateDetails.Text = $"Update details for \"{_selectedCategory.CategoryName}\"";
            lblCategoryId.Text = $"#{_selectedCategory.CategoryId}";
            txtCategoryName.Text = _selectedCategory.CategoryName;
            txtDescription.Text = _selectedCategory.Description;
            lblProductCount.Text = $"Products in this category: {_selectedCategory.Products.Count}";
        }
    }
}
