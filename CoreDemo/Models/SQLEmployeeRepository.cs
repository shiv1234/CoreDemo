using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Models
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDBContext dbContext;
        public SQLEmployeeRepository(AppDBContext appDBContext)
        {
            this.dbContext = appDBContext;
        }

        public Employee AddEmployee(Employee employee)
        {
            dbContext.Employees.Add(employee);
            dbContext.SaveChanges();
            return employee;
        }

        public Employee DeleteEmployee(int id)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee != null)
            {
                dbContext.Employees.Remove(employee);
                dbContext.SaveChanges();
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return dbContext.Employees;
        }

        public Employee GetEmployee(int Id)
        {
            return dbContext.Employees.Find(Id);
        }

        public Employee UpdateEmployee(Employee employeeChanges)
        {
            var employee = dbContext.Employees.Attach(employeeChanges);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dbContext.SaveChanges();
            return employeeChanges;
        }
    }
}
