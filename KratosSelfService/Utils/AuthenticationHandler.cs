using System.Security.Claims;
using KratosSelfService.Services;
using Microsoft.AspNetCore.Authentication;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.Utils;

/**
 * Created per request to handle authentication for a particular scheme.
 */
public class AuthenticationHandler(ApiService api) : IAuthenticationHandler
{
    private HttpContext _context = null!;

    /**
     * Initialize the authentication handler. The handler should initialize anything it needs
     * from the request and scheme as part of this method.
     */
    public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
    {
        _context = context;
        return Task.CompletedTask;
    }

    /**
     * An authentication scheme's authenticate action is responsible for constructing the user's
     * identity based on request context. It returns an AuthenticateResult indicating whether
     * authentication was successful and, if so, the user's identity in an authentication ticket.
     */
    public async Task<AuthenticateResult> AuthenticateAsync()
    {
        var cookies = _context.Request.Headers.Cookie;
        KratosSession session;
        try
        {
            session = await api.Frontend.ToSessionAsync(cookie: cookies);
        }
        catch (ApiException exception)
        {
            return AuthenticateResult.Fail("No valid session.");
        }

        if (!session.Active)
            return AuthenticateResult.Fail("The session is inactive.");

        _context.Items[typeof(KratosSession)] = session;

        // Construct AuthenticationTicket objects representing the user's identity
        // if authentication is successful
        // https://andrewlock.net/introduction-to-authentication-with-asp-net-core/
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, session.Identity.Id, ClaimValueTypes.String)
        };
        var identity = new ClaimsIdentity(claims, "KratosIdentity");
        var principal = new ClaimsPrincipal(identity);
        var authTicket = new AuthenticationTicket(principal, "LoggedIn");
        return AuthenticateResult.Success(authTicket);
    }

    /**
     * An authentication challenge is invoked by Authorization when an unauthenticated user requests an endpoint
     * that requires authentication. An authentication challenge is issued, for example, when an anonymous user
     * requests a restricted resource or follows a login link. Authorization invokes a challenge using the specified
     * authentication scheme(s), or the default if none is specified.
     * A challenge action should let the user know what authentication mechanism to use to access
     * the requested resource.
     */
    public Task ChallengeAsync(AuthenticationProperties? properties)
    {
        // redirect to login page
        _context.Response.Redirect("login");
        return Task.CompletedTask;
    }

    /**
     * An authentication scheme's forbid action is called by Authorization when an authenticated
     * user attempts to access a resource they're not permitted to access.
     */
    public Task ForbidAsync(AuthenticationProperties? properties)
    {
        // redirect to a page where the user can request the
        // authorisations but return 403 for now
        _context.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    }
}