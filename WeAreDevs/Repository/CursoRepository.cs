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
            var curso = _context.Cursos
                .Where(x => x.Id == id)
                .Select(x => new CursoResponseDto
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Descricao = x.Descricao,
                    CargaHoraria = x.CargaHoraria
                })
                .FirstOrDefault();

            if (curso == default)
            {
                throw new Exception("Curso não encontrado");
            }

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

        public void RemoverCurso(Guid id)
        {
            var curso = _context.Cursos
                .FirstOrDefault(x => x.Id == id);

            if (curso == default)
            {
                throw new Exception("Curso não encontrado");
            }

            _context.Cursos.Remove(curso);
            _context.SaveChanges();
        }
    }
}