using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll.Models
{
    [Table("JobTitles")]
    public class JobDtlM
    {
        public int? JobId { get; set; }
        public string Job { get; set; }
        public decimal? Salary { get; set; }
        public string? Desc { get; set; }
        public DateTime? CreatedDt { get; set; }
        public DateTime? UpdatedDt { get; set; }

    }
}
