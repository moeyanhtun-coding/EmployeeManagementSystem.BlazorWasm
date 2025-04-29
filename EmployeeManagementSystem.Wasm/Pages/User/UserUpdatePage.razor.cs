using EmployeeManagementSystem.Model.Models.User;

namespace EmployeeManagementSystem.Wasm.Pages.User
{
    public partial class UserUpdatePage
    {
        [Parameter]
        public string UserCode { get; set; }
        private UserDetailByCodeModel userModel = new();

        [Inject]
        private DevCode devCode { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await ((CustomAuthStateProvider)authenticationStateProvider).GetAuthenticationStateAsync();
            var user = authState.User;

            if (!user.Identity.IsAuthenticated || !user.IsInRole("Admin"))
            {
                // Redirect to a 404 Not Found page
                nav.NavigateTo("/404", forceLoad: false);
            }
        }

        public async Task GetUserData()
        {
            await devCode.SetAuthorizeHeader();
            var response = await httpClient.GetAsync($"api/User/getUser/{UserCode}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<BaseResponseModel>(result)!;
                if (data.IsSuccess)
                {
                    userModel = JsonConvert.DeserializeObject<UserDetailByCodeModel>(data.Data!.ToString()!)!;
                }
            }
            else
            {
                Console.WriteLine(response.ToString());
            }
        }
        private async Task Submit()
        {

        }
    }
}
