using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Windows.Devices.Gpio;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace IoTHubDistanceSensor
{
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer temporizador = new DispatcherTimer();

        GpioPin echoPin = null; //ECHO
        GpioPin triggerPin = null; //TRIGGER

        static DeviceClient deviceClient;
        static string iotHubUri = "demoHubAmin.azure-devices.net";
        static string deviceKey = "11/uS3+aPjTKc9WdWUJG/yueMEqvbIuN8ckfcB2e+y8=";
        static string deviceId = "dispositivo001";

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            deviceClient = DeviceClient.Create(iotHubUri, AuthenticationMethodFactory.CreateAuthenticationWithRegistrySymmetricKey(deviceId, deviceKey), TransportType.Http1);

            InitGpio();

            temporizador.Interval = TimeSpan.FromSeconds(1);
            temporizador.Tick += temporizador_Tick;
            temporizador.Start();
        }

        private void InitGpio()
        {
            var gpioControler = GpioController.GetDefault();

            echoPin = gpioControler.OpenPin(6);
            triggerPin = gpioControler.OpenPin(13);
            echoPin.SetDriveMode(GpioPinDriveMode.Input);
            triggerPin.SetDriveMode(GpioPinDriveMode.Output);
            triggerPin.Write(GpioPinValue.Low);
            var value = triggerPin.Read();
        }

        private void temporizador_Tick(object sender, object e)
        {
            var distance = GetDistance();
            txtDistancia.Text = String.Format("Distancia: {0} cms.", distance);

            EnviarInformaciónAlHub(distance);
        }

        public int GetDistance()
        {
            ManualResetEvent mre = new ManualResetEvent(false);
            mre.WaitOne(500);
            Stopwatch pulseLength = new Stopwatch();

            triggerPin.Write(GpioPinValue.High);
            mre.WaitOne(TimeSpan.FromMilliseconds(0.01));
            triggerPin.Write(GpioPinValue.Low);

            while (echoPin.Read() == GpioPinValue.Low)
            {
            }

            pulseLength.Start();

            while (echoPin.Read() == GpioPinValue.High)
            {
            }
            pulseLength.Stop();

            TimeSpan timeBetween = pulseLength.Elapsed;
            double distance = timeBetween.TotalSeconds * 17000;

            return Convert.ToInt32(distance);
        }

        private async void EnviarInformaciónAlHub(int distance)
        {
            var telemetryDataPoint = new
            {
                deviceId = deviceId,
                distance = distance,
                date = DateTime.Now
            };

            var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
            var message = new Message(Encoding.ASCII.GetBytes(messageString));

            Debug.WriteLine(messageString);
            await deviceClient.SendEventAsync(message);
            //txtEnvioDatos.Text = "Datos enviados";
        }
    }
}
