using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace Storefront.DATA.EF
{
    [MetadataType(typeof(OtherProductMetadata))]
    public partial class OtherProduct
    {

    }

    public class OtherProductMetadata
    {
        [Required]
        public int ProdutID { get; set; }
        
        [Required]
        [Display(Name = "Product")]
        [StringLength(500, ErrorMessage = "Max 50 characters")]
        public string ProductName { get; set; }

        [StringLength(500, ErrorMessage = "Max 500 characters")]
        public string Description { get; set; }

        public Nullable<decimal> Price { get; set; }
    }
}