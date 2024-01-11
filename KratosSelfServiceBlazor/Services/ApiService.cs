﻿using KratosSelfServiceBlazor.Extensions;
using Ory.Hydra.Client.Api;
using Ory.Kratos.Client.Api;
using Ory.Kratos.Client.Client;

namespace KratosSelfServiceBlazor.Services;

public class ApiService(EnvService env)
{
    public readonly FrontendApi Frontend = new(new Configuration
    {
        BasePath = env.KratosPublicUrl
    });

    public readonly OAuth2Api? HydraOAuth2 = env.HydraAdminUrl == null
        ? null
        : new OAuth2Api(new Ory.Hydra.Client.Client.Configuration
        {
            BasePath = env.HydraAdminUrl
        });

    public string GetUrlForBrowserFlow(string flow, Dictionary<string, string?>? query = null)
    {
        var queryString = query?.EncodeQueryString();
        var baseUrl = env.KratosBrowserUrl ?? env.KratosPublicUrl;
        return $"{baseUrl.RemoveTrailingSlash()}/self-service/{flow}/browser?{queryString}";
    }
}