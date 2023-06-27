namespace WeAreDevs.Models
{
    public class Erro
    {
        public Erro(Exception ex)
        {
            Tipo = ex.GetType().Name;
            Mensagem = ex.Message;
        }

        public string Tipo { get; set; }
        public string Mensagem { get; set; }
    }
}