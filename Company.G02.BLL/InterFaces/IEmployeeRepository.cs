using Company.G02.DAL.Modles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G02.BLL.InterFaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();

        Employee? Get(int id);


        int Add(Employee employee);

        int Update(Employee employee);


        int Delete(Employee employee);
    }
}
