﻿using EmployeeManagementSystem.Model.Models.Employee;
using Microsoft.JSInterop;

namespace EmployeeManagementSystem.Wasm.Pages.Employee
{
    public partial class EmployeeCreate
    {
        [Inject] private DevCode devCode { get; set; }
        public EmployeeRequestModel EmployeeModel { get; set; } = new();

        public BaseResponseModel? BaseResponseModel { get; set; } = new();

        public async Task ClearData()
        {
            EmployeeModel = new EmployeeRequestModel();
            StateHasChanged(); // Force UI refresh
        }

        public async Task Submit()
        {
            await devCode.SetAuthorizeHeader();
            var response = await httpClient.PostAsJsonAsync("api/Employee/createEmployee", EmployeeModel);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<BaseResponseModel>(result);
                if (res.IsSuccess)
                    nav.NavigateTo("/employeeList?created=true");
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                toastService.ShowError("One or more field is required");
                Console.WriteLine("Error occurred: " + error);
            }
        }
    }
}