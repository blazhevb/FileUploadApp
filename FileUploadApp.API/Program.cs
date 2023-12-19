using FileUploadApp.Contracts;
using FileUploadApp.Contracts.Factory;
using FileUploadApp.Contracts.FileSystem;
using FileUploadApp.Implementation;
using FileUploadApp.Implementation.Converters;
using FileUploadApp.Implementation.Factory;
using FileUploadApp.Implementation.FileSystem;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<FileSettings>(builder.Configuration.GetSection("FileSettings"));

RegisterServices(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if(builder.Environment.IsDevelopment())
{
    //Allow CORS in development, so we can access the API from our simple web page.
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("DevCorsPolicy", builder =>
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader());
    });
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("DevCorsPolicy");
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