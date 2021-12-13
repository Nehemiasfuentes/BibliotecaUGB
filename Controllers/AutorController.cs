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

    /// <summary>
    /// Obteniendo listado de Autores.
    /// </summary>
    /// <remarks>
    /// Mediante este verbo se obtienen todos los registros de Autores que hay en la base de datos.
    /// </remarks>
    [HttpGet]
    public async Task<ActionResult<List<Autor>>> ObtenerListadoDeAutores()
    {
        var autores = await _context.Autores.ToListAsync();
        return autores;
    }

    /// <summary>
    /// Obteniendo datos de Autor mediante el Id de este.
    /// </summary>
    /// <remarks>
    /// Mediante el mismo verbo Get pero ahora pasandole un parametro para realizar una busqueda filtrada por el Id del Autor.
    /// </remarks>
    /// <param name="id_autor">N. Identificador de Autor.</param>
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


    /// <summary>
    /// Creando nuevo registro de Autor.
    /// </summary>
    /// <remarks>
    /// Mediante este verbo creamos un nuevo registro de Autor en la base de datos.
    /// </remarks>
    [HttpPost]
    public async Task<ActionResult<Autor>> PublicarAutor(Autor autor)
    {
        _context.Autores.Add(autor);
        await _context.SaveChangesAsync();
        return CreatedAtAction("ObetnerAutorPorID", new{id_autor=autor.AutorID},autor);
    }

    /// <summary>
    /// Actualizando Datos del Autor. N.
    /// </summary>
    /// <remarks>
    /// Mediante este verbo actualizamos un registro de Autor en la base de datos mediante el Id.
    /// </remarks>
    /// <param name="id_autor">N. Identificador de Autor.</param>
    /// <param name="autor"></param>
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

    /// <summary>
    /// ELiminando a un Autor de los registros mediante su Id.
    /// </summary>
    /// <remarks>
    /// COn este verbo podremos eliminar el registro de un autor, mediante su Id.
    /// </remarks>
    /// <param name="id_autor">N. Identificador de Autor</param>
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