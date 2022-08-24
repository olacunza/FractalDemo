using FractalDemo.Models;

namespace FractalDemo.ViewModel
{
    public class IndexVM: BaseModelo
    {
        public Producto Producto { get; set; }
        public List<Producto> Productos { get; set; }
        public List<Producto> Productos2 { get; set; }
    }
}
