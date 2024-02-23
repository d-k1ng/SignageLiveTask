using System.ComponentModel.DataAnnotations;

namespace SignageLivePlayer.Api.Data.Dtos;

public class SiteReadDto
{
    [Required]
    public required string Id { get; set; }

    public string SiteName { get; set; } = string.Empty;

    public string SiteAddress1 { get; set; } = string.Empty;

    public string SiteAddress2 { get; set; } = string.Empty;

    public string SiteTown { get; set; } = string.Empty;

    public string SiteCounty { get; set; } = string.Empty;

    public string SitePostcode { get; set; } = string.Empty;

    public string SiteCountry { get; set; } = string.Empty;

    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    public DateTime? DateModified { get; set; }
}
