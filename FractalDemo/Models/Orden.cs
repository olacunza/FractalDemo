using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FractalDemo.Models
{
    public class Orden
    {
        [Key]
        public int Orden_Id { get; set; }
        [DisplayName("N°")]
        public int NumOrden { get; set; }
        [DisplayName("Status")]
        public string EstadoPedido { get; set; }
        [DisplayName("Consumer")]
        public string Cliente { get; set; }
        [DisplayName("Date")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
        [DisplayName("Quantity")]
        public int? CantidadProducto { get; set; }
        [DisplayName("SubTotal")]
        public double? MontoInicial { get; set; }
        public double? IGV { get; set; }
        [DisplayName("Total")]
        [DefaultValue(0)]
        public double? MontoFinal { get; set; }
        public ICollection<OrdenProducto>? OrdenProducto { get; set; }
    }
}
