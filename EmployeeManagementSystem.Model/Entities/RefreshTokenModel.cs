using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.Model.Entities
{
    [Table("Tbl_RefreshToken")]
    public class RefreshTokenModel
    {
        [Key]
        public int RefreshTokenId { get; set; }
        public int UserId { get; set; }
        public string RefreshToken { get; set; }
    }
}
