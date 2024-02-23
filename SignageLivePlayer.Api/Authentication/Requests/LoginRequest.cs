namespace SignageLivePlayer.Api.Authentication.Requests;

public record LoginRequest(
    string Email,
    string Password
);
