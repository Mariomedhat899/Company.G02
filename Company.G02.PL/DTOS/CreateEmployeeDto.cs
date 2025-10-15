

using Newtonsoft.Json.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.G02.PL.DTOS
{
    public class CreateEmployeeDto
    {

        [Required(ErrorMessage = "Name is required !")]
        public string Name { get; set; }

        [Range(22, 60, ErrorMessage = "Age must be between 22 and 60")]
        public int? Age { get; set; }
        [Required(ErrorMessage = "Email is required !")]
        [DataType(DataType.EmailAddress, ErrorMessage  = "Email Is Not Valid !") ]
        public string Email { get; set; }

        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",ErrorMessage = "Address Must be Like 12-Street-Cairo-Egypt")]
        public string Address { get; set; }

        [Phone(ErrorMessage = "Phone Number Is Not Valid")]
        public string Phone { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [DisplayName("Hiring Date")]
        public DateTime HireDate { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [DisplayName("Date Of Creation")]
        public DateTime CreateAt { get; set; }

        [DisplayName("Departments")]
        public int? DepartmentId { get; set; }




    }
}
