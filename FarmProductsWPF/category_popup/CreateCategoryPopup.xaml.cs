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
    /// Interaction logic for CreateCategoryPopup.xaml
    /// </summary>
    public partial class CreateCategoryPopup : Window
    {
        public delegate void CategoryCreatedEventHandler(object sender, EventArgs e);
        public event CategoryCreatedEventHandler CategoryCreated;

        private readonly ICategoryRepo _categoryRepo;

        public CreateCategoryPopup()
        {
            InitializeComponent();
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

        private void btnCreateCategory_Click(object sender, RoutedEventArgs e)
        {
            string categoryName = txtCategoryName.Text.Trim();
            string description = txtDescription.Text.Trim();

            if (string.IsNullOrWhiteSpace(categoryName))
            {
                MessageBox.Show("Category name cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ProductCategory newCategory = new ProductCategory
            {
                CategoryName = categoryName,
                Description = description
            };

            ProductCategory createdCategory = _categoryRepo.AddCategory(newCategory);
            if (createdCategory == null)
            {
                MessageBox.Show("Failed to create category. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show($"Create new Category successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            CategoryCreated?.Invoke(this, EventArgs.Empty);
            this.Close();
        }
    }
}
