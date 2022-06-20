using Microsoft.Extensions.Azure;
using REST_API_XFIA.Modules.Service;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAzureClients(b => {
    b.AddBlobServiceClient("DefaultEndpointsProtocol=https;AccountName=xfiaonline;AccountKey=GaMRXj40jLfqHarY9UMbvh/Oi/qmOkUGHLu2bFw8zPQDuy5HZkwkvo+S6yhkmMxTGmwLGizMq7uz+AStyVSVPQ==;EndpointSuffix=core.windows.net");});
builder.Services.AddTransient<IStorageService,StorageService>();
builder.Services.AddDbContext<REST_API_XFIA.SQL_Model.DB_Context.RESTAPIXFIA_dbContext>(ServiceLifetime.Transient);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                      });
});

var app = builder.Build();



// Configure the HTTP request pipeline.
/*
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
*/
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(MyAllowSpecificOrigins);

app.MapControllers();

app.Run();
