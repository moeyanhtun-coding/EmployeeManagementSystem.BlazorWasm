using EmployeeManagementSystem.Wasm.Services.Endpoints;

namespace EmployeeManagementSystem.Wasm.Pages.Employee
{
    public partial class EmployeeUpdate
    {
        [Parameter]
        public int Id { get; set; }
        private EmployeeModel employeeModel = new();
        [Inject]
        private DevCode devCode { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetEmployeeData();
        }

        public async Task ClearData()
        {
            employeeModel.FirstName = "";
            employeeModel.LastName = "";
            employeeModel.PhoneNumber = "";
            employeeModel.Email = "";
            employeeModel.Address = "";
            employeeModel.DepartmentCode = "";
            employeeModel.PositionCode = "";
            employeeModel.DateOfJoining = DateTime.Now;
            employeeModel.DateOfBirth = DateTime.Now;
            StateHasChanged(); // Force UI refresh
        }

        public async Task GetEmployeeData()
        {
            await devCode.SetAuthorizeHeader();
            var res = await httpClient.GetAsync(EmployeeEndpoints.Edit(Id));
            if (res.IsSuccessStatusCode)
            {
                var result = await res.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<BaseResponseModel>(result);
                if (data.IsSuccess)
                {
                    employeeModel = JsonConvert.DeserializeObject<EmployeeModel>(data.Data.ToString()!)!;
                }
            }
            else
            {
                Console.WriteLine(res.ToString());
            }
        }

        public async Task Submit()
        {
            await devCode.SetAuthorizeHeader();
            var res = await httpClient.PatchAsJsonAsync<EmployeeModel>(EmployeeEndpoints.Update(Id), employeeModel);
            if (res.IsSuccessStatusCode)
            {
                var jsonStr = await res.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<BaseResponseModel>(jsonStr);
                if (result.IsSuccess)
                    nav.NavigateTo("/employeeList?updated=true");
            }
            else
            {
                var error = await res.Content.ReadAsStringAsync();
                Console.WriteLine("Error occurred: " + error);
            }
        }
    }
}
