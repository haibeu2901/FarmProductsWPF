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
    /// Interaction logic for UpdateStockPopup.xaml
    /// </summary>
    public partial class UpdateStockPopup : Window
    {
        private Account _user;
        private Stock _stockProduct;
        private readonly IStockRepo _stockRepo;

        public delegate void StockUpdatedEventHandler(object sender, EventArgs e);
        public event StockUpdatedEventHandler StockUpdated;

        public UpdateStockPopup(Account account, Stock stockProduct)
        {
            InitializeComponent();
            _user = account;
            _stockProduct = stockProduct;
            _stockRepo = new StockRepo();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateStockButton_Click(object sender, RoutedEventArgs e)
        {
            // Update stock fields
            _stockProduct.Quantity += int.Parse(txtAddedStock.Text);
            _stockProduct.Notes = txtNotes.Text;
            _stockProduct.UpdatedBy = _user.AccountId;

            Stock updatedStock = _stockRepo.UpdateStockLevel(_stockProduct);
            if (updatedStock != null)
            {
                MessageBox.Show($"Stock for {_stockProduct.Product.ProductName} updated successfully.\nNew stock level: {updatedStock.Quantity} {_stockProduct.Product.Unit}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                
                StockUpdated?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("Failed to update stock. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtSubheader.Text = $"Update stock inventory for {_stockProduct.Product.ProductName}";
            txtProductName.Text = _stockProduct.Product.ProductName;
            txtProductId.Text = $"ID: {_stockProduct.ProductId}";
            txtStockLevel.Text = $"{_stockProduct.Quantity} {_stockProduct.Product.Unit}";
        }
    }
}
