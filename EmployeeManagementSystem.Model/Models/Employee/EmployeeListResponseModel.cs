using EmployeeManagementSystem.Model.Entities;

namespace EmployeeManagementSystem.Model.Models.Employee;

public class EmployeeListResponseModel
{
    public PageSettingModel PageSetting { get; set; }
    public List<EmployeeModel> DataList { get; set; }
}