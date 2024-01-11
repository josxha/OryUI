using KratosSelfServiceBlazor.Extensions;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;
using Ory.Hydra.Client.Model;

namespace KratosSelfServiceBlazor.Components.Pages;

public partial class Consent
{
    [SupplyParameterFromQuery(Name = "challenge")]
    private string? challengeToken { get; set; }

    private bool _isLoading = true;
    private HydraOAuth2ConsentRequest _challengeRequest = default!;
    private string _clientName = default!;

    protected override async Task OnInitializedAsync()
    {
        if (env.HydraAdminUrl == null)
        {
            accessor.HttpContext!.Response.StatusCode = 404;
            return;
        }
        var oAuth2Api = api.HydraOAuth2!;

        // This section processes consent requests and either shows the consent UI or accepts
        // the consent request right away if the user has given consent to this app before.
        _challengeRequest = await oAuth2Api.GetOAuth2ConsentRequestAsync(challengeToken);
        // If a user has granted this application the requested scope, hydra will tell us to not show the UI.
        if (!CanSkipConsent(_challengeRequest))
        {
            return;
        }

        logger.LogDebug("Skipping consent request and accepting it.");

        // Now it's time to grant the consent request. You could also deny the request if something went terribly wrong
        var acceptRequest = await AcceptRequest(
            challengeToken,
            _challengeRequest.RequestedScope,
            _challengeRequest.RequestedAccessTokenAudience,
            false
        );

        _clientName = string.IsNullOrWhiteSpace(_challengeRequest._Client.ClientName)
            ? _challengeRequest._Client.ClientId
            : _challengeRequest._Client.ClientName;

        logger.LogDebug("Consent request successfully accepted");
        // All we need to do now is to redirect the user back to hydra
        nav.NavigateTo(acceptRequest.RedirectTo);
    }

    private async Task<HydraOAuth2RedirectTo> AcceptRequest(string challenge, List<string> grantScopes,
        List<string> grantAccessTokenAudience, bool remember)
    {
        // extractSession only gets the session data from the request
        // You can extract more data from the Ory Identities admin API
        var kratosSession = accessor.HttpContext!.GetSession()!;
        var kratosTraits = (JObject)kratosSession.Identity.Traits;
        // https://openid.net/specs/openid-connect-core-1_0.html#StandardClaims
        var idToken = new JObject();

        if (env.HydraTraitsMapping != null)
            foreach (var mapping in env.HydraTraitsMapping.Split(","))
            {
                var parts = mapping.Split(":");
                var oidcClaim = parts[1];
                if (!grantScopes.Contains(oidcClaim)) continue;

                var traitPathSegments = parts[0].Split(".");
                var tmpTraits = kratosTraits;
                for (var i = 0; i < traitPathSegments.Length; i++)
                {
                    // last index
                    if (i == traitPathSegments.Length - 1)
                    {
                        idToken[oidcClaim] = tmpTraits[traitPathSegments.Last()];
                        break;
                    }

                    // not last index
                    if (tmpTraits[traitPathSegments[i]] is JObject jObject)
                    {
                        tmpTraits = jObject;
                    }
                    else
                    {
                        logger.LogError("Can't find identity trait {TraitPath}", parts[0]);
                        break;
                    }
                }
            }

        // The session allows us to set session data for id and access tokens
        var hydraSession = new HydraAcceptOAuth2ConsentRequestSession(idToken: idToken);

        return await api.HydraOAuth2!.AcceptOAuth2ConsentRequestAsync(challenge,
            new HydraAcceptOAuth2ConsentRequest
            {
                // We can grant all scopes that have been requested - hydra already checked for us that no
                // additional scopes are requested accidentally.
                GrantScope = grantScopes,
                // If the environment variable CONFORMITY_FAKE_CLAIMS is set we are assuming that
                // the app is built for the automated OpenID Connect Conformity Test Suite. You
                // can peak inside the code for some ideas, but be aware that all data is fake
                // and this only exists to fake a login system which works in accordance to OpenID Connect.
                // If that variable is not set, the session will be used as-is.
                Session = hydraSession,
                // ORY Hydra checks if requested audiences are allowed by the client, so we can simply echo this.
                GrantAccessTokenAudience = grantAccessTokenAudience,
                // This tells hydra to remember this consent request and allow the same client to request the same
                // scopes from the same user, without showing the UI, in the future.
                Remember = remember,
                // When this "remember" session expires, in seconds. Set this to 0 so it will never expire.
                RememberFor = env.HydraRememberConsentSessionForSeconds,
                HandledAt = DateTime.Now
            });
    }

    private bool CanSkipConsent(HydraOAuth2ConsentRequest challenge)
    {
        if (challenge.Skip || challenge._Client.SkipConsent) return true;

        if (challenge._Client.ClientId == null || env.HydraTrustedClientIds == null) return false;
        return env.HydraTrustedClientIds.Split(",").Contains(challenge._Client.ClientId);
    }
}