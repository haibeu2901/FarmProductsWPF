using FarmProductsWPF_BOs;
using FarmProductsWPF_DAOs;
using FarmProductsWPF_Repositories.Interfaces;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductsWPF_Repositories.Implements
{
    public class AccountRepo : IAccountRepo
    {
        public Account AddAccount(Account account)
        {
            return AccountDAO.Instance.AddAccount(account);
        }

        public bool DeleteAccount(int accountId)
        {
            return AccountDAO.Instance.DeleteAccount(accountId);
        }

        public Account? GetAccountById(int accountId)
        {
            return AccountDAO.Instance.GetAccountById(accountId);
        }

        public Account? GetAccountByLogin(string username, string password)
        {
            return AccountDAO.Instance.GetAccountByLogin(username, password);
        }

        public List<Account> GetAllAccounts()
        {
            return AccountDAO.Instance.GetAllAccounts();
        }

        public Account? GetCustomerByPhoneNumber(string phoneNumber)
        {
            return AccountDAO.Instance.GetCustomerByPhoneNumber(phoneNumber);
        }

        public Account UpdateAccount(Account account)
        {
            return AccountDAO.Instance.UpdateAccount(account);
        }
    }
}
