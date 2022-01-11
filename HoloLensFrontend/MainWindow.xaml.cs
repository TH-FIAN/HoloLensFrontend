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
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            TcpClient tcpClient = new TcpClient("172.22.100.151", 12021);
            NetworkStream networkStream = tcpClient.GetStream();
            string s = "Hallo";
            byte[] byteArray = ASCIIEncoding.ASCII.GetBytes(s);
            networkStream.Write(byteArray);
        }
    }
}
