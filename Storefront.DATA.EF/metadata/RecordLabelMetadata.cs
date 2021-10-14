using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storefront.DATA.EF
{ 
    [MetadataType(typeof(RecordLabelMetadata))]
    public partial class RecordLabel
    {

    }

    public class RecordLabelMetadata
    {
        [Required]
        public int RecordLabelID { get; set; }

        [Required]
        [Display(Name = "Record Label")]
        [StringLength(50, ErrorMessage = "Max 50 characters.")]
        public string RecordLabelName { get; set; }

        [StringLength(50, ErrorMessage = "Max 50 characters.")]
        public string City { get; set; }

        [StringLength(2, ErrorMessage = "Max 2 characters.")]
        public string State { get; set; }

        public Nullable<bool> IsActive { get; set; }

    }
}