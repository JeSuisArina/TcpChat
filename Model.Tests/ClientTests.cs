using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Net.Sockets;

namespace Model.Tests
{
    [TestFixture]
    class ClientTests
    {
        //[TestCase]
        //public void ProcessTest()
        //{
        //    ClientWPF.MainWindow mainW = new ClientWPF.MainWindow();
        //    Server server = new Server();
        //    TcpClient tcpclient = new TcpClient();
        //    Client client = new Client(tcpclient, server);
        //    server.AddConnection(client);
        //    client.Process();            
        //    Assert.(client.Process());
        //}

        //[TestCase]
        //public void GetMessageTest()
        //{
        //    Server server = new Server();
        //    TcpClient tcpclient = new TcpClient();
        //    Client client = new Client(tcpclient, server);
        //    server.AddConnection(client);
        //    string message = "message";
        //    string id = server.clients.Last().Id;
        //    server.BroadcastMessage(id, id);
        //    string res = client.GetMessage();
        //    Assert.AreSame(message, res);
        //    server.RemoveConnection(id);
            
        //}


    }
}
