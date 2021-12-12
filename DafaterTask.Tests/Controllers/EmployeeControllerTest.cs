using DafaterTask.Controllers.api;
using DafaterTask.Data;
using DafaterTask.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DafaterTask.Tests.Controllers
{


    [TestClass]
    public class EmployeeControllerTest
    {
        public static HttpClient TestHttpClient;
        public static Mock<IEmployeeRepository> MockEmployeeRepository;


        [TestMethod]
        public void TestGetAllEmployees()
        {
            var mockEmps = new List<Employee>();
            mockEmps.Add(new Employee()
            {
                id = 1,
                address = "TestAddress",
                age = 23,
                country_of_origin = "Egypt",
                email = "a@dafater.com",
                family_name = "familyName",
                hired = false,
                name = "name"
            });
            mockEmps.Add(new Employee()
            {
                id = 2,
                address = "TestAddress",
                age = 23,
                country_of_origin = "Egypt",
                email = "a@dafater.com",
                family_name = "familyName",
                hired = false,
                name = "name"
            });

            Mock<IEmployeeRepository> MockEmployeeRepository = new Mock<IEmployeeRepository>();
            MockEmployeeRepository.Setup(x => x.GetEmployees()).Returns(mockEmps);

            dynamic response = new EmployeeController(MockEmployeeRepository.Object).GetAllEmployees() ;
            Assert.AreEqual(2, response.Content.Count);
            Assert.AreEqual(mockEmps[0].id, response.Content[0].ID);
        }
        [TestMethod]
        public void TestGetbyidEmployee()
        {
            var mockEmps = new List<Employee>();
            mockEmps.Add(new Employee()
            {
                id = 1,
                address = "TestAddress",
                age = 23,
                country_of_origin = "Egypt",
                email = "a@dafater.com",
                family_name = "familyName",
                hired = false,
                name = "name1"
            });
            mockEmps.Add(new Employee()
            {
                id = 2,
                address = "TestAddress",
                age = 23,
                country_of_origin = "Egypt",
                email = "a@dafater.com",
                family_name = "familyName",
                hired = false,
                name = "name"
            });

            Mock<IEmployeeRepository> MockEmployeeRepository = new Mock<IEmployeeRepository>();
            MockEmployeeRepository.Setup(x => x.GetEmployeeByID(1)).Returns(new Employee()
            {
                id = 1,
                address = "TestAddress",
                age = 23,
                country_of_origin = "Egypt",
                email = "a@dafater.com",
                family_name = "familyName",
                hired = false,
                name = "name1"
            });

            dynamic response = new EmployeeController(MockEmployeeRepository.Object).GetEmployeeById(1);
            Assert.AreEqual(mockEmps[0].name, response.Content.name);
        }
        [TestMethod]
        public void TestCreatemployeeWithInvalidCharacLength()
        {
            var mockEmps = new List<Employee>();
            mockEmps.Add(new Employee()
            {
                id = 1,
                address = "TestAddress",
                age = 23,
                country_of_origin = "Egypt",
                email = "a@dafater.com",
                family_name = "familyName",
                hired = false,
                name = "name"
            });
            mockEmps.Add(new Employee()
            {
                id = 2,
                address = "TestAddress",
                age = 23,
                country_of_origin = "Egypt",
                email = "a@dafater.com",
                family_name = "familyName",
                hired = false,
                name = "name"
            });


            
            var mockEmp = new EmployeeViewModel()
            {
                ID = 1,
                address = "TestAddress",
                age = 23,
                countryOfOrigin = "Egypt",
                email = "a@dafater.com",
                familyName = "familyName",
                hired = false,
                name = "name1"
            };
            

            Mock<IEmployeeRepository> MockEmployeeRepository = new Mock<IEmployeeRepository>();
            MockEmployeeRepository.Setup(x => x.GetEmployees()).Returns(mockEmps);
            MockEmployeeRepository.Setup(x => x.InsertEmployee(new Employee() { 
                id= mockEmps.Count+1,
                name =mockEmp.name,
                address=mockEmp.address,
                age = mockEmp.age,
                country_of_origin=mockEmp.countryOfOrigin,
                email=mockEmp.email,
                family_name=mockEmp.familyName,
                hired=mockEmp.hired
           } ));
            MockEmployeeRepository.Setup(x => x.Save());
            dynamic response = new EmployeeController(MockEmployeeRepository.Object).PostNewEmployee(mockEmp);
            Assert.IsNotNull(response);
        }

    }
}
