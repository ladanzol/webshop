//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Alb.Omdehsara.Common.Product
{
    using System;
    using System.Collections.Generic;

    public partial class ProductLocation
    {
        public ProductLocation()
        {
        }
        public long ProductTypeID { get; set; }
        public long ProductID { get; set; }
        public int ProductQuantity { get; set; }
        public long CategoryID { get; set; }
        public string ProductTitle { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public int? StoreID { get; set; }
        public string StoreTitle { get; set; }
        public int? ShelfID { get; set; }
        public string ShelfCode { get; set; }
        public string Row { get; set; }
        public string Column { get; set; }
        public int Quantity { get; set; }
        public int ColorID { get; set; }
        public string ColorName { get; set; }
        public int SizeID { get; set; }
        public string SizeName { get; set; }
        public string Email { get; set; }
        public long OrderID { get; set; }
        public DateTime? InsDate { get; set; }
        public string InsDatePersian { get; set; }
    }
}
