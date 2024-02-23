namespace SignageLivePlayer.Api.Data.Repositories.Responses;

public class RepoResponse<T>
{
    public T? Data { get; set; }
    public bool IsError { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
}
