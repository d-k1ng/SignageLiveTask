using System.ComponentModel.DataAnnotations;

namespace SignageLivePlayer.Client.Models;

public class PlayerCreateDto
{   
    [Required]
    public string PlayerName { get; set; } = string.Empty;

    public string SiteId { get; set; } = string.Empty;

    public int CheckInFrequency { get; set; }

}
