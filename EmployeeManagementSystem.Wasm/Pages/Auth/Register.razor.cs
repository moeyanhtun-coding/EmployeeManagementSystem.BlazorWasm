namespace EmployeeManagementSystem.Wasm.Pages.Auth
{
    public partial class Register
    {
        private RegisterModel _registerModel { get; set; } = new RegisterModel();

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity != null && user.Identity.IsAuthenticated)
            {
                // 👇 Redirect if already logged in
                nav.NavigateTo("/", true); // true = force reload
            }
        }
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                _registerModel = new RegisterModel()
                {
                    Name = "",
                    Email = "",
                    Password = "",
                    ConfirmPassword = "",
                };
                StateHasChanged();
            }
        }

        public async Task HandleRegister()
        {
            var res = await httpClient.PostAsJsonAsync("/api/auth/register", _registerModel);
            if (res.IsSuccessStatusCode)
            {
                var jsonStr = await res.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<LoginResponseModel>(jsonStr)!;
                toastService.ShowSuccess("Registration Successful!");
                await ((CustomAuthStateProvider)AuthStateProvider).MarkUserAsAuthenticated(loginResponse);
                nav.NavigateTo("/");
            }
            else
            {
                toastService.ShowError("Registration Failed");
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