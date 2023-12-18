using FileUploadApp.Contracts;
using FileUploadApp.Contracts.Factory;
using FileUploadApp.Contracts.FileSystem;
using FileUploadApp.Implementation;
using FileUploadApp.Implementation.Converters;
using FileUploadApp.Implementation.Factory;
using FileUploadApp.Implementation.FileSystem;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<FileSettings>(builder.Configuration.GetSection("FileSettings"));

RegisterServices(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

void RegisterServices(IServiceCollection services)
{
    services.AddSingleton<IFileProcessor, FileProcessor>();
    services.AddSingleton<IConverterFactory, ConverterFactory>();
    services.AddSingleton<IFileSystemManager, FileSystemManager>();   

    services.AddTransient<XmlToJsonConverter>();
}