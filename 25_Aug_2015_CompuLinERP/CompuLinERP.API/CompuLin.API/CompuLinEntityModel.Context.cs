﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CompuLinEntityModelEntities : DbContext
    {
        public CompuLinEntityModelEntities()
            : base("name=CompuLinEntityModelEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ACC_CAT_L1> ACC_CAT_L1 { get; set; }
        public DbSet<ACC_CAT_L2> ACC_CAT_L2 { get; set; }
        public DbSet<ACC_CAT_L3> ACC_CAT_L3 { get; set; }
        public DbSet<ACC_CAT_L4> ACC_CAT_L4 { get; set; }
        public DbSet<ACC_CAT_L5> ACC_CAT_L5 { get; set; }
        public DbSet<CAT_L2> CAT_L2 { get; set; }
        public DbSet<CAT_L3> CAT_L3 { get; set; }
        public DbSet<CAT_L4> CAT_L4 { get; set; }
        public DbSet<CAT_MAST> CAT_MAST { get; set; }
        public DbSet<CUST_MAST> CUST_MAST { get; set; }
        public DbSet<CUSTOMER_LEDGER> CUSTOMER_LEDGER { get; set; }
        public DbSet<DocumentInfo> DocumentInfoes { get; set; }
        public DbSet<F2HELP_MAST> F2HELP_MAST { get; set; }
        public DbSet<GL> GLs { get; set; }
        public DbSet<GRN_DETAIL> GRN_DETAIL { get; set; }
        public DbSet<GRN_MAST> GRN_MAST { get; set; }
        public DbSet<INV_DETAIL> INV_DETAIL { get; set; }
        public DbSet<INV_MAST> INV_MAST { get; set; }
        public DbSet<ITEM_CUTLIST> ITEM_CUTLIST { get; set; }
        public DbSet<ITEM_MAST> ITEM_MAST { get; set; }
        public DbSet<ITEM_SUPP> ITEM_SUPP { get; set; }
        public DbSet<LOCA_MAST> LOCA_MAST { get; set; }
        public DbSet<NAVIGATION> NAVIGATIONs { get; set; }
        public DbSet<PHY_MAST> PHY_MAST { get; set; }
        public DbSet<PRICE_TYPES> PRICE_TYPES { get; set; }
        public DbSet<RECEIPT_DETAIL> RECEIPT_DETAIL { get; set; }
        public DbSet<RECEIPT_INV> RECEIPT_INV { get; set; }
        public DbSet<RECEIPT_MAST> RECEIPT_MAST { get; set; }
        public DbSet<STOCK> STOCKs { get; set; }
        public DbSet<SUPP_MAST> SUPP_MAST { get; set; }
        public DbSet<TAX_MAST> TAX_MAST { get; set; }
        public DbSet<TRANS_DETAIL> TRANS_DETAIL { get; set; }
        public DbSet<TRANS_MAST> TRANS_MAST { get; set; }
        public DbSet<TXN_TYPES> TXN_TYPES { get; set; }
        public DbSet<UNIT_MAST> UNIT_MAST { get; set; }
        public DbSet<USERINFO> USERINFOes { get; set; }
        public DbSet<ITEM_MAST_003> ITEM_MAST_003 { get; set; }
        public DbSet<ITEM_MAST_004> ITEM_MAST_004 { get; set; }
        public DbSet<NETWORK> NETWORKs { get; set; }
        public DbSet<LOCA_DETAIL> LOCA_DETAIL { get; set; }
        public DbSet<INV_ACC_GLCODES> INV_ACC_GLCODES { get; set; }
    }
}
