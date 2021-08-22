using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesContext
{
    public interface IRepository<T>
    {
        public IEnumerable<T> GetEmployees();
        public void AddEmployee(T employee);
    }
}
