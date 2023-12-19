﻿namespace UserService.DTOs
{
    public class DoctorResponse
    {
        public int UserId { get; set; }

        public string Username { get; set; } = null!;

        public string Gender { get; set; } = null!;

        public DateTime Birthday { get; set; }

        public string? Major { get; set; }

        public string? Experience { get; set; }

        public string? WorkPlace { get; set; }

        public string? Qualification { get; set; }

        public string? EmergencyContact { get; set; }

        public bool IsActive { get; set; }
    }
}
