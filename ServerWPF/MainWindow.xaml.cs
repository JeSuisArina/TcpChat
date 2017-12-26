using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Sockets;
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
using System.Threading;
using Model;

namespace ServerWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        static Server server; // сервер
        static Thread listenThread; // потока для прослушивания
        int port;
        string ipAddress;

        public MainWindow()
        {
            InitializeComponent();
        }        

        private void button_Click(object sender, RoutedEventArgs e)
        {
            
            ListBoxItem item = new ListBoxItem();
            item.Content = "Старт сервера....";
            listBox.Items.Add(item);

            try
            {
                port = Convert.ToInt32(Port.Text);
                ipAddress = IpAddress.Text.ToString();
                server = new Server();
                server.Port(port);
                server.IpAddress(ipAddress);
                listenThread = new Thread(new ThreadStart(server.Listen));
                listenThread.Start(); //старт потока
                Dispatcher.BeginInvoke(new Action(delegate ()
                {
                    ListBoxItem item1 = new ListBoxItem();
                    item1.Content = "Сервер запущен. Ожидание подключений...";
                    listBox.Items.Add(item1);
                }));
                button.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                Dispatcher.BeginInvoke(new Action(delegate ()
                {
                    item = new ListBoxItem();
                    item.Content = ex.Message;
                    listBox.Items.Add(item);
                }));
                server.Disconnect();
            }
        }

        private void buttonDis_Click(object sender, RoutedEventArgs e)
        {
            server.Disconnect();
        }        
    }
}
