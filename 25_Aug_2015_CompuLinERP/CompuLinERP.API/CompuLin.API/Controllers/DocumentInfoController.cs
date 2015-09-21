using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuLinERP.API.Controllers
{
   public class DocumentInfoController
    {
       CompuLinEntityModelEntities entities;

       public string InsertNewDoc(DocumentInfo docInfo)
       {
           bool status=true;

           while (status)
           {
               try
               {
                   using (entities = new CompuLinEntityModelEntities())
                   {
                       var query = (from logInfo in entities.DocumentInfoes
                                    orderby logInfo.Unik descending
                                    select logInfo);

                       if (query.Any())
                       {
                           docInfo.Unik = query.First().Unik + 1;
                           docInfo.DocumentNumber = docInfo.DocumentNumber + docInfo.Unik.ToString().PadLeft(5,'0');
                       }
                       else
                       {
                           docInfo.Unik = 1;
                           docInfo.DocumentNumber = docInfo.DocumentNumber + docInfo.Unik.ToString().PadLeft(5, '0');
                       }

                       entities.DocumentInfoes.Add(docInfo);
                       entities.SaveChanges();
                       status = false;
                   }
               }
               catch(Exception ex)
               {
                   continue;
               }
           }

           return docInfo.DocumentNumber;
       }

       public DocumentInfo GetAllDocInfo()
       {
           DocumentInfo docInfo = new DocumentInfo();

           using (entities = new CompuLinEntityModelEntities())
           {
               var query = (from logInfo in entities.DocumentInfoes
                            select logInfo);

               if (query.Any())
                   docInfo = query.ToList().First();
           }

           return docInfo;
       }
    }
}
