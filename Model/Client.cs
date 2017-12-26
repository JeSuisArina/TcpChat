using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Client
    {
        public string Id { get; private set; }
        public NetworkStream Stream { get; private set; }
        public string userName;
        TcpClient client;
        Server server; // объект сервера

        public Client(TcpClient _client, Server _server)
        {
            Id = Guid.NewGuid().ToString();
            client = _client;
            server = _server;
            server.AddConnection(this);
        }        

        public void Process()
        {
            try
            {
                Stream = client.GetStream();
                string message = GetMessage();
                userName = message;
                string time = String.Format("{0}:{1}:{2}", 
                    DateTime.Now.TimeOfDay.Hours, 
                    DateTime.Now.TimeOfDay.Minutes, 
                    DateTime.Now.TimeOfDay.Seconds);
                message = userName + " вошел в чат в " + time;
                // посылаем сообщение о входе в чат всем подключенным пользователям
                server.BroadcastMessage(message, Id);
                // в бесконечном цикле получаем сообщения от клиента
                while (true)
                {
                    try
                    {
                        message = GetMessage();
                        time = String.Format("{0}:{1}:{2}",
                            DateTime.Now.TimeOfDay.Hours,
                            DateTime.Now.TimeOfDay.Minutes,
                            DateTime.Now.TimeOfDay.Seconds);
                        message = String.Format("{0} {1}: {2}", time, userName, message);
                        server.BroadcastMessage(message, Id);
                    }
                    catch
                    {
                        time = String.Format("{0}:{1}:{2}",
                            DateTime.Now.TimeOfDay.Hours,
                            DateTime.Now.TimeOfDay.Minutes,
                            DateTime.Now.TimeOfDay.Seconds);
                        message = String.Format("{0}: покинул чат в {1}", userName, time);
                        server.BroadcastMessage(message, Id);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                // в случае выхода из цикла закрываем ресурсы
                server.RemoveConnection(Id);
                Close();
            }
        }

        // чтение входящего сообщения и преобразование в строку
        public string GetMessage()
        {
            byte[] data = new byte[64]; // буфер для получаемых данных
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (Stream.DataAvailable);

            return builder.ToString();
        }

        // закрытие подключения
        public void Close()
        {
            if (Stream != null)
                Stream.Close();
            if (client != null)
                client.Close();
        }
    }
}
