using KratosSelfService.Extensions;
using KratosSelfService.Models;
using KratosSelfService.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Ory.Hydra.Client.Model;

namespace KratosSelfService.Controllers;

public class OAuth2Controller(ILogger<OAuth2Controller> logger, ApiService api, EnvService env) : Controller
{
    [HttpGet("consent")]
    public async Task<IActionResult> ConsentGet([FromQuery(Name = "consent_challenge")] string challenge)
    {
        if (env.HydraAdminUrl == null) return NotFound();
        var oAuth2Api = api.HydraOAuth2!;

        // This section processes consent requests and either shows the consent UI or accepts
        // the consent request right away if the user has given consent to this app before.
        var challengeRequest = await oAuth2Api.GetOAuth2ConsentRequestAsync(challenge);
        // If a user has granted this application the requested scope, hydra will tell us to not show the UI.
        if (!CanSkipConsent(challengeRequest))
        {
            var model = new ConsentModel(challengeRequest);
            return View("Consent", model);
        }

        logger.LogDebug("Skipping consent request and accepting it.");

        // Now it's time to grant the consent request. You could also deny the request if something went terribly wrong
        var consentRequest = await oAuth2Api.AcceptOAuth2ConsentRequestAsync(challenge,
            new HydraAcceptOAuth2ConsentRequest
            {
                // We can grant all scopes that have been requested - hydra already checked for
                // us that no additional scopes are requested accidentally.
                GrantScope = challengeRequest.RequestedScope,
                // ORY Hydra checks if requested audiences are allowed by the client, so we can simply echo this.
                GrantAccessTokenAudience = challengeRequest.RequestedAccessTokenAudience,
                // The session allows us to set session data for id and access tokens
                Session = new HydraAcceptOAuth2ConsentRequestSession()
            });

        logger.LogDebug("Consent request successfully accepted");
        // All we need to do now is to redirect the user back to hydra
        return Redirect(consentRequest.RedirectTo);
    }

    [HttpPost("consent")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConsentPost(
        [FromForm(Name = "consent_challenge")] string challenge,
        [FromForm] bool remember,
        [FromForm(Name = "grant_scope")] List<string> grantScopes,
        [FromForm] string action)
    {
        if (env.HydraAdminUrl == null) return NotFound();
        var oAuth2Api = api.HydraOAuth2!;

        // extractSession only gets the session data from the request
        // You can extract more data from the Ory Identities admin API

        // Let's fetch the consent request again to be able to set `grantAccessTokenAudience` properly.
        // Let's see if the user decided to accept or reject the consent request.
        if (action != "accept")
        {
            // Looks like the consent request was denied by the user
            logger.LogDebug("Consent request denied by the user");
            var rejectRequest = await oAuth2Api.RejectOAuth2ConsentRequestAsync(challenge,
                new HydraRejectOAuth2Request
                {
                    Error = "access_denied",
                    ErrorDescription = "The resource owner denied the request"
                });
            // All we need to do now is to redirect the browser back to hydra
            return Redirect(rejectRequest.RedirectTo);
        }

        // Let's fetch the consent request again to be able to set `grantAccessTokenAudience` properly.
        // Let's see if the user decided to accept or reject the consent request.
        logger.LogDebug("Consent request was accepted by the user");
        var consentRequest = await oAuth2Api.GetOAuth2ConsentRequestAsync(challenge);

        var kratosSession = HttpContext.GetSession()!;
        var kratosTraits = (JObject)kratosSession.Identity.Traits;

        // https://openid.net/specs/openid-connect-core-1_0.html#StandardClaims
        var idToken = new JObject();

        if (env.HydraTraitsMapping != null)
        {
            var list = env.HydraTraitsMapping.Split(",");
            foreach (var entry in list)
            {
                var parts = entry.Split(":");
                if (!grantScopes.Contains("email") || kratosTraits["email"] == null) continue;
                var pathItems = parts[0].Split(".");
                var traits = kratosTraits;
                var success = true;
                for (var i = 0; i < pathItems.Length - 1; i++)
                    if (traits[pathItems[i]] is JObject jObject)
                    {
                        traits = jObject;
                    }
                    else
                    {
                        success = false;
                        break;
                    }

                if (success)
                    idToken[parts[1]] = traits[pathItems.Last()];
                else
                    logger.LogError("Can't find identity trait {TraitPath}", parts[0]);
            }
        }

        // The session allows us to set session data for id and access tokens
        var hydraSession = new HydraAcceptOAuth2ConsentRequestSession(idToken: idToken);

        var acceptRequest = await oAuth2Api.AcceptOAuth2ConsentRequestAsync(challenge,
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
                GrantAccessTokenAudience = consentRequest.RequestedAccessTokenAudience,
                // This tells hydra to remember this consent request and allow the same client to request the same
                // scopes from the same user, without showing the UI, in the future.
                Remember = remember,
                // When this "remember" session expires, in seconds. Set this to 0 so it will never expire.
                RememberFor = env.HydraRememberConsentSessionForSeconds,
                HandledAt = DateTime.Now
            });

        // All we need to do now is to redirect the user back!
        logger.LogDebug("Consent request successfully accepted, redirect to: {URL}", acceptRequest.RedirectTo);
        return Redirect(acceptRequest.RedirectTo);
    }

    private bool CanSkipConsent(HydraOAuth2ConsentRequest challenge)
    {
        if (challenge.Skip || challenge._Client.SkipConsent) return true;

        if (challenge._Client.ClientId == null || env.HydraTrustedClientIds == null) return false;
        return env.HydraTrustedClientIds.Split(",").Contains(challenge._Client.ClientId);
    }
}