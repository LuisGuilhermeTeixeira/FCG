namespace FCG.Api.Dominio.Entidades
{
    public class JogoUsuario
    {
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public int JogoId { get; set; }
        public Jogo Jogo { get; set; } = null!;
    }
}
