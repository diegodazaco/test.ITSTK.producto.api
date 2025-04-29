using test.ITSTK.producto.api.Models;

namespace test.ITSTK.producto.api.AppServices
{
    public interface IProductoAppServices
    {
        public Task<List<Producto>> GetAll();
        public Task<Producto> GetById(int id);
        public Task<Producto> Create(Producto producto);
        public Task<Producto> Update(Producto producto);
        public Task<bool> Delete(int id);
    }
}
