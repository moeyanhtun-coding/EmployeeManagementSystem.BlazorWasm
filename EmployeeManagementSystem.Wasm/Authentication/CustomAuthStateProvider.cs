namespace EmployeeManagementSystem.Wasm.Authentication
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorage;

        public CustomAuthStateProvider(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            throw new NotImplementedException();
        }

        public async Task MarkUserAsAuthenticated(string token)
        {
           await localStorage.SetItemAsync("authToken", token);
           var identity = GetClaimsIdentity(token);
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
    }
}
