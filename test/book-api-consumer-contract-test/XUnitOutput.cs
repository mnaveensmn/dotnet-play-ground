using PactNet.Infrastructure.Outputters;
using Xunit.Abstractions;

namespace book_api_consumer_contract_test;

public class XUnitOutput: IOutput
{
    private readonly ITestOutputHelper _output;

    public XUnitOutput(ITestOutputHelper output)
    {
        _output = output;
    }

    public void WriteLine(string line)
    {
        _output.WriteLine(line);
    }
}