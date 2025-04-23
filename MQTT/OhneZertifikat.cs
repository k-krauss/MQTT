using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using System.Text;

namespace MQTT
{
    public partial class OhneZertifikat : Form
    {
        private const string url = "test.mosquitto.org";
        private const int port = 1883;

        private IMqttClient mqttClient;
        private IMqttClientOptions mqttOptions;

        public OhneZertifikat()
        {
            InitializeComponent();
        }

        #region Methoden

        private void InitializeMqttClient()
        {
            MqttFactory mqttFactory = new MqttFactory();
            mqttClient = mqttFactory.CreateMqttClient();
            mqttOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(url, port)
                .Build();

            mqttClient.ConnectedHandler = new MqttClientConnectedHandlerDelegate(async e =>
            {
                if (txtLog.InvokeRequired)
                {
                    txtLog.Invoke((MethodInvoker)delegate
                    {
                        txtLog.AppendText("Verbunden mit dem Broker.\r\n");
                    });
                }
                else
                {
                    txtLog.AppendText("Verbunden mit dem Broker.\r\n");
                }
            });

            mqttClient.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate(e =>
            {
                if (txtLog.InvokeRequired)
                {
                    txtLog.Invoke((MethodInvoker)delegate
                    {
                        txtLog.AppendText("Verbindung zum Broker getrennt.\r\n");
                    });
                }
                else
                {
                    txtLog.AppendText("Verbindung zum Broker getrennt.\r\n");
                }
                return Task.CompletedTask;
            });
        }

        private async void SubscribeToTopic(string topic)
        {
            if (mqttClient != null && mqttClient.IsConnected)
            {
                await mqttClient.SubscribeAsync(new MQTTnet.Client.Subscribing.MqttClientSubscribeOptionsBuilder()
                    .WithTopicFilter(topic)
                    .Build());

                InitializeMessageHandler();

                txtLog.AppendText($"Thema '{topic}' abonniert.\r\n");
            }
            else
            {
                txtLog.AppendText("Client ist nicht verbunden. Abonnement fehlgeschlagen.\r\n");
            }
        }

        private void InitializeMessageHandler()
        {
            mqttClient.ApplicationMessageReceivedHandler = new MQTTnet.Client.Receiving.MqttApplicationMessageReceivedHandlerDelegate(e =>
            {
                if (e.ApplicationMessage.Topic != null && e.ApplicationMessage.Payload != null)
                {
                    string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    string topic = e.ApplicationMessage.Topic;

                    if (txtLog.InvokeRequired)
                    {
                        txtLog.Invoke((MethodInvoker)delegate
                        {
                            txtLog.AppendText($"Nachricht empfangen:\r\nThema: {topic}\r\nNachricht: {payload}\r\n");
                        });
                    }
                    else
                    {
                        txtLog.AppendText($"Nachricht empfangen:\r\nThema: {topic}\r\nNachricht: {payload}\r\n");
                    }
                }
            });
        }

        private async void UnsubscribeFromTopic(string topic)
        {
            if (mqttClient != null && mqttClient.IsConnected)
            {
                await mqttClient.UnsubscribeAsync(topic);
                if (txtLog.InvokeRequired)
                {
                    txtLog.Invoke((MethodInvoker)delegate
                    {
                        txtLog.AppendText($"Thema '{topic}' abbestellt.\r\n");
                    });
                }
                else
                {
                    txtLog.AppendText($"Thema '{topic}' abbestellt.\r\n");
                }
            }
            else
            {
                if (txtLog.InvokeRequired)
                {
                    txtLog.Invoke((MethodInvoker)delegate
                    {
                        txtLog.AppendText("Client ist nicht verbunden. Abbestellung fehlgeschlagen.\r\n");
                    });
                }
                else
                {
                    txtLog.AppendText("Client ist nicht verbunden. Abbestellung fehlgeschlagen.\r\n");
                }
            }
        }

        private async void PublishMessage(string topic, string message)
        {
            if (mqttClient != null && mqttClient.IsConnected)
            {
                var mqttMessage = new MQTTnet.MqttApplicationMessageBuilder()
                    .WithTopic(topic)
                    .WithPayload(message)
                    .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                    .Build();

                await mqttClient.PublishAsync(mqttMessage);
                if (txtLog.InvokeRequired)
                {
                    txtLog.Invoke((MethodInvoker)delegate
                    {
                        txtLog.AppendText($"Nachricht an '{topic}' gesendet: {message}\r\n");
                    });
                }
                else
                {
                    txtLog.AppendText($"Nachricht an '{topic}' gesendet: {message}\r\n");
                }
            }
            else
            {
                if (txtLog.InvokeRequired)
                {
                    txtLog.Invoke((MethodInvoker)delegate
                    {
                        txtLog.AppendText("Client ist nicht verbunden. Nachricht konnte nicht gesendet werden.\r\n");
                    });
                }
                else
                {
                    txtLog.AppendText("Client ist nicht verbunden. Nachricht konnte nicht gesendet werden.\r\n");
                }
            }
        }

        #endregion

        #region Events

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            if (mqttClient == null)
            {
                InitializeMqttClient();
            }

            if (!mqttClient.IsConnected)
            {
                try
                {
                    await mqttClient.ConnectAsync(mqttOptions);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                txtLog.AppendText("Client ist bereits verbunden.\r\n");
            }
        }

        private async void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (mqttClient.IsConnected)
            {
                await mqttClient.DisconnectAsync();
            }
            else
            {
                txtLog.AppendText("Client ist bereits getrennt.\r\n");
            }
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            txtLog.Text = String.Empty;
        }

        private void btnSubscribe_Click(object sender, EventArgs e)
        {
            SubscribeToTopic("/mein/mqtt/test");
        }

        private void btnUnsubscribe_Click(object sender, EventArgs e)
        {
            UnsubscribeFromTopic("/mein/mqtt/test");
        }

        private void btnPublish_Click(object sender, EventArgs e)
        {
            PublishMessage("/mein/mqtt/test", "Das ist ein Test");
        }

        #endregion
    }
}