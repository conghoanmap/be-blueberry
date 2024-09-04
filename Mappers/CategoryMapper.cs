using blueberry.Dtos.Category;
using blueberry.Models;

namespace blueberry.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDisplay ModelToDisplay(this Category category)
        {
            return new CategoryDisplay
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                // CategoryImage = category.CategoryImage,
                // Products = category.Products.Select(product => product.ModelToDisplay()).ToList()
            };
        }
    }
}