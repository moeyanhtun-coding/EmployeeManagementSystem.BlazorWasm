namespace EmployeeManagementSystem.Model.Entities
{
    public class LeaveModel
    {
        public int LeaveId { get; set; }
        public string LeaveCode { get; set; }
        public string EmployeeCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LeaveType { get; set; } // e.g., Sick, Vacation
        public string Status { get; set; } // e.g., Pending, Approved, Rejected
    }
}

