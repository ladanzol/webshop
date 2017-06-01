using Alb.Tools.Common;
using Alb.Tools.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alb.Omdehsara.Common
{
    public class ProductSearchProperty : Paged
    {
        public int? ProvinceId { get; set; }
        public string City { get; set; }
        public Int64? CategoryID { get; set; }
        public int? FromPrice { get; set; }
        public int? ToPrice { get; set; }
        public int[] SelectedBrands { get; set; }
        public int[] SelectedSizes { get; set; }
        public int[] SelectedColors{ get; set; }
        public IEnumerable<PropertySearch> ProductProperties { get; set; }
    }
}
