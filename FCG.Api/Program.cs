using FCG.Api.Aplicação.Servicos;
using FCG.Api.Dominio.Enum;
using FCG.Api.Dominio.Interfaces.Infraestrutura;
using FCG.Api.Dominio.Interfaces.Servico;
using FCG.Api.Infraestrutura.Data;
using FCG.Api.Infraestrutura.Repositorio;
using FCG.Api.Infraestrutura.Token;
using FCG.Api.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ======= BANCO DE DADOS =======
builder.Services.AddDbContext<DBContexto>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConexaoHOM"),
        x => x.MigrationsHistoryTable("__EFMigrationsHistory", "fcg")),
    ServiceLifetime.Scoped);


// ======= CULTURA =======
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-BR");
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FCG API", Version = "v1" });
});

// Auth
var key = Encoding.ASCII.GetBytes("u7!3$9Lq2@aSdF9*1sT!B2nX#4pP&8Km");
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});



// ======= INJEÇÃO DE DEPENDÊNCIA =======
// Configuração de dependências (IoC)
builder.Services.AddScoped(typeof(IBaseRepositorio<>), typeof(BaseRepositorio<>));
builder.Services.AddScoped(typeof(IBaseServico<>), typeof(BaseServico<>));

// Serviços de aplicação
builder.Services.AddScoped<IUsuarioServico, UsuarioServico>();


// Repositórios
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IJogoRepositorio, JogoRepositorio>();



// Infraestrutura do token
builder.Services.AddScoped<TokenServico>();


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("PerfilAdmin", policy =>
        policy.Requirements.Add(new AutorizacaoRequerimento(PerfilEnum.Administrador)));

    options.AddPolicy("PerfilUsuario", policy =>
        policy.Requirements.Add(new AutorizacaoRequerimento(PerfilEnum.Usuario)));
});


builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ErroMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
