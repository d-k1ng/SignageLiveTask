using System.ComponentModel.DataAnnotations;

namespace SignageLivePlayer.Api.Data.Dtos;

public class PlayerUpdateDto
{

    [Required]
    public string PlayerName { get; set; } = string.Empty;

    public string SiteId { get; set; } = string.Empty;

    public int CheckInFrequency { get; set; }

}
