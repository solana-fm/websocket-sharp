using System;
using WebSocketSharp.NetCore.Server;

namespace WebSocketSharp.NetCore.Example3
{
  public class Echo : WebSocketBehavior
  {
    protected override void OnMessage (MessageEventArgs e)
    {
      var name = Context.QueryString["name"];
      Send (!name.IsNullOrEmpty () ? String.Format ("\"{0}\" to {1}", e.Data, name) : e.Data);
    }
  }
}
