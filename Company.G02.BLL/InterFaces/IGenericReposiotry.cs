using Company.G02.DAL.Modles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G02.BLL.InterFaces
{
    public interface IGenericReposiotry<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();

        T? Get(int id);


        int Add(T model);

        int Update(T model);


        int Delete(T model);
    }
}
