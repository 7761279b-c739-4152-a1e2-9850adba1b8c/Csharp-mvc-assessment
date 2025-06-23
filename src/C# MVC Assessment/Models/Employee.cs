using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace C__MVC_Assessment.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Display(Name = "Company")]
        public int? CompanyId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Company Company { get; set; }
        public Employee()
        {

        }
    }
}