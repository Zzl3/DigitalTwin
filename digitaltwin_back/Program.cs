using auth.Services;
using auth.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ������������
builder.Services.AddCors(c => c.AddPolicy("any", p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));


// Add services to the container.
/*builder.Services.AddControllers();*/
builder.Services.AddControllers(setupAction =>
{
    setupAction.ReturnHttpNotAcceptable = true;
}).AddJsonOptions(options =>
{
    // ����ѭ��Ƕ�׶���Ľ�����
    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
}).AddXmlDataContractSerializerFormatters();


// ע��JWT������
builder.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection(nameof(JWTTokenOptions)));
builder.Services.AddTransient<IJWTService, JWTService>();

// ����ע��
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IFileService, FileService>();

builder.Services.AddTransient<DataManageService>();
builder.Services.AddTransient<ConsumptionService>();
builder.Services.AddTransient<OnandoffService>();
builder.Services.AddTransient<UserusingService>();
builder.Services.AddTransient<UserloadingService>();
builder.Services.AddTransient<RecentconsumptionService>();
builder.Services.AddTransient<AirelectricityService>();
builder.Services.AddTransient<AirstateService>();
builder.Services.AddTransient<RunstrategyService>();
builder.Services.AddTransient<SystemairService>();
builder.Services.AddTransient<SystemelectricityService>();
builder.Services.AddTransient<ElectricityfeeService>();


// ����JWT��tokenУ��ѡ��
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var secretByte = Encoding.UTF8.GetBytes(builder.Configuration["JWTTokenOptions:SecretKey"]);
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,//�Ƿ���֤������
            ValidateAudience = true,//�Ƿ���֤������
            ValidateLifetime = true,//�Ƿ���֤����ʱ��
            ValidateIssuerSigningKey = true,//�Ƿ���֤ǩ��
            IssuerSigningKey = new SymmetricSecurityKey(secretByte),//������Կ
            ValidIssuer = builder.Configuration["JWTTokenOptions:Issuser"],//������
            ValidAudience = builder.Configuration["JWTTokenOptions:Audience"],//������
            ClockSkew = TimeSpan.Zero,//�������ʱ�䣬����Ϊ0
            RequireExpirationTime = true,
        };
        options.Events = new JwtBearerEvents()
        {
            OnChallenge = context =>
            {
                bool isRefresh = context.Request.Path == "/auth/refresh";
                if (context.AuthenticateFailure?.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.HandleResponse();
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    var paylod = new { code = ResponseStatusCode.TokenExpired, msg = "��¼���ڣ������µ�¼" };
                    context.Response.Headers.Add("Token-Expired", true.ToString());
                    context.Response.WriteAsJsonAsync(paylod);
                }
                return Task.FromResult(0);
            }
        };
    });




// ����mysql���ݿ�
builder.Services.AddDbContext<auth.Database.AppDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("mysql"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql")));
});

// ����Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("redis")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    #region swagger���Ӷ�JWT��֧��
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "������token,��ʽΪ Bearer jwtToken(ע���м�����пո�)",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });//���Ӱ�ȫ����
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {   //���Ӱ�ȫҪ��
                    new OpenApiSecurityScheme
                    {
                        Reference =new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id ="Bearer"
                        }
                    },
                    new string[]{ }
                }
                });
    #endregion
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ���ÿ�������м��
app.UseCors("any");

app.UseHttpsRedirection();

// ��Ȩ��ȷ��������û�
app.UseAuthentication();//�����������У����Ȩ--����

// ��Ȩ��ȷ���û����Է�����Щ��Դ
app.UseAuthorization();

app.MapControllers();

app.Run();
