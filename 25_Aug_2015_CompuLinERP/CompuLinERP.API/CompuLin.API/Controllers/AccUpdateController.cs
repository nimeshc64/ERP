using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuLinERP.API.Controllers
{
    class AccUpdateController
    {
        CompuLinEntityModelEntities entities;

        public bool InsertAccUpdate(INV_ACC_GLCODES details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.INV_ACC_GLCODES
                             select info);

                entities.INV_ACC_GLCODES.Add(details);
                entities.SaveChanges();
            }

            return true;
        }
    }
}
