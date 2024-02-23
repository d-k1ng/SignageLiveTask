using System.ComponentModel.DataAnnotations;

namespace SignageLivePlayer.Api.Data.Models;

public class Role
{
    [Required]
    [Key]
    public string Id { get; set; } = string.Empty;
    [Required]
    public string RoleName { get; set; } = string.Empty;
}
