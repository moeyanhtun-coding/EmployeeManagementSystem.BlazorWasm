﻿using EmployeeManagementSystem.Model.Models.Employee;
using EmployeeManagementSystem.Wasm.Services;
using EmployeeManagementSystem.Wasm.Services.Endpoints;
using Microsoft.AspNetCore.WebUtilities;

namespace EmployeeManagementSystem.Wasm.Pages.Employee
{
    public partial class EmployeeListPage
    {
        private int pageCount;
        private int pageNo = 1;
        private int pageSize = 10;
        private string sortName;
        private bool isShow = false;
        private string? AlertMessage;
        private string AlertIcon;
        private string AlertColor;
        private List<EmployeeModel> employeeModels;
        private EmployeeListResponseModel employeeList;
        private BaseResponseModel? baseResponseModel = new();
        private int DeleteId;
        private AppModal Modal;
        [Inject] private DevCode devCode { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await PageChanged(pageNo);
        }
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var uri = new Uri(NavigationManager.Uri);
                var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
                var created = query["created"];
                var updated = query["updated"];

                if (created == "true" || updated == "true")
                {
                    await Task.Delay(200);
                    if (created == "true")
                    {
                        AlertFunction("Employee Created Successfully", "fa-check-circle", "#1cc88a");
                    }
                    else if (updated == "true")
                    {
                        AlertFunction("Employee Updated Successfully", "fa-check-circle", "#1cc88a");
                    }
                    var baseUri = uri.GetLeftPart(UriPartial.Path);
                    NavigationManager.NavigateTo(baseUri, forceLoad: false, replace: true);
                    isShow = true;
                    StateHasChanged();
                    await Task.Delay(3000);
                    isShow = false;
                    StateHasChanged();
                }
            }
        }

        public void  EmployeeListSorting(string sort)
        {
            sortName = sort;
            if (sort == "Ascending")
            {
                employeeModels = employeeList.DataList;
            }else if (sort == "Descending")
            {
                employeeModels = employeeList.DataList.OrderByDescending(x => x.EmployeeCode).ToList();
            }
        }
        public async Task GetEmployees()
        {
            await devCode.SetAuthorizeHeader();
            var response = await httpClient.GetAsync(EmployeeEndpoints.GetListPagination(pageNo, pageSize));
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                baseResponseModel = JsonConvert.DeserializeObject<BaseResponseModel>(jsonResponse)!;
                if (baseResponseModel!.IsSuccess)
                {
                    employeeList = JsonConvert.DeserializeObject<EmployeeListResponseModel>(baseResponseModel.Data.ToString())!;
                    pageCount = employeeList.PageSetting.PageCount;
                    EmployeeListSorting("Ascending");
                }
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Error occurred: " + error.ToString());
            }
        }

        private async Task Delete()
        {
            await devCode.SetAuthorizeHeader();
            var res = await httpClient.DeleteAsync( EmployeeEndpoints.Delete(DeleteId));
            if (res.IsSuccessStatusCode)
            {
                var jsonStr = await res.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<BaseResponseModel>(jsonStr);
                if (result.IsSuccess)
                {
                    Modal.Hide();
                    await GetEmployees();
                    AlertFunction("Employee Delete Successful", "fa-check-circle", "#1cc88a");
                    isShow = true;
                    StateHasChanged();
                    await Task.Delay(3000);
                    isShow = false;
                    StateHasChanged();
                }
            }
            else
            {
                var error = await res.Content.ReadAsStringAsync();
                Console.WriteLine("Error occurred: " + error);
            }
        }

        public async Task PageChanged(int i)
        {
            pageNo = i;
            await GetEmployees();
        }
        public void AlertFunction(string alertMessage, string icon, string alertColor)
        {
            AlertIcon = icon;
            AlertColor = alertColor;
            AlertMessage = alertMessage;
        }
    }
}
