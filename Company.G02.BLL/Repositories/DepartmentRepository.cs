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
    public class DepartmentRepository : IDepartmentRepository
    {

        private readonly CompanyDBContext _context;

        public DepartmentRepository(CompanyDBContext context)
        {
            _context = context;

        }
        public IEnumerable<Department> GetAll()
        {
          

            return _context.departments.ToList();
           
        }
        public Department? Get(int id)
        {
           

            return _context.departments.Find(id);
        }
        public int Add(Department model)
        {
           

             _context.departments.Add(model);

           return _context.SaveChanges();
        }

        public int Update(Department model)
        {
            

            _context.departments.Update(model);

            return _context.SaveChanges();
        }
        public int Delete(Department model)
        {
           

            _context.departments.Remove(model);

            return _context.SaveChanges();

        }



    }
}
