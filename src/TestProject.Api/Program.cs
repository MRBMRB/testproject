var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => Results.Text("testproject: the pilot repository for Agent Workbench's Stage 4 writes."));
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.Run();

/// <summary>The app's entry point, public so the tests' WebApplicationFactory can start it.</summary>
public partial class Program;
