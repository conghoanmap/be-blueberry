using blueberry.Data;
using blueberry.Helpers;
using blueberry.Interfaces;
using blueberry.Models;
using Microsoft.EntityFrameworkCore;

namespace blueberry.Services
{
    public class ProductService : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        // private readonly IDbContextTransaction _transaction;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CountProductsAsync()
        {
            return await _context.Products.CountAsync();
        }


        public async Task<Product> CreateAsync(Product product)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Products.AddAsync(product);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return product;
                }
                catch (System.Exception)
                {
                    await transaction.RollbackAsync();
                    return null;
                }
            }
        }

        public async Task<Product?> DeleteAsync(string id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var product = await _context.Products.FindAsync(id);
                    if (product == null)
                    {
                        return null;
                    }
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return product;
                }
                catch (System.Exception)
                {
                    await transaction.RollbackAsync();
                    return null;
                }
            }
        }

        public Task<string> GenerateProductIdAsync()
        {
            Random random = new Random();
            int min = 10000000;
            int max = 99999999;

            // Tạo số ngẫu nhiên trong khoảng từ min đến max (bao gồm cả min, không bao gồm max)
            int randomNumber = random.Next(min, max + 1);
            return Task.FromResult("#" + randomNumber.ToString());
        }


        public async Task<List<Product>> getAllAsync(QueryObject query)
        {
            var products = _context.Products.Include(u => u.Unit)
            .Include(c => c.Color)
            .Include(c => c.Category)
            .Include(a => a.Assesses)
            .AsQueryable();
            // Lọc theo danh mục
            if (query.CategoryId != null)
            {
                if(query.CategoryId != 0)
                {
                    products = products.Where(product => product.CategoryId == query.CategoryId);
                }
            }
            // Lọc theo tên
            if (!string.IsNullOrWhiteSpace(query.ProductName))
            {
                products = products.Where(product => product.ProductName.Contains(query.ProductName));
            }
            // Sắp xếp
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("ProductId", StringComparison.OrdinalIgnoreCase))
                {
                    products = query.IsDecsending ? products.OrderByDescending(product => product.ProductId) : products.OrderBy(product => product.ProductId);
                }
                if (query.SortBy.Equals("ProductName", StringComparison.OrdinalIgnoreCase))
                {
                    products = query.IsDecsending ? products.OrderByDescending(product => product.ProductName) : products.OrderBy(product => product.ProductName);
                }
                if (query.SortBy.Equals("Price", StringComparison.OrdinalIgnoreCase))
                {
                    products = query.IsDecsending ? products.OrderByDescending(product => product.Price) : products.OrderBy(product => product.Price);
                }
                if (query.SortBy.Equals("Discount", StringComparison.OrdinalIgnoreCase))
                {
                    products = query.IsDecsending ? products.OrderByDescending(product => product.Discount) : products.OrderBy(product => product.Discount);
                }
                if (query.SortBy.Equals("Inventory", StringComparison.OrdinalIgnoreCase))
                {
                    products = query.IsDecsending ? products.OrderByDescending(product => product.Inventory) : products.OrderBy(product => product.Inventory);
                }
                if (query.SortBy.Equals("ValueUnit", StringComparison.OrdinalIgnoreCase))
                {
                    products = query.IsDecsending ? products.OrderByDescending(product => product.ValueUnit) : products.OrderBy(product => product.ValueUnit);
                }
            }
            
            // Phân trang
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await products.Skip(skipNumber).Take(query.PageSize).ToListAsync();

            // return await products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(string id)
        {
            return await _context.Products
            .Include(u => u.Unit)
            .Include(c => c.Color)
            .Include(c => c.Category)
            .Include(a => a.Assesses)
            .FirstOrDefaultAsync(x => x.ProductId == id);
        }

        public async Task<Product?> UpdateAsync(string id, Product productDto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
                    if (product == null)
                    {
                        return null;
                    }
                    product.ProductName = productDto.ProductName;
                    product.Price = productDto.Price;
                    product.Discount = productDto.Discount;
                    product.Inventory = productDto.Inventory;
                    product.ValueUnit = productDto.ValueUnit;
                    product.CategoryId = productDto.CategoryId;
                    product.ColorId = productDto.ColorId;
                    product.UnitId = productDto.UnitId;
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return product;
                }
                catch (System.Exception)
                {
                    await transaction.RollbackAsync();
                    return null;
                }
            }
        }

    }
}