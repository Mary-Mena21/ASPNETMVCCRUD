using ASPNETMVCCRUD.Data;
using ASPNETMVCCRUD.Models;
using ASPNETMVCCRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNETMVCCRUD.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;
        //private readonly object addEmployeeViewModel;

        public EmployeesController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Employee=await mvcDemoDbContext.Employees.ToListAsync();
            return View(Employee);
        }

        //[HttpDelete]
        //public async Task<ActionResult> Delete(string id)
        //{
        //    var id = await Id.ToListAsyinc();
        //}

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        /*--------------------With async-----------------------*/
        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
                Department = addEmployeeRequest.Department,
            };

            await mvcDemoDbContext.Employees.AddAsync(employee);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        /*--------------------Without async-----------------------*/
        //[HttpPost]
        //public IActionResult Add(AddEmployeeViewModel addEmployeeRequest)
        //{
        //    var employee = new Employee()
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = addEmployeeRequest.Name,
        //        Email = addEmployeeRequest.Email,
        //        Salary = addEmployeeRequest.Salary,
        //        DateOfBirth = addEmployeeRequest.DateOfBirth,
        //        Department = addEmployeeRequest.Department,
        //    };

        //    mvcDemoDbContext.Employees.Add(employee);
        //    mvcDemoDbContext.SaveChanges();
        //    return RedirectToAction("Add");
        //}

    }
}
//