using C__MVC_Assessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C__MVC_Assessment.ViewModels
{
    public class CompanyEditViewModel : CompanyBaseViewModel
    {
        public int Id
        {
            get { return Company.Id; }
            set { Company.Id = value; }
        }
    }
}