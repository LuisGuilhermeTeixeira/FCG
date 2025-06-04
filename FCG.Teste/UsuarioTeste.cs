using FCG.Api.Aplicação.Servicos;
using FCG.Api.Dominio.Entidades;
using FCG.Api.Dominio.Interfaces;
using FCG.Api.Dominio.Interfaces.Infraestrutura;
using FCG.Api.Dominio.Interfaces.Servico;
using FCG.Api.DTOs;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace FCG.Api.Testes.Servico
{
    public class UsuarioServicoTeste
    {
        private readonly Mock<IUsuarioRepositorio> _usuarioRepositorioMock;
        private readonly Mock<ILogger<UsuarioServico>> _loggerMock;
        private readonly UsuarioServico _usuarioServico;

        public UsuarioServicoTeste()
        {
            _usuarioRepositorioMock = new Mock<IUsuarioRepositorio>();
            _loggerMock = new Mock<ILogger<UsuarioServico>>();
            _usuarioServico = new UsuarioServico(_usuarioRepositorioMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task CriarUsuarioAsync_DeveCriarUsuario_QuandoDadosValidos()
        {

            var dto = new UsuarioDTO
            {
                Nome = "João",
                Email = "joao@email.com",
                Senha = "Senha@123"
            };

            _usuarioRepositorioMock.Setup(r => r.EmailExisteAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            // Act
            var (sucesso, erro) = await _usuarioServico.CriarUsuarioAsync(dto);

            // Assert
            Assert.True(sucesso);
            Assert.Null(erro);
            _usuarioRepositorioMock.Verify(r => r.Add(It.IsAny<Usuario>()), Times.Once);
        }

        [Fact]
        public async Task CriarUsuarioAsync_DeveRetornarErro_QuandoEmailInvalido()
        {
            var dto = new UsuarioDTO
            {
                Nome = "Maria",
                Email = "mariaemail.com", // email inválido
                Senha = "Senha@123"
            };

            var result = await _usuarioServico.CriarUsuarioAsync(dto);

            Assert.False(result.sucesso);
            Assert.Equal("Email inválido.", result.erro);
        }

        [Fact]
        public async Task CriarUsuarioAsync_DeveRetornarErro_QuandoSenhaFraca()
        {
            var dto = new UsuarioDTO
            {
                Nome = "Carlos",
                Email = "carlos@email.com",
                Senha = "123" // fraca
            };

            var result = await _usuarioServico.CriarUsuarioAsync(dto);

            Assert.False(result.sucesso);
            Assert.StartsWith("Senha insegura", result.erro);
        }
    }
}
