using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuLinERP.API.Controllers
{
    public class InvoiceMastController
    {
        CompuLinEntityModelEntities entities;

        public bool InsertNewDetails(INV_MAST details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.INV_MAST                            
                             select info);

                entities.INV_MAST.Add(details);
                entities.SaveChanges();
            }

            return true;
        }

     

        public bool UpdateDetails(INV_MAST searchdetails, INV_MAST details)
        {
            //using (entities = new CompuLinEntityModelEntities())
            //{
            //    var query = (from logInfo in entities.UNIT_MAST
            //                 where logInfo.COMPCODE == searchdetails.COMPCODE &&
            //                 logInfo.UNIT_CODE == searchdetails.UNIT_CODE
            //                 select logInfo);

            //    if (query.Any())
            //    {
            //        UNIT_MAST catDetails = query.First();

            //        entities.UNIT_MAST.Remove(query.First());
            //        entities.SaveChanges();

            //        entities.UNIT_MAST.Add(details);
            //        entities.SaveChanges();
            //    }
            //}

            return true;
        }

        public bool DeleteDetails(string compcode, string usercode )
        {
            //using (entities = new CompuLinEntityModelEntities())
            //{
            //    var query = (from details in entities.UNIT_MAST
            //                 where details.COMPCODE == compcode &&
            //                 details.UNIT_CODE == usercode
            //                 select details);

            //    if (query.Any() )
            //    {
            //        entities.UNIT_MAST.Remove(query.First());
            //        entities.SaveChanges();
            //    }
            //    else
            //        return false;
            //}

            return true;
        }

        
    }
}
