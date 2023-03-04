using System.ComponentModel.DataAnnotations;
namespace laboratorio1.Models
{
    public class usuarios
    {
        public int usuarioId { get; set; }
        public int rolId { get; set; }
        public string nombreUsuario { get; set; }
        public string clave { get; set;}
        public string nombre { get; set; }
        public string apellido { get; set;}
    }
}
