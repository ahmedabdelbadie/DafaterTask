using DafaterTask.Data;
using DafaterTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DafaterTask.Controllers
{
    public class EmployeeController : Controller
    {
       
        // GET: Employee
        public ActionResult Index()
        {
            IEnumerable<EmployeeViewModel> Employees = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44305/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Employee");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<EmployeeViewModel>>();
                    readTask.Wait();

                    Employees = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    Employees = Enumerable.Empty<EmployeeViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View("Index", Employees);
        }
        public ActionResult create()
        {
            return View("create");
        }
        [HttpPost]
        public ActionResult create(EmployeeViewModel employeeModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44305/api/Employee");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<EmployeeViewModel>("Employee", employeeModel);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View("Create", employeeModel);
        }

        public ActionResult Edit(int id)
        {
            EmployeeViewModel employeeModel = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44305/api/Employee/");
                //HTTP GET
                var responseTask = client.GetAsync(id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<EmployeeViewModel>();
                    readTask.Wait();

                    employeeModel = readTask.Result;
                }
            }
            return View(employeeModel);
        }
        public ActionResult Details(int id)
        {
            EmployeeViewModel employeeModel = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44305/api/Employee/");
                //HTTP GET
                var responseTask = client.GetAsync(id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<EmployeeViewModel>();
                    readTask.Wait();

                    employeeModel = readTask.Result;
                }
            }
            return View(employeeModel);
        }
        [HttpPost]
        public ActionResult Edit(EmployeeViewModel employeeModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44305/api/Employee");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<EmployeeViewModel>("Employee", employeeModel);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(employeeModel);
        }
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44305/api/Employee/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync(id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

    }
}