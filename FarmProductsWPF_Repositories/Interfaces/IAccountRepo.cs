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
    }
}
