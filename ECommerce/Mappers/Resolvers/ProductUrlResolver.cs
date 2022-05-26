﻿using AutoMapper;
using Core.Entities;
using ECommerce.Dto;

namespace ECommerce.Mappers.Resolvers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{configuration["ApiUrl"]}/{source.PictureUrl}";
            }

            return null;
        }
    }
}
