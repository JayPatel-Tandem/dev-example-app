using Okta.AspNetCore;
using System.Runtime.CompilerServices;

namespace dev_example_app.Extensions
{
    public static class OktaAuthenticationExtensions
    {

        public static void AddOktaAuthnetication(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = OktaDefaults.ApiAuthenticationScheme;
                options.DefaultChallengeScheme = OktaDefaults.ApiAuthenticationScheme;
            })
                .AddOktaWebApi(new OktaWebApiOptions()
                {
                    OktaDomain = configuration["Okta:OktaDomain"],
                    AuthorizationServerId = configuration["Okta:AuthorizationServerId"],
                    Audience = configuration["Okta:Audience"]
                });

            service.AddAuthorization();
        }

    }
}
