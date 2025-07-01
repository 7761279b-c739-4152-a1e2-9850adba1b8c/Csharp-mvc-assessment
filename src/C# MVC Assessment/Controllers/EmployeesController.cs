using C__MVC_Assessment.Data;
using C__MVC_Assessment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace C__MVC_Assessment.Controllers
{
    public class EmployeesController : BaseController
    {
        public EmployeesController(Context context) : base(context)
        {
        }
        public ActionResult Index()
        {
            var employees = Context.Employees
                .Include(e => e.Company)
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToList();

            return View(employees);
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }

            var employee = Context.Employees
                .Where(e => e.Id == (int)id)
                .Include(e => e.Company)
                .SingleOrDefault();

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        public ActionResult Add()
        {
            ViewBag.Companies = new SelectList(Context.Companies, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Add(Employee employee)
        {
            ValidateEmployee(employee);

            if (ModelState.IsValid)
            {
                Context.Employees.Add(employee);
                Context.SaveChanges();

                TempData["Message"] = "Employee created successfully";

                return RedirectToAction("Detail", new { id = employee.Id });
            }

            return View(employee);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var employee = Context.Employees
                .Where(e => e.Id == (int)id)
                .Include(e => e.Company)
                .SingleOrDefault();

            if (employee == null)
            {
                return NotFound();
            }

            ViewBag.Companies = new SelectList(Context.Companies, "Id", "Name");
            return View(employee);

        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            ValidateEmployee(employee);

            if (ModelState.IsValid)
            {
                Context.Entry(employee).State = EntityState.Modified;
                Context.SaveChanges();

                TempData["Message"] = "Employee updated successfully";

                return RedirectToAction("Detail", new { id = employee.Id });
            }

            return View(employee);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var employee = Context.Employees
                .Where(e => e.Id == (int)id)
                .Include(e => e.Company)
                .SingleOrDefault();

            if (employee == null)
            {
                return NotFound();
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
            
        }
    }
}
