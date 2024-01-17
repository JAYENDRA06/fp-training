using first_mvc_application.Models;
using Microsoft.AspNetCore.Mvc;

namespace first_mvc_application.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult ShowEmpDetails()
        {
            List<EmployeeModel> employees = [
                new EmployeeModel(){
                Id = 101,
                Name = "Jayendra",
                Salary = 102034.34m
                },
                new EmployeeModel(){
                    Id = 102,
                    Name = "Abhinandita",
                    Salary = 102099.34m
                }
            ];


            return View(employees);
        }
        public IActionResult GetAllEmployees()
        {
            List<EmployeeModel> employees = [
                new EmployeeModel(){
                Id = 101,
                Name = "Jayendra",
                Salary = 102034.34m
                },
                new EmployeeModel(){
                    Id = 102,
                    Name = "Abhinandita",
                    Salary = 102099.34m
                }
            ];


            return View(employees);
        }

        public IActionResult AddEmployee()
        {
            return View();
        }
    }
}