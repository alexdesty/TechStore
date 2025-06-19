using TechStore.Domain.Enums;

namespace TechStore.Api.DTO;

public class UserDTO
{
    public int Id { get; set; } 
    public string Name { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Login { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public UserRole Role { get; set; }
}
