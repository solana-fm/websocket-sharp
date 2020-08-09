using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace WebSocketSharp.NetCore.Tests
{
    public class WebSocketClientTests
    {
        [Fact]
        public void SyncClientConnection()
        {
            var websocket = new WebSocket("wss://s-usc1c-nss-209.firebaseio.com/.ws?v=5&ns=coinhako-1e092");

            websocket.Connect();
            
            Assert.True(true);
        }
        
        [Fact]
        public async Task AsyncClientConnection()
        {
            var websocket = new WebSocket("wss://s-usc1c-nss-209.firebaseio.com/.ws?v=5&ns=coinhako-1e092");

            await websocket.ConnectAsync();
            Assert.True(true);
        }

        [Fact]
        public async Task AsyncClientTestListeners()
        {
            var socket = new WebSocket("wss://stream.binance.com:9443/ws/!ticker@arr")
            {
                Compression = CompressionMethod.Deflate,
                EmitOnPing = true,
                EnableRedirection = false
            };
            var payload = JsonDocument.Parse("{}");
            
            // Pre-request processing
            socket.OnOpen += (sender, args) =>
            {
                Assert.True(true); // TODO: Some initial ops?
            };

            // Incoming processing
            socket.OnMessage += (sender, args) =>
            {
                if (args.IsPing) // Just in case the endpoint is a mother trucker
                    socket.Ping();
                else if (!string.IsNullOrEmpty(args.Data))
                {
                    payload = JsonDocument.Parse(args.Data);
                    socket.Close();
                }
            };

            // Error processing
            socket.OnError += (sender, args) =>
            {
                if (socket.ReadyState == WebSocketState.Open)
                    socket.Close();
            };

            socket.OnClose += (sender, args) =>
            {
                Assert.True(!payload.RootElement.ValueKind.Equals(JsonValueKind.Null) 
                            && !payload.RootElement.ValueKind.Equals(JsonValueKind.Undefined));
            };

            await socket.ConnectAsync();
            
            // Ensure test doesn't terminate until we receive a payload.
            do { } while (!socket.ReadyState.Equals(WebSocketState.Closed)); // If is not closed, continue.
            
            Assert.True(payload.RootElement.ValueKind.Equals(JsonValueKind.Array));
        }
    }
}