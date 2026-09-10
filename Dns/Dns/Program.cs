using System;
using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Dns;

class Program
{
    // There's multiple servers that provide DNS service, for simplicity we will stick to one for now
    private static readonly IPAddress RootServer = IPAddress.Parse("198.41.0.4");    
    private const int DNSPort = 53;
    
    public static void Main(string[] args)
    {
        string dnsToResolve = "www.yahoo.com";
        
        using var udpClient = new UdpClient();
        udpClient.Connect(RootServer, 53);
        
        //construct payload
        
        // udpClient.Send(sendBytes, sendBytes.Length, "127.0.0.1", 11000);
        Console.WriteLine("Hello, World!");
    }

    /*
     * 2 bytes ID
     * 2 bytes flags TODO look into what these are, 
     * QDCount
     * ANCount
     * NSCount
     * ARCount
     */
    public static byte[] ConstructPayload()
    {
        var header = new byte[12];
        // https://www.geeksforgeeks.org/computer-networks/dns-message-format/
        
        // byte 0 and 1 are the transaction ID 16 bits
        // 2^16 = 65536
        var transaction = (short) Random.Shared.Next(0, 65535);
        BinaryPrimitives.WriteInt16BigEndian(new Span<byte>(header, 0, 2), transaction);
    }
}