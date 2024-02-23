using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignageLivePlayer.Api.Data.Models;

[PrimaryKey("UserId", "RoleId")]
public class UserRole
{
    [Required]
    public string UserId { get; set; } = string.Empty;
    [Required]
    public string RoleId { get; set; } = string.Empty;
    [ForeignKey("RoleId")]
    public Role? Role { get; set; }
}
