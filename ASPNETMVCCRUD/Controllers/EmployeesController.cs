﻿using ASPNETMVCCRUD.Data;
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

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        /*------------------Add--With async-----------------------*/
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
        /*-----------------------Edit---------------------------*/
        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            var employee =await mvcDemoDbContext.Employees.FirstOrDefaultAsync(x=>x.Id == id);
            if (employee != null)
            {
                var editModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    DateOfBirth = employee.DateOfBirth,
                    Department = employee.Department,
                };
                return View(editModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateEmployeeViewModel employeeModel)
        {
            var employee = await mvcDemoDbContext.Employees.FindAsync(employeeModel.Id);
            if (employee != null) 
            {
                employee.Id = employeeModel.Id;
                employee.Name = employeeModel.Name;
                employee.Email = employeeModel.Email;
                employee.Salary = employeeModel.Salary;
                employee.DateOfBirth = employeeModel.DateOfBirth;
                employee.Department = employeeModel.Department;
            };

            //await mvcDemoDbContext.Employees.AddAsync(employee);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel employeeModel)
        {
            var employee = await mvcDemoDbContext.Employees.FindAsync(employeeModel.Id);
            if (employee != null)
            {
                mvcDemoDbContext.Employees.Remove(employee);
                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");//Navigate to Index view
        }

    }
}
//