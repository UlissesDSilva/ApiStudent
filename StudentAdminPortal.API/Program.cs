using StudentAdminPortal.API.Data;
using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.Data.IRepository;
using StudentAdminPortal.API.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var mySqlConnection = builder.Configuration.GetConnectionString("StudentContext");
builder.Services.AddDbContext<StudentContext>(options =>
    options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

//Add Scope
builder.Services.AddScoped<StudentContext>();
builder.Services.AddScoped<IRepositoryStudent, RepositoryStudent>();

builder.Services.AddControllers();

builder.Services.AddCors(options => {
    options.AddPolicy("angularApiWeb", policy => {
        policy.WithOrigins("*")
            .AllowAnyHeader()
            .WithMethods("GET", "POST", "PUT", "DELETE")
            .WithExposedHeaders("*");
        });
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("angularApiWeb");

app.UseAuthorization();

app.MapControllers();

app.Run();
