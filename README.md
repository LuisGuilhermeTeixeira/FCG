# FCG
Entregável do primeiro Tech Challenge do curso de pós-graduação


🧱 Arquitetura Utilizada
.NET 8, arquitetura monolítica

Aplicação estruturada em camadas conforme os princípios de DDD (Domain-Driven Design)

Camadas:
Apresentação (controladores ou endpoints)
Aplicação (serviços de aplicação)
Domínio (entidades, interfaces, serviços de domínio)
Infraestrutura (reposições e serviços externos como geração de token)

🧍 Funcionalidades de Usuário
✅ Criar Usuário
Recebe UsuarioDTO
Valida:
Nome não vazio
Email válido e não duplicado
Senha segura (mínimo 8 caracteres, com letras, números e símbolos)
Cria usuário com senha criptografada
Armazena via IUsuarioRepositorio

🔐 Login
Recebe LoginDTO
Valida email e senha
Verifica hash da senha com BCrypt
Gera token JWT com TokenServico se credenciais forem válidas

🕹️ Funcionalidades de Jogo
➕ Adicionar Jogo
Recebe JogoDTO
Verifica se já existe jogo com o mesmo título
Se não existir, cria e armazena via IJogoRepositorio

🔎 Buscar Todos os Jogos
Recupera todos os registros
Converte para lista de JogoDTO
Retorna lista para o frontend

🧩 Objetos Envolvidos
Entidades
Usuario (Nome, Email, SenhaHash)
Jogo (Título, Categoria, Descrição, Preço)

DTOs
UsuarioDTO
LoginDTO
JogoDTO

Serviços
UsuarioServico
JogoServico

Repositórios
IUsuarioRepositorio
IJogoRepositorio

🔐 Segurança
Senhas criptografadas com BCrypt
JWT utilizado para autenticação
Validações robustas na entrada de dados

🔄 Event Storming (resumido)
Comandos: CriarUsuário, Login, AdicionarJogo, BuscarJogos
Políticas: ValidarEmail, ValidarSenha, VerificarDuplicidade, GerarToken
Eventos: UsuarioCriado, UsuarioAutenticado, JogoCriado, ListaDeJogosRetornada

