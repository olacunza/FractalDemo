using FractalDemo.Models;

namespace FractalDemo.ViewModel
{
    public class IndexVM: BaseModelo
    {
        public Producto Producto { get; set; }
        public Orden Orden { get; set; }
        public List<Orden> Ordenes { get; set; }
        public List<Producto> Productos { get; set; }
        public List<Producto> Productos2 { get; set; }
    }
}
