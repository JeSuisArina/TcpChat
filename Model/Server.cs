using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Model
{
    public class Server
    {
        private int _port;
        private IPAddress ipAddress;
        private string _userName;
        public TcpListener tcpListener; // сервер для прослушивания
        public List<Client> clients = new List<Client>(); // все подключения
        Dictionary<string, string> userNames = new Dictionary<string, string>();

        public void AddConnection(Client client)
        {
            clients.Add(client);
            userNames.Add(client.Id, _userName);
            UserNames();
        }
        public void RemoveConnection(string id)
        {
            // получаем по id закрытое подключение
            Client client = clients.FirstOrDefault(c => c.Id == id);
            // и удаляем его из списка подключений
            if (client != null)
            {
                clients.Remove(client);
                userNames.Remove(client.Id);
                UserNames();
            }                
        }

        public void UserNames()
        {
            UdpClient udpclient = new UdpClient();
            udpclient.Connect(ipAddress, _port);
            foreach (var u in userNames)
            {
                string message = String.Format("{0} {1}", u.Key, u.Value);
                byte[] data = Encoding.Unicode.GetBytes(message);
                udpclient.Send(data, data.Length);
            }
        }

        //переопределение номера порта
        public void Port(int port)
        {
            _port = port;
        }
        public void IpAddress(string ipaddress)
        {
            ipAddress = IPAddress.Parse(ipaddress);
        }

        public void UserName(string userName)
        {
            _userName = userName;
        }

        //public string UserName()
        //{
        //    return _userName;
        //}

        public void Listen()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, _port);
                tcpListener.Start();
                
                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    Client client = new Client(tcpClient, this);
                    Thread clientThread = new Thread(new ThreadStart(client.Process));
                    clientThread.Start();
                }
            }
            catch
            {
                Disconnect();
            }
        }

        // трансляция сообщения подключенным клиентам
        public void BroadcastMessage(string message, string id)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            for (int i = 0; i < clients.Count; i++)
            {
                //if (clients[i].Id != id) // если id клиента не равно id отправляющего
                //{
                    clients[i].Stream.Write(data, 0, data.Length); //передача данных
                //}
            }
        }
        // отключение всех клиентов
        public void Disconnect()
        {
            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].Close(); //отключение клиента
            }
            Environment.Exit(0); //завершение процесса
        }
    }
}
