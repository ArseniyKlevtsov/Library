namespace Library.Application.DTOs.AuthDtos.Request;

public class RegisterRequestDto
{ 
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber {  get; set; }
}
