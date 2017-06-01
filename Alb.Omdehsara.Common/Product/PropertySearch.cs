
namespace Alb.Omdehsara.Common
{
    using System;
    using System.Collections.Generic;

    public class PropertySearch 
    {
        public int PropertyID { get; set; }
        public string PropertyValue { get; set; }
        public string PropertyType { get; set; }
        public IEnumerable<TblConstant> SelectedConstants { get; set; }
    }
}
