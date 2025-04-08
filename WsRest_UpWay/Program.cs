using System.Text;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.IdentityModel.Tokens;
using WsRest_UpWay.Models;
using WsRest_UpWay.Models.Cache;
using WsRest_UpWay.Models.DataManager;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;
using MemoryCache = Microsoft.Extensions.Caching.Memory.MemoryCache;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<S215UpWayContext>(options =>
    options.UseNpgsql(builder.Configuration["DB_CONNECTION_URL"],
        x => x.MigrationsHistoryTable("__EFMigrationsHistory", "upways")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDataRepository<CompteClient>, UserManager>();
builder.Services.AddScoped<IDataAccessoire, AccessoireManager>();
builder.Services.AddScoped<IDataRepository<Information>, InformationManager>();
builder.Services.AddScoped<IDataRepository<Magasin>, MagasinManager>();
builder.Services.AddScoped<IDataRepository<Marque>, MarqueManager>();
builder.Services.AddScoped<IDataPanier, PanierManager>();
builder.Services.AddScoped<IDataRepository<DetailCommande>, DetailCommandeManager>();
builder.Services.AddScoped<IDataRepository<CategorieArticle>, CategorieArticleManager>();
builder.Services.AddScoped<IDataRepository<Categorie>, CategorieManager>();
builder.Services.AddScoped<IDataRepository<LignePanier>, LignePanierManager>();
builder.Services.AddScoped<IDataRepository<MarquageVelo>, MarquageVeloManager>();
builder.Services.AddScoped<IDataRepository<Assurance>, AssuranceManager>();
builder.Services.AddScoped<IDataRepository<AjouterAccessoire>, AjouterAccessoireManager>();
builder.Services.AddScoped<IDataRepository<Article>, ArticleManager>();
builder.Services.AddScoped<IDataContenuArticles, ContenuArticlesManager>();
builder.Services.AddScoped<IDataArticles, ArticleManager>();
builder.Services.AddScoped<IDataVelo, VeloManager>();
builder.Services.AddScoped<IDataEstRealise, EstRealiseManager>();

CacheType type;
if (!Enum.TryParse(builder.Configuration["CACHE_TYPE"], out type))
{
    type = CacheType.Memory;
    Console.WriteLine("Failed to parse cache type! Defaulting to Memory.");
}

switch (type)
{
    case CacheType.Memory:
    {
        builder.Services.AddSingleton<IMemoryCache>(sp => new MemoryCache(new MemoryCacheOptions
        {
            SizeLimit = int.Parse(builder.Configuration["CACHE_SIZE"])
        }));

        builder.Services.AddSingleton<ICache, WsRest_UpWay.Models.Cache.MemoryCache>();
        break;
    }
    case CacheType.Redis:
    {
        builder.Services.AddSingleton<IDistributedCache>(sp => new RedisCache(new RedisCacheOptions
        {
            Configuration = builder.Configuration["REDIS_CONFIGURATION"],
            InstanceName = builder.Configuration["REDIS_INSTANCE_NAME"]
        }));

        builder.Services.AddSingleton<ICache, DistributedCache>();
        break;
    }
}


builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT_ISSUER"],
            ValidAudience = builder.Configuration["JWT_AUDIENCE"],
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT_SECRET_KEY"])),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization(config =>
{
    config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
    config.AddPolicy(Policies.User, Policies.UserPolicy());
});

builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            httpContext.Connection.RemoteIpAddress + ":" + httpContext.Connection.RemotePort,
            partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 30,
                QueueLimit = 10,
                Window = TimeSpan.FromMinutes(1)
            }));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(origin => true)
        .AllowCredentials());
else
    app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod()
        .WithOrigins(builder.Configuration["FRONTEND_URL"].Split(";"))
        .AllowCredentials());

app.UseRateLimiter();

using (var scope = app.Services.CreateScope())
{
    // ensure database is up to date with latest migrations
    var db = scope.ServiceProvider.GetRequiredService<S215UpWayContext>();
    var migrations = db.Database.GetPendingMigrations();

    if (migrations.Any()) db.Database.Migrate();
}


app.UseAuthentication();
app.UseAuthorization();

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