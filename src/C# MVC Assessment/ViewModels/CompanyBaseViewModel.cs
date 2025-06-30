using C__MVC_Assessment.Models;

namespace C__MVC_Assessment.ViewModels
{
    public class CompanyBaseViewModel
    {
        public Company Company { get; set; } = new Company();

        public virtual void Init()
        {

        }
    }
}