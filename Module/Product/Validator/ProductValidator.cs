using FleetingOffers.Attributes;

namespace FleetingOffers.Module.Product;

[ScopedService]
public class ProductValidator 
{
    // Product Validations
    public void ValidateCreateProduct(CreateProductDto dto) 
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
        {
            throw new Exception("PRODUCT_VALIDATION: Product title cannot be empty");
        }
    }
    
    public void ValidateCreateProductAdmin(CreateProductAdminDto dto) 
    {
        ValidateCreateProduct(dto.Product);
    }
    
    public void ValidateUpdateProduct(UpdateProductDetailsDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
        {
            throw new Exception("PRODUCT_VALIDATION: Product title cannot be empty");
        }
    }
    
    // Product Category Validations
    public void ValidateCreateProductCategory(CreateProductCategoryDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new Exception("CATEGORY_VALIDATION: Category name cannot be empty");
        }
    }

    public void ValidateUpdateProductCategory(UpdateProductCategoryDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new Exception("CATEGORY_VALIDATION: Category name cannot be empty");
        }
    }

    // Product Industry Validations
    public void ValidateCreateProductIndustry(CreateProductIndustryDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new Exception("INDUSTRY_VALIDATION: Industry name cannot be empty");
        }
    }

    public void ValidateUpdateProductIndustry(UpdateProductIndustryDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new Exception("INDUSTRY_VALIDATION: Industry name cannot be empty");
        }
    }

    // Product Deal Validations
    public void ValidateCreateProductDeal(CreateProductDealDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new Exception("DEAL_VALIDATION: Deal name cannot be empty");
        }
    }

    public void ValidateUpdateProductDeal(UpdateProductDealDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new Exception("DEAL_VALIDATION: Deal name cannot be empty");
        }
    }
}
