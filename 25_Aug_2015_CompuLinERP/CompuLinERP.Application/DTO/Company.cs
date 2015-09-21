using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompuLinINV.WIN.DTO
{
    public class Company
    {
        private string _name;
        private string _email;
        private string _code;
        

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }


        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

    }
}
