using FractalDemo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FractalDemo.ViewModel
{
    public class OrdenProductoVM
    {
        public OrdenProducto OrdenProducto { get; set; }
        public Orden Orden { get; set; }
        //public Producto Producto { get; set; }
        public IEnumerable<OrdenProducto> ListaOrdenProductos { get; set; }
        public IEnumerable<SelectListItem> ListaProductos { get; set; }
        //public IEnumerable<SelectListItem> ListaOrdenes { get; set; }
    }
}
