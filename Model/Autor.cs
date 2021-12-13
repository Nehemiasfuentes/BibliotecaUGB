using System;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaUGB.Models
{
    public class Autor
    {
        [Key]
        public int AutorID { get; set; }
        public string NombresAutor { get; set; }
        public string ApellidosAutor { get; set; }
        public string EdadAutor { get; set; }
        public string PaisAutor { get; set; }
    }
}