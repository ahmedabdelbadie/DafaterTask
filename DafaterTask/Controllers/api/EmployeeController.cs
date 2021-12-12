using DafaterTask.Data;
using DafaterTask.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;





namespace DafaterTask.Controllers.api
{
    
    public class EmployeeController : ApiController
    {
        private IEmployeeRepository EmployeeRepository;

        public EmployeeController()
        {
            this.EmployeeRepository = new EmployeeRepository(new EmployeeEntities());
        }
        public EmployeeController(IEmployeeRepository EmployeeRepository)
        {
            this.EmployeeRepository = EmployeeRepository;
        }

        [HttpGet]
        [Route("api/Employee")]
        public IHttpActionResult GetAllEmployees()
        {

            //var ctx = new CompanyD();
            IList<EmployeeViewModel> employeesModels = new List<EmployeeViewModel>();
            var Employees = from s in EmployeeRepository.GetEmployees()
                           select s;
            
            foreach(var employee in Employees)
            {
                employeesModels.Add(new EmployeeViewModel
                {
                    ID=employee.id,
                    age=employee.age,
                    address=employee.address,
                    familyName = employee.family_name,
                    countryOfOrigin=employee.country_of_origin,
                    email=employee.email,
                    hired=employee.hired,
                    name=employee.name                       
                });
            }
            
            if (employeesModels.Count == 0)
            {
                return NotFound();
            }

            return Ok(employeesModels);
        }
        [HttpGet]
        [Route("api/Employee/{id}")]
        public IHttpActionResult GetEmployeeById(int id)
        {
            Employee employee = EmployeeRepository.GetEmployeeByID(id);
            EmployeeViewModel employeeViewModel = new EmployeeViewModel()
            {
                ID = employee.id,
                age = employee.age,
                address = employee.address,
                familyName = employee.family_name,
                countryOfOrigin = employee.country_of_origin,
                email = employee.email,
                hired = employee.hired,
                name = employee.name
            };
            if (employeeViewModel == null)
            {
                return NotFound();
            }
            return Ok(employeeViewModel);
        }
        [HttpPost]
        [Route("api/Employee")]
        public IHttpActionResult PostNewEmployee([FromBody] EmployeeViewModel Employee)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            Employee employee = CreateEmployee(Employee, EmployeeRepository.GetEmployees().ToList<Employee>().Count +1);
            EmployeeRepository.InsertEmployee(employee);
            EmployeeRepository.Save();
            return Ok();
        }
        [NonAction]
        private Employee CreateEmployee(EmployeeViewModel Employee,int count)
        {
            if(Employee.hired == null) { }
            return new Employee
            {
                id=count+1,
                address = Employee.address,
                email = Employee.email,
                hired = Employee.hired == null ? false : Employee.hired,
                age = Employee.age,
                family_name = Employee.familyName,
                name = Employee.name,
                country_of_origin = Employee.countryOfOrigin

            };

        }
        [HttpPut]
        [Route("api/Employee")]
        public IHttpActionResult Put([FromBody] EmployeeViewModel Employee)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            Employee existingEmployee = EmployeeRepository.GetEmployeeByID(Convert.ToInt32(Employee.ID));


            if (existingEmployee != null)
            {
 
                existingEmployee.address = Employee.address;
                existingEmployee.email = Employee.email;
                existingEmployee.hired = Employee.hired == null ? false : Employee.hired;
                existingEmployee.age = Employee.age;
                existingEmployee.family_name = Employee.familyName;
                existingEmployee.name = Employee.name;
                existingEmployee.country_of_origin = Employee.countryOfOrigin; 


                EmployeeRepository.Save();
            }
            else
            {
                return NotFound();
            }


            return Ok();
        }
        [HttpDelete]
        [Route("api/Employee/{id}")]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");
            EmployeeRepository.DeleteEmployee(id);
            EmployeeRepository.Save();
            

            return Ok();
        }
    }
}
