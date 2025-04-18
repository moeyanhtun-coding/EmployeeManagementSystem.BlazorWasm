using EmployeeManagementSystem.Model.Models;
using EmployeeManagementSystem.Wasm.Authentication;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace EmployeeManagementSystem.Wasm.Pages.Login
{
    public partial class Login
    {

        private LoginModel _loginModel = new LoginModel();

        private async Task HandleLogin()
        {
            var res = await httpClient.PostAsJsonAsync("api/auth/login", _loginModel);
            if (res.IsSuccessStatusCode)
            {
                var jsonStr = await res.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<LoginResponseModel>(jsonStr);

                await ((CustomAuthStateProvider)AuthStateProvider).MarkUserAsAuthenticated(result);
                nav.NavigateTo("/");
            }
            else
            {

                var content = await res.Content.ReadAsStringAsync();
                try
                {
                    var errorResponse = JsonConvert.DeserializeObject<ValidationErrorResponse>(content);

                    if (errorResponse?.Errors is not null)
                    {
                        foreach (var fieldErrors in errorResponse.Errors)
                        {
                            foreach (var errorMsg in fieldErrors.Value)
                            {
                                toastService.ShowError(errorMsg);
                            }
                        }
                    }
                    else
                    {
                        var resError = JsonConvert.DeserializeObject<BaseResponseModel>(content);
                        toastService.ShowError(resError.Message);
                    }
                }
                catch
                {
                    toastService.ShowError("Failed to parse error response.");
                }
                Console.WriteLine("Credential Do Not Match.");
            }
        }
    }
}
