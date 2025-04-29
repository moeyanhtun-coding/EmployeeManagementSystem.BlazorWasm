using Microsoft.AspNetCore.WebUtilities;

namespace EmployeeManagementSystem.Wasm.Pages.Employee
{
    public partial class EmployeeListPage
    {
        private bool isShow = false;
        private string? AlertMessage;
        private string AlertIcon;
        private string AlertColor;
        private List<EmployeeModel> employeeModels;
        private BaseResponseModel? baseResponseModel = new();
        private int DeleteId;
        private AppModal Modal;
        [Inject] private DevCode devCode { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetEmployees();
        }
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var uri = new Uri(NavigationManager.Uri);
                var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
                var created = query["created"];
                var updated = query["updated"];

                if (created == "true" || updated == "true")
                {
                    await Task.Delay(200);
                    if (created == "true")
                    {
                        AlertFunction("Employee Created Successfully", "fa-check-circle", "#1cc88a");
                    }
                    else if (updated == "true")
                    {
                        AlertFunction("Employee Updated Successfully", "fa-check-circle", "#1cc88a");
                    }
                    var baseUri = uri.GetLeftPart(UriPartial.Path);
                    NavigationManager.NavigateTo(baseUri, forceLoad: false, replace: true);
                    isShow = true;
                    StateHasChanged();
                    await Task.Delay(3000);
                    isShow = false;
                    StateHasChanged();

                }
            }
        }
        public async Task GetEmployees()
        {
            await devCode.SetAuthorizeHeader();
            var response = await httpClient.GetAsync("api/Employee/employeeList");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                baseResponseModel = JsonConvert.DeserializeObject<BaseResponseModel>(jsonResponse)!;
                if (baseResponseModel!.IsSuccess)
                {
                    employeeModels = JsonConvert.DeserializeObject<List<EmployeeModel>>(baseResponseModel.Data!.ToString()!)!;
                }
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Error occurred: " + error.ToString());
            }
        }

        private async Task Delete()
        {
            await devCode.SetAuthorizeHeader();
            var res = await httpClient.DeleteAsync($"api/Employee/deleteEmployee/{DeleteId}");
            if (res.IsSuccessStatusCode)
            {
                var jsonStr = await res.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<BaseResponseModel>(jsonStr);
                if (result.IsSuccess)
                {
                    Modal.Hide();
                    await GetEmployees();
                    AlertFunction("Employee Delete Successful", "fa-check-circle", "#1cc88a");
                    isShow = true;
                    StateHasChanged();
                    await Task.Delay(3000);
                    isShow = false;
                    StateHasChanged();
                }
            }
            else
            {
                var error = await res.Content.ReadAsStringAsync();
                Console.WriteLine("Error occurred: " + error);
            }
        }

        public void AlertFunction(string alertMessage, string icon, string alertColor)
        {
            AlertIcon = icon;
            AlertColor = alertColor;
            AlertMessage = alertMessage;
        }
    }
}
