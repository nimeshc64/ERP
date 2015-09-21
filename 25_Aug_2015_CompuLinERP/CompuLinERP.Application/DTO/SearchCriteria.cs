using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompuLinINV.WIN.DTO
{
    public class SearchCriteria
    {
        private string _tableName;

        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }
        private string _companyCode;

        public string CompanyCode
        {
            get { return _companyCode; }
            set { _companyCode = value; }
        }
        private string _location;

        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }
        private int _sequenceNo;

        public int SequenceNo
        {
            get { return _sequenceNo; }
            set { _sequenceNo = value; }
        }
        
        private string _searchStartingCharacters;
        public string SearchStartingCharacters
        {
            get { return _searchStartingCharacters; }
            set { _searchStartingCharacters = value; }
        }

      

    }
}
