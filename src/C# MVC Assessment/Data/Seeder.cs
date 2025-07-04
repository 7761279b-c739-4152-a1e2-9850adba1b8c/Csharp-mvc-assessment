using C__MVC_Assessment.Models;

namespace C__MVC_Assessment.Data
{
    public static class Seeder
    {
        public static void Seed(Context context)
        {
            if (context.Companies.Any() || context.Employees.Any()) { return; }


            List<Company> companies = new List<Company>
            {
                new Company
                {
                    Name = "Test Company",
                    Email = "contact@test_company.example",
                    WebsiteUrl = "https://test_company.example"
                }
            };

            context.Companies.AddRange(companies);
            context.SaveChanges();

            // fake test data
            List<Employee> employees = new List<Employee>
            {
                new Employee
                {
                    FirstName = "Harry",
                    LastName = "Powlowski",
                    CompanyId = companies[0].Id,
                    Email = "",
                    PhoneNumber = ""

                },
                new Employee
                {
                    FirstName = "Tito",
                    LastName = "Reinger",
                    CompanyId = companies[0].Id,
                    Email = "",
                    PhoneNumber = ""

                },
                new Employee
                {
                    FirstName = "Timmothy",
                    LastName = "Mraz",
                    CompanyId = companies[0].Id,
                    Email = "",
                    PhoneNumber = ""

                }
            };

            context.Employees.AddRange(employees);
            context.SaveChanges();


        }
    }
}
