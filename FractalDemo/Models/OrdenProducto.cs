using System.ComponentModel.DataAnnotations.Schema;

namespace FractalDemo.Models
{
    public class OrdenProducto
    {
        [ForeignKey("Orden")]
        public int Orden_Id { get; set; }
        [ForeignKey("Producto")]
        public int Producto_Id { get; set; }
        public Orden Orden { get; set; }
        public Producto Producto { get; set; }
    }
}
