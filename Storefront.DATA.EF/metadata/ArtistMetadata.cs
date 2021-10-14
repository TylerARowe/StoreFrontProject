using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storefront.DATA.EF
{
    [MetadataType(typeof(ArtistMetadata))]
    public partial class Artist
    {
        [Display(Name = "Artist")]
        public string FullName { get { return FirstName + " " + LastName; } }
    }

    public class ArtistMetadata
    {
        [Required]
        [StringLength(50, ErrorMessage = "Max 50 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Max 50 characters.")]
        public string LastName { get; set; }

        [StringLength(2, ErrorMessage = "Max 2 characters.")]
        public string State { get; set; }

        [StringLength(6, ErrorMessage = "Max 6 characters.")]
        public string ZipCode { get; set; }

        [StringLength(50, ErrorMessage = "Max 50 characters.")]
        public string Country { get; set; }

        [StringLength(10, ErrorMessage = "Max 10 characters.")]
        public string Gender { get; set; }

        [Required]
        public int ArtistID { get; set; }
    }
}
