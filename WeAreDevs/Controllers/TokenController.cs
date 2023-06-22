using Microsoft.AspNetCore.Mvc;
using WeAreDevs.DTOs.Request;
using WeAreDevs.DTOs.Response;
using WeAreDevs.Repository;
using WeAreDevs.Services;

namespace WeAreDevs.Controllers
{
    [Route("token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IUsuarioRepository _usuario;
        private readonly ITokenService _token;

        public TokenController(
            IUsuarioRepository usuario,
            ITokenService token)
        {
            _usuario = usuario;
            _token = token;
        }

        /// <summary>
        /// Obtém um token de acesso.
        /// </summary>
        /// <returns></returns>
        [HttpPost("login")]
        public LoginResponseDto GerarToken([FromBody] LoginRequestDto dto)
        {
            var usuario = _usuario.AutenticarUsuario(dto);
            var response = _token.GerarTokenJwt(usuario);
            return response;
        }
    }
}
