namespace EmployeeManagementSystem.Wasm.Pages.User;

public partial class UserListPage
{
    private AppModal Modal;
    [Inject] private DevCode devCode { get; set; }
    private List<UserModel>? UserLists { get; set; }

    private async Task GetUserList()
    {
        await devCode.SetAuthorizeHeader();
        var response = await httpClient.GetAsync("api/User/userList");
        if (response.IsSuccessStatusCode)
        {
            var jsonResponse =await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<BaseResponseModel>(jsonResponse);
            if (res.IsSuccess)
            {
                UserLists = res.Data as List<UserModel>;
            }
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine(error);
        }
    }
}