namespace MangaAPI.Dto
{
    public class CreateUserDTO
    {
        public string Firstname { get; set; } = string.Empty;

        public string Lastname { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}