using EmployeeManagementSystem.Model.Entities;
using Microsoft.AspNetCore.Components;

namespace EmployeeManagementSystem.Wasm.Pages.Employee
{
    public partial class EmployeeUpdate
    {
        [Parameter]
        public int Id { get; set; }
        private EmployeeModel employee = new();

        protected override async Task OnInitializedAsync()
        {
            
        }
    }
}
