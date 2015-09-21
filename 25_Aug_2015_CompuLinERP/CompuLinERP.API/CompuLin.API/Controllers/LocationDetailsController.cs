using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuLinERP.API.Controllers
{
    public class LocationDetailsController
    {
        CompuLinEntityModelEntities entities;

        public bool InsertNewDetails(LOCA_MAST details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.LOCA_MAST                             
                             select info);

                entities.LOCA_MAST.Add(details);
                entities.SaveChanges();
            }

            return true;
        }

        public bool UpdateDetails(LOCA_MAST searchdetails, LOCA_MAST details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from logInfo in entities.LOCA_MAST
                             where logInfo.COMPCODE == searchdetails.COMPCODE &&
                             logInfo.LOCA_CODE == searchdetails.LOCA_CODE 
                             select logInfo);

                if (query.Any())
                {
                    entities.LOCA_MAST.Remove(query.First());
                    entities.SaveChanges();

                    entities.LOCA_MAST.Add(details);
                    entities.SaveChanges();
                }
            }

            return true;
        }

        public string GetNextDocumentNumber(LOCA_MAST searchdetails)
        {
            string documentNumber=null;
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from logInfo in entities.LOCA_MAST
                             where logInfo.COMPCODE == searchdetails.COMPCODE &&
                             logInfo.LOCA_CODE == searchdetails.LOCA_CODE
                             select logInfo);

                if (query.Any())
                {
                    LOCA_MAST details = query.ToList().First();
                    int docId = int.Parse( details.PHY_NO);
                    docId++;
                    documentNumber = docId.ToString(); 
                    details.PHY_NO = (documentNumber).ToString();
                    
                    entities.SaveChanges();                    
                }
            }

            return documentNumber.ToString();
        }

        public string GetNextInvoiceNumber(LOCA_MAST searchdetails)
        {
            string documentNumber = null;
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from logInfo in entities.LOCA_MAST
                             where logInfo.COMPCODE == searchdetails.COMPCODE &&
                             logInfo.LOCA_CODE == searchdetails.LOCA_CODE
                             select logInfo);

                if (query.Any())
                {
                    LOCA_MAST details = query.ToList().First();
                    int docId = int.Parse(details.INV_NO);
                    docId++;
                    documentNumber = docId.ToString();
                    details.INV_NO = (documentNumber).ToString();

                    entities.SaveChanges();
                }
            }

            return documentNumber.ToString();
        }

        public bool DeleteDetails(string code, string compCode)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from details in entities.LOCA_MAST
                             where details.COMPCODE == compCode &&
                             details.LOCA_CODE == code
                             select details);

                if (query.Any() )
                {
                    entities.LOCA_MAST.Remove(query.First());
                    entities.SaveChanges();
                }
                else
                    return false;
            }

            return true;
        }

        public List<LOCA_MAST> GetAllDetails(string netId)
        {
            List<LOCA_MAST> details = new List<LOCA_MAST>();

            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.LOCA_MAST
                             where info.COMPCODE == netId   
                                 select info);

                    if (query.Any())
                        details = query.ToList();  
            }

            return details;
        }
        
        public List<LOCA_MAST> GetAllDetailsForLoginUser(string netId, string allowedLocations)
        {
            List<LOCA_MAST> details = new List<LOCA_MAST>();

            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.LOCA_MAST
                             where info.COMPCODE == netId 
                             select info);
                //&& info.LOCA_CODE.Contains(allowedLocations)
                if (query.Any())
                    details = query.ToList();
            }

            return details;
        }


        public LOCA_MAST GetAllDetailsById(string netId, string locaCode)
        {
            LOCA_MAST details = new LOCA_MAST();

            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.LOCA_MAST
                             where info.COMPCODE == netId &&
                             info.LOCA_CODE == locaCode
                             select info);

                if (query.Any())
                    details = query.ToList().First();
            }

            return details;
        }

    }
}
