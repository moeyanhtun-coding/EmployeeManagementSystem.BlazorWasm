using EmployeeManagementSystem.Model.Models;
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
            var sessionModel = await localStorage.GetItemAsync<LoginResponseModel>("sessionState");
            var identity = string.IsNullOrEmpty(sessionModel.Token)
                ? new ClaimsIdentity()
                : GetClaimsIdentity(sessionModel.Token);
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
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