using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace FractalDemo.Models
{
    public class Producto
    {
        [Key]
        public int Producto_Id { get; set; }
        [DisplayName("Name")]
        public string NombreProducto { get; set; }
        [DisplayName("Price")]
        public double PrecioUnitario { get; set; }
        [DisplayName("Status")]
        public Boolean EstadoProducto { get; set; }
        [DisplayName("Category")]
        [ForeignKey("Categoria")]
        public int Categoria_Id { get; set; }
        [DisplayName("Category")]
        public Categoria? Categoria { get; set; }
        public ICollection<OrdenProducto>? OrdenProducto { get; set; }
    }
}
