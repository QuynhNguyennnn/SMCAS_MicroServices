using System.ComponentModel.DataAnnotations;

namespace UserService.DTOs
{
    public class CreateRoleRequest
    {
        [Required]
        public string RoleName { get; set; }
    }
}
