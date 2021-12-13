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
    public class AutorController : Controller
    {
    private DatabaseContext _context;
    public AutorController(DatabaseContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Autor>>> ObtenerListadoDeAutores()
    {
        var autores = await _context.Autores.ToListAsync();
        return autores;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Autor>> ObetnerAutorPorID(int id_autor)
    {
        var autores = await _context.Autores.FindAsync(id_autor);
        if(autores==null)
        {
            return NotFound();
        }
        return autores;
    }

    [HttpPost]
    public async Task<ActionResult<Autor>> PublicarAutor(Autor autor)
    {
        _context.Autores.Add(autor);
        await _context.SaveChangesAsync();
        return CreatedAtAction("ObetnerAutorPorID", new{id_autor=autor.AutorID},autor);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Autor>> EditarDatosAutor(int id_autor, Autor autor)
    {
    if(id_autor!= autor.AutorID)
        {
        return BadRequest();
        }
    _context.Entry(autor).State = EntityState.Modified;
    try
    {
        await _context.SaveChangesAsync();
    }
    catch(DbUpdateConcurrencyException)
    {
        if(!ExisteElAutor(id_autor))
        {
            return NotFound();
        }
        else
        {
            throw;
        }
    }
    return CreatedAtAction("ObtenerAutorPorID", new{id_autor=autor.AutorID},autor);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Autor>> EliminarAutor(int id_autor)
    {
        var autores = await _context.Autores.FindAsync(id_autor);
        if(autores==null)
        {
            return NotFound();
        }
        _context.Autores.Remove(autores);
        await _context.SaveChangesAsync();
        return autores;
        
    }
    private bool ExisteElAutor(int id_autor)
    {
        return _context.Autores.Any(a=>a.AutorID==id_autor);
    }
    }
}