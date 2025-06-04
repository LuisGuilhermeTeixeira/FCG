using FCG.Api.Dominio.Enum;
using Microsoft.AspNetCore.Authorization;

namespace FCG.Api.Aplicação.Servicos
{
    public class AutorizacaoAcesso : AuthorizationHandler<AutorizacaoRequerimento>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AutorizacaoRequerimento requirement)
        {
            var perfilClaim = context.User.FindFirst(claim => claim.Type == "Perfil");

            if (perfilClaim is null)
            {
                // Usuário não possui a claim "Perfil"
                return Task.CompletedTask;
            }

            // Verifica se o perfil do usuário inclui o requisito
            if (Enum.TryParse<PerfilEnum>(perfilClaim.Value, out var perfilUsuario))
            {
                if (perfilUsuario == requirement.Perfil)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
