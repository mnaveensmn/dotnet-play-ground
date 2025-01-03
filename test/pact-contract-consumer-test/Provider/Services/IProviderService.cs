using pact_contract_test.Provider.Model;

namespace pact_contract_test.ProviderService;

public interface IProviderService
{
    public User GetUser(string id);
}