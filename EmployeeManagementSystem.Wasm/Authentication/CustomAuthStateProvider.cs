namespace EmployeeManagementSystem.Wasm.Authentication
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorage;

        public CustomAuthStateProvider(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var jsonStr = await localStorage.GetItemAsync<string>("sessionState");
            if (string.IsNullOrWhiteSpace(jsonStr))
            {
                // No user is logged in
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            try
            {
                var sessionModel = JsonConvert.DeserializeObject<LoginResponseModel>(jsonStr);
                var identity = string.IsNullOrEmpty(sessionModel?.Token)
                    ? new ClaimsIdentity()
                    : GetClaimsIdentity(sessionModel.Token);
                var user = new ClaimsPrincipal(identity);
                return new AuthenticationState(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to parse sessionState: {ex.Message}");
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }

        public async Task MarkUserAsAuthenticated(LoginResponseModel loginResponseModel)
        {
            await localStorage.SetItemAsync("sessionState", loginResponseModel);
            var identity = GetClaimsIdentity(loginResponseModel.Token);
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private ClaimsIdentity GetClaimsIdentity(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var claims = jwtToken.Claims;
            return new ClaimsIdentity(claims, "jwt");
        }

        public async Task MarkUserAsLogout()
        {
            await localStorage.RemoveItemAsync("sessionState");
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}