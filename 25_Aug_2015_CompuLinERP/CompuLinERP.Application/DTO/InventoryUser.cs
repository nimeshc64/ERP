using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompuLinINV.WIN.DTO
{
    public class InventoryUser
    {
        private string _fullName;              
        private string _username;
        private string _password;
        private string _rightCode;
        private string _userLevel;
        private Company _company;
        private string _locations;
        

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

        public string Password
        {
            get { return this._password; }
            set { this._password = value; }
        }

        public string RightCode
        {
            get { return _rightCode; }
            set { _rightCode = value; }
        }

        public string UserLevel
        {
            get { return _userLevel; }
            set { _userLevel = value; }
        }  

        public Company Company
        {
            get { return this._company; }
            set { this._company = value; }
        }

        public string Locations
        {
            get { return _locations; }
            set { _locations = value; }
        }

    }
}
