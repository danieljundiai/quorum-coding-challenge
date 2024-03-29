using quorum_data.dataaccess;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel(serverOptions =>
{
    serverOptions.ListenLocalhost(5000); // Para HTTP
    serverOptions.ListenLocalhost(5001, listenOptions => // Para HTTPS
    {
        listenOptions.UseHttps();
    });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddSingleton<VotesDataAccess>();
builder.Services.AddSingleton<VoteResultsDataAccess>();
builder.Services.AddSingleton<LegislatorsDataAccess>();
builder.Services.AddSingleton<BillsDataAccess>();

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(
        builder => {
            builder.WithOrigins("http://localhost:3000") // URL da aplicação React
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); 

app.UseCors();

app.Run();