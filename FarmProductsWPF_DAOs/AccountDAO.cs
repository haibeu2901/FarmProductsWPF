using FarmProductsWPF_BOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductsWPF_DAOs
{
    public class AccountDAO
    {
        private readonly FarmProductsDbContext _context;
        private static AccountDAO? _instance;

        public AccountDAO()
        {
            _context = new FarmProductsDbContext();
        }

        public static AccountDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AccountDAO();
                }
                return _instance;
            }
        }

        public Account? GetAccountByLogin(string username, string password)
        {
            return _context.Accounts
                .FirstOrDefault(a => (a.Username == username || a.Email == username) && 
                                        a.Password == password);
        }

        public Account? GetCustomerByPhoneNumber(string phoneNumber)
        {
            return _context.Accounts
                .FirstOrDefault(a => a.PhoneNumber == phoneNumber);
        }

        public List<Account> GetAllAccounts()
        {
            return _context.Accounts.ToList();
        }
        
        public Account? GetAccountById(int accountId)
        {
            return _context.Accounts.Find(accountId);
        }

        public Account AddAccount(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();
            return account;
        }

        public Account UpdateAccount(Account account)
        {
            var existingAccount = GetAccountById(account.AccountId);
            if (existingAccount != null)
            {
                existingAccount.FullName = account.FullName;
                existingAccount.Email = account.Email;
                existingAccount.PhoneNumber = account.PhoneNumber;
                existingAccount.Address = account.Address;
                _context.SaveChanges();
            }
            return existingAccount;
        }

        public bool DeleteAccount(int accountId)
        {
            var account = GetAccountById(accountId);
            if (account != null)
            {
                // Get orders related to this account
                var orderDAO = OrderDAO.Instance;
                var allOrders = orderDAO.GetOrdersByAccountId(accountId);

                // Update account id in orders
                foreach (var order in allOrders)
                {
                    if (order.CustomerId == accountId)
                    {
                        order.CustomerId = null; // Set to null if this account was the customer
                    }
                    if (order.StaffId == accountId)
                    {
                        order.StaffId = null; // Set to null if this account was the staff
                    }
                    orderDAO.UpdateOrder(order);
                }

                // Get and update stocks where this account is the updater
                var stockDAO = StockDAO.Instance;
                var allStocks = stockDAO.GetAllStocks();
                var accountStocks = allStocks.Where(s => s.UpdatedBy == accountId).ToList();
                
                foreach (var stock in accountStocks)
                {
                    stock.UpdatedBy = null;
                    stockDAO.UpdateStockLevel(stock);
                }

                // Remove the account
                _context.Accounts.Remove(account);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Account> SearchAccounts(string searchText)
        {
            return _context.Accounts
                .Where(a => a.FullName.ToLower().Contains(searchText.ToLower()) ||
                            a.Email.ToLower().Contains(searchText.ToLower()) ||
                            a.PhoneNumber.Contains(searchText) ||
                            a.Username.ToLower().Contains(searchText.ToLower()) ||
                            a.AccountId.ToString() == searchText)
                .ToList();
        }
    }
}
