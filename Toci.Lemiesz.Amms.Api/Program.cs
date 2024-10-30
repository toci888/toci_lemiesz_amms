using Google.Api;
using Google.Cloud.Speech.V1;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddHttpClient<ChatGptService>(client =>
{
    client.BaseAddress = new Uri("https://api.openai.com/v1/completions");
    client.DefaultRequestHeaders.Add("Authorization", "Bearer YOUR_OPENAI_API_KEY");
});

//builder.Services.AddSingleton<Speech.SpeechClient>();
//builder.Services.AddScoped<SpeechToTextService>();
//builder.Services.AddScoped<ConversationProcessor>();
builder.Services.AddControllers();


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
