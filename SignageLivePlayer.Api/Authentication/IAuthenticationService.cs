namespace SignageLivePlayer.Api.Authentication
{
    public interface IAuthenticationService
    {
        public AuthenticationResult Register(string email, string password, string firstName, string lastName);
        
        public AuthenticationResult Login(string email, string password);
    }
}
