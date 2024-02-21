using SignageLivePlayer.Api.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace SignageLivePlayer.Api.Data.Dtos;

public class PlayerReadDto
{
    [Required]
    public string PlayerUniqueId { get; set; } = string.Empty;

    [Required]
    public string PlayerName { get; set; } = string.Empty;

    public Site Site { get; set; } = new Site();

    public int CheckInFrequency { get; set; }

    [Required]
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    public DateTime DateModified { get; set; }

}
