using C__MVC_Assessment.Models;
using C__MVC_Assessment.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace C__MVC_Assessment.Controllers
{
    public class EmployeesController : BaseController
    {
        public ActionResult Index()
        {
            var employees = Context.Employees
                .Include(e  => e.Company)
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToList();

            return View(employees);
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var employee = Context.Employees
                .Where(e => e.Id == (int)id)
                .Include(e => e.Company)
                .SingleOrDefault();

            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }
        
        public ActionResult Add()
        {
            var viewModel = new EmployeeAddViewModel();

            viewModel.Init(Context);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(EmployeeAddViewModel viewModel)
        {
            ValidateEmployee(viewModel.Employee);

            if (ModelState.IsValid)
            {
                var employee = viewModel.Employee;

                Context.Employees.Add(employee);
                Context.SaveChanges();

                TempData["Message"] = "Employee created successfully";

                return RedirectToAction("Detail", new {id = employee.Id});
            }

            viewModel.Init(Context);

            return View(viewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var employee = Context.Employees
                .Where(e => e.Id == (int)id)
                .Include(e => e.Company)
                .SingleOrDefault();

            if (employee == null)
            {
                return HttpNotFound();
            }

            var viewModel = new EmployeeEditViewModel()
            {
                Employee = employee
            };
            viewModel.Init(Context);

            return View(viewModel);

        }

        [HttpPost]
        public ActionResult Edit(EmployeeEditViewModel viewModel)
        {
            ValidateEmployee(viewModel.Employee);

            if (ModelState.IsValid)
            {
                var employee = viewModel.Employee;

                Context.Entry(employee).State = EntityState.Modified;
                Context.SaveChanges();

                TempData["Message"] = "Employee updated successfully";

                return RedirectToAction("Detail", new { id = employee.Id });
            }

            viewModel.Init(Context);

            return View(viewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var employee = Context.Employees
                .Where(e => e.Id == (int)id)
                .Include(e => e.Company)
                .SingleOrDefault();

            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var employee = Context.Employees
                .Where(e => e.Id == id)
                .SingleOrDefault();
            Context.Employees.Remove(employee);
            Context.SaveChanges();

            TempData["Message"] = "Employee deleted successfully";

            return RedirectToAction("Index");
        }

        private void ValidateEmployee(Employee employee)
        {
            if (ModelState.IsValidField("Employee.Company"))
            {

            }
        }
    }
}