using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibliotecaUGB.Data;
using BibliotecaUGB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BibliotecaUGB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibroController : Controller
    {
         private DatabaseContext _context;
    public LibroController(DatabaseContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Libro>>> ObtenerListaLibros()
    {
        var libros = await _context.Libros.ToListAsync();
        return libros;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Libro>> ObtenerLibroPorID(int id_libro)
    {
        var libros = await _context.Libros.FindAsync(id_libro);
        if(libros==null)
        {
            return NotFound();
        }
        return libros;
    }

    [HttpPost]
    public async Task<ActionResult<Libro>> PublicarLibro(Libro libro)
    {
        _context.Libros.Add(libro);
        await _context.SaveChangesAsync();
        return CreatedAtAction("ObtenerLibroPorID", new{id_libro=libro.LibroID},libro);
    }
    }
}