using Microsoft.EntityFrameworkCore;
using WeAreDevs.Context;
using WeAreDevs.DTOs.Request;
using WeAreDevs.DTOs.Response;
using WeAreDevs.Models;

namespace WeAreDevs.Repository
{
    public interface IUsuarioRepository
    {
        List<UsuarioResponseDto> ObterUsuarios();
        UsuarioResponseDto ObterUsuario(Guid id);
        UsuarioResponseDto InserirUsuario(UsuarioRequestDto dto);
        UsuarioResponseDto AtualizarUsuario(UsuarioUpdateRequestDto dto);
        void RemoverUsuario(Guid id);
    }

    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApiDbContext _context;

        public UsuarioRepository(ApiDbContext context)
        {
            _context = context;
        }

        public List<UsuarioResponseDto> ObterUsuarios()
        {
            var query = _context.Usuarios
                .Select(x => new UsuarioResponseDto
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Email = x.Email,
                    Tipo = x.Conta.Tipo,
                    Status = x.Conta.Status
                })
                .ToList();

            return query;
        }

        public UsuarioResponseDto ObterUsuario(Guid id)
        {
            var query = _context.Usuarios
                .Where(x => x.Id == id)
                .Select(x => new UsuarioResponseDto
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Email = x.Email,
                    Tipo = x.Conta.Tipo,
                    Status = x.Conta.Status
                })
                .FirstOrDefault();

            return query;
        }

        public UsuarioResponseDto InserirUsuario(UsuarioRequestDto dto)
        {
            var contaUsuario = new Conta
            {
                Tipo = dto.Tipo,
                Status = dto.Status
            };

            _context.Contas.Add(contaUsuario);

            var usuario = new Usuario()
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha,
                Conta = contaUsuario
            };

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return ObterUsuario(usuario.Id);
        }

        public UsuarioResponseDto AtualizarUsuario(UsuarioUpdateRequestDto dto)
        {
            var usuario = _context.Usuarios
                .Where(x => x.Id == dto.Id)
                .FirstOrDefault();

            if (usuario == default)
            {
                throw new Exception("Usuário não encontrado");
            }

            usuario.Nome = dto.Nome;
            usuario.Email = dto.Email;
            usuario.Senha = dto.Senha;

            _context.SaveChanges();

            return ObterUsuario(usuario.Id);
        }

        public void RemoverUsuario(Guid id)
        {
            var usuario = _context.Usuarios
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (usuario == default)
            {
                throw new Exception("Usuário não encontrado");
            }

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }
    }
}