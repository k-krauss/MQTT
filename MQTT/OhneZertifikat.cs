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
        private const string url = "test.mosquitto.org";    //URL des MQTT-Brokers
        private const int port = 1883;                      //Port des MQTT-Brokers

        private IMqttClient mqttClient;
        private IMqttClientOptions mqttOptions;

        public OhneZertifikat()
        {
            InitializeComponent();
        }

        #region Methoden

        private void InitializeMqttClient()
        {
            //MqttClient initialisieren
            MqttFactory mqttFactory = new MqttFactory();
            mqttClient = mqttFactory.CreateMqttClient();
            mqttOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(url, port)
                .Build();

            //Wird aufgerufen, wenn der Client eine Verbindung hergestellt hat
            mqttClient.ConnectedHandler = new MqttClientConnectedHandlerDelegate(async e =>
            {
                if (txtLog.InvokeRequired)  //Wird verwendet, da der Aufruf nicht vom UI-Thread erfolgt
                {
                    txtLog.Invoke((MethodInvoker)delegate
                    {
                        txtLog.AppendText("Verbunden mit dem Broker.\r\n\r\n");
                    });
                }
                else
                {
                    txtLog.AppendText("Verbunden mit dem Broker.\r\n\r\n");
                }
            });

            //Wird aufgerufen, wenn der Client die Verbindung getrennt hat
            mqttClient.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate(e =>
            {
                if (txtLog.InvokeRequired)  //Wird verwendet, da der Aufruf nicht vom UI-Thread erfolgt
                {
                    txtLog.Invoke((MethodInvoker)delegate
                    {
                        txtLog.AppendText("Verbindung zum Broker getrennt.\r\n\r\n");
                    });
                }
                else
                {
                    txtLog.AppendText("Verbindung zum Broker getrennt.\r\n\r\n");
                }
                return Task.CompletedTask;
            });
        }
        
        //Abonniert das angegebene Thema
        private async void SubscribeToTopic(string topic)
        {
            if (mqttClient != null && mqttClient.IsConnected)
            {
                await mqttClient.SubscribeAsync(new MQTTnet.Client.Subscribing.MqttClientSubscribeOptionsBuilder()
                    .WithTopicFilter(topic)
                    .Build());

                InitializeMessageHandler();

                txtLog.AppendText($"Thema '{topic}' abonniert.\r\n\r\n");
            }
            else
            {
                txtLog.AppendText("Client ist nicht verbunden. Abonnement fehlgeschlagen.\r\n\r\n");
            }
        }

        //Erstellt einen ApplicationMessageReceivedHandler, um die Empfangenen Nachrichten in die Textbox zu schreiben
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
                            txtLog.AppendText($"Nachricht empfangen:\r\nThema: {topic}\r\nNachricht: {payload}\r\n\r\n");
                        });
                    }
                    else
                    {
                        txtLog.AppendText($"Nachricht empfangen:\r\nThema: {topic}\r\nNachricht: {payload}\r\n\r\n");
                    }
                }
            });
        }

        //Deabonniert das angegebene Thema
        private async void UnsubscribeFromTopic(string topic)
        {
            if (mqttClient != null && mqttClient.IsConnected)
            {
                await mqttClient.UnsubscribeAsync(topic);
                if (txtLog.InvokeRequired)
                {
                    txtLog.Invoke((MethodInvoker)delegate
                    {
                        txtLog.AppendText($"Thema '{topic}' abbestellt.\r\n\r\n");
                    });
                }
                else
                {
                    txtLog.AppendText($"Thema '{topic}' abbestellt.\r\n\r\n");
                }
            }
            else
            {
                if (txtLog.InvokeRequired)
                {
                    txtLog.Invoke((MethodInvoker)delegate
                    {
                        txtLog.AppendText("Client ist nicht verbunden. Abbestellung fehlgeschlagen.\r\n\r\n");
                    });
                }
                else
                {
                    txtLog.AppendText("Client ist nicht verbunden. Abbestellung fehlgeschlagen.\r\n\r\n");
                }
            }
        }

        //Veröffentlicht die angegebene Nachricht, zum angegebenen Thema
        private async void PublishMessage(string topic, string message)
        {
            if (mqttClient != null && mqttClient.IsConnected)
            {
                var mqttMessage = new MqttApplicationMessageBuilder()
                    .WithTopic(topic)
                    .WithPayload(message)
                    .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                    .Build();

                await mqttClient.PublishAsync(mqttMessage);
                if (txtLog.InvokeRequired)
                {
                    txtLog.Invoke((MethodInvoker)delegate
                    {
                        txtLog.AppendText($"Nachricht an '{topic}' gesendet: {message}\r\n\r\n");
                    });
                }
                else
                {
                    txtLog.AppendText($"Nachricht an '{topic}' gesendet: {message}\r\n\r\n");
                }
            }
            else
            {
                if (txtLog.InvokeRequired)
                {
                    txtLog.Invoke((MethodInvoker)delegate
                    {
                        txtLog.AppendText("Client ist nicht verbunden. Nachricht konnte nicht gesendet werden.\r\n\r\n");
                    });
                }
                else
                {
                    txtLog.AppendText("Client ist nicht verbunden. Nachricht konnte nicht gesendet werden.\r\n\r\n");
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
                txtLog.AppendText("Client ist bereits verbunden.\r\n\r\n");
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
                txtLog.AppendText("Client ist bereits getrennt.\r\n\r\n");
            }
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            txtLog.Text = String.Empty;
        }

        private void btnSubscribe_Click(object sender, EventArgs e)
        {
            SubscribeToTopic("/beispiel/temperatur");
        }

        private void btnUnsubscribe_Click(object sender, EventArgs e)
        {
            UnsubscribeFromTopic("/beispiel/temperatur");
        }

        private void btnPublish_Click(object sender, EventArgs e)
        {
            PublishMessage("/beispiel/temperatur", "5.4°C");
        }

        #endregion
    }
}