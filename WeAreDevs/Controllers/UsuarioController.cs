using Microsoft.AspNetCore.Mvc;
using WeAreDevs.DTOs.Request;
using WeAreDevs.DTOs.Response;
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
        public IList<UsuarioResponseDto> ObterUsuarios()
        {
            return _usuarioRepository.ObterUsuarios();
        }

        /// <summary>
        /// Obtém o usuário pelo seu id.
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns></returns>
        [HttpGet("filtro")]
        public UsuarioResponseDto ObterUsuarioUsandoParametros([FromQuery] Guid id)
        {
            return _usuarioRepository.ObterUsuario(id);
        }

        /// <summary>
        /// Obtém o usuário pelo seu id.
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns></returns>
        [HttpGet("filtro/{id}")]
        public UsuarioResponseDto ObterUsuarioUsandoRota([FromRoute] Guid id)
        {
            return _usuarioRepository.ObterUsuario(id);
        }

        /// <summary>
        /// Insere um usuário.
        /// </summary>
        /// <param name="usuario">Objeto para a criação de um usuário</param>
        /// <returns></returns>
        [HttpPost]
        public UsuarioResponseDto InserirUsuario([FromBody] UsuarioRequestDto usuario)
        {
            return _usuarioRepository.InserirUsuario(usuario);
        }

        /// <summary>
        /// Atualiza um usuário.
        /// </summary>
        /// <param name="dadosUsuario">Objeto para a atualização de um usuário</param>
        /// <returns></returns>
        [HttpPut]
        public UsuarioResponseDto AtualizarUsuario([FromBody] UsuarioUpdateRequestDto dadosUsuario)
        {
            return _usuarioRepository.AtualizarUsuario(dadosUsuario);
        }

        /// <summary>
        /// Remove um usuário.
        /// </summary>
        /// <param name="id">Id do usuário</param>
        [HttpDelete]
        public void RemoverUsuario([FromQuery] Guid id)
        {
            _usuarioRepository.RemoverUsuario(id);
        }
    }
}