using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignageLivePlayer.Api.Data.Models;

public class Player

/*
    ● ID
    ● Unique Identifier
    ● Name
    ● Site Address 1
    ● Site Address 2
    ● Site Town
    ● Site County
    ● Site Postcode
    ● Site Country
    ● Check In Frequency
    ● Date Created
    ● Date Modified
*/
{
    [Required]
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string PlayerUniqueId { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string PlayerName { get; set; } = string.Empty;

    public string? SiteId { get; set; }
    [ForeignKey("Id")]
    public Site? Site { get; set; }

    public int CheckInFrequency { get; set; }

    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    public DateTime DateModified{ get; set; } = DateTime.UtcNow;



}
