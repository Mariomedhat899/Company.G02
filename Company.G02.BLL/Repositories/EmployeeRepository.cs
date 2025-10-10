using Company.G02.BLL.InterFaces;
using Company.G02.DAL.Data.Contexts;
using Company.G02.DAL.Modles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G02.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(CompanyDBContext context): base(context)
        {
            
        }

    }
}
