namespace EmployeeManagementSystem.Model.Models
{
    public class ValidationErrorResponse
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
    }
}
