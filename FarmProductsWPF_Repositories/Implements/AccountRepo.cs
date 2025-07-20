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
        public Account? GetAccountByLogin(string username, string password)
        {
            return AccountDAO.Instance.GetAccountByLogin(username, password);
        }
    }
}
