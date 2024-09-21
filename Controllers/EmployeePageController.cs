using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Payroll.ViewModels;
using Payroll.Models;
using Payroll.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Controllers
{
    public class EmployeePageController : Controller
    {
        private readonly AppDbContext _context;
        
        public EmployeePageController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var jobTitles = await _context.JobTitles    
                .Select(j => new SelectListItem
                {
                    Value = j.JobId.ToString(),
                    Text = j.Job
                })
                .ToListAsync();

            // Fetch the existing employees to display in the table
            var employees = await _context.Employees.Include(e => e.Job).ToListAsync();
            // Create the view model
            var model = new EmployeeViewModel
            {
                JobTitles = jobTitles, // Populating dropdown
                Employees = employees  // Populating the list of employees
            };

            return View(model);
        }

        // POST: Add or Update Employee
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateEmployee(EmployeeViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Employee.Name) && model.Employee.JobId != null)
            {
                if (model.Employee.EmployeeId == 0)
                {
                    model.Employee.CreatedDt= DateTime.Now;
                    _context.Employees.Add(model.Employee);
                }
                else
                {
                    model.Employee.UpdatedDt = DateTime.Now;
                    model.Employee.CreatedDt = model.Employee.CreatedDt;
                    _context.Employees.Update(model.Employee);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            // Reload job titles in case of validation error
            model.JobTitles = _context.JobTitles
                .Select(j => new SelectListItem
                {
                    Value = j.JobId.ToString(),
                    Text = j.Job
                });

            model.Employees = await _context.Employees.Include(e => e.Job).ToListAsync();
            return View("Index", model);
        }

        // GET: Edit
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            var jobTitles = _context.JobTitles
                .Select(j => new SelectListItem
                {
                    Value = j.JobId.ToString(),
                    Text = j.Job
                })
                .ToList();

            var model = new EmployeeViewModel
            {
                Employee = employee,
                JobTitles = jobTitles,
                Employees = await _context.Employees.Include(e => e.Job).ToListAsync()
            };

            

            return View("Index", model);
        }

        //[HttpPost]
        //public async Task<IActionResult> AddEmployee(EmployeeViewModel model)
        //{
        //    var modelss = model;
        //    if (!string.IsNullOrEmpty(model.Employee.Name) && model.Employee.JobId != null)
        //    {
        //        // Add new employee
        //        _context.Employees.Add(model.Employee);
        //        await _context.SaveChangesAsync();

        //        return RedirectToAction("Index"); // Redirect after form submission
        //    }

        //    model.JobTitles = await _context.JobTitles
        //        .Select(j => new SelectListItem
        //        {
        //            Value = j.JobId.ToString(),
        //            Text = j.Job
        //        })
        //        .ToListAsync();

        //    model.Employees = await _context.Employees.Include(e => e.Job).ToListAsync();

        //    return View("Index", model);  // Reload the page with the invalid form data
        //}
    }
}



































//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using Payroll.ViewModels;
//using Payroll.Models;
//using Payroll.Data;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Payroll.Controllers
//{
//    public class EmployeePageController : Controller
//    {
//        private readonly AppDbContext _context;
//        public EmployeePageController(AppDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<IActionResult> Index()
//        {

//            var jobTitles = _context.JobTitles
//                .Select(j => new SelectListItem
//                {
//                    Value = j.JobId.ToString(),
//                    Text = j.Job 
//                })
//                .ToList();

//            var model = new EmployeeViewModel
//            {
//                JobTitles = jobTitles
//            };

//            return View(model);
//            //    var employees = await _context.Employees.ToListAsync();
//            //    return View(employees);
//            //var viewModel = new EmployeeViewModel
//            //{
//            //    JobTitles = _context.JobTitles
//            //        .Select(j => new SelectListItem
//            //        {
//            //            Value = j.JobId.ToString(),
//            //            Text = j.Job
//            //        }),
//            //    Employees = await _context.Employees.ToListAsync()
//            //};

//            //return View();
//        }
//        [HttpPost]
//        public async Task<IActionResult> AddEmployee(EmployeeViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Employees.Add(model.Employee);
//                await _context.SaveChangesAsync();

//                model.Employees = await _context.Employees.ToListAsync();
//                model.JobTitles = _context.JobTitles
//                    .Select(j => new SelectListItem
//                    {
//                        Value = j.JobId.ToString(),
//                        Text = j.Job
//                    });

//                return View("Index", model);
//            }

//            return View("Index", model);
//        }
//    }
//}


