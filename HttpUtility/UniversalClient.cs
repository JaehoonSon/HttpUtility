using System.Net;

namespace HttpUtility;

public class UniversalClient : IDisposable
{
    public static int _success = 0;
    private static bool _isRunning = true;
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

    public async Task ReadHttp()
    {
        try
        {
            var data = await _client.GetAsync(BaseURL);
            Console.WriteLine(await data.Content.ReadAsStringAsync());
        }
        catch { }
    }

    public static void AddQuery(string key, string value)
    {
        string query = "?";
        var values = value.Split(" ");
        if (values.Length == 0)
        {
            query = "";
            Console.WriteLine("Invalid value");
        }
        else if (values.Length == 1)
        {
            query += key + "=" + values[0];
        }
        else
        {
            query += key + "=" + values[0];
            for (int i = 1; i < values.Length; i++)
            {
                query += "+" + values[i];
            }
        }
        BaseURL += query;
    }


    public async Task GetHttpByNumberAsync(int number)
    {
        try
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
        catch { }

    }

    public void Dispose()
    {
    }
}
