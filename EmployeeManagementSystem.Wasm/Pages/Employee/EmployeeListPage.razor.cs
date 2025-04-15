using EmployeeManagementSystem.Model.Entities;
using EmployeeManagementSystem.Model.Models;
using EmployeeManagementSystem.Wasm.Compoments.BaseCompoments;
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
        private int DeleteId;
        private AppModal Modal;
        [Inject]
        private DevCode devCode { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetEmployees();
        }

        private async Task GetEmployees()
        {
            await devCode.SetAuthorizeHeader();
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
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                toastService.ShowError("One or more field is required");
                Console.WriteLine("Error occurred: " + error);
            }
        }

        private async Task Delete()
        {
            await devCode.SetAuthorizeHeader();
            var res = await httpClient.DeleteAsync($"api/Employee/deleteEmployee/{DeleteId}");
            if(res.IsSuccessStatusCode)
            {
                var jsonStr = await res.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<BaseResponseModel>(jsonStr);
                if (result.IsSuccess)
                {
                    Modal.Hide();
                    await GetEmployees();
                    toastService.ShowError(result.Message);
                }
            }
            else
            {
                var error = await res.Content.ReadAsStringAsync();
                toastService.ShowError("One or more field is required");
                Console.WriteLine("Error occurred: " + error);
            }
        }
    }
}
