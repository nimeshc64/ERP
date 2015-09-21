using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuLinERP.API.Controllers
{
    public class HelpMasterController
    {
        CompuLinEntityModelEntities entities;

        public F2HELP_MAST GetAllDetailsBySeqNo(int seqNo)
        {
            F2HELP_MAST details = new F2HELP_MAST();

            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.F2HELP_MAST
                             where info.SEQ_NO == seqNo   
                                 select info);

                    if (query.Any())
                        details = query.ToList().First();  
            }

            return details;
        }

        
    }
}
