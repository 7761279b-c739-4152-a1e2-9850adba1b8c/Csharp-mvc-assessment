using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C__MVC_Assessment.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Company Company { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Employee()
        {

        }
    }
}