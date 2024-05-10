using System.ComponentModel.DataAnnotations;

namespace TeamHost.Application.DTOs.User;

public class UserRegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MaxLength(30)]
    public string Username { get; set; }

    [Required]
    [MinLength(8)]
    public string Password { get; set; }
}