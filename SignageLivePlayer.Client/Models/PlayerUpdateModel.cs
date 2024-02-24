using System.ComponentModel.DataAnnotations;

namespace SignageLivePlayer.Client.Models;

public class PlayerUpdateModel
{
    [Required]
    public string PlayerUniqueId { get; set; } = string.Empty;
    [Required]
    public string PlayerName { get; set; } = string.Empty;
    [Required]
    public string SiteId { get; set; } = string.Empty;
    [Required]
    public int CheckInFrequency { get; set; }
}
