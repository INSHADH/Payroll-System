using Microsoft.AspNetCore.Mvc.Rendering;
using Payroll.Models;

namespace Payroll.ViewModels
{
    public class EmployeeViewModel
    {
        public EmployeeM Employee { get; set; }
        public JobDtlM JobList { get; set; }

        public IEnumerable<SelectListItem> JobTitles { get; set; }
        public IEnumerable<EmployeeM> Employees { get; set; }
        public IEnumerable<JobDtlM> JobLists { get; set; }
    }
}
