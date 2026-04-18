var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

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

app.Run();

record Todo(int Id, string Title, bool IsComplete);
record TodoInput(string Title, bool IsComplete);
