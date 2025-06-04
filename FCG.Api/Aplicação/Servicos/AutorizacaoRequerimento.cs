using FCG.Api.Dominio.Enum;
using Microsoft.AspNetCore.Authorization;

namespace FCG.Api.Aplicação.Servicos
{
    public class AutorizacaoRequerimento : IAuthorizationRequirement
    {
        public PerfilEnum Perfil { get; }
        public AutorizacaoRequerimento(PerfilEnum perfil)
        {
            Perfil = perfil;
        }
    }
}
