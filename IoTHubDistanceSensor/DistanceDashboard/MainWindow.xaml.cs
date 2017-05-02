using Microsoft.Azure.Devices;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace DistanceDashboard
{
    public partial class MainWindow : Window
    {
        static ServiceClient serviceClient;
        static string connectionString = "cadena de conexión del hub";
        static string iotHubD2cEndpoint = "messages/events";
        static EventHubClient eventHubClient;
        EventHubReceiver eventHubReceiver;
        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();

            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);

            eventHubClient = EventHubClient.CreateFromConnectionString(connectionString, iotHubD2cEndpoint);
            eventHubReceiver = eventHubClient.GetDefaultConsumerGroup().CreateReceiver("1", DateTime.UtcNow);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            EventData eventData = await eventHubReceiver.ReceiveAsync();
            string incomingData = Encoding.UTF8.GetString(eventData.GetBytes());
            dynamic finalData = JObject.Parse(incomingData);

            int distance = Convert.ToInt16(finalData.distance);
            HandleDistance(distance);
        }

        private void HandleDistance(int distance)
        {
            if (distance > 190)
            {
                rect1.Visibility = Visibility.Visible;
                rect2.Visibility = Visibility.Collapsed;
                rect3.Visibility = Visibility.Collapsed;
                rect4.Visibility = Visibility.Collapsed;
                rect5.Visibility = Visibility.Collapsed;
                rect6.Visibility = Visibility.Collapsed;
                rect7.Visibility = Visibility.Collapsed;
                rect8.Visibility = Visibility.Collapsed;
                rect9.Visibility = Visibility.Collapsed;
                rect10.Visibility = Visibility.Collapsed;
            }
            else if (distance < 189 && distance > 170)
            {
                rect1.Visibility = Visibility.Visible;
                rect2.Visibility = Visibility.Visible;
                rect3.Visibility = Visibility.Collapsed;
                rect4.Visibility = Visibility.Collapsed;
                rect5.Visibility = Visibility.Collapsed;
                rect6.Visibility = Visibility.Collapsed;
                rect7.Visibility = Visibility.Collapsed;
                rect8.Visibility = Visibility.Collapsed;
                rect9.Visibility = Visibility.Collapsed;
                rect10.Visibility = Visibility.Collapsed;
            }
            else if (distance < 169 && distance > 150)
            {
                rect1.Visibility = Visibility.Visible;
                rect2.Visibility = Visibility.Visible;
                rect3.Visibility = Visibility.Visible;
                rect4.Visibility = Visibility.Collapsed;
                rect5.Visibility = Visibility.Collapsed;
                rect6.Visibility = Visibility.Collapsed;
                rect7.Visibility = Visibility.Collapsed;
                rect8.Visibility = Visibility.Collapsed;
                rect9.Visibility = Visibility.Collapsed;
                rect10.Visibility = Visibility.Collapsed;
            }
            else if (distance < 149 && distance > 130)
            {
                rect1.Visibility = Visibility.Visible;
                rect2.Visibility = Visibility.Visible;
                rect3.Visibility = Visibility.Visible;
                rect4.Visibility = Visibility.Visible;
                rect5.Visibility = Visibility.Collapsed;
                rect6.Visibility = Visibility.Collapsed;
                rect7.Visibility = Visibility.Collapsed;
                rect8.Visibility = Visibility.Collapsed;
                rect9.Visibility = Visibility.Collapsed;
                rect10.Visibility = Visibility.Collapsed;
            }
            else if (distance < 129 && distance > 110)
            {
                rect1.Visibility = Visibility.Visible;
                rect2.Visibility = Visibility.Visible;
                rect3.Visibility = Visibility.Visible;
                rect4.Visibility = Visibility.Visible;
                rect5.Visibility = Visibility.Visible;
                rect6.Visibility = Visibility.Collapsed;
                rect7.Visibility = Visibility.Collapsed;
                rect8.Visibility = Visibility.Collapsed;
                rect9.Visibility = Visibility.Collapsed;
                rect10.Visibility = Visibility.Collapsed;
            }
            else if (distance < 109 && distance > 90)
            {
                rect1.Visibility = Visibility.Visible;
                rect2.Visibility = Visibility.Visible;
                rect3.Visibility = Visibility.Visible;
                rect4.Visibility = Visibility.Visible;
                rect5.Visibility = Visibility.Visible;
                rect6.Visibility = Visibility.Visible;
                rect7.Visibility = Visibility.Collapsed;
                rect8.Visibility = Visibility.Collapsed;
                rect9.Visibility = Visibility.Collapsed;
                rect10.Visibility = Visibility.Collapsed;
            }
            else if (distance < 89 && distance > 70)
            {
                rect1.Visibility = Visibility.Visible;
                rect2.Visibility = Visibility.Visible;
                rect3.Visibility = Visibility.Visible;
                rect4.Visibility = Visibility.Visible;
                rect5.Visibility = Visibility.Visible;
                rect6.Visibility = Visibility.Visible;
                rect7.Visibility = Visibility.Visible;
                rect8.Visibility = Visibility.Collapsed;
                rect9.Visibility = Visibility.Collapsed;
                rect10.Visibility = Visibility.Collapsed;
            }
            else if (distance < 69 && distance > 50)
            {
                rect1.Visibility = Visibility.Visible;
                rect2.Visibility = Visibility.Visible;
                rect3.Visibility = Visibility.Visible;
                rect4.Visibility = Visibility.Visible;
                rect5.Visibility = Visibility.Visible;
                rect6.Visibility = Visibility.Visible;
                rect7.Visibility = Visibility.Visible;
                rect8.Visibility = Visibility.Visible;
                rect9.Visibility = Visibility.Collapsed;
                rect10.Visibility = Visibility.Collapsed;
            }
            else if (distance < 49 && distance > 30)
            {
                rect1.Visibility = Visibility.Visible;
                rect2.Visibility = Visibility.Visible;
                rect3.Visibility = Visibility.Visible;
                rect4.Visibility = Visibility.Visible;
                rect5.Visibility = Visibility.Visible;
                rect6.Visibility = Visibility.Visible;
                rect7.Visibility = Visibility.Visible;
                rect8.Visibility = Visibility.Visible;
                rect9.Visibility = Visibility.Visible;
                rect10.Visibility = Visibility.Collapsed;
            }
            else if (distance < 29 && distance > 0)
            {
                rect1.Visibility = Visibility.Visible;
                rect2.Visibility = Visibility.Visible;
                rect3.Visibility = Visibility.Visible;
                rect4.Visibility = Visibility.Visible;
                rect5.Visibility = Visibility.Visible;
                rect6.Visibility = Visibility.Visible;
                rect7.Visibility = Visibility.Visible;
                rect8.Visibility = Visibility.Visible;
                rect9.Visibility = Visibility.Visible;
                rect10.Visibility = Visibility.Visible;
            }
        }
    }
}
