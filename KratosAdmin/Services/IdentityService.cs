using Ory.Client.Model;

namespace KratosAdmin.Services;

public class IdentityService(ApiService apiService)
{
    public async Task<List<ClientIdentity>> ListIdentities()
    {
        return await apiService.IdentityApi.ListIdentitiesAsync();
    }
}