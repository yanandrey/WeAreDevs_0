using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WeAreDevs.DTOs.Response;
using WeAreDevs.Models;

namespace WeAreDevs.Services
{
    public interface ITokenService
    {
        LoginResponseDto GerarTokenJwt(Usuario usuario);
    }

    public class TokenService : ITokenService
    {
        private readonly Token _tokenConfig;

        public TokenService(Token tokenConfig)
        {
            _tokenConfig = tokenConfig;
        }

        public LoginResponseDto GerarTokenJwt(Usuario usuario)
        {
            var bytes = Encoding.UTF8.GetBytes(_tokenConfig.Secret);
            var chave = new SymmetricSecurityKey(bytes);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, usuario.Nome),
                new(ClaimTypes.Email, usuario.Email)
            };

            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _tokenConfig.Issuer,
                audience: _tokenConfig.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_tokenConfig.ExpiracaoDoTokenDeAcesso),
                signingCredentials: credenciais);

            return new LoginResponseDto
            {
                TokenDeAcesso = new JwtSecurityTokenHandler().WriteToken(token),
                DataDeExpiracao = token.ValidTo
            };
        }
    }
}