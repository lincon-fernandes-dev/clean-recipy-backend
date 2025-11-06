namespace Application.DTOs
{
    public class CreateUserDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public bool IsVerified { get; set; }
    }
}