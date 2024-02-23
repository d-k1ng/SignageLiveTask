namespace SignageLivePlayer.Api.Authentication.Responses;

public record AuthenticationResponse(
        string Id,
        string Email,
        string FirstName,
        string LastName,
        string Token
    );
