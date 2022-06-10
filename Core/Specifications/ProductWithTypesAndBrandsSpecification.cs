using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        // Interesante porque la expresion en caso que NO enviase BrandId y TypeID quedaria como true y true 
        // lo que retornaria haria que se retornaran todos los registros que es lo que necesitamos
        public ProductWithTypesAndBrandsSpecification(ProductSpecParams productSpecParams)
            : base(x => (!productSpecParams.BrandId.HasValue || x.ProductBrandId == productSpecParams.BrandId) &&
                       (!productSpecParams.TypeId.HasValue || x.ProductTypeId == productSpecParams.TypeId))
        {
            this.Includes.Add(d => d.ProductBrand);
            this.Includes.Add(d => d.ProductType);
            this.AddOrderBy(d => d.Name);
            this.ApplyPaging(productSpecParams.PageSize * (productSpecParams.PageIndex - 1), productSpecParams.PageSize);


            if (!string.IsNullOrEmpty(productSpecParams.Sort))
            {
                switch (productSpecParams.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(d => d.Name);
                        break;
                }
            }

        }
        public ProductWithTypesAndBrandsSpecification(int id) : base(d => d.Id == id)
        {
            this.Includes.Add(d => d.ProductBrand);
            this.Includes.Add(d => d.ProductType);
        }
    }
}
