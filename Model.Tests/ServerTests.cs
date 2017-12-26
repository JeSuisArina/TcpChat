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
    class ServerTests
    {
        [TestCase]
        public void AddConnectionTest()
        {
            Server server = new Server();
            int count1 = server.clients.Count();
            TcpClient tcpclient = new TcpClient();
            Client client = new Client(tcpclient, server);
            server.AddConnection(client);
            int count2 = server.clients.Count();
            Assert.AreNotEqual(count1, count2);
            client = server.clients.Last();
            server.RemoveConnection(client.Id);
        }

        [TestCase]
        public void RemoveConnectionTest()
        {
            Server server = new Server();
            TcpClient tcpclient = new TcpClient();
            Client client = new Client(tcpclient, server);
            server.AddConnection(client);
            string id = server.clients.Last().Id;
            int count1 = server.clients.Count();
            server.RemoveConnection(id);
            int count2 = server.clients.Count();
            Assert.AreNotEqual(count1, count2);

        }

        // тест в бесконечном цикле
        //[TestCase] 
        //public void ListenTest()
        //{
        //    Server server = new Server();
        //    TcpClient tcpclient = new TcpClient();
        //    Client client = new Client(tcpclient, server);
        //    server.AddConnection(client);
        //    server.Port(8888);
        //    server.Listen(); 
        //    Assert.NotNull(server.tcpListener);
        //}

        //[TestCase]
        //public void BroadcastMessageTest()
        //{
        //    Server server = new Server();
        //    TcpClient tcpclient = new TcpClient();
        //    Client client = new Client(tcpclient, server);
        //    server.AddConnection(client);
        //    string id = server.clients.Last().Id;
        //    string message = "message";            
        //    //server.BroadcastMessage(message, id);
        //    string getMes = client.GetMessage();
        //    Assert.NotNull(id);
        //}

        //[TestCase]
        //public void DisconnectTest()
        //{
        //    Server server = new Server();
        //    TcpClient tcpclient = new TcpClient();
        //    Client client = new Client(tcpclient, server);
        //    server.AddConnection(client);
        //    server.Disconnect();  //при выполнении метода приложение закрывается, не знаю как протестировать это 
        //    Assert.();
        //}
    }
}
