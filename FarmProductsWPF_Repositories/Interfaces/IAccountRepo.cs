using FarmProductsWPF_BOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductsWPF_Repositories.Interfaces
{
    public interface IAccountRepo
    {
        Account? GetAccountByLogin(string username, string password);
        Account? GetCustomerByPhoneNumber(string phoneNumber);
        bool DeleteAccount(int accountId);
        Account UpdateAccount(Account account);
        Account AddAccount(Account account);
        Account? GetAccountById(int accountId);
        List<Account> GetAllAccounts();
        List<Account> SearchAccounts(string searchText);
    }
}
