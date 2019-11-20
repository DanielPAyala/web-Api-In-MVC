using Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            IEnumerable<mvcEmployeeModel> empList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Employees").Result;
            empList = response.Content.ReadAsAsync<IEnumerable<mvcEmployeeModel>>().Result;
            return View(empList);
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new mvcEmployeeModel());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Employees/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcEmployeeModel>().Result);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(mvcEmployeeModel obj)
        {
            if (obj.EmployeeID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Employees", obj).Result;
                TempData["SuccessMessage"] = "Saved successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Employees/" + obj.EmployeeID, obj).Result;
                TempData["SuccessMessage"] = "Updated successfully";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Employees/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted successfully";
            return RedirectToAction("Index");
        }
    }
}