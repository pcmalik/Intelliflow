using System;

namespace WebSvc1.Models
{
    public class EmployeeBonus
    {
        public long EmployeeNo { get; set; }
        public decimal BonusAmount { get; set; }
        public DateTime BonusDate { get; set; }
    }
}