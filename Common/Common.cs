using System.IO.Ports;
using System.Text.Json;

public static class Common
{
    public static ConsoleCancelEventHandler CancelIfCancelKeyPress(CancellationTokenSource cancellationTokenSource)
    {
        return (sender, eventArgs) =>
        {
            cancellationTokenSource.Cancel();
            eventArgs.Cancel = true;
        };
    }

    static string Resolve(string fileName)
    {
        return Path.Join(AppContext.BaseDirectory, fileName);
    }

    public static SerialPort SerialPortFromConfig()
    {
        return SerialPortFactory.FromJson(Common.Resolve("serialPort.config.json"));
    }

    public static string UriFromConfig()
    {
        return JsonSerializer.Deserialize<ServerConfig>(File.ReadAllText(Common.Resolve("server.config.json"))).Uri;
    }

    public static void WriteBuffer(byte[] buffer)
    {
        foreach (var bin in buffer)
        {
            Console.Write("{0,0:X2} ", bin);
        }
        Console.Write("\n");
    }

    public static void WaitCancellation(CancellationToken cancellationToken)
    {
        try
        {
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }
        }
        catch (OperationCanceledException e)
        {
            Console.Error.WriteLine(e.Message);
        }
    }
}
