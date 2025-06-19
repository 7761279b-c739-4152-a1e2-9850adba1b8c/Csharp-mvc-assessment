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
    public class CompaniesController : BaseController
    {
        public ActionResult Index()
        {
            var companies = Context.Companies
                .Include(e => e.Employees)
                .OrderBy(e => e.Name)
                .ToList();

            return View(companies);
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var company = Context.Companies
                .Where(e => e.Id == (int)id)
                .Include(e => e.Employees)
                .SingleOrDefault();

            if (company == null)
            {
                return HttpNotFound();
            }

            return View(company);
        }

        public ActionResult Add()
        {
            var viewModel = new CompanyAddViewModel();

            viewModel.Init();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(CompanyAddViewModel viewModel)
        {
            ValidateCompany(viewModel.Company);

            if (ModelState.IsValid)
            {
                var company = viewModel.Company;

                Context.Companies.Add(company);
                Context.SaveChanges();

                TempData["Message"] = "Company created successfully";

                return RedirectToAction("Detail", new { id = company.Id });
            }

            viewModel.Init();

            return View(viewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var company = Context.Companies
                .Where(e => e.Id == (int)id)
                //.Include(e => e.Employees)
                .SingleOrDefault();

            if (company == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CompanyEditViewModel()
            {
                Company = company
            };
            viewModel.Init();

            return View(viewModel);

        }

        [HttpPost]
        public ActionResult Edit(CompanyEditViewModel viewModel)
        {
            ValidateCompany(viewModel.Company);

            if (ModelState.IsValid)
            {
                var company = viewModel.Company;

                Context.Entry(company).State = EntityState.Modified;
                Context.SaveChanges();

                TempData["Message"] = "Company updated successfully";

                return RedirectToAction("Detail", new { id = company.Id });
            }

            viewModel.Init();

            return View(viewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var company = Context.Companies
                .Where(e => e.Id == (int)id)
                .Include(e => e.Employees)
                .SingleOrDefault();

            if (company == null)
            {
                return HttpNotFound();
            }

            return View(company);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {

            // todo include employees and check its empty
            var company = Context.Companies
                .Where(e => e.Id == id)
                .SingleOrDefault();
            Context.Companies.Remove(company);
            Context.SaveChanges();

            TempData["Message"] = "Company deleted successfully";

            return RedirectToAction("Index");
        }

        private void ValidateCompany(Company company)
        {
            // todo
        }
    }
}