using C__MVC_Assessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C__MVC_Assessment.ViewModels
{
    public class EmployeeBaseViewModel
    {
        public Employee Employee { get; set; } = new Employee();

        public virtual void Init()
        {
            
        }

    }
}