using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuLinERP.API.Controllers
{
    public class TransactionTypesController
    {
        CompuLinEntityModelEntities entities;

        public List<TXN_TYPES>  GetTransactionTypes(string compcode, string txntype)
        {
            List<TXN_TYPES> txnTypes = new List<TXN_TYPES>();

            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from logInfo in entities.TXN_TYPES
                             where logInfo.TXN_TYPE == txntype && logInfo.COMPCODE == compcode
                             select logInfo);

                if (query.Any())
                    txnTypes = query.ToList();  
            }

            return txnTypes;
        }


    }
}
