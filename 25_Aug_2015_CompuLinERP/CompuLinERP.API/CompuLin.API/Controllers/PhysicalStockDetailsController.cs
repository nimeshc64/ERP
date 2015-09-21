using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuLinERP.API.Controllers
{
    public class StockAdjustmentsDetailsController
    {
        CompuLinEntityModelEntities entities;

        public bool InsertNewDetails(PHY_MAST PHY_MAST_List, string docNo)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.PHY_MAST
                             select info);
                PHY_MAST_List.PHY_NO = docNo;
                entities.PHY_MAST.Add(PHY_MAST_List);
                entities.SaveChanges();
            }

            return true;
        }   

    }
}
