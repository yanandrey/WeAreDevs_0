using WeAreDevs.Models;

namespace WeAreDevs.Repository
{
    public interface IUsuarioRepository
    {
        List<Usuario> ObterUsuarios();
        Usuario ObterUsuario(int id);
        List<Usuario> InserirUsuario(Usuario novoUsuario);
    }

    public class UsuarioRepository : IUsuarioRepository
    {
        List<Usuario> listaDeUsuarios = new List<Usuario>()
        {
            new Usuario()
            {
                Id = 1,
                Nome = "Yan",
                Descricao = ".",
                Idade = 45
            },
            new Usuario()
            {
                Id = 2,
                Nome = "João",
                Descricao = ".",
                Idade = 25
            },
            new Usuario()
            {
                Id = 3,
                Nome = "Maria",
                Descricao = ".",
                Idade = 30
            }
        };

        public List<Usuario> ObterUsuarios()
        {
            return listaDeUsuarios;
        }

        public Usuario ObterUsuario(int id)
        {
            var usuario = listaDeUsuarios.Where(x => x.Id == id).FirstOrDefault();
            
            if (usuario != default)
            {
                return usuario;
            }

            return new Usuario();
        }

        public List<Usuario> InserirUsuario(Usuario novoUsuario)
        {
            listaDeUsuarios.Add(novoUsuario);
            return listaDeUsuarios;
        }
    }
}