using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// configure the swagger page 
builder.Services.AddSwaggerGen(opts =>
{
    var title = "Our Versioned API";
    var description = "This is a Web API that demonstrates versioning.";
    var terms = new Uri("https://localhost:7040/terms");
    var license = new OpenApiLicense()
    {
        Name = "This is my full license information or a link to it."
    };
    var contact = new OpenApiContact()
    {
        Name = "Letícia Egle Helpdesk",
        Email = "leticiaegle@yahoo.com.br",
        Url = new Uri("https://github.com/egleticia")
    };
    opts.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = $"{title} v1",
        Description = description,
        TermsOfService = terms,
        License = license,
        Contact = contact
    });

    opts.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2",
        Title = $"{title} v2",
        Description = description,
        TermsOfService = terms,
        License = license,
        Contact = contact
    });
});

builder.Services.AddApiVersioning(opts =>
{
    opts.AssumeDefaultVersionWhenUnspecified = true;
    opts.DefaultApiVersion = new(1, 0);
    opts.ReportApiVersions = true;
} );

builder.Services.AddVersionedApiExplorer(opts =>
{
    opts.GroupNameFormat = "'v'VVV";
    opts.SubstituteApiVersionInUrl= true;
}


);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opts =>
    {
        opts.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
        opts.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
