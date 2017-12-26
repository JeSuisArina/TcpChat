using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;

namespace ClientWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string username;
        static TcpClient tcpclient;
        public static NetworkStream stream;
        static Server server;
        private int _port;
        List<string> usersOnline = new List<string>();

        public MainWindow()
        {
            InitializeComponent();            
        }      

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            username = userName.Text.ToString();
            server = new Server();
            tcpclient = new TcpClient();            
            string _ipAddress = ipAddress.Text.ToString();
            _port = Convert.ToInt32(port.Text);
            try
            {
                
                tcpclient.Connect(_ipAddress, _port);
                stream = tcpclient.GetStream();
                if (tcpclient.Connected)
                {
                    string message = "Соединение с сервером установлено...";
                    ListBoxItem item = new ListBoxItem();
                    item.Content = message;
                    ChatItself.Items.Add(item);
                    message = username;
                    byte[] data = Encoding.Unicode.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                    
                    // запускаем новый поток для получения данных
                    Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                    receiveThread.Start(); //старт потока                    
                    Dispatcher.BeginInvoke(new Action(delegate ()
                    {
                        
                        ListBoxItem item1 = new ListBoxItem();
                        item1.Content = "Добро пожаловать, " + message;
                        ChatItself.Items.Add(item1);
                    }));
                    
                    btnConnect.Visibility = Visibility.Hidden;
                    userName.IsEnabled = false;

                    ReceiveUsers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        public void SubmitMes_Click(object sender, RoutedEventArgs e)
        {
            string message = TextMessage.Text.ToString();
            TextMessage.Clear();
            byte[] data = Encoding.Unicode.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }

        public void ReceiveUsers()
        {
            UdpClient receiver = new UdpClient(_port); // UdpClient для получения данных
            IPEndPoint remoteIp = null; // адрес входящего подключения
            try
            {
                //while (true)
                //{
                    byte[] data = receiver.Receive(ref remoteIp); // получаем данные
                    string message = Encoding.Unicode.GetString(data);
                    Dispatcher.BeginInvoke(new Action(delegate ()
                    {                        
                        ListBoxItem item1 = new ListBoxItem();
                        item1.Content = message;
                        UsersOnline.Items.Add(item1);                        
                    }));
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                receiver.Close();
            } 
        }

        public void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[64]; // буфер для получаемых данных
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));                        
                    }
                    while (stream.DataAvailable);

                    string message = builder.ToString();
                    Dispatcher.BeginInvoke(new Action(delegate ()
                    {
                        ListBoxItem item = new ListBoxItem();
                        item.Content = message;
                        ChatItself.Items.Add(item);//вывод сообщения
                    }));
                }

                catch
                {
                    Dispatcher.BeginInvoke(new Action(delegate ()
                    {
                        ListBoxItem item = new ListBoxItem();
                        item.Content = "Подключение прервано!";
                        ChatItself.Items.Add(item);
                    }));

                    Disconnect();
                }
            }
        }

        private void Quit_Click(object sender, RoutedEventArgs e)        
        {
            Disconnect();
        }

        static void Disconnect()
        {
            if (stream != null)
                stream.Close();//отключение потока
            if (tcpclient != null)
                tcpclient.Close();//отключение клиента
            Environment.Exit(0); //завершение процесса
        }

        private void userName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (userName.Text != null)
            {
                btnConnect.IsEnabled = true;
            }
        }
    }
}
