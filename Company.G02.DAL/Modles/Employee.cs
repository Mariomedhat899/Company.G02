using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G02.DAL.Modles
{
    public class Employee :BaseEntity
    {

        

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime HireDate { get; set; }

        public DateTime CreateAt { get; set; }


        public int? Age { get; set; }

        public string Address { get; set; }

        public decimal Salary { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public string Phone { get; set; }



    }
}
