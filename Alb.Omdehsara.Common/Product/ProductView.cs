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

    public partial class ProductView : TblProduct
    {
        public ProductView()
        {

        }
        public string ProductTitle { get; set; }
        /// <summary>
        /// Email of user who created this product
        /// </summary>
        public string Email { get; set; }
        public string ColorType { get; set; }
        public string ColorName { get;set;}
        public string SizeType { get; set; }
        public string SizeName { get; set; }
        public int Price { get; set; }

    }
}