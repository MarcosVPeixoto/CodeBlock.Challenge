using CodeBlock.Challenge.Business.Business;
using CodeBlock.Challenge.Business.Validator;
using CodeBlock.Challenge.Domain.DTOs;
using CodeBlock.Challenge.IBusiness.IBusiness;
using CodeBlock.Challenge.IRepository.Repositories;
using CodeBlock.Challenge.Repository.Data;
using CodeBlock.Challenge.Repository.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer("Server=tcp:35.247.214.2,1433;Initial Catalog=codeblock;Persist Security Info=False;User ID=sqlserver;Password=codeblock;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=true;Connection Timeout=30;"));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .SetIsOriginAllowed((host)=> true);
    }
        );
});
builder.Services.AddScoped<DataContext>();
builder.Services.AddScoped<IOperacaoCargueiroBusiness, OperacaoCargueiroBusiness>();
builder.Services.AddScoped<ICargaSemanalBusiness, CargaSemanalBusiness>();
builder.Services.AddScoped<IOperacaoCargueiroRepository, OperacaoCargueiroRepository>();
builder.Services.AddScoped<ICargaRepository, CargaRepository>();
builder.Services.AddScoped<IValidator<OperacaoCargueiroDto>, OperacaoCargueiroDtoValidator>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowAll");

app.Run();
