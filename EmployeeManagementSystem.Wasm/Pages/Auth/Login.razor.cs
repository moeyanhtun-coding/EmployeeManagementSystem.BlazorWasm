namespace EmployeeManagementSystem.Wasm.Pages.Auth
{
    public partial class Login
    {
        private string _errorMessage;
        private bool _isLoading = false;
        private LoginModel _loginModel = new LoginModel();

        protected override async Task OnInitializedAsync()
        {
            var authState = await ((CustomAuthStateProvider)authenticationStateProvider).GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity != null && user.Identity.IsAuthenticated)
            {
                nav.NavigateTo("/");
            }
        }
        private async Task HandleLogin()
        {
            _errorMessage = "";
            _isLoading = true;
            try
            {
                var res = await httpClient.PostAsJsonAsync("api/auth/login", _loginModel);
                if (res.IsSuccessStatusCode)
                {
                    var jsonStr = await res.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<LoginResponseModel>(jsonStr);

                    await ((CustomAuthStateProvider)AuthStateProvider).MarkUserAsAuthenticated(result);
                    toastService.ShowSuccess("Successfully Login");
                    nav.NavigateTo("/");
                }
                else
                {
                    var content = await res.Content.ReadAsStringAsync();
                    var errorResponse = JsonConvert.DeserializeObject<ValidationErrorResponse>(content);

                    if (errorResponse?.Errors is not null)
                    {
                        foreach (var fieldErrors in errorResponse.Errors)
                        {
                            foreach (var errorMsg in fieldErrors.Value)
                            {
                                _errorMessage = errorMsg.ToString();
                                Console.WriteLine(errorMsg.ToString());
                            }
                        }
                    }
                    else
                    {
                        var resError = JsonConvert.DeserializeObject<BaseResponseModel>(content);
                        _errorMessage = resError.Message;
                        Console.WriteLine(resError.Message);
                    }
                }
            }
            finally
            {
                _isLoading = false;
            }
        }
    }
}
