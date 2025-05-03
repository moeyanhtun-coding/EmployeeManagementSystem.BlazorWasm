namespace EmployeeManagementSystem.Model.Entities
{
    [Table("Tbl_RefreshToken")]
    public class RefreshTokenModel
    {
        [Key]
        public int RefreshTokenId { get; set; }
        public string UserCode { get; set; }
        public string RefreshToken { get; set; }
    }
}
