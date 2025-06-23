using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace C__MVC_Assessment.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string LogoFilepath { get; set; }
        [Display(Name="Website")]
        public string WebsiteUrl { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public Company()
        {

        }
    }
}
