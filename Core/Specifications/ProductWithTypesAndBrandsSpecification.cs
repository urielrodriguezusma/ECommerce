using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductWithTypesAndBrandsSpecification(string sort)
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
                        AddOrderBy(d=>d.Name);
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
