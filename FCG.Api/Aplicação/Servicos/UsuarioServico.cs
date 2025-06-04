using FCG.Api.Dominio.Entidades;
using FCG.Api.Dominio.Helpers;
using FCG.Api.Dominio.Interfaces.Infraestrutura;
using FCG.Api.Dominio.Interfaces.Servico;
using FCG.Api.DTOs;
using FCG.Api.Infraestrutura.Token;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace FCG.Api.Aplicação.Servicos
{
    public class UsuarioServico : BaseServico<Usuario>, IUsuarioServico
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ILogger<UsuarioServico> _logger;

        public UsuarioServico(IUsuarioRepositorio usuarioRepositorio, ILogger<UsuarioServico> logger)
            : base(usuarioRepositorio) // Passa o repositório para o construtor da classe base
        {
            _usuarioRepositorio = usuarioRepositorio;
            _logger = logger;
        }

        public async Task<(bool sucesso, string? erro)> CriarUsuarioAsync(UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null)
                return (false, "Dados do usuário não podem ser nulos.");

            string nome = usuarioDTO.Nome?.Trim() ?? string.Empty;
            string email = usuarioDTO.Email?.Trim().ToLowerInvariant() ?? string.Empty;
            string senha = usuarioDTO.Senha?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(nome))
                return (false, "Nome é obrigatório.");

            if (!Utilidades.ValidarEmail(email))
                return (false, "Email inválido.");

            if (!Utilidades.ValidarSenha(senha))
                return (false, "Senha insegura. Deve ter ao menos 8 caracteres, incluindo letras, números e caracteres especiais.");

            try
            {
                if (await _usuarioRepositorio.EmailExisteAsync(email))
                    return (false, "Email já está em uso.");

                var hash = BCrypt.Net.BCrypt.HashPassword(senha);
                var usuario = new Usuario
                {
                    Nome = nome,
                    Email = email,
                    SenhaHash = hash
                };

                _usuarioRepositorio.Add(usuario);
                return (true, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar o usuário com email: {Email}", email);
                throw;
            }
        }

        public async Task<string> LoginAsync(LoginDTO loginDTO)
        {
            string email = loginDTO.Email?.Trim().ToLowerInvariant() ?? string.Empty;
            string senha = loginDTO.Senha?.Trim() ?? string.Empty;
            try
            {
                if (!Utilidades.ValidarEmail(email))
                    throw new InvalidOperationException("E-mail incorreto.");

                if (string.IsNullOrWhiteSpace(senha))
                    throw new InvalidOperationException("Senha incorreto.");

                var usuario = await _usuarioRepositorio.ObterPorEmailAsync(email);

                if (usuario == null)
                    throw new InvalidOperationException("Email não cadastrado.");


                if (BCrypt.Net.BCrypt.Verify(senha, usuario.SenhaHash))
                {
                    TokenServico servicoToken = new TokenServico();

                    var token = servicoToken.GerarToken(usuario);

                    _logger.LogInformation("Login realizado com sucesso para {Email}", email);
                    return (token);
                }
                else
                {
                    _logger.LogWarning("Senha incorreta para o usuário {Email}", email);
                    throw new InvalidOperationException("Email ou senha incorretos.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao realizar login do usuário {Email}", email);
                throw new Exception(ex.Message);
            }
        }
    }
}
