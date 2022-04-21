using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class EmployeeTestController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var httpClient = new HttpClient();
            var responseMessage = await httpClient.GetAsync("https://localhost:44303/api/Default");
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<Class1>>(jsonString) ;
            return View(values);
        }

        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddEmployee(Class1 employee)
        {
            var httpClient = new HttpClient();
            var jsonEmployee = JsonConvert.SerializeObject(employee);
            StringContent content = new StringContent(jsonEmployee,Encoding.UTF8,"application/json");
            var responseMessage = await httpClient.PostAsync("https://localhost:44303/api/Default", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> EditEmployee(int id)
        {
            var httpClient = new HttpClient();
            var responseMessage=await httpClient.GetAsync("https://localhost:44303/api/Default/"+ id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonEmployee = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<Class1>(jsonEmployee);
                return View(values);
            }
                return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployee(Class1 employee)
        {
            var httpClient = new HttpClient();
            var jsonEmployee = JsonConvert.SerializeObject(employee);
            var content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PutAsync("https://localhost:44303/api/Default ", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(employee);
        }


        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var httpClient = new HttpClient();
            var responseMessage = await httpClient.DeleteAsync("https://localhost:44303/api/Default/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
    public class Class1
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
