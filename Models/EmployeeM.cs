using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll.Models
{
    [Table("EmpData")]
    public class EmployeeM
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public int JobId { get; set; }
        public int? Duration { get; set; }
        public JobDtlM? Job { get; set; }
        public DateTime? CreatedDt { get; set; }
        public DateTime? UpdatedDt { get; set; }

    }
}
