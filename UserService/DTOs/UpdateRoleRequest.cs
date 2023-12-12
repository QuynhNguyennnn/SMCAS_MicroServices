﻿namespace UserService.DTOs
{
    public class UpdateRoleRequest
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; } = null!;

        public bool IsActive { get; set; }
    }
}
