using System.ComponentModel.DataAnnotations;

namespace test.ITSTK.producto.api.Models
{
    public class Producto
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre debe ser obligatorio y tener un máximo de 100 caracteres")]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El precio debe ser mayor a 0")]
        [Range(0.01, 9999999.99, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }
        public string? Descripcion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
