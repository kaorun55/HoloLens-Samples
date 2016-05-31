using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DataReciever
{
    class Program
    {
        class App
        {
            public void Run()
            {
                var listener = new TcpListener(IPAddress.Any, 44444);
                listener.Start();
                Console.WriteLine("listener.Start");

                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Console.WriteLine("AcceptTcpClient");

                    using (var stream = client.GetStream())
                    {
                        var size = new byte[4];
                        var count = stream.Read(size, 0, size.Length);
                        var length = BitConverter.ToInt32(size, 0);
                        Console.WriteLine(string.Format("size({0}) : {1}", count, length));
                        if (length == 0)
                        {
                            break;
                        }

                        var data = new byte[length];
                        count = stream.Read(data, 0, data.Length);
                        Console.WriteLine(string.Format("data({0})", count));

                        var filename = string.Format("{0}.obj", DateTime.Now.ToString("yyyyMMdd-hhmmss"));
                        File.WriteAllBytes(filename, data);
                        client.Close();
                        Console.WriteLine("Write : " + filename);
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            App app = new App();
            app.Run();
        }
    }
}
