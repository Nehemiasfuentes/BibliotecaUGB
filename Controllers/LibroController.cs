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

    /// <summary>
    /// Obteniendo listado de Libros.
    /// </summary>
    /// <remarks>
    /// Mediante este verbo se obtienen todos los ibros registrados que hay en la base de datos.
    /// </remarks>
    [HttpGet]
    public async Task<ActionResult<List<Libro>>> ObtenerListaLibros()
    {
        var libros = await _context.Libros.ToListAsync();
        return libros;
    }

    /// <summary>
    /// Obteniendo datos de Libros mediante el Id de este.
    /// </summary>
    /// <remarks>
    /// Mediante el mismo verbo Get pero ahora pasandole un parametro para realizar una busqueda filtrada por el Id del Libro.
    /// </remarks>
    /// <param name="id_libro">N. Identificador del Libro.</param>
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

    /// <summary>
    /// Creando nuevo registro de un nuevo Libro.
    /// </summary>
    /// <remarks>
    /// Mediante este verbo creamos un nuevo Libro en los registros de la base de datos.
    /// </remarks>
    [HttpPost]
    public async Task<ActionResult<Libro>> PublicarLibro(Libro libro)
    {
        _context.Libros.Add(libro);
        await _context.SaveChangesAsync();
        return CreatedAtAction("ObtenerLibroPorID", new{id_libro=libro.LibroID},libro);
    }

    /// <summary>
    /// Actualizando Datos de un Libro, mediante su Id.
    /// </summary>
    /// <remarks>
    /// Mediante este verbo actualizamos un registro de un Libro en la base de datos mediante el Id.
    /// </remarks>
    /// <param name="id_libro">N. Identificador de un Libro.</param>
    /// <param name="libro"></param>
    [HttpPut("{id}")]
    public async Task<ActionResult<Libro>> EditarLibro(int id_libro, Libro libro)
    {
    if(id_libro!= libro.LibroID)
        {
        return BadRequest();
        }
    _context.Entry(libro).State = EntityState.Modified;
    try
    {
        await _context.SaveChangesAsync();
    }
    catch(DbUpdateConcurrencyException)
    {
        if(!ExisteLibro(id_libro))
        {
            return NotFound();
        }
        else
        {
            throw;
        }
    }
    return CreatedAtAction("ObtenerLibroPorID", new{id_libro=libro.LibroID},libro);
    }

    /// <summary>
    /// ELiminando a un Libro de los registros mediante su Id.
    /// </summary>
    /// <remarks>
    /// COn este verbo podremos eliminar el registro de un Libro, mediante su Id.
    /// </remarks>
    /// <param name="id_libro">N. Identificador de Libro</param>

    [HttpDelete("{id}")]
    public async Task<ActionResult<Libro>> EliminarLibro(int id_libro)
    {
        var libros = await _context.Libros.FindAsync(id_libro);
        if(libros==null)
        {
            return NotFound();
        }
        _context.Libros.Remove(libros);
        await _context.SaveChangesAsync();
        return libros;
        
    }
    private bool ExisteLibro(int id_libro)
    {
        return _context.Libros.Any(l=>l.LibroID==id_libro);
    }
    }
}