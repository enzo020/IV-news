using Microsoft.EntityFrameworkCore; 
using IVnews.Data;
using IVNews.Services;
using IVNews.Backend.Services; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<INoticiaService, NoticiaService>();
builder.Services.AddHttpClient<ImagemService>();
builder.Services.AddScoped<ArmazenamentoImagemService>();
builder.Services.AddHttpClient<IGeminiService, GeminiService>();
builder.Services.AddScoped<
    IVNews.Backend.Services.IContentExtractorService,
    IVNews.Backend.Services.ContentExtractorService>();
builder.Services.AddScoped<IGeracaoDeImagensService, GeracaoDeImagensService>();

var app = builder.Build();

app.UseCors("ReactPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sua API v1");
    
    c.RoutePrefix = string.Empty; 
});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
