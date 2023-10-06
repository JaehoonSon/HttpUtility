using System.Net;

namespace HttpUtility;

public class UniversalClient : IDisposable
{
    public static int _success = 0;
    private static bool _isRunning = true;
    private bool _isDisposed = false;
    private readonly HttpClient _client;

    public static string? BaseURL { get; set; }

    public UniversalClient(WebProxy? proxy = null)
    {
        HttpClientHandler handler = new()
        {
            Proxy = proxy,
            UseProxy = proxy is not null,
        };


        _client = new(handler);
        ClientHelper.AddRandomUserAgent(ref _client);
    }


    public async Task GetHttpByNumber(int number)
    {
        switch (number)
        {
            case -1:
                while (_isRunning)
                {
                    await _client.GetAsync(BaseURL);
                    Interlocked.Increment(ref _success);
                }
                break;
            case > 0:
                for (int i = 0; i < number; i++)
                {
                    await _client.GetAsync(BaseURL);
                }
                break;
            default:
                Console.WriteLine("Invalid");
                break;
        }
    }

    public void Dispose()
    {
    }
}
