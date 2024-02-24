using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using SignageLivePlayer.Client.Dtos;
using System.ComponentModel.DataAnnotations;

namespace SignageLivePlayer.Client.Models;

public class PlayerViewModel
{
    [Required]
    public string PlayerUniqueId { get; set; } = string.Empty;
    [Required]
    public string PlayerName { get; set; } = string.Empty;
    [Required]
    public string SiteId { get; set; } = string.Empty;
    [Required]
    public SiteReadDto? Site { get; set; }
    [Required]
    public int CheckInFrequency { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    [ValidateNever]
    public IEnumerable<SelectListItem>? SiteList { get; set; }
}
