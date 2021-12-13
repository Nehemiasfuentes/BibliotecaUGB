using System.ComponentModel.DataAnnotations;
using System;

namespace BibliotecaUGB.Models
{
    public class Libro
    {
        [Key]
        public int LibroID { get; set; }
        public string TituloLibro { get; set; }
        public string PortadaLibro { get; set; }
        public string ISBLibro { get; set; }
        public string CategoriaLibro { get; set; }
        public string Descripcion { get; set; }
        public int AutorID { get; set; }
    }
}