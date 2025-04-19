namespace EmployeeManagementSystem.Model.Models
{
    public class BaseResponseModel
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public Object? Data { get; set; }
    }
}
