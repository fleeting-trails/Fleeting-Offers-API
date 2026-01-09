using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FleetingOffers.Module.Upload;
using FleetingOffers.Module.User;

namespace FleetingOffers.Module.Product;

public class ProductEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Title { get; set; }

    [MaxLength(255)]
    public string? Subtitle { get; set; }

    public string? Description { get; set; }

    // Foreign Keys for Images
    public string? CoverImageId { get; set; }
    [ForeignKey("CoverImageId")]
    public UploadEntity? CoverImage { get; set; }

    public string? ThumbnailImageId { get; set; }
    [ForeignKey("ThumbnailImageId")]
    public UploadEntity? ThumbnailImage { get; set; }

    // Foreign Keys for Category & Industry
    public string? CategoryId { get; set; }
    public ProductCategoryEntity? Category { get; set; }

    public string? SubCategoryId { get; set; }
    public ProductIndustryEntity? SubCategory { get; set; }

    [Required]
    public string CreatedById { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Relationships
    public ICollection<ProductAdditionalImageEntity> AdditionalImages { get; set; } = new List<ProductAdditionalImageEntity>();
    public ICollection<ProductTagEntity> Tags { get; set; } = new List<ProductTagEntity>();
    public ICollection<ProductOwnerEntity> Owners { get; set; } = new List<ProductOwnerEntity>();
    public UserEntity CreatedBy { get; set; }
}

public class ProductOwnerEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    public string ProductId { get; set; }
    public ProductEntity Product { get; set; }

    [Required]
    public string UserId { get; set; }
    public UserEntity User { get; set; }

    [Required]
    public PRODUCT_OWNERSHIP OwnershipType { get; set; }
}

public class ProductAdditionalImageEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    [ForeignKey("Products")]
    public string ProductId { get; set; }

    [Required]
    [ForeignKey("Files")]
    public string ImageId { get; set; }
    public UploadEntity Image { get; set; }
}

// Category Entity - Managed by SUPER_ADMIN
public class ProductCategoryEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Slug { get; set; }

    public string? ImageId { get; set; }
    public UploadEntity? Image { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

// Industry Entity - Managed by SUPER_ADMIN
public class ProductIndustryEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Slug { get; set; }

    public string? ImageId { get; set; }
    public UploadEntity? Image { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class ProductTagEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    public string Tag { get; set; }

    [Required]
    [ForeignKey("Products")]
    public string ProductId { get; set; }
}

public enum PRODUCT_OWNERSHIP
{
    OWNER,
    EDITOR,
    VIEWER
}
