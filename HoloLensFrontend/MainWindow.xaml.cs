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
using System.Net.WebSockets;
using System.Net.Http;
using System.Net;
using System.IO;

namespace HoloLensFrontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> bauteile;
        public List<string> url;
        public TcpClient tcpClient;

        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //TcpClient tcpClient = new TcpClient(ipBox.Text, Int32.Parse(portBox.Text));
                NetworkStream networkStream = this.tcpClient.GetStream();
                string input = inputBox.Text;
                int index = bauteile.FindIndex(b => b == input);
                byte[] byteArray = ASCIIEncoding.ASCII.GetBytes(url[index]);
                networkStream.Write(byteArray);
                networkStream.Flush();
                byte[] bytes = new byte[1024];
                string msg = "";
                if (networkStream.DataAvailable)
                {
                    int count = networkStream.Read(bytes, 0, bytes.Length);
                    while (count > 0)
                    {
                        msg += ASCIIEncoding.ASCII.GetString(bytes, 0, count);
                        if (count >= bytes.Length)
                            count = networkStream.Read(bytes, 0, bytes.Length);
                        else
                            count = 0;
                    }
                    Debug.WriteLine(msg);
                }

                //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://localhost/ziptest.zip");
                //myReq.GetResponse().GetResponseStream();
            }

            catch(SocketException ex)
            {
                connectionBox.Header = "Nicht Verbunden";
                MessageBox.Show(ex.Message, "Verbindungsfehler", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private async Task Listen()
        {
            TcpListener listener = new TcpListener(12021);
            listener.Start();

            while (true)
            {
                TcpClient remoteClient = listener.AcceptTcpClient();
                NetworkStream ns = remoteClient.GetStream();
                byte[] bytes = new byte[1024];
                string msg = "";
                int count = ns.Read(bytes, 0, bytes.Length);
                while (count > 0)
                {
                    msg += ASCIIEncoding.ASCII.GetString(bytes, 0, count);
                    if (count >= bytes.Length)
                        count = ns.Read(bytes, 0, bytes.Length);
                    else
                        count = 0;
                }
                Debug.WriteLine(msg);
            }

        }

        private void inputBox_Initialized(object sender, EventArgs e)
        {
            //File.ReadAllText("Bauteile.csv");

            using (var reader = new StreamReader("./Bauteile.csv", Encoding.UTF7))
            {
                List<string> bauteile = new List<string>();
                List<string> url = new List<string>();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(';');

                    bauteile.Add(values[0]);
                    url.Add(values[1]);
                    
                }
                this.bauteile = bauteile;
                this.url = url;
            }
            inputBox.Text = bauteile[0];
            
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string bauteil = inputBox.Text;
            int index = bauteile.FindIndex(b=> b == bauteil);
            if ((index + 1) < bauteile.Count) inputBox.Text = bauteile[index + 1];
            else inputBox.Text = bauteile[0];
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string bauteil = inputBox.Text;
            int index = bauteile.FindIndex(b => b == bauteil);
            if (index == 0) inputBox.Text = bauteile[bauteile.Count - 1];
            else inputBox.Text = bauteile[index - 1];
        }

        private void connectionBox_Initialized(object sender, EventArgs e)
        {
            Task.Run(() => Listen());
            ipBox.Text = "172.22.100.147";
            portBox.Text = "12021";
            try
            {
                int port = Int32.Parse(portBox.Text);
                this.tcpClient = new TcpClient(ipBox.Text, port);
                connectionBox.Header = "Verbunden";
            }

            catch (SocketException ex)
            {
                connectionBox.Header = "Nicht Verbunden";
                MessageBox.Show(ex.Message, "Verbindungsfehler", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }

        private void connectionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int port = Int32.Parse(portBox.Text);
                this.tcpClient = new TcpClient(ipBox.Text, port);
                connectionBox.Header = "Verbunden";
            }

            catch (SocketException ex)
            {
                connectionBox.Header = "Nicht Verbunden";
                MessageBox.Show(ex.Message, "Verbindungsfehler", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
    }
}
