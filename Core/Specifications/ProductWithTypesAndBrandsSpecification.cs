using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductWithTypesAndBrandsSpecification()
        {
            this.Includes.Add(d => d.ProductBrand);
            this.Includes.Add(d => d.ProductType);
        }
        public ProductWithTypesAndBrandsSpecification(int id) : base(d => d.Id == id)
        {
            this.Includes.Add(d => d.ProductBrand);
            this.Includes.Add(d => d.ProductType);
        }
    }
}
