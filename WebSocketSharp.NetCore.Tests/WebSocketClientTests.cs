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
        }
    }
}