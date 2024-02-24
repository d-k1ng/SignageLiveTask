using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SignageLivePlayer.Client.Models;

public class SiteViewModel
{
    [Required]
    [DisplayName("Site Id")]
    public string Id { get; set; } = string.Empty;
    [Required]
    [DisplayName("Site Name")]
    public string SiteName { get; set; } = string.Empty;
    [DisplayName("Site Address 1")]
    public string? SiteAddress1 { get; set; } = string.Empty;
    [DisplayName("Site Address 2")]
    public string? SiteAddress2 { get; set; } = string.Empty;
    [DisplayName("Site Town")]
    public string? SiteTown { get; set; } = string.Empty;
    [DisplayName("Site County")]
    public string? SiteCounty { get; set; } = string.Empty;
    [DisplayName("Site Postcode")]
    public string? SitePostcode { get; set; } = string.Empty;
    [DisplayName("Site Country")]
    public string? SiteCountry { get; set; } = string.Empty;
    [DisplayName("Site Added")]
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    public DateTime? DateModified { get; set; }
}
