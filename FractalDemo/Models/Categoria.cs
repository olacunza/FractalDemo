using System.ComponentModel.DataAnnotations;

namespace FractalDemo.Models
{
    public class Categoria
    {
        [Key]
        public int Categoria_Id { get; set; }
        public string Nombre { get; set; }
        public List<Producto>? Producto { get; set; }
    }
}
