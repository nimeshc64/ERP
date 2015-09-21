using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuLinERP.API.Controllers
{
    public class SalesPriceTypesController
    {
        CompuLinEntityModelEntities entities;

        public  List<PRICE_TYPES> GetAllPriceTypes(string compcode, string txntype)
        {
             List<PRICE_TYPES> allPrices = new List<PRICE_TYPES>();

            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from logInfo in entities.PRICE_TYPES 
                             where logInfo.TXN_TYPE  == txntype && logInfo.COMPCODE == compcode
                             select logInfo);

                if (query.Any())
                    allPrices = query.ToList();
            }

            return allPrices;
        }
    }
}
