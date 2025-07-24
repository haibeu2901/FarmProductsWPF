using FarmProductsWPF.category_popup;
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
    /// Interaction logic for CategoryManagementWindow.xaml
    /// </summary>
    public partial class CategoryManagementWindow : Window
    {
        private Account _user;
        private readonly ICategoryRepo _categoryRepo;

        public Account CurrentUser
        {
            get { return _user; }
            private set { _user = value; }
        }

        public CategoryManagementWindow(Account account)
        {
            InitializeComponent();
            _user = account;
            this.DataContext = this;
            _categoryRepo = new CategoryRepo();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            this.Close();
            login.Show();
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

        private void btnStockManagementWindow_Click(object sender, RoutedEventArgs e)
        {
            StockManagementWindow stockManagementWindow = new StockManagementWindow(_user);
            stockManagementWindow.Show();
            this.Close();
        }

        private void LoadDataGrid(string searchText)
        {
            dtgCategories.ItemsSource = _categoryRepo.SearchCategory(searchText).Select(c => new
            {
                c.CategoryId,
                c.CategoryName,
                c.Description,
            }).ToList();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            LoadDataGrid(searchText);   
        }

        private void dtgCategories_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataGrid(string.Empty);
        }

        private void btnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            CreateCategoryPopup createCategoryPopup = new CreateCategoryPopup();
            createCategoryPopup.CategoryCreated += (s, args) =>
            {
                LoadDataGrid(string.Empty);
            };
            createCategoryPopup.Show();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dtgCategories.SelectedItem != null)
            {
                dynamic selectedCategory = dtgCategories.SelectedItem;
                if (selectedCategory != null)
                {
                    ProductCategory category = _categoryRepo.GetCategoryById(selectedCategory.CategoryId);
                    if (category != null)
                    {
                        EditCategoryPopup editCategoryPopup = new EditCategoryPopup(category);
                        editCategoryPopup.CategoryEdited += (s, args) =>
                        {
                            LoadDataGrid(category.CategoryName);
                        };
                        editCategoryPopup.Show();
                    }
                    else
                    {
                        MessageBox.Show("Selected category not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dtgCategories.SelectedItem != null)
            {
                dynamic selectedCategory = dtgCategories.SelectedItem;
                if (selectedCategory != null)
                {
                    ProductCategory category = _categoryRepo.GetCategoryById(selectedCategory.CategoryId);
                    if (category != null)
                    {
                        DeleteCategoryPopup deleteCategoryPopup = new DeleteCategoryPopup(category);
                        deleteCategoryPopup.CategoryDeleted += (s, args) =>
                        {
                            LoadDataGrid(string.Empty);
                        };
                        deleteCategoryPopup.Show();
                    }
                    else
                    {
                        MessageBox.Show("Selected category not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void btnProductsWindow_Click(object sender, RoutedEventArgs e)
        {
            ProductManagementWindow productManagementWindow = new ProductManagementWindow(_user);
            productManagementWindow.Show();
            this.Close();
        }
    }
}
