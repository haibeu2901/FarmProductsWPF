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

namespace FarmProductsWPF.stock_popup
{
    /// <summary>
    /// Interaction logic for StockDetailsPopup.xaml
    /// </summary>
    public partial class StockDetailsPopup : Window
    {
        private readonly Account _account;
        private readonly Stock _stockProduct;

        private readonly IImportedStockRepo _importedStockRepo;
        private readonly IAccountRepo _accountRepo;

        public StockDetailsPopup(Account account, Stock stockProduct)
        {
            InitializeComponent();
            _account = account;
            _stockProduct = stockProduct;
            _importedStockRepo = new ImportedStockRepo();
            _accountRepo = new AccountRepo();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateStockButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateStockPopup updateStockPopup = new UpdateStockPopup(_account, _stockProduct);
            updateStockPopup.StockUpdated += (s, args) =>
            {
                // Handle stock updated event
                LoadDataGrid();
            };
            updateStockPopup.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtSubtitle.Text = $"View all stock updates for {_stockProduct.Product.ProductName}";
            txtProductName.Text = _stockProduct.Product.ProductName;
            txtCurrentStock.Text = _stockProduct.Quantity.ToString() + " " + _stockProduct.Product.Unit;
            txtCategory.Text = _stockProduct.Product.Category?.CategoryName ?? "No Category";
            var Status = _stockProduct.Quantity > 25 ? "In Stock" : (_stockProduct.Quantity > 0 ? "Low Stock" : "Out of Stock");
            txtStatus.Text = Status;
        }

        private void LoadDataGrid()
        {
            dgStockHistory.ItemsSource = _importedStockRepo.GetStockHistoryByProductId(_stockProduct.ProductId)
                .Select(s => new
                {
                    UpdateDate = s.UpdatedAt,
                    BeforeQuantity = s.StockBeforeUpdate,
                    ChangeAmount = s.UpdatedStockQuantity,
                    AfterQuantity = s.StockAfterUpdate,
                    Notes = s.Notes,
                    UpdatedBy = _accountRepo.GetAccountById(s.UpdatedBy)?.FullName ?? "Unknown"
                }).ToList();
        }

        private void dgStockHistory_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataGrid();
        }

    }
}
