using System.Collections.Generic;
using System.Linq;

namespace EmployeesContext
{
    public class EmployeeRep: IRepository<Employee>
    {
        private readonly EmployeeDbContext _context;

        public EmployeeRep(EmployeeDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employees.ToList();
        }
        public void AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }
    }
}
