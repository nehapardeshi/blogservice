﻿namespace Blog.WebAPI.Models
{
    public class PasswordResetRequest
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
