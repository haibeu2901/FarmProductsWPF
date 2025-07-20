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
    }
}
