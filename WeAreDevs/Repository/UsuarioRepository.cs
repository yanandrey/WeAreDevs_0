using WeAreDevs.Context;
using WeAreDevs.DTOs.Request;
using WeAreDevs.DTOs.Response;
using WeAreDevs.Exceptions;
using WeAreDevs.Helpers;
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
        Usuario AutenticarUsuario(LoginRequestDto dto);
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
            var usuarios = new List<UsuarioResponseDto>();

            var idUsuarios = _context.Usuarios
                .Select(x => x.Id)
                .ToList();

            foreach (var id in idUsuarios)
            {
                var dadosUsuario = ObterUsuario(id);
                usuarios.Add(dadosUsuario);
            }

            return usuarios;
        }

        public UsuarioResponseDto ObterUsuario(Guid id)
        {
            var listaDeCursos = new List<UsuarioCursoResponseDto>();

            var usuario = _context.Usuarios
                .Where(x => x.Id == id)
                .Select(x => new UsuarioResponseDto
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Email = x.Email,
                    Tipo = x.Conta.Tipo,
                    Status = x.Conta.Status,
                    Cursos = new List<UsuarioCursoResponseDto>()
                })
                .FirstOrDefault();

            if (usuario == default) throw new NotFoundException("Usuário não encontrado");

            var cursosDoUsuario = _context.UsuarioCursos
                .Where(x => x.UsuarioId == id)
                .Select(x => x.CursoId)
                .ToList();

            foreach (var item in cursosDoUsuario)
            {
                var curso = _context.Cursos
                    .Where(x => x.Id == item)
                    .Select(x => new UsuarioCursoResponseDto
                    {
                        Id = x.Id,
                        Nome = x.Nome,
                        Descricao = x.Descricao,
                        CargaHoraria = x.CargaHoraria
                    })
                    .FirstOrDefault();

                if (curso == default) throw new NotFoundException("Curso não encontrado");

                listaDeCursos.Add(curso);
            }

            usuario.Cursos = listaDeCursos;

            return usuario;
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
                Senha = HashingHelper.CriarHash(dto.Senha),
                Conta = contaUsuario
            };

            _context.Usuarios.Add(usuario);

            if (dto.Cursos.Any())
            {
                foreach (var item in dto.Cursos)
                {
                    var curso = _context.Cursos
                        .FirstOrDefault(x => x.Id == item);

                    if (curso == default) throw new NotFoundException("Curso não encontrado");

                    var usuarioCurso = new UsuarioCurso
                    {
                        UsuarioId = usuario.Id,
                        CursoId = item
                    };

                    _context.UsuarioCursos.Add(usuarioCurso);
                }
            }

            _context.SaveChanges();

            return ObterUsuario(usuario.Id);
        }

        public UsuarioResponseDto AtualizarUsuario(UsuarioUpdateRequestDto dto)
        {
            var usuario = _context.Usuarios
                .Where(x => x.Id == dto.Id)
                .FirstOrDefault();

            if (usuario == default) throw new NotFoundException("Usuário não encontrado");

            usuario.Nome = dto.Nome;
            usuario.Email = dto.Email;
            usuario.Senha = HashingHelper.CriarHash(dto.Senha);

            var usuarioCursos = _context.UsuarioCursos
                .Where(x => x.UsuarioId == dto.Id)
                .Select(x => x.CursoId)
                .ToList();

            //comparação dto => banco
            foreach (var dtoCurso in dto.Cursos)
            {
                var curso = _context.Cursos
                    .FirstOrDefault(x => x.Id == dtoCurso);

                if (curso == default) throw new NotFoundException("Curso não encontrado");

                var usuarioCursoExistente = _context.UsuarioCursos
                    .Where(x => x.UsuarioId == usuario.Id)
                    .Where(x => x.CursoId == curso.Id)
                    .FirstOrDefault();

                if (usuarioCursoExistente == default)
                {
                    var usuarioCurso = new UsuarioCurso
                    {
                        UsuarioId = usuario.Id,
                        CursoId = curso.Id,
                    };

                    _context.UsuarioCursos.Add(usuarioCurso);
                }
            }

            //comparação banco => dto
            foreach (var curso in usuarioCursos)
            {
                var usuarioCursoExistente = dto.Cursos.Contains(curso);

                if (!usuarioCursoExistente)
                {
                    var usuarioCurso = _context.UsuarioCursos
                        .Where(x => x.UsuarioId == usuario.Id)
                        .Where(x => x.CursoId == curso)
                        .FirstOrDefault();

                    if (usuarioCurso == default) throw new NotFoundException("UsuarioCurso não encontrado");

                    _context.UsuarioCursos.Remove(usuarioCurso);
                }
            }

            _context.SaveChanges();

            return ObterUsuario(usuario.Id);
        }

        public void RemoverUsuario(Guid id)
        {
            var usuario = _context.Usuarios
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (usuario == default) throw new NotFoundException("Usuário não encontrado");

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }

        public Usuario AutenticarUsuario(LoginRequestDto dto)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(x => x.Email == dto.Email);

            if (usuario == default) throw new NotFoundException("Usuário não encontrado");

            if (usuario.Senha == null || !HashingHelper.ValidarHash(dto.Senha, usuario.Senha))
            {
                throw new UnauthorizedException("Credencial/usuário inválido");
            }

            return usuario;
        }
    }
}