using laboratorio1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace laboratorio1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class publicacionesController : ControllerBase
    {
        private readonly blog _blogPublicaciones;
        public publicacionesController(blog blogPublicaciones)
        {
            _blogPublicaciones = blogPublicaciones;


        }
        [HttpGet]
        [Route("getAll")]
        public IActionResult Get()
        {
            try
            {
                List<publicaciones> listPublicaciones = (from e in _blogPublicaciones.publicaciones select e).ToList();
                if (listPublicaciones.Count == 0)
                {
                    return NotFound();
                }
                return Ok(listPublicaciones);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("agregar")]

        public IActionResult Post([FromBody] publicaciones publicaciones)
        {
            try
            {
                _blogPublicaciones.publicaciones.Add(publicaciones);
                _blogPublicaciones.SaveChanges();
                return Ok(publicaciones);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizacion/{id}")]

        public IActionResult actualizarPublicacion(int id, [FromBody] publicaciones publicacionesActualiz)
        {
            try
            {
                publicaciones? publicacionesact = (from e in _blogPublicaciones.publicaciones where e.publicacionId == id select e).FirstOrDefault();
                if (publicacionesact == null) return NotFound();

                publicacionesact.publicacionId = publicacionesActualiz.publicacionId;
                publicacionesact.titulo = publicacionesActualiz.titulo;
                publicacionesact.descripcion = publicacionesActualiz.descripcion;
                publicacionesact.usuarioid = publicacionesActualiz.usuarioid ;

                _blogPublicaciones.Entry(publicacionesact).State = EntityState.Modified;
                _blogPublicaciones.SaveChanges();
                return Ok(publicacionesActualiz);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult eliminarUs(int id)
        {
            try
            {
                publicaciones? publicacionesBorrar = (from e in _blogPublicaciones.publicaciones where e.publicacionId == id select e).FirstOrDefault();
                if (publicacionesBorrar == null) return NotFound();
                _blogPublicaciones.SaveChanges();
                return Ok(publicacionesBorrar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("filtrar/{nombre}")]
        public IActionResult getNomyApe(string nombre)
        {
            try
            {
                List<publicaciones> listnombre = (from e in _blogPublicaciones.publicaciones  select e).ToList();
                if (listnombre == null) return NotFound();
                _blogPublicaciones.SaveChanges();
                return Ok(listnombre);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
