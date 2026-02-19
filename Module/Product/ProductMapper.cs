using AutoMapper;

namespace FleetingOffers.Module.Product;

public class ProductMapper : Profile 
{
    public ProductMapper() 
    {
        CreateMap<ProductEntity, ProductDto>().ReverseMap();
        CreateMap<CreateProductDto, ProductDto>();
        CreateMap<ProductOwnerEntity, ProductOwnerDto>().ReverseMap();

        // Category mappings
        CreateMap<ProductCategoryEntity, ProductCategoryDto>().ReverseMap();
        CreateMap<CreateProductCategoryDto, ProductCategoryDto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => GenerateSlug(src.Name)))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<UpdateProductCategoryDto, ProductCategoryDto>()
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => GenerateSlug(src.Name)))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        // Industry mappings
        CreateMap<ProductIndustryEntity, ProductIndustryDto>().ReverseMap();
        CreateMap<CreateProductIndustryDto, ProductIndustryDto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => GenerateSlug(src.Name)))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<UpdateProductIndustryDto, ProductIndustryDto>()
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => GenerateSlug(src.Name)))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        // Projections
        CreateMap<ProductProjection_AllDto, ProductEntity>().ReverseMap();

        // HTTP Dtos
        CreateMap<CreateProductDto, ProductEntity>();
        CreateMap<UpdateProductDetailsDto, ProductDto>();
    }
    
    private static string GenerateSlug(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return string.Empty;
            
        return name.ToLowerInvariant()
                   .Replace(" ", "-")
                   .Replace("_", "-")
                   .Trim('-');
    }
}
