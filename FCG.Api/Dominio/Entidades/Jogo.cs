namespace FCG.Api.Dominio.Entidades
{
    public class Jogo
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public string Categoria { get; set; } = string.Empty;

        public ICollection<JogoUsuario> Usuario { get; set; }
    }
}
