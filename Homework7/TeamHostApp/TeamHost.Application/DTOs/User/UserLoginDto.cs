using System.ComponentModel.DataAnnotations;

namespace TeamHost.Application.DTOs.User;

public class UserLoginDto
{
    [EmailAddress] [Required] public string Email { get; set; } = null!;

    [Required] public string Password { get; set; } = null!;
}