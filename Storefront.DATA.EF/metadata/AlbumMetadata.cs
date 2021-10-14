using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storefront.DATA.EF
{
    [MetadataType(typeof(AlbumMetadata))]
    public partial class Album
    {

    }

    public class AlbumMetadata
    {
        //[Required]
        //public int AlbumID { get; set; }

        [Required]
        [StringLength(50)]
        public string AlbumTitle { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [Display(Name = "Genre")]
        public int GenreID { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Price { get; set; }


        public Nullable<int> UnitsSold { get; set; }

        [Display(Name = "Publish Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> PublishDate { get; set; }

        [Required]
        [Display(Name = "Record Label")]
        public int RecordLabelID { get; set; }

        [StringLength(100)]
        [Display(Name = "Album Cover")]
        public string AlbumCover { get; set; }

        [Required]
        [Display(Name = "In Stock?")]
        public int AlbumStatusID { get; set; }

        [Required]
        [Display(Name = "Artist")]
        public int ArtistID { get; set; }
    }

}
