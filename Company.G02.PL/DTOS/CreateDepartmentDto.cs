using System.ComponentModel.DataAnnotations;

namespace Company.G02.PL.DTOS
{
    public class CreateDepartmentDto
    {

        [Required(ErrorMessage = "Code is required !")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is required !")]

        public string Name { get; set; }

        [Required(ErrorMessage = "CreateAt is required !")]

        public DateTime CreateAt { get; set; }
    }
}
