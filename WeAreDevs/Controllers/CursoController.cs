﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeAreDevs.DTOs.Request;
using WeAreDevs.DTOs.Response;
using WeAreDevs.Repository;

namespace WeAreDevs.Controllers
{
    [Route("curso")]
    [Authorize]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly ICursoRepository _cursoRepository;

        public CursoController(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        /// <summary>
        /// Obtém a lista de cursos.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IList<CursoResponseDto> ObterCursos()
        {
            return _cursoRepository.ObterCursos();
        }

        /// <summary>
        /// Obtém o curso pelo seu id.
        /// </summary>
        /// <param name="id">Id do curso</param>
        /// <returns></returns>
        [HttpGet("filtro/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(IgnoreApi = true)]
        public CursoResponseDto ObterCursoUsandoRota([FromRoute] Guid id)
        {
            return _cursoRepository.ObterCurso(id);
        }

        /// <summary>
        /// Insere um curso.
        /// </summary>
        /// <param name="dto">Objeto para a criação de um curso</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public CursoResponseDto InserirCurso([FromBody] CursoRequestDto dto)
        {
            return _cursoRepository.InserirCurso(dto);
        }

        /// <summary>
        /// Atualiza um curso.
        /// </summary>
        /// <param name="dto">Objeto para a atualização de um curso</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public CursoResponseDto AtualizarCurso([FromBody] CursoUpdateRequestDto dto)
        {
            return _cursoRepository.AtualizarCurso(dto);
        }

        /// <summary>
        /// Remove um curso.
        /// </summary>
        /// <param name="id">Id do curso</param>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public void RemoverCurso([FromQuery] Guid id)
        {
            _cursoRepository.RemoverCurso(id);
        }
    }
}