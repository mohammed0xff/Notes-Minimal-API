using NotesMinimalApi.Data;
using NotesMinimalApi.Models;
using NotesMinimalApi.Services;
using NotesMinimalApi.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
    );

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});

builder.Services.AddAuthorization();


builder.Services.AddScoped<IAuthentication, Authentication>();
builder.Services.AddScoped<INotesRepositroy, NotesRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();



app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("api/auth/login", Auth.Login);
app.MapPost("api/auth/register", Auth.Register);
app.MapPost("api/auth/logout",  Auth.Logout);


app.MapGet("api/notes", Notes.GetAllNotes)
    .RequireAuthorization();

app.MapGet("api/notes/{id}", Notes.GetNoteById)
    .RequireAuthorization();

app.MapPost("api/notes", Notes.CreateNewNote)
    .RequireAuthorization();

app.MapPut("api/notes/{id}", Notes.UpdateNote)
    .RequireAuthorization();

app.MapDelete("api/notes/{id}", Notes.DeleteNote)
    .RequireAuthorization();


app.Run();

