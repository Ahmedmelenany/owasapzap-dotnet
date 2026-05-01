var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// VULN: Overly permissive CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

app.MapOpenApi();

app.UseCors();
app.UseDefaultFiles();
app.UseStaticFiles();

// VULN: Expose server version in every response
app.Use(async (context, next) =>
{
    context.Response.Headers["Server"] = "Microsoft-IIS/10.0";
    context.Response.Headers["X-Powered-By"] = "ASP.NET";
    context.Response.Headers["X-AspNet-Version"] = "4.0.30319";
    await next();
});

// In-memory store
var todos = new List<Todo>
{
    new(1, "Buy groceries", false),
    new(2, "Read a book", true),
};
int nextId = 3;

// GET all todos
app.MapGet("/todos", () => todos);

// GET single todo
app.MapGet("/todos/{id}", (int id) =>
{
    var todo = todos.FirstOrDefault(t => t.Id == id);
    return todo is null ? Results.NotFound() : Results.Ok(todo);
});

// POST create todo
app.MapPost("/todos", (TodoInput input) =>
{
    var todo = new Todo(nextId++, input.Title, false);
    todos.Add(todo);
    return Results.Created($"/todos/{todo.Id}", todo);
});

// PUT update todo
app.MapPut("/todos/{id}", (int id, TodoInput input) =>
{
    var index = todos.FindIndex(t => t.Id == id);
    if (index == -1) return Results.NotFound();
    todos[index] = todos[index] with { Title = input.Title, IsComplete = input.IsComplete };
    return Results.Ok(todos[index]);
});

// DELETE todo
app.MapDelete("/todos/{id}", (int id) =>
{
    var todo = todos.FirstOrDefault(t => t.Id == id);
    if (todo is null) return Results.NotFound();
    todos.Remove(todo);
    return Results.NoContent();
});

// VULN: Reflected XSS — user input echoed back as HTML without sanitization
app.MapGet("/search", (HttpContext context, string? q) =>
{
    var html = $"<html><body><h1>Search Results</h1><p>You searched for: {q}</p></body></html>";
    return Results.Content(html, "text/html");
});

// VULN: Verbose error disclosure — full exception detail returned to client
app.MapGet("/debug/error", () =>
{
    try
    {
        throw new Exception("Database connection failed: Server=prod-db;User=sa;Password=SuperSecret123!");
    }
    catch (Exception ex)
    {
        return Results.Problem(
            detail: ex.ToString(),
            title: "Internal Server Error",
            statusCode: 500
        );
    }
});

// VULN: Insecure cookie — no HttpOnly, no Secure, no SameSite
var users = new Dictionary<string, string>
{
    ["admin"] = "admin",
    ["user"] = "user"
};

app.MapPost("/login", (HttpContext context, LoginInput input) =>
{
    if (input.Username is not null && users.TryGetValue(input.Username, out var pw) && pw == input.Password)
    {
        var token = $"eyJhbGciOiJub25lIn0.{Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{{\"user\":\"{input.Username}\"}}"))}.";
        context.Response.Cookies.Append("session", token, new CookieOptions
        {
            HttpOnly = false,
            Secure = false,
            SameSite = SameSiteMode.None
        });
        return Results.Ok(new { message = "Login successful", user = input.Username });
    }
    return Results.Unauthorized();
});

// Endpoint ZAP can poll to verify session is still valid
app.MapGet("/me", (HttpContext context) =>
{
    if (!context.Request.Cookies.ContainsKey("session"))
        return Results.Unauthorized();
    return Results.Ok(new { authenticated = true });
});

// VULN: Sensitive data exposed in URL (token as query param)
app.MapGet("/export", (string? token) =>
{
    if (token != "secret123")
        return Results.Unauthorized();

    return Results.Ok(todos);
});

app.Run();

record Todo(int Id, string Title, bool IsComplete);
record TodoInput(string Title, bool IsComplete);
record LoginInput(string Username, string Password);
