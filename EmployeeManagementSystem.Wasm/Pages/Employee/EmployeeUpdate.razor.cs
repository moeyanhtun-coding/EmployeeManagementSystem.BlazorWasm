﻿using Azure;
using Blazored.Toast.Services;
using EmployeeManagementSystem.Model.Entities;
using Newtonsoft.Json;

namespace EmployeeManagementSystem.Wasm.Pages.Employee
{
    public partial class EmployeeUpdate
    {
        [Parameter]
        public int Id { get; set; }
        private EmployeeModel employee = new();
        [Inject]
        private DevCode devCode { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetEmployeeData();
        }

        public async Task GetEmployeeData()
        {
            await devCode.SetAuthorizeHeader();
            var res = await httpClient.GetAsync($"api/Employee/getEmployee/{Id}");
            var result = await res.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<BaseResponseModel>(result);
            if (data.IsSuccess)
            {
                employee = JsonConvert.DeserializeObject<EmployeeModel>(data.Data.ToString()!)!;
            }

        }

        public async Task Submit()
        {
            await devCode.SetAuthorizeHeader();
            var res = await httpClient.PatchAsJsonAsync<EmployeeModel>($"api/Employee/updateEmployee/{Id}", employee);
            if (res.IsSuccessStatusCode)
            {
                var jsonStr = await res.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<BaseResponseModel>(jsonStr);
                if (result.IsSuccess)
                    nav.NavigateTo("/employeeList");
                toastService.ShowSuccess(result.Message);
            }
            else
            {
                var error = await res.Content.ReadAsStringAsync();
                Console.WriteLine("Error occurred: " + error);
            }
        }
    }
}
