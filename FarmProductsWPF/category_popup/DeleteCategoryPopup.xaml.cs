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
    /// Interaction logic for DeleteCategoryPopup.xaml
    /// </summary>
    public partial class DeleteCategoryPopup : Window
    {
        private ProductCategory _selectedCategory;
        private readonly ICategoryRepo _categoryRepo;

        public delegate void CategoryDeletedEventHandler(object sender, EventArgs e);
        public event CategoryDeletedEventHandler CategoryDeleted;

        public DeleteCategoryPopup(ProductCategory selectedCategory)
        {
            InitializeComponent();
            _selectedCategory = selectedCategory;
            _categoryRepo = new CategoryRepo();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtConfirmation.Text = $"Are you sure you want to delete the category \"{_selectedCategory.CategoryName}\"?";
            lblCategoryName.Text = _selectedCategory.CategoryName;
            lblDescription.Text = _selectedCategory.Description;
            lblProductsCount.Text = $"{_selectedCategory.Products.Count} products";
            lblWarning.Text = $"Warning: {_selectedCategory.Products.Count} Products in this category";

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show($"Do you really want to delete the category \"{_selectedCategory.CategoryName}\"?", "Confirm Delete", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                int id = _selectedCategory.CategoryId;
                _categoryRepo.DeleteCategory(id);
                if (_categoryRepo.GetCategoryById(id) == null)
                {
                    MessageBox.Show($"Category \"{_selectedCategory.CategoryName}\" deleted successfully.", "Success", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show($"Failed to delete category \"{_selectedCategory.CategoryName}\".", "Error", MessageBoxButton.OK);
                }
                CategoryDeleted?.Invoke(this, EventArgs.Empty);
                this.Close();
            }
        }
    }
}
