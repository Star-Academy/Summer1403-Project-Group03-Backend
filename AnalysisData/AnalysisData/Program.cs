using System.Text;
using AnalysisData.CookieService;
using AnalysisData.CookieService.abstractions;
using AnalysisData.Data;
using AnalysisData.DataProcessService;
using AnalysisData.Graph.DataProcessService;
using AnalysisData.JwtService;
using AnalysisData.JwtService.abstractions;
using AnalysisData.MiddleWare;
using AnalysisData.Repository.AccountRepository;
using AnalysisData.Repository.AccountRepository.Abstraction;
using AnalysisData.Repository.TransactionRepository;
using AnalysisData.Repository.TransactionRepository.Abstraction;
using AnalysisData.Repository.UserRepository;
using AnalysisData.Repository.UserRepository.Abstraction;
using AnalysisData.Services;
using AnalysisData.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IJwtService,JwtService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICookieService, CookieService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IAdminService,AdminService>();
builder.Services.AddScoped<IRegexService, RegexService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IDataProcessor, DataReadProcessor>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
// builder.Services.AddAuthentication(options =>
//     {
//         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//     })
//     .AddJwtBearer(options =>
//     {
//         options.RequireHttpsMetadata = false;
//         options.SaveToken = true;
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = false,
//             ValidateAudience = false,
//             ValidateLifetime = true,
//             ValidateIssuerSigningKey = true,
//             IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
//         };
//         options.Events = new JwtBearerEvents
//         {
//             OnMessageReceived = context =>
//             {
//                 var cookie = context.Request.Cookies["AuthToken"];
//                 if (!string.IsNullOrEmpty(cookie))
//                 {
//                     context.Token = cookie;
//                 }
//         
//                 return Task.CompletedTask;
//             }
//         };
//     });

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
}
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseRouting();
app.UseMiddleware<JwtMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();