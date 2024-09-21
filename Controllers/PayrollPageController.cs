using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Payroll.BuisnessLayer;
using Payroll.Data;
using Payroll.Models;
using Payroll.ViewModels;
using Payroll.BuisnessLayer;

namespace Payroll.Controllers
{
    public class PayrollPageController : Controller
    {
        private readonly AppDbContext _context;

        public PayrollPageController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> PayrollCalc()
        {
            var Employees = await _context.Employees
                .Select(j => new SelectListItem
                {
                    Value = j.EmployeeId.ToString(),
                    Text = j.Name
                })
                .ToListAsync();
            var Months = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "January" },
                new SelectListItem { Value = "2", Text = "February" },
                new SelectListItem { Value = "3", Text = "March" },
                new SelectListItem { Value = "4", Text = "April" },
                new SelectListItem { Value = "5", Text = "May" },
                new SelectListItem { Value = "6", Text = "June" },
                new SelectListItem { Value = "7", Text = "July" },
                new SelectListItem { Value = "8", Text = "August" },
                new SelectListItem { Value = "9", Text = "September" },
                new SelectListItem { Value = "10", Text = "October" },
                new SelectListItem { Value = "11", Text = "November" },
                new SelectListItem { Value = "12", Text = "December" }
            };


            // Create the view model
            var model = new PayrollViewModel
            {
                Joblists = Employees,
                months= Months,
                Payroll = new PayrollM()

            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> CalcPayroll(PayrollViewModel model)
        {
            //DateTime targetDate = new DateTime(payroll.Year, payroll.MonthId, 1);

            //int yearsDifference = targetDate.Year;

            if (model.Payroll.EmployeeId != null && model.Payroll.MonthId!=null && model.Payroll.Year != null && model.Payroll.WorkingHours != null  )
            {

                var employee = await _context.Employees
                    .Where(e => e.EmployeeId == model.Payroll.EmployeeId)
                    .Select(e => new
                    {
                        e.JobId,
                        e.CreatedDt
                    })
                    .FirstOrDefaultAsync();

                if (employee != null)
                {
                    model.Payroll.JobId= employee.JobId;
                    model.Payroll.JoinedDate= employee.CreatedDt;
                    // Fetch the salary from JobTitles based on the JobId
                    var jobTitle = await _context.JobTitles
                        .Where(j => j.JobId == employee.JobId)
                        .Select(j => j.Salary)
                        .FirstOrDefaultAsync();

                    if (jobTitle != null)
                    {
                        model.Payroll.Salary = jobTitle;
                        // Use the job's salary in the calculation instead of a fixed rate
                        model.Payroll = PayrollCalculationsB.CalculateSalaries(model.Payroll);
                    }
                }

                


                // Reload the employee and month lists for display
                model.Joblists = await _context.Employees
                    .Select(j => new SelectListItem
                    {
                        Value = j.EmployeeId.ToString(),
                        Text = j.Name
                    })
                    .ToListAsync();

                model.months = new List<SelectListItem>
                {
                    new SelectListItem { Value = "1", Text = "January" },
                    new SelectListItem { Value = "2", Text = "February" },
                    new SelectListItem { Value = "3", Text = "March" },
                    new SelectListItem { Value = "4", Text = "April" },
                    new SelectListItem { Value = "5", Text = "May" },
                    new SelectListItem { Value = "6", Text = "June" },
                    new SelectListItem { Value = "7", Text = "July" },
                    new SelectListItem { Value = "8", Text = "August" },
                    new SelectListItem { Value = "9", Text = "September" },
                    new SelectListItem { Value = "10", Text = "October" },
                    new SelectListItem { Value = "11", Text = "November" },
                    new SelectListItem { Value = "12", Text = "December" }
                };

                // Return the view with calculated data
                return View("PayrollCalc", model);
            }

            // If validation fails, return the form with the existing data
            return View("PayrollCalc", model);
        }
    }
}
