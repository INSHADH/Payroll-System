namespace Payroll.Models
{
    public class PayrollM
    {
        public int? JobId { get; set; }
        public int? EmployeeId { get; set; }
        public int MonthId { get; set; }
        public int Year { get; set; }
        public DateTime? JoinedDate { get; set; }
        public int? WorkingHours { get; set; }
        public decimal? Salary { get; set; }
        public decimal? BasicSalary { get; set; }
        public decimal? HousingSalary { get; set; }
        public decimal? TransportSalary { get; set; }
        public decimal? Tax { get; set; }
        public decimal? TaxableAmt { get; set; }
        public decimal? TotalSalary { get; set; }
        public decimal? AfterTax { get; set; }

    }
}
