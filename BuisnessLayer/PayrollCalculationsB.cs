using Payroll.Models;

namespace Payroll.BuisnessLayer
{
    public class PayrollCalculationsB
    {
        public static PayrollM CalculateSalaries(PayrollM payroll)
        {
            DateTime joinedDate = payroll.JoinedDate.Value;
            DateTime targetDate = new DateTime(payroll.Year, payroll.MonthId, 1);

            int yearsDifference = targetDate.Year - joinedDate.Year;

            if (targetDate.Month < joinedDate.Month || (targetDate.Month == joinedDate.Month && targetDate.Day < joinedDate.Day))
            {
                yearsDifference--;
            }

            for (int i=0;i< yearsDifference;i++)
            {
                payroll.Salary = (payroll.Salary + payroll.Salary / 10);
            }
            payroll.TotalSalary = payroll.Salary * payroll.WorkingHours;

            payroll.BasicSalary = (payroll.TotalSalary * 64)/100;
            payroll.HousingSalary = (payroll.TotalSalary * 24) / 100; 
            payroll.TransportSalary = (payroll.TotalSalary * 12) / 100; 
            if(payroll.BasicSalary>1000)
            {
                payroll.TaxableAmt = payroll.BasicSalary - 1000;
                payroll.Tax = (payroll.TaxableAmt*3)/10;

            }
            else
            {
                payroll.Tax = 0;

            }
            payroll.AfterTax = payroll.TotalSalary - payroll.Tax; 

            return payroll;
        }
    }
}
