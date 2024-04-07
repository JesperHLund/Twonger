using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using TweetService;
using TweetService.Database;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton(new MessageClient(RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")));
builder.Services.AddDbContext<Database.TweetContext>(options => options.UseInMemoryDatabase("TweetDatabase"));
builder.Services.AddHostedService<MessageHandler>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseAuthorization();

app.MapControllers();

app.Run();
