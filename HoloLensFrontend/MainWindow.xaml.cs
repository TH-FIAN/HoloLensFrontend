using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Net.Sockets;
using System.Diagnostics;

namespace HoloLensFrontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Task.Run(() => Listen());
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            TcpClient tcpClient = new TcpClient("localhost", 12021);
            NetworkStream networkStream = tcpClient.GetStream();
            string s = "HalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHallo";
            s += "123456891234568912345689123456891234568912345689123456891234568912345689123456891234568912345689123456891234568912345689123456891234568912345689123456891234568912345689123456891234568912345689123456891234568912345689123456891234568912345689123456891234568912345689123456891234568912345689123456891234568912345689123456891234568912345689123456891234568912345689123456891234568912345689123456891234568912345689123456891234568912345689123456891234568912345689123456891234568912345689123456891234568912345689123456891234568912345689123456891234568912345689";
            s += "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz";
            string input = inputBox.Text;
            byte[] byteArray = ASCIIEncoding.ASCII.GetBytes(input);
            networkStream.Write(byteArray);
            


        }

        private async Task Listen()
        {
            TcpListener listener = new TcpListener(12021);
            listener.Start();

            while(true)
            {
                TcpClient remoteClient = listener.AcceptTcpClient();
                NetworkStream ns = remoteClient.GetStream();
                byte[] bytes = new byte[1024];
                string msg="";
                int count = ns.Read(bytes, 0, bytes.Length);
                while (count>0)
                {
                    msg +=ASCIIEncoding.ASCII.GetString(bytes,0,count);
                    if (count >= bytes.Length)
                        count = ns.Read(bytes, 0, bytes.Length);
                    else
                        count = 0;
                }
                Debug.WriteLine(msg);
                Dispatcher.Invoke(() => { messageText.Text = msg; });
                
                
            }

        }

        private void inputBox_Initialized(object sender, EventArgs e)
        {

        }

        private void messageText_Initialized(object sender, EventArgs e)
        {

        }
    }
}
