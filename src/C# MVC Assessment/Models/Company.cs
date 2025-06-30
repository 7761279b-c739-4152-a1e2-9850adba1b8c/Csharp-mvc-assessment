using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace C__MVC_Assessment.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? LogoFilepath { get; set; }
        [NotMapped]
        public IFormFile? LogoFile { get; set; }

        [Display(Name = "Website")]
        public string? WebsiteUrl { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();

    }
}
