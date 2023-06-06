using Microsoft.AspNetCore.Mvc;
using WeAreDevs.Models;
using WeAreDevs.Repository;

namespace WeAreDevs.Controllers
{
    [Route("usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Obtém a lista de usuários.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IList<Usuario> ObterUsuarios()
        {
            return _usuarioRepository.ObterUsuarios();
        }

        /// <summary>
        /// Obtém o usuário pelo seu id.
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns></returns>
        [HttpGet("filtro")]
        public Usuario ObterUsuarioUsandoParametros([FromQuery] int id)
        {
            return _usuarioRepository.ObterUsuario(id);
        }

        /// <summary>
        /// Obtém o usuário pelo seu id.
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns></returns>
        [HttpGet("filtro/{id}")]
        public Usuario ObterUsuarioUsandoRota([FromRoute] int id)
        {
            return _usuarioRepository.ObterUsuario(id);
        }

        /// <summary>
        /// Insere um usuário.
        /// </summary>
        /// <param name="usuario">Objeto para a criação de um usuário</param>
        /// <returns></returns>
        [HttpPost]
        public List<Usuario> InserirUsuario([FromBody] Usuario usuario)
        {
            return _usuarioRepository.InserirUsuario(usuario);
        }
    }
}