using C__MVC_Assessment.Data;
using C__MVC_Assessment.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace C__MVC_Assessment.ViewModels
{
    public class EmployeeBaseViewModel
    {
        public Employee Employee { get; set; } = new Employee();

        public SelectList CompaniesSelectListItems { get; set; }

        public virtual void Init(Context context)
        {
            CompaniesSelectListItems = new SelectList(
                context.Companies
                .OrderBy(c => c.Name)
                .ToList()
                , "Id", "Name");
        }

    }
}