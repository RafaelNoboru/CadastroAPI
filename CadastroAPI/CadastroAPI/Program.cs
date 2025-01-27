using System.Data;
using CadastroApi.Repository;
using CadastroAPI.Models.Models;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var connectionString = configuration.GetConnectionString("DefaultConnection");


builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(connectionString));
builder.Services.AddScoped<IRepository<Pessoa>, PessoaRepository>();
builder.Services.AddScoped<IRepository<Endereco>, EnderecoRepository>();


builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Pessoa}/{action=Index}/{id?}");

app.Run();
