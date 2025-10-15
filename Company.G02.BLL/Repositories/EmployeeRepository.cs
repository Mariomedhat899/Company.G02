using Company.G02.BLL.InterFaces;
using Company.G02.DAL.Data.Contexts;
using Company.G02.DAL.Modles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G02.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly CompanyDBContext _context;

        public EmployeeRepository(CompanyDBContext context): base(context)
        {
            _context = context;
        }

        public List<Employee> GetByName(string name)
        {
          return _context.Employees.Include(D => D.Department).Where(e => e.Name.ToLower().Contains(name.ToLower())).ToList();
        }
    }
}
