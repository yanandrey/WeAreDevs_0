using WeAreDevs.Context;
using WeAreDevs.DTOs.Request;
using WeAreDevs.DTOs.Response;
using WeAreDevs.Models;

namespace WeAreDevs.Repository
{
    public interface ICursoRepository
    {
        List<CursoResponseDto> ObterCursos();
        CursoResponseDto ObterCurso(Guid id);
        CursoResponseDto InserirCurso(CursoRequestDto dto);
        CursoResponseDto AtualizarCurso(CursoUpdateRequestDto dto);
        void RemoverCurso(Guid id);
    }

    public class CursoRepository : ICursoRepository
    {
        private readonly ApiDbContext _context;

        public CursoRepository(ApiDbContext context)
        {
            _context = context;
        }

        public List<CursoResponseDto> ObterCursos()
        {
            var cursos = new List<CursoResponseDto>();

            var idCursos = _context.Cursos
                .Select(x => x.Id)
                .ToList();

            foreach (var id in idCursos)
            {
                var dadosCurso = ObterCurso(id);
                cursos.Add(dadosCurso);
            }

            return cursos;
        }

        public CursoResponseDto ObterCurso(Guid id)
        {
            var listaDeTopicos = new List<TopicoResponseDto>();

            var curso = _context.Cursos
                .Where(x => x.Id == id)
                .Select(x => new CursoResponseDto
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Descricao = x.Descricao,
                    CargaHoraria = x.CargaHoraria,
                    Topicos = new List<TopicoResponseDto>()
                })
                .FirstOrDefault();

            if (curso == default) throw new Exception("Curso não encontrado");

            var topicosDoCurso = _context.Topicos
                .Where(x => x.Curso.Id == id)
                .Select(x => x.Id)
                .ToList();

            foreach (var item in topicosDoCurso)
            {
                var topico = _context.Topicos
                    .Where(x => x.Id == item)
                    .Select(x => new TopicoResponseDto
                    {
                        Id = x.Id,
                        Nome = x.Nome,
                        Descricao = x.Descricao,
                        Nivel = x.Nivel
                    })
                    .FirstOrDefault();

                if (topico == default) throw new Exception("Tópico não encontrado");

                listaDeTopicos.Add(topico);
            }

            curso.Topicos = listaDeTopicos;

            return curso;
        }

        public CursoResponseDto InserirCurso(CursoRequestDto dto)
        {
            var curso = new Curso
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                CargaHoraria = dto.CargaHoraria
            };

            _context.Cursos.Add(curso);
            _context.SaveChanges();

            return ObterCurso(curso.Id);
        }

        public CursoResponseDto AtualizarCurso(CursoUpdateRequestDto dto)
        {
            var curso = _context.Cursos
                .FirstOrDefault(x => x.Id == dto.Id);

            if (curso == default) throw new Exception("Curso não encontrado");

            curso.Nome = dto.Nome;
            curso.Descricao = dto.Descricao;
            curso.CargaHoraria = dto.CargaHoraria;

            _context.SaveChanges();

            return ObterCurso(curso.Id);
        }

        public void RemoverCurso(Guid id)
        {
            var curso = _context.Cursos
                .FirstOrDefault(x => x.Id == id);

            if (curso == default) throw new Exception("Curso não encontrado");

            _context.Cursos.Remove(curso);
            _context.SaveChanges();
        }
    }
}