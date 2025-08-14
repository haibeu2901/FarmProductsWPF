using FarmProductsWPF.account_popup;
using FarmProductsWPF_Repositories.Implements;
using FarmProductsWPF_Repositories.Interfaces;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FarmProductsWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IProductRepo _productRepo;

        public MainWindow()
        {
            InitializeComponent();
            _productRepo = new ProductRepo();
        }

        private void dtgProduct_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataGrid(string.Empty);
        }

        private void LoadDataGrid(string text)
        {
            dtgProduct.ItemsSource = _productRepo.SearchProduct(text).Select(p => new
            {
                p.ProductId,
                p.ProductName,
                Category = p.Category.CategoryName,
                p.Unit,
                p.SellingPrice,
                p.Description
            }).ToList();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterCustomerAccountPopup registerCustomerAccountPopup = new RegisterCustomerAccountPopup();
            registerCustomerAccountPopup.ShowDialog();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            LoadDataGrid(searchText);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
            this.Close();
        }
    }
}