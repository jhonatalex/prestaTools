using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using prestaToolsApi.Data.Repository;
using prestaToolsApi.ModelsEntity;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var mySQLConfiguration = new MySQLConfiguration(builder.Configuration.GetConnectionString("MySqlConnection"));
//uilder.Services.AddSingleton(mySQLConfiguration);


//USANDO ENTITY FRAMEWORK MSSQL
builder.Services.AddDbContext<PrestatoolsContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));





//CONFIGURA LAS REFERNCIAS CICLICAS DE LA RELACIONES
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

});



builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<ILenderRepository, LenderRepository>();

//CORS HABILITAR
var misReglasCors = "ReglasCors";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: misReglasCors, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });

});






var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}


app.UseSwagger();
 app.UseSwaggerUI();



app.UseCors(misReglasCors);


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
