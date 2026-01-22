var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Регистрируем сервис
builder.Services.AddSingleton<ActivityEvaluationService>();

// === CORS ===
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()   // разрешаем любые домены
              .AllowAnyMethod()   // GET, POST, PUT, DELETE и т.д.
              .AllowAnyHeader();  // любые заголовки
    });
});

var app = builder.Build();

// Swagger в разработке
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Используем CORS
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
