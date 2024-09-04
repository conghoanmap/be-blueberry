using blueberry.Dtos.Product;
using blueberry.Models;

namespace blueberry.Mappers
{
    public static class ProductMapper
    {
        public static ProductDisplay ModelToDisplay(this Product productModel)
        {
            return new ProductDisplay
            {
                ProductId = productModel.ProductId,
                ProductName = productModel.ProductName,
                ProductImage = productModel.ProductImage,
                Price = productModel.Price,
                Discount = productModel.Discount,
                Inventory = productModel.Inventory,
                ValueUnit = productModel.ValueUnit,
                Description = productModel.Description,
                Unit = productModel.Unit,
                Color = productModel.Color,
                Category = productModel.Category?.ModelToDisplay(),
                Assesses = productModel.Assesses.Select(assess => assess.ModelToDisplay()).ToList()
            };
        }

        public static Product RequestToModel(this ProductRequest productRequest)
        {
            return new Product
            {
                ProductName = productRequest.ProductName,
                ProductImage = productRequest.ProductImage,
                Price = productRequest.Price,
                Discount = productRequest.Discount,
                Inventory = productRequest.Inventory,
                ValueUnit = productRequest.ValueUnit,
                Description = productRequest.Description,
                UnitId = productRequest.UnitId,
                ColorId = productRequest.ColorId,
                CategoryId = productRequest.CategoryId
            };
        }
    }
}