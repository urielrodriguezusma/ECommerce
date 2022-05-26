using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using ECommerce.Dto;
using ECommerce.Errors;
using ECommerce.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> productRepository;
        private readonly IGenericRepository<ProductBrand> brandRepository;
        private readonly IMapper mapper;

        public ProductController(IGenericRepository<Product> productRepository,
                                 IGenericRepository<ProductBrand> brandRepository,
                                 IMapper mapper)
        {
            this.productRepository = productRepository;
            this.brandRepository = brandRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> Get()
        {
            var productSpecification = new ProductWithTypesAndBrandsSpecification();
            var products = await this.productRepository.ListAsync(productSpecification);
            var resp = this.mapper.Map<ProductToReturnDto[]>(products);
            //var products = await this.productRepository.GetAllAsync();
            return this.Ok(resp);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var productSpecification = new ProductWithTypesAndBrandsSpecification(id);
            var product = await this.productRepository.GetEntityWithSpec(productSpecification);
            if (product == null)
            {
                return this.NotFound(new ApiResponse(StatusCodes.Status404NotFound));
            }
            var resp = this.mapper.Map<ProductToReturnDto>(product);
            //var product = await this.productRepository.GetByIdAsync(id);
            return this.Ok(resp);
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await this.brandRepository.GetAllAsync();
            return this.Ok(brands);
        }
    }
}
