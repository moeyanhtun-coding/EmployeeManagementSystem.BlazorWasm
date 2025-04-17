using Blazored.Toast.Services;
using EmployeeManagementSystem.Model.Models;
using EmployeeManagementSystem.Wasm.Authentication;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace EmployeeManagementSystem.Wasm
{
    public class DevCode
    {
        private readonly ILocalStorageService localStorage;
        private readonly HttpClient httpClient;
        private readonly AuthenticationStateProvider authenticationStateProvider;
        private readonly NavigationManager nav;
        private readonly ToastService toastService;

        public DevCode(ILocalStorageService localStorage, HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, NavigationManager nav, ToastService toastService)
        {
            this.localStorage = localStorage;
            this.httpClient = httpClient;
            this.authenticationStateProvider = authenticationStateProvider;
            this.nav = nav;
            this.toastService = toastService;
        }

        public async Task SetAuthorizeHeader()
        {
            var sessionState = (await localStorage.GetItemAsync<LoginResponseModel>("sessionState"));
            if (sessionState is not null && !string.IsNullOrEmpty(sessionState.Token))
            {
                if (sessionState.TokenExpired < DateTimeOffset.UtcNow.ToUnixTimeSeconds())
                {
                    await ((CustomAuthStateProvider)authenticationStateProvider).MarkUserAsLogout();
                    nav.NavigateTo("/login");
                    toastService.ShowWarning("Session Expired. You Need to Login Again!");
                }
                else if (sessionState.TokenExpired < DateTimeOffset.UtcNow.AddMinutes(25).ToUnixTimeSeconds())
                {
                    var res = await httpClient.GetFromJsonAsync<LoginResponseModel>($"/api/auth/loginByRefreshToken?refreshToken={sessionState.RefreshToken}");
                    if (res is not null)
                    {
                        await ((CustomAuthStateProvider)authenticationStateProvider).MarkUserAsAuthenticated(res);
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", res.Token);
                    }
                    else
                    {
                        await ((CustomAuthStateProvider)authenticationStateProvider).MarkUserAsLogout();
                    }
                }
                else
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessionState.Token);
                }
            }
        }
    }
}
