﻿using FCG.Api.Dominio.Enum;

namespace FCG.Api.Dominio.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;

        public PerfilEnum Perfil{ get; set; }

        public ICollection<JogoUsuario> Jogo { get; set; }
    }
}
