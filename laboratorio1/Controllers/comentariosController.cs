using laboratorio1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace laboratorio1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class comentariosController : ControllerBase 
    {
        private readonly blog _blogComentarios;
        public comentariosController(blog blogComentarios)
        {
            _blogComentarios = blogComentarios;


        }
        [HttpGet]
        [Route("getAll")]
        public IActionResult Get()
        {
            try
            {
                List<comentarios> listComentarios = (from e in _blogComentarios.comentarios select e).ToList();
                if (listComentarios.Count == 0)
                {
                    return NotFound();
                }
                return Ok(listComentarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("agregar")]

        public IActionResult Post([FromBody] comentarios comentarios)
        {
            try
            {
                _blogComentarios.comentarios.Add(comentarios);
                _blogComentarios.SaveChanges();
                return Ok(comentarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizacion/{id}")]

        public IActionResult actualizarComentario(int id, [FromBody] comentarios comentarioActualiz)
        {
            try
            {
                comentarios? comentarioact = (from e in _blogComentarios.comentarios where e.comentarioId == id select e).FirstOrDefault();
                if (comentarioact == null) return NotFound();

                comentarioact.comentarioId = comentarioActualiz.comentarioId;
                comentarioact.publicacionId = comentarioActualiz.publicacionId;
                comentarioact.comentario = comentarioActualiz.comentario;
                comentarioact.usuarioid = comentarioActualiz.usuarioid;

                _blogComentarios.Entry(comentarioact).State = EntityState.Modified;
                _blogComentarios.SaveChanges();
                return Ok(comentarioActualiz);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult eliminarcom(int id)
        {
            try
            {
                comentarios? comentarioBorrar = (from e in _blogComentarios.comentarios where e.usuarioid == id select e).FirstOrDefault();
                if (comentarioBorrar == null) return NotFound();
                _blogComentarios.SaveChanges();
                return Ok(comentarioBorrar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       
           
        
       

    }

}
