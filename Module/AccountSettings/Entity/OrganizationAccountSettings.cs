using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleetingOffers.Module.AccountSettings;

public class OrganizationAccountSettings {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }  // Auto-generated ID

    [ForeignKey("Users")]
    [Required]
    public string UserId { get; set;}
    public bool AllowAdminCreateAdvertise { get; set; } = true;
    public bool AllowAdminEditMyAdvertise { get; set; } = true;
}