using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuLinERP.API.Controllers
{
    public class ItemSupplierController
    {
        CompuLinEntityModelEntities entities;

        public bool InsertNewSupplier(List<ITEM_SUPP> collectionDetails)
        {
            foreach (var details in collectionDetails)
            {
                 using (entities = new CompuLinEntityModelEntities())
                    {
                        var query = (from info in entities.ITEM_SUPP                                    
                                     select info);
                       
                        details.Last_Change_Date = DateTime.Now;

                        entities.ITEM_SUPP.Add(details);
                        entities.SaveChanges();
                    }
                
            }            

            return true;
        }

        public bool DeleteAllSuppliersByItem(ITEM_SUPP item)
        {
           
                using (entities = new CompuLinEntityModelEntities())
                {
                    var query = (from details in entities.ITEM_SUPP
                                 where details.COMPCODE == item.COMPCODE &&
                                 details.LOCA_CODE == item.LOCA_CODE &&                                 
                                 details.ITEMCODE == item.ITEMCODE
                                 select details);

                    if (query.Any())
                    {
                        entities.ITEM_SUPP.Remove(query.First());
                        entities.SaveChanges();
                    }
                }
            

            return true;
        }

        public bool DeleteSupplier(List<ITEM_SUPP> collectionDetails)
        {
            foreach (ITEM_SUPP item in collectionDetails)
            {
                using (entities = new CompuLinEntityModelEntities())
                {
                    var query = (from details in entities.ITEM_SUPP
                                 where details.COMPCODE == item.COMPCODE &&
                                 details.LOCA_CODE == item.LOCA_CODE &&
                                 details.SUPP_CODE == item.SUPP_CODE &&
                                 details.ITEMCODE == item.ITEMCODE
                                 select details);

                    if (query.Any())
                    {
                        entities.ITEM_SUPP.Remove(query.First());
                        entities.SaveChanges();
                    }                   
                }
            }           

            return true;
        }

        public bool UpdateSupplierByItemId(ITEM_SUPP searchdetails, List<ITEM_SUPP> collectionDetails)
        {
            foreach (var details in collectionDetails)
            {
                using (entities = new CompuLinEntityModelEntities())
                {
                    var query = (from logInfo in entities.ITEM_SUPP
                                 where details.COMPCODE == searchdetails.COMPCODE &&
                                 details.LOCA_CODE == searchdetails.LOCA_CODE &&
                                 details.SUPP_CODE == searchdetails.SUPP_CODE &&
                                 details.ITEMCODE == searchdetails.ITEMCODE
                                 select logInfo);

                    if (query.Any())
                    {
                        ITEM_SUPP catDetails = query.First();
                        catDetails.SUPP_CODE = details.SUPP_CODE;
                        catDetails.P_PRICE = details.P_PRICE;                       

                        entities.SaveChanges();
                    }
                }
            }

            return true;
        }

        public bool DeleteSupplierByItemId(ITEM_SUPP searchdetails)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from details in entities.ITEM_SUPP
                             where details.COMPCODE == searchdetails.COMPCODE &&
                                 details.LOCA_CODE == searchdetails.LOCA_CODE &&
                                 details.SUPP_CODE == searchdetails.SUPP_CODE &&
                                 details.ITEMCODE == searchdetails.ITEMCODE
                             select details);

                if (query.Any())
                {
                    List<ITEM_SUPP> list = query.ToList();

                    foreach (var item in list)
                    {
                        entities.ITEM_SUPP.Remove(item);
                        entities.SaveChanges();
                    }
                }
                else
                    return false;
            }

            return true;
        }

        public List<ITEM_SUPP> GetAllSupplierDetailsByItemId(ITEM_SUPP searchdetails)
        {
            List<ITEM_SUPP> details = new List<ITEM_SUPP>();

            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.ITEM_SUPP
                             where info.COMPCODE == searchdetails.COMPCODE &&
                                 info.LOCA_CODE == searchdetails.LOCA_CODE &&
                                 info.ITEMCODE == searchdetails.ITEMCODE
                                 select info);

                    if (query.Any())
                        details = query.ToList();                
            }

            return details;
        }

       
    }
}
