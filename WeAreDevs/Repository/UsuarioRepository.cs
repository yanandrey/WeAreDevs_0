using WeAreDevs.Context;
using WeAreDevs.Models;

namespace WeAreDevs.Repository
{
    public interface IUsuarioRepository
    {
        List<Usuario> ObterUsuarios();
        Usuario ObterUsuario(Guid id);
        Usuario InserirUsuario(Usuario novoUsuario);
        Usuario AtualizarUsuario(Usuario dadosUsuario);
        void RemoverUsuario(Guid id);
    }

    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApiDbContext _context;

        public UsuarioRepository(ApiDbContext context)
        {
            _context = context;
        }

        public List<Usuario> ObterUsuarios()
        {
            var query = _context.Usuarios
                .ToList();

            return query;
        }

        public Usuario ObterUsuario(Guid id)
        {
            var query = _context.Usuarios
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return query;
        }

        public Usuario InserirUsuario(Usuario novoUsuario)
        {
            var usuario = new Usuario()
            {
                Nome = novoUsuario.Nome,
                Email = novoUsuario.Email,
                Senha = novoUsuario.Senha
            };

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return usuario;
        }

        public Usuario AtualizarUsuario(Usuario dadosUsuario)
        {
            var usuario = _context.Usuarios
                .Where(x => x.Id == dadosUsuario.Id)
                .FirstOrDefault();

            if (usuario == default)
            {
                throw new Exception("Usuário não encontrado");
            }

            usuario.Nome = dadosUsuario.Nome;
            usuario.Email = dadosUsuario.Email;
            usuario.Senha = dadosUsuario.Senha;

            _context.SaveChanges();

            return usuario;
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