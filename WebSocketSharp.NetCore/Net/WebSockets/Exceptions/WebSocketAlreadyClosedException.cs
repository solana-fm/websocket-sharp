using System;

namespace WebSocketSharp.NetCore.Net.WebSockets.Exceptions
{
    public class WebSocketAlreadyClosedException : Exception
    {
        public WebSocketAlreadyClosedException() { }

        public WebSocketAlreadyClosedException(string name)
            : base(string.Format($"The websocket is already closed: {name}", name))
        {

        }
    }
}