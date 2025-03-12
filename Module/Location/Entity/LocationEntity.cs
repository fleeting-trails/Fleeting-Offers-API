using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleetingOffers.Module.Location;

public class LocationEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }  // Auto-generated ID

    [Required]
    public double Lat { get; set; }  // Latitude (Required)

    [Required]
    public double Long { get; set; }  // Longitude (Required)

    public string? AddressText { get; set; }  // Full address (Optional)

    [Required]
    [MaxLength(100)]
    public string District { get; set; }  // Administrative district

    [Required]
    [MaxLength(100)]
    public string Upazila { get; set; }  // Sub-district or Upazila

    [MaxLength(100)]
    public string? Union { get; set; }  // Union (Optional)

    [MaxLength(100)]
    public string? Village { get; set; }  // Village (Optional)

    [MaxLength(100)]
    public string? Area { get; set; }  // Area (Optional)

    [MaxLength(20)]
    public string? PostalCode { get; set; }  // Postal code (Optional)

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Timestamp of creation
}
