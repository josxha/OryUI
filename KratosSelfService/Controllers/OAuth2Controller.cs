using System.Text.Json.Nodes;
using KratosSelfService.Models;
using KratosSelfService.Services;
using Microsoft.AspNetCore.Mvc;
using Ory.Hydra.Client.Model;

namespace KratosSelfService.Controllers;

public class OAuth2Controller(ILogger<OAuth2Controller> logger, ApiService api, EnvService env) : Controller
{
    [HttpGet("consent")]
    public async Task<IActionResult> ConsentGet([FromQuery(Name = "consent_challenge")] string challenge)
    {
        if (!HydraEnabled()) return NotFound();
        var oAuth2Api = api.HydraOAuth2!;

        // This section processes consent requests and either shows the consent UI or accepts
        // the consent request right away if the user has given consent to this app before.
        var challengeRequest = await oAuth2Api.GetOAuth2ConsentRequestAsync(challenge);
        // If a user has granted this application the requested scope, hydra will tell us to not show the UI.
        if (!CanSkipConsent(challengeRequest))
        {
            logger.LogDebug("Expected CSRF token middleware to be set but received none.");
            return BadRequest("Expected CSRF token middleware to be set but received none.");
        }

        logger.LogDebug("Skipping consent request and accepting it.");

        // Now it's time to grant the consent request. You could also deny the request if something went terribly wrong
        var consentRequest = await oAuth2Api.AcceptOAuth2ConsentRequestAsync(challenge,
            new HydraAcceptOAuth2ConsentRequest
            {
                // We can grant all scopes that have been requested - hydra already checked for us that no additional scopes
                // are requested accidentally.
                GrantScope = challengeRequest.RequestedScope,
                // ORY Hydra checks if requested audiences are allowed by the client, so we can simply echo this.
                GrantAccessTokenAudience = challengeRequest.RequestedAccessTokenAudience,
                // The session allows us to set session data for id and access tokens
                Session = new HydraAcceptOAuth2ConsentRequestSession()
            });

        logger.LogDebug("Consent request successfuly accepted");
        // All we need to do now is to redirect the user back to hydra!
        return Redirect(consentRequest.RedirectTo);
    }

    [HttpPost("consent")]
    public async Task<IActionResult> ConsentPost([FromBody] ConsentBody body)
    {
        if (!HydraEnabled()) return NotFound();
        var oAuth2Api = api.HydraOAuth2!;

        // extractSession only gets the session data from the request
        // You can extract more data from the Ory Identities admin API

        // Let's fetch the consent request again to be able to set `grantAccessTokenAudience` properly.
        // Let's see if the user decided to accept or reject the consent request.
        if (body.Action != "accept")
        {
            // Looks like the consent request was denied by the user
            logger.LogDebug("Consent request denied by the user");
            var rejectRequest = await oAuth2Api.RejectOAuth2ConsentRequestAsync(body.Challenge,
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
        var consentRequest = await oAuth2Api.GetOAuth2ConsentRequestAsync(body.Challenge);

        var grantScope = body.GrantScope.Split(",").ToList();
        var session = new HydraAcceptOAuth2ConsentRequestSession();

        var acceptRequest = await oAuth2Api.AcceptOAuth2ConsentRequestAsync(body.Challenge,
            new HydraAcceptOAuth2ConsentRequest
            {
                // We can grant all scopes that have been requested - hydra already checked for us that no additional scopes
                // are requested accidentally.
                GrantScope = grantScope,
                // If the environment variable CONFORMITY_FAKE_CLAIMS is set we are assuming that
                // the app is built for the automated OpenID Connect Conformity Test Suite. You
                // can peak inside the code for some ideas, but be aware that all data is fake
                // and this only exists to fake a login system which works in accordance to OpenID Connect.
                // If that variable is not set, the session will be used as-is.
                Session = OidcConformityMaybeFakeSession(grantScope, session),
                // ORY Hydra checks if requested audiences are allowed by the client, so we can simply echo this.
                GrantAccessTokenAudience = consentRequest.RequestedAccessTokenAudience,
                // This tells hydra to remember this consent request and allow the same client to request the same
                // scopes from the same user, without showing the UI, in the future.
                Remember = body.Remember,
                // When this "remember" session expires, in seconds. Set this to 0 so it will never expire.
                RememberFor = env.HydraRememberConsentSessionForSeconds
            });

        // All we need to do now is to redirect the user back!
        logger.LogDebug("Consent request successfully accepted, redirect to: {URL}", acceptRequest.RedirectTo);
        return Redirect(acceptRequest.RedirectTo);
    }

    private HydraAcceptOAuth2ConsentRequestSession OidcConformityMaybeFakeSession(List<string> grantScope,
        HydraAcceptOAuth2ConsentRequestSession session)
    {
        var idToken = new Dictionary<string, object?>();

        // If the email scope was granted, fake the email claims.
        if (grantScope.Contains("email"))
        {
            // But only do so if the email was requested!
            idToken["email"] = "foo@bar.com";
            idToken["email_verified"] = true;
        }

        // If the phone scope was granted, fake the phone claims.
        if (grantScope.Contains("phone"))
        {
            idToken["phone_number"] = "1337133713371337";
            idToken["phone_number_verified"] = true;
        }

        // If the profile scope was granted, fake the profile claims.
        if (grantScope.Contains("profile"))
        {
            idToken["name"] = "Foo Bar";
            idToken["given_name"] = "Foo";
            idToken["family_name"] = "Bar";
            idToken["website"] = "https://www.ory.sh";
            idToken["zoneinfo"] = "Europe/Berlin";
            idToken["birthdate"] = "1.1.2014";
            idToken["gender"] = "robot";
            idToken["profile"] = "https://www.ory.sh";
            idToken["preferred_username"] = "robot";
            idToken["middle_name"] = "Baz";
            idToken["locale"] = "en-US";
            idToken["picture"] =
                "https://raw.githubusercontent.com/ory/web/master/static/images/favico.png";
            idToken["updated_at"] = 1604416603;
            idToken["nickname"] = "foobot";
        }

        // If the address scope was granted, fake the address claims.
        if (grantScope.Contains("address"))
            idToken["address"] = new List<string>
            {
                "Localhost",
                "Intranet",
                "Local Street 1337"
            };

        foreach (var item in (session.IdToken as JsonObject)!) idToken[item.Key] = item.Value;

        return new HydraAcceptOAuth2ConsentRequestSession(session.AccessToken, idToken);
    }

    private bool HydraEnabled()
    {
        return env is { HydraCsrfCookieSecret: not null, HydraCsrfCookieName: not null, HydraAdminUrl: not null };
    }

    private bool CanSkipConsent(HydraOAuth2ConsentRequest challenge)
    {
        if (challenge.Skip || challenge._Client.SkipConsent) return true;

        var trustedClients = env.HydraTrustedClientIds?.Split(",") ?? Array.Empty<string>();
        return challenge._Client.ClientId != null &&
               trustedClients.Contains(challenge._Client.ClientId);
    }
}