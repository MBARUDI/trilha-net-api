﻿using Microsoft.EntityFrameworkCore;
using MeuProjeto.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// 🔧 Configuração do banco de dados
builder.Services.AddDbContext<OrganizadorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));

// 🔧 Configuração dos controladores e enum como string no JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// 🔧 Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🔧 Middleware do Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Tarefas V1");
    c.RoutePrefix = "swagger"; // Acesse em http://localhost:5000/swagger
});

// 🔧 Redirecionamento HTTPS (desative se estiver testando localmente sem certificado)
app.UseHttpsRedirection();

// 🔧 Autorização (mesmo sem autenticação, mantém o pipeline preparado)
app.UseAuthorization();

// 🔧 Mapeamento dos controladores
app.MapControllers();

// 🔧 Inicialização da aplicação
app.Run();
