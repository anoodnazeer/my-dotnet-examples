

using Domain.Extensions;
using Domain.Mail;
using Domain.Models;
using Job_Portal.API.Admin.Helper;
using Job_Portal.API.JobSeeker.Helper;
using Job_Portal.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.Configure<MailSettings>("Provider", builder.Configuration.GetSection("ProviderMailSettings"));

builder.Services.AddAutoMapper(typeof(SeekerProfiles));
//builder.Services.Configure<Domain.Helper.MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddAutoMapper(typeof(SeekerProfile));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AuthSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };

        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = async context =>
            {
                var db = context.HttpContext.RequestServices.GetRequiredService<HireMeNowDbContext>();
                var userId = context.Principal.FindFirst(ClaimTypes.Sid)?.Value;
                var tokenConnectionId = context.Principal.FindFirst("ConnectionId")?.Value;

                if (Guid.TryParse(userId, out var guid))
                {
                    var user = await db.AuthUsers.FindAsync(guid);

                    if (user == null || user.ConnectionId == null || user.ConnectionId != tokenConnectionId)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("Session expired or invalid.");

                    }
                }
            }
        };
    });


builder.Services.AddCors(options => options.AddPolicy(name: "NgOrigins",
policy =>
{
    policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));


builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;

    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();