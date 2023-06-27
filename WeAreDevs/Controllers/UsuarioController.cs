using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeAreDevs.DTOs.Request;
using WeAreDevs.DTOs.Response;
using WeAreDevs.Repository;

namespace WeAreDevs.Controllers
{
    [Route("usuario")]
    [Authorize]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(IgnoreApi = true)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(IgnoreApi = true)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public UsuarioResponseDto AtualizarUsuario([FromBody] UsuarioUpdateRequestDto dadosUsuario)
        {
            return _usuarioRepository.AtualizarUsuario(dadosUsuario);
        }

        /// <summary>
        /// Remove um usuário.
        /// </summary>
        /// <param name="id">Id do usuário</param>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public void RemoverUsuario([FromQuery] Guid id)
        {
            _usuarioRepository.RemoverUsuario(id);
        }
    }
}