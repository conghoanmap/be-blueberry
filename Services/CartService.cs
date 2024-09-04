using blueberry.Data;
using blueberry.Interfaces;
using blueberry.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace blueberry.Services
{
    public class CartService : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Cart> CreateAsync(Cart cart)
        {
            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == cart.ProductId);
                    if (product == null)
                    {
                        return null;
                    }
                    // Kiểm tra tồn tại giỏ hàng với productId và userId
                    var existingCart = await _context.Carts.FirstOrDefaultAsync(c => c.ProductId == cart.ProductId && c.AppUserId == cart.AppUserId);
                    // Nếu đã tồn tại giỏ hàng thì cộng thêm số lượng
                    if (existingCart != null)
                    {
                        existingCart.Quantity += cart.Quantity;
                        existingCart.TotalPrice = existingCart.Quantity * product.Price;
                        await _context.SaveChangesAsync();
                        return existingCart;
                    }
                    cart.CartId = await GenerateCartId();
                    cart.TotalPrice = cart.Quantity * product.Price;
                    await _context.Carts.AddAsync(cart);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return cart;
                }
                catch (System.Exception e)
                {
                    Console.WriteLine(e.Message);
                    await transaction.RollbackAsync();
                    return null;
                }
            }
        }

        public async Task<Cart?> DeleteAsync(string id)
        {
            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var cartToDelete = await _context.Carts.FirstOrDefaultAsync(cart => cart.CartId == id);
                    if (cartToDelete == null)
                    {
                        return null;
                    }
                    _context.Carts.Remove(cartToDelete);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return cartToDelete;
                }
                catch (System.Exception)
                {
                    await transaction.RollbackAsync();
                    return null;
                }
            }
        }

        public async Task<List<Cart>> getAllAsync(string userId)
        {
            return await _context.Carts.Include(p => p.Product).Where(c => c.AppUserId == userId).ToListAsync();
        }

        public async Task<Cart?> GetByIdAsync(string id)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(cart => cart.CartId == id);
            if (cart == null)
            {
                return null;
            }
            return cart;
        }

        // public async Task<Cart?> UpdateAsync(string id, int quantity)
        // {
        //     using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
        //     {
        //         try
        //         {
        //             var cart = await _context.Carts.FirstOrDefaultAsync(c => c.CartId == id);
        //             if (cart == null)
        //             {
        //                 return null;
        //             }
        //             cart.Quantity = quantity;
        //             cart.TotalPrice = cart.Quantity * cart.Product.Price;
        //             await _context.SaveChangesAsync();
        //             await transaction.CommitAsync();
        //             return cart;
        //         }
        //         catch (System.Exception)
        //         {
        //             await transaction.RollbackAsync();
        //             return null;
        //         }
        //     }
        // }
        public Task<string> GenerateCartId()
        {
            Random random = new Random();
            int min = 10000000;
            int max = 99999999;

            // Tạo số ngẫu nhiên trong khoảng từ min đến max (bao gồm cả min, không bao gồm max)
            int randomNumber = random.Next(min, max + 1);
            return Task.FromResult("#CART" + randomNumber.ToString());
        }

        public async Task<bool> PlusQuantityAsync(string id)
        {
            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var cart = await _context.Carts.FirstOrDefaultAsync(c => c.CartId == id);
                    if (cart == null)
                    {
                        return false;
                    }
                    cart.Quantity++;
                    var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == cart.ProductId);
                    cart.TotalPrice = cart.Quantity * product.Price;
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return true;
                }
                catch (System.Exception)
                {
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        }
        public async Task<bool> MinusQuantityAsync(string id)
        {
            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var cart = await _context.Carts.FirstOrDefaultAsync(c => c.CartId == id);
                    if (cart == null)
                    {
                        return false;
                    }
                    if (cart.Quantity == 1)
                    {
                        return false;
                    }
                    cart.Quantity--;
                    var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == cart.ProductId);
                    cart.TotalPrice = cart.Quantity * product.Price;
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return true;
                }
                catch (System.Exception)
                {
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        }

    }
}