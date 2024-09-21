using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Payroll.Data;
using Payroll.ViewModels;

namespace Payroll.Controllers
{
    public class JobPageController : Controller
    {
        private readonly AppDbContext _context;

        public JobPageController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> JobSettings()
        {
            
            // Fetch the existing employees to display in the table
            var Jobs = await _context.JobTitles.ToListAsync();
            // Create the view model
            var model = new EmployeeViewModel
            {
                JobLists = Jobs,
            };

            return View(model);
        }
        // POST: Add or Update Employee
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateJobs(EmployeeViewModel model)
        {
            if (!string.IsNullOrEmpty(model.JobList.Job) && model.JobList.Salary != null)
            //if(ModelState.IsValid)
            {
                if (model.JobList.JobId == 0)
                {
                    _context.JobTitles.Add(model.JobList);
                    model.JobList.CreatedDt = DateTime.Now;
                }
                else
                {
                    _context.JobTitles.Update(model.JobList);
                    model.JobList.UpdatedDt = DateTime.Now;
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("JobSettings");
            }

            // Reload job titles in case of validation error
            model.JobTitles = _context.JobTitles
                .Select(j => new SelectListItem
                {
                    Value = j.JobId.ToString(),
                    Text = j.Job
                });

            var Jobs = await _context.JobTitles.ToListAsync();
            model = new EmployeeViewModel
            {
                JobLists = Jobs,
            };

            return View("JobSettings", model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var job = await _context.JobTitles.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            var model = new EmployeeViewModel
            {
                JobList = job,
                JobLists = await _context.JobTitles.ToListAsync()
            };

            return View("JobSettings", model);
        }
    }

    
    
}
