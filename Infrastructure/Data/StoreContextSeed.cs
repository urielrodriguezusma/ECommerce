using Core.Entities;
using Core.Entities.OrdersAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public static class StoreContextSeed
    {
        public async static Task SeedAsync(StoreContext storeContext)
        {

            if (!storeContext.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands != null)
                {
                    foreach (var brand in brands)
                    {
                        storeContext.ProductBrands.Add(brand);
                    }
                }

                await storeContext.SaveChangesAsync();
            }

            if (!storeContext.ProductTypes.Any())
            {
                var productTypesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");

                var productTypes = JsonSerializer.Deserialize<List<ProductType>>(productTypesData);
                if (productTypes != null)
                {
                    foreach (var prodType in productTypes)
                    {
                        storeContext.ProductTypes.Add(prodType);
                    }
                }

                await storeContext.SaveChangesAsync();
            }

            if (!storeContext.Products.Any())
            {
                var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products != null)
                {
                    foreach (var product in products)
                    {
                        storeContext.Products.Add(product);
                    }
                }

                await storeContext.SaveChangesAsync();
            }

            if (!storeContext.DeliveryMethods.Any())
            {
                var deliveryData = File.ReadAllText("../Infrastructure/Data/SeedData/delivery.json");

                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                if (deliveryMethods != null)
                {
                    foreach (var delivery in deliveryMethods)
                    {
                        storeContext.DeliveryMethods.Add(delivery);
                    }
                }

                await storeContext.SaveChangesAsync();
            }
        }
    }
}
