using Microsoft.AspNetCore.Mvc;
using test.ITSTK.producto.api.AppServices;
using test.ITSTK.producto.api.Models;

namespace test.ITSTK.producto.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController(IProductoAppServices productoAppServices, ILogger<ProductoController> logger) : ControllerBase
    {
        private readonly ILogger<ProductoController> _logger = logger;
        private readonly IProductoAppServices _productoAppServices = productoAppServices;
        
        [HttpGet(Name = "GetAll")]
        public async Task<List<Producto>> GetAll()
        {
            List<Producto> productos = await _productoAppServices.GetAll();
            if (productos == null || productos.Count == 0)
            {
                _logger.LogWarning("No se encontraron productos.");
                return new List<Producto>();
            }
            return productos;
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<Producto> GetById(int id)
        {
            Producto producto = await _productoAppServices.GetById(id);
            if (producto == null)
            {
                _logger.LogWarning($"No se encontró el producto con ID {id}.");
                return null;
            }
            return producto;
        }

        [HttpPost(Name = "Create")]
        public async Task<Producto> Create([FromBody] Producto producto)
        {
            Producto createdProducto = await _productoAppServices.Create(producto);
            if (createdProducto == null)
            {
                _logger.LogError("Error al crear el producto.");
                return null;
            }
            return createdProducto;
        }

        [HttpPut("{id}", Name = "Update")]
        public async Task<Producto> Update([FromBody] Producto producto)
        {
            Producto updatedProducto = await _productoAppServices.Update(producto);
            if (updatedProducto == null)
            {
                _logger.LogError("Error al actualizar el producto.");
                return null;
            }
            return updatedProducto;
        }

        [HttpDelete("{id}", Name = "Delete")]
        public async Task<bool> Delete(int id)
        {
            bool result = await _productoAppServices.Delete(id);
            if (!result)
            {
                _logger.LogError($"Error al eliminar el producto con ID {id}.");
                return false;
            }
            return true;
        }
    }
}
