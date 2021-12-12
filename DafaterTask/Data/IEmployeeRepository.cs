using DafaterTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DafaterTask.Data
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployeeByID(int EmployeeId);
        void InsertEmployee(Employee Employee);
        void DeleteEmployee(int EmployeeID);
        void UpdateEmployee(Employee Employee);
        void Save();
    }
}
