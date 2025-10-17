using Company.G02.BLL.InterFaces;
using Company.G02.BLL.Repositories;
using Company.G02.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G02.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyDBContext context;

        public IDepartmentRepository DepartmentRepository { get; }

        public IEmployeeRepository EmployeeRepository { get; }

        public UnitOfWork(CompanyDBContext _context)
        {
            context = _context;

            DepartmentRepository = new DepartmentRepository(context);

            EmployeeRepository = new EmployeeRepository(context);
        }

        public async Task<int> CompleteAsync()
        {
           return await context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
           await context.DisposeAsync();
        }
    }
}
