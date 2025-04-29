using EmployeeManagementSystem.Model.Models.User;

namespace EmployeeManagementSystem.Wasm.Pages.User
{
    public partial class UserUpdatePage
    {
        [Parameter]
        public int Id { get; set; }
        private UserDetailModel userDetailModel = new();

        [Inject]
        private DevCode devCode { get; set; }

        protected override async Task OnInitializedAsync()
        {

        }

        public async Task GetUserData()
        {
            await devCode.SetAuthorizeHeader();
            var response = await httpClient.GetAsync($"api/User/getUser/{Id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<BaseResponseModel>(result)!;
                if (data.IsSuccess)
                {
                    userDetailModel = JsonConvert.DeserializeObject<UserDetailModel>(data.Data!.ToString()!)!;
                }
            }
            else
            {
                Console.WriteLine(response.ToString());
            }
        }
    }
}
