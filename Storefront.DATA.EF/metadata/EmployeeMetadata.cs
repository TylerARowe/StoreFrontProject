using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace Storefront.DATA.EF
{
    [MetadataType(typeof(EmployeeMetadata))]
    public partial class Employee
    {

    }

    public class EmployeeMetadata
    {
        [Required]
        [StringLength(50, ErrorMessage ="Max 50 characters")]
        public int DirectReportID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string LastName { get; set; }

        [Required]
        public int EmployeeID { get; set; }

        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string JobTitle { get; set; }

        [StringLength(10, ErrorMessage = "Max 10 characters")]
        public string TitleOfCourtesy { get; set; }

        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string Address { get; set; }

        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string City { get; set; }

        [StringLength(2, ErrorMessage = "Max 2 characters")]
        public string State { get; set; }

        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string Country { get; set; }

        [Required]
        public int DepartmentID { get; set; }
    }
}
