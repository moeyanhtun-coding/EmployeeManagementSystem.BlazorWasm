using Newtonsoft.Json.Linq;

namespace EmployeeManagementSystem.Wasm.Authentication
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorage;

        public CustomAuthStateProvider(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
           var token = await localStorage.GetItemAsync<string>("authToken");
            var identity = string.IsNullOrEmpty(token) ? new ClaimsIdentity() : GetClaimsIdentity(token);
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
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

        public async Task MarkUserAsLogout()
        {
            await localStorage.RemoveItemAsync("authToken");
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}
