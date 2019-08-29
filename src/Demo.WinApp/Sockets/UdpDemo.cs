using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Demo.WinApp.Sockets
{
    public class UdpDemo
    {
        public void Send()
        {
            var sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //a not exist address
            var ipAddress = IPAddress.Parse("192.168.111.111");
            var endPoint = new IPEndPoint(ipAddress, 11000);
            var text = "Hello";
            var sendBuffer = Encoding.ASCII.GetBytes(text);
            sock.SendTo(sendBuffer, endPoint);
        }

        public static UdpDemo Instance = new UdpDemo();
    }
}
