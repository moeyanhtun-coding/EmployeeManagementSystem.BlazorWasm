using EmployeeManagementSystem.Model.Entities;
using EmployeeManagementSystem.Model.Models;
using EmployeeManagementSystem.Model.Models.User;
using EmployeeManagementSystem.Wasm.Pages.Employee;

namespace EmployeeManagementSystem.Wasm.Pages
{
    public partial class Home
    {
        [Inject]
        private DevCode devCode { get; set; }
        private List<EmployeeModel> employees { get; set; }
        private List<UserDetailModel> users { get; set; }
        protected async override Task OnInitializedAsync()
        {
            await GetEmployeeData();
            await GetUserData();
        }

        private async Task GetEmployeeData()
        {
            await devCode.SetAuthorizeHeader();
            var response = await httpClient.GetAsync("api/Employee/employeeList");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var baseResponseModel = JsonConvert.DeserializeObject<BaseResponseModel>(jsonResponse)!;
                if (baseResponseModel!.IsSuccess)
                {
                    employees = JsonConvert.DeserializeObject<List<EmployeeModel>>(baseResponseModel.Data!.ToString()!)!;
                }
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Error occurred: " + error.ToString());
            }
        }

        private async Task GetUserData()
        {
            await devCode.SetAuthorizeHeader();
            var response = await httpClient.GetAsync("api/User/getUserList");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var baseResponseModel = JsonConvert.DeserializeObject<BaseResponseModel>(jsonResponse);
                if (baseResponseModel.IsSuccess)
                {
                    users = JsonConvert.DeserializeObject<List<UserDetailModel>>(baseResponseModel.Data.ToString());
                }
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Error occurred: " + error.ToString());
            }
        }
    }
}
