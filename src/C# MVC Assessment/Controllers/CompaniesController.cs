using C__MVC_Assessment.Data;
using C__MVC_Assessment.Models;
using C__MVC_Assessment.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;


namespace C__MVC_Assessment.Controllers
{
    public class CompaniesController : BaseController
    {
        public CompaniesController(Context context) : base(context)
        {
        }

        public IActionResult Index()
        {
            var companies = Context.Companies
                .Include(e => e.Employees)
                .OrderBy(e => e.Name)
                .ToList();

            return View(companies);
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var company = Context.Companies
                .Where(e => e.Id == (int)id)
                .Include(e => e.Employees)
                .SingleOrDefault();

            if (company == null)
            {
                return NotFound();
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
        public async Task<IActionResult> Add(Company company)
        {
            ValidateCompany(company);


            if (ModelState.IsValid)
            {
                // get file from request
                // todo fix this

                if (company.LogoFile != null)
                {
                    if (!company.LogoFile.ContentType.StartsWith("image/"))
                    {
                        ModelState.AddModelError("LogoFile", "Logo must be an image");
                        return View(company);
                    }

                    // todo image size validation
                    //var image = Image.FromStream(company.LogoFile.OpenReadStream());

                    string filepath = Path.GetDirectoryName(Path.Combine(Directory.GetCurrentDirectory(), "LogoFiles"));
                    Directory.CreateDirectory(filepath);

                    using (var fs = new FileStream(filepath, FileMode.Create))
                    {
                        await company.LogoFile.CopyToAsync(fs);
                    }

                    company.LogoFilepath = "/" + Path.Combine("LogoFiles", company.LogoFile.FileName);
                }

                Context.Companies.Add(company);
                Context.SaveChanges();

                TempData["Message"] = "Company created successfully";

                return RedirectToAction("Detail", new { id = company.Id });
            }

            return View(company);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var company = Context.Companies
                .Where(e => e.Id == (int)id)
                //.Include(e => e.Employees)
                .SingleOrDefault();

            if (company == null)
            {
                return NotFound();
            }

            var viewModel = new CompanyEditViewModel()
            {
                Company = company
            };
            viewModel.Init();

            return View(viewModel);

        }

        [HttpPost]
        public IActionResult Edit(CompanyEditViewModel viewModel)
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

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var company = Context.Companies
                .Where(e => e.Id == (int)id)
                .Include(e => e.Employees)
                .SingleOrDefault();

            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        [HttpPost]
        public IActionResult Delete(int id)
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
