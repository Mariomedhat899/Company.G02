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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly CompanyDBContext _context;
        public EmployeeRepository(CompanyDBContext context)
        {
            _context = context;
        }
        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }
        public Employee? Get(int id)
        {
           return _context.Employees.Find(id);
        }
        public int Add(Employee employee)
        {
               _context.Employees.Add(employee);
                return _context.SaveChanges();
        }

        public int Update(Employee employee)
        {
            _context.Employees.Update(employee);
            return _context.SaveChanges();
        }
        public int Delete(Employee employee)
        {
            _context.Employees.Remove(employee);
            return _context.SaveChanges();
        }
    }
}
