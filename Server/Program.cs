using Fleck;


var cancellationTokenSource = new CancellationTokenSource();
var cancellationToken = cancellationTokenSource.Token;
Console.CancelKeyPress += Common.CancelIfCancelKeyPress(cancellationTokenSource);


var baseDirectory = AppContext.BaseDirectory;
#if !DEBUG
    using var serialPort = Common.SerialPortFromConfig();
    serialPort.Open();
#endif


var uri = Common.UriFromConfig();
var server = new WebSocketServer(uri);
server.Start(socket =>
{
    socket.OnBinary = buffer =>
    {
#if !DEBUG
        serialPort.Write(buffer, 0, buffer.Length);
#endif
        Common.WriteBuffer(buffer);
    };
});


Common.WaitCancellation(cancellationToken);
