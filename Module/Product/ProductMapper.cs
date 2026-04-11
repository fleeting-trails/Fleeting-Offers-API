using AutoMapper;
using FleetingOffers.Module.Upload;

namespace FleetingOffers.Module.Product;

public class ProductMapper : Profile 
{
    public ProductMapper() 
    {
        CreateMap<ProductEntity, ProductDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.SubCategory, opt => opt.MapFrom(src => src.SubCategory))
            .ForMember(dest => dest.Deal, opt => opt.MapFrom(src => src.Deal))
            .ForMember(dest => dest.CoverImage, opt => opt.MapFrom(src => src.CoverImage))
            .ForMember(dest => dest.ThumbnailImage, opt => opt.MapFrom(src => src.ThumbnailImage))
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
            .ForMember(dest => dest.AdditionalImages, opt => opt.MapFrom(src => src.AdditionalImages))
            .ReverseMap();
            
        CreateMap<CreateProductDto, ProductDto>();
        CreateMap<ProductOwnerEntity, ProductOwnerDto>().ReverseMap();

        CreateMap<UploadEntity, UploadDto>().ReverseMap();

        CreateMap<ProductCategoryEntity, ProductCategoryDto>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
            .ReverseMap();
        CreateMap<CreateProductCategoryDto, ProductCategoryDto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => GenerateSlug(src.Name)))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<UpdateProductCategoryDto, ProductCategoryDto>()
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => GenerateSlug(src.Name)))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        CreateMap<ProductIndustryEntity, ProductIndustryDto>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
            .ReverseMap();
        CreateMap<CreateProductIndustryDto, ProductIndustryDto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => GenerateSlug(src.Name)))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<UpdateProductIndustryDto, ProductIndustryDto>()
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => GenerateSlug(src.Name)))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        CreateMap<ProductDealEntity, ProductDealDto>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
            .ReverseMap();
        CreateMap<CreateProductDealDto, ProductDealDto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => GenerateSlug(src.Name)))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<UpdateProductDealDto, ProductDealDto>()
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => GenerateSlug(src.Name)))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        CreateMap<ProductTagEntity, ProductTagDto>().ReverseMap();

        CreateMap<ProductAdditionalImageEntity, ProductAdditionalImageDto>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
            .ReverseMap();

        CreateMap<ProductProjection_AllDto, ProductEntity>().ReverseMap();

        CreateMap<CreateProductDto, ProductDto>()
            .ForMember(dest => dest.Tags, opt => opt.Ignore())
            .ForMember(dest => dest.AdditionalImages, opt => opt.Ignore());
        CreateMap<UpdateProductDetailsDto, ProductDto>()
            .ForMember(dest => dest.Tags, opt => opt.Ignore())
            .ForMember(dest => dest.AdditionalImages, opt => opt.Ignore());
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
