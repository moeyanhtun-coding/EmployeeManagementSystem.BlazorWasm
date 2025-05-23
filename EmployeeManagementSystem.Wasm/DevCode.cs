﻿
namespace EmployeeManagementSystem.Wasm
{
    public class DevCode
    {
        private bool isShow = false;
        private string AlertIcon;
        private string AlertColor;
        private string AlertMessage;
        private readonly ILocalStorageService localStorage;
        private readonly HttpClient httpClient;
        private readonly AuthenticationStateProvider authenticationStateProvider;
        private readonly NavigationManager nav;


        public DevCode(ILocalStorageService localStorage, HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, NavigationManager nav)
        {
            this.localStorage = localStorage;
            this.httpClient = httpClient;
            this.authenticationStateProvider = authenticationStateProvider;
            this.nav = nav;

        }

        public async Task  SetAuthorizeHeader()
        {
            try
            {
                var sessionState = (await localStorage.GetItemAsync<LoginResponseModel>("sessionState"));
                if (sessionState is not null && !string.IsNullOrEmpty(sessionState.Token))
                {
                    if (sessionState.TokenExpired < DateTimeOffset.UtcNow.ToUnixTimeSeconds())
                    {
                        await ((CustomAuthStateProvider)authenticationStateProvider).MarkUserAsLogout();
                        nav.NavigateTo("/login");
                    }
                    else if (sessionState.TokenExpired < DateTimeOffset.UtcNow.AddMinutes(25).ToUnixTimeSeconds())
                    {
                        var res = await httpClient.GetFromJsonAsync<LoginResponseModel>($"/api/auth/loginByRefreshToken/{sessionState.RefreshToken}");
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
            catch(HttpRequestException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}
