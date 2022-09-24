namespace CarBuddy.Application.Models.DTOs
{
    public class RegisterDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string ActivationToken { get; set; }

        public RegisterDto(Result result)
        {
            Id = result.GetProperty("Id").ToString();
            Email = result.GetProperty("Email").ToString();
            ActivationToken = result.GetProperty("ActivationToken").ToString();
        }
    }
}
