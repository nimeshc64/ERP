using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuLinERP.API.Controllers
{
    public class UserInfoController
    {
        CompuLinEntityModelEntities entities;

        public bool IsValidUser(string username, string password, string companyId)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from logInfo in entities.USERINFOes
                             where logInfo.USERCODE==username && logInfo.COMPCODE==companyId && logInfo.Password==password                             
                             select logInfo);

                if (query.Any())
                    return true;
                else
                    return false;
            }
        }

        public USERINFO GetUserDetails(USERINFO user)
        {
            USERINFO userDetails = new USERINFO();

            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from logInfo in entities.USERINFOes
                             where logInfo.USERCODE == user.USERCODE && logInfo.COMPCODE == user.COMPCODE
                             select logInfo);

                if (query.Any())
                    userDetails = query.First();                
            }

            return userDetails;
        }

        public List<USERINFO> GetAllUserDetailsByCompany(string networkId)
        {
            List<USERINFO> userDetails = new List<USERINFO>();

            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from logInfo in entities.USERINFOes
                             where logInfo.COMPCODE == networkId
                             select logInfo);

                if (query.Any())
                    userDetails = query.ToList();
            }

            return userDetails;
        }

        public bool InsertNewUser(USERINFO user)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from logInfo in entities.USERINFOes                            
                             select logInfo);

                entities.USERINFOes.Add(user);
                entities.SaveChanges();
            }

            return true;
        }

        public bool UpdateUser(USERINFO searchUser, USERINFO updatedDetails)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from logInfo in entities.USERINFOes
                             where logInfo.USERCODE == searchUser.USERCODE && logInfo.COMPCODE == searchUser.COMPCODE
                             select logInfo);

                if (query.Any())
                {
                    USERINFO userDetails = query.First();
                    userDetails.Address = updatedDetails.Address;
                    userDetails.Birthday = updatedDetails.Birthday;
                    userDetails.DepartmentCode = updatedDetails.DepartmentCode;
                    userDetails.DesignationCode = updatedDetails.DesignationCode;
                    userDetails.FullName = updatedDetails.FullName;
                    userDetails.COMPCODE = updatedDetails.COMPCODE;
                    userDetails.NIC = updatedDetails.NIC;
                    userDetails.Password = updatedDetails.Password;
                    userDetails.RightCodes = updatedDetails.RightCodes;
                    userDetails.USERCODE = updatedDetails.USERCODE;
                    userDetails.UserLevel = updatedDetails.UserLevel;

                    entities.SaveChanges();
                }
            }

            return true;
        }

        public bool UpdateUserPassword(USERINFO searchUser, string newpwd)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from logInfo in entities.USERINFOes
                             where logInfo.USERCODE == searchUser.USERCODE && logInfo.COMPCODE == searchUser.COMPCODE
                             select logInfo);

                if (query.Any())
                {
                    USERINFO userDetails = query.First();
                    userDetails.Password = newpwd;                  

                    entities.SaveChanges();
                }
            }

            return true;
        }
        public bool DeleteUser(USERINFO searchUser)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from logInfo in entities.USERINFOes
                             where logInfo.USERCODE == searchUser.USERCODE && logInfo.COMPCODE == searchUser.COMPCODE
                             select logInfo);

                if (query.Any())
                {
                    entities.USERINFOes.Remove(query.First());                    
                    entities.SaveChanges();
                }
            }

            return true;
        }
    }
}
