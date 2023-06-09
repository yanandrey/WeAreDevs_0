﻿namespace WeAreDevs.Models
{
    public class Curso
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int CargaHoraria { get; set; }
        public ICollection<Topico> Topicos { get; set; }
        public ICollection<UsuarioCurso> Usuario { get; set; }
    }
}