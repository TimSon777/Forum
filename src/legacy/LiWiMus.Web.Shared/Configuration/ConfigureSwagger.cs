using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

namespace LiWiMus.Web.Shared.Configuration;

public static class ConfigureSwagger
{
    public static IServiceCollection AddSwaggerWithAuthorize(this IServiceCollection services, string appName)
    {
        var info = new OpenApiInfo
        {
            Title = appName
        };
        
        var scheme = new OpenApiSecurityScheme
        {
            Name = HeaderNames.Authorization,
            Type = SecuritySchemeType.OAuth2,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            BearerFormat = JwtConstants.TokenType,
            In = ParameterLocation.Header,
            
            Flows = new OpenApiOAuthFlows
            {
                Password = new OpenApiOAuthFlow
                {
                    TokenUrl = new Uri("http://localhost:5020/auth/connect/token")
                }
            }
        };

        var securityRequirement = new OpenApiSecurityRequirement();
        var securityScheme = new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "oauth2"
            }
        };
        
        securityRequirement.Add(securityScheme, Array.Empty<string>());
        
        return services.AddSwaggerGen(o =>
        {
            o.SwaggerDoc("v1", info);
            o.AddSecurityDefinition("oauth2", scheme);
            o.AddSecurityRequirement(securityRequirement);
            o.CustomSchemaIds(t => t.ToString());
        });
    }
}