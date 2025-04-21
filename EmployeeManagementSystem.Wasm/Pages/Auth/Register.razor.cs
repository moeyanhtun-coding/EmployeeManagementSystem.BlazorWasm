namespace EmployeeManagementSystem.Wasm.Pages.Auth
{
    public partial class Register
    {
        private RegisterModel _registerModel { get; set; } = new RegisterModel();


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
                await ((CustomAuthStateProvider)AuthStateProvider).MarkUserAsAuthenticated(loginResponse);
                nav.NavigateTo("/");
            }
        }
    }
}