using System.ComponentModel.DataAnnotations;

namespace SignageLivePlayer.Api.Data.Models;

public class Site
{

    [Required]
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string SiteName { get; set; } = string.Empty;


    public string SiteAddress1 { get; set; } = string.Empty;

    public string SiteAddress2 { get; set; } = string.Empty;

    public string SiteTown { get; set; } = string.Empty;

    public string SiteCounty { get; set; } = string.Empty;

    public string SitePostcode { get; set; } = string.Empty;

    public string SiteCountry { get; set; } = string.Empty;

    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    
    public DateTime DateModified { get; set; } = DateTime.UtcNow;
}
