using AutoMapper;
using FleetingOffers.Attributes;
using FleetingOffers.Http;

namespace FleetingOffers.Module.Product;

[ScopedService]
public class ProductControllerService 
{
    private readonly ProductRepository _repository;
    private readonly ProductValidator _validator;
    private readonly IMapper _mapper;

    public ProductControllerService(
        ProductRepository repository, 
        ProductValidator validator,
        IMapper mapper
    ) 
    {
        _repository = repository;
        _validator = validator;
        _mapper = mapper;
    }

    // Product Operations
    public async Task<ProductDto> GetProductAsync(string id) 
    {
        var product = await _repository.GetProductWithAllAsync(id);
        if (product == null) 
        {
            throw new Exception("PRODUCT_404: No Product found with this ID");
        }
        return product;
    }

    public async Task<ProductDto> GetOwnProductAsync(string userId, string id) 
    {
        var product = await _repository.GetOwnProductWithAllAsync(userId, id);
        if (product == null) 
        {
            throw new Exception("PRODUCT_404: No Product found with this ID");
        }
        return product;
    }

    public async Task<PaginatedResult<ProductDto>> GetOwnProductsPaginatedAsync(string userId, int page, int pageSize) 
    {
        var result = await _repository.GetOwnProductsPaginatedAsync(userId, page, pageSize);
        if (result == null) 
        {
            throw new Exception("FAILED: Failed to fetch list of products");
        }
        return result;
    }

    public async Task<PaginatedResult<ProductDto>> GetAllProductsPaginatedAsync(int page, int pageSize) 
    {
        var result = await _repository.GetProductsPaginatedAsync(page, pageSize);
        if (result == null) 
        {
            throw new Exception("FAILED: Failed to fetch list of products");
        }
        return result;
    }

    public async Task CreateProductAsync(string createdBy, CreateProductDto dto) 
    {
        _validator.ValidateCreateProduct(dto);
        ProductOwnerDto owner = new ProductOwnerDto 
        {
            UserId = createdBy,
            OwnershipType = PRODUCT_OWNERSHIP.OWNER
        };
        var productDto = _mapper.Map<ProductDto>(dto);
        await _repository.CreateProductAsync(createdBy, productDto, [owner]);
    }

    public async Task CreateProductByAdminAsync(string createdBy, CreateProductAdminDto dto) 
    {
        _validator.ValidateCreateProductAdmin(dto);
        var productDto = _mapper.Map<ProductDto>(dto.Product);
        var productOwners = new List<ProductOwnerDto>();
        
        foreach (var owner in dto.Owners) 
        {
            productOwners.Add(new ProductOwnerDto() 
            {
                UserId = owner.UserId,
                OwnershipType = owner.OwnershipType
            });
        }
        
        await _repository.CreateProductAsync(createdBy, productDto, productOwners);
    }

    public async Task UpdateProductDetailsAsync(string userId, UpdateProductDetailsDto dto)
    {
        _validator.ValidateUpdateProduct(dto);
        var productDto = _mapper.Map<ProductDto>(dto);
        await _repository.UpdateProductAsync(userId, productDto);
    }

    public async Task DeleteProductAsync(string userId, string id)
    {
        await _repository.DeleteProductAsync(userId, id);
    }
    
    public async Task DeleteProductByAdminAsync(string id)
    {
        await _repository.DeleteProductByAdminAsync(id);
    }

    // Category Operations (SUPER_ADMIN)
    public async Task<ProductCategoryDto> CreateCategoryAsync(CreateProductCategoryDto dto)
    {
        _validator.ValidateCreateProductCategory(dto);
        var categoryDto = _mapper.Map<ProductCategoryDto>(dto);
        return await _repository.CreateCategoryAsync(categoryDto);
    }

    public async Task<ProductCategoryDto> UpdateCategoryAsync(string id, UpdateProductCategoryDto dto)
    {
        _validator.ValidateUpdateProductCategory(dto);
        var categoryDto = _mapper.Map<ProductCategoryDto>(dto);
        categoryDto.Id = id;
        return await _repository.UpdateCategoryAsync(id, categoryDto);
    }

    public async Task<ProductCategoryDto> GetCategoryAsync(string id)
    {
        var category = await _repository.GetCategoryAsync(id);
        if (category == null)
        {
            throw new Exception("CATEGORY_404: No Category found with this ID");
        }
        return category;
    }

    public async Task<List<ProductCategoryDto>> GetAllCategoriesAsync()
    {
        return await _repository.GetAllCategoriesAsync();
    }

    public async Task DeleteCategoryAsync(string id)
    {
        await _repository.DeleteCategoryAsync(id);
    }

    // Industry Operations (SUPER_ADMIN)
    public async Task<ProductIndustryDto> CreateIndustryAsync(CreateProductIndustryDto dto)
    {
        _validator.ValidateCreateProductIndustry(dto);
        var industryDto = _mapper.Map<ProductIndustryDto>(dto);
        return await _repository.CreateIndustryAsync(industryDto);
    }

    public async Task<ProductIndustryDto> UpdateIndustryAsync(string id, UpdateProductIndustryDto dto)
    {
        _validator.ValidateUpdateProductIndustry(dto);
        var industryDto = _mapper.Map<ProductIndustryDto>(dto);
        industryDto.Id = id;
        return await _repository.UpdateIndustryAsync(id, industryDto);
    }

    public async Task<ProductIndustryDto> GetIndustryAsync(string id)
    {
        var industry = await _repository.GetIndustryAsync(id);
        if (industry == null)
        {
            throw new Exception("INDUSTRY_404: No Industry found with this ID");
        }
        return industry;
    }

    public async Task<List<ProductIndustryDto>> GetAllIndustriesAsync()
    {
        return await _repository.GetAllIndustriesAsync();
    }

    public async Task DeleteIndustryAsync(string id)
    {
        await _repository.DeleteIndustryAsync(id);
    }
}
