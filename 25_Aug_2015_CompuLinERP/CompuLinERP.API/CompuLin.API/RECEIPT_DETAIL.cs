//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CompuLinERP.API
{
    using System;
    using System.Collections.Generic;
    
    public partial class RECEIPT_DETAIL
    {
        public string COMPCODE { get; set; }
        public string REC_NO { get; set; }
        public System.DateTime REC_DATE { get; set; }
        public string REFNO { get; set; }
        public string REFSYS { get; set; }
        public string MODE_ { get; set; }
        public string REC_DESC { get; set; }
        public decimal AMOUNT { get; set; }
        public string DEPBANK { get; set; }
        public int POSTED { get; set; }
        public int CANPOSTED { get; set; }
        public int ISREBANK { get; set; }
        public Nullable<System.DateTime> CHQ_DATE { get; set; }
        public string DIRECT_SETTLE_TIME { get; set; }
    }
}