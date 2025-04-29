using Microsoft.EntityFrameworkCore;
using test.ITSTK.producto.api.Data;
using test.ITSTK.producto.api.Models;

namespace test.ITSTK.producto.api.AppServices
{
    public class ProductoAppServices : IProductoAppServices
    {
        private readonly DataContext _context;
        private readonly ILogger<ProductoAppServices> _logger;
        public ProductoAppServices(DataContext context, ILogger<ProductoAppServices> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<List<Producto>> GetAll()
        {
            try
            {
                List<Producto> productos = await _context.Productos.ToListAsync();

                if (!productos.Any())
                    return null!;

                return productos;
            }
            catch (Exception)
            {
                return null!;
            }
        }
        public async Task<Producto> GetById(int id)
        {
            try
            {
                Producto producto = await _context.Productos.FindAsync(id);
                if (producto == null)
                    return null!;
                
                return producto;
            }
            catch (Exception)
            {
                return null!;
            }
        }
        public async Task<Producto> Create(Producto producto)
        {
            try
            {
                if (!ValidarProducto(producto))
                    return null!;

                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();

                return producto;
            }
            catch (Exception)
            {
                return null!;
            }
        }
        public async Task<Producto> Update(Producto producto)
        {
            try
            {
                if (!ValidarProducto(producto))
                    return null!;

                Producto? productoExistente = await _context.Productos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == producto.Id);
                if (productoExistente == null)
                    return null!;

                _context.Productos.Update(producto);
                await _context.SaveChangesAsync();
                return producto;
            }
            catch (Exception)
            {
                return null!;
            }
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                Producto? producto = await _context.Productos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                if (producto == null)
                    return false;

                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool ValidarProducto(Producto producto)
        {
            if (producto == null)
            {
                _logger.LogWarning("El producto no puede ser nulo.");
                return false;
            }
            if (string.IsNullOrEmpty(producto.Nombre))
            {
                _logger.LogWarning("El nombre del producto no puede estar vacío.");
                return false;
            }
            else if (producto.Nombre.Length > 100)
            {
                _logger.LogWarning("El nombre del producto no puede tener un nombre mayor a 100 caracteres.");
                return false;
            }

            if (producto.Precio <= 0)
            {
                _logger.LogWarning("El precio del producto debe ser mayor a 0.");
                return false;
            }
            return true;
        }
    }
}
