﻿namespace BackendProject.Api.DTOs.Usuarios
{
    public class SignUpDto
    {
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public List<string>? Roles { get; set; }
    }
}
