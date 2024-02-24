namespace SignageLivePlayer.Client.Authentication;

public record LoginRequest(
    string Email,
    string Password
);
