using System.ComponentModel.DataAnnotations;

namespace SignageLivePlayer.Client.Models;

public class PlayerUpdateDto
{

    [Required]
    public string PlayerName { get; set; } = string.Empty;

    public string SiteId { get; set; } = string.Empty;

    public int CheckInFrequency { get; set; }

}
