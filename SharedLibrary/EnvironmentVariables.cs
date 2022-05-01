using Microsoft.Extensions.Configuration;
using System;

namespace SharedLibrary
{
    public static class EnvironmentVariables
    {
        /// <summary>
        /// Return connection string from environment. Default option is for SqlServer
        /// </summary>
        public static string GetConnectionStringFromENV(this IConfiguration configuration, string name = null)
        {
            return string.IsNullOrEmpty(name)
                ? Environment.GetEnvironmentVariable("ConnectionStringSqlServer", EnvironmentVariableTarget.Machine)
                : Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Machine);
        }

        public static string GetJwtIssuerFromENV(this IConfiguration configuration)
        {
            return Environment.GetEnvironmentVariable("JwtIssuer", EnvironmentVariableTarget.Machine);
        }
        public static string GetJwtAudienceFromENV(this IConfiguration configuration)
        {
            return Environment.GetEnvironmentVariable("JwtAudience", EnvironmentVariableTarget.Machine);
        }
        public static string GetJwtKeyFromENV(this IConfiguration configuration)
        {
            return Environment.GetEnvironmentVariable("JwtKey", EnvironmentVariableTarget.Machine);
        }
    }
}
