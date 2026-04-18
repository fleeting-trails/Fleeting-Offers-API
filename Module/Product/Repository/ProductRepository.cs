using AutoMapper;
using AutoMapper.QueryableExtensions;
using FleetingOffers.Attributes;
using FleetingOffers.Http;
using Microsoft.EntityFrameworkCore;
using static FleetingOffers.AppDbContext;

namespace FleetingOffers.Module.Product;

[ScopedService]
public class ProductRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public ProductRepository(AppDbContext context, IMapper mapper)
    {
        _dbContext = context;
        _mapper = mapper;
    }

    private IQueryable<ProductEntity> GetProductsWithAllIncludes(IQueryable<ProductEntity> query)
    {
        return query
            .Include(p => p.Category).ThenInclude(c => c.Image)
            .Include(p => p.SubCategory).ThenInclude(s => s.Image)
            .Include(p => p.Deal).ThenInclude(d => d.Image)
            .Include(p => p.CoverImage)
            .Include(p => p.ThumbnailImage)
            .Include(p => p.Tags)
            .Include(p => p.AdditionalImages).ThenInclude(a => a.Image);
    }

    // Product CRUD Operations
    public async Task<ProductProjection_AllDto?> GetOwnProductWithAllAsync(string userId, string id)
    {
        var productDto = await GetProductsWithAllIncludes(
            _dbContext.Products.AsQueryable()
                .Where(p => p.Id == id && (p.Owners.Any(o => o.UserId == userId) || p.CreatedById == userId))
        ).ProjectTo<ProductProjection_AllDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return productDto;
    }

    public async Task<ProductProjection_AllDto?> GetProductWithAllAsync(string id)
    {
        var productDto = await GetProductsWithAllIncludes(
            _dbContext.Products.AsQueryable().Where(p => p.Id == id)
        ).ProjectTo<ProductProjection_AllDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return productDto;
    }

    public async Task<PaginatedResult<ProductDto>> GetOwnProductsPaginatedAsync(string userId, int page, int pageSize)
    {
        var query = _dbContext.Products
            .AsQueryable()
            .Where(p => p.Owners.Any(o => o.UserId == userId) || p.CreatedById == userId)
            .OrderByDescending(p => p.CreatedAt);

        var totalItems = await query.CountAsync();
        
        var items = await GetProductsWithAllIncludes(query)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new PaginatedResult<ProductDto>
        {
            Items = items,
            TotalItems = totalItems,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<PaginatedResult<ProductDto>> GetProductsPaginatedAsync(int page, int pageSize)
    {
        var query = _dbContext.Products
            .AsQueryable()
            .OrderByDescending(p => p.CreatedAt);

        var totalItems = await query.CountAsync();
        
        var items = await GetProductsWithAllIncludes(query)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new PaginatedResult<ProductDto>
        {
            Items = items,
            TotalItems = totalItems,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<ProductDto> CreateProductAsync(
        string createdBy,
        ProductDto dto,
        List<ProductOwnerDto> owners,
        List<string>? tags = null
    )
    {
        ProductEntity productEntity = _mapper.Map<ProductEntity>(dto);
        List<ProductOwnerEntity> ownersEntity = new();

        productEntity.CreatedById = createdBy;
        _dbContext.Add(productEntity);

        await _dbContext.SaveChangesWithUploadsAsync(new[]
        {
            new SaveChangesWithUploadsAsyncPropsDto("CoverImageId", new[] { "image/jpeg", "image/png" }),
            new SaveChangesWithUploadsAsyncPropsDto("ThumbnailImageId", new[] { "image/jpeg", "image/png", "image/webp" }),
        });

        foreach (var owner in owners)
        {
            owner.ProductId = productEntity.Id;
            ownersEntity.Add(_mapper.Map<ProductOwnerEntity>(owner));
        }

        _dbContext.AddRange(ownersEntity);
        
        if (tags?.Any() == true)
        {
            var tagEntities = tags
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .Distinct()
                .Select(t => new ProductTagEntity 
                { 
                    Tag = t.Trim(),
                    ProductId = productEntity.Id
                })
                .ToList();
            _dbContext.AddRange(tagEntities);
        }
        
        await _dbContext.SaveChangesAsync();

        var created = await GetProductsWithAllIncludes(
            _dbContext.Products.AsQueryable().Where(p => p.Id == productEntity.Id)
        ).FirstOrDefaultAsync();

        return _mapper.Map<ProductDto>(created);
    }

    public async Task<ProductDto> UpdateProductAsync(string userId, ProductDto dto)
    {
        var entity = await _dbContext.Products
            .Where(p => p.Id == dto.Id && (p.Owners.Any(o => o.UserId == userId) || p.CreatedById == userId))
            .FirstOrDefaultAsync();

        if (entity == null)
            throw new KeyNotFoundException("Product not found");

        entity.Title = dto.Title;
        entity.Subtitle = dto.Subtitle;
        entity.Description = dto.Description;
        entity.Price = dto.Price;
        entity.DealId = dto.DealId;
        entity.UpdatedAt = DateTime.UtcNow;

        _dbContext.Products.Update(entity);
        await _dbContext.SaveChangesAsync();

        var updated = await GetProductsWithAllIncludes(
            _dbContext.Products.AsQueryable().Where(p => p.Id == dto.Id)
        ).FirstOrDefaultAsync();

        return _mapper.Map<ProductDto>(updated);
    }

    public async Task DeleteProductAsync(string userId, string id)
    {
        var entity = await _dbContext.Products
            .Where(p => p.Id == id && (p.Owners.Any(o => o.UserId == userId) || p.CreatedById == userId))
            .FirstOrDefaultAsync();

        if (entity == null)
            throw new KeyNotFoundException("Product not found");

        _dbContext.Products.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteProductByAdminAsync(string id)
    {
        var entity = await _dbContext.Products
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();

        if (entity == null)
            throw new KeyNotFoundException("Product not found");

        _dbContext.Products.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    // Category Operations (SUPER_ADMIN only)
    public async Task<ProductCategoryDto> CreateCategoryAsync(ProductCategoryDto dto)
    {
        var entity = _mapper.Map<ProductCategoryEntity>(dto);
        entity.Slug = GenerateSlug(dto.Name);
        
        _dbContext.ProductCategories.Add(entity);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<ProductCategoryDto>(entity);
    }

    public async Task<ProductCategoryDto> UpdateCategoryAsync(string id, ProductCategoryDto dto)
    {
        var entity = await _dbContext.ProductCategories.FindAsync(id);
        if (entity == null)
            throw new KeyNotFoundException("Category not found");

        entity.Name = dto.Name;
        entity.Slug = GenerateSlug(dto.Name);
        entity.ImageId = dto.ImageId;

        _dbContext.ProductCategories.Update(entity);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<ProductCategoryDto>(entity);
    }

    public async Task<ProductCategoryDto?> GetCategoryAsync(string id)
    {
        var entity = await _dbContext.ProductCategories.FindAsync(id);
        return entity != null ? _mapper.Map<ProductCategoryDto>(entity) : null;
    }

    public async Task<List<ProductCategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await _dbContext.ProductCategories
            .OrderBy(c => c.Name)
            .ProjectTo<ProductCategoryDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return categories;
    }

    public async Task DeleteCategoryAsync(string id)
    {
        var entity = await _dbContext.ProductCategories.FindAsync(id);
        if (entity == null)
            throw new KeyNotFoundException("Category not found");

        _dbContext.ProductCategories.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    // Industry Operations (SUPER_ADMIN only)
    public async Task<ProductIndustryDto> CreateIndustryAsync(ProductIndustryDto dto)
    {
        var entity = _mapper.Map<ProductIndustryEntity>(dto);
        entity.Slug = GenerateSlug(dto.Name);
        
        _dbContext.ProductIndustries.Add(entity);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<ProductIndustryDto>(entity);
    }

    public async Task<ProductIndustryDto> UpdateIndustryAsync(string id, ProductIndustryDto dto)
    {
        var entity = await _dbContext.ProductIndustries.FindAsync(id);
        if (entity == null)
            throw new KeyNotFoundException("Industry not found");

        entity.Name = dto.Name;
        entity.Slug = GenerateSlug(dto.Name);
        entity.ImageId = dto.ImageId;

        _dbContext.ProductIndustries.Update(entity);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<ProductIndustryDto>(entity);
    }

    public async Task<ProductIndustryDto?> GetIndustryAsync(string id)
    {
        var entity = await _dbContext.ProductIndustries.FindAsync(id);
        return entity != null ? _mapper.Map<ProductIndustryDto>(entity) : null;
    }

    public async Task<List<ProductIndustryDto>> GetAllIndustriesAsync()
    {
        var industries = await _dbContext.ProductIndustries
            .OrderBy(i => i.Name)
            .ProjectTo<ProductIndustryDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return industries;
    }

    public async Task DeleteIndustryAsync(string id)
    {
        var entity = await _dbContext.ProductIndustries.FindAsync(id);
        if (entity == null)
            throw new KeyNotFoundException("Industry not found");

        _dbContext.ProductIndustries.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    // Deal Operations (SUPER_ADMIN only)
    public async Task<ProductDealDto> CreateDealAsync(ProductDealDto dto)
    {
        var entity = _mapper.Map<ProductDealEntity>(dto);
        entity.Slug = GenerateSlug(dto.Name);

        _dbContext.ProductDeals.Add(entity);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<ProductDealDto>(entity);
    }

    public async Task<ProductDealDto> UpdateDealAsync(string id, ProductDealDto dto)
    {
        var entity = await _dbContext.ProductDeals.FindAsync(id);
        if (entity == null)
            throw new KeyNotFoundException("Deal not found");

        entity.Name = dto.Name;
        entity.Slug = GenerateSlug(dto.Name);
        entity.ImageId = dto.ImageId;

        _dbContext.ProductDeals.Update(entity);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<ProductDealDto>(entity);
    }

    public async Task<ProductDealDto?> GetDealAsync(string id)
    {
        var entity = await _dbContext.ProductDeals.FindAsync(id);
        return entity != null ? _mapper.Map<ProductDealDto>(entity) : null;
    }

    public async Task<List<ProductDealDto>> GetAllDealsAsync()
    {
        var deals = await _dbContext.ProductDeals
            .OrderBy(d => d.Name)
            .ProjectTo<ProductDealDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return deals;
    }

    public async Task DeleteDealAsync(string id)
    {
        var entity = await _dbContext.ProductDeals.FindAsync(id);
        if (entity == null)
            throw new KeyNotFoundException("Deal not found");

        _dbContext.ProductDeals.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    // Helper method
    private string GenerateSlug(string name)
    {
        return name.ToLower().Replace(" ", "-").Replace("_", "-");
    }
}
