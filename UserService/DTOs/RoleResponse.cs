namespace UserService.DTOs
{
    public class RoleResponse
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; } = null!;

        public bool IsActive { get; set; }
    }
}
