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