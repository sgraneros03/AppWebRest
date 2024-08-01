using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Modelo;




namespace Clase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaseController : ControllerBase
    {
        private readonly ClaseRepositorio _claseRepositorio;

        public ClaseController(ClaseRepositorio claseRepositorio)
        {
            _claseRepositorio = claseRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClaseDTO>>> GetClases()
        {
            var clases = await _claseRepositorio.ObtenerTodasLasClases();
            return Ok(clases);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClaseDTO>> GetClases(int id)
        {
            var clase = await _claseRepositorio.ObtenerClasePorId(id);
            if (clase == null) return NotFound();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> PostClase(ClaseDTO clase)
        { 
            await _claseRepositorio.AgregarClase(clase);
            return CreatedAtAction(nameof(GetClases), new { id = clase.Id }, clase);   
        }

        [HttpPut("{id}")]   
        public async Task<IActionResult> PutClase(int id, ClaseDTO clase)
        {
            if (id != clase.Id) return BadRequest();
            await _claseRepositorio.ActualizarClase(clase);
            return NoContent();
        }

        [HttpDelete("{id}")]      
        public async Task<IActionResult> DeleteClase(int id)
        {
            var clase = await _claseRepositorio.ObtenerClasePorId(id);
            if (clase == null) return NotFound();
            await _claseRepositorio.EliminarClase(id);
            return NoContent();
        }
    }
}