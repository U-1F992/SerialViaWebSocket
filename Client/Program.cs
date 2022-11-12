using System.IO.Ports;
using System.Net.WebSockets;


var cancellationTokenSource = new CancellationTokenSource();
var cancellationToken = cancellationTokenSource.Token;
Console.CancelKeyPress += Common.CancelIfCancelKeyPress(cancellationTokenSource);


using var serialPort = Common.SerialPortFromConfig();
serialPort.Open();


var uri = Common.UriFromConfig();
using var webSocket = new ClientWebSocket();
await webSocket.ConnectAsync(new Uri(uri), CancellationToken.None);


serialPort.DataReceived += (sender, eventArgs) =>
{
    var sp = sender as SerialPort;
    if (sp == null || !sp.IsOpen)
    {
        return;
    }
    
    var bytesToRead = serialPort.BytesToRead;
    var buffer = Enumerable.Repeat<byte>(0, bytesToRead).ToArray();

    sp.Read(buffer, 0, bytesToRead);
    webSocket.SendAsync(buffer, WebSocketMessageType.Binary, true, CancellationToken.None).Wait();
    
    Common.WriteBuffer(buffer);
};


Common.WaitCancellation(cancellationToken);
