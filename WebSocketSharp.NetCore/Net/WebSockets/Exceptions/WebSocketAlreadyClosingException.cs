using System;

namespace WebSocketSharp.NetCore.Net.WebSockets.Exceptions
{
    [Serializable]
    public class WebSocketAlreadyClosingException : Exception
    {
        public WebSocketAlreadyClosingException() { }

        public WebSocketAlreadyClosingException(string name)
            : base(string.Format($"The websocket is already scheduled for closure: {name}", name))
        {

        }
    }
}