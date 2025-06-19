using C__MVC_Assessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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