using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        // Interesante porque la expresion en caso que NO enviase BrandId y TypeID quedaria como true y true 
        // lo que retornaria haria que se retornaran todos los registros que es lo que necesitamos
        public ProductWithTypesAndBrandsSpecification(string sort, int? brandId, int? typeId)
            : base(x => (!brandId.HasValue || x.ProductBrandId == brandId) &&
                       (!typeId.HasValue || x.ProductTypeId == typeId))
        {
            this.Includes.Add(d => d.ProductBrand);
            this.Includes.Add(d => d.ProductType);


            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
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
            AddOrderBy(d => d.Name);
        }
        public ProductWithTypesAndBrandsSpecification(int id) : base(d => d.Id == id)
        {
            this.Includes.Add(d => d.ProductBrand);
            this.Includes.Add(d => d.ProductType);
        }
    }
}
