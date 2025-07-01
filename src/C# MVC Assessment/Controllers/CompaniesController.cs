using C__MVC_Assessment.Data;
using C__MVC_Assessment.Models;
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
            return View();
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

                    string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "LogoFiles");
                    Directory.CreateDirectory(filepath);

                    using (var fs = new FileStream(Path.Combine(filepath, company.LogoFile.FileName), FileMode.Create))
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

            return View(company);

        }

        [HttpPost]
        public IActionResult Edit(Company company)
        {
            ValidateCompany(company);

            if (ModelState.IsValid)
            {

                Context.Entry(company).State = EntityState.Modified;
                Context.SaveChanges();

                TempData["Message"] = "Company updated successfully";

                return RedirectToAction("Detail", new { id = company.Id });
            }

            return View(company);
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
