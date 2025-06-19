using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C__MVC_Assessment.ViewModels
{
    public class EmployeeEditViewModel : EmployeeBaseViewModel
    {

        public int Id
        {
            get { return Employee.Id; }
            set { Employee.Id = value; }
        }
    }
}