namespace AllocellaAPI.DTOs.Auth;

public class AuthResponse
{
    // The gibberish token /j
    public string Token {get;set;} = string.Empty;
    public DateTime ExpiresAt {get;set;}
    public UserInfo User {get;set;} = null!;
    // For success message, ex. "Login Successful!"
    public string Message {get;set;} = string.Empty;
}