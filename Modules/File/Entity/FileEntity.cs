namespace FleetingOffers.Modules.File;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class FileEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }  // Auto-generated ID

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }  // Unique file name (e.g., UUID-based)

    [MaxLength(255)]
    public string? OriginalName { get; set; }  // Original file name from the user (optional)

    [Required]
    public string URL { get; set; }  // Path or URL to the file location

    [Required]
    public FILE_STORAGE_TYPE Storage { get; set; } = FILE_STORAGE_TYPE.LOCAL;  // Default is Local storage

    [Required]
    public bool IsUsed { get; set; } = false;  // Indicates if the file is actively used

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Default creation timestamp

    public DateTime? UpdatedAt { get; set; }  // Timestamp for last update (optional)
}
