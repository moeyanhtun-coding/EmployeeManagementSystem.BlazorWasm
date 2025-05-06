using EmployeeManagementSystem.Model.Models.User;

namespace EmployeeManagementSystem.Wasm.Pages.User
{
    public partial class UserListPage
    {
        private string? currentUserEmail;
        private List<UserDetailModel> UserDetailModels;
        private List<UserDetailModel> UserLists;
        private BaseResponseModel baseResponseModel;
        private string role;
        private AppModal Modal;
        [Inject] private DevCode devCode { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await ((CustomAuthStateProvider)authenticationStateProvider).GetAuthenticationStateAsync();
            var user = authState.User;
            currentUserEmail = user.FindFirst(c => c.Type == ClaimTypes.Email || c.Type == "email")?.Value;
        
            if (!user.Identity.IsAuthenticated || !user.IsInRole("Admin"))
            {
                // Redirect to a 404 Not Found page
                nav.NavigateTo("/404", forceLoad: false);
            }
            await GetUserList();
        }
        public void GetUserListByRole(string roleName)
        {
            role = roleName;
            if (roleName == "All")
            {
                UserLists = UserDetailModels;
            }
            else
            {
                UserLists = UserDetailModels.Where(x => x.RoleName == roleName).ToList();
            }

        }
        public async Task GetUserList()
        {
            try
            {
                await devCode.SetAuthorizeHeader();
                var response = await httpClient.GetAsync("api/User/getUserList");
                if (response.IsSuccessStatusCode)
                {
                    var JsonStr = await response.Content.ReadAsStringAsync();
                    baseResponseModel = JsonConvert.DeserializeObject<BaseResponseModel>(JsonStr)!;
                    if (baseResponseModel.IsSuccess)
                    {
                        UserDetailModels = JsonConvert.DeserializeObject<List<UserDetailModel>>(baseResponseModel.Data!.ToString()!)!;
                        GetUserListByRole("All");
                    }
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

