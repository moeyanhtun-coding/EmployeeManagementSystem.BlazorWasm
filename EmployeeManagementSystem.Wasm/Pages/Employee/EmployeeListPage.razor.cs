using EmployeeManagementSystem.Model.Entities;
using EmployeeManagementSystem.Model.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using System.Dynamic;
using System.Net.Http.Json;

namespace EmployeeManagementSystem.Wasm.Pages.Employee
{
    public partial class EmployeeListPage
    {
        private List<EmployeeModel>? employeeModels;
        private BaseResponseModel? baseResponseModel = new();

        protected override async Task OnInitializedAsync()
        {
            await GetEmployees();
        }

        private async Task GetEmployees()
        {
            var response = await httpClient.GetAsync("api/Employee/employeeList");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                baseResponseModel = JsonConvert.DeserializeObject<BaseResponseModel>(jsonResponse)!;
                if (baseResponseModel!.IsSuccess)
                {
                    employeeModels = JsonConvert.DeserializeObject<List<EmployeeModel>>(baseResponseModel.Data.ToString()!)!;
                }
            }
        }
    }
}
