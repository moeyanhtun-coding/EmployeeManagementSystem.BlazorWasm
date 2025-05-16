namespace EmployeeManagementSystem.Model.Entities
{
    [Table("Tbl_AttendanceLog")]
    public class AttendanceModel
    {
        [Key]
        public int AttendanceId { get; set; }
        public string AttendanceCode { get; set; }
        public string EmployeeCode { get; set; }
        public DateTime LogTime { get; set; }
        public string Type { get; set; }
    }
}
