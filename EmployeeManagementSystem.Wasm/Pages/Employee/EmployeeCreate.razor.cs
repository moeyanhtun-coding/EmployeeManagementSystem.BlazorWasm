using EmployeeManagementSystem.Model.Entities;
using EmployeeManagementSystem.Model.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace EmployeeManagementSystem.Wasm.Pages.Employee
{
    public partial class EmployeeCreate
    {
        public EmployeeModel EmployeeModel { get; set; } = new();

        public BaseResponseModel? BaseResponseModel { get; set; } = new();
        public async Task Submit()
        {
            var response = await httpClient.PostAsJsonAsync("api/Employee/createEmployee", EmployeeModel);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<BaseResponseModel>(result);
                if (res.IsSuccess)
                    nav.NavigateTo("/employeeList");
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Error occurred: " + error);
                // Handle the error appropriately (e.g., show error to user)
            }
        }
    }
}

