using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuLinERP.API.Controllers
{
    public class ItemCutListController
    {
        CompuLinEntityModelEntities entities;

        public bool InsertNewDetails(ITEM_CUTLIST details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.ITEM_CUTLIST
                             select info);

                entities.ITEM_CUTLIST.Add(details);
                entities.SaveChanges();
            }

            return true;
        }

        public List<ITEM_CUTLIST> GetItemCustList(ITEM_CUTLIST searchDetails)
        {
            List<ITEM_CUTLIST> details = new List<ITEM_CUTLIST>();

            //Get All details
                using (entities = new CompuLinEntityModelEntities())
                {
                    var query = (from info in entities.ITEM_CUTLIST
                                 where info.COMPCODE == searchDetails.COMPCODE &&
                                 info.ITEM  == searchDetails.ITEM && 
                                 info.LOCA_CODE == searchDetails.LOCA_CODE 
                                 select info);

                    if (query.Any())
                        details = query.ToList();

                }
            
            return details;
        }

        public bool DeleteDetails(ITEM_CUTLIST deleteDetails)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from details in entities.ITEM_CUTLIST
                             where details.COMPCODE == deleteDetails.COMPCODE  &&
                             details.LOCA_CODE == deleteDetails.LOCA_CODE && details.ITEM == deleteDetails.ITEM && details.CUT_ITEM == deleteDetails.CUT_ITEM 
                             select details);

                if (query.Any())
                {
                    entities.ITEM_CUTLIST.Remove(query.First());
                    entities.SaveChanges();
                }
                else
                    return false;
            }

            return true;
        }

    }

}
