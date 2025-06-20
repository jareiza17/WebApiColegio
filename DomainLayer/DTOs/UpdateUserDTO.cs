namespace DomainLayer.DTOs
{
    public class UpdateUserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public int RoleID { get; set; }
        public int StatusID { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
