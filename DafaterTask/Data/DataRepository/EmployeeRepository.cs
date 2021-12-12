using DafaterTask.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace DafaterTask.Data
{
    public class EmployeeRepository : IEmployeeRepository, IDisposable
    {
        private EmployeeEntities context;
        private bool disposed = false;
        public EmployeeRepository(EmployeeEntities context)
        {
            this.context = context;
        }

        public void DeleteEmployee(int EmployeeID)
        {
            Employee student = context.Employees.Find(EmployeeID);
            context.Employees.Remove(student);
        }

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return context.Employees.ToList();
        }

        public Employee GetEmployeeByID(int EmployeeId)
        {
            return context.Employees.Find(EmployeeId);
        }

        public void InsertEmployee(Employee Employee)
        {
            context.Employees.Add(Employee);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateEmployee(Employee Employee)
        {
            context.Employees.Attach(Employee);
            context.Entry(Employee).State = EntityState.Modified;
            //if (context.Entry(Employee).State != EntityState.Modified)
            //{
                

            //}
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}