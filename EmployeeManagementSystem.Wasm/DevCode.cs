using System.Net.Http.Headers;

namespace EmployeeManagementSystem.Wasm
{
    public class DevCode
    {
       private readonly ILocalStorageService localStorage;
        private readonly HttpClient httpClient;

        public DevCode(ILocalStorageService localStorage, HttpClient httpClient)
        {
            this.localStorage = localStorage;
            this.httpClient = httpClient;
        }

        public async Task SetAuthorizeHeader()
        {
            var token = (await localStorage.GetItemAsync<string>("authToken")).ToString();
            if (token is not null)
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
