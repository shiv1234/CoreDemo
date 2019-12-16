using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;
        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee() {Id = 1,Name="Shiv",Email = "shiv@gmail.com",Department= Dept.Admin},
                //new Employee() {Id = 2,Name="Sunny",Email = "sunny@gmail.com",Department="Store"},
                new Employee() {Id = 3,Name="Gunjan",Email = "gunjan@gmail.com",Department=Dept.HR},
                new Employee() {Id = 4,Name="Rajawat",Email = "rajawat@gmail.com",Department=Dept.Support}
            };

        }

        public Employee AddEmployee(Employee employee)
        {
            var employeeId = _employeeList.Max(x => x.Id) + 1;
            employee.Id = employeeId;
             _employeeList.Add(employee);
            return employee;
        }

        public Employee DeleteEmployee(int id)
        {
            var employee =_employeeList.FirstOrDefault(x => x.Id == id);
            if (employee != null)
            {
                _employeeList.Remove(employee);
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(x => x.Id == Id);
        }

        public Employee UpdateEmployee(Employee employeeChanges)
        {
            var employee = _employeeList.FirstOrDefault(x => x.Id == employeeChanges.Id);
            if (employee != null)
            {
                employee.Name = employeeChanges.Name;
                employee.Department = employeeChanges.Department;
                employee.Email = employeeChanges.Email;
            }
            return employee;
        }
    }
}
