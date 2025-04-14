using EmployeeManagementSystem.Model.Entities;
using EmployeeManagementSystem.Model.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace EmployeeManagementSystem.Wasm.Pages.Employee
{
    public partial class EmployeeUpdate
    {
        [Parameter]
        public int Id { get; set; }
        private EmployeeModel employee = new();

        protected override async Task OnInitializedAsync()
        {
            
        }
        
        public async Task GetEmployeeData()
        {
            var res = await httpClient.GetAsync($"api/Employee/getEmployeeById/{Id}");
            if (res.IsSuccessStatusCode)
            {
                var result = await res.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<BaseResponseModel>(result);
                if (data.IsSuccess)
                {
                    employee = JsonConvert.DeserializeObject<EmployeeModel>(data.Data.ToString()!)!;
                }
            }
        }
    }
}
