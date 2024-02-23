using SignageLivePlayer.Api.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace SignageLivePlayer.Api.Data.Dtos;

public class PlayerReadDto
{
    [Required]
    public string PlayerUniqueId { get; set; } = string.Empty;

    [Required]
    public string PlayerName { get; set; } = string.Empty;

    public SiteReadDto? Site { get; set; }

    public int CheckInFrequency { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

}
