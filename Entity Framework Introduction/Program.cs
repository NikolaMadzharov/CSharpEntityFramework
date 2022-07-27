using System;
using System.Linq;
using System.Text;
using SoftUni.Data;
using SoftUni.Models;

namespace SoftUni
{
    public class StartUp
    {
        public static void Main()
        {
            SoftUniContext context = new SoftUniContext();

            //Console.WriteLine(GetEmployeesFullInformation(context));
            //Console.WriteLine(GetEmployeesWithSalaryOver50000(context));
            //Console.WriteLine(GetEmployeesFromResearchAndDevelopment(context));
            Console.WriteLine(AddNewAddressToEmployee(context));

        }

        public  static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var employeeInformation = context.Employees
                .Select(x => new
                {
                    x.EmployeeId,
                    x.FirstName,
                    x.MiddleName,
                    x.LastName,
                    x.JobTitle,
                    x.Salary
                })
                .OrderBy(x=>x.EmployeeId)
                .ToArray();

            StringBuilder output = new StringBuilder();

            foreach (var employee in employeeInformation)
            {
                
                output.AppendLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:f2}");
            }

            return output.ToString().Trim();
        }

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var employeeSalary = context.Employees
                .Select(x => new
                {
                    x.FirstName,
                    x.Salary
                })
                .Where(x => x.Salary > 50000)
                .OrderBy(x => x.FirstName)
                .ToArray();

            StringBuilder output = new StringBuilder();

            foreach (var employee in employeeSalary)
            {
                output.AppendLine($"{employee.FirstName} - {employee.Salary:f2}");
            }

            return output.ToString().TrimEnd();
        }

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var employeeDetails = context.Employees
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    DepartmentName = x.Department.Name,
                    x.Salary,
                })
                .Where(x => x.DepartmentName == "Research and Development")
                .OrderBy(x => x.Salary)
                .ThenByDescending(x => x.FirstName)
                .ToArray();

            StringBuilder output = new StringBuilder();

            foreach (var employee in employeeDetails)
            {
                output.AppendLine($"{employee.FirstName} {employee.LastName} from {employee.DepartmentName} - ${employee.Salary:f2}");
            }

            return output.ToString().TrimEnd();
        }

        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            Address address = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            Employee nakovEmployee = context.Employees.First(x => x.LastName == "Nakov");

            context.Addresses.Add(address);

            nakovEmployee.Address = address;

            context.SaveChanges();


            var employee = context.Employees
                .Select(x => new
                {
                    x.AddressId,
                    adressText = x.Address.AddressText
                })
                .OrderByDescending(x => x.AddressId)
                .Take(10)
                .ToArray();
            StringBuilder output = new StringBuilder();

            foreach (var em in employee)
            {
                output.AppendLine(em.adressText);
            }

            return output.ToString().TrimEnd();
        }
            public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var employees = context.Employees
                .Include(db => db.EmployeesProjects)
                .ThenInclude(db => db.Project)
                .Where(x => x.EmployeesProjects
                    .Select(x => x.Project)
                    .Any(x => x.StartDate.Year >= 2001 && x.StartDate.Year <= 2003))
                .Take(10)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    managerFirstName = x.Manager.FirstName,
                    managerLastName = x.Manager.LastName,
                    projects = x.EmployeesProjects.Select(x => x.Project)
                })
                .ToArray();

            StringBuilder output = new StringBuilder();

            foreach (var employee in employees)
            {
                output.AppendLine($"{employee.FirstName} {employee.LastName} - Manager: {employee.managerFirstName} {employee.managerLastName}");


                       foreach (var project in employee.projects)
                       {

                           string endDate = null;
                           if (project.EndDate==null)
                           {
                               endDate = "not finished";
                           }
                           else
                           {
                               endDate= $"{project.EndDate:M/d/yyyy h:mm:ss tt}";
                           }

                           output.AppendLine($"--{project.Name} - {project.StartDate:M/d/yyyy h:mm:ss tt} - {endDate}");

                       }
            }

            return output.ToString().TrimEnd();
        }

        public static string GetAddressesByTown(SoftUniContext context)
        {
            var employeeAddressInformation = context.Addresses
                .Include(db => db.Employees)
                .Include(db => db.Town)
                .Select(x => new
                {
                    x.AddressText,
                    town = x.Town.Name,
                    employeesCount = x.Employees.Count()
                })
                .OrderByDescending(x => x.employeesCount)
                .ThenBy(x => x.town)
                .ThenBy(x => x.AddressText)
                .Take(10)
                .ToArray();

            StringBuilder output = new StringBuilder();

            foreach (var employeeInformation in employeeAddressInformation)
            {
                output.AppendLine(
                    $"{employeeInformation.AddressText},{employeeInformation.town} - {employeeInformation.employeesCount} employees");
            }

            return output.ToString().TrimEnd();

        }

        public static string GetEmployee147(SoftUniContext context)
        {
            var employee147 = context.Employees
                .FirstOrDefault(x => x.EmployeeId == 147);


            string[] projects = employee147.EmployeesProjects
                .Select(x => x.Project.Name)
                .OrderBy(name => name)
                .ToArray();

            StringBuilder output = new StringBuilder();

            output.AppendLine($"{employee147.FirstName} {employee147.LastName} - {employee147.JobTitle}");

            foreach (var employeeProjects in projects)
            {
                output.AppendLine(employeeProjects);
            }

            return output.ToString().Trim();
        }
    }
}
