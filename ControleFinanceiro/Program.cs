using Application.UseCases.CategoriaUseCases;
using Application.UseCases.ContaUseCases;
using Application.UseCases.DashBoardUseCases;
using Application.UseCases.TransacaoUseCases;
using ControleFinanceiro.EndPoints;
using Domain.Interfaces;
using Infraestructure.Data;
using Infraestructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
        .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // A URL exata do seu Angular
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging().LogTo(Console.WriteLine));

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddSingleton<TimeProvider>(TimeProvider.System);
builder.Services.AddSingleton<IEmailSender<IdentityUser>, EmailSender>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Insira o token JWT aqui",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

//Categorias
builder.Services.AddScoped<AddCategoriaUseCase>();
builder.Services.AddScoped<GetAllCategoriasUseCase>();
builder.Services.AddScoped<DeleteCategoriaUseCase>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();


//Contas
builder.Services.AddScoped<AddContaUseCase>();
builder.Services.AddScoped<GetAllContasUseCase>();
builder.Services.AddScoped<IContaRepository, ContaRepository>();

//Transacoes
builder.Services.AddScoped<AddTransacaoUseCase>();
builder.Services.AddScoped<GetAllTransacoesUseCase>();
builder.Services.AddScoped<ITransacaoRepository,  TransacaoRepository>();

//Dashboard
builder.Services.AddScoped<GetResumoFinanceiroUseCase>();
builder.Services.AddScoped<GetUltimasTransacoesUseCase>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("PermitirAngular");

app.UseAuthentication();
app.UseAuthorization();

//Mapas de EndPoints
app.MapGroup("/auth").MapIdentityApi<IdentityUser>();
CategoriaEndPoints.MapCategoriaEndPoints(app);
ContaEndPoints.MapContaEndPoints(app);
TransacaoEndPoints.MapTransacaoEndPoints(app);
DashBoardEndPoints.MapDashBoardEndPoints(app);

app.MapControllers();

app.Run();

public class EmailSender : IEmailSender<IdentityUser>
{
    public Task SendConfirmationLinkAsync(IdentityUser user, string email, string confirmationLink)
    {
        // Aqui você poderia logar no console o link para testar
        Console.WriteLine($"Simulando envio de email para {email}: {confirmationLink}");
        return Task.CompletedTask;
    }

    public Task SendPasswordResetLinkAsync(IdentityUser user, string email, string resetLink)
    {
        Console.WriteLine($"Simulando reset de senha para {email}: {resetLink}");
        return Task.CompletedTask;
    }

    public Task SendPasswordResetCodeAsync(IdentityUser user, string email, string resetCode)
    {
        Console.WriteLine($"Simulando código de reset para {email}: {resetCode}");
        return Task.CompletedTask;
    }
}
