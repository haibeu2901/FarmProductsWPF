using FarmProductsWPF_BOs;
using FarmProductsWPF_DAOs;
using FarmProductsWPF_Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductsWPF_Repositories.Implements
{
    public class ImportedStockRepo : IImportedStockRepo
    {
        public ImportedStock AddStock(ImportedStock imStock) => ImportedStockDAO.Instance.AddStock(imStock);

        public List<ImportedStock> GetStockHistoryByProductId(int productId) => ImportedStockDAO.Instance.GetStockHistoryByProductId(productId);
    }
}
