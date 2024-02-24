using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using SignageLivePlayer.Client.Dtos;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SignageLivePlayer.Client.Models;

public class PlayerViewModel
{
    [Required]
    [DisplayName("Player Unique Id")]
    public string PlayerUniqueId { get; set; } = string.Empty;
    [Required]
    [DisplayName("Player Name")]
    public string PlayerName { get; set; } = string.Empty;
    [Required]
    [DisplayName("Site")]
    public string SiteId { get; set; } = string.Empty;
    [Required]
    [DisplayName("Site")]
    public SiteReadDto? Site { get; set; }
    [Required]
    [DisplayName("Check In Frequency (Mins)")]
    public int CheckInFrequency { get; set; }
    [DisplayName("Date Added")]
    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    [ValidateNever]
    public IEnumerable<SelectListItem>? SiteList { get; set; }
}
