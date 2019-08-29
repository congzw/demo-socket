using Demo.WinApp.Sockets;

namespace Demo.WinApp.UI
{
    public class UpdSendCtrl
    {
        public void Send(CallArgs args)
        {
            var lazySimpleLog = LazySimpleLog<UpdSendCtrl>.Instance;
            for (int i = 0; i < args.Count; i++)
            {
                lazySimpleLog.LogInfo(string.Format(">>> Send Start => {0}", i + 1));
                var udpDemo = UdpDemo.Instance;
                udpDemo.Send();
                lazySimpleLog.LogInfo(string.Format(">>> Send Finish => {0}", i + 1));
            }
        }
    }
}
