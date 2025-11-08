using AiJobEx1.Application.Ai.Commands;
using AiJobEx1.Application.Common.Interfaces;
using AiJobEx1.Application.JobDescriptions.Commands;
using AiJobEx1.Application.JobDescriptions.Queries;
using AiJobEx1.Infrastructure;
using AiJobEx1.Infrastructure.Persistence;
using AiJobEx1.Web.Data;
using AiJobEx1.Web.Hubs;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext();
});

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddLocalization(options => options.ResourcesPath = "Localization");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddMediatR(typeof(AskAiCommand).Assembly, typeof(GetMyJobDescriptionQuery).Assembly, typeof(RequestJobDescriptionChangeCommand).Assembly);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdmin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequireHR", policy => policy.RequireRole("HR", "Admin"));
});

builder.Services.AddSignalR();

builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>("Database");

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAccessContext, HttpAccessContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseRequestLocalization(options =>
{
    options.SetDefaultCulture("fa-IR");
    options.AddSupportedUICultures("fa-IR", "en-US");
    options.AddSupportedCultures("fa-IR", "en-US");
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapHub<AiChatHub>("/hubs/ai-chat");
app.MapFallbackToPage("/_Host");
app.MapHealthChecks("/health");
app.MapHealthChecks("/ready");

app.Run();

public partial class Program
{
}
