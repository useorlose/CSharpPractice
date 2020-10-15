using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class EmpDataController : Controller
    {
        public ViewResult AllEmployees()
        {
            var context = new MyDatabase();
            var model = context.EmpTables.ToList();
            return View(model);
        }

        public ViewResult Find(string id)
        {
            int empId = int.Parse(id);
            var context = new MyDatabase();
            var model = context.EmpTables.FirstOrDefault((e) => e.EmpID == empId);
            return View(model);

        }
        //ActionResult is the abstract class for all kinds of action returns....
        [HttpPost]
        public ActionResult Find(EmpTables emp)
        {
            var context = new MyDatabase();
            var model = context.EmpTables.FirstOrDefault((e) => e.EmpID == emp.EmpID);
            model.EmpName = emp.EmpName;
            model.EmpAddress = emp.EmpAddress;
            model.EmpSalary = emp.EmpSalary;
            context.SaveChanges();//Commits the changes made to the records...
            return RedirectToAction("AllEmployees");
        }

        public ViewResult NewEmployee()
        {
            var model = new EmpTables();//No Values in it...
            return View(model);
        }

        [HttpPost]
        public ActionResult NewEmployee(EmpTables emp)
        {
            var context = new MyDatabase();
            context.EmpTables.Add(emp);
            context.SaveChanges();
            return RedirectToAction("AllEmployees");
        }

        public ActionResult Delete(string id)
        {
            //convert string to int
            int empId = int.Parse(id);
            var context = new MyDatabase();
            var model = context.EmpTables.FirstOrDefault((e) => e.EmpID == empId);
            context.EmpTables.Remove(model);
            context.SaveChanges();
            return RedirectToAction("AllEmployees");
        }
    }
}