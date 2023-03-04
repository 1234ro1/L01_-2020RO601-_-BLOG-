using laboratorio1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace laboratorio1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly blog _blogUsuario;
        public usuariosController(blog blogUsuario)
        {
            _blogUsuario = blogUsuario;


        }
        [HttpGet]
        [Route("getAll")]
        public IActionResult Get()
        {
            try
            {
                List<usuarios> listUsuario = (from e in _blogUsuario.usuarios select e).ToList();
                if (listUsuario.Count == 0)
                {
                    return NotFound();
                }
                return Ok(listUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("agregar")]

        public IActionResult Post([FromBody] usuarios usuario) 
        {
            try
            {
                _blogUsuario.usuarios.Add(usuario);
                _blogUsuario.SaveChanges();
                return Ok(usuario);    
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);  
            }
        }

        [HttpPut]
        [Route("actualizacion/{id}")]

        public IActionResult actualizarUsuario(int id, [FromBody] usuarios usuariosActualiz) 
        {
            try
            {
                usuarios? usuarioact = (from e in _blogUsuario.usuarios where e.usuarioId == id select e).FirstOrDefault();
                if (usuarioact == null) return NotFound();

                usuarioact.nombreUsuario = usuariosActualiz.nombreUsuario;
                usuarioact.clave = usuariosActualiz.clave;
                usuarioact.nombre = usuariosActualiz.nombre;
                usuarioact.apellido = usuariosActualiz.apellido;

                _blogUsuario.Entry(usuarioact).State = EntityState.Modified;
                _blogUsuario.SaveChanges();
                return Ok(usuariosActualiz);


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
                usuarios? usuarioBorrar = (from e in _blogUsuario.usuarios where e.usuarioId == id select e).FirstOrDefault();
                if (usuarioBorrar == null) return NotFound();
                _blogUsuario.SaveChanges();
                return Ok(usuarioBorrar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("filtrar/{nombre}/{apellido}")]
        public IActionResult getNomyApe(string nombre, string apellido )
        {
            try
            {
                List<usuarios> listnombreapellido= (from e in _blogUsuario.usuarios where e.nombre.Contains(nombre) && e.apellido.Contains(apellido) select e).ToList();
                if (listnombreapellido == null) return NotFound();
                _blogUsuario.SaveChanges();
                return Ok(listnombreapellido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("getrol/{id}")]

        public IActionResult Getrolid(int id) 
        {
            try
            {
                usuarios? usuariorol = (from e in _blogUsuario.usuarios where e.rolId == id select e).FirstOrDefault();
                if (usuariorol == null) return NotFound();
                return Ok(usuariorol);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        

    }
}
